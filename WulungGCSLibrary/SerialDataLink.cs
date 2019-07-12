using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading;

namespace WulungGCSLibrary
{
    public class SerialDataLink : IDisposable
    {
        public SerialDataLink(in string portname) : this(portname,
            SerialLinkRate.B115200,
            SerialParity.None,
            SerialStopBits.One)
        {
        }
        public SerialDataLink(in string portName,
            in SerialLinkRate bitRate,
            in SerialParity parity,
            in SerialStopBits stopBits)
        {
            contextPortName = portName;
            contextPortBitRate = bitRate;
            contextPortParity = parity;
            contextPortStopBits = stopBits;
            contextPort = new SerialPort(contextPortName,
                (int)contextPortBitRate,
                (Parity)(int)contextPortParity,
                8,
                (StopBits)(int)contextPortStopBits)
            {
                Handshake = Handshake.None,
                ReadBufferSize = MaxLength,
                WriteBufferSize = MaxLength,
                ReadTimeout = (int)interFrameSpan.TotalMilliseconds,
                WriteTimeout = (int)interFrameSpan.TotalMilliseconds
            };
        }

        public bool IsRunning
        {
            get
            {
                return rxThread != null;
            }
        }
        public DateTimeOffset StartTimestamp
        {
            get;
            private set;
        }
        public DateTimeOffset LastRxTimestamp
        {
            get;
            private set;
        }
        public uint ValidInboundMessages
        {
            get;
            private set;
        }
        public void Start()
        {
            stopFlag = false;
            rxThreadEvent.Reset();
            rxThread = new Thread(protocolRxHandler)
            {
                IsBackground = true,
                Priority = ThreadPriority.AboveNormal
            };
            rxThread.Start();
            StartTimestamp = DateTimeOffset.UtcNow;
        }
        public void Stop()
        {
            if (!IsRunning)
            {
                return;
            }
            stopFlag = true;
            rxThreadEvent.Set();
            rxThread.Join();
            rxThread = null;
            receiveableFrames = null;
            StartTimestamp = DateTimeOffset.MinValue;
            ValidInboundMessages = 0;
        }
        public bool SendMesage(in byte[] message)
        {
            bool retVal = false;

            if (Monitor.TryEnter(sendingLock, interFrameSpan) && contextPort != null && contextPort.IsOpen)
            {
                try
                {
                    txBufferL2Length = message.Length;
                    txBufferL2 = message;
                    contextPort.BaseStream.Write(txBufferL2, 0, txBufferL2Length);
                    contextPort.BaseStream.Flush();
                    retVal = true;
                }
                catch
                {
                    retVal = false;
                }
                finally
                {
                    Monitor.Exit(sendingLock);
                }
            }

            return retVal;
        }
        public static byte[] ReceiveData;

        const byte MaxLength = 0x80;
        const byte Length = 99;
        const byte MessageIdPosition = 3;
        const byte HeaderLength = 6;
        const byte ChecksumLength = 2;
        const byte SyncCode0 = 0x4C;
        const byte SyncCode1 = 0x4F;

        private volatile bool stopFlag = false;
        private readonly TimeSpan interFrameSpan = TimeSpan.FromMilliseconds(100);
        private readonly object sendingLock = new object();
        private readonly ManualResetEventSlim rxThreadEvent = new ManualResetEventSlim(false);
        private byte[] txBufferL2 = new byte[MaxLength];
        private int txBufferL2Length = 0;
        private Thread rxThread = null;
        private SerialPort contextPort = null;
        private Dictionary<byte, ProtoFrame> receiveableFrames = null;
        private string contextPortName = string.Empty;
        private SerialLinkRate contextPortBitRate = SerialLinkRate.B115200;
        private SerialParity contextPortParity = SerialParity.None;
        private SerialStopBits contextPortStopBits = SerialStopBits.One;
        
        private void protocolRxHandler()
        {
            
            byte[] rxBufferL2 = new byte[MaxLength];
            byte rxBufferL2Position = 0;
            int tempRead0 = -1;
            int tempRead1 = -1;
            ushort tempPCH = 0;
            UInt16T rxPCH = 0;
            byte tempDataLength = 0;
            byte tempFrameLength = 0;

            while (!stopFlag)
            {
                if (contextPort == null) //Make a new serial connection if there is no serial connection
                {
                    try
                    {
                        contextPort = new SerialPort(contextPortName, (int)contextPortBitRate,
                                        (Parity)(int)contextPortParity, 8, (StopBits)(int)contextPortStopBits)
                        {
                            Handshake = Handshake.None,
                            ReadBufferSize = MaxLength,
                            WriteBufferSize = MaxLength,
                            ReadTimeout = (int)interFrameSpan.TotalMilliseconds,
                            WriteTimeout = (int)interFrameSpan.TotalMilliseconds
                        };
                    }
                    catch
                    {
                        contextPort = null;
                    }
                }
                if (contextPort != null && !contextPort.IsOpen) //Open the serial connection port if there is a serial connection but still closed
                {
                    try
                    {
                        contextPort.Open();
                    }
                    catch
                    {
                        contextPort = null;
                    }
                }
                if (contextPort != null && contextPort.IsOpen) //Read serial data if the serial connection is open
                {
                    if (rxBufferL2Position == MaxLength) //If the position of the buffer reachs the maximum length, than restart the position into 0
                    {
                        rxBufferL2Position = 0;
                    }

                    try //try to read the serial data then build the the received byte serial
                    {
                        try //read the serial into an integer variable
                        {
                            tempRead0 = contextPort.BaseStream.ReadByte();
                        }
                        catch
                        {
                            tempRead0 = -1;
                        }

                        if (tempRead0 >= 0x00 && tempRead0 <= 0xFF) //if the value is lies between 0 to 255 then process it
                        {
                            if (rxBufferL2Position == 0 && tempRead0 == SyncCode0) //if the position is at the first array and the recorded data is matched with the first syncode, then process it
                            {
                                tempRead1 = contextPort.BaseStream.ReadByte();
                                if (tempRead1 == SyncCode1) //if the second byte is the second syncode, then process it
                                {
                                    rxBufferL2[0] = SyncCode0;
                                    rxBufferL2[1] = SyncCode1;
                                    rxBufferL2Position = 2;
                                }
                            }

                            else if (rxBufferL2Position == 0 && tempRead0 != SyncCode0) //if the recorded byte is not same as the syncode, then wait
                            {
                                rxThreadEvent.Wait(1);
                            }

                            else //if the buffer position is 2 or above then process it
                            {
                                rxBufferL2[rxBufferL2Position++] = (byte)tempRead0;

                                if (rxBufferL2Position == rxBufferL2[Length])
                                {
                                    tempFrameLength = rxBufferL2[Length];
                                    tempDataLength = (byte)(tempFrameLength - HeaderLength - ChecksumLength);
                                    rxPCH[1] = rxBufferL2[tempFrameLength - 2];
                                    rxPCH[0] = rxBufferL2[tempFrameLength - 1];
                                    tempPCH = 0;

                                    ReceiveData = rxBufferL2;                 
                                    
                                    rxBufferL2Position = 0;
                                }
                            }
                        }
                        else
                        {
                            rxThreadEvent.Wait(1);
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    stopFlag = true;
                    rxThreadEvent.Set();
                    if (contextPort != null)
                    {
                        contextPort.Dispose();
                        contextPort = null;
                    }
                    if (rxThread != null)
                    {
                        rxThread = null;
                    }
                    if (receiveableFrames != null)
                    {
                        receiveableFrames = null;
                    }
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }

    public enum SerialLinkRate : int
    {
        B19200 = 19200,
        B9600 = 9600,
        B115200 = 115200
    }

    public enum SerialStopBits : int
    {
        One = 1,
        Two = 2
    }

    public enum SerialParity : int
    {
        None = 0,
        Odd = 1,
        Even = 2
    }
}
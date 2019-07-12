using System;

namespace WulungGCSLibrary
{
    public class ProtoFrame
    {
        public const byte MaxLength = 0x80;
        public const byte Length = 99;
        public const byte MessageIdPosition = 3;
        public const byte HeaderLength = 6;
        public const byte ChecksumLength = 2;
        public const byte SyncCode0 = 0x4C;
        public const byte SyncCode1 = 0x4F;

        public ProtoFrame(byte length, byte messageId)
        {
            DataLength = (byte)(length - HeaderLength - ChecksumLength);
            rawFrame[0] = SyncCode0;
            rawFrame[1] = SyncCode1;
            rawFrame[2] = length;
            rawFrame[3] = messageId;
            UInt16T checksum = 0;
            for (int i = 0; i < (HeaderLength - 1); i++)
            {
                checksum += rawFrame[i];
            }
            rawFrame[4] = checksum[0];
            checksum += rawFrame[4];
            rawFrame[rawFrame[2] - 2] = checksum[1];
            rawFrame[rawFrame[2] - 1] = checksum[0];
        }

        public ProtoFrame(ProtoFrame copySource)
        {
            DataLength = copySource.DataLength;
            for (int i = 0; i < MaxLength; i++)
            {
                rawFrame[i] = copySource.rawFrame[i];
            }
        }

        public UInt8T SC0
        {
            get
            {
                return rawFrame[0];
            }
        }
        public UInt8T SC1
        {
            get
            {
                return rawFrame[1];
            }
        }
        public UInt8T LEN
        {
            get
            {
                return rawFrame[Length];
            }
        }
        public UInt8T MID
        {
            get
            {
                return rawFrame[3];
            }
        }
        public UInt8T HCH
        {
            get
            {
                UInt8T retVal = 0;
                lock (atomicityLock)
                {
                    retVal = rawFrame[4];
                }
                return retVal;
            }
        }
        public UInt8T this[int index]
        {
            get
            {
#if DEBUG
                if (index < 0 || index >= DataLength)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, $"Value \"index\" should be greater equal than 0 and less than {DataLength}");
                }
#endif
                UInt8T retVal;
                lock (atomicityLock)
                {
                    retVal = rawFrame[HeaderLength + index];
                }
                return retVal;
            }
            set
            {
#if DEBUG
                if (index < 0 || index >= DataLength)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, $"Value \"index\" should be greater equal than 0 and less than {DataLength}");
                }
#endif
                lock (atomicityLock)
                {
                    byte oldValue = rawFrame[HeaderLength + index];
                    if (oldValue == value)
                    {
                        goto DoNothing;
                    }
                    UInt16T oldChecksum = 0;
                    oldChecksum[1] = rawFrame[rawFrame[2] - 2];
                    oldChecksum[0] = rawFrame[rawFrame[2] - 1];
                    rawFrame[HeaderLength + index] = value;
                    unchecked
                    {
                        if (value > oldValue)
                        {
                            oldChecksum = (ushort)(oldChecksum + (value - oldValue));
                        }
                        else
                        {
                            oldChecksum = (ushort)(oldChecksum - (oldValue - value));
                        }
                    }
                    rawFrame[rawFrame[2] - 2] = oldChecksum[1];
                    rawFrame[rawFrame[2] - 1] = oldChecksum[0];
                }
                DoNothing:
                return;
            }
        }
        public UInt16T PCH
        {
            get
            {
                UInt16T retVal = 0;
                lock (atomicityLock)
                {
                    retVal[0] = rawFrame[rawFrame[2] - 2];
                    retVal[1] = rawFrame[rawFrame[2] - 1];
                }
                return retVal;
            }
        }
        public byte DataLength
        {
            get;
            private set;
        }
        public DateTimeOffset ReceivedTimestamp
        {
            get;
            internal set;
        }

        private readonly object atomicityLock = new object();
        private byte[] rawFrame = new byte[MaxLength];

#if DEBUG
        public override string ToString()
        {
            string retVal = "0x";
            lock (atomicityLock)
            {
                for (int i = 0; i < rawFrame[2]; i++)
                {
                    retVal += rawFrame[i].ToString("X2");
                }
            }
            return retVal;
        }
        public string ToString(bool reverse = true)
        {
            string retVal = string.Empty;
            if (reverse)
            {
                retVal = ToString();
            }
            else
            {
                lock (atomicityLock)
                {
                    for (int i = 0; i < rawFrame[2]; i++)
                    {
                        retVal += rawFrame[rawFrame[2] - 1 - i].ToString("X2");
                    }
                }
            }
            return retVal;
        }
#endif
        public bool CopyRawFrameTo(ref byte[] destination, int startIndex = 0)
        {
            bool retVal = false;
            try
            {
                for (int i = 0; i < Length; i++)
                {
                    destination[startIndex + i] = rawFrame[i];
                }
                retVal = true;
            }
            catch
            {
            }
            return retVal;
        }

        public bool CopyFrameTo(ref ProtoFrame destination, bool forceCopy = false)
        {
            bool retVal = false;
            if (forceCopy)
            {
                destination.DataLength = DataLength;
                destination.rawFrame = new byte[MaxLength];
                for (int i = 0; i < MaxLength; i++)
                {
                    destination.rawFrame[i] = rawFrame[i];
                }
                destination.ReceivedTimestamp = ReceivedTimestamp;
                retVal = true;
            }
            else if (destination.MID == MID && destination.LEN == LEN)
            {
                for (int i = 0; i < destination.DataLength; i++)
                {
                    destination[i] = this[i];
                }
                destination.ReceivedTimestamp = ReceivedTimestamp;
                retVal = true;
            }
            else
            {
                retVal = false;
            }
            return retVal;
        }
    }
}
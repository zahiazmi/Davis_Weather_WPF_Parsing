using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WulungGCSLibrary;

namespace WpfApp12
{
    public static class RTData
    {
        #region REAL-TIME DATA

        public static bool IsRunning = false;

        public static DTORawWeatherVal RawWeatherVal = new DTORawWeatherVal();
        public static DTOWeatherVal WeatherVal = new DTOWeatherVal();

        public static string DLStatus = "---";
        public static string TelemetryStatus = "---";

        public static ulong lastTickCmd = 0;
        public static ulong lastTickSnd = 0;
        public static bool IsConnecting = false;


        //public static bool gainIsUpdating = false;
        #endregion

        #region DATALINK
        static SerialDataLink dataLink;

        static System.Timers.Timer dlRcvTimer;

        public static void StartDataLink()
        {
            if (dlRcvTimer == null)
            {
                dlRcvTimer = new System.Timers.Timer(50);
                dlRcvTimer.AutoReset = true;
                dlRcvTimer.Elapsed += DlRcvTimer_Elapsed;
            }
            dlRcvTimer.Stop();

            dataLink = new SerialDataLink(DataLinkSetting.ComPort, DataLinkSetting.LinkRate, DataLinkSetting.Parity, DataLinkSetting.StopBits);

            try
            {
                dataLink.Start();
                IsRunning = true;
                DLStatus = "DL STARTED";
            }
            catch (Exception)
            {
                dlRcvTimer.Stop();
                IsRunning = false;
                DLStatus = "DL FAILED";
            }
       
        }

        public static void StopDataLink()
        {
            if (dlRcvTimer != null) dlRcvTimer.Stop();
            if (dataLink != null)
            {
                dataLink.Stop();
                dataLink = null;
            }
            IsRunning = false;
            DLStatus = "DL STOPPED";
        }


        public static void LoopCmdSend()
        {
            byte[] LoopCmd = {0x4C,0x4F,0x4F,0x50, 0x20, 0x31, 0x30, 0x30, 0x0A };            
            try
            {
                dataLink.SendMesage(LoopCmd);
                dlRcvTimer.Interval = DataLinkSetting.RcvInterval;
                dlRcvTimer.Start();
                DLStatus = "COMMAND SENT";
            }
            catch (Exception)
            {
                DLStatus = "COMMAND ERROR";
            }
        }

        public static UInt16 i=0;
        private static void DlRcvTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            i++;

            #region TELEMETRY
            try
            {
                DTORawWeatherVal newRawWeatherVal = new DTORawWeatherVal();

                newRawWeatherVal.Barometer     = BitConverter.ToUInt16(SerialDataLink.ReceiveData.Skip(7).Take(2).ToArray(),0);
                newRawWeatherVal.InTemperature = BitConverter.ToUInt16(SerialDataLink.ReceiveData.Skip(9).Take(2).ToArray(),0);
                newRawWeatherVal.InHumidity    = SerialDataLink.ReceiveData[11];
                newRawWeatherVal.OutTemperature= BitConverter.ToUInt16(SerialDataLink.ReceiveData.Skip(12).Take(2).ToArray(),0);
                newRawWeatherVal.WindSpeed     = SerialDataLink.ReceiveData[14];
                newRawWeatherVal.WindDirection = BitConverter.ToUInt16(SerialDataLink.ReceiveData.Skip(16).Take(2).ToArray(),0);
                newRawWeatherVal.DewPoint      = BitConverter.ToUInt16(SerialDataLink.ReceiveData.Skip(30).Take(2).ToArray(), 0);
                newRawWeatherVal.OutHumidity   = SerialDataLink.ReceiveData[33];
                newRawWeatherVal.RainRate      = BitConverter.ToUInt16(SerialDataLink.ReceiveData.Skip(41).Take(2).ToArray(),0);
                RawWeatherVal = newRawWeatherVal;

                DLStatus = "DATA RECEIVED";
            }
            catch (Exception)
            {
                DLStatus = "DATA NOT RECEIVED";
            }

            #endregion
            #region PROCESSING RAW DATA


            WeatherVal.Barometer = (float)RawWeatherVal.Barometer/1000/0.02953;
            WeatherVal.InTemperature = ((float)RawWeatherVal.InTemperature / 10 - 32) * 5 / 9;
            WeatherVal.InHumidity = (float)RawWeatherVal.InHumidity;
            WeatherVal.OutTemperature = ((float)RawWeatherVal.OutTemperature / 10 - 32) * 5 / 9;
            WeatherVal.WindSpeed = (float)RawWeatherVal.WindSpeed*1.609;
            WeatherVal.WindDirection = (float)RawWeatherVal.WindDirection;
            WeatherVal.OutHumidity = (float)RawWeatherVal.OutHumidity;
            WeatherVal.RainRate = (float)RawWeatherVal.RainRate;

            double alpha = Math.Log(WeatherVal.OutHumidity / 100) + 17.62 * WeatherVal.OutTemperature / (243.12 + 27);
            WeatherVal.DewPoint = 243.12 * alpha / (17.62 - alpha);
            WeatherVal.WetBulbTemp = ((float)RawWeatherVal.OutTemperature /10 - ((float)RawWeatherVal.OutTemperature /10 - ((float)WeatherVal.DewPoint*9/5+32)) / 3-32)*5/9;

            #endregion
        }


        #endregion

    }

    //

    public static class DataLinkSetting
    {
        public static string ComPort = "COM1";
        public static SerialLinkRate LinkRate = SerialLinkRate.B19200;
        public static SerialParity Parity = SerialParity.None;
        public static SerialStopBits StopBits = SerialStopBits.One;
        public static int RcvInterval = 50; //ms
        public static int CfgReqCount = 3;
        public static int TelemetryTimeout = 1000; // ms

    }   
}

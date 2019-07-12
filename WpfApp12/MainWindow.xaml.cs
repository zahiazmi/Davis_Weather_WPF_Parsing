using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp12
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer UITimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            UITimer.Tick += updateDisplay;
            UITimer.Interval = TimeSpan.FromMilliseconds(100);
            UITimer.Start();
        }

        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            RTData.StartDataLink();
        }

        private void updateDisplay(object sender, EventArgs e)
        {
            TbStatus.Text = RTData.DLStatus;
            TbBarometer.Text = RTData.WeatherVal.Barometer.ToString("0.00");
            TbInTemperature.Text = RTData.WeatherVal.InTemperature.ToString("0.00");
            TbInHumidity.Text = RTData.WeatherVal.InHumidity.ToString();
            TbOutTemperature.Text = RTData.WeatherVal.OutTemperature.ToString("0.00");
            TbDewPoint.Text = RTData.WeatherVal.DewPoint.ToString("0.00");
            TbWetBulbTemp.Text = RTData.WeatherVal.WetBulbTemp.ToString("0.00");
            TbWindSpeed.Text = RTData.WeatherVal.WindSpeed.ToString();
            TbWindDirection.Text = RTData.WeatherVal.WindDirection.ToString();
            TbOutHumidity.Text = RTData.WeatherVal.OutHumidity.ToString();
            TbRainRate.Text = RTData.WeatherVal.RainRate.ToString();
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            RTData.LoopCmdSend();
        }
    }
}

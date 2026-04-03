using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using YukkuriMovieMaker;
using Timer = System.Timers.Timer;

#pragma warning disable WPF0001
namespace YMM4_Earthquake_Plugin {
    /// <summary>
    /// MainEarthquakeWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainEarthquakeWindow : UserControl {

        public MainEarthquakeWindow() {
            InitializeComponent();
            InitWindow();
        }

        public void InitWindow() {
            EarthquakeViewManager earthquakeViewManager = EarthquakeViewManager.GetInstance(this);
            earthquakeViewManager.startEarthquakeTask();
        }

        private void OnClick(object sender, RoutedEventArgs e) {
            //デバック用
            EarthquakeViewManager earthquakeViewManager = EarthquakeViewManager.GetInstance(this);
            earthquakeViewManager.setLatestDate(DateTime.MinValue);
        }

    }
}

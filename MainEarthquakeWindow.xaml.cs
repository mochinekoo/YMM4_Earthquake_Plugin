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

        public static int TASK_DURATION = 3;
        private Timer earthquakeTask_;
        private DateTime latestDate_;

        public MainEarthquakeWindow() {
            InitializeComponent();
            InitWindow();
        }

        public void InitWindow() {
            var apiList = P2PEarthquakeAPI.GetInstance();
            for (int i = 0; i < apiList.Count(); i++) {
                var api = apiList[i];
                var earthquake = api.earthquake;
                var hypocenter = earthquake.hypocenter;
                if (i == 0) {
                    latestDate_ = DateTime.Parse(earthquake.time);
                }

                var grid = CreateEarthquakeGrid(earthquake.time, hypocenter.name, hypocenter.magnitude, earthquake.maxScale);
                EarthquakeListStack.Items.Add(grid);
            }
            startEarthquakeTask();
        }

        private Grid CreateEarthquakeGrid(string time, string name, float magnitude, int maxScale) {
            Grid grid = new Grid();
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.Background = convertColor(maxScale);

            ThemeMode theme = Application.Current.ThemeMode;

            TextBlock upText = new TextBlock();
            upText.Margin = new Thickness(10, 0, 0, 0);
            upText.Text = time;
            upText.Foreground = new SolidColorBrush(Colors.Black);
            Grid.SetColumn(upText, 0);
            Grid.SetRow(upText, 0);
            grid.Children.Add(upText);

            TextBlock downText = new TextBlock();
            downText.Text = name + " " + magnitude.ToString();
            downText.Margin = new Thickness(10, 0, 0, 0);
            downText.Foreground = new SolidColorBrush(Colors.Black);
            Grid.SetColumn(downText, 0);
            Grid.SetRow(downText, 1);
            grid.Children.Add(downText);

            TextBlock maxScaleText = new TextBlock();
            maxScaleText.Margin = new Thickness(0, 0, 10, 0);
            maxScaleText.Text = convertScale(maxScale);
            maxScaleText.Foreground = new SolidColorBrush(Colors.Black);
            maxScaleText.VerticalAlignment = VerticalAlignment.Center;
            maxScaleText.HorizontalAlignment = HorizontalAlignment.Right;
            Grid.SetColumn(maxScaleText, 1);
            Grid.SetRowSpan(maxScaleText, 10);
            grid.Children.Add(maxScaleText);

            return grid;
        }

        public void startEarthquakeTask() {
            if (earthquakeTask_ == null) {
                Timer timer = new Timer(1000 * TASK_DURATION);
                timer.Elapsed += (sender, e) => {
                    Application.Current.Dispatcher.Invoke(() => {
                        P2PEarthquakeAPI firstAPI = P2PEarthquakeAPI.GetInstance()[0];
                        var earthquake = firstAPI.earthquake;
                        var hypocenter = earthquake.hypocenter;
                        DateTime apiGetTime = DateTime.Parse(earthquake.time);
                        if (apiGetTime.ToBinary() > latestDate_.ToBinary()) {
                            var latestGrid = CreateEarthquakeGrid(earthquake.time, hypocenter.name, hypocenter.magnitude, earthquake.maxScale);
                            EarthquakeListStack.Items.Insert(0, latestGrid);
                            latestDate_ = apiGetTime;
                        }
                        System.Diagnostics.Debug.WriteLine((apiGetTime.ToBinary() > latestDate_.ToBinary()) + ":" + apiGetTime.ToBinary() + ":" + latestDate_.ToBinary());
                    });
                };
                earthquakeTask_ = timer;
                earthquakeTask_.Start();
            }
        }

        private void OnClick(object sender, RoutedEventArgs e) {
            //デバック用
            latestDate_ = DateTime.MinValue;
        }

        public void stopEarthquakeTask() {
            if (earthquakeTask_ != null) {
                earthquakeTask_.Stop();
            }
        }


        public string convertScale(int maxScale) {
            if (maxScale == 10) return "震度1";
            else if (maxScale == 20) return "震度2";
            else if (maxScale == 30) return "震度3";
            else if (maxScale == 40) return "震度4";
            else if (maxScale == 45) return "震度5弱";
            else if (maxScale == 50) return "震度5強";
            else if (maxScale == 55) return "震度6弱";
            else if (maxScale == 60) return "震度6強";
            else if (maxScale == 65) return "震度7";
            return "不明";
        }

        public SolidColorBrush convertColor(int maxScale)
        {
            if (maxScale == 10) return new SolidColorBrush(Colors.White);
            else if (maxScale == 20) return new SolidColorBrush(Colors.AliceBlue);
            else if (maxScale == 30) return new SolidColorBrush(Colors.Green);
            else if (maxScale == 40) return new SolidColorBrush(Colors.Yellow);
            else if (maxScale == 45) return new SolidColorBrush(Colors.Orange);
            else if (maxScale == 50) return new SolidColorBrush(Colors.DarkOrange);
            else if (maxScale == 55) return new SolidColorBrush(Colors.Red);
            else if (maxScale == 60) return new SolidColorBrush(Colors.DarkRed);
            else if (maxScale == 65) return new SolidColorBrush(Colors.Purple);
            return new SolidColorBrush(Colors.White);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static YMM4_Earthquake_Plugin.EarthquakeData.EarthquakeInfo;
using Timer = System.Timers.Timer;

#pragma warning disable WPF0001
namespace YMM4_Earthquake_Plugin {
    internal class EarthquakeViewManager {

        public static int TASK_DURATION = 2;
        private static EarthquakeViewManager instance;
        private MainEarthquakeWindow window;
        private DateTime latestDate_; //一番最新の地震情報の時間
        private Timer earthquakeTask_; //地震を取得するタスク

        private EarthquakeViewManager(MainEarthquakeWindow window) {
            this.window = window;
        }

        public static EarthquakeViewManager GetInstance(MainEarthquakeWindow window) {
            if (instance == null) {
                instance = new EarthquakeViewManager(window);
            }
            return instance;
        }

        public ItemsControl GetEarthquakeView() {
            return window.EarthquakeListStack;
        }

        public void AddEarthquakeView(EarthquakeData earthquakeData) {
            var earthquake = earthquakeData.earthquake;
            var hypocenter = earthquake.hypocenter;
            var apiGetTime = DateTime.Parse(earthquake.time);
            var latestGrid = CreateEarthquakeGrid(earthquake.time, hypocenter.name, hypocenter.magnitude, earthquake.maxScale);
            GetEarthquakeView().Items.Insert(0, latestGrid); //一番最初に挿入する
            latestDate_ = apiGetTime;
        }

        public void resetEarthquakeView() {
            GetEarthquakeView().Items.Clear();
            var apiList = P2PEarthquakeAPI.GetInstance();
            for (int i = 0; i < apiList.Count(); i++) {
                var api = apiList[i];
                var earthquake = api.earthquake;
                var hypocenter = earthquake.hypocenter;
                if (i == 0) {
                    latestDate_ = DateTime.Parse(earthquake.time); //一番最新のやつは、フィールドに代入する
                }

                var grid = CreateEarthquakeGrid(earthquake.time, hypocenter.name, hypocenter.magnitude, earthquake.maxScale);
                GetEarthquakeView().Items.Add(grid);
            }
        }

        public void startEarthquakeTask() {
            if (earthquakeTask_ == null) {
                resetEarthquakeView(); //リセットする
                Timer timer = new Timer(1000 * TASK_DURATION);
                timer.Elapsed += (sender, e) => {
                    var firstAPI = P2PEarthquakeAPI.GetInstance()[0]; //一番最新の地震情報
                    var earthquake = firstAPI.earthquake;
                    var hypocenter = earthquake.hypocenter;
                    Application.Current.Dispatcher.Invoke(() => { //UI操作なので、UIスレッドで操作する
                        DateTime apiGetTime = DateTime.Parse(earthquake.time); //一番最新の地震情報の時刻
                        if (apiGetTime.ToBinary() > latestDate_.ToBinary()) { //APIの一番最新 と UIの一番最新　を比較する
                            AddEarthquakeView(firstAPI);
                        }
                        System.Diagnostics.Debug.WriteLine((apiGetTime.ToBinary() > latestDate_.ToBinary()) + ":" + apiGetTime.ToBinary() + ":" + latestDate_.ToBinary());
                    });
                };
                earthquakeTask_ = timer;
                earthquakeTask_.Start();
            }
        }

        public void stopEarthquakeTask() {
            if (earthquakeTask_ != null) {
                earthquakeTask_.Stop();
            }
        }

        public DateTime getLatestDate() {
            return latestDate_;
        }

        public void setLatestDate(DateTime date) {
            latestDate_ = date;
        }

        private static Grid CreateEarthquakeGrid(string time, string name, float magnitude, int maxScale) {
            Grid grid = new Grid();
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.Background = EarthquakeData.convertColor(P2PEarthquakeAPI.convertScaleType(maxScale));

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
            maxScaleText.Text = P2PEarthquakeAPI.convertScaleName(maxScale);
            maxScaleText.Foreground = new SolidColorBrush(Colors.Black);
            maxScaleText.VerticalAlignment = VerticalAlignment.Center;
            maxScaleText.HorizontalAlignment = HorizontalAlignment.Right;
            Grid.SetColumn(maxScaleText, 1);
            Grid.SetRowSpan(maxScaleText, 10);
            grid.Children.Add(maxScaleText);

            return grid;
        }

    }
}

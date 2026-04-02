using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            foreach (var api in P2PEarthquakeAPI.GetInstance()) {
                var earthquake = api.earthquake;
                var hypocenter = earthquake.hypocenter;
                var stackPanel = CreateEarthquakeGrid(earthquake.time, hypocenter.name, hypocenter.magnitude, earthquake.maxScale);
                EarthquakeListStack.Items.Add(stackPanel);
            }
        }

        private Grid CreateEarthquakeGrid(string time, string name, float magnitude, int maxScale) {
            Grid grid = new Grid();
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.Background = convertColor(maxScale);

            TextBlock textBox = new TextBlock();
            textBox.Margin = new Thickness(10, 0, 0, 0);
            textBox.Text = time + "　" + name;
            Grid.SetColumn(textBox, 0);
            Grid.SetRow(textBox, 0);
            grid.Children.Add(textBox);

            TextBlock magnitudeText = new TextBlock();
            magnitudeText.Text = magnitude.ToString();
            magnitudeText.Margin = new Thickness(10, 0, 0, 0);
            Grid.SetColumn(magnitudeText, 0);
            Grid.SetRow(magnitudeText, 1);
            grid.Children.Add(magnitudeText);

            TextBlock maxScaleText = new TextBlock();
            maxScaleText.Margin = new Thickness(0, 0, 10, 0);
            maxScaleText.Text = convertScale(maxScale);
            maxScaleText.VerticalAlignment = VerticalAlignment.Center;
            maxScaleText.HorizontalAlignment = HorizontalAlignment.Right;
            Grid.SetColumn(maxScaleText, 1);
            Grid.SetRowSpan(maxScaleText, 10);
            grid.Children.Add(maxScaleText);

            return grid;
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

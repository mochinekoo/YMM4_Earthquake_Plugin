using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace YMM4_Earthquake_Plugin {
    internal class EarthquakeData {

        public EarthquakeInfo? earthquake { get; set; }
        public EarthquakePoint[]? points { get; set; }

        public class EarthquakeInfo {
            public string domesticTsunami { get; set; }
            public string time { get; set; }
            public int maxScale { get; set; }
            public Hypocenter hypocenter { get; set; }

            public class Hypocenter {
                public int depth { get; set; }
                public float latitude { get; set; }
                public float longitude { get; set; }
                public float magnitude { get; set; }
                public string name { get; set; }
            }
        }

        public class EarthquakePoint {
            public string addr;
            public string pref;
            public int scale;
        }

        public static SolidColorBrush convertColor(EarthquakeType type) {
            if (type == EarthquakeType.ONE) return new SolidColorBrush(Colors.White);
            else if (type == EarthquakeType.TWO) return new SolidColorBrush(Colors.AliceBlue);
            else if (type == EarthquakeType.THREE) return new SolidColorBrush(Colors.Green);
            else if (type == EarthquakeType.FOUR) return new SolidColorBrush(Colors.Yellow);
            else if (type == EarthquakeType.FIVE_LOW) return new SolidColorBrush(Colors.Orange);
            else if (type == EarthquakeType.FIVE_HIGH) return new SolidColorBrush(Colors.DarkOrange);
            else if (type == EarthquakeType.SIX_LOW) return new SolidColorBrush(Colors.Red);
            else if (type == EarthquakeType.SIX_HIGH) return new SolidColorBrush(Colors.DarkRed);
            else if (type == EarthquakeType.SEVEN) return new SolidColorBrush(Colors.Purple);
            return new SolidColorBrush(Colors.White);
        }
    }

    public enum EarthquakeType {
        ZERO,
        ONE,
        TWO,
        THREE,
        FOUR,
        FIVE_LOW,
        FIVE_HIGH,
        SIX_LOW,
        SIX_HIGH,
        SEVEN,
        UNKNOWN
    }
}

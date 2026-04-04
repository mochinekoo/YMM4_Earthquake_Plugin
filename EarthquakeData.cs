using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace YMM4_Earthquake_Plugin {
    internal class EarthquakeData {

        public Issue issue { get; set; }
        public EarthquakeInfo earthquake { get; set; }
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

        public class Issue {
            public string time { get; set; }
            public string type { get; set; }
        }

        public static IssueType? convertIssueType(string type) {
            if (Enum.TryParse<IssueType>(type, out var issue)) {
                return issue;
            }
            return null;
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
        ZERO, //震度0
        ONE, //震度1
        TWO, //震度2
        THREE, //震度3
        FOUR, //震度4
        FIVE_LOW, //震度5弱
        FIVE_HIGH, //震度5強
        SIX_LOW, //震度6弱
        SIX_HIGH, //震度6強
        SEVEN, //震度7
        UNKNOWN //不明
    }

    public enum IssueType {
        ScalePrompt, //震度速報（※3以上）
        Destination, //震源に関する情報（※3以上）
        ScaleAndDestination, //震度・震源に関する情報
        DetailScale, //各地の震度に関する情報
        Foreign, //遠地地震
        Other //その他
    }
}

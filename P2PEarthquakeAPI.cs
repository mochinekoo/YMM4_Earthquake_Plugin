using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Transactions;
using System.Windows.Media;

namespace YMM4_Earthquake_Plugin {
    internal class P2PEarthquakeAPI {

        public static List<EarthquakeData>? GetInstance() {
            using (HttpClient client = new HttpClient()) {
                string url = "https://api.p2pquake.net/v2/history?codes=551";
                HttpResponseMessage responseMessage = client.GetAsync(url).Result;
                if (responseMessage.IsSuccessStatusCode) {
                    string jsonRaw = responseMessage.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<EarthquakeData>>(jsonRaw);
                }
            }
            return null;
        }

        public static string convertScaleName(int scale) {
            if (scale == 10) return "震度1";
            else if (scale == 20) return "震度2";
            else if (scale == 30) return "震度3";
            else if (scale == 40) return "震度4";
            else if (scale == 45) return "震度5弱";
            else if (scale == 50) return "震度5強";
            else if (scale == 55) return "震度6弱";
            else if (scale == 60) return "震度6強";
            else if (scale == 65) return "震度7";
            return "不明";
        }

        public static EarthquakeType convertScaleType(int scale) {
            if (scale == 10) return EarthquakeType.ONE;
            else if (scale == 20) return EarthquakeType.TWO;
            else if (scale == 30) return EarthquakeType.THREE;
            else if (scale == 40) return EarthquakeType.FOUR;
            else if (scale == 45) return EarthquakeType.FIVE_LOW;
            else if (scale == 50) return EarthquakeType.FIVE_HIGH;
            else if (scale == 55) return EarthquakeType.SIX_LOW;
            else if (scale == 60) return EarthquakeType.SIX_HIGH;
            else if (scale == 65) return EarthquakeType.SEVEN;
            return EarthquakeType.UNKNOWN;
        }

    }
}

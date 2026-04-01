using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Transactions;

namespace YMM4_Earthquake_Plugin {
    internal class P2PEarthquakeAPI {

        public static List<P2PEarthquakeAPI>? GetInstance() {
            using (HttpClient client = new HttpClient()) {
                string url = "https://api.p2pquake.net/v2/history?codes=551";
                HttpResponseMessage responseMessage = client.GetAsync(url).Result;
                if (responseMessage.IsSuccessStatusCode) {
                    string jsonRaw = responseMessage.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<P2PEarthquakeAPI>>(jsonRaw);
                }
            }
            return null;
        }

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

    }
}

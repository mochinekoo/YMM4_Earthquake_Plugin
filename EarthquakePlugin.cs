using YukkuriMovieMaker.Plugin;

namespace YMM4_Earthquake_Plugin {
    public class EarthquakePlugin : IToolPlugin {
        public Type ViewModelType => typeof(MainViewModel);
        public Type ViewType => typeof(MainEarthquakeWindow);
        public string Name => "地震プラグイン";
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using YukkuriMovieMaker.Plugin;

namespace YMM4_Earthquake_Plugin {
    internal class PluginSettings : SettingsBase<PluginSettings> {

        public override SettingsCategory Category => SettingsCategory.Tool;

        public override string Name => "地震プラグイン";

        public override bool HasSettingView => true;

        public override object? SettingView {
            get {
                PluginSettingView pluginSettingView = new PluginSettingView();
                return pluginSettingView;
            }
        }

        public override void Initialize() {

        }
    }
}

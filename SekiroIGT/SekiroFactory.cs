using System;
using LiveSplit.Model;
using LiveSplit.UI.Components;
using SekiroIGT;

[assembly: ComponentFactory(typeof(SekiroFactory))]
namespace SekiroIGT{
    class SekiroFactory : IComponentFactory {
        public ComponentCategory Category => ComponentCategory.Timer;

        public string ComponentName => "Sekiro: Shadows Die Twice IGT";
        public string UpdateName => ComponentName;
        public string Description => "Sekiro: Shadows Die Twice In-Game Timer";
        public Version Version => Version.Parse(Config.Version);

        public string XMLURL => $"{UpdateURL}update.xml";
        public string UpdateURL => "https://raw.githubusercontent.com/Jiiks/SekiroIgt/master/";

        public IComponent Create(LiveSplitState state) => new SekiroComponent(state);
    }
}

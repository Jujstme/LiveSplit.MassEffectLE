using System;
using System.Reflection;
using LiveSplit.MassEffectLE;
using LiveSplit.Model;
using LiveSplit.UI.Components;

[assembly: ComponentFactory(typeof(Factory))]

namespace LiveSplit.MassEffectLE
{
    public class Factory : IComponentFactory
    {
        public string ComponentName => "Mass Effect: Legendary Edition - Autosplitter";
        public string Description => "Automatic splitting and load remover";
        public ComponentCategory Category => ComponentCategory.Control;
        public string UpdateName => this.ComponentName;
        public string UpdateURL => "https://raw.githubusercontent.com/Jujstme/LiveSplit.MassEffectLE/master/";
        public Version Version => Assembly.GetExecutingAssembly().GetName().Version;
        public string XMLURL => this.UpdateURL + "Components/update.LiveSplit.MassEffectLE.xml";
        public IComponent Create(LiveSplitState state) { return new Component(state); }
    }
}

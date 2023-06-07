using System.ComponentModel;

namespace ClientPlugin.Config
{
    public interface IPluginConfig : INotifyPropertyChanged
    { 
        float Scale { get; set; }
    }
}
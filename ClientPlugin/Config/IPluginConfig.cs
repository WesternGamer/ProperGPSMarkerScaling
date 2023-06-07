using System.ComponentModel;

namespace ClientPlugin.Config
{
    public interface IPluginConfig : INotifyPropertyChanged
    {

        float Scale { get; set; }

        // TODO: Add config properties here, then extend the implementing classes accordingly
    }
}
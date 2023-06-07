using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

#if !TORCH

namespace ClientPlugin.Config
{
    public class PluginConfig : IPluginConfig
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void SetValue<T>(ref T field, T value, [CallerMemberName] string propName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return;

            field = value;

            OnPropertyChanged(propName);
        }

        private void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if (propertyChanged == null)
                return;

            propertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private float scale = 1f;

        public float Scale
        {
            get => scale;
            set =>SetValue(ref scale, value);
        }
    }
}

#endif
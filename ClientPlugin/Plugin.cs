using System;
using System.IO;
using System.Reflection;
using ClientPlugin.Config;
using ClientPlugin.GUI;
using ClientPlugin.Patches;
using HarmonyLib;
using Sandbox.Game.World;
using Sandbox.Graphics.GUI;
using Sandbox.ModAPI;
using VRage.FileSystem;
using VRage.Input;
using VRage.Plugins;
using VRageMath;

namespace ClientPlugin
{
    // ReSharper disable once UnusedType.Global
    public class Plugin : IPlugin, IDisposable
    {
        public const string Name = "GpsMarkerScaling";
        public static Plugin Instance { get; private set; }
        private static float desiredScale = 1;

        public IPluginConfig Config => config?.Data;
        private PersistentConfig<PluginConfig> config;
        private static readonly string ConfigFileName = $"{Name}.cfg";


        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public void Init(object gameInstance)
        {
            Instance = this;

            // TODO: Put your one time initialization code here.
            //Harmony.DEBUG = true;

            var configPath = Path.Combine(MyFileSystem.UserDataPath, "Storage\\PluginData", ConfigFileName);
            config = PersistentConfig<PluginConfig>.Load(configPath);

            Harmony harmony = new Harmony(Name);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public void Dispose()
        {
            // TODO: Save state and close resources here, called when the game exits (not guaranteed!)
            // IMPORTANT: Do NOT call harmony.UnpatchAll() here! It may break other plugins.

            Instance = null;
        }

        public void Update()
        {
            if (MySession.Static != null && MyInput.Static != null)
            {
                float currScale = Config.Scale;
                // TODO: Put your update code here. It is called on every simulation frame!

                if (MyInput.Static.IsKeyPress(MyKeys.Alt) && MyInput.Static.IsKeyPress(MyKeys.Shift))
                {
                    float delta = MyInput.Static.DeltaMouseScrollWheelValue();
                    if (delta != 0)
                    {
                        desiredScale = MathHelper.Clamp(desiredScale - MathHelper.ToRadians(delta / 100f), 0.018f, 2.5f);
                        MyAPIGateway.Utilities.ShowNotification("Scale: " + Math.Round(currScale, 1), 20);
                    }
                    if (Math.Round(desiredScale, 2) != Math.Round(currScale, 2))
                    {
                        Config.Scale = (float)MathHelper.Lerp(currScale, desiredScale, .15f);
                    }
                }
            }
        }

        // TODO: Uncomment and use this method to create a plugin configuration dialog
        // ReSharper disable once UnusedMember.Global
        public void OpenConfigDialog()
        {
            MyGuiSandbox.AddScreen(new MyPluginConfigDialog());
        }
    }
}
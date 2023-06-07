using HarmonyLib;
using Sandbox.Engine.Platform.VideoMode;
using Sandbox.Game.Gui;
using Sandbox.Game.GUI;
using Sandbox.Game.GUI.HudViewers;
using Sandbox.Game.World;
using Sandbox.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using VRage.Game.Gui;
using VRage.Game;
using VRage.Utils;
using VRageMath;
using Sandbox.Game.Localization;
using VRage;

namespace ClientPlugin.Patches
{
    [HarmonyPatch("Sandbox.Game.GUI.HudViewers.MyHudMarkerRender+PointOfInterest", "Draw")]
    internal static class PointOfInterest_Draw
    {
        private static Vector2 GPSScale { get => new Vector2((float)Math.Round(1920f - (Plugin.Instance.Config.Scale * 1920f / 3)), (float)Math.Round(1080f - (Plugin.Instance.Config.Scale * 1080f / 3))); }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> code = instructions.ToList();

            code[647] = new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(PointOfInterest_Draw), nameof(GetValueX)));
            code[654] = new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(PointOfInterest_Draw), nameof(GetValueY)));
            code[678] = new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(PointOfInterest_Draw), nameof(GetValueY)));
            code[693] = new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(PointOfInterest_Draw), nameof(GetValueX)));
            code[700] = new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(PointOfInterest_Draw), nameof(GetValueY)));
            code[724] = new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(PointOfInterest_Draw), nameof(GetValueY)));

            return code;
        }

        public static float GetValueX()
        {
            return (float)GPSScale.X;
        }

        public static float GetValueY()
        {
            return (float)GPSScale.Y;
        }
    }
}

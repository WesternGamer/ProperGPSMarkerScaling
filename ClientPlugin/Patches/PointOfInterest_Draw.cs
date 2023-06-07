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
        private const int NormalScreenSizeY = 1080;
        private const int DebugSimulatedScreenSizeY = 2160;

        private static Vector2 GPSScale { get => new Vector2((float)Math.Round(1920f - (Plugin.Instance.Config.Scale * 1920f / 3)), (float)Math.Round(1080f - (Plugin.Instance.Config.Scale * 1080f / 3))); }

        private static bool Prefix(object __instance, ref MyHudMarkerRender renderer, ref float alphaMultiplierMarker, ref float alphaMultiplierText, ref float scale, ref bool drawBox)
        {
          

            return true;
        }

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

            //code.Insert(0, new CodeInstruction())

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

        /*private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> code = instructions.ToList();

            code[807].operand = AccessTools.Method(typeof(PointOfInterest_Draw), nameof(PointOfInterest_Draw.LerpDetour));
            code[994].operand = AccessTools.Method(typeof(PointOfInterest_Draw), nameof(PointOfInterest_Draw.LerpDetour));

            //scale = MyGuiManager.GetFullscreenRectangle().Height / NormalScreenSizeY;
            code.Insert(0, new CodeInstruction(OpCodes.Ldarg_3));
            code.Insert(1, new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MyGuiManager), nameof(MyGuiManager.GetFullscreenRectangle))));
            code.Insert(2, new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(Rectangle), nameof(Rectangle.Height))));
            code.Insert(3, new CodeInstruction(OpCodes.Ldc_I4, 0x438)); //1080
            code.Insert(4, new CodeInstruction(OpCodes.Div));
            code.Insert(5, new CodeInstruction(OpCodes.Conv_R4));
            code.Insert(6, new CodeInstruction(OpCodes.Stind_R4));
            code.Insert(6, new CodeInstruction(OpCodes.Ldarg_3));
            code.Insert(6, new CodeInstruction(OpCodes.Ldind_R4));
            code.Insert(6, new CodeInstruction(OpCodes.Ldc_R4, 1));
            code.Insert(6, new CodeInstruction(OpCodes.Clt));
            code.Insert(6, new CodeInstruction(OpCodes.stloc));



            return code;
        }*/


        private static Vector2 LerpDetour(Vector2 value1, Vector2 value2, float amount) 
        {
            float scale = DebugSimulatedScreenSizeY / NormalScreenSizeY;

            if (scale < 1)
                scale = 1;

            amount += scale * amount;

            Vector2 result = default(Vector2);
            result.X = value1.X + (value2.X - value1.X) * amount;
            result.Y = value1.Y + (value2.Y - value1.Y) * amount;
            return result;
        }
    }
}

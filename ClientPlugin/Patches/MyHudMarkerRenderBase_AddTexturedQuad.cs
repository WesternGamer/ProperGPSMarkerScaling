using HarmonyLib;
using System;
using VRage.Game.Gui;
using VRageMath;

namespace ClientPlugin.Patches
{
    [HarmonyPatch("Sandbox.Game.GUI.HudViewers.MyHudMarkerRenderBase", "AddTexturedQuad")]
    [HarmonyPatch(new Type[] { typeof(MyHudTexturesEnum), typeof(Vector2), typeof(Vector2), typeof(Color), typeof(float), typeof(float) })]
    internal static class MyHudMarkerRenderBase_AddTexturedQuad
    {
        private static bool Prefix(ref float halfWidth, ref float halfHeight)
        {
            halfWidth *= Plugin.Instance.Config.Scale;
            halfHeight *= Plugin.Instance.Config.Scale;

            return true;
        }
    }

    [HarmonyPatch("Sandbox.Game.GUI.HudViewers.MyHudMarkerRenderBase", "AddTexturedQuad")]
    [HarmonyPatch(new Type[] { typeof(string), typeof(Vector2), typeof(Vector2), typeof(Color), typeof(float), typeof(float) })]
    internal static class MyHudMarkerRenderBase_AddTexturedQuad_String
    {
        private static bool Prefix(ref float halfWidth, ref float halfHeight)
        {
            halfWidth *= Plugin.Instance.Config.Scale;
            halfHeight *= Plugin.Instance.Config.Scale;

            return true;
        }
    }
}

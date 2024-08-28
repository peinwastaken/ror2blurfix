using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ror2blurfix
{
    [BepInPlugin("com.pein.rorblurfix", "Risk of Rain 2 Blur fix", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static new ManualLogSource Logger;
        public Camera GetCamera() => Camera.main;
        public PostProcessLayer GetPPLayer() => GetCamera().GetComponent<PostProcessLayer>();

        private void Awake()
        {
            Logger = base.Logger;

            var harmony = new Harmony("com.pein.rorblurfix");
            harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(PostProcessLayer), "RenderFinalPass")]
    public class PostProcessLayerOnEnablePatch
    {
        public static PostProcessLayer GetPPLayer() => Camera.main.GetComponentInChildren<PostProcessLayer>();

        static bool Prefix(PostProcessLayer __instance, PostProcessRenderContext context, int releaseTargetAfterUse, int eye)
        {
            __instance.antialiasingMode = PostProcessLayer.Antialiasing.None;
            return true;
        }
    }
}

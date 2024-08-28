using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using RoR2;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ror2blurfix
{
    [BepInPlugin("com.pein.rorblurfix", "Risk of Rain 2 Blur fix", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static new ManualLogSource Logger;

        private void Awake()
        {
            Logger = base.Logger;

            var harmony = new Harmony("com.pein.rorblurfix");
            harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(CameraRigController), "Start")]
    public class CameraControllerPatch
    {
        static void Postfix(CameraRigController __instance)
        {
            Camera sceneCam = __instance.sceneCam;
            PostProcessLayer ppLayer = sceneCam.GetComponent<PostProcessLayer>();
            ppLayer.antialiasingMode = PostProcessLayer.Antialiasing.None;
        }
    }
}

using UMM;
using UnityEngine;
using HarmonyLib;
using System.IO;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace lustforgrey
{
    [UKPlugin("simon.LustForGrey", "A Lust for Grey", "1.0.0", "Removes the Lust Layer's purple hue/changes it to black and white.", true, false)]
    public class ALFG : UKMod
    {
        private static Harmony harmony;

        internal static AssetBundle StraightAssetBundle;

        public override void OnModLoaded()
        {
            Debug.Log("straight lust starting");

            //load the asset bundle
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "lustforgrey";
            {
                StraightAssetBundle = AssetBundle.LoadFromFile(Path.Combine(ModPath(), resourceName));
            }

            //start harmonylib to swap assets
            harmony = new Harmony("simon.LustForGrey");
            harmony.PatchAll();
        }

        public static string ModPath()
        {
            return Assembly.GetExecutingAssembly().Location.Substring(0, Assembly.GetExecutingAssembly().Location.LastIndexOf(@"\"));
        }

        public override void OnModUnload()
        {
            harmony.UnpatchSelf();
            base.OnModUnload();
        }

        public static Light lulight1;
        public static Light lulight2;
        public static Light lulight3;
        public static Light blulight1;
        public static Light blulight2;
        public static Color purpleColor = new Color(1f, 0f, 0.9071f, 1f);
        public static Color lightpurpleColor = new Color(0.895f, 0.5529f, 1f, 1f);

        [HarmonyPatch(typeof(StockMapInfo), "Awake")]
        internal class Patch00
        {
            static void Postfix(StockMapInfo __instance)
            {
                foreach (var levelpic in Resources.FindObjectsOfTypeAll<SpriteRenderer>())
                {
                    if (levelpic.sprite.name == "2-1 In the Air Tonight") //Minos' corpse
                    {
                        levelpic.sprite = StraightAssetBundle.LoadAsset<Sprite>("2-1 pic.png");
                    }
                }
                    foreach (var renderer in Resources.FindObjectsOfTypeAll<Material>())
                {
                    if (renderer.name == "Minos") //Minos' corpse
                    {
                        renderer.mainTexture = StraightAssetBundle.LoadAsset<Texture2D>("straightminos1.png");
                    }
                    if (renderer.name == "Minos2")
                    {
                        renderer.mainTexture = StraightAssetBundle.LoadAsset<Texture2D>("straightminos1.png");
                    }
                    if (renderer.name == "Minos2Eyeless")
                    {
                        renderer.mainTexture = StraightAssetBundle.LoadAsset<Texture2D>("straightminos2.png");
                    }
                    if (renderer.name == "MinosArm 1")
                    {
                        renderer.mainTexture = StraightAssetBundle.LoadAsset<Texture2D>("straightarm1.png");
                    }
                    if (renderer.name == "MinosWire")
                    {
                        renderer.mainTexture = StraightAssetBundle.LoadAsset<Texture2D>("straightwire.png");
                    }
                    if (renderer.name == "Mat_MinosArmWires")
                    {
                        renderer.mainTexture = StraightAssetBundle.LoadAsset<Texture2D>("straightwire.png");
                    }
                    if (renderer.name == "MinosArm")
                    {
                        renderer.mainTexture = StraightAssetBundle.LoadAsset<Texture2D>("straightarm1.png");
                    }
                    if (renderer.name == "MinosArmUnlit")
                    {
                        renderer.mainTexture = StraightAssetBundle.LoadAsset<Texture2D>("straightarm1.png");
                    }
                    if (renderer.name == "Mat_MinosArm_0")
                    {
                        renderer.mainTexture = StraightAssetBundle.LoadAsset<Texture2D>("straightarm1.png");
                    }
                    if (renderer.name == "Mat_MinosArm")
                    {
                        renderer.mainTexture = StraightAssetBundle.LoadAsset<Texture2D>("straightarm1.png");
                    }
                    if (renderer.name == "CityFromAbove")
                    {
                        renderer.mainTexture = StraightAssetBundle.LoadAsset<Texture2D>("straightcity.png");
                    }
                    if (renderer.name == "CityFromAboveTiled")
                    {
                        renderer.mainTexture = StraightAssetBundle.LoadAsset<Texture2D>("straightcity.png"); //City
                    }
                    if (renderer.name == "LustSky")
                    {
                        renderer.mainTexture = StraightAssetBundle.LoadAsset<Texture2D>("straighsky.png"); //Sky
                        foreach (var source in Resources.FindObjectsOfTypeAll<Light>()) //This mess is for the lighting
                        {
                            if (source.color.r >= 1f && source.color.g >= 0f && source.color.b >= 0.9071f && source.color.a >= 1f)
                            {
                                blulight1 = source;
                                blulight1.color = new Color(0.5142f, 0.5726f, 1f, 1f);
                            }
                            if (source.color.r <= 0.895f && source.color.g <= 05529f && source.color.b <= 1f && source.color.a <= 1f)
                            {
                                blulight2 = source;
                                blulight2.color = new Color(0.5142f, 0.5726f, 1f, 1f);
                            }
                            if (source.color.r >= 0.9305f && source.color.g >= 0f && source.color.b >= 1f && source.color.a >= 1f)
                            {
                                blulight1 = source;
                                blulight1.color = new Color(0.5142f, 0.5726f, 1f, 1f);
                            }
                            if (source.color.r <= 0.9305f && source.color.g <= 0f && source.color.b <= 1f && source.color.a <= 1f)
                            {
                                blulight1 = source;
                                blulight1.color = new Color(0.5142f, 0.5726f, 1f, 1f);
                            }
                            if (source.color.r <= 1f && source.color.g <= 0f && source.color.b <= 0.6664f && source.color.a <= 1f)
                            {
                                blulight2 = source;
                                blulight2.color = new Color(0.5142f, 0.5726f, 1f, 1f);
                            }
                            if (source.color.r >= 1f && source.color.g >= 0f && source.color.b >= 0.6664f && source.color.a >= 1f)
                            {
                                blulight2 = source;
                                blulight2.color = new Color(0.5142f, 0.5726f, 1f, 1f);
                            }
                            if (source.name == "Directional Light (4)")
                            {
                                lulight1 = source;
                                lulight1.color = new Color(0.231f, 0.231f, 0.231f, 1);
                            }
                            if (source.name == "Directional Light (5)")
                            {
                                lulight2 = source;
                                lulight2.color = new Color(0.439f, 0.439f, 0.439f, 1);
                            }
                            if (source.name == "Directional Light (6)")
                            {
                                lulight3 = source;
                                lulight3.color = new Color(0.439f, 0.439f, 0.439f, 1);
                            }
                        }

                    }

                }

            }
        }
    }
}

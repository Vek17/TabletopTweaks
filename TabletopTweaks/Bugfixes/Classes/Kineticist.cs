﻿using HarmonyLib;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;

namespace TabletopTweaks.Bugfixes.Classes {
    class Kineticist {
        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch {
            static bool Initialized;

            static void Postfix() {
                if (Initialized) return;
                Initialized = true;
                Main.LogHeader("Patching Kineticist");
                PatchBase();
            }
            static void PatchBase() {
                var ElementalOverflowProgression = Resources.GetBlueprint<BlueprintFeatureBase>("86beb0391653faf43aec60d5ec05b538");
                ElementalOverflowProgression.HideInUI = false;
            }
        }
    }
}

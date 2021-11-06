﻿using HarmonyLib;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Items;
using Kingmaker.Items.Slots;
using Kingmaker.PubSubSystem;
using Kingmaker.TurnBasedMode.Controllers;
using Kingmaker.View.Equipment;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Designers.Mechanics.Recommendations;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using TabletopTweaks.Config;
using TabletopTweaks.Extensions;
using TabletopTweaks.NewComponents;
using TabletopTweaks.NewUnitParts;
using TabletopTweaks.Utilities;
using TurnBased.Controllers;
using static TabletopTweaks.NewUnitParts.UnitPartCustomMechanicsFeatures;

namespace TabletopTweaks.NewContent.Feats {
{
    class QuickDraw {
        public static void AddQuickDraw() {


            var icon = AssetLoader.LoadInternal("Feats", "Icon_QuickDraw.png");
            var QuickDraw = Helpers.CreateBlueprint<BlueprintFeature>("QuickDraw", bp => {
                bp.IsClassFeature = true;
                bp.HideInUI = false;
                bp.ReapplyOnLevelUp = true;
                bp.Ranks = 1;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Feat, FeatureGroup.CombatFeat };
                bp.SetName("Quick Draw");
                bp.SetDescription("You can draw a weapon as a free action instead of as a move action.");
                bp.HideInUI = false;
                bp.m_Icon = icon;
                bp.AddComponent<AddCustomMechanicsFeature>(c => {
                    c.Feature = CustomMechanicsFeature.QuickDraw;
                });
                bp.AddPrerequisite<PrerequisiteStatValue>(c => {
                    c.Stat = Kingmaker.EntitySystem.Stats.StatType.BaseAttackBonus;
                    c.Value = 1;
                });
                bp.AddComponent(Helpers.Create<RecommendationStatMiminum>(c => {
                    c.Stat = StatType.Dexterity;
                    c.MinimalValue = 14;
                }));
                bp.AddComponent(Helpers.Create<FeatureTagsComponent>(c => {
                    c.FeatureTags = FeatureTag.Attack;
                }));
            });

            if (ModSettings.AddedContent.Feats.IsDisabled("QuickDraw")) { return; }
            FeatTools.AddAsFeat(QuickDraw);
        }

        [HarmonyPatch(typeof(UnitViewHandsEquipment), nameof(UnitViewHandsEquipment.HandleEquipmentSlotUpdated))]
        static class UnitViewHandsEquipment_HandleEquipmentSlotUpdated_QuickDraw_Patch {
            static bool Prefix(UnitViewHandsEquipment __instance, HandSlot slot, ItemEntity previousItem) {
                if (!__instance.Active || __instance.GetSlotData(slot) == null) { return true; }

                if (__instance.Owner.CustomMechanicsFeature(CustomMechanicsFeature.QuickDraw)
                    && __instance.InCombat
                    && (__instance.Owner.State.CanAct || __instance.IsDollRoom)
                    && slot.Active) {
                    __instance.ChangeEquipmentWithoutAnimation();
                    return false;
                }
                return true;
            }
        }
        [HarmonyPatch(typeof(UnitViewHandsEquipment), nameof(UnitViewHandsEquipment.HandleEquipmentSetChanged))]
        static class UnitViewHandsEquipment_HandleEquipmentSetChanged_QuickDraw_Patch {
            static bool Prefix(UnitViewHandsEquipment __instance) {
                if (!__instance.Active) { return true; }

                if (__instance.Owner.CustomMechanicsFeature(CustomMechanicsFeature.QuickDraw)
                    && __instance.InCombat
                    && (__instance.Owner.State.CanAct || __instance.IsDollRoom)) {
                    __instance.ChangeEquipmentWithoutAnimation();
                    return false;
                }
                return true;
            }
        }
        [HarmonyPatch(typeof(TurnController), nameof(TurnController.CalculatePredictionForWeaponSetChange))]
        static class TurnController_CalculatePredictionForWeaponSetChange_QuickDraw_Patch {
            static bool Prefix(TurnController __instance, bool state, bool alreadyApplyed) {
                if (state && __instance.SelectedUnit.CustomMechanicsFeature(CustomMechanicsFeature.QuickDraw)) {
                    __instance.GetActionsStates(__instance.SelectedUnit).Clear();
                    __instance.GetActionsStates(__instance.SelectedUnit).Free
                        .SetPrediction(CombatAction.UsageType.ChangeWeapon, CombatAction.ActivityType.Ability, CombatAction.ActivityState.WillBeUsed, null, null);
                    EventBus.RaiseEvent(delegate (IActionsPredictionHandler h) {
                        h.PredictionChanged();
                    }, true);
                    return false;
                }
                return true;
            }
        }
    }
}

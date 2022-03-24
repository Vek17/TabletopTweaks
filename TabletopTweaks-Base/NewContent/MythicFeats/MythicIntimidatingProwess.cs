﻿using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
using TabletopTweaks.Core.NewComponents;
using TabletopTweaks.Core.Utilities;
using TabletopTweaks.Core.Wrappers;
using static TabletopTweaks.Base.Main;

namespace TabletopTweaks.Base.NewContent.MythicFeats {
    static class MythicIntimidatingProwess {
        public static void AddMythicIntimidatingProwess() {
            var IntimidatingProwess = BlueprintTools.GetBlueprint<BlueprintFeature>("d76497bfc48516e45a0831628f767a0f");
            var IntimidatingProwessMythicFeature = Helpers.CreateBlueprint<BlueprintFeature>(TTTContext, "IntimidatingProwessMythicFeature", bp => {
                bp.m_Icon = IntimidatingProwess.m_Icon;
                bp.SetName(TTTContext, "Intimidating Prowess (Mythic)");
                bp.SetDescription(TTTContext, "Your mythic stature makes others uneasy.\n" +
                    "You gain a bonus on Intimidate checks equal to your mythic rank.");
                bp.IsClassFeature = true;
                bp.Ranks = 1;
                bp.Groups = new FeatureGroup[] { FeatureGroup.MythicFeat };
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Stat = StatType.CheckIntimidate;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank
                    };
                });
                bp.AddContextRankConfig(c => {
                    c.m_BaseValueType = ContextRankBaseValueType.MythicLevel;
                    c.m_Progression = ContextRankProgression.AsIs;
                });
                bp.AddComponent<RecalculateOnLevelUp>();
                bp.AddPrerequisiteFeature(IntimidatingProwess);
            });
            if (TTTContext.AddedContent.MythicFeats.IsDisabled("MythicIntimidatingProwess")) { return; }
            FeatTools.AddAsMythicFeat(IntimidatingProwessMythicFeature);
        }
    }
}

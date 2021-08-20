﻿using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.FactLogic;
using TabletopTweaks.Config;
using TabletopTweaks.Extensions;
using TabletopTweaks.Utilities;

namespace TabletopTweaks.NewContent.Races {
    class Gnome {
        public static void AddGnomeHeritage() {
            var KitsuneHeritageSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("ec40cc350b18c8c47a59b782feb91d1f");
            var GnomeRace = Resources.GetBlueprint<BlueprintRace>("ef35a22c9a27da345a4528f0d5889157");
            var DestinyBeyondBirthMythicFeat = Resources.GetBlueprint<BlueprintFeature>("325f078c584318849bfe3da9ea245b9d");

            var GnomeHeritageDefaultFeature = Helpers.CreateBlueprint<BlueprintFeature>("GnomeHeritageDefaultFeature", bp => {
                bp.IsClassFeature = true;
                bp.HideInUI = true;
                bp.Ranks = 1;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Racial };
                bp.SetName("Gnome");
                bp.SetDescriptionTagged("Gnomes are physically weak but surprisingly hardy, and their attitude "
                    + "makes them naturally agreeable. They gain +2 Constitution, +2 Charisma, and –2 Strength.");
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Racial;
                    c.Stat = StatType.Charisma;
                    c.Value = 2;
                }));
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Racial;
                    c.Stat = StatType.Constitution;
                    c.Value = 2;
                }));
                bp.AddComponent(Helpers.Create<AddStatBonusIfHasFact>(c => {
                    c.Descriptor = ModifierDescriptor.Racial;
                    c.Stat = StatType.Strength;
                    c.Value = -2;
                    c.InvertCondition = true;
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { DestinyBeyondBirthMythicFeat.ToReference<BlueprintUnitFactReference>() };
                }));
                bp.AddComponent(Helpers.Create<RecalculateOnFactsChange>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { DestinyBeyondBirthMythicFeat.ToReference<BlueprintUnitFactReference>() };
                }));
            });
            var RemoveDefaultHeritage = Helpers.Create<RemoveFeatureOnApply>(c => {
                c.m_Feature = GnomeHeritageDefaultFeature.ToReference<BlueprintUnitFactReference>();
            });
            var GnomeHeritageClassicFeature = Helpers.CreateBlueprint<BlueprintFeature>("GnomeHeritageClassicFeature", bp => {
                bp.IsClassFeature = true;
                bp.Ranks = 1;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Racial };
                bp.SetName("Gnome");
                bp.SetDescription(GnomeHeritageDefaultFeature.Description);
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Racial;
                    c.Stat = StatType.Charisma;
                    c.Value = 2;
                }));
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Racial;
                    c.Stat = StatType.Constitution;
                    c.Value = 2;
                }));
                bp.AddComponent(Helpers.Create<AddStatBonusIfHasFact>(c => {
                    c.Descriptor = ModifierDescriptor.Racial;
                    c.Stat = StatType.Strength;
                    c.Value = -2;
                    c.InvertCondition = true;
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { DestinyBeyondBirthMythicFeat.ToReference<BlueprintUnitFactReference>() };
                }));
                bp.AddComponent(Helpers.Create<RecalculateOnFactsChange>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { DestinyBeyondBirthMythicFeat.ToReference<BlueprintUnitFactReference>() };
                }));
                bp.AddComponent(RemoveDefaultHeritage);
            });
            var GnomeHeritageArtisanFeature = Helpers.CreateBlueprint<BlueprintFeature>("GnomeHeritageArtisanFeature", bp => {
                bp.IsClassFeature = true;
                bp.Ranks = 1;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Racial };
                bp.SetName("Gnome Artisan");
                bp.SetDescriptionTagged("Some gnomes lack their race’s iconic humor and propensity for pranks, instead devoting nearly "
                    + "all of their time and energy to their crafts. Such gnomes gain +2 Constitution, +2 Intelligence, "
                    + "and -2 Strength. This racial trait alters the gnomes’ ability score modifiers.");
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Racial;
                    c.Stat = StatType.Intelligence;
                    c.Value = 2;
                }));
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Racial;
                    c.Stat = StatType.Constitution;
                    c.Value = 2;
                }));
                bp.AddComponent(Helpers.Create<AddStatBonusIfHasFact>(c => {
                    c.Descriptor = ModifierDescriptor.Racial;
                    c.Stat = StatType.Strength;
                    c.Value = -2;
                    c.InvertCondition = true;
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { DestinyBeyondBirthMythicFeat.ToReference<BlueprintUnitFactReference>() };
                }));
                bp.AddComponent(Helpers.Create<RecalculateOnFactsChange>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { DestinyBeyondBirthMythicFeat.ToReference<BlueprintUnitFactReference>() };
                }));
                bp.AddComponent(RemoveDefaultHeritage);
            });
            var GnomeHeritageSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("GnomeHeritageSelection", bp => {
                bp.IsClassFeature = true;
                bp.Ranks = 1;
                bp.SetName("Gnome Heritage");
                bp.SetDescriptionTagged("The following alternate heritages may be selected for gnome {g|Encyclopedia:Race}race{/g}.");
                bp.m_Icon = KitsuneHeritageSelection.Icon;
                bp.m_Features = new BlueprintFeatureReference[] {
                    GnomeHeritageClassicFeature.ToReference<BlueprintFeatureReference>(),
                    GnomeHeritageArtisanFeature.ToReference<BlueprintFeatureReference>(),
                };
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    GnomeHeritageClassicFeature.ToReference<BlueprintFeatureReference>(),
                    GnomeHeritageArtisanFeature.ToReference<BlueprintFeatureReference>(),
                };
                bp.Group = FeatureGroup.KitsuneHeritage;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Racial };
            });

            if (ModSettings.AddedContent.Races.DisableAll || !ModSettings.AddedContent.Races.Enabled["GnomeHomebrewHeritage"]) { return; }
            GnomeRace.ComponentsArray = new BlueprintComponent[0];
            GnomeRace.AddComponent(Helpers.Create<AddFeatureOnApply>(c => {
                c.m_Feature = GnomeHeritageDefaultFeature.ToReference<BlueprintFeatureReference>();
            }));
            GnomeRace.m_Features = new BlueprintFeatureBaseReference[] { GnomeHeritageSelection.ToReference<BlueprintFeatureBaseReference>() }.AppendToArray(GnomeRace.m_Features);
        }
    }
}

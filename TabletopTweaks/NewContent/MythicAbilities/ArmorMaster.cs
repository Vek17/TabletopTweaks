﻿using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Designers.Mechanics.Facts;
using TabletopTweaks.Config;
using TabletopTweaks.Extensions;
using TabletopTweaks.NewComponents;
using TabletopTweaks.Utilities;

namespace TabletopTweaks.NewContent.MythicAbilities {
    class ArmorMaster {
        public static void AddArmorMaster() {
            var MythicAbilitySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("ba0e5a900b775be4a99702f1ed08914d");
            var ExtraMythicAbilityMythicFeat = Resources.GetBlueprint<BlueprintFeatureSelection>("8a6a511c55e67d04db328cc49aaad2b8");
            var icon = AssetLoader.LoadInternal("Feats", "Icon_ArmorMaster.png");

            var ArmorMasterLightFeature = Helpers.CreateBlueprint<BlueprintFeature>("ArmorMasterLightFeature", bp => {
                bp.IsClassFeature = true;
                bp.ReapplyOnLevelUp = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.MythicAbility };
                bp.Ranks = 1;
                bp.m_Icon = icon;
                bp.SetName("Armor Master (Light Armor)");
                bp.SetDescription("You don’t take an armor check penalty or incur an arcane spell failure chance when wearing light armor or using any shield. " +
                    "In addition, the maximum Dexterity bonus of light armor doesn’t apply to you.");
                bp.AddComponent(Helpers.Create<ArcaneArmorProficiency>(c => {
                    c.Armor = new ArmorProficiencyGroup[] {
                        ArmorProficiencyGroup.Light,
                        ArmorProficiencyGroup.LightShield,
                        ArmorProficiencyGroup.HeavyShield,
                        ArmorProficiencyGroup.TowerShield
                    };
                }));
                bp.AddComponent(Helpers.Create<IgnoreArmorMaxDexBonus>(c => {
                    c.CheckCategory = true;
                    c.Categorys = new ArmorProficiencyGroup[] {
                        ArmorProficiencyGroup.Light
                    };
                }));
                bp.AddComponent(Helpers.Create<IgnoreArmorCheckPenalty>(c => {
                    c.CheckCategory = true;
                    c.Categorys = new ArmorProficiencyGroup[] {
                        ArmorProficiencyGroup.Light,
                        ArmorProficiencyGroup.LightShield,
                        ArmorProficiencyGroup.HeavyShield,
                        ArmorProficiencyGroup.TowerShield
                    };
                }));
            });
            var ArmorMasterMediumFeature = Helpers.CreateBlueprint<BlueprintFeature>("ArmorMasterMediumFeature", bp => {
                bp.IsClassFeature = true;
                bp.ReapplyOnLevelUp = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.MythicAbility };
                bp.Ranks = 1;
                bp.m_Icon = icon;
                bp.SetName("Armor Master (Medium Armor)");
                bp.SetDescription("You don’t take an armor check penalty or incur an arcane spell failure chance when wearing medium armor or using any shield. " +
                    "In addition, the maximum Dexterity bonus of medium armor doesn’t apply to you.");
                bp.AddComponent(Helpers.Create<ArcaneArmorProficiency>(c => {
                    c.Armor = new ArmorProficiencyGroup[] {
                        ArmorProficiencyGroup.Medium,
                        ArmorProficiencyGroup.LightShield,
                        ArmorProficiencyGroup.HeavyShield,
                        ArmorProficiencyGroup.TowerShield
                    };
                }));
                bp.AddComponent(Helpers.Create<IgnoreArmorMaxDexBonus>(c => {
                    c.CheckCategory = true;
                    c.Categorys = new ArmorProficiencyGroup[] {
                        ArmorProficiencyGroup.Medium
                    };
                }));
                bp.AddComponent(Helpers.Create<IgnoreArmorCheckPenalty>(c => {
                    c.CheckCategory = true;
                    c.Categorys = new ArmorProficiencyGroup[] {
                        ArmorProficiencyGroup.Medium,
                        ArmorProficiencyGroup.LightShield,
                        ArmorProficiencyGroup.HeavyShield,
                        ArmorProficiencyGroup.TowerShield
                    };
                }));
                bp.AddPrerequisite(Helpers.Create<PrerequisiteFeature>(c => {
                    c.m_Feature = ArmorMasterLightFeature.ToReference<BlueprintFeatureReference>();
                }));
            });
            var ArmorMasterHeavyFeature = Helpers.CreateBlueprint<BlueprintFeature>("ArmorMasterHeavyFeature", bp => {
                bp.IsClassFeature = true;
                bp.ReapplyOnLevelUp = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.MythicAbility };
                bp.Ranks = 1;
                bp.m_Icon = icon;
                bp.SetName("Armor Master (Heavy Armor)");
                bp.SetDescription("You don’t take an armor check penalty or incur an arcane spell failure chance when wearing heavy armor or using any shield. " +
                    "In addition, the maximum Dexterity bonus of heavy armor doesn’t apply to you.");
                bp.AddComponent(Helpers.Create<ArcaneArmorProficiency>(c => {
                    c.Armor = new ArmorProficiencyGroup[] {
                        ArmorProficiencyGroup.Heavy,
                        ArmorProficiencyGroup.LightShield,
                        ArmorProficiencyGroup.HeavyShield,
                        ArmorProficiencyGroup.TowerShield
                    };
                }));
                bp.AddComponent(Helpers.Create<IgnoreArmorMaxDexBonus>(c => {
                    c.CheckCategory = true;
                    c.Categorys = new ArmorProficiencyGroup[] {
                        ArmorProficiencyGroup.Heavy
                    };
                }));
                bp.AddComponent(Helpers.Create<IgnoreArmorCheckPenalty>(c => {
                    c.CheckCategory = true;
                    c.Categorys = new ArmorProficiencyGroup[] {
                        ArmorProficiencyGroup.Heavy,
                        ArmorProficiencyGroup.LightShield,
                        ArmorProficiencyGroup.HeavyShield,
                        ArmorProficiencyGroup.TowerShield
                    };
                }));
                bp.AddPrerequisite(Helpers.Create<PrerequisiteFeature>(c => {
                    c.m_Feature = ArmorMasterMediumFeature.ToReference<BlueprintFeatureReference>();
                }));
            });

            if (ModSettings.AddedContent.MythicAbilities.IsDisabled("ArmorMaster")) { return; }
            FeatTools.AddAsMythicAbility(ArmorMasterLightFeature);
            FeatTools.AddAsMythicAbility(ArmorMasterMediumFeature);
            FeatTools.AddAsMythicAbility(ArmorMasterHeavyFeature);
        }
    }
}

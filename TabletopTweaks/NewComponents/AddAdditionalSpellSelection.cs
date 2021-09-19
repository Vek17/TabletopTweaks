﻿using Kingmaker.Blueprints;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;
using System;

namespace TabletopTweaks.NewComponents {
    class AddAdditionalSpellSelection : UnitFactComponentDelegate {

        private Spellbook spellbook { get => Owner.DemandSpellbook(SpellCastingClass); }
        private int adjustedMaxLevel {
            get {
                if (!UseOffset) { return MaxSpellLevel; }
                return Math.Max((spellbook?.MaxSpellLevel ?? 0) - SpellLevelOffset, 1);
            }
        }

        public override void OnActivate() {
            LevelUpController controller = Kingmaker.Game.Instance?.LevelUpController;
            if (controller == null) { return; }
            if (spellbook == null) { return; }
            spellSelection = controller.State.DemandSpellSelection(spellbook.Blueprint, SpellList);
            spellSelection.SetExtraSpells(Count, adjustedMaxLevel);
        }
        public override void OnDeactivate() {
            LevelUpController controller = Kingmaker.Game.Instance?.LevelUpController;
            if (controller == null) { return; }
            if (spellbook == null) { return; }
            controller.State.SpellSelections.Remove(spellSelection);
        }

        private SpellSelectionData spellSelection;

        public BlueprintSpellListReference SpellList;
        public BlueprintCharacterClassReference SpellCastingClass;
        public int MaxSpellLevel;
        public bool UseOffset;
        public int SpellLevelOffset;
        public int Count;
    }
}

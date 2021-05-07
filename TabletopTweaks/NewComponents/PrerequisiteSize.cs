﻿using JetBrains.Annotations;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;

namespace TabletopTweaks.NewComponents {
    class PrerequisiteSize: Prerequisite {
        public override bool CheckInternal([CanBeNull] FeatureSelectionState selectionState, [NotNull] UnitDescriptor unit, [CanBeNull] LevelUpState state) {
            return unit.OriginalSize == Size;
        }

        public override string GetUITextInternal(UnitDescriptor unit) {
            return $"Is Size: {Size}";
        }

        public Kingmaker.Enums.Size Size = Kingmaker.Enums.Size.Medium;
    }
}

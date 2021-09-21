﻿using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabletopTweaks.NewComponents
{
    [ComponentName("Resist Energy")]
    [AllowedOn(typeof(BlueprintUnitFact), false)]
    [AllowedOn(typeof(BlueprintUnit), false)]
    [TypeId("030d370d17474a15bd6a1f2dfed82b0c")]
    public class TTResistEnergy : TTAddDamageResistanceEnergy
    {
    }
}

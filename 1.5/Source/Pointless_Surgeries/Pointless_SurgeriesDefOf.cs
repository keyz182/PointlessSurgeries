﻿using RimWorld;
using Verse;

namespace Pointless_Surgeries;

[DefOf]
public static class Pointless_SurgeriesDefOf
{
    // Remember to annotate any Defs that require a DLC as needed e.g.
    // [MayRequireBiotech]
    // public static GeneDef YourPrefix_YourGeneDefName;
    public static ThingDef MedicineEssentialOils;
    public static ThingDef MedicineHealingCrystals;
    public static ThingDef HomeRemedy;
    public static HediffDef SmoothBrainTemp;
    public static HistoryEventDef ManifestDestinyEnacted;

    public static DamageDef BombGiga;

    static Pointless_SurgeriesDefOf()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(Pointless_SurgeriesDefOf));
    }
}
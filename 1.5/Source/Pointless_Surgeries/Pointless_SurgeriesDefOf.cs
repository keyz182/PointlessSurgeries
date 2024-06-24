using RimWorld;
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
    public static ThoughtDef ImplantSuperCucumberThought;
    public static ThoughtDef ThoughtsAndPrayersThought;
    public static ThoughtDef SmoothBrainThought;
    public static HediffDef SmoothBrainTemp;

    static Pointless_SurgeriesDefOf()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(Pointless_SurgeriesDefOf));
    }
}
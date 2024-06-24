using HarmonyLib;
using RimWorld;
using Verse;

namespace Pointless_Surgeries;

[HarmonyPatch(typeof(TendUtility))]
public static class TendUtility_Patch
{
    [HarmonyPatch(nameof(TendUtility.DoTend))]
    [HarmonyPostfix]
    public static void DoTend_Patch(Pawn doctor, Pawn patient, Medicine medicine)
    {
        if (medicine is not PSMedicine psMed) return;

        var chance = 0.001f;
        if (medicine.def == Pointless_SurgeriesDefOf.MedicineEssentialOils)
        {
            patient.health.AddHediff(HediffDefOf.FoodPoisoning);
            chance = 0.01f;
        }
        else if (medicine.def == Pointless_SurgeriesDefOf.MedicineHealingCrystals)
        {
            chance = 0.001f;
        }

        if (!Rand.Chance(chance)) return;
        var text = HealthUtility.FixWorstHealthCondition(patient);
        Messages.Message(text, (Thing)patient, MessageTypeDefOf.PositiveEvent);
    }
}
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
        }else if (medicine.def == Pointless_SurgeriesDefOf.HomeRemedy)
        {
            var alchohol = patient.health.GetOrAddHediff(HediffDefOf.AlcoholHigh);
            alchohol.Severity += 0.016f;
            
            var alcoholToleranceDef = DefDatabase<HediffDef>.GetNamed("AlcoholTolerance");
            if (alcoholToleranceDef != null)
            {
                var alcoholTolerance = patient.health.GetOrAddHediff(alcoholToleranceDef);
                alcoholTolerance.Severity += 0.016f;
            }

            var yayodef = DefDatabase<HediffDef>.GetNamed("YayoHigh");
            var yayo = patient.health.GetOrAddHediff(yayodef);
            yayo.Severity += 0.75f;

            var psychiteToleranceDef = DefDatabase<HediffDef>.GetNamed("PsychiteTolerance");
            if (psychiteToleranceDef != null)
            {
                var psychiteTolerance = patient.health.GetOrAddHediff(psychiteToleranceDef);
                psychiteTolerance.Severity += 0.04f;
            }
            return;
        }

        if (!Rand.Chance(chance)) return;
        var text = HealthUtility.FixWorstHealthCondition(patient);
        Messages.Message(text, (Thing)patient, MessageTypeDefOf.PositiveEvent);
    }
}
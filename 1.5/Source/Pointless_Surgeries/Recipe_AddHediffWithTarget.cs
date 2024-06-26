using System.Collections.Generic;
using RimWorld;
using Verse;

namespace Pointless_Surgeries;

public class Recipe_AddHediffWithTarget: Recipe_AddHediff
{
    public override void ApplyOnPawn(
        Pawn pawn,
        BodyPartRecord part,
        Pawn billDoer,
        List<Thing> ingredients,
        Bill bill)
    {
        if (billDoer != null)
        {
            if (this.CheckSurgeryFail(billDoer, pawn, ingredients, part, bill))
                return;
            TaleRecorder.RecordTale(TaleDefOf.DidSurgery, (object) billDoer, (object) pawn);
        }

        var dinfo = new DamageInfo(null, 0, 0, 0, billDoer);
        
        pawn.health.AddHediff(this.recipe.addsHediff, part, dinfo);
        this.OnSurgerySuccess(pawn, part, billDoer, ingredients, bill);
        if (!this.IsViolationOnPawn(pawn, part, Faction.OfPlayerSilentFail))
            return;
        this.ReportViolation(pawn, billDoer, pawn.HomeFaction, -70);
    }
}
using System.Linq;
using RimWorld;
using Verse;

namespace Pointless_Surgeries;

public class HediffRandomApplier : HediffComp
{
    public HediffCompProperties_RandomApplier Props => (HediffCompProperties_RandomApplier)props;

    public override void CompPostTick(ref float severityAdjustment)
    {
        if(!Rand.Chance(Props.chance))
            return;
        
        var eye = parent.pawn.health.hediffSet.GetNotMissingParts().Where(p => p.def == BodyPartDefOf.Eye).RandomElement();
        
        parent.pawn.health.GetOrAddHediff(Props.hediff, eye);
    }
    
    public override void Notify_PawnUsedVerb(Verb verb, LocalTargetInfo target)
    {
        if(!parent.pawn.IsHashIntervalTick(Props.evalTick))
            return;
        
        if(target.Pawn == null)
            return;
        
        if(!Rand.Chance(Props.verbChance))
            return;

        var targetPawn = target.Pawn;
        if (Rand.Chance(0.5f))
            targetPawn = parent.pawn;

        var eye = targetPawn.health.hediffSet.GetNotMissingParts().Where(p => p.def == BodyPartDefOf.Eye).RandomElement();
        
        targetPawn.health.GetOrAddHediff(Props.hediff, eye);
    }
}
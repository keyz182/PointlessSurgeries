using RimWorld;
using Verse;

namespace Pointless_Surgeries;

public class Projectile_PlaqueBullet: Bullet
{
    public ModExtension_PlaqueBullet Props => def.GetModExtension<ModExtension_PlaqueBullet>();

    public override void Impact(Thing hitThing, bool blockedByShield = false)
    {
        base.Impact(hitThing, blockedByShield);
        if (Props == null || hitThing is not Pawn hitPawn) return;
        
        float rand = Rand.Value;
        if (!(rand <= Props.addHediffChance))
        {
            MoteMaker.ThrowText(hitThing.PositionHeld.ToVector3(), hitThing.MapHeld, "PS_PlaqueBullet_FailureMote".Translate(Props.addHediffChance), 12f);;
            return;
        };
        
        Messages.Message("PS_PlaqueBullet_SuccessMessage".Translate(
            launcher.Label, hitPawn.Label
        ), MessageTypeDefOf.NeutralEvent);
        Hediff plagueOnPawn = hitPawn.health?.hediffSet?.GetFirstHediffOfDef(Props.hediffToAdd);
        float randomSeverity = Rand.Range(0.15f, 0.30f);
        if (plagueOnPawn != null)
        {
            plagueOnPawn.Severity += randomSeverity;
        }
        else
        {
            Hediff hediff = HediffMaker.MakeHediff(Props.hediffToAdd, hitPawn);
            hediff.Severity = randomSeverity;
            hitPawn.health?.AddHediff(hediff);
        }
    }
}
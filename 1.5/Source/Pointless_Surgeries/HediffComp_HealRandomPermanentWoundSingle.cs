using System;
using System.Linq;
using RimWorld;
using Verse;

namespace Pointless_Surgeries;

public class HediffComp_HealRandomPermanentWoundSingle : HediffComp
{
    private HediffCompProperties_HealRandomPermanentWoundSingle Props => (HediffCompProperties_HealRandomPermanentWoundSingle) this.props;
    
    public override void CompPostPostAdd(DamageInfo? dinfo)
    {
        if (Rand.Chance(Props.chance))
            HediffComp_HealPermanentWounds.TryHealRandomPermanentWound(this.Pawn, this.parent.LabelCap);
    }

    public static void TryHealRandomPermanentWound(Pawn pawn, string cause)
    {
        Hediff result;
        if (!pawn.health.hediffSet.hediffs.Where<Hediff>((Func<Hediff, bool>) (hd => hd.IsPermanent() || hd.def.chronic)).TryRandomElement<Hediff>(out result))
            return;
        HealthUtility.Cure(result);
        if (!PawnUtility.ShouldSendNotificationAbout(pawn))
            return;
        Messages.Message((string) "MessagePermanentWoundHealed".Translate((NamedArgument) cause, (NamedArgument) pawn.LabelShort, (NamedArgument) result.Label, pawn.Named("PAWN")), (LookTargets) (Thing) pawn, MessageTypeDefOf.PositiveEvent);
    }
    
}
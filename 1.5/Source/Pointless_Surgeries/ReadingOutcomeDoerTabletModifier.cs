using RimWorld;
using Verse;

namespace Pointless_Surgeries;

public class ReadingOutcomeDoerTabletModifier : BookOutcomeDoer
{
    public BookOutcomeProperties_TabletModifier Props => (BookOutcomeProperties_TabletModifier)props;

    public override bool DoesProvidesOutcome(Pawn reader)
    {
        return true;
    }

    public override bool BenefitDetailsCanChange(Pawn reader = null)
    {
        return true;
    }

    public override void OnBookGenerated(Pawn author = null)
    {
        Book.SetJoyFactor(BookUtility.GetNovelJoyFactorForQuality(Quality));
    }

    public override string GetBenefitsString(Pawn reader = null)
    {
        return string.Format(" - {0}: x{1}", "Pointless_Surgeries_TabletJoyFactor".Translate(),
            Book.JoyFactor.ToStringPercent());
    }

    public override void OnReadingTick(Pawn reader, float factor)
    {
        base.OnReadingTick(reader, factor);

        if (!Parent.IsHashIntervalTick(300)) return;

        if (!reader.health.hediffSet.TryGetHediff(Pointless_SurgeriesDefOf.SmoothBrainTemp, out var hediff))
        {
            hediff = reader.health.AddHediff(Pointless_SurgeriesDefOf.SmoothBrainTemp);
            hediff.Severity = 0;
        }

        hediff.Severity += 0.01f;
    }
}
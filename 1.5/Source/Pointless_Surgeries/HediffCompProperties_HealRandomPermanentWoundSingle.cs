using Verse;

namespace Pointless_Surgeries;

public class HediffCompProperties_HealRandomPermanentWoundSingle : HediffCompProperties
{
    public float chance;
    protected HediffCompProperties_HealRandomPermanentWoundSingle() => this.compClass = typeof (HediffComp_HealRandomPermanentWoundSingle);
}
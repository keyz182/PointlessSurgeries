using Verse;

namespace Pointless_Surgeries;

public class HediffCompProperties_HealRandomPermanentWoundSingle : HediffCompProperties
{
    public float chance;

    public HediffCompProperties_HealRandomPermanentWoundSingle()
    {
        compClass = typeof (HediffComp_HealRandomPermanentWoundSingle);  
    } 
}
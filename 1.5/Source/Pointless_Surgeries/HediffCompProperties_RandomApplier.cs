using Verse;

namespace Pointless_Surgeries;

public class HediffCompProperties_RandomApplier : HediffCompProperties
{
    public HediffDef hediff;
    public float chance;
    public float verbChance;
    public int evalTick;
    
    public HediffCompProperties_RandomApplier()
    {
        this.compClass = typeof(HediffRandomApplier);
    }
}
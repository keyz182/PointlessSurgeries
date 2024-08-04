using RimWorld;
using Verse;

namespace Pointless_Surgeries;

public class HediffCompProperties_JumpingBean : HediffCompProperties
{
    public IntRange JumpFrequency;
    
    public HediffCompProperties_JumpingBean()
    {
        this.compClass = typeof (HediffComp_JumpingBean);
    }
}
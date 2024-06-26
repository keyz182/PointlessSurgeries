using System.Collections.Generic;
using RimWorld;
using Verse;

namespace Pointless_Surgeries;

public class HediffCompProperties_MultipleThoughtSetter : HediffCompProperties
{
    public List<ThoughtDef>thoughts;
    public int moodOffset;
    public FloatRange moodOffsetRange = FloatRange.Zero;
    public bool removeThoughtOnHediffExpiry = true;

    public HediffCompProperties_MultipleThoughtSetter()
    {
        this.compClass = typeof (HediffComp_MultipleThoughtSetter);
    }
}
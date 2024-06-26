using RimWorld;
using Verse;

namespace Pointless_Surgeries;

public class HediffCompProperties_SocialThoughtSetter : HediffCompProperties
{
    public ThoughtDef thought;
    public int moodOffset;
    public FloatRange moodOffsetRange = FloatRange.Zero;
    public bool removeThoughtOnHediffExpiry = true;

    public HediffCompProperties_SocialThoughtSetter()
    {
        this.compClass = typeof (HediffComp_SocialThoughtSetter);
    }
}
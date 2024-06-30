using RimWorld;
using Verse;

namespace Pointless_Surgeries;

public class HediffCompProperties_GiveThoughtOnMapResourceLevel : HediffCompProperties
{
    public ThoughtDef thought;
    public ThingDef resource;
    public int amount;

    public HediffCompProperties_GiveThoughtOnMapResourceLevel()
    {
        this.compClass = typeof (HediffComp_GiveThoughtOnMapResourceLevel);
    }
}
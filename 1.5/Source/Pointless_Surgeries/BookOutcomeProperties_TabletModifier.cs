using System;
using RimWorld;
using Verse;

namespace Pointless_Surgeries;

public class BookOutcomeProperties_TabletModifier : BookOutcomeProperties
{
    public HediffDef hediffDef;

    public override Type DoerClass => typeof(ReadingOutcomeDoerTabletModifier);
}
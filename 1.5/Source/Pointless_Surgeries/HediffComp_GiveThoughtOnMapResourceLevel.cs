using RimWorld;
using UnityEngine;
using Verse;

namespace Pointless_Surgeries;

public class HediffComp_GiveThoughtOnMapResourceLevel: HediffComp
{
    public HediffCompProperties_GiveThoughtOnMapResourceLevel Props =>
        (HediffCompProperties_GiveThoughtOnMapResourceLevel)props;

    public override void CompPostTick(ref float severityAdjustment)
    {
        if (Pawn.IsHashIntervalTick(3600))
        {
            var pawnThought = Pawn.needs?.mood?.thoughts?.memories?.GetFirstMemoryOfDef(Props.thought);
            var count = Pawn.Map.resourceCounter.GetCount(Props.resource);
            
            if (count < Props.amount && pawnThought == null)
            {
                Thought_Memory newThought = (Thought_Memory)ThoughtMaker.MakeThought(Props.thought);
                newThought.permanent = true;
                this.Pawn.needs?.mood?.thoughts?.memories?.TryGainMemory(newThought);
            }else if (count >= Props.amount && pawnThought != null)
            {
                Pawn.needs?.mood?.thoughts?.memories?.RemoveMemory(pawnThought);
            }
        }
    }
}
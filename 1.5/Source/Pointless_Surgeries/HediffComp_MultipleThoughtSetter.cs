using RimWorld;
using UnityEngine;
using Verse;

namespace Pointless_Surgeries;

public class HediffComp_MultipleThoughtSetter : HediffComp
{
    public HediffCompProperties_MultipleThoughtSetter Props
    {
        get => (HediffCompProperties_MultipleThoughtSetter)this.props;
    }

    public void OverrideMoodOffset(int offset)
    {
        foreach (var thought in Props.thoughts)
        {        
            Thought_Memory firstMemoryOfDef =
                this.Pawn.needs?.mood?.thoughts?.memories?.GetFirstMemoryOfDef(thought);
            if (firstMemoryOfDef == null)
                continue;
            firstMemoryOfDef.moodOffset = offset;  
        }

    }

    public override void CompPostPostAdd(DamageInfo? dinfo)
    {
        base.CompPostPostAdd(dinfo);
        this.TryAddMemory();
    }

    public override void CompPostPostRemoved()
    {
        base.CompPostPostRemoved();
        if(!Props.removeThoughtOnHediffExpiry) return;
        foreach (var thought in Props.thoughts)
        {
            this.Pawn.needs?.mood?.thoughts?.memories?.RemoveMemoriesOfDef(thought);
        }
    }

    public override void Notify_Spawned() => this.TryAddMemory();

    private void TryAddMemory()
    {

        foreach (var thought in Props.thoughts)
        {
            if (this.Pawn.needs?.mood?.thoughts?.memories?.GetFirstMemoryOfDef(thought) != null)
                continue;
            Thought_Memory newThought = (Thought_Memory)ThoughtMaker.MakeThought(thought);
            newThought.permanent = true;
            newThought.moodOffset = !(this.Props.moodOffsetRange == FloatRange.Zero)
                ? Mathf.RoundToInt(this.Props.moodOffsetRange.RandomInRange)
                : this.Props.moodOffset;
            this.Pawn.needs?.mood?.thoughts?.memories?.TryGainMemory(newThought);
        }
    }
}
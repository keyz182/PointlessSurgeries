using RimWorld;
using UnityEngine;
using Verse;

namespace Pointless_Surgeries;

public class HediffComp_SocialThoughtSetter: HediffComp
{
    private Pawn otherPawn;
    
    public override void CompExposeData()
    {
        base.CompExposeData();
        Scribe_References.Look(ref otherPawn, "otherPawn");
    }
    
    public HediffCompProperties_SocialThoughtSetter Props
    {
        get => (HediffCompProperties_SocialThoughtSetter) this.props;
    }

    public void OverrideMoodOffset(int offset)
    {
        Thought_Memory firstMemoryOfDef = this.Pawn.needs?.mood?.thoughts?.memories?.GetFirstMemoryOfDef(this.Props.thought);
        if (firstMemoryOfDef == null)
            return;
        firstMemoryOfDef.moodOffset = offset;
    }

    public override void CompPostPostAdd(DamageInfo? dinfo)
    {
        base.CompPostPostAdd(dinfo);
        this.TryAddMemory(dinfo);
    }

    public override void CompPostPostRemoved()
    {
        base.CompPostPostRemoved();
        if(Props.removeThoughtOnHediffExpiry)
            this.Pawn.needs?.mood?.thoughts?.memories?.RemoveMemoriesOfDef(this.Props.thought);
    }

    public override void Notify_Spawned() => this.TryAddMemory();

    private void TryAddMemory(DamageInfo? dinfo = null)
    {
        if (this.Pawn.needs?.mood?.thoughts?.memories?.GetFirstMemoryOfDef(this.Props.thought) != null)
            return;
        Thought_Memory newThought = (Thought_Memory) ThoughtMaker.MakeThought(this.Props.thought);
        newThought.permanent = false;
        newThought.moodOffset = !(this.Props.moodOffsetRange == FloatRange.Zero) ? Mathf.RoundToInt(this.Props.moodOffsetRange.RandomInRange) : this.Props.moodOffset;
        
        var other = (Pawn)dinfo?.Instigator;
        if (other != null)
        {
            otherPawn = other;
        }
        
        this.Pawn.needs?.mood?.thoughts?.memories?.TryGainMemory(newThought, otherPawn);
    }
}
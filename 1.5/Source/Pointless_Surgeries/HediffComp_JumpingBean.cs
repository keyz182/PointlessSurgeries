using RimWorld;
using UnityEngine;
using Verse;

namespace Pointless_Surgeries;

public class HediffComp_JumpingBean: HediffComp
{
    public int NextJumpTick = -1;
    
    public HediffCompProperties_JumpingBean Props
    {
        get => (HediffCompProperties_JumpingBean) this.props;
    }
    
    public override void CompPostTick(ref float severityAdjustment)
    {
        EffecterDef flightEffecterDef = DefDatabase<EffecterDef>.GetNamed("JumpFlightEffect", false);
        SoundDef landingSound = DefDatabase<SoundDef>.GetNamed("Longjump_Land", false);
        
        if (NextJumpTick <= 0)
        {
            Map map = Pawn.Map;
            
            bool selected = Find.Selector.IsSelected(Pawn);
            
            IntVec3 cell = CellFinder.RandomClosewalkCellNear(Pawn.Position, map, 18);
            
            PawnFlyer pawnFlyer = PawnFlyer.MakeFlyer(ThingDefOf.PawnFlyer, Pawn, cell, flightEffecterDef,
                landingSound);
            if (pawnFlyer != null)
                FleckMaker.ThrowDustPuff(Pawn.Position.ToVector3Shifted() + Gen.RandomHorizontalVector(0.5f), map, 2f);
            GenSpawn.Spawn(pawnFlyer, cell, map, WipeMode.Vanish);
            if (selected) Find.Selector.Select(Pawn, false, false);
            NextJumpTick = Props.JumpFrequency.RandomInRange;
        }
        else
        {
            NextJumpTick--;
        }
    }
}
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace Pointless_Surgeries;

public class DamageWorker_GigaExplosion : DamageWorker_AddInjury
{
    
    public override void ExplosionDamageThing(
        Explosion explosion,
        Thing t,
        List<Thing> damagedThings,
        List<Thing> ignoredThings,
        IntVec3 cell)
    {
        if (t.def.category == ThingCategory.Mote || t.def.category == ThingCategory.Ethereal || damagedThings.Contains(t))
            return;
        damagedThings.Add(t);
        if (ignoredThings != null && ignoredThings.Contains(t))
            return;
        if (def == Pointless_SurgeriesDefOf.BombGiga && t.def == ThingDefOf.Fire && !t.Destroyed)
        {
            t.Destroy();
        }
        else
        {
            float angle = !(t.Position == explosion.Position) ? (t.Position - explosion.Position).AngleFlat : (float) Rand.RangeInclusive(0, 359);
            DamageInfo dinfo = new DamageInfo(this.def, (float) explosion.GetDamageAmountAt(cell), explosion.GetArmorPenetrationAt(cell), angle, explosion.instigator, weapon: explosion.weapon, intendedTarget: explosion.intendedTarget);

            BattleLogEntry_ExplosionImpact entryExplosionImpact = (BattleLogEntry_ExplosionImpact) null;
            var pawn = t as Pawn;
            if (pawn != null)
            {
                entryExplosionImpact = new BattleLogEntry_ExplosionImpact(explosion.instigator, t, explosion.weapon, explosion.projectile, this.def);
                Find.BattleLog.Add((LogEntry) entryExplosionImpact);
            }
            DamageResult damage = t.TakeDamage(dinfo);
            damage.AssociateWithLog((LogEntry_DamageResult) entryExplosionImpact);
            if (pawn == null || !damage.wounded || pawn.stances == null)
                return;
            pawn.stances.stagger.StaggerFor(95);
        }
    }
    
    public override IEnumerable<IntVec3> ExplosionCellsToHit(
      IntVec3 center,
      Map map,
      float radius,
      IntVec3? needLOSToCell1 = null,
      IntVec3? needLOSToCell2 = null,
      FloatRange? affectedAngle = null)
    {
      openCells.Clear();
      adjWallCells.Clear();
      
      var cellsInRadius = GenRadial.NumCellsInRadius(radius);
      for (var i = 0; i < cellsInRadius; ++i)
      {
        var cellToCheck = center + GenRadial.RadialPattern[i];
        if (cellToCheck.InBounds(map))
        {
          openCells.Add(cellToCheck);
        }
      }
      
      foreach (var openCell in openCells)
      {
          if (!openCell.Walkable(map)) continue;
          for (var index2 = 0; index2 < 4; ++index2)
          {
              IntVec3 c = openCell + GenAdj.CardinalDirections[index2];
              if (c.InHorDistOf(center, radius) && c.InBounds(map) && !c.Standable(map) && c.GetEdifice(map) != null && !openCells.Contains(c) && !adjWallCells.Contains(c))
                  adjWallCells.Add(c);
          }
      }
      
      foreach (var cell in openCells)
          yield return cell;
      foreach (var cell in adjWallCells)
          yield return cell;
    }

}
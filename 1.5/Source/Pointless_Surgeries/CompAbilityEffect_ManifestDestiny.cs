using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace Pointless_Surgeries;

public class CompAbilityEffect_ManifestDestiny : CompAbilityEffect
{
    public bool HasFired = false;
    public override bool ShouldHideGizmo => HasFired;
    public new CompProperties_AbilityManifestDestiny Props => (CompProperties_AbilityManifestDestiny) this.props;

    public override void PostExposeData()
    {
        base.PostExposeData();
        Scribe_Values.Look(ref HasFired, "HasFired", false);
    }
    public bool ShouldHaveInspectString
    {
        get => ModsConfig.BiotechActive && parent.pawn.RaceProps.IsMechanoid;
    }

    public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
    {
        base.Apply(target, dest);
        
        if  (Props.ExplosionEffect != null)
        {
            Effecter eff = Props.ExplosionEffect.Spawn();
            parent.pawn.Map.effecterMaintainer.AddEffecterToMaintain(
                eff,
                this.parent.pawn.Position.ToVector3().ToIntVec3(),
                600);
        }
        
        GenExplosion.DoExplosion(
            target.Cell, 
            this.parent.pawn.MapHeld, 
            this.Props.Radius,
            Props.DamageType,
            (Thing) this.parent.pawn,
            ignoredThings: [parent.pawn],
            propagationSpeed: 0.5f,
            postExplosionGasType: new GasType?(GasType.ToxGas));

        foreach (var skillRecord in parent.pawn.skills.skills)
        {
            skillRecord.Level = Mathf.Max(skillRecord.Level, 20);
        }

        foreach (var relation in Find.FactionManager.OfPlayer.relations)
        {
            var current = Find.FactionManager.OfPlayer.GoodwillWith(relation.other);
            var newWill = current * -1;

            var change = newWill - current;

            Find.FactionManager.OfPlayer.TryAffectGoodwillWith(
                relation.other,
                change,
                true,
                true,
                Pointless_SurgeriesDefOf.ManifestDestinyEnacted);
        }

        var luci = parent.pawn.health.AddHediff(Props.HighHediff);
        luci.Severity = 1f;
        var lucineed = parent.pawn.health.AddHediff(Props.NeedHediff);
        lucineed.Severity = 1f;
        HasFired = true;
    }

    public override void DrawEffectPreview(LocalTargetInfo target)
    {
        GenDraw.DrawRadiusRing(target.Cell, this.Props.Radius);
    }

    public override string CompInspectStringExtra()
    {
        if (!this.ShouldHaveInspectString)
            return (string) null;
        return this.parent.CanCast ? (string) "AbilityMechSmokepopCharged".Translate() : (string) "AbilityMechSmokepopRecharging".Translate((NamedArgument) this.parent.CooldownTicksRemaining.ToStringTicksToPeriod());
    }
}
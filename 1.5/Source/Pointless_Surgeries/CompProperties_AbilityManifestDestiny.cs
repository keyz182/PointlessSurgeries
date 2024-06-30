using RimWorld;
using Verse;

namespace Pointless_Surgeries;

public class CompProperties_AbilityManifestDestiny : CompProperties_AbilityEffect
{
    public float Radius = 80f;
    public DamageDef DamageType;
    public EffecterDef ExplosionEffect;
    public HediffDef HighHediff;
    public HediffDef NeedHediff;
    
    public CompProperties_AbilityManifestDestiny() => this.compClass = typeof (CompAbilityEffect_ManifestDestiny);
}
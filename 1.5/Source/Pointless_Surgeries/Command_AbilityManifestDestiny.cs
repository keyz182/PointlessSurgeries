using RimWorld;
using UnityEngine;
using Verse;

namespace Pointless_Surgeries;

public class Command_AbilityManifestDestiny : Command_Ability
{

    public Command_AbilityManifestDestiny(Ability ability, Pawn pawn)
        : base(ability, pawn)
    {
        // this.defaultLabel = this.Label;
        // this.icon = ability.def.iconPath == null ? (Texture) this.Ritual.Icon : (Texture) ContentFinder<Texture2D>.Get(ability.def.iconPath);
    }   
}
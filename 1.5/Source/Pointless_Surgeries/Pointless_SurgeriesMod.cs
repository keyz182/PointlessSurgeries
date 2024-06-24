using Verse;
using UnityEngine;
using HarmonyLib;

namespace Pointless_Surgeries;

public class Pointless_SurgeriesMod : Mod
{
    public static Settings settings;

    public Pointless_SurgeriesMod(ModContentPack content) : base(content)
    {
        Log.Message("Hello world from Pointless Surgeries");

        // initialize settings
        settings = GetSettings<Settings>();
#if DEBUG
        Harmony.DEBUG = true;
#endif
        Harmony harmony = new Harmony("keyz182.rimworld.Pointless_Surgeries.main");	
        harmony.PatchAll();
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        base.DoSettingsWindowContents(inRect);
        settings.DoWindowContents(inRect);
    }

    public override string SettingsCategory()
    {
        return "Pointless Surgeries_SettingsCategory".Translate();
    }
}

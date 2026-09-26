using System.Reflection.Emit;
using HarmonyLib;
using StardewValley;

namespace CrabPotFishVariety.Integrations;

public static class EnableCmcmIntegration
{
    public static void AddGmcmOptions(
        this ModEntry modEntry,
        IGenericModConfigMenu configMenu)
    {
        configMenu.AddBoolOption(
            mod: modEntry.ModManifest,
            name: () => modEntry.Helper.Translation.Get("config.option.allow-vanilla-fish"),
            tooltip: () => modEntry.Helper.Translation.Get("config.option.allow-vanilla-fish.tooltip"),
            getValue: () => modEntry.Config.AllowVanillaFish,
            setValue: value => modEntry.Config.AllowVanillaFish = value
        );
    }
}
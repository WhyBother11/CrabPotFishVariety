using HarmonyLib;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.GameData.Locations;
using StardewValley.Objects;

namespace CrabPotFishVariety.Framework.Core;

// is this possible with CP? yes
// do i want to bother with creating fish areas, present and future compat? not really
// (may still need to adjust catch chance because crab pots catch logic, uh)
public static class GameLocationCrabPotPatch
{

    #nullable disable
    private static IMonitor _monitor;
    #nullable restore
    
    public static void Register(Harmony harmony, IMonitor monitor)
    {
        _monitor = monitor;
        _monitor.Log($"Applying prefix Harmony patch {nameof(GameLocation_GetCrabPotFishForTile_Postfix)}");
        harmony.Patch(
            original: AccessTools.Method(typeof(GameLocation), nameof(GameLocation.GetCrabPotFishForTile)),
            postfix: new HarmonyMethod(typeof(GameLocationCrabPotPatch), nameof(GameLocation_GetCrabPotFishForTile_Postfix))
        );
    }

    /// <summary>
    /// Postfix patch for GameLocation::GetCrabPotFishForTile to adjust getting crab pot fish for tile if location is supported by mod.
    /// </summary>
    /// <param name="__instance"><see cref="GameLocation"/> instance.</param>
    /// <param name="__result">Result crab pot fish types.</param>
    private static void GameLocation_GetCrabPotFishForTile_Postfix(
        GameLocation __instance,
        ref IList<string>? __result)
    {
        var crabPotFish = GetCrabPotFishType(__instance);
        if (crabPotFish is null) return;
        
        try
        {
            var result = new List<string>();
            if (__result is not null) result.AddRange(__result);
            
            foreach (var fishType in crabPotFish)
            {
                result.Add(fishType);
            }
            if (!Config.AllowVanillaFish && __instance.ShouldExcludeVanillaCrabPotFishTypes())
            {
                // remove any vanilla crab pot type
                result.Remove("freshwater");
                result.Remove("ocean");
            }

            __result = result;
        }
        catch (Exception e)
        {
            _monitor.Log($"Failed to execute postfix {nameof(GameLocation_GetCrabPotFishForTile_Postfix)} for GameLocation::GetCrabPotFishForTile\nException: {e}", LogLevel.Error);
        }
    }

    /// <summary>
    /// Get mod-specific crab pot fish types based on given <paramref name="location"/>.
    /// </summary>
    /// <param name="location">Crab Pot location.</param>
    /// <returns>List of mod-specific types if <paramref name="location"/> is supported, <c>null</c> otherwise.</returns>
    private static IList<string>? GetCrabPotFishType(GameLocation location)
    {
        return location switch
        {
            _ when location.IsDesert() => new List<string> { CustomCrabPotFishLocation.Desert },
            _ when location.IsSwamp() => new List<string> { CustomCrabPotFishLocation.Swamp },
            _ when location.IsSecretWoods() => new List<string> { CustomCrabPotFishLocation.SecretWoods },
            _ when location.IsBugLand() => new List<string> { CustomCrabPotFishLocation.BugLand },
            _ when location.IsSewers() => new List<string> { CustomCrabPotFishLocation.Sewers },
            _ => null
        };
    }
}

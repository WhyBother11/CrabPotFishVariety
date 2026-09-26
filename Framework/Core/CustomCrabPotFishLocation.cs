using StardewValley;
using StardewValley.Locations;

namespace CrabPotFishVariety.Framework.Core;

public static class CustomCrabPotFishLocation
{
    public static readonly string Desert = $"{Id}_desert";
    
    public static readonly string Swamp = $"{Id}_swamp";
    
    public static readonly string SecretWoods = $"{Id}_secretWoods";
    
    public static readonly string Sewers = $"{Id}_sewers";
    
    public static readonly string BugLand = $"{Id}_bugLair";
    
    public static bool IsDesert(this GameLocation location)
    {
        return location is Desert;
    }

    public static bool IsSwamp(this GameLocation location)
    {
        return location.Name == "WitchSwamp";
    }

    public static bool IsSecretWoods(this GameLocation location)
    {
        return location is Woods;
    }
    
    public static bool IsSewers(this GameLocation location)
    {
        return location is Sewer;
    }

    public static bool IsBugLand(this GameLocation location)
    {
        return location is BugLand;
    }

    public static bool ShouldExcludeVanillaCrabPotFishTypes(this GameLocation location)
    {
        return location.IsDesert();
    }
}
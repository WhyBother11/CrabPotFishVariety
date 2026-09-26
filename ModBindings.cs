global using static CrabPotFishVariety.ModBindings;

namespace CrabPotFishVariety;

public static class ModBindings
{
    
    public static ModConfig Config => ModEntry.Instance.Config;
    
    public static string Id => ModEntry.Instance.ModManifest.UniqueID;
    
}
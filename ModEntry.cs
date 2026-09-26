using CrabPotFishVariety.Framework.Core;
using CrabPotFishVariety.Integrations;
using HarmonyLib;
using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace CrabPotFishVariety;

public class ModEntry : Mod
{
    #nullable disable
    public static ModEntry Instance { get; private set; }
    
    public ModConfig Config { get; private set; }
    #nullable restore

    public override void Entry(IModHelper helper)
    {
        Instance = this;
        Config = helper.ReadConfig<ModConfig>();
        var harmony = new Harmony(ModManifest.UniqueID);
        GameLocationCrabPotPatch.Register(harmony, Monitor);
        helper.Events.GameLoop.GameLaunched += OnGameLaunched_AddGmcmIntegration;
    }
    
    private void OnGameLaunched_AddGmcmIntegration(object? sender, GameLaunchedEventArgs e)
    {
        var configMenu = Helper.ModRegistry.GetApi<IGenericModConfigMenu>("spacechase0.GenericModConfigMenu");
        if (configMenu is null)
            return;

        configMenu.Register(
            mod: ModManifest,
            reset: () => Config = new ModConfig(),
            save: () => Helper.WriteConfig(Config)
        );

        this.AddGmcmOptions(
            configMenu: configMenu
        );
    }
}

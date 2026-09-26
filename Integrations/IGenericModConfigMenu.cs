using StardewModdingAPI;

namespace CrabPotFishVariety.Integrations;

public interface IGenericModConfigMenu
{
    void Register(IManifest mod, Action reset, Action save, bool titleScreenOnly = false);

    void AddBoolOption(IManifest mod, Func<bool> getValue, Action<bool> setValue, Func<string> name, Func<string> tooltip = null, string fieldId = null);

}
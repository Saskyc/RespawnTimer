using Exiled.API.Features;
using Exiled.API.Interfaces;
using Exiled.Loader;

namespace RespawnTimer.API;

public class Initialize
{
    public static void Indeed()
    {
        foreach (IPlugin<IConfig> plugin in Loader.Plugins)
        {
            switch (plugin.Name)
            {
                case "Serpents Hand" when plugin.Config.IsEnabled:
                    API.SerpentsHandTeam.Init(plugin.Assembly);
                    Log.Debug("Serpents Hand plugin detected!");
                    break;

                case "UIURescueSquad" when plugin.Config.IsEnabled:
                    API.UiuTeam.Init(plugin.Assembly);
                    Log.Debug("UIURescueSquad plugin detected!");
                    break;
            }
        }
    }
}
using Exiled.API.Features;
using RespawnTimer.API;

namespace RespawnTimer.Placeholders;

public class WarheadStatus : Placeholder
{
    public override string ThePlaceholder { get; set; } = "{warhead_status}";
    public override string ToReplace(Player player)
    {
        Exiled.API.Enums.WarheadStatus warheadStatus = Warhead.Status;
        return Manager.Properties.WarheadStatus[warheadStatus].ToString();
    }
}
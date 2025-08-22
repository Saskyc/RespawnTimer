using System.Globalization;
using Exiled.API.Features;
using RespawnTimer.API;
using UnityEngine;

namespace RespawnTimer.Placeholders;

public class DetonationTime : Placeholder
{
    public override string ThePlaceholder { get; set; } = "{detonation_time}";
    public override string ToReplace(Player player)
    {
        Exiled.API.Enums.WarheadStatus warheadStatus = Warhead.Status;
        return warheadStatus is Exiled.API.Enums.WarheadStatus.InProgress ? Mathf.Round(Warhead.DetonationTimer).ToString(CultureInfo.InvariantCulture) : string.Empty;
    }
}
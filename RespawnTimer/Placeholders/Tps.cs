using System;
using System.Globalization;
using Exiled.API.Features;
using RespawnTimer.API;
using UnityEngine;

namespace RespawnTimer.Placeholders;

public class Tps : Placeholder
{
    public override string ThePlaceholder { get; set; } = "{tps}";
    public override string ToReplace(Player player)
    {
        return Math.Round(1.0 / Time.smoothDeltaTime).ToString(CultureInfo.InvariantCulture);
    }
}
using System;
using System.Globalization;
using Exiled.API.Features;
using RespawnTimer.API;
using UnityEngine;

namespace RespawnTimer.Placeholders;

public class Tickrate : Placeholder
{
    public override string ThePlaceholder { get; set; } = "{tickrate}";
    public override string ToReplace(Player player)
    {
        return Application.targetFrameRate.ToString();
    }
}
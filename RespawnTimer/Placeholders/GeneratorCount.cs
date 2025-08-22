using System.Globalization;
using Exiled.API.Features;
using PlayerRoles.PlayableScps.Scp079;
using RespawnTimer.API;
using UnityEngine;

namespace RespawnTimer.Placeholders;

public class GeneratorCount : Placeholder
{
    public override string ThePlaceholder { get; set; } = "{generator_count}";
    public override string ToReplace(Player player)
    {
        return Scp079Recontainer.AllGenerators.Count.ToString();
    }
}
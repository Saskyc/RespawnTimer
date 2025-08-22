using System.Globalization;
using System.Linq;
using Exiled.API.Features;
using PlayerRoles.PlayableScps.Scp079;
using RespawnTimer.API;
using UnityEngine;

namespace RespawnTimer.Placeholders;

public class GeneratorEngaged : Placeholder
{
    public override string ThePlaceholder { get; set; } = "{generator_engaged}";
    public override string ToReplace(Player player)
    {
        return Scp079Recontainer.AllGenerators.Where(x => x.Engaged).ToList().Count.ToString();
    }
}
using System;
using Exiled.API.Features;
using RespawnTimer.API;

namespace RespawnTimer.Placeholders;

public sealed class DynamicPlaceholder : Placeholder
{
    public override string ThePlaceholder { get; set; }
    
    public readonly Func<Player, string> Function;
    
    public override string ToReplace(Player player)
    {
        return Function.Invoke(player);
    }

    public DynamicPlaceholder(string placeHolder, Func<Player, string> func)
    {
        ThePlaceholder = placeHolder;
        Function = func;
    }
}
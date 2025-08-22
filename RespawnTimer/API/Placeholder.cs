
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Exiled.API.Features;
using RespawnTimer.API.Features;
using RespawnTimer.Placeholders;

namespace RespawnTimer.API;

public abstract class Placeholder
{
    public abstract string ThePlaceholder { get; set; }
    public abstract string ToReplace(Player player);
    
    public TimerPlayer TimerPlayer { get; set; }

    public TimerManager Manager => TimerPlayer.Manager;
    
    public Assembly Assembly;
    public static List<Placeholder> List = [];

    public static void RegisterAll(Assembly assembly = null)
    {
        if(assembly is null)
            assembly = Assembly.GetCallingAssembly();
        Type[] types = assembly.GetTypes();
        foreach (var type in types)
        {
            if (!type.IsSubclassOf(typeof(Placeholder)) || type.IsAbstract || type.Name == "DynamicPlaceholder") continue;
            
            var instance = (Placeholder)Activator.CreateInstance(type);
            instance.Assembly = assembly;
            List.Add(instance);
        }
    }
    
    public static void UnregisterAll(Assembly assembly = null)
    {
        if(assembly is null)
            assembly = Assembly.GetCallingAssembly();
        
        List.RemoveAll(x => x.Assembly == assembly);
    }

    public static void Flush()
    {
        List.Clear();
    }

    public static StringBuilder FormText(StringBuilder builder, TimerPlayer timerPlayer)
    {
        foreach (var placeholder in List)
        {
            placeholder.TimerPlayer = timerPlayer;
            builder = placeholder.Add(builder, timerPlayer.Player);
        }

        return builder;
    }
    
    public StringBuilder Add(StringBuilder builder, Player player)
    {
        return builder.Replace(ThePlaceholder, ToReplace(player));
    }

    public static void Register(string placeHolder, Func<Player, string> func)
    {
        List.Add(new DynamicPlaceholder(placeHolder, func));
    }
}
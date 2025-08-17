using RespawnTimer.Main;

namespace RespawnTimer.API.Features
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Configs;
    using Respawning;
    using Serialization;
    using UnityEngine;
    using Exiled.API.Features;

    public partial class TimerView
    {
        public static readonly Dictionary<string, TimerView> CachedTimers = new();

        public int HintIndex { get; private set; }

        private int HintInterval { get; set; }

        public static void AddTimer(string name)
        {
            if (CachedTimers.ContainsKey(name))
                return;

            string directoryPath = Path.Combine(Helper.RespawnTimerDirectoryPath, name);
            if (!Directory.Exists(directoryPath))
            {
                Log.Error($"{name} directory does not exist!");
                return;
            }

            string timerBeforePath = Path.Combine(directoryPath, "TimerBeforeSpawn.txt");
            if (!File.Exists(timerBeforePath))
            {
                Log.Error($"{Path.GetFileName(timerBeforePath)} file does not exist!");
                return;
            }

            string timerDuringPath = Path.Combine(directoryPath, "TimerDuringSpawn.txt");
            if (!File.Exists(timerDuringPath))
            {
                Log.Error($"{Path.GetFileName(timerDuringPath)} file does not exist!");
                return;
            }

            string propertiesPath = Path.Combine(directoryPath, "Properties.yml");
            if (!File.Exists(propertiesPath))
            {
                Log.Error($"{Path.GetFileName(propertiesPath)} file does not exist! Creating...");
                File.WriteAllText(propertiesPath, YamlParser.Serializer.Serialize(new Properties()));
            }

            string hintsPath = Path.Combine(directoryPath, "Hints.txt");
            List<string> hints = new();
            if (File.Exists(hintsPath))
                hints.AddRange(File.ReadAllLines(hintsPath));

            TimerView timerView = new(
                File.ReadAllText(timerBeforePath),
                File.ReadAllText(timerDuringPath),
                YamlParser.Deserializer.Deserialize<Properties>(File.ReadAllText(propertiesPath)),
                hints);
            
            Log.Info($"Added {name}");
            CachedTimers.Add(name, timerView);
        }

        public static bool TryGetTimerForPlayer(Player player, out TimerView timerView)
        {
            string groupName = !ServerStatic.PermissionsHandler.Members.TryGetValue(player.UserId, out string str) ? null : str;

            // Check by group name
            if (groupName is not null && Main.RespawnTimer.Instance.Config.Timers.TryGetValue(groupName, out string timerName))
            {
                timerView = CachedTimers[timerName];
                if (timerView is null) return false;
                return true;
            }

            // Check by user id
            if (Main.RespawnTimer.Instance.Config.Timers.TryGetValue(player.UserId, out timerName))
            {
                timerView = CachedTimers[timerName];
                if (timerView is null) return false;
                return true;
            }

            
            // Use fallback default timer
            if (Main.RespawnTimer.Instance.Config.Timers.TryGetValue("default", out timerName))
            {
                if (!CachedTimers.ContainsKey(timerName))
                {
                    timerView = null;
                    return false;
                }
                timerView = CachedTimers[timerName];
                if (timerView is null) return false;
                return true;
            }

            // Default fallback does not exist
            timerView = null!;
            return false;
        }

        public string GetText(int? spectatorCount = null)
        {
            StringBuilder.Clear();
            StringBuilder.Append(WaveManager.State is WaveQueueState.WaveSpawning ?  DuringRespawnString : BeforeRespawnString);

            SetAllProperties(spectatorCount);
            StringBuilder.Replace("{RANDOM_COLOR}", $"#{Random.Range(0x0, 0xFFFFFF):X6}");
            StringBuilder.Replace('{', '[').Replace('}', ']');

            return StringBuilder.ToString();
        }

        internal void IncrementHintInterval()
        {
            HintInterval++;
            if (HintInterval != Properties.HintInterval)
                return;

            HintInterval = 0;
            IncrementHintIndex();
        }

        private void IncrementHintIndex()
        {
            HintIndex++;
            if (Hints.Count == HintIndex)
                HintIndex = 0;
        }

        private TimerView(string beforeRespawnString, string duringRespawnString, Properties properties, List<string> hints)
        {
            BeforeRespawnString = beforeRespawnString;
            DuringRespawnString = duringRespawnString;
            Properties = properties;
            Hints = hints;
        }

        public string BeforeRespawnString { get; }

        public string DuringRespawnString { get; }

        public Properties Properties { get; }

        public List<string> Hints { get; }

        private readonly StringBuilder StringBuilder = new(1024);
    }
}
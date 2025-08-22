namespace RespawnTimer.Configs
{
    using System.Collections.Generic;
    using System.ComponentModel;

    public sealed class Properties
    {
        [Description("Whether the leading zeros should be added in minutes and seconds if number is less than 10.")]
        public bool LeadingZeros { get; private set; } = true;
        
        public Dictionary<Exiled.API.Enums.WarheadStatus, string> WarheadStatus { get; private set; } = new()
        {
            {
                Exiled.API.Enums.WarheadStatus.NotArmed, "<color=green>Unarmed</color>"
            },
            {
                Exiled.API.Enums.WarheadStatus.Armed, "<color=orange>Armed</color>"
            },
            {
                Exiled.API.Enums.WarheadStatus.InProgress, "<color=red>In Progress - </color>"
            },
            {
                Exiled.API.Enums.WarheadStatus.Detonated, "<color=#640000>Detonated</color>"
            },
        };
    }
}
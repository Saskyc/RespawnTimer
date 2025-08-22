# RespawnTimer ![img](https://img.shields.io/github/downloads/Saskyc/RespawnTimer/total?style=for-the-badge)
A SCP: Secret Laboratory plugin that shows when the next respawn wave will happen.

# Features
- Fully customizable timer that may show additional info, like round time, server TPS, amount of spectators, enabled generators etc.
- Different timer interface depending on a player group name.
- Option for adding multiple custom texts (hints) to the interface, where you can put advertisements and/or gameplay hints for players.
- Option for hiding the timer interface (**.timer** command in client console).
- Option for hiding the timer interface if admin is using Overwatch mode (enabled by default).
- Support for [X]

# Configuration
When you first install the plugin, the `ExampleTimer` folder will be automatically downloaded. You are free to edit the config files inside this directory as you see fit. You can change how the timer looks like, hints etc. Removing `ExampleTimer` folder will redownload it once again after server restart.

# Placeholders
* {detonation_time} - when will warhead detonate
* {generator_count} - all generator count
* {generator_engaged} - amount of generators engaged
* {hint} - the fucking text down there where you can add fuck all
* {RANDOM_COLOR} - bitchass randomizing color I spent too much time updating
* {round_minutes} - the minutes round has been going through
* {round_seconds} - same as minutes for some apperent fucking reason you can see 60s, so OCD people have fun
* {spectators_num} - guys that are spectating
* {team} - team that gonna spawn
* {tickrate} - the tick rate
* {tps} - the tps
* {warhead_status} - status of warhead
* {minutes} - should be named {wave_minutes}, but it isn't, it's minutes until wave spawn
* {wave_ready} - is the wave ready? Always shows yes :tf:
* {seconds} - same as {minutes}, but seconds

# Credits
* Plugin made by Michal78900
* Update to EXILED 8 and compatibility fixes for SH and UIU plugins by Misfiy
* Code cleanup, port to Exiled 9.8.1, maintained by Saskyc

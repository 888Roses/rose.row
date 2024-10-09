using rose.row.console.commands.arguments;
using rose.row.easy_package.ui.text;
using rose.row.ui.console;
using rose.row.util;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static InstantActionMaps;

namespace rose.row.console.commands
{
    public class MapCommand : AbstractConsoleCommand
    {
        public const string k_MapArgument = "map";
        public const string k_BotCountArgument = "bot_count";
        public const string k_SpawnTimeArgument = "spawn_time";
        public const string k_ConfigureFlagsArgument = "configure_flags";

        public static IEnumerable<MapEntry> cachedMapEntries;

        public override string root => "map";
        public override string description => "Starts a match on the given map, with the given detail arguments.";

        public MapCommand()
        {
            arguments = new AbstractArgument[]
            {
                new StringArgument(k_MapArgument, string.Empty)
                {
                    hasCustomSuggestions = true,
                    customSuggestions = pullFromMapPool
                },
                new IntArgument(k_BotCountArgument, -1),
                new IntArgument(k_SpawnTimeArgument, 5),
                new BooleanArgument(k_ConfigureFlagsArgument, false),
            };
        }

        public static string prepare(string text) => text.Replace(" ", "").ToLowerInvariant();

        public static List<SuggestionPair> pullFromMapPool(string command, string lastArgument)
        {
            var list = new List<SuggestionPair>();

            if (instance == null)
            {
                var component = new TextComponent("Could not find maps because the managers responsible for that are not initialised.");
                component.setStyle(TextStyle.empty.withColor(Console.getColorForLogType(LogType.Error)));
                list.Add(new SuggestionPair(component.getString(), ""));
                return list;
            }

            var preparedLastArgument = prepare(lastArgument.Replace($"{k_MapArgument}:", ""));
            var hasDoubleQuotes = preparedLastArgument.StartsWith("\"") && preparedLastArgument.EndsWith("\"");
            var hasSimpleQuotes = preparedLastArgument.StartsWith("'") && preparedLastArgument.EndsWith("'");
            if (hasDoubleQuotes || hasSimpleQuotes)
                preparedLastArgument = preparedLastArgument.Remove(0, 1).Remove(preparedLastArgument.Length - 2, 1);

            if (cachedMapEntries == null)
                cachedMapEntries = MapUtil.allMapEntries();

            foreach (var map in cachedMapEntries)
            {
                if (map == null)
                    continue;

                var preparedMapName = prepare(map.getDisplayName());
                if (preparedMapName.StartsWith(preparedLastArgument))
                {
                    var displayName = new TextComponent(map.getDisplayName());
                    var description = new TextComponent(" " + map.sceneName);
                    var descriptionStyle = TextStyle.empty.withColor("#828282");
                    description.setStyle(descriptionStyle);
                    displayName.append(description);
                    var valueBuilder = new StringBuilder();
                    var commandWithoutLastArgument =
                        command.Remove(command.Length - lastArgument.Length - 1, lastArgument.Length + 1);
                    valueBuilder
                        .Append(commandWithoutLastArgument)
                        .Append(" ")
                        .Append(k_MapArgument)
                        .Append(":");

                    if (map.getDisplayName().Contains(" "))
                        valueBuilder.Append("\"");

                    valueBuilder.Append(map.getDisplayName());

                    if (map.getDisplayName().Contains(" "))
                        valueBuilder.Append("\"");

                    list.Add(new SuggestionPair(displayName.getString(), valueBuilder.ToString()));
                }

                if (list.Count > 15)
                    break;
            }

            return list;
        }

        public override void execute()
        {
            var mapArg = this[k_MapArgument] as StringArgument;
            var botCountArg = this["bot_count"] as IntArgument;
            var spawnTimeArg = this["spawn_time"] as IntArgument;
            var configureFlagsArg = this["configure_flags"] as BooleanArgument;

            var name = mapArg.value;
            var botCount = botCountArg.value;
            var spawnTime = spawnTimeArg.value;
            var configureFlags = configureFlagsArg.value;

            if (string.IsNullOrEmpty(name))
            {
                Debug.LogError($"Cannot start a map with an empty name!");
                return;
            }

            var maps = MapUtil.allMapEntries();
            if (maps == null)
            {
                Debug.LogError($"Could not find maps because the managers responsible for that are not initialised.");
                return;
            }

            foreach (var map in maps)
            {
                string prepare(string original) => original.ToLowerInvariant().Replace(" ", "");
                if (prepare(map.sceneName) == prepare(name) || prepare(map.getDisplayName()) == prepare(name))
                {
                    var parameters = GameManager.GameParameters();
                    parameters.actorCount = botCount < 0 ? map.getSuggestedBots() : botCount;
                    parameters.respawnTime = spawnTimeArg.value;
                    parameters.configFlags = configureFlags;

                    Debug.Log($"Starting map '{name}'. Bot count: {parameters.actorCount}. Spawn time: {spawnTime}. Configure flags: {configureFlags}.");
                    GameManager.StartLevel(map, parameters);
                    return;
                }
            }

            Debug.LogError($"Couldn't find map with name '{name}'!");
            return;
        }
    }
}
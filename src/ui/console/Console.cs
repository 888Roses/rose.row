﻿using rose.row.console.commands;
using rose.row.easy_package.ui.text;
using rose.row.util;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace rose.row.ui.console
{
    public static class Console
    {
        public static readonly List<AbstractConsoleCommand> consoleCommands = new List<AbstractConsoleCommand>();
        public static readonly List<string> sentCommands = new List<string>();

        public static void initialize()
        {
            Application.logMessageReceived += onLogMessageReceived;
            populateCommandsList();
        }

        private static IEnumerable<Type> getAllConsoleCommands()
            => AppDomain.CurrentDomain
                        .GetAssemblies()
                        .SelectMany(x => x.GetTypes())
                        .Where(x => x.BaseType == typeof(AbstractConsoleCommand));

        private static void populateCommandsList()
        {
            foreach (var consoleCommand in getAllConsoleCommands())
                consoleCommands.Add((AbstractConsoleCommand) Activator.CreateInstance(consoleCommand));
        }

        public static string getColorForLogType(LogType type)
        {
            return type switch
            {
                LogType.Assert => "#3B83BD",
                LogType.Error => "#E52B50",
                LogType.Exception => "#AF2B1E",
                LogType.Log => "#f6f6f6",
                LogType.Warning => "#ffc61e",
                _ => "#f6f6f6",
            };
        }

        private static void onLogMessageReceived(string condition, string stackTrace, LogType type)
        {
            var text = new TextComponent(condition + " " + stackTrace);
            var style = TextStyle.empty;
            style.color.setColor(getColorForLogType(type));
            text.setStyle(style);
            ConsoleManager.instance.addMessage(new TextComponent(type.ToString() + ": ").withStyle(TextStyle.empty.withColor("#828282")).append(text).getString());
        }

        public static AbstractConsoleCommand getCommandWithRoot(string root)
        {
            foreach (var command in consoleCommands)
                if (command.root == root)
                    return command;

            return null;
        }

        public static void executeCommand(string value)
        {
            sentCommands.Add(value);

            if (!value.Contains(" "))
            {
                var trimmed = value.Trim();
                var commandWithoutArguments = getCommandWithRoot(trimmed);
                if (commandWithoutArguments == null)
                {
                    Debug.LogWarning($"Could not find command with root {trimmed}.");
                    return;
                }

                commandWithoutArguments.execute();
                return;
            }

            var spaceSplits = value.safeSplitWhitespace();

            if (spaceSplits == null || spaceSplits.Count == 0)
            {
                Debug.LogError($"Cannot execute command with null syntax!");
                return;
            }

            var root = spaceSplits[0];
            var command = getCommandWithRoot(root);
            if (command == null)
            {
                Debug.LogWarning($"Could not find command with root {root}.");
                return;
            }

            spaceSplits.RemoveAt(0);
            foreach (var argument in command.arguments)
            {
                argument.reset();
                foreach (var split in spaceSplits)
                {
                    if (!split.Contains(":"))
                        continue;

                    var nameValueSplit = split.Split(':');
                    if (nameValueSplit[0] != argument.name)
                        continue;

                    if (!argument.accept(nameValueSplit[1].Trim()))
                    {
                        argument.throwError(nameValueSplit[1]);
                        return;
                    }
                }
            }

            command.execute();
        }
    }
}
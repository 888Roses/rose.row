using rose.row.console.commands.arguments;
using rose.row.easy_package.ui.text;
using rose.row.ui.console;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static WeaponManager;

namespace rose.row.console.commands
{
    public class SelectWeaponCommand : AbstractConsoleCommand
    {
        public const string k_WeaponNameArgument = "weapon_name";
        public const string k_PreferedSlotArgument = "prefered_slot_name";
        public const string k_AutoEquipArgument = "auto_equip_weapon";

        public override string root => "weapon.select";
        public override string description => "Selects the given weapon in the player's current loadout.";

        public static string prepare(string text) => text.Replace(" ", "").ToLowerInvariant();

        public static List<SuggestionPair> pullFromWeaponManager(string command, string lastArgument)
        {
            var list = new List<SuggestionPair>();

            var preparedLastArgument = prepare(lastArgument.Replace($"{k_WeaponNameArgument}:", ""));
            var hasDoubleQuotes = preparedLastArgument.StartsWith("\"") && preparedLastArgument.EndsWith("\"");
            var hasSimpleQuotes = preparedLastArgument.StartsWith("'") && preparedLastArgument.EndsWith("'");
            if (hasDoubleQuotes || hasSimpleQuotes)
                preparedLastArgument = preparedLastArgument.Remove(0, 1).Remove(preparedLastArgument.Length - 2, 1);

            foreach (var weapon in instance.allWeapons)
            {
                if (weapon == null)
                    continue;

                var preparedWeaponName = prepare(weapon.name);
                if (preparedWeaponName.StartsWith(preparedLastArgument))
                {
                    var displayName = new TextComponent(weapon.name);
                    var description = new TextComponent(" " + weapon.slot.ToString());
                    var descriptionStyle = TextStyle.empty.withColor(ConsoleColors.autoCompletionSuggestionDescription);
                    description.setStyle(descriptionStyle);
                    displayName.append(description);
                    var valueBuilder = new StringBuilder();
                    var commandWithoutLastArgument =
                        command.Remove(command.Length - lastArgument.Length - 1, lastArgument.Length + 1);
                    valueBuilder
                        .Append(commandWithoutLastArgument)
                        .Append(" ")
                        .Append(k_WeaponNameArgument)
                        .Append(":");

                    if (weapon.name.Contains(" "))
                        valueBuilder.Append("\"");

                    valueBuilder.Append(weapon.name);

                    if (weapon.name.Contains(" "))
                        valueBuilder.Append("\"");

                    list.Add(new SuggestionPair(displayName.getString(), valueBuilder.ToString()));
                }

                if (list.Count > 15)
                    break;
            }

            return list;
        }

        public static List<SuggestionPair> pullWeaponSlots(string command, string lastArgument)
        {
            return CommandSuggestionUtil.pullSuggestionsFromEnum(
                argumentName: k_PreferedSlotArgument,
                enumType: typeof(WeaponSlot),
                command: command,
                lastArgument: lastArgument
            );
        }

        public SelectWeaponCommand()
        {
            arguments = new AbstractArgument[]
            {
                new StringArgument(k_WeaponNameArgument)
                {
                    hasCustomSuggestions = true,
                    customSuggestions = pullFromWeaponManager
                },
                new StringArgument(k_PreferedSlotArgument)
                {
                    hasCustomSuggestions = true,
                    customSuggestions = pullWeaponSlots
                },
                new BooleanArgument(k_AutoEquipArgument, true),
            };
        }

        public override void execute()
        {
            var weaponNameArg = this[k_WeaponNameArgument] as StringArgument;

            var name = weaponNameArg.value;
            if (string.IsNullOrEmpty(name))
            {
                Debug.LogError($"Cannot give a weapon with an empty name!");
                return;
            }

            var preferedSlotArg = this[k_PreferedSlotArgument] as StringArgument;
            var preferedSlotName = preferedSlotArg.value;

            var autoEquipArg = this[k_AutoEquipArgument] as BooleanArgument;
            var autoEquip = autoEquipArg.value;

            if (instance == null)
            {
                Debug.LogError("Could not execute command because the WeaponManager is not initialised.");
                return;
            }

            if (FpsActorController.instance == null)
            {
                Debug.LogError("Could not execute command because the FpsActorController is not initialised. This command can only be used in-game.");
                return;
            }

            string prepare(string text) => text.ToLowerInvariant().Replace(" ", "");
            var preparedName = prepare(name);

            var identicalCharactersCount = 0;
            WeaponEntry weaponEntry = null;

            foreach (var weapon in WeaponManager.instance.allWeapons)
            {
                var preparedWeaponName = prepare(weapon.name);

                if (preparedWeaponName == preparedName)
                {
                    weaponEntry = weapon;
                    break;
                }
                else
                {
                    var identicalCharacters = 0;
                    var uniqueCharacters = new List<char>();
                    for (int i = 0; i < preparedWeaponName.Length; i++)
                        if (!uniqueCharacters.Contains(preparedWeaponName[i]))
                            uniqueCharacters.Add(preparedWeaponName[i]);

                    foreach (var @char in uniqueCharacters)
                        if (preparedName.Contains(@char.ToString()))
                            identicalCharacters++;

                    if (identicalCharacters > identicalCharactersCount)
                    {
                        weaponEntry = weapon;
                        identicalCharactersCount = identicalCharacters;
                        continue;
                    }
                }
            }

            if (weaponEntry == null)
            {
                Debug.LogWarning($"Couldn't find a weapon with the name '{name}' (prepared '{preparedName}') :(");
                return;
            }

            var slot = WeaponSlot.Primary;
            if (!string.IsNullOrEmpty(preferedSlotName))
                if (!Enum.TryParse(preferedSlotName, true, out slot))
                {
                    Debug.LogWarning($"Couldn't find a slot with the name '{preferedSlotName}'. Slot names:");
                    foreach (var weaponSlot in Enum.GetValues(typeof(WeaponSlot)))
                        Debug.LogWarning($" * '{weaponSlot}'");
                }

            Debug.Log($"Equiping '{weaponEntry.name}' in slot {slot}.");
            FpsActorController.instance.actor.EquipNewWeaponEntry(weaponEntry, (int) slot, autoEquip);
        }
    }
}
using HarmonyLib;
using UnityEngine;

namespace rose.row.mods
{
    [HarmonyPatch(typeof(ModContentInformation))]
    [HarmonyPatch("sourceMod", MethodType.Setter)]
    internal class SetSourceModPatcher
    {
        [HarmonyPrefix]
        static bool prefix(ModContentInformation __instance, ModInformation value)
        {
            Traverse.Create(__instance).Field("_sourceMod").SetValue(value);
            //__instance.serializableSourceModReference = new SerializedModReference(__instance._sourceMod);
            __instance.serializableSourceModReference = ScriptableObject.CreateInstance<SerializedModReference>();
            __instance.serializableSourceModReference.modInformation = (ModInformation) Traverse.Create(__instance).Field("_sourceMod").GetValue();
            return false;
        }
    }
}

using rose.row.killfeed.info;
using System.Collections.Generic;

namespace rose.row.data
{
    public static class Constants
    {
        public const string k_ActorCanFallOver = "actor.canFallOver";
        public const string k_VehicleRamForce = "vehicle.ramForce";

        public static readonly Dictionary<string, object> defaultValues = new Dictionary<string, object>()
        {
            { k_ActorCanFallOver, false },
            { k_VehicleRamForce, 10f },
        };

        /// <summary>
        /// Number added to the <see cref="SpawnPoint.GetCaptureRange()"/> for
        /// <see cref="CapturePointAttackInfo"/> and <see cref="CapturePointDefenseInfo"/>
        /// kills.
        /// </summary>
        public const float k_SpawnPointDefenseAttackAdditionalRange = 20f;

        public static string basePath => $"{Files.documents}/Heroes & Generals";
    }
}
using rose.row.vehicles;
using UnityEngine;

namespace rose.row.dev.vehicle_selector
{
    public readonly struct VehicleInfoPair
    {
        public readonly VehicleInfo info;
        public readonly Texture2D texture;

        public VehicleInfoPair(VehicleInfo info, Texture2D texture)
        {
            this.info = info;
            this.texture = texture;
        }
    }
}
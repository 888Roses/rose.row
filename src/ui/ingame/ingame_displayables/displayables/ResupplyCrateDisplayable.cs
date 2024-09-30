using rose.row.data;
using UnityEngine;

namespace rose.row.ui.ingame.ingame_displayables.displayables
{
    public class ResupplyCrateDisplayable : AbstractDisplayable
    {
        public override Vector3 offset => Vector3.up;
        public override float iconSize => 32f;

        public override Texture2D icon => ImageRegistry.resupplyAmmunition.get();
    }
}
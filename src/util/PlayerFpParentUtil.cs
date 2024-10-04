using HarmonyLib;

namespace rose.row.util
{
    public static class PlayerFpParentUtil
    {
        public const string k_PositionSpringName = "positionSpring";
        public const string k_RotationSpringName = "rotationSpring";

        private static void setSpring(this PlayerFpParent parent, string name, Spring spring)
        {
            Traverse.Create(parent).Field(name).SetValue(spring);
        }

        private static Spring getSpring(this PlayerFpParent parent, string name)
        {
            return (Spring)Traverse.Create(parent).Field(name).GetValue();
        }

        public static Spring positionSpring(this PlayerFpParent parent) => parent.getSpring(k_PositionSpringName);
        public static void setPositionSpring(this PlayerFpParent parent, Spring spring) => parent.setSpring(k_PositionSpringName, spring);

        public static Spring rotationSpring(this PlayerFpParent parent) => parent.getSpring(k_RotationSpringName);
        public static void setRotationSpring(this PlayerFpParent parent, Spring spring) => parent.setSpring(k_RotationSpringName, spring);
    }
}

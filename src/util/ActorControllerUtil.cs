namespace rose.row.util
{
    public static class ActorControllerUtil
    {
        /// <summary>
        /// Retrieves the team of that actor controller.
        /// </summary>
        /// <remarks>
        /// This is the same as doing <see cref="ActorController.actor.team"/>.
        /// </remarks>
        public static int team(this ActorController controller) => controller.actor.team;

        /// <summary>
        /// Checks if that actor controller is dead or not.
        /// </summary>
        /// <remarks>
        /// This is the same as doing <see cref="ActorController.actor.dead"/>.
        /// </remarks>
        public static bool dead(this ActorController controller) => controller.actor.dead;
    }
}
using HarmonyLib;
using UnityEngine;

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

        #region ai

        public static Vector3 getAvoidancePassSegment(this AiActorController aiActorController)
        {
            return (Vector3) Traverse.Create(aiActorController).Field("avoidancePassSegment").GetValue();
        }

        public static void setAvoidancePassSegment(this AiActorController aiActorController, Vector3 value)
        {
            Traverse.Create(aiActorController).Field("avoidancePassSegment").SetValue(value);
        }

        public static Vector3 getAvoidancePassPoint(this AiActorController aiActorController)
        {
            return (Vector3) Traverse.Create(aiActorController).Field("avoidancePassPoint").GetValue();
        }

        public static void setAvoidancePassPoint(this AiActorController aiActorController, Vector3 value)
        {
            Traverse.Create(aiActorController).Field("avoidancePassPoint").SetValue(value);
        }

        public static bool getIsOnAvoidancePath(this AiActorController aiActorController)
        {
            return (bool) Traverse.Create(aiActorController).Field("isOnAvoidancePath").GetValue();
        }

        public static void setIsOnAvoidancePath(this AiActorController aiActorController, bool value)
        {
            Traverse.Create(aiActorController).Field("isOnAvoidancePath").SetValue(value);
        }

        #endregion
    }
}
using HarmonyLib;
using UnityEngine;

namespace rose.row.util
{
    public static class ActorUtil
    {
        private static T getField<T>(this Actor actor, string name)
            => (T) Traverse.Create(actor).Field(name).GetValue();

        private static void setField<T>(this Actor actor, string name, T value)
            => Traverse.Create(actor).Field(name).SetValue(value);

        public static string getNameSafe(this Actor actor)
        {
            if (actor.scoreboardEntry == null)
                return "";

            return actor.scoreboardEntry.nameText.text;
        }

        public static TimedAction hurtAction(this Actor actor)
        {
            return (TimedAction) Traverse.Create(actor).Field("hurtAction").GetValue();
        }

        public static ActorIk ik(this Actor actor) => actor.getField<ActorIk>("ik");
        public static Vector3 cachedVelocity(this Actor actor) => actor.getField<Vector3>("cachedVelocity");
        public static TimedAction fallAction(this Actor actor) => actor.getField<TimedAction>("fallAction");
        public static TimedAction getupAction(this Actor actor) => actor.getField<TimedAction>("getupAction");
        public static bool getupActionWasStarted(this Actor actor) => actor.getField<bool>("getupActionWasStarted");
        public static void setGetupActionWasStarted(this Actor actor, bool value) => actor.setField("getupActionWasStarted", value);
    }
}
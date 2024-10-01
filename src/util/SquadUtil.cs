using HarmonyLib;

namespace rose.row.util
{
    public static class SquadUtil
    {
        public static ActorController leader(this Squad squad)
        {
            return (ActorController) Traverse.Create(squad).Field("leader").GetValue();
        }

        public static Squad.MovePathSegment activePathSegment(this Squad squad)
        {
            return (Squad.MovePathSegment) Traverse.Create(squad).Field("activeSegmentPath").GetValue();
        }

        public static void setActiveSegmentPath(this Squad squad, Squad.MovePathSegment segmentPath)
        {
            Traverse.Create(squad).Field("activeSegmentPath").SetValue(segmentPath);
        }

        public static void issueMovePathSegment(this Squad squad, Squad.MovePathSegment segmentPath)
        {
            Traverse.Create(squad).Method("IssueMovePathSegment", segmentPath).GetValue();
        }
    }
}

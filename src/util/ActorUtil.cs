namespace rose.row.util
{
    public static class ActorUtil
    {
        public static string getNameSafe(this Actor actor)
        {
            if (actor.scoreboardEntry == null)
                return "";

            return actor.scoreboardEntry.nameText.text;
        }
    }
}
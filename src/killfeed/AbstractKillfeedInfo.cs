namespace rose.row.killfeed
{
    public abstract class AbstractKillfeedInfo
    {
        public abstract int getXP();
        public abstract string getMessage(int xp);

        public abstract bool compare(AbstractKillfeedInfo info);
    }
}
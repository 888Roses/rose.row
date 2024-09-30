using System.Collections.Generic;

namespace rose.row.data
{
    /// <summary>
    /// A class containing various tables from weapon name aliases to vehicle stuff.
    /// </summary>
    public static class DataTables
    {
        public static readonly Dictionary<string, string> weaponNames = new Dictionary<string, string>()
        {
            { "NAME", "ALIAS" }
        };
    }
}
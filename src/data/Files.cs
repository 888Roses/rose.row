using System;

namespace rose.row.data
{
    public static class Files
    {
        public static string documents => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    }
}
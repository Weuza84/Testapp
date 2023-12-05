using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Testapp
{
    public static class Constants
    {
        public const string url = "http://192.168.0.106:8000";
        public const string DatabaseFilename = "FFSQLite.db3";

        public const SQLite.SQLiteOpenFlags Flags = SQLite.SQLiteOpenFlags.ReadWrite | SQLite.SQLiteOpenFlags.Create | SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
    }
}

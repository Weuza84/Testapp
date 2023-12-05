using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Testapp.Services
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}

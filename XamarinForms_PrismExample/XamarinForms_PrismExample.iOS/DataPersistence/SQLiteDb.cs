using SQLite;
using System;
using System.IO;
using Xamarin.Forms;
using XamarinForms_PrismExample.DataPersistence;
using XamarinForms_PrismExample.iOS.Persistence;

[assembly: Dependency(typeof(SQLiteDb))]
namespace XamarinForms_PrismExample.iOS.Persistence
{
    public class SQLiteDb : ISQLitePaltformSpecific
    {
        public SQLiteAsyncConnection GetConnection()
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            var path = Path.Combine(libFolder, "MoviesSQLite.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}

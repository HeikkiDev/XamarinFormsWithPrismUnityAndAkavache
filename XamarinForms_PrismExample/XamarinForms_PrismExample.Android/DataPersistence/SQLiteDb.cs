using System.IO;
using SQLite;
using Xamarin.Forms;
using XamarinForms_PrismExample.DataPersistence;
using XamarinForms_PrismExample.Droid.Persistence;

[assembly: Dependency(typeof(SQLiteDb))]
namespace XamarinForms_PrismExample.Droid.Persistence
{
    public class SQLiteDb : ISQLitePaltformSpecific
    {
        public SQLiteAsyncConnection GetConnection()
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var databasePath = Path.Combine(path, "MoviesSQLite.db3");

            return new SQLiteAsyncConnection(databasePath);
        }
    }
}
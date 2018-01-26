using SQLite;

namespace XamarinForms_PrismExample.DataPersistence
{
    /// <summary>
    /// Interface para resolver las implementaciones platform-specific en Android y iOS
    /// </summary>
    public interface ISQLitePaltformSpecific
    {
        SQLiteAsyncConnection GetConnection();
    }
}

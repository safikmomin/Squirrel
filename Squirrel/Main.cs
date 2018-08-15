using POSUP.Model;
using SQLite;
using UIKit;
using Xamarin.Essentials;

namespace Squirrel
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            VersionTracking.Track();
            var dbPath = DataAcess.dbPath;
            var fileManager = new Foundation.NSFileManager();
            var DoesDBExists = fileManager.FileExists(dbPath);
            if (!DoesDBExists)
            {
                using (var db = new SQLiteConnection(dbPath))
                {
                    db.CreateTable<Item>();
                    db.CreateTable<Order>();
                    db.CreateTable<OrderItem>();
                    db.CreateTable<Customer>();
                }
            }
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}

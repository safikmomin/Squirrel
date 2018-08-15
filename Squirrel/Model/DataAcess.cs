using System;
using System.IO;

namespace POSUP.Model
{
    
    public class DataAcess
    {
        public static string dbPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.Personal),
        "POSUP.db3");
        private object locker = new object();

    }
}

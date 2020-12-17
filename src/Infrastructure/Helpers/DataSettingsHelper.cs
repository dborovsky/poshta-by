using System;
using System.Collections.Generic;
using System.Text;
using PoshtaBy.Infrastructure.Data;

namespace PoshtaBy.Infrastructure.Helpers
{
    public static class DataSettingsHelper
    {
        private static bool? _databaseIsInstalled;
        private static string _connectionString;
        
        public static bool DatabaseIsInstalled()
        {
            if (!_databaseIsInstalled.HasValue)
            {
                var manager = new DataSettingsManager();
                var settings = manager.LoadSettings();
                _databaseIsInstalled = settings != null && !String.IsNullOrEmpty(settings.DataConnectionString);
                if (!String.IsNullOrEmpty(settings.DataConnectionString))
                    _connectionString = settings.DataConnectionString;
            }
            return _databaseIsInstalled.Value;
        }
        public static void InitConnectionString()
        {
            var manager = new DataSettingsManager();
            var settings = manager.LoadSettings();
            if (!String.IsNullOrEmpty(settings.DataConnectionString))
                _connectionString = settings.DataConnectionString;
        }
        public static string ConnectionString()
        {
            return _connectionString;
        }

        public static void ResetCache()
        {
            _databaseIsInstalled = false;
        }
    }
}

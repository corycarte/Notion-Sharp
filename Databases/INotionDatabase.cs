using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotionAPI.Databases
{
    using NotionAPI.Data;
    public interface INotionDatabase
    {
        #region Sync
        public bool testDbConnection(string dbId);
        public string getDatabase(string dbId);
        public string queryDatabase(string dbId, string query);
        public string updateDatabase(string dbId, string propUpdates);
        public string createDatabase(string parentPageId, List<NotionText> title, string propertySchema); // NOTE: string propertySchema subject to change
        #endregion
        #region Async
        public Task<string> getDatabaseAsync(string dbId);
        public Task<string> queryDatabaseAsync(string dbId, string query);
        public Task<string> updateDatabaseAsync(string dbId, string propUpdates);
        public Task<string> createDatabaseAsync(string parentPageId, List<NotionText> title, string propertySchema); // NOTE: string propertySchema subject to change
        #endregion
    }
}

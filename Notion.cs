#nullable enable
namespace NotionAPI
{
    using NotionAPI.Blocks;
    using NotionAPI.Data;
    using NotionAPI.Pages;
    using NotionAPI.Utilities;

    using System.Net.Http;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Text;
    using Newtonsoft.Json;

    public class Notion : INotion
    {
        #region Class Members
        private static readonly HttpClient _client = new HttpClient();
        
        private const string _apiBase = "https://api.notion.com/v1";
        private const string _dbEndpoint = $"{_apiBase}/databases";
        private const string _pageEndpoint = $"{_apiBase}/pages";
        private const string _notionVersion = "2022-06-28";

        private string _apiKey;
        private Dictionary<string, string> _headers;

        public string ApiKey
        {
            private get
            {
                return _apiKey != string.Empty ? _apiKey : throw new ArgumentException("Notion API key can't be null");
            }
            set
            {
                this._apiKey = value;
                this._headers["Authorization"] = _apiKey;
            }
        }

        // TODO: Implement logging with ILogger
        #endregion
        #region Constructors

        public Notion()
        {
            _apiKey = string.Empty;
            _headers = new Dictionary<string, string> {
                { "Accept", "application/json" },
                { "Notion-Version", _notionVersion },
                { "Authorization", _apiKey }
            };
        }

        public Notion(string apiKey)
        {
            _apiKey = apiKey;
            _headers = new Dictionary<string, string> {
                { "Accept", "application/json" },
                { "Notion-Version", _notionVersion },
                { "Authorization", _apiKey }
            };
        }
        #endregion
        #region Helpers
        private HttpRequestMessage buildRequest(HttpMethod method, string url, Dictionary<string, string>? extraHeaders = null)
        {
            var request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(url),
                Headers = {
                    { "accept", "application/json" },
                    { "Authorization", $"Bearer {ApiKey}" },
                    { "Notion-Version", NotionAPIMetadata.notionVersion }
                }
            };

            if (extraHeaders != null)
            {
                foreach (KeyValuePair<string, string> kvp in extraHeaders)
                {
                    request.Headers.Add(kvp.Key, kvp.Value);
                }
            }

            return request;
        }

        private HttpRequestMessage addContent(HttpRequestMessage request, string bodyContent)
        {
            request.Content = new StringContent(bodyContent, Encoding.UTF8, "application/json");
            return request;
        }
        private string sendAndProcessRequest(HttpRequestMessage req)
        {
            return this.sendAndProcessRequestAsync(req).Result;
        }
        private async Task<string> sendAndProcessRequestAsync(HttpRequestMessage req)
        {
            var response = await _client.SendAsync(req);
            response.EnsureSuccessStatusCode();
            string rawContent = await response.Content.ReadAsStringAsync();
            return rawContent;
        }
        #endregion
        #region INotionDatabase methods
        #region Sync
        public bool testDbConnection(string dbId) { return this.testDbConnectionAsync(dbId).Result; }

        public string getDatabase(string dbId) { return this.getDatabaseAsync(dbId).Result; }

        public string queryDatabase(string dbId, string query) { return this.queryDatabaseAsync(dbId, query).Result; }

        public string updateDatabase(string dbId, string propUpdates)
        {
            throw new NotImplementedException();
        }

        public string createDatabase(string parentPageId, List<NotionText> title, string propertySchema)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region ASync
        public async Task<bool> testDbConnectionAsync(string dbId)
        {
            var res = await getDatabaseAsync(dbId);

            return res != null;
        }

        public async Task<string> getDatabaseAsync(string dbId)
        {
            var request = buildRequest(HttpMethod.Get, $"{NotionAPIEndpoints.dbEndpoint}/{dbId}");
            return await sendAndProcessRequestAsync(request);
        }

        public async Task<string> queryDatabaseAsync(string dbId, string query)
        {
            var request = buildRequest(HttpMethod.Post, $"{_dbEndpoint}/{dbId}/query");
            request = addContent(request, $"{{ \"filter\": {query}}}");
            var response = await sendAndProcessRequestAsync(request);
            return response;
        }

        public Task<string> updateDatabaseAsync(string dbId, string propUpdates)
        {
            throw new NotImplementedException();
        }

        public Task<string> createDatabaseAsync(string parentPageId, List<NotionText> title, string propertySchema)
        {
            throw new NotImplementedException();
        }
        #endregion
        #endregion
        #region INotionPage Operations
        #region Sync
        public string getPage(string pageId)
        {
            return this.getPageAsync(pageId).Result;
        }
        #endregion
        #region ASync
        public async Task<string> getPageAsync(string pageId)
        {
            try
            {
                var request = buildRequest(HttpMethod.Get, $"{_apiBase}/{pageId}");
                var res = _client.Send(request);

                res.EnsureSuccessStatusCode();
                
                string rawContent = await res.Content.ReadAsStringAsync();
                return rawContent;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to retrieve page Id: {pageId}: {ex.Message}");
            }

            return string.Empty;
        }
        #endregion
        #endregion
        #region INotionBlock Operations
        #region Sync
        #endregion
        #region ASync
        #endregion
        #endregion
    }
}
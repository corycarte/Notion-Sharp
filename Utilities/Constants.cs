using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotionAPI.Utilities
{
    public static class NotionAPIEndpoints
    {
        public const string apiBase = "https://api.notion.com/v1";
        public const string dbEndpoint = $"{apiBase}/databases";
        public const string pageEndpoint = $"{apiBase}/pages";

    }

    public static class NotionAPIMetadata
    {
        public const string notionVersion = "2022-06-28";
    }
}

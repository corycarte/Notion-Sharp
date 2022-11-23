using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotionAPI.Pages
{
    public interface INotionPage
    {
        #region Synch
        public string getPage(string pgId);
        // TODO: Create Page
        // TODO: Update Page
        #endregion
        #region Async
        public Task<string> getPageAsync(string pgId);
        // TODO: Create Page Async
        // TODO: Update Page Async
        #endregion
    }
}

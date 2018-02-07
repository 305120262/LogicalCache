using System;
using System.Web;
using LogicalCacheLibrary;

namespace LogicalCacheWeb
{
    public class LogicalCacheHandler : IHttpHandler
    {
        private LogicalCache logCache = new LogicalCache();

        public LogicalCacheHandler()
        {
            logCache.LoadConfig(@"D:\Projects\LogicalCache\LogicalCacheWeb\demo.json");
        }

        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: https://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Clear();
            //write your handler implementation here.
            if (context.Request.RawUrl.Contains("LogicalCache"))
            {
                string[] components = context.Request.RawUrl.Split('/');
                if(components.Length==6)
                { 
                    string serviceName = components[2];
                    int level = int.Parse(components[3]);
                    int row = int.Parse(components[4]);
                    int column = int.Parse(components[5]);
                    logCache.GetLogicalCacheTile(level, row, column,context.Response.OutputStream);
                    context.Response.ContentType = "image/png";
                }
            }
        }

        #endregion
    }
}

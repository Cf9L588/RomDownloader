using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RomDownloader
{
    internal static class Globals
    {
        internal static CoreEngine Core;

        internal static bool PageExists(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Head;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            var output =  response.StatusCode == HttpStatusCode.OK;
            response.Dispose();
            request = null;
            return output;
        }
    }
}

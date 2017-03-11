using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RomDownloader.Models;
using HtmlAgilityPack;
using System.Net;

namespace RomDownloader.RomSources
{
    class DopeRoms : RomSource
    {
        internal DopeRoms(): base("DopeRoms", "http://doperoms.com/")
        {
            
        }

        internal override List<GameConsole> GetSystems()
        {
            WebClient webClient = new WebClient();
            string page = webClient.DownloadString($"{URL}/roms");

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(page);

            var testing = doc.DocumentNode.SelectNodes("//html[1]/body[1]/div[2]/div[1]/div[10]/table[2]/tr[2]/td[1]/font[1]/table[1]/tr[1]/td[1]/table[1]/");
                //.Descendants()
                //.Where(n => n.Id == "listitem");
            //var testing = doc.GetElementbyId("listitem");
            //var testing = doc.DocumentNode.SelectNodes("//div");
            //.Where(c => c.Attributes["id"].Value == "listitem")
            //.ToList();

            //foreach(var c in testing)
            //{
            //    var x = c;
            //}

            return null;
                
        }
    }
}

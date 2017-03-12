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

        internal DopeRoms(): base("DopeRoms", "http://doperoms.com")
        {
            
        }
        
        internal override List<GameConsole> GetSystems()
        {
            //use the webclient to grab the source code of the page
            WebClient webClient = new WebClient();
            string page = webClient.DownloadString(new Uri(URL, "roms"));

            // pass the source code into the html document
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(page);

            // navigate the nodes to find the game console listings
            SystemList = doc.DocumentNode.SelectNodes("//html/body/div/div/div/table[2]/tr/td[1]/font[1]/table/tr/td/table/tr/td/a")
                // select the name and the provided url from the node
               .Select(n => new GameConsole(n.InnerText, n.Attributes["href"].Value)).ToList();
            
            return SystemList; 
        }
    }
}

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
        public DopeRoms()
        {
            Name = "DopeRoms";
            URL = new Uri("http://doperoms.com");
        }

        internal override  List<GameConsole> GetSystems()
        {
            //use the webclient to grab the source code of the page
            using (WebClient webClient = new WebClient())
            {
                string page = webClient.DownloadString(new Uri(URL, "roms"));

                // pass the source code into the html document
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(page);

                // navigate the nodes to find the game console listings
                SystemList = doc.DocumentNode.SelectNodes("//html/body/div/div/div/table[2]/tr/td[1]/font[1]/table/tr/td/table/tr/td/a")
                   // select the name and the provided url from the node
                   .Select(n => new GameConsole(n.InnerText, new Uri(URL, n.Attributes["href"].Value), this)).ToList();
            }
            
            
            return SystemList; 
        }
        
        internal override List<Rom> GetSystemRoms(GameConsole system)
        {
            int pageNum = 0;
            string currentPage = system.RomListUrl.ToString();
            system.Roms = new List<Rom>();
            string page = string.Empty;
            using (WebClient webClient = new WebClient())
            {
                page = webClient.DownloadString(currentPage);
                while (!string.IsNullOrEmpty(page))
                {
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(page);

                    var listingNodes = doc.DocumentNode.SelectNodes("//html[1]/body[1]/div[2]/div[1]/div[10]/table[2]/tr[2]/td[1]/table[1]/tr[1]/td[1]/div[1]/font[1]//table/tr")
                        .Where(node => node.Id == "listitem");
                    int romCount = 0;
                    foreach (var node in listingNodes)
                    {
                        var mainNode = node.SelectSingleNode(".//td/a");
                        var statusNodes = node.SelectNodes(".//td/font/img");
                        Rom newRom = CreateRomsFromNodes(mainNode, statusNodes);
                        if (newRom != null && newRom.Name != "No Roms")
                        {
                            system.Roms.Add(newRom);
                            romCount++;
                        }
                    }

                    if (romCount == 0)
                        break;
                    page = string.Empty;
                    pageNum++;
                    currentPage = new Uri(URL, $"roms/{system.Name.Replace(" ", "_")}/ALL/{pageNum * 50}.html").ToString();
                    page = webClient.DownloadString(currentPage);
                }
                return system.Roms;
            }
        }
        
        private Rom CreateRomsFromNodes(HtmlNode mainNode, HtmlNodeCollection StatusNodes)
        {
            Rom output = null;
            if(mainNode != null)
            {
                string romName = mainNode.Attributes["name"].Value;
                output = new Rom(romName, new Uri(this.URL, mainNode.Attributes["href"].Value));
                GetRomInfoFromName(ref output);
                if(StatusNodes != null)
                {
                    foreach (var statusNode in StatusNodes)
                    {
                        switch (statusNode.Attributes["alt"].Value.ToUpper())
                        {
                            case "VERIFIED GOOD":
                                output.Status = Rom.StatusType.Good;
                                break;
                            case "PUBLIC DOMAIN OR FREE":
                                output.Status = Rom.StatusType.Good;
                                output.IsPublicDomain = true;
                                break;
                        }
                    }
                }
            }
            return output;
        }

        private void GetRomInfoFromName(ref Rom rom)
        {
            // remove the .zip extension
            rom.Name = rom.Name.Replace(".zip", "")
                // Remove the good dump marker
                .Replace("[!]", "")
                // Replace the checksum marker
                .Replace("[c]", "")
                .Trim();
            
            int lastIndex = rom.Name.IndexOf("(");
            while (lastIndex != -1 && rom.Name.Length > lastIndex)
            {
                string info = string.Empty;
                for (int i = lastIndex; i < rom.Name.Length; i++)
                {
                    char c = rom.Name[i];
                    info += c;
                    if (c == ')')
                    {
                        bool known = true;
                        switch (info)
                        {
                            case "(U)":
                                rom.Language = Rom.LanguageType.English;
                                break;
                            case "(E)":
                                rom.Language = Rom.LanguageType.English;
                                break;
                            case "(J)":
                                rom.Language = Rom.LanguageType.Japanese;
                                break;
                            case "(JU)":
                                rom.Language = Rom.LanguageType.Japanese;
                                break;
                            case "(PD)":
                                rom.IsPublicDomain = true;
                                break;
                            case "(W)":
                                // not sure what this one is, don't need it in the name
                                break;
                            default:
                                lastIndex = i + 1;
                                break;
                        }
                        if (known)
                        {
                            rom.Name = rom.Name.Replace(info, "").Trim();
                            break;
                        }
                    }
                }
                int nextIndex = lastIndex < rom.Name.Length ? rom.Name.Substring(lastIndex).IndexOf("(") : -1;
                lastIndex = nextIndex != -1 ? lastIndex + nextIndex : -1; 
            }
        }
    }
}

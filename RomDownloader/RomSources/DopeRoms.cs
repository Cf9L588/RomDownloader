using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RomDownloader.Models;
using HtmlAgilityPack;
using System.Net;
using System.IO;

namespace RomDownloader.RomSources
{
    class DopeRoms : RomSource
    {

        public DopeRoms()
        {
            Name = "DopeRoms";
            URL = new Uri("http://doperoms.com");
        }

        internal override List<GameConsole> GetSystems()
        {
            //use the webclient to grab the source code of the page
            HttpWebRequest request = (WebRequest.Create(new Uri(URL, "roms"))) as HttpWebRequest;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            var status = response.StatusCode;
            if(status == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                string page = readStream.ReadToEnd();
                // pass the source code into the html document
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(page);

                // navigate the nodes to find the game console listings
                foreach (var node in doc.DocumentNode.SelectNodes("//html/body/div/div/div/table[2]/tr/td[1]/font[1]/table/tr/td/table/tr/td/a"))
                {
                    var system = new GameConsole(node.InnerText, new Uri(URL, node.Attributes["href"].Value), this);
                    SystemList.Add(system);
                    TriggerSystemFound(system);
                }
            }
            
            return SystemList; 
        }
        
        internal override List<Rom> GetSystemRoms(GameConsole system)
        {
            // scalar value to track the current page since pages iterate by values of 50
            int pageNum = 0;
            // first page (0) is the default url provided by the system
            string currentPage = system.RomListUrl.ToString();
            // Instantiate the ROMs list for the system
            system.Roms = new List<Rom>();
            // decalre the page variable that will hold the html source code for each page
            string page = string.Empty;
            HttpWebRequest request = (WebRequest.Create(system.RomListUrl)) as HttpWebRequest;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            var status = response.StatusCode;
            while (status == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                page = readStream.ReadToEnd();
                status = HttpStatusCode.Found;
                // I thought that the page would return empty if it wasn't a legit page, but it just returns a page with no roms....
                while (!string.IsNullOrEmpty(page))
                {
                    // Prepare a HtmlDocument from the HTMLAgilityPack
                    HtmlDocument doc = new HtmlDocument();
                    // load the HTMLDocument with the source code
                    doc.LoadHtml(page);

                    // Create a list of nodes containing the roms and their statuses on the current page
                    var listingNodes = doc.DocumentNode.SelectNodes("//html/body/div[contains(@id, 'dwrap')]/div[contains(@id, 'dbody')]/div[contains(@id, 'obody')]/table/tr[2]/td/table/tr/td/"  +
                        "div[contains(@id, 'csearchajax')]/font/table/tr")?.Where(node => node.Id == "listitem");

                    // this is the work around for ending the loop
                    // if any ROMs are on the page go to the next
                    // exit the loop if none
                    int romCount = 0;

                    // go through all of the rom nodes
                    foreach (var node in listingNodes)
                    {
                        // There'a a main node that contains the name and the url
                        var mainNode = node.SelectSingleNode(".//td/a");
                        // There is also a status
                        var statusNodes = node.SelectNodes(".//td/font/img");
                        // instantiate the rom based on the main and status nodes
                        Rom newRom = CreateRomsFromNodes(mainNode, statusNodes, system);
                        // if this is a legit rom
                        if (newRom != null && newRom.Name != "No Roms")
                        {
                            // Add it to the list
                            system.Roms.Add(newRom);
                            // increase the ROM counter
                            romCount++;
                        }
                    }

                    // If there were no ROMs
                    if (romCount == 0)
                        // Exit the loop
                        break;

                    // Clear the source code. This is just in case
                    page = string.Empty;
                    // Increment the page number
                    pageNum++;
                    // Construct the next page URL
                    currentPage = new Uri(URL, $"roms/{system.Id.Replace(" ", "_")}/ALL/{pageNum * 50}.html").ToString();
                    // This is a delay to try and fix the problem. I hate it
                    Task.Delay(500);
                    request = (WebRequest.Create(currentPage)) as HttpWebRequest;
                    response = (HttpWebResponse)request.GetResponse();
                    status = response.StatusCode;
                }
            }
            // Return the systems list of ROMs
            return system.Roms;
        }

        private void FixConsoleName(GameConsole system)
        {
            // There's a dictionary at the end of this class
            // it contains the name from the site and the common name
            if (ConsoleNames.ContainsKey(system.Id))
                system.Name = ConsoleNames[system.Id];
        }
        
        private Rom CreateRomsFromNodes(HtmlNode mainNode, HtmlNodeCollection StatusNodes, GameConsole system)
        {
            // Create a blank variable to start with
            // this is for the case that the nodes can't create a legit rom and we can return a null value
            Rom output = null;
            // Only try to make a rom if the mainNode is not null
            if(mainNode != null)
            {
                // Extract the rom name from the mainNode
                string romName = mainNode.Attributes["name"].Value;
                // Create a rom with the name and the href
                output = new Rom(romName, new Uri(this.URL, mainNode.Attributes["href"].Value), system);

                // If the status node is not null, try to get the status
                if(StatusNodes != null)
                {
                    // There can be multiple status nodes, go through each
                    foreach (var statusNode in StatusNodes)
                    {
                        // The alt text contains the key to the status legend
                        // This is only specific to this RomSource
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

                // Pass the rom to this function to remove some unneeded  parts from the name and to get value from them
                GetRomInfoFromName(output);
            }
            // Return the rom value (or null)
            return output;
        }

        private void GetRomInfoFromName(Rom rom)
        {
            // remove the .zip extension
            rom.Name = rom.Name.Replace(".zip", "")
                //Remove the .rar extension
                .Replace(".rar", "")
                // Remove the good dump marker
                .Replace("[!]", "")
                // Replace the checksum marker
                .Replace("[c]", "")
                .Trim();
            
            // get the index of the first occurance of "("
            // this will return a -1 if one is not found
            int lastIndex = rom.Name.IndexOf("(");

            // Loop as long as there is a "(" or that the lastindex point is less than the total length
            while (lastIndex != -1 && rom.Name.Length > lastIndex)
            {
                // Start with an empty string
                string info = string.Empty;
                // Loop from the lastindex where a "(" was found to the end of the word
                for (int i = lastIndex; i < rom.Name.Length; i++)
                {
                    // Add the current character to the info variable
                    // First one will be "("
                    char c = rom.Name[i];
                    info += c;
                    // If the character that we are on is ")"
                    if (c == ')')
                    {
                        // I put this here so we can keep track if we know this value
                        bool known = true;
                        // Find the purpose of this value
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
                                // If we do not know this value we shouldn't remove it
                                // instead, we set the lastIndex to one character after the current char
                                // when we get here the current char will always be ")"
                                // this means that below, we will search for the next () in the string after this one
                                lastIndex = i + 1;
                                known = false;
                                break;
                        }

                        // If we knew the purpose of the () value
                        if (known)
                        {
                            // Remove it from the string
                            rom.Name = rom.Name.Replace(info, "").Trim();
                            break;
                        }

                    }
                }
                //if lastIndex is before the end of the string we find the index of the next "("
                int nextIndex; // = lastIndex < rom.Name.Length ? rom.Name.Substring(lastIndex).IndexOf("(") : -1;
                if(lastIndex < rom.Name.Length)
                {
                    nextIndex = rom.Name.Substring(lastIndex).IndexOf("(");
                }
                else
                {
                    nextIndex = -1;
                }
                // if another instance occurs in the string, add it's value to the current lastIndex
                lastIndex = nextIndex != -1 ? lastIndex + nextIndex : -1; 
            }
        }

        // I added the string comparer so that we won't have to worry about cases - Chandler
        /// <summary>
        /// This dictionary corrects the names of consoles based on known incorrect values
        /// </summary>
        private static Dictionary<string, string> ConsoleNames = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "32x",  "Sega 32X" },
            { "Nintendo Nes", "Nintendo Entertainment System (NES)" },
            { "Amiga Cd32", "Amiga CD32"},
            { "Amstrad Cpc", "Amstrad CPC" },
            { "Apple Ii", "Apple II" },
            { "Atari Jaguar Cd", "Atari Jaguar CD" },
            { "Atari St", "Atari ST" },
            { "Atari Xe", "Atari XE" },
            { "Casio Pb-1000", "Casio PV-1000" },
            { "Dragon 32-64", "Dragon 32/64" },
            { "Epoch super Cassette Vision", "Epoch Super Cassette Vision" },
            { "Famicom Disk", "Famicom Disk System" },
            { "Neo Geo Cd", "Neo Geo CD" }
        };
    }
}

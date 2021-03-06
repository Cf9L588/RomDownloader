﻿using System;
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
                    FixConsoleName(system);
                    SystemList.Add(system);
                    TriggerSystemFound(system);
                }
            }
            
            return SystemList; 
        }

        internal override async Task<List<GameConsole>> GetSystemsAsync()
        {//use the webclient to grab the source code of the page
            HttpWebRequest request = (WebRequest.Create(new Uri(URL, "roms"))) as HttpWebRequest;
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            var status = response.StatusCode;
            if (status == HttpStatusCode.OK)
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
                    FixConsoleName(system);
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
        
        internal override async Task<List<Rom>> GetSystemRomsAsync(GameConsole system)
        {// scalar value to track the current page since pages iterate by values of 50
            int pageNum = 0;
            // first page (0) is the default url provided by the system
            string currentPage = system.RomListUrl.ToString();
            // Instantiate the ROMs list for the system
            system.Roms = new List<Rom>();
            // decalre the page variable that will hold the html source code for each page
            string page = string.Empty;
            HttpWebRequest request = (WebRequest.Create(system.RomListUrl)) as HttpWebRequest;
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
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
                    var listingNodes = doc.DocumentNode.SelectNodes("//html/body/div[contains(@id, 'dwrap')]/div[contains(@id, 'dbody')]/div[contains(@id, 'obody')]/table/tr[2]/td/table/tr/td/" +
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
                    request = (WebRequest.Create(currentPage)) as HttpWebRequest;
                    response = (HttpWebResponse)await request.GetResponseAsync();
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
            system.Name = system.Name.Trim();
            if (Core.ConsoleNames.ContainsKey(system.Id))
                system.Name = Core.ConsoleNames[system.Id];
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
                string downloadUrl = GetUrlFromRom(this.URL, romName, system.Id);
                output = new Rom(romName, downloadUrl, system);

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
                Core.GetRomInfoFromName(output);
            }
            // Return the rom value (or null)
            return output;
        }
        private string GetUrlFromRom(Uri domainUri, string romName, string systemId)
        {
            string output = new Uri(domainUri, $"/files/roms/{systemId}/GETFILE_{romName}").ToString();
            return output;
        }
    }
}
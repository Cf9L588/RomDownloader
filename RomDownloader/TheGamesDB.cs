using RomDownloader.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RomDownloader
{
    internal static class TheGamesDB
    {
        private static Uri baseUri = new Uri("http://thegamesdb.net/api/");

        internal static async Task GetGame(Rom rom)
        {
            await GetGame(rom.Name, rom.System.Name);
        }

        internal static async Task GetGame(string rom, string system)
        {
            var test = new Uri(baseUri, $"GetGame.php?exactname={rom}&platform={system}");
            HttpWebRequest request = (WebRequest.Create(test.ToString())) as HttpWebRequest;
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
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
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(page);
        }

    }
}

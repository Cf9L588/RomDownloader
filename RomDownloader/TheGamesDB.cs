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
        private static Uri baseUri = new Uri("http://thegamesdb.net");

        internal static async Task<GameInfo> GetGame(Rom rom)
        {
            return await GetGame(rom.Name, rom.System.Name);
        }

        internal static async Task<GameInfo> GetGame(string rom, string system)
        {
            var apiURL = new Uri(baseUri, $"api/GetGame.php?exactname={rom}&platform={system}");
            HttpWebRequest request = (WebRequest.Create(apiURL.ToString())) as HttpWebRequest;
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

            XmlNode node = xDoc.SelectSingleNode("Data/Game");
            GameInfo output = null;
            if (node != null)
            {
                string id = null;
                string title = null;
                string platformId = null;
                string platform = null;
                string overview = null;
                List<string> genres = new List<string>();
                int? players = null;
                bool coOp = false;
                string publisher = null;
                string developer = null;
                List<GameInfo.Image> images = new List<GameInfo.Image>();
                foreach (XmlNode child in node.ChildNodes)
                {
                    switch (child.Name)
                    {
                        case "id":
                            id = child.InnerText;
                            break;
                        case "GameTitle":
                            title = child.InnerText;
                            break;
                        case "PlatformId":
                            platformId = child.InnerText;
                            break;
                        case "Platform":
                            platform = child.InnerText;
                            break;
                        case "Overview":
                            overview = child.InnerText;
                            break;
                        case "Genres":
                            foreach (XmlNode genre in child.ChildNodes)
                            {
                                genres.Add(genre.InnerText);
                            }
                            break;
                        case "Players":
                            int holder;
                            if(int.TryParse(child.InnerText, out holder))
                            {
                                players = holder;
                            }
                            players = Convert.ToInt32(child.InnerText);
                            break;
                        case "Co-op":
                            switch (child.InnerText)
                            {
                                case "Yes":
                                    coOp = true;
                                    break;
                                case "No":
                                    coOp = false;
                                    break;
                            }
                            break;
                        case "Publisher":
                            publisher = child.InnerText;
                            break;
                        case "Developer":
                            developer = child.InnerText;
                            break;
                        case "Images":
                            foreach (XmlNode imageNode in child.ChildNodes)
                            {
                                GameInfo.Image.ImageStyle style;
                                switch (imageNode.Name)
                                {
                                    case "boxart":
                                        style = GameInfo.Image.ImageStyle.BoxArt;
                                        break;
                                    case "screenshot":
                                        style = GameInfo.Image.ImageStyle.ScreenShot;
                                        break;
                                    case "clearlogo":
                                        style = GameInfo.Image.ImageStyle.ClearLogo;
                                        break;
                                    case "banner":
                                        style = GameInfo.Image.ImageStyle.Banner;
                                        break;
                                    case "fanart":
                                        style = GameInfo.Image.ImageStyle.FanArt;
                                        break;
                                    default:
                                        throw new NotImplementedException();
                                }

                                string imageUri = null;
                                if(imageNode.ChildNodes.Count == 1)
                                {
                                    imageUri = imageNode.InnerText;
                                }
                                else
                                {
                                    foreach (XmlNode imageSizeNode in imageNode.ChildNodes)
                                    {
                                        if (imageSizeNode.Name == "original")
                                        {
                                            imageUri = imageSizeNode.InnerText;
                                        }
                                    }
                                }
                                if (!imageUri.StartsWith("banners"))
                                {
                                    imageUri = "banners/" + imageUri;
                                }
                                string url = new Uri(baseUri, imageUri).ToString();
                                images.Add(new GameInfo.Image(url, style));
                            }
                            break;
                    }
                }
                output = new GameInfo(id, title, genres, coOp, players, publisher, developer, images);
            }
            return output;
        }

    }
}

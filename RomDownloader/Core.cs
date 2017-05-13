using RomDownloader.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RomDownloader
{
    internal static class Core
    {
        private const string gameInfoFileName = "gameInfo.xml";
        private static string AppFolder = null;
        private static string GameInfoFilePath = null;
        private static List<RomSource> _sources;
        private static List<GameConsole> _systemList;
        private static Dictionary<string, HashSet<string>> SystemRoms;
        internal static List<GameConsole> SystemList
        {
            get
            {
                return _systemList;
            }

            set
            {
                _systemList = value;
            }
        }

        internal static List<RomSource> Sources
        {
            get
            {
                return _sources;
            }

            set
            {
                _sources = value;
            }
        }

        internal static void Init()
        {
            // Instantiate to List Objects
            Sources = new List<RomSource>();
            SystemList = new List<GameConsole>();
            // Gather all RomSources in the Assembly
            GetRomSourcesList();
            LoadGameInfoFile();
            // get a List of Systems available
            //GetSystemsList();
            SystemRoms = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Get all class that inherit from RomSource from within the assembly.
        /// </summary>
        private static void GetRomSourcesList()
        {
            // initialize a new list to work with
            List<RomSource> sources = new List<RomSource>();

            // Iterate through all the type in the assemby that inherit from  the RomSources class
            foreach (Type type in
                Assembly.GetAssembly(typeof(RomSource)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(RomSource))))
            {
                var source = (RomSource)(Activator.CreateInstance(type));
                source.SystemFound += OnSystemFound;
                sources.Add(source);
            }
            sources.Sort();
            Sources = sources;
        }

        /// <summary>
        /// Get all GameConsoles on each RomSource
        /// </summary>
        public static void  GetSystemsList()
        {
            // foreach RomSource in our program
            foreach(var source in Sources)
            {
                // populate system list from this source if it is null
                source.GetSystems();
            }
        }

        public static async Task  GetSystemsListAsync()
        {
            // foreach RomSource in our program
            foreach (var source in Sources)
            {
                // populate system list from this source if it is null
                await source.GetSystemsAsync();
            }
        }

        private static void OnSystemFound(GameConsole system)
        {
            SystemList.Add(system);
        }
        
        internal static List<string> GetSystemNames()
        {
            // Create a hashset so we can add things to it without worrying about duplicates
            // Added the string comparer to avoid case sensitive mismatching - Chandler
            HashSet<string> names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            //  add all systems names since we cannot have dups
            SystemList.ForEach(s => names.Add(s.Name));

            // Return it as a sorted list, because it's easier to work with
            // Convert HashSet to List
            var output = names.ToList();
            // Order the list
            output.Sort();
            //return the list
            return output;
        }

        internal static List<string> GetSystemRoms(string systemName)
        {
            HashSet<string> romNames = new HashSet<string>();

            foreach( var system in SystemList.Where(s => s.Name == systemName))
            {
                system.Roms?.ForEach(rom => romNames.Add(rom.Name));

            }

            var output = romNames.ToList();
            output.Sort();
            return output;
        }

        internal static List<Rom> GetRomsByName(string systemName, string romName)
        {

            List<Rom> output = new List<Rom>();

            foreach (var system in SystemList.Where(s => s.Name == systemName))
            {
                var matchingRoms = system.Roms?.Where(rom => romName == rom.Name);
                foreach (var rom in matchingRoms)
                {
                    output.Add(rom);
                }

            }
            return output;
        }

        internal static async Task<List<string>> GetSystemRomsAsync(string systemName)
        {
            if (!SystemRoms.ContainsKey(systemName))
                SystemRoms[systemName] = new HashSet<string>();

            var romNames = SystemRoms[systemName];

            foreach (var system in SystemList.Where(s => s.Name == systemName))
            {
                if (system.Roms == null)
                    await system.GetRomsAsync();


                foreach(var rom in system.Roms)
                    romNames.Add(rom.Name);

            }
            var output = romNames.ToList();
            output.Sort();
            return output;
        }
        
        internal static void GetRomInfoFromName(Rom rom)
        {
            // remove the .zip extension
            rom.Name = rom.Name.Replace(".zip", "")
                //Remove the .rar extension
                .Replace(".rar", "")
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
                        switch (info.Trim())
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
                        }
                        info = string.Empty;

                    }
                }
                //if lastIndex is before the end of the string we find the index of the next "("
                int nextIndex; // = lastIndex < rom.Name.Length ? rom.Name.Substring(lastIndex).IndexOf("(") : -1;
                if (lastIndex < rom.Name.Length)
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
            lastIndex = rom.Name.IndexOf("[");
            // Loop as long as there is a "[" or that the lastindex point is less than the total length
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
                    // If the character that we are on is "]"
                    if (c == ']')
                    {
                        // I put this here so we can keep track if we know this value
                        bool known = true;
                        // Find the purpose of this value
                        switch (info)
                        {
                            case "[!]":
                                break;
                            case "[c]":
                                break;
                            default:
                                // If we do not know this value we shouldn't remove it
                                // instead, we set the lastIndex to one character after the current char
                                // when we get here the current char will always be "]"
                                // this means that below, we will search for the next [] in the string after this one
                                lastIndex = i + 1;
                                //known = false;
                                break;
                        }

                        // If we knew the purpose of the [] value
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
                if (lastIndex < rom.Name.Length)
                {
                    nextIndex = rom.Name.Substring(lastIndex).IndexOf("[");
                }
                else
                {
                    nextIndex = -1;
                }
                // if another instance occurs in the string, add it's value to the current lastIndex
                lastIndex = nextIndex != -1 ? lastIndex + nextIndex : -1;
            }
        }
        
        internal static void LoadGameInfoFile()
        {
            //Create a path that is the users appdata path and the program name
            string AppFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), System.AppDomain.CurrentDomain.FriendlyName.Split('.')[0]);
            //Create the directory if it doesn't exist
            Directory.CreateDirectory(AppFolder);
            string GameInfoFilePath = Path.Combine(AppFolder, gameInfoFileName);
            if (!File.Exists(GameInfoFilePath))
            {
                CreateXmlFile(GameInfoFilePath);
            }
        }

        private static void CreateXmlFile(string filePath)
        {
            using (XmlWriter xmlWriter = XmlWriter.Create(filePath))
            {
                xmlWriter.WriteStartElement("Systems");
                xmlWriter.WriteEndElement();
                xmlWriter.Close();
            }
        }

        internal async static Task<GameInfo> GetGameInfo(string rom, string system)
        {
            if (File.Exists(GameInfoFilePath))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(GameInfoFilePath);
                foreach(XmlNode systemNode in doc.SelectNodes($"Systems/System"))
                {
                    if(systemNode.Attributes["Name"].Value == system)
                    {
                        foreach (XmlNode romNode in systemNode.SelectNodes("Roms/Rom"))
                        {
                            if(romNode.Attributes["Name"].Value == rom)
                            {
                                return ConvertNodeToInfo(romNode);
                            }
                        }
                    }
                }
            }
            var getInfo = TheGamesDB.GetGame(rom, system);
            GameInfo output = await getInfo;
            ConvertInfoToNode(output);
            return null;
        }

        private static GameInfo ConvertNodeToInfo(XmlNode node)
        {
            return null;
        }

        private static void ConvertInfoToNode(GameInfo info)
        {
            if (!File.Exists(GameInfoFilePath))
            {
                CreateXmlFile(GameInfoFilePath);
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(GameInfoFilePath);
            XmlNode systemsNode = doc.SelectSingleNode("Systems");
            XmlNode systemNode = systemsNode.SelectSingleNode($"System[@Name='{info.SystemName}']");

            if (systemNode == null)
            {
                XmlElement element = doc.CreateElement("System");
                element.SetAttribute("Name", info.SystemName);
                systemsNode.AppendChild(element);
                systemNode = systemsNode.SelectSingleNode($"System[@Name='{info.SystemName}']");
            }

            XmlNode romsNode = systemNode.SelectSingleNode("Roms");
            
            if (romsNode == null)
            {
                XmlElement element = doc.CreateElement("Roms");
                systemNode.AppendChild(element);
                romsNode = systemNode.SelectSingleNode("Roms");
            }
            XmlNode romNode = romsNode.SelectSingleNode($"Rom[@Name = '{info.SystemName}']");

            if (romNode == null)
            {
                XmlElement element = doc.CreateElement("Rom");
                element.SetAttribute("Name", info.Title);
                romsNode.AppendChild(element);
            }
            doc.Save(GameInfoFilePath);
        }


        // I added the string comparer so that we won't have to worry about cases - Chandler
        /// <summary>
        /// This dictionary corrects the names of consoles based on known incorrect values
        /// </summary>
        internal static Dictionary<string, string> ConsoleNames = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "3do",  "3DO" },
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

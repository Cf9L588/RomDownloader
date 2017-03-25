using RomDownloader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RomDownloader
{
    internal static class Core
    {
        private static List<RomSource> _sources;
        private static List<GameConsole> _systemList;
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
            // get a List of Systems available
            GetSystemsList();
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
                sources.Add((RomSource)(Activator.CreateInstance(type)));
            }
            sources.Sort();
            Sources = sources;
        }

        /// <summary>
        /// Get all GameConsoles on each RomSource
        /// </summary>
        private static void  GetSystemsList()
        {
            // foreach RomSource in our program
            foreach(var source in Sources)
            {
                // populate system list from this source if it is null
                if (source.SystemList == null)
                {
                    source.SystemFound += OnSystemFound;
                    source.GetSystems();
                }
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

    }
}

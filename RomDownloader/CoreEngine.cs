using RomDownloader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RomDownloader
{
    internal class CoreEngine
    {
        private List<RomSource> _sources;
        private List<GameConsole> _systemList;
        internal List<GameConsole> SystemList
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

        internal List<RomSource> Sources
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

        internal CoreEngine()
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
        private void GetRomSourcesList()
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
        private void GetSystemsList()
        {
            // foreach RomSource in our program
            foreach(var source in Sources)
            {
                // populate system list from this source if it is null
                if (source.SystemList == null)
                {
                    source.SystemFound += (System) => SytemList.Add(System);
                    source.GetSystems();
                }

                // Foreach system for the current source
                foreach(var system in source.SystemList)
                {
                    // Add it to the core list of systems
                    SystemList.Add(system);
                }
            }
        }
        
        internal List<string> GetSystemNames()
        {
            // Create a hashset so we can add things to it without worrying about duplicates
            HashSet<string> names = new HashSet<string>();
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

        internal List<string> GetRomsListForSystem(string systemName)
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

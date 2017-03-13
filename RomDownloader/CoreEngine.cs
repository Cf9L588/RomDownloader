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

        public List<GameConsole> SystemList
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
        

        public CoreEngine()
        {
            Sources = new List<RomSource>();
            SystemList = new List<GameConsole>();
            GetRomSourcesList();
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
            foreach(var source in Sources)
            {
                foreach(var system in source.SystemList)
                {
                    SystemList.Add(system);
                }
            }
            //Sources.ForEach(s => s.SystemList.ForEach(l => SystemList.Add(l)));
        }
        
        internal List<string> GetSystemNames()
        {
            HashSet<string> names = new HashSet<string>();
            SystemList.ForEach(s => names.Add(s.Name));
            return names.ToList();
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

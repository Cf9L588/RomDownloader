using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomDownloader.Models
{
    internal abstract class RomSource
    {
        internal delegate void RomFoundHandler(Rom rom);
        internal event RomFoundHandler RomFound;
        internal delegate void SystemFoundHandler(GameConsole system);
        internal event SystemFoundHandler SystemFound;

        internal string Name { get;  set; }
        internal Uri URL { get;  set; }
        protected List<GameConsole> systemList;
        private List<Rom> romList;

        internal List<GameConsole> SystemList
        {
            get
            {
                return systemList;
            }
            set
            {
                systemList = value;
            }
        }

        protected List<Rom> RomList
        {
            get
            {
                return romList;
            }

            set
            {
                romList = value;
            }
        }

        internal RomSource(string name, string url)
        {
            Name = name;
            URL = new Uri(url);
        }

        protected void TriggerSystemFound(GameConsole system)
        {
            SystemFound?.Invoke(system);
        }

        internal RomSource()
        {
            SystemList = new List<GameConsole>();
            RomList = new List<Rom>();
        }

        abstract internal List<GameConsole> GetSystems();

        abstract internal List<Rom> GetSystemRoms(GameConsole system);

        abstract internal Task<List<Rom>> GetSystemRomsAsync(GameConsole system);
        
    }
}

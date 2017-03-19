using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomDownloader.Models
{
    internal abstract class RomSource
    {
        internal string Name { get;  set; }
        internal Uri URL { get;  set; }
        protected List<GameConsole> systemList;

        internal delegate void SystemFoundHandler(GameConsole system);
        internal event SystemFoundHandler SystemFound;

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

        }

        abstract internal Task<List<GameConsole>> GetSystems();

        abstract internal List<Rom> GetSystemRoms(GameConsole system);
    }
}

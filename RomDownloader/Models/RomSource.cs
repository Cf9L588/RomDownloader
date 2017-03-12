using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomDownloader.Models
{
    internal abstract class RomSource
    {
        internal readonly string Name;
        internal readonly Uri URL;
        protected List<GameConsole> systemList;

        internal List<GameConsole> SystemList
        {
            get
            {
                if (systemList == null)
                    systemList = GetSystems();

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


        abstract internal List<GameConsole> GetSystems();
    }
}

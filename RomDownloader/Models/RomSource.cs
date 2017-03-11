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
        internal readonly string URL;

        internal RomSource(string name, string url)
        {
            Name = name;
            URL = url;
        }

        abstract internal List<GameConsole> GetSystems();
    }
}

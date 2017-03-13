using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomDownloader.Models
{
    internal class GameConsole
    {
        internal readonly RomSource Source;
        internal readonly string Name;
        internal readonly Uri RomListUrl;
        private List<Rom> roms;

        internal List<Rom> Roms
        {
            get
            {
                if(roms == null)
                {
                    roms = Source.GetSystemRoms(this);
                }
                return roms;
            }

            set
            {
                roms = value;
            }
        }

        internal GameConsole(string name, string url, RomSource source)
            : this(name, new Uri(url), source) { }

        internal GameConsole(string name, Uri url, RomSource source)
        {
            Name = name;
            RomListUrl = url;
            Source = source;
        }

    }
}

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
        internal string Name;
        internal readonly Uri RomListUrl;
        internal string Id { get; set; }
        private List<Rom> roms;

        internal List<Rom> Roms
        {
            get
            {
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
            Id = Name;
        }

        internal async Task<List<Rom>> GetRomsAsync()
        {
            return await Source.GetSystemRomsAsync(this);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RomDownloader.Models;

namespace RomDownloader.RomSources
{
    class DopeRoms : RomSource
    {
        internal DopeRoms(): base("DopeRoms", "http://doperoms.com/")
        {
            
        }

        internal override List<GameConsole> GetSystems()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomDownloader.Models
{
    interface IRomHandler
    {
        void DownloadRom(string url);
    }
}

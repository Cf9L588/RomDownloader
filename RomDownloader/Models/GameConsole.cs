﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomDownloader.Models
{
    public class GameConsole
    {
        internal readonly string Name;
        internal readonly string RomListUrl;

        public GameConsole(string name, string url)
        {
            Name = name;
            RomListUrl = url;
        }
    }
}

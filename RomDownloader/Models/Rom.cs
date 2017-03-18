using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomDownloader.Models
{
    class Rom
    {
        #region Fields
        private string _name;
        private string _downloadUrl;
        private StatusType _status;
        private LanguageType _language;
        private CheckSumType _checksum;
        private bool _isPublicDomain;
        private GameConsole _system;
        #endregion

        #region Properties
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if(!string.IsNullOrEmpty(value))
                    _name = value;
            }
        }

        public string DownloadUrl
        {
            get
            {
                return _downloadUrl;
            }

            set
            {
                _downloadUrl = value;
            }
        }

        internal StatusType Status
        {
            get
            {
                return _status;
            }

            set
            {
                _status = value;
            }
        }

        internal LanguageType Language
        {
            get
            {
                return _language;
            }

            set
            {
                _language = value;
            }
        }

        public bool IsPublicDomain
        {
            get
            {
                return _isPublicDomain;
            }

            set
            {
                _isPublicDomain = value;
            }
        }

        internal GameConsole System
        {
            get
            {
                return _system;
            }

            set
            {
                _system = value;
            }
        }

        internal RomSource Source
        {
            get { return System.Source; }
        }

        internal CheckSumType Checksum
        {
            get
            {
                return _checksum;
            }

            set
            {
                _checksum = value;
            }
        }
        #endregion

        public Rom(string name, string url, GameConsole system)
        {
            Name = name;
            DownloadUrl = url;
            System = system;
        }

        public Rom(string name, Uri url, GameConsole system): this(name, url.ToString(), system){ }

        internal enum StatusType
        {
            Unknown,
            Good,
            Bad
        }

        internal enum LanguageType
        {
            Unknown,
            English,
            Japanese
        }

        internal enum CheckSumType
        {
            Unknown,
            Good, 
            Bad
        }
    }
}

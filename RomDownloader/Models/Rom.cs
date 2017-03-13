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
        private bool _isPublicDomain;
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
        #endregion

        public Rom(string name, string url)
        {
            Name = name;
            DownloadUrl = url;
        }

        public Rom(string name, Uri url): this(name, url.ToString()){ }

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
    }
}

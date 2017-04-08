using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomDownloader.Models
{
    class GameInfo
    {
        #region fields
        private string id;
        private string title;
        private List<string> genres;
        private bool coOp;
        private int players;
        private string publisher;
        private string developer;
        private List<Image> images;
        

        

        #endregion

        #region properties
        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
            }
        }

        public List<string> Genres
        {
            get
            {
                return genres;
            }

            set
            {
                genres = value;
            }
        }

        public bool CoOp
        {
            get
            {
                return coOp;
            }

            set
            {
                coOp = value;
            }
        }

        public int Players
        {
            get
            {
                return players;
            }

            set
            {
                players = value;
            }
        }

        public string Publisher
        {
            get
            {
                return publisher;
            }

            set
            {
                publisher = value;
            }
        }

        public string Developer
        {
            get
            {
                return developer;
            }

            set
            {
                developer = value;
            }
        }

        internal List<Image> Images
        {
            get
            {
                return images;
            }

            set
            {
                images = value;
            }
        }


        #endregion

        public GameInfo(string id, string title, List<string> genres, bool coOp, int players, string publisher, string developer, List<Image> images)
        {
            Id = id;
            Title = title;
            Genres = genres;
            CoOp = coOp;
            Players = players;
            Publisher = publisher;
            Developer = developer;
            this.Images = images;   
        }
        public GameInfo(string id, string title, List<string> genres, bool coOp, int players, List<Image> images) : this (id, title, genres, coOp, players, null, null, images)
        {
        }

        public class Image
        {
            public string Url;
            public ImageStyle Style;

            public Image(string url, ImageStyle style)
            {
                Url = url;
                Style = style;
            }

            public enum ImageStyle
            {
                ScreenShot,
                BoxArt
            }
            
        }
    }
}

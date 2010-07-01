using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using IMDb.DataFiles.Parser;

namespace IMDb.DataFiles.Types
{
    public class Song
    {
        public string Title
        {
            get;
            set;
        }

        public string Lyricist
        {
            get;
            set;
        }

        public string Composer
        {
            get;
            set;
        }

        public string Performer
        {
            get;
            set;
        }

        public string Publisher
        {
            get;
            set;
        }

        public override bool Equals(object obj)
        {
            var song = obj as Song;
            if (song == null)
            {
                return false;
            }

            return this.Title == song.Title &&
                   this.Composer == song.Composer &&
                   this.Lyricist == song.Lyricist &&
                   this.Performer == song.Performer &&
                   this.Publisher == song.Publisher;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

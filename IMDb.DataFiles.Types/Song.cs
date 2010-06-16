using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IMDb.DataFiles.Types
{
    public class Song
    {
        private static readonly Regex SongTitleLineRegex = new Regex(@"^- ""?(?<title>.*?)""?( \(uncredited\))?$",
                                                                     RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.ExplicitCapture);
        private static readonly Regex LycricistLineRegex = new Regex(@"^  Lyrics by '?(?<lyricist>.*?)('? \(qv\))?$",
                                                                     RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.ExplicitCapture);
        private static readonly Regex ComposerLineRegex = new Regex(@"^  Music by '?(?<composer>.*?)('? \(qv\))?$",
                                                                     RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.ExplicitCapture);
        private static readonly Regex WriterLineRegex = new Regex(@"^  Written by '?(?<writer>.*?)('? \(qv\))?$",
                                                                     RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.ExplicitCapture);
        private static readonly Regex PerformerLineRegex = new Regex(@"^  Performed by '?(?<performer>.*?)('? \(qv\))?$",
                                                                     RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.ExplicitCapture);
        
        // This one's much more complicated and needs thinking out...
        private static readonly Regex PublisherLineRegex = new Regex(@"^  (Courtesy of|Published by) '?(?<publisher>.*)('? \(qv\))?$",
                                                                     RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.ExplicitCapture);

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

        public static Song Parse(IList<string> soundtrackDefinition)
        {
            Song song = new Song();

            foreach (var songDetails in soundtrackDefinition)
            {
                if (SongTitleLineRegex.IsMatch(songDetails))
                {
                    song.Title = SongTitleLineRegex.Match(songDetails).Groups["title"].Value;
                    continue;
                }

                if (LycricistLineRegex.IsMatch(songDetails))
                {
                    song.Lyricist = LycricistLineRegex.Match(songDetails).Groups["lyricist"].Value;
                    continue;
                }

                if (ComposerLineRegex.IsMatch(songDetails))
                {
                    song.Composer = ComposerLineRegex.Match(songDetails).Groups["composer"].Value;
                    continue;
                }

                if (WriterLineRegex.IsMatch(songDetails))
                {
                    var writer = WriterLineRegex.Match(songDetails).Groups["writer"].Value;
                    song.Composer = writer;
                    song.Lyricist = writer;
                    continue;
                }

                if (PerformerLineRegex.IsMatch(songDetails))
                {
                    song.Performer = PerformerLineRegex.Match(songDetails).Groups["performer"].Value;
                    continue;
                }

                if (songDetails.Trim() == "Traditional")
                {
                    song.Composer = songDetails.Trim();
                    continue;
                }
            }

            return song;
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

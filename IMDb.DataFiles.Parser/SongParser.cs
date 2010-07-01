using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMDb.DataFiles.Types;
using log4net;
using System.Text.RegularExpressions;

namespace IMDb.DataFiles.Parser
{
    public class SongParser
    {
        private static readonly Regex SongTitleLineRegex = new Regex(@"^- ""?(?<title>.*?)""?( \(uncredited\))?$",
                                                                     RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        // Names can be in one of two formats: 
        //  1. qv-referenced (quoted in single quotes followed by the string (qv))
        //  2. non-referenced (no quotes, no tailing string).
        // 
        // Names are divided by commas and a single space; the final name is preceded 
        // by the string " and " and no comma.  Both formats of name can optionally be
        // followed by the string " (uncredited)".

        private const string NameBasedRegex = @"^  {0} by (?<name>('.*' \(qv\)( \(uncredited\))?)|(.*))( \(uncredited\))?$";

        private static readonly Regex LycricistLineRegex = new Regex(string.Format(NameBasedRegex, @"(?:Lyrics|\w+ lyrics)"),
                                                                     RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.ExplicitCapture);
        private static readonly Regex ComposerLineRegex = new Regex(string.Format(NameBasedRegex, "Music"),
                                                                     RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.ExplicitCapture);
        private static readonly Regex WriterLineRegex = new Regex(string.Format(NameBasedRegex, @"(Written)?"),
                                                                     RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.ExplicitCapture);
        private static readonly Regex PerformerLineRegex = new Regex(string.Format(NameBasedRegex, @"(?:Sung|Performed)"),
                                                                     RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        // This one's much more complicated and needs thinking out...
        private static readonly Regex PublisherLineRegex = new Regex(@"^  (Courtesy of|Published by) '?(?<publisher>.*)('? \(qv\))?$",
                                                                     RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        private ILog Logger { get; set; }

        public SongParser(ILog logger)
        {
            Logger = logger;
        }

        public Song Parse(IList<string> soundtrackDefinition)
        {
            Song song = new Song();

            foreach (var songDetails in soundtrackDefinition)
            {
                if (SongTitleLineRegex.IsMatch(songDetails))
                {
                    song.Title = SongTitleLineRegex.Match(songDetails).Groups["title"].Value;
                    continue;
                }

                if (WriterLineRegex.IsMatch(songDetails))
                {
                    var writer = GetNamesFromRegex(songDetails, WriterLineRegex);
                    song.Composer = writer;
                    song.Lyricist = writer;

                    continue;
                }

                if (LycricistLineRegex.IsMatch(songDetails))
                {
                    song.Lyricist = GetNamesFromRegex(songDetails, LycricistLineRegex);
                    continue;
                }

                if (ComposerLineRegex.IsMatch(songDetails))
                {
                    song.Composer = GetNamesFromRegex(songDetails, ComposerLineRegex);
                    continue;
                }

                if (PerformerLineRegex.IsMatch(songDetails))
                {
                    song.Performer = GetNamesFromRegex(songDetails, PerformerLineRegex);
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

        private static string GetNamesFromRegex(string songDetails, Regex regex)
        {
            var name = regex.Match(songDetails).Groups["name"].Value;
            Regex qvName = new Regex(@"'(?<name>.*?)' \(qv\)");
            if (qvName.IsMatch(name))
            {
                name = qvName.Replace(name, x => x.Groups["name"].Value);
            }

            return name;
        }
    }
}

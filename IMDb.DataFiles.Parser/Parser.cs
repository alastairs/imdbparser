using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using IMDb.DataFiles.Parser.Types;

namespace IMDb.DataFiles.Parser
{
    public class Parser
    {
        private static readonly Regex MovieTitleLineRegex = new Regex(
            @"^\# ""?(?<title>.*)""? \((?<year>\d{4})\)( (\{(?<episodeTitle>.*) \(\#(?<series>\d+)\.(?<episode>\d+)\)\})| \(VG\))?$");
        private static readonly Regex SongTitleLineRegex = new Regex(@"^- "".*""( \(uncredited\))?$");

        private Parser() { }

        public static IEnumerable<SoundtrackRecord> Parse(string soundtracksListFile) {
            FileStream soundtracksList = File.OpenRead(soundtracksListFile);

            yield return new SoundtrackRecord();
        }
    }
}

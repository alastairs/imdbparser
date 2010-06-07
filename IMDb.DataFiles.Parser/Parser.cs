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
            @"^\# ""?(?<title>.*)""? \((?<year>\d{4})\)( (\{(?<episodeTitle>.*) \(\#(?<series>\d+)\.(?<episode>\d+)\)\})| \((?<videoGame>VG)\))?$");
        private static readonly Regex SongTitleLineRegex = new Regex(@"^- "".*""( \(uncredited\))?$");

        private Parser() { }

        public static IEnumerable<SoundtrackRecord> Parse(string soundtracksListFile) {
            FileStream soundtracksList = File.OpenRead(soundtracksListFile);

            var file = new StreamReader(soundtracksList);
            var line = file.ReadLine();
            while (line != null)
            {
                Production production = null;
                IList<Song> songs = new List<Song>();

                while (line != string.Empty)
                {
                    if (MovieTitleLineRegex.IsMatch(line))
                    {
                        production = Production.Parse(MovieTitleLineRegex.Match(line));
                    }

                    if (SongTitleLineRegex.IsMatch(line))
                    {
                        
                    }

                    line = file.ReadLine();
                }

                yield return new SoundtrackRecord
                {
                    Production = production,
                    Songs = songs
                };

                line = file.ReadLine();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using IMDb.DataFiles.Types;
using IMDb.DataFiles.Parser.Factories;
using IMDb.DataFiles.Parser.Interfaces;

namespace IMDb.DataFiles.Parser
{
    public class SoundtrackFileParser : IImdbDataFileParser<SoundtrackRecord>
    {
        private static readonly Regex SongTitleLineRegex = new Regex(@"^- "".*""( \(uncredited\))?$");

        public IEnumerable<SoundtrackRecord> Parse(Stream dataFileStream) 
        {
            var file = new StreamReader(dataFileStream);
            var line = file.ReadLine();
            while (line != null)
            {
                IProduction production = null;
                IList<Song> songs = new List<Song>();

                while (line != string.Empty)
                {
                    if (line.StartsWith("#"))
                    {
                        production = Production.Parse(line);
                    }

                    if (line.StartsWith("-"))
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

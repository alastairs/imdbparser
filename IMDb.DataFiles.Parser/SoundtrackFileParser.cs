using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IMDb.DataFiles.Types;
using IMDb.DataFiles.Parser.Factories;
using IMDb.DataFiles.Parser.Interfaces;

namespace IMDb.DataFiles.Parser
{
    public class SoundtrackFileParser : IImdbDataFileParser<SoundtrackRecord>
    {
        public IEnumerable<SoundtrackRecord> Parse(Stream dataFileStream)
        {
            var file = new StreamReader(dataFileStream);
            var line = file.ReadLine();
            while (line != null)
            {
                if (line.StartsWith("#"))
                {
                    yield return ReadProductionSoundtrack(line, file);
                }

                line = file.ReadLine();
            }
        }

        private SoundtrackRecord ReadProductionSoundtrack(string productionDefinition, StreamReader file)
        {
            IProduction production = Production.Parse(productionDefinition);

            string line = file.ReadLine();
            IList<string> soundtrackDefinition = new List<string>();
            IList<Song> songs = new List<Song>();
            while (!string.IsNullOrEmpty(line))
            {
                if (line.StartsWith("-") && soundtrackDefinition.Count > 0)
                {
                    songs.Add(Song.Parse(soundtrackDefinition));
                    soundtrackDefinition.Clear();
                }
                soundtrackDefinition.Add(line);
                line = file.ReadLine();
            }

            songs.Add(Song.Parse(soundtrackDefinition));

            return new SoundtrackRecord
            {
                Production = production,
                Songs = songs
            };            
        }
    }
}

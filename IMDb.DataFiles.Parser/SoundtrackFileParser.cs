using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IMDb.DataFiles.Types;
using IMDb.DataFiles.Parser.Factories;
using IMDb.DataFiles.Parser.Interfaces;
using log4net;

namespace IMDb.DataFiles.Parser
{
    public class SoundtrackFileParser : IImdbDataFileParser<SoundtrackRecord>
    {
        private ILog Logger { get; set; }

        public SoundtrackFileParser(ILog logger)
        {
            Logger = logger;
        }

        public IEnumerable<SoundtrackRecord> Parse(Stream dataFileStream)
        {
            var file = new StreamReader(dataFileStream);
            var line = file.ReadLine();
            while (line != null)
            {
                if (line.StartsWith("#"))
                {
                    //Logger.DebugFormat("Found production definition: {0}", line);
                    yield return ReadProductionSoundtrack(line, file);
                }

                line = file.ReadLine();
            }
        }

        private SoundtrackRecord ReadProductionSoundtrack(string productionDefinition, StreamReader file)
        {
            IProduction production = Production.Parse(productionDefinition);

            string line = file.ReadLine();
            IList<string> artistCredits = new List<string>();
            IList<Song> songs = new List<Song>();
            while (!string.IsNullOrEmpty(line))
            {
                if (line.StartsWith("-"))
                {
                    //Logger.DebugFormat("Found song title definition: {0}", line);

                    if (artistCredits.Count > 0)
                    {
                        songs.Add(Song.Parse(artistCredits));
                        
                        // There are still artist credits listed for the previous song,
                        // so clear the list of artist credits ready for this new song.
                        artistCredits.Clear();
                    }
                }
                else
                {
                    //Logger.DebugFormat("Found song artist definition: {0}", line);
                }
                    
                artistCredits.Add(line);
                line = file.ReadLine();
            }

            songs.Add(Song.Parse(artistCredits));

            return new SoundtrackRecord
            {
                Production = production,
                Songs = songs
            };            
        }
    }
}

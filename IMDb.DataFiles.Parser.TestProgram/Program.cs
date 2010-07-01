using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMDb.DataFiles.Parser;
using System.IO;
using IMDb.DataFiles.Types;
using System.Diagnostics;

namespace IMDb.DataFiles.Parser.TestProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            var soundtracksListPath = @".\soundtracks.list";
            if (args.Length > 0)
            {
                soundtracksListPath = args[0];
            }

            Console.WriteLine("Opening file for read...");
            Stream soundtracksStream = File.OpenRead(soundtracksListPath);
            var reader = new StreamReader(soundtracksStream);
            Console.WriteLine("...done");

            Console.WriteLine("Counting productions in the file...");
            string line = reader.ReadLine();
            IList<string> rawProductions = new List<string>();
            IList<string> rawSongs = new List<string>();
            while (line != null)
            {
                if (line.StartsWith("#"))
                {
                    rawProductions.Add(line);
                }

                if (line.StartsWith("-"))
                {
                    line = line.Replace(" (uncredited)", string.Empty).TrimStart('-').Trim();
                    rawSongs.Add(line);
                }

                line = reader.ReadLine();
            }
            Console.WriteLine("...done");
            Console.WriteLine("Counted {0} productions and {1} songs.", rawProductions.Count, rawSongs.Count);

            soundtracksStream.Seek(0, SeekOrigin.Begin);

            Console.WriteLine("Parsing productions in the file...");
            var parser = new SoundtrackFileParser();

            IList<string> failedParses = new List<string>();

            IEnumerable<SoundtrackRecord> records = null;

            try
            {
                records = parser.Parse(soundtracksStream);
            }
            catch (ParseException e)
            {
                failedParses.Add(e.ParseFailure);
            }

            Console.WriteLine("...done");

            var parsedSongsCount = (from r in records
                                   select r.Songs.Count).Sum();
            Console.WriteLine("Parsed {0} productions of {1} total and {2} songs of {3} total.", 
                records.Count(), rawProductions.Count, parsedSongsCount, rawSongs.Count);

            Console.ReadLine();
        }
    }
}

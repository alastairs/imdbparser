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
            int productionCount = 0, songCount = 0;
            while (line != null)
            {
                if (line.StartsWith("#"))
                {
                    productionCount++;
                }

                if (line.StartsWith("-"))
                {
                    songCount++;
                }

                line = reader.ReadLine();
            }
            Console.WriteLine("...done");
            Console.WriteLine("Counted {0} productions and {1} songs.", productionCount, songCount);

            soundtracksStream.Seek(0, SeekOrigin.Begin);

            Console.WriteLine("Parsing productions in the file...");
            var parser = new SoundtrackFileParser();
            IEnumerable<SoundtrackRecord> records = parser.Parse(soundtracksStream);
            Console.WriteLine("...done");

            var parsedSongCount = (from r in records
                                  select r.Songs.Count).Sum();
            Console.WriteLine("Parsed {0} productions of {1} total and {2} songs of {3} total.", 
                records.Count(), productionCount, parsedSongCount, songCount);

            Console.ReadLine();
        }
    }
}

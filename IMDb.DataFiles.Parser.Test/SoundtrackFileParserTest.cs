using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using MbUnit.Framework;
using System.IO;
using IMDb.DataFiles.Types;

namespace IMDb.DataFiles.Parser.Test
{
    /// <summary>
    /// Summary description for SoundtrackFileParserTest
    /// </summary>
    [TestFixture]
    public class SoundtrackFileParserTest
    {
        [Test]
        public void TestParseCorrectlyParsesRecordsFromAStream()
        {
            #region data = ...

            var data = @"CRC: 0x75F0BF28  File: soundtracks.list  Date: Sat May 29 17:00:00 2010

Copyright 1991-2010 The Internet Movie Database Ltd. All rights reserved.

http://www.imdb.com

soundtracks.list

2010-5-27 
-----------------------------------------------------------------------------

SOUNDTRACKS LIST
================



# ""$weepstake$"" (1979)
- ""Without a Dream""
  Music by 'Charles Fox (I)' (qv)
  Lyrics by 'Norman Gimbel' (qv)
  Performed by 'Ron Dante' (qv)

# ""'Allo 'Allo!"" (1982) {A Marriage of Inconvenience (#5.6)}
- ""Hear my song, Violetta"" (uncredited)
  Written by Othmar Klose, Rudolph Lukesch and 'Harry S. Pepper' (qv)
  Performed by 'Carmen Silvera' (qv)

# ""'Allo 'Allo!"" (1982) {Camp Dance (#4.2)}
- ""Mad About the Boy"" (uncredited)
  Written by 'Noel Coward' (qv)
  Performed by 'Guy Siner' (qv)

# ""'Allo 'Allo!"" (1982) {Christmas Puddings (#5.19)}
- ""Boom""
  Written by 'E. Ray Goetz' (qv) (uncredited) and 'Charles Trenet' (qv) (uncredited)
  Performed by 'Carmen Silvera' (qv)

# ""'Allo 'Allo!"" (1982) {Communists in the Cupboard (#5.14)}
- ""Love Is Where You Find It"" (uncredited)
  Music by 'Nacio Herb Brown' (qv)
  Lyrics by 'Earl K. Brent' (qv)
  Performed by 'Carmen Silvera' (qv)
- ""Ol' Man River"" (uncredited)
  Music by 'Jerome Kern' (qv)
  Lyrics by 'Oscar Hammerstein II' (qv)
  Performed by 'Carmen Silvera' (qv)";

            #endregion

            #region expectedSequence = ...

            IEnumerable<SoundtrackRecord> expectedSequence = new List<SoundtrackRecord>
            {
                new SoundtrackRecord {
                    Production = new Movie {
                        Title = "$weepstake$",
                        Year = 1979
                    },
                    Songs = new List<Song> {
                        new Song {
                            Title = "Without a Dream",
                            Composer = "Charles Fox (I)",
                            Lyricist = "Norman Gimbel",
                            Performer = "Ron Dante"
                        }
                    }
                },
                new SoundtrackRecord {
                    Production = new TelevisionShow {
                        Title = "'Allo 'Allo",
                        Year = 1982,
                        EpisodeTitle = "A Marriage of Inconvenience",
                        SeriesNumber = 5,
                        EpisodeNumber = 6
                    },
                    Songs = new List<Song> {
                        new Song {
                            Title = "Hear my song, Violetta",
                            Composer = "Othmar Klose, Rudolph Lukesch and 'Harry S. Pepper'",
                            Lyricist = "Othmar Klose, Rudolph Lukesch and 'Harry S. Pepper'",
                            Performer = "Carmen Silvera"
                        }
                    }
                },
                new SoundtrackRecord {
                    Production = new TelevisionShow {
                        Title = "'Allo 'Allo",
                        Year = 1982,
                        EpisodeTitle = "Camp Dance",
                        SeriesNumber = 4,
                        EpisodeNumber = 2
                    },
                    Songs = new List<Song> {
                        new Song {
                            Title = "Mad About the Boy",
                            Composer = "Noel Coward",
                            Lyricist = "Noel Coward",
                            Performer = "Guy Siner"
                        }
                    }
                },
                new SoundtrackRecord {
                    Production = new TelevisionShow {
                        Title = "'Allo 'Allo",
                        Year = 1982,
                        EpisodeTitle = "Christmas Puddings",
                        SeriesNumber = 5,
                        EpisodeNumber = 19
                    },
                    Songs = new List<Song> {
                        new Song {
                            Title = "Boom",
                            Composer = "E. Ray Goetz and Charles Trenet",
                            Lyricist = "E. Ray Goetz and Charles Trenet",
                            Performer = "Carmen Silvera"
                        }
                    }
                },
                new SoundtrackRecord {
                    Production = new TelevisionShow {
                        Title = "'Allo 'Allo",
                        Year = 1982,
                        EpisodeTitle = "Communists in the Cupboard",
                        SeriesNumber = 5,
                        EpisodeNumber = 14
                    },
                    Songs = new List<Song> {
                        new Song {
                            Title = "Love Is Where You Find It",
                            Composer = "Nacio Herb Brown",
                            Lyricist = "Earl K. Brent",
                            Performer = "Carmen Silvera"
                        },
                        new Song {
                            Title = "Ol' Man River",
                            Composer = "Jerome Kern",
                            Lyricist = "Oscar Hammerstein II",
                            Performer = "Carmen Silvera"
                        }
                    }
                }
            };

            #endregion

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(data);

            IEnumerable<SoundtrackRecord> records = new SoundtrackFileParser().Parse(stream);
            Assert.AreElementsEqual(expectedSequence, records);
        }
    }
}

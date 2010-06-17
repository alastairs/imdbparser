using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using MbUnit.Framework;
using IMDb.DataFiles.Types;

namespace IMDb.DataFiles.Parser.Test
{
    [TestFixture]
    public class SongTest
    {
        [Test]
        public void TestParseCreatesValidObjectWithCorrectlyFormattedDefinition()
        {
            #region data = ...
            var data = @"- ""Without a Dream""
  Music by 'Charles Fox (I)' (qv)
  Lyrics by 'Norman Gimbel' (qv)
  Performed by 'Ron Dante' (qv)";
            #endregion

            IList<string> songDefinition = new List<string>(data.Split(new string[] {"\r\n"}, StringSplitOptions.None));
            var expected = new Song
            {
                Title = "Without a Dream",
                Composer = "Charles Fox (I)",
                Lyricist = "Norman Gimbel",
                Performer = "Ron Dante"
            };

            Song actual = Song.Parse(songDefinition);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestParseCreatesValidObjectWithMultipleWriters()
        {
            #region data = ...
            var data = @"- ""Hear my song, Violetta"" (uncredited)
  Written by Othmar Klose, Rudolph Lukesch and 'Harry S. Pepper' (qv)
  Performed by 'Carmen Silvera' (qv)";
            #endregion

            IList<string> songDefinition = new List<string>(data.Split(new string[] { "\r\n" }, StringSplitOptions.None));
            var expected = new Song
            {
                Title = "Hear my song, Violetta",
                Composer = "Othmar Klose, Rudolph Lukesch and Harry S. Pepper",
                Lyricist = "Othmar Klose, Rudolph Lukesch and Harry S. Pepper",
                Performer = "Carmen Silvera"
            };

            Song actual = Song.Parse(songDefinition);

            Assert.AreEqual(expected, actual);
        }
    }
}

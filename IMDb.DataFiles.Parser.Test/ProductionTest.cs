using System;
using System.Collections.Generic;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using IMDb.DataFiles.Parser.Types;

namespace IMDb.DataFiles.Parser.Test
{
    [TestFixture]
    public class ProductionTest
    {
        [Test]
        public void TestParseCreatesValidMovieObjectWithNonWordCharacters()
        {
            var tvShowString = @"# ""$weepstake$"" (1979)";
            var expected = new Movie
            {
                Title = "$weepstake$",
                Year = 1979
            };

            var actual = Production.ParseProduction(tvShowString);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestParseCreatesValidTelevisionShowObjectWithNonWordCharacters()
        {
            var movieString = @"# ""'Allo 'Allo!"" (1982) {A Marriage of Inconvenience (#5.6)}";
            var expected = new TelevisionShow
            {
                Title = "'Allo 'Allo!",
                Year = 1982,
                EpisodeTitle = "A Marriage of Inconvenience",
                SeriesNumber = 5,
                EpisodeNumber = 6
            };

            var actual = Production.ParseProduction(movieString);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestParseCreatesValidVideoGameObjectWithNonWordCharacters()
        {
            var videoGameString = @"# .hack//G.U. Vol. 1: Saitan (2006) (VG)";
            var expected = new VideoGame
            {
                Title = ".hack//G.U. Vol. 1: Saitan",
                Year = 2006
            };

            var actual = Production.ParseProduction(videoGameString);
            Assert.AreEqual(expected, actual);
        }
    }
}

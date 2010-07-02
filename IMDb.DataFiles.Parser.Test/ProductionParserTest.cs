using System;
using System.Collections.Generic;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using IMDb.DataFiles.Types;
using Moq;
using log4net;

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

            var logger = new Mock<ILog>();
            var actual = new ProductionParser(logger.Object).Parse(tvShowString);
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

            var logger = new Mock<ILog>();
            var actual = new ProductionParser(logger.Object).Parse(movieString);
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

            var logger = new Mock<ILog>();
            var actual = new ProductionParser(logger.Object).Parse(videoGameString);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestParseCreatesValidObjectWithNoQuotesAroundProductionTitle()
        {
            var productionDefinition = @"# Guitar Hero 5 (2009) (VG)";
            var expected = new VideoGame
            {
                Title = "Guitar Hero 5",
                Year = 2009
            };

            var logger = new Mock<ILog>();
            var actual = new ProductionParser(logger.Object).Parse(productionDefinition);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestParseCreatesValidTelevisionShowObjectWithNoSeriesOrEpisodeNumbers() {
            var productionDefinition = @"# ""'t Schaep Met De 5 Pooten"" (1969) {Liedjes uit 't Schaep Met 5 Pooten}";
            var expected = new TelevisionShow
            {
                Title = "'t Schaep Met De 5 Pooten",
                Year = 1969,
                EpisodeTitle = "Liedjes uit 't Schaep Met 5 Pooten",
                SeriesNumber = 0,
                EpisodeNumber = 0
            };

            var logger = new Mock<ILog>();
            var actual = new ProductionParser(logger.Object).Parse(productionDefinition);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestParseCreatesValidTelevisionShowObjectWithNoEpisodeTitle() {
            var productionDefinition = @"# ""1 quart de 3"" (2008) {(#1.1)}";

            var expected = new TelevisionShow()
            {
                Title = "1 quart de 3",
                Year = 2008,
                SeriesNumber = 1,
                EpisodeNumber = 1,
                EpisodeTitle = string.Empty
            };

            var logger = new Mock<ILog>();
            var actual = new ProductionParser(logger.Object).Parse(productionDefinition);
            Assert.AreEqual(expected, actual);
        }

    }
}

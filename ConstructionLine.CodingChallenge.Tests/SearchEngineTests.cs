using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEngineTests : SearchEngineTestsBase
    {
        [Test]
        public void ShirtsOnlyTest()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> {Color.Red},
                Sizes = new List<Size> {Size.Small}
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(results.Shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(results.Shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void PerfomanceTestWithMultiColoredSearchOptions()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Small", Size.Small, Color.Black),
                new Shirt(Guid.NewGuid(), "Black - Large", Size.Large, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
                new Shirt(Guid.NewGuid(), "Blue - Medium", Size.Large, Color.Blue),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),

                new Shirt(Guid.NewGuid(), "Yellow - Small", Size.Small, Color.Yellow),
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.Black, Color.Yellow },
                Sizes = new List<Size> { Size.Small, Size.Medium },
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(results.Shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(results.Shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void SearchEngineTestWithNoShirtsAvailable()
        {
            var shirts = new List<Shirt>
            {
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Yellow, Color.Red },
                Sizes = new List<Size> { Size.Small, Size.Large },
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(results.Shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(results.Shirts, searchOptions, results.ColorCounts);
        }
    }
}

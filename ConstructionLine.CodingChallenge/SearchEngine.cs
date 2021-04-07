using System;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;
        }


        public SearchResults Search(SearchOptions options)
        {
            if (options == null || options.Colors == null || options.Sizes == null )
            {
                throw new ArgumentException("Options missing");
            }

            // find the shirts based on the color and size from the options
            var foundShirts = (from shirt in _shirts.ToArray() 
                               where (!options.Colors.Any() || options.Colors.Any(coloredShirt => coloredShirt == shirt.Color) )
                               && (!options.Sizes.Any() || options.Sizes.Any(sizedShirt => sizedShirt == shirt.Size) )
                               select new { Shirt = shirt, shirt.Size, shirt.Color }).ToList();

            //find all the colored counted shirts from foundShirts 
            var colorCountWithoutEmpties = (from foundShirt in foundShirts
                                            group foundShirt by foundShirt.Color
                                            into coloredByGroup
                                            select new ColorCount
                                            { Color = coloredByGroup.Key, Count = coloredByGroup.Count() })
                                            .ToList();
            var sizedCountResultWithoutEmpties = (from foundShirt in foundShirts
                                                  group foundShirt by foundShirt.Size
                                                  into sizedGroup
                                                  select new SizeCount
                                                  { Size = sizedGroup.Key, Count = sizedGroup.Count() })
                                                  .ToList();

            var coloredResult = ColorResultsWithEmpty(colorCountWithoutEmpties);
            var sizedResult = SizedResultsWithEmpty(sizedCountResultWithoutEmpties);

            return new SearchResults
            {
                Shirts = foundShirts.Select(x => x.Shirt).ToList(),
                ColorCounts = coloredResult,
                SizeCounts = sizedResult
            };
        }

        private List<ColorCount> ColorResultsWithEmpty(List<ColorCount> coloredList)
        {
            foreach (var missedColor in Color.All.Where(color => !coloredList.Any(x => x.Color == color)))
            {
                coloredList.Add(new ColorCount { Color = missedColor, Count = 0 });
            }

            return coloredList;
        }

        private List<SizeCount> SizedResultsWithEmpty(List<SizeCount> sizedList)
        {
            foreach(var missedSize in Size.All.Where(size => !sizedList.Any(x=>x.Size == size)))
            {
                sizedList.Add(new SizeCount { Size = missedSize, Count = 0 });
            }

            return sizedList;
        }
    }
}
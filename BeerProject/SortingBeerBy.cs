using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerProject
{
    enum SortBy
    {
        UNIT,
        PERCENT,
        VOLUME
    }

    internal class SortingBeerBy : IComparer<Beer>
    {
        private SortBy sortBy;

        public SortingBeerBy(SortBy sortbyParam)
        {
            this.sortBy = sortbyParam;
        }

        public int Compare(Beer? x, Beer? y)
        {
            switch (sortBy)
            {
                case SortBy.UNIT:
                    return (int)(x.Percent * x.Volume-y.Percent*y.Volume);
                    break;

                case SortBy.PERCENT:
                    return (int)(x.Percent*100 - y.Percent*100);
                    break;

                case SortBy.VOLUME:
                    return x.Volume - y.Volume;
            }
            return 0;
        }
    }
}

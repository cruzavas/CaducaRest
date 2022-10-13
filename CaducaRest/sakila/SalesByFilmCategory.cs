using System;
using System.Collections.Generic;

#nullable disable

namespace CaducaRest.sakila
{
    public partial class SalesByFilmCategory
    {
        public string Category { get; set; }
        public decimal? TotalSales { get; set; }
    }
}

using CommodityService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CommodityService.DAL
{
    public class CommoditiesContext:DbContext
    {
        public CommoditiesContext():
            base("name=CommoditiesContext")
        {

        }
        public DbSet<Commodity> Commodities { get; set; }
    }
}
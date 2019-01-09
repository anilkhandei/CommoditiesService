using CommodityService.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CommodityService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Commodity>("Commodities");
            config.MapODataServiceRoute(
                routeName:"ODataRoute",
                routePrefix:null,
                model:builder.GetEdmModel());
        }
    }
}

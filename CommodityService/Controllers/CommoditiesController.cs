using CommodityService.DAL;
using CommodityService.Models;
using Microsoft.AspNet.OData;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace CommodityService.Controllers
{
    public class CommoditiesController:ODataController
    {
        CommoditiesContext db = new CommoditiesContext();
        private bool ProductExists(int key)
        {
            return db.Commodities.Any(p => p.Id == key);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        [EnableQuery]
        public IQueryable<Commodity> Get()
        {
            return db.Commodities;
        }

        [EnableQuery]
        public SingleResult<Commodity> Get([FromODataUri] int key)
        {
            IQueryable<Commodity> result = db.Commodities.Where(c => c.Id == key);
            return SingleResult.Create(result);  
        }

        public async Task<IHttpActionResult> Post(Commodity commodity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Commodities.Add(commodity);
            await db.SaveChangesAsync();
            return Created(commodity);
        }

        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Commodity> commodity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = await db.Commodities.FindAsync(key);

            if (entity == null)
            {
                return NotFound();
            }

            commodity.Patch(entity);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(key))
                {
                    return NotFound();
                }
                else
                    throw;
            }

            return Updated(entity);  
        }

        public async Task<IHttpActionResult> Put([FromODataUri] int key, Commodity update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(key != update.Id)
            {
                return BadRequest();
            }
            db.Entry(update).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(key))
                {
                    return NotFound();
                }
                else
                    throw;
            }
            return Updated(update);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            var commodity = await db.Commodities.FindAsync(key);
            if (commodity == null)
            {
                return NotFound();
            }
            db.Commodities.Remove(commodity);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Snusbrother.DataAccess;
using Snusbrother.Models.Models;

namespace Snusbrother.DataService.Controllers
{
    public class SnusController : ApiController
    {
        private SnusbrotherDataContext db = new SnusbrotherDataContext();

        // GET: api/Snus
        public IQueryable<Snus> GetSnus()
        {
            return db.Snus;
        }

        // GET: api/Snus/5
        [ResponseType(typeof(Snus))]
        public async Task<IHttpActionResult> GetSnus(int id)
        {
            Snus snus = await db.Snus.FindAsync(id);
            if (snus == null)
            {
                return NotFound();
            }

            return Ok(snus);
        }

        // PUT: api/Snus/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSnus(int id, Snus snus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != snus.SnusId)
            {
                return BadRequest();
            }

            db.Entry(snus).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SnusExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Snus
        [ResponseType(typeof(Snus))]
        public async Task<IHttpActionResult> PostSnus(Snus snus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Snus.Add(snus);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = snus.SnusId }, snus);
        }

        [HttpGet]
        [ResponseType(typeof(Snus[]))]
        [Route("api/Profile/{id}/Snus")]
        public IHttpActionResult GetSnusForProfile(int id)
        {
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
                return NotFound();
            List<Snus> snusForProfiles = new List<Snus>();
            foreach (Snus s in db.Snus)
            {
                if (s.Profile != null && profile.ProfileId == s.Profile.ProfileId)
                    snusForProfiles.Add(s);
            }
            return Ok(snusForProfiles);
        }

        // DELETE: api/Snus/5
        [ResponseType(typeof(Snus))]
        public async Task<IHttpActionResult> DeleteSnus(int id)
        {
            Snus snus = await db.Snus.FindAsync(id);
            if (snus == null)
            {
                return NotFound();
            }

            db.Snus.Remove(snus);
            await db.SaveChangesAsync();

            return Ok(snus);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SnusExists(int id)
        {
            return db.Snus.Count(e => e.SnusId == id) > 0;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using kpz4;
using System.Web.Http.Cors;

namespace kpz4.Controllers
{
     [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class poasController : ApiController
    {
        private RegisterEntities db = new RegisterEntities();

        // GET: api/poas
        public IQueryable<poa> Getpoas()
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.poas;
        }

        // GET: api/poas/5
        [ResponseType(typeof(poa))]
        public IHttpActionResult Getpoa(int id)
        {
            poa poa = db.poas.Find(id);
            if (poa == null)
            {
                return NotFound();
            }

            return Ok(poa);
        }

        // PUT: api/poas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putpoa(int id, poa poa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != poa.idPOA)
            {
                return BadRequest();
            }

            db.Entry(poa).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!poaExists(id))
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

        // POST: api/poas
        [ResponseType(typeof(poa))]
        public IHttpActionResult Postpoa(poa poa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.poas.Add(poa);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = poa.idPOA }, poa);
        }

        // DELETE: api/poas/5
        [ResponseType(typeof(poa))]
        public IHttpActionResult Deletepoa(int id)
        {
            poa poa = db.poas.Find(id);
            if (poa == null)
            {
                return NotFound();
            }

            db.poas.Remove(poa);
            db.SaveChanges();

            return Ok(poa);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool poaExists(int id)
        {
            return db.poas.Count(e => e.idPOA == id) > 0;
        }
    }
}
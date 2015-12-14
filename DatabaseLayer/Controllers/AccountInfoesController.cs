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
using DatabaseLayer.Models;

namespace DatabaseLayer.Controllers
{
    public class AccountInfoesController : ApiController
    {
        private DatabaseLayerContext db = new DatabaseLayerContext();

        // GET: api/AccountInfoes
        public IQueryable<AccountInfo> GetAccountInfoes()
        {
            return db.AccountInfoes;
        }

        // GET: api/AccountInfoes/5
        [ResponseType(typeof(AccountInfo))]
        public AccountInfo GetAccountInfo(string id)
        {
            AccountInfo accountInfo = db.AccountInfoes.Find(id);
            if (accountInfo == null)
            {
                //throw new Exception(String.Format("{0} was not found!", id));
                return null;
            }

            return accountInfo;
        }

        // PUT: api/AccountInfoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAccountInfo(string id, AccountInfo accountInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accountInfo.userid)
            {
                return BadRequest();
            }

            db.Entry(accountInfo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountInfoExists(id))
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

        // POST: api/AccountInfoes
        [ResponseType(typeof(AccountInfo))]
        public IHttpActionResult PostAccountInfo(AccountInfo accountInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AccountInfoes.Add(accountInfo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AccountInfoExists(accountInfo.userid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = accountInfo.userid }, accountInfo);
        }

        // DELETE: api/AccountInfoes/5
        [ResponseType(typeof(AccountInfo))]
        public IHttpActionResult DeleteAccountInfo(string id)
        {
            AccountInfo accountInfo = db.AccountInfoes.Find(id);
            if (accountInfo == null)
            {
                return NotFound();
            }

            db.AccountInfoes.Remove(accountInfo);
            db.SaveChanges();

            return Ok(accountInfo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccountInfoExists(string id)
        {
            return db.AccountInfoes.Count(e => e.userid == id) > 0;
        }
    }
}
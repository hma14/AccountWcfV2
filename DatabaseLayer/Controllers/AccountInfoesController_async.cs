using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DatabaseLayer.Models;
using System.ServiceModel;

namespace DatabaseLayer.Controllers
{
    public class AccountInfoesController_async : ApiController
    {
        private DatabaseLayerContext db = new DatabaseLayerContext();

        // GET: api/AccountInfoes
        public IQueryable<AccountInfo> GetAccountInfoes()
        {
            return db.AccountInfoes;
        }

#if true
        // GET: api/AccountInfoes/5
        public AccountInfo GetAccountInfo(string id)
        {
            AccountInfo accountInfo = db.AccountInfoes.Find(id);
            if (accountInfo == null)
            {
                throw new Exception(String.Format("{0} was not found", id));
            }

            return accountInfo;
        }
#else
        // GET: api/AccountInfoes/5
        //[ResponseType(typeof(AccountInfo))]
        plic async Task<IHttpActionResult> GetAccountInfo(string id)
        {
            AccountInfo accountInfo = await db.AccountInfoes.FindAsync(id).ConfigureAwait(false);
            if (accountInfo == null)
            {
                return NotFound();
            }

            return Ok(accountInfo);
        }
#endif

        // PUT: api/AccountInfoes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAccountInfo(string id, AccountInfo accountInfo)
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
                await db.SaveChangesAsync();
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

#if false
        // POST: api/AccountInfoes
        [ResponseType(typeof(AccountInfo))]
        public async Task<IHttpActionResult> PostAccountInfo(AccountInfo accountInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AccountInfoes.Add(accountInfo);

            try
            {
                await db.SaveChangesAsync();
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
#else
        public AccountInfo PostAccountInfo(AccountInfo accountInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AccountInfoes.Add(accountInfo);

            try
            {
                db.SaveChangesAsync();
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
#endif
        // DELETE: api/AccountInfoes/5
        [ResponseType(typeof(AccountInfo))]
        public async Task<IHttpActionResult> DeleteAccountInfo(string id)
        {
            AccountInfo accountInfo = await db.AccountInfoes.FindAsync(id);
            if (accountInfo == null)
            {
                return NotFound();
            }

            db.AccountInfoes.Remove(accountInfo);
            await db.SaveChangesAsync();

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
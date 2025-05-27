using Dal.Api;
using Dal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Services
{
    public class DalFlightsService : IFlight
    {
        dbcontext db;
        public DalFlightsService(dbcontext db)
        {
            this.db = db;
        }

        #region Create
        public async Task Create(Flight item)
        {

            await db.Flights.AddAsync(item);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("cant saveChanges - Flight" +ex);
            }
        }
#endregion

        #region Delete
        public async Task Delete(int id)
        {
            var fl = (await GetAll()).Find(f => f.Id == id);
            if(fl != null) { 
            db.Remove(fl);
                try
                {
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception("cant saveChanges - Flight" + ex);
                }
            }
        }
#endregion

        #region GetAll
        public async Task<List<Flight>> GetAll() => await db.Flights.Include(c=> c.DestinationNavigation)
                                                                    .Include(a=> a.SourceNavigation).ToListAsync();
        #endregion

        #region GetById
        public async Task<Flight?> GetById(int id) => (await GetAll()).Find(x => x.Id == id);
        #endregion

        #region Update
        public async Task<List<Flight>> Update(Flight item)
        {
            Flight? f = (await GetAll()).Find(x => x.Id == item.Id);
            if (f != null) { 
            f.Id = item.Id;
            f.Source = item.Source;
            f.Destination = item.Destination;
            f.TimeOfFlight = item.TimeOfFlight;
            f.Sold = item.Sold;}
            try
            {
                await db.SaveChangesAsync();
                return await GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("cant saveChanges - Customer" + ex);
            } 
        }
        #endregion
    }

}


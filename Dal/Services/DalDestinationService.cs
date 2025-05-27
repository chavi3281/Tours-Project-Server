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
    public class DalDestinationService : IDestination
    {
        dbcontext dbcontext;
        public DalDestinationService(dbcontext data)
        {
            this.dbcontext = data;
        }

        #region Create
        public async Task<List<Destination>> Create(Destination item)
        {
           await dbcontext.Destinations.AddAsync(item);
            try
            {
                await dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("cant saveChanges - Destination" + ex);
            }
            return await GetAll();
        }
        #endregion

        #region Delete
        public async Task Delete(int destination)
        {
            var d = (await GetAll()).Find(d => d.Id == destination);
            if(d != null) { 
             dbcontext.Remove(d);
                try
                {
                    await dbcontext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception("cant saveChanges - Destination" + ex);
                }
            }
        }
        #endregion

        #region GetAll
        public async Task<List<Destination>> GetAll() => await dbcontext.Destinations.Include(d => d.FlightSourceNavigations)
                                                                                     .Include(d => d.FlightDestinationNavigations).ToListAsync();

        #endregion

        #region GetById
        public async Task<Destination?> GetById(string destination) => (await GetAll()).Find(d => d.Destination1 == destination);

        #endregion

        #region Update
        public async Task<Destination?> Update(Destination item)
        {
            Destination? d = (await GetAll()).Find(d => d.Id == item.Id);
            if(d == null) 
                return null;
            d.Destination1 = item.Destination1;
            d.Path = item.Path;
            try
            {
                await dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("cant saveChanges - Destination" + ex);
            }
            return d;
        }
        #endregion

    }
}

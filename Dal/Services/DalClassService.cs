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
    public class DalClassService : IClasses
    {
        dbcontext dbcontext;
        public DalClassService(dbcontext data)
        {
            this.dbcontext = data;
        }

        #region Create
        public async Task Create(Class item)
        {
            await dbcontext.Classes.AddAsync(item);
            try { 
            await dbcontext.SaveChangesAsync();}
            catch (Exception ex)
            {
                throw new Exception("cant saveChanges - class" + ex);
            }
        }
        #endregion

        #region Delete
        public async Task Delete(string description)
        {
            var cll = (await GetAll()).Find(c => c.Description == description);
            if(cll != null) { 
            dbcontext.Remove(cll);
                try { 
            await dbcontext.SaveChangesAsync();}
                catch (Exception ex)
            {
               throw new Exception("cant saveChanges - class" + ex);
            }

            }
        }
        #endregion

        #region GetAll
        public async Task<List<Class>> GetAll() => await dbcontext.Classes.Include(x => x.ClassToFlights).ToListAsync();
        #endregion

        #region GetById
        public async Task<Class?> GetById(int description)
        {
           return (await GetAll()).Find(c => c.Id == description);
        }
        #endregion

        #region Update
        public async Task<List<Class>> Update(Class item)
        {
            Class? c = (await GetAll()).Find(c => c.Description == item.Description);
            if (c == null)
            {
                throw new Exception($"Class with ID {item.Id} not found");
            }
            c.Id = item.Id;
            c.Description = item.Description;
            try
            {
               await dbcontext.SaveChangesAsync();
                return await GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("cant saveChanges - class" + ex);
            }
        }
        #endregion

    }


}

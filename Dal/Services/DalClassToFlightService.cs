
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Api;
using Dal.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dal.Services
{
    public class DalClassToFlightService : IClassToFlight
    {
        dbcontext dbcontext;
        public DalClassToFlightService(dbcontext data)
        {
            dbcontext = data;
        }

        #region Create
        public async Task Create(ClassToFlight item)
        {
           await dbcontext.ClassToFlights.AddAsync(item);
            try
            {
                await dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("cant saveChanges - classToFlight" + ex);
            }
        }
        #endregion

        #region Delete
        public async Task Delete(int id)
        {
            List<ClassToFlight> fList = (await GetAll()).FindAll(f => f.Thisflight.Id == id);
            if(fList != null) { 
            dbcontext.RemoveRange(fList);
                try
                {
                    await dbcontext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception("cant saveChanges - orderDetails" + ex);
                }
            }
        }
        #endregion

        #region GetAll
        public async Task<List<ClassToFlight>> GetAll()
        {
              return await dbcontext.ClassToFlights.Include(x => x.Class)
                                                   .Include(x => x.Thisflight).ToListAsync();
         }
        #endregion

        #region GetAllSales
        public async Task<List<ClassToFlight>> GetAllSales()
        {  
            return (await GetAll()).FindAll(x => x.Hanacha > 0 && DateTime.Now < x.Thisflight.Date);

        }
        #endregion

        #region GetByClassFlightId
        public async Task<ClassToFlight?> GetByClassFlightId(string classs, int flightId)
        {
            var ctf = await GetAll();
            ClassToFlight? c = ctf.Find(c => c.Class.Description == classs);
            if (c == null) { return c; }
            Class cl = c.Class;
            ClassToFlight? classToFlight =  ctf.Find(f => f.ClassId == cl.Id && f.ThisflightId == flightId && DateTime.Now < f.Thisflight.Date);
            return classToFlight;
        }
        #endregion

        #region Update
        public async Task<List<ClassToFlight>> Update(ClassToFlight item)
        {
            ClassToFlight? f = (await GetAll()).Find(x => x.Id == item.Id);

            if (f == null)
            {
                throw new Exception($"ClassToFlight with ID {item.Id} not found");
            }
            f.ClassId = item.ClassId;
            f.ThisflightId = item.ThisflightId;
            f.NumberOfSeats = item.NumberOfSeats;
            f.Price = item.Price;
            f.WeightLoad = item.WeightLoad;
            f.Hanacha = item.Hanacha;
            f.Sold = item.Sold;
            try
            {
               await dbcontext.SaveChangesAsync();
                return await GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("cant saveChanges - orderDetails" + ex);
            }
        }
        #endregion

    }
}


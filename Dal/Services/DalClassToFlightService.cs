
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Api;
using Dal.Models;

namespace Dal.Services
{
    public class DalClassToFlightService : IClassToFlight
    {
        dbcontext dbcontext;
        public DalClassToFlightService(dbcontext data)
        {
            dbcontext = data;
        }

        public void Create(ClassToFlight item)
        {
            dbcontext.ClassToFlights.Add(item);
            dbcontext.SaveChanges();
        }

        public async Task Delete(int id)
        {
            List<ClassToFlight> fList = GetAll().FindAll(f => f.Thisflight.Id == id);
            if(fList != null) { 
            dbcontext.RemoveRange(fList);
            await dbcontext.SaveChangesAsync();}
        }

        public  List<ClassToFlight> GetAll()
        {
            if(dbcontext.ClassToFlights == null)
            {
                return null;
            }
            else
            {
              var x=  dbcontext.ClassToFlights.Include(x => x.Class).Include(x => x.Thisflight).ToList();
                return x;
            }
        } 

        public List<ClassToFlight> GetAllSales()
        {
            List<ClassToFlight> ctf = GetAll().FindAll(x => x.Hanacha > 0);
            return ctf;
        }


        //no use
        public ClassToFlight? GetByClassFlightId(string classs, int flightId)
        {
 
            ClassToFlight? c = GetAll().Find(c => c.Class.Description == classs);
            if(c == null) { return c; }
            Class cl = c.Class;
           ClassToFlight? classToFlight = GetAll().Find(f => f.ClassId == cl.Id && f.ThisflightId == flightId);
            return classToFlight;
           }


        public List<ClassToFlight> Update(ClassToFlight item)
        {
            ClassToFlight? f = GetAll().Find(x => x.Id == item.Id);

            f.ClassId = item.ClassId;
            f.ThisflightId = item.ThisflightId;
            f.NumberOfSeats = item.NumberOfSeats;
            f.Price = item.Price;
            f.WeightLoad = item.WeightLoad;
            f.Hanacha = item.Hanacha;
            f.Sold = item.Sold;
       
            dbcontext.SaveChanges();
            return GetAll();

        }


    }




}


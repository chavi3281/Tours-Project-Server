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



        public void Create(Class item)
        {
            dbcontext.Classes.Add(item);
            dbcontext.SaveChanges();
        }

        public void Delete(string description)
        {
            List<Class> clist = GetAll();
            dbcontext.Remove(clist.Find(c => c.Description == description));
            dbcontext.SaveChanges();
        }

        public List<Class> GetAll() => dbcontext.Classes.Include(x => x.ClassToFlights).ToList();

        public Class? GetById(int description) => GetAll().Find(c => c.Id == description);



        public Class Update(Class item)
        {
            Class c = GetAll().Find(c => c.Description == item.Description);
            c.Id = item.Id;
            c.Description = item.Description;
            dbcontext.SaveChanges();
            return c;
        }
    }


    }

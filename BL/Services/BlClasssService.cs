using BL.Api;
using BL.Models;
using Dal.Api;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class BlClasssService : IBlClass
    {

        IDal dal;
        public BlClasssService(IDal dal)
        {
            this.dal = dal;
        }

        public Class castingClassFromBlToDal(BlClass c) =>
             new Class()
             {
                 Id = c.Id,
                 Description = c.Description,
             };


        public BlClass castingClassFromDalToBl(Class c) =>
             new BlClass()
             {
                 Id = c.Id,
                 Description = c.Description,
             };


        public List<BlClass> castingListClassFromDalToBl(List<Class> c)
        {
            List<BlClass> blList = new();
            c.ToList().ForEach(cl => blList.Add(castingClassFromDalToBl(cl)));
            return blList;

        }



        public List<BlClass> Create(BlClass item)
        {
            BlClass? clas = GetAll().Find(cl => cl.Id == item.Id);
            if (clas == null)
            {
                Class clss = new()
                {
                    Description = item.Description,
                };

                dal.Classes.Create(clss);
            }
            return GetAll();
        }

        public List<BlClass> Delete(string description)
        {
            dal.Classes.Delete(description);
            return GetAll();
        }

        public List<BlClass> GetAll()
        {
            return castingListClassFromDalToBl(dal.Classes.GetAll());

        }

        public BlClass? GetById(int id)
        {
            Class? c = dal.Classes.GetById(id);
            if(c !=  null)
            return castingClassFromDalToBl(c);
            return null;
        }

        public List<BlClass> Update(BlClass item)
        {
            Class tf = dal.Classes.Update(castingClassFromBlToDal(item));
            return GetAll();
        }
    }
}

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

        #region castingClassFromBlToDal
        public Class castingClassFromBlToDal(BlClass? c) {
            if (c == null)
                throw new ArgumentNullException("null");
        return  new Class()
    {
        Id = c.Id,
        Description = c.Description,
         };}
        #endregion

        #region castingClassFromDalToBl
        public BlClass castingClassFromDalToBl(Class c) =>
           new BlClass()
            {
                Id = c.Id,
                Description = c.Description,
            };

        #endregion

        #region castingListClassFromDalToBl
        public List<BlClass> castingListClassFromDalToBl(List<Class> c)
        {
            List<BlClass> blClasses = new List<BlClass>();
            c.ForEach(item => blClasses.Add(castingClassFromDalToBl(item)));
            return blClasses;
        }
        #endregion

        #region Create
        public async Task<List<BlClass>> Create(BlClass item)
        {
            BlClass? clas = (await GetAll()).Find(cl => cl.Id == item.Id);
            if (clas == null)
            {
                Class clss = new()
                {
                    Description = item.Description,
                };
                await dal.Classes.Create(clss);
            }
            return await GetAll();
        }
        #endregion

        #region Delete
        public async Task<List<BlClass>> Delete(string description)
        {
            await dal.Classes.Delete(description);
            return await GetAll();
        }
        #endregion

        #region GetAll
        public async Task<List<BlClass>> GetAll()
        {
            return  castingListClassFromDalToBl(await dal.Classes.GetAll());
        }
        #endregion

        #region GetById
        public async Task<BlClass?> GetById(int id)
        {
            Class? c = await dal.Classes.GetById(id);
            if (c != null)
                return  castingClassFromDalToBl(c);
            return null;
        }
        #endregion

        #region Update
        public async Task<List<BlClass>> Update(BlClass item)
        {
            List<Class> tf = await dal.Classes.Update(castingClassFromBlToDal(item));
            return castingListClassFromDalToBl(tf);
        }
        #endregion

    }
}
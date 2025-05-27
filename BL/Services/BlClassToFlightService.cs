using BL.Api;
using BL.Models;
using Dal.Api;
using Dal.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class BlClassToFlightService : IBlClassToFlight
    {
        IDal dal;
        IBlClass classs;
        IBlThisFlight thisFlight;
        public BlClassToFlightService(IDal dal, IBlThisFlight thisFlight, IBlClass classs)
        {
            this.dal = dal;
            this.thisFlight = thisFlight;
            this.classs = classs;
        }


        #region Create
        public async Task Create(BlClassToFlight item)
        {
            ClassToFlight f = new ClassToFlight()
            {
                ClassId = item.ClassId,
                ThisflightId = item.ThisflightId,
                NumberOfSeats = item.NumberOfSeats,
                Price = item.Price,
                WeightLoad = item.WeightLoad,
                Hanacha = item.Hanacha,
                Sold = item.Sold,
            };
            await dal.ClassToFlight.Create(f);
        }
        #endregion

        #region GetAll
        public async Task<List<BlClassToFlight>> GetAll()
        {
            var fList = await dal.ClassToFlight.GetAll();
            List<BlClassToFlight> ctf = castingClassToFlightFromDalToBlNormalList(fList);
            return ctf;
        }
        #endregion


        #region castingclassToFlightFromDalToBl
        public BlClassToFlight castingclassToFlightFromDalToBl(ClassToFlight f) => new BlClassToFlight()
        {
            Id = f.Id,
            ClassId = f.ClassId,
            ThisflightId = f.ThisflightId,
            NumberOfSeats = f.NumberOfSeats,
            Price = f.Price,
            WeightLoad = f.WeightLoad,
            Hanacha = f.Hanacha,
            Sold = f.Sold,
            Thisflight = thisFlight.castingOver(f.ThisflightId).Result,
            Class = classs.castingClassFromDalToBl(f.Class),

        };
        #endregion


        #region castingclassToFlightFromBlToDal
        public ClassToFlight castingclassToFlightFromBlToDal(BlClassToFlight f) { 
        return  new ClassToFlight()
        {
            Id = f.Id,
            ClassId = f.ClassId,
            ThisflightId = f.ThisflightId,
            NumberOfSeats = f.NumberOfSeats,
            Price = f.Price,
            WeightLoad = f.WeightLoad,
            Hanacha = f.Hanacha,
            Sold = f.Sold,
            Class =  classs.castingClassFromBlToDal(f.Class),
        };        
        }
        #endregion

        #region updateOrderCount
        public ICollection<BlClassToFlight> castingclassToFlightFromBlToDallist(ICollection<ClassToFlight> f)
        {
            List<BlClassToFlight> bf = new List<BlClassToFlight>();
            f.ToList().ForEach(f => bf.Add(new BlClassToFlight()
            {
                Id = f.Id,
                ClassId = f.ClassId,
                ThisflightId = f.ThisflightId,
                NumberOfSeats = f.NumberOfSeats,
                Price = f.Price,
                WeightLoad = f.WeightLoad,
                Hanacha = f.Hanacha,
                Sold= f.Sold,
                Class = classs.castingClassFromDalToBl(f.Class)

            }));
            return bf;
        }
#endregion


        #region updateOrderCount
        public async Task<BlClassToFlight?> GetByClassFlightId(string classs, int flight)
        {
            ClassToFlight? f = await dal.ClassToFlight.GetByClassFlightId(classs, flight);
            if (f == null) return null;
            return  castingclassToFlightFromDalToBl(f);
        }
        #endregion

        #region updateOrderCount
        public async Task<List<BlClassToFlight>> Update(BlClassToFlight item)
        {
            List<ClassToFlight>? f = await dal.ClassToFlight.Update( castingclassToFlightFromBlToDal(item));
            return castingClassToFlightFromDalToBlNormalList(f);
        }
        #endregion


        #region updateOrderCount
        public async Task<List<BlThisFlight>> Delete(int id)
        {
            await dal.ClassToFlight.Delete(id);
            return await thisFlight.Delete(id);
        }
#endregion


        #region updateOrderCount
        public ICollection<BlClassToFlight>? castingClassToFlightFromDalToBllist(ICollection<ClassToFlight>? f)
        {
            if (f == null)
                return null;
            List<BlClassToFlight> bf = new();
            f.ToList().ForEach(fl => bf.Add(castingclassToFlightFromDalToBl(fl)));
            return bf;
        }
        #endregion

        #region updateOrderCount
        public List<BlClassToFlight> castingClassToFlightFromDalToBlNormalList(List<ClassToFlight>? f)
        {
            if (f == null) throw new ArgumentNullException(nameof(f));
            List<BlClassToFlight> ctf = new();
            f.ForEach(item => ctf.Add(castingclassToFlightFromDalToBl(item)));
            return ctf;
        }
#endregion

        #region updateOrderCount
        public async Task<List<BlClassToFlight>> GetAllSales()
        {
            List<ClassToFlight> fList = await dal.ClassToFlight.GetAllSales();
            List<BlClassToFlight> blctf = castingClassToFlightFromDalToBlNormalList(fList);
            return blctf;
        }
        #endregion


        #region updateOrderCount
        public async Task updateOrderCount(int f, int cnt)
        {
            var ct = await GetAll();
            BlClassToFlight? c = ct.Find(x => x.Id == f);
            if (c != null)
            {
                c.Sold = c.Sold + cnt;
                await Update(c);
            }
        }
        #endregion
    }
}
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

        public void Create(BlClassToFlight item)
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
            dal.ClassToFlight.Create(f);
        }



        public List<BlClassToFlight> GetAll()
        {
            var fList = dal.ClassToFlight.GetAll();
            List<BlClassToFlight> list = new();
            fList.ForEach(f => list.Add(castingclassToFlightFromDalToBl(f)));
            return list;
        }

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
            Thisflight = thisFlight.castingOver(f.ThisflightId),
            Class = classs.castingClassFromDalToBl(f.Class),
            
        };

        public ClassToFlight castingclassToFlightFromBlToDal(BlClassToFlight f) =>
        new ClassToFlight()
        {
            Id = f.Id,
            ClassId = f.ClassId,
            ThisflightId = f.ThisflightId,
            NumberOfSeats = f.NumberOfSeats,
            Price = f.Price,
            WeightLoad = f.WeightLoad,
            Hanacha = f.Hanacha,
            Sold = f.Sold,
            Class = classs.castingClassFromBlToDal(f.Class),
        };

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


        public BlClassToFlight? GetByClassFlightId(string classs, int flight)
        {
            ClassToFlight? f = dal.ClassToFlight.GetByClassFlightId(classs, flight);
            return castingclassToFlightFromDalToBl(f);
        }

        public List<BlClassToFlight> Update(BlClassToFlight item)
        {
            List<ClassToFlight>? f = dal.ClassToFlight.Update(castingclassToFlightFromBlToDal(item));
            return castingClassToFlightFromDalToBlNormalList(f);
        }

        public async Task<List<BlThisFlight>> Delete(int id)
        {
            await dal.ClassToFlight.Delete(id);
            return await thisFlight.Delete(id);
        }

        public ICollection<BlClassToFlight>? castingClassToFlightFromDalToBllist(ICollection<ClassToFlight> f)
        {
            List<BlClassToFlight> bf = new List<BlClassToFlight>();
            f.ToList().ForEach(f => bf.Add(castingclassToFlightFromDalToBl(f)));
            return bf;
        }

        public List<BlClassToFlight>? castingClassToFlightFromDalToBlNormalList(List<ClassToFlight>? f)
        {
            List<BlClassToFlight> bf = new List<BlClassToFlight>();
            f.ForEach(f => bf.Add(castingclassToFlightFromDalToBl(f)));
            return bf;
        }

        public List<BlClassToFlight> GetAllSales()
        {
            List<ClassToFlight> fList = dal.ClassToFlight.GetAllSales();
            List<BlClassToFlight> list = new();
            fList.ForEach(f => list.Add(castingclassToFlightFromDalToBl(f)));
            return list;
        }

        public void updateOrderCount(int f, int cnt)
        {
            BlClassToFlight? c = GetAll().Find(x => x.Id == f);
            if (c != null) { 
           c.Sold = c.Sold + cnt;
            Update(c);}
        }
    }
}

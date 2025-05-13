using BL.Api;
using BL.Models;
using Dal.Api;
using Dal.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    internal class BlDestinationService : IBlDestination
    {
        IDal dal;
        public BlDestinationService(IDal dal)
        {
            this.dal = dal;
        }

        public List<BlDestination> Create(BlDestination item)
        {


            BlDestination? de =  GetAll().Find(d => d.Destination == item.Destination);
            if(de == null) { 

            Destination d = new()
            {
                Destination1 = item.Destination,
                Path = item.Path,
            };
            //שולח לשרת ליצור את האוביקט החדש
            List<Destination> des= dal.Destination.Create(d);
            return castingDestinationListFromDalToBl(des);
            }
            return GetAll();
        }

        public void Create(List<BlDestination> item)
        {


            foreach (var item1 in item)
            {
                Destination d = new()
                {
                    Destination1 = item1.Destination,
                    Path = item1.Path,
                };
                dal.Destination.Create(d);
            }
        }

        public Destination castingDestinationFromBlToDal(BlDestination item) => 
            new Destination()
            {
                Id = item.Id,
                Destination1 = item.Destination,
                Path = item.Path,
            };

        public List<BlDestination> castingDestinationListFromDalToBl(List<Destination> item)
        {
            List<BlDestination> des = new();
            item.ForEach(d => des.Add(castingDestinationFromDalToBl(d)));
            return des;
        }
   



        public BlDestination castOver(int id)
        {
            BlDestination d = GetAll().Find(x => x.Id == id);
            return d;
        }

        public void Delete(int destination)
        {
            dal.Destination.Delete(destination);
        }

        public List<BlDestination> GetAll()
        {
            var d = dal.Destination.GetAll();
            List<BlDestination> list = new();
            d.ForEach(d => list.Add(castingDestinationFromDalToBl(d)));
            return list;
        }

        public BlDestination? GetById(string destination)
        {
            Destination d = dal.Destination.GetById(destination);
            return castingDestinationFromDalToBl(d);
        }

        public List<BlDestination> Update(BlDestination item)
        {
            Destination d = dal.Destination.Update(castingDestinationFromBlToDal(item));
            return GetAll();
        }

        public BlDestination castingDestinationFromDalToBl(Destination d) => new BlDestination()
        {
            Id = d.Id,
            Destination = d.Destination1,
            Path = d.Path,
            
        };


    }
}

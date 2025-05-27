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

        #region Create
        public async Task<List<BlDestination>> Create(BlDestination item)
        {
            var allDestinations = await GetAll();
            BlDestination? de = allDestinations.Find(d => d.Destination == item.Destination);
            if (de == null)
            {
                Destination d = new()
                {
                    Destination1 = item.Destination,
                    Path = item.Path,
                };
                //שולח לשרת ליצור את האוביקט החדש
                List<Destination> des = await dal.Destination.Create(d);
                return  await castingDestinationListFromDalToBl(des);
            }
            return allDestinations;
        }
        #endregion

        #region Create
        public async Task Create(List<BlDestination> item)
        {
            foreach (var item1 in item)
            {
                Destination d = new()
                {
                    Destination1 = item1.Destination,
                    Path = item1.Path,
                };
                await dal.Destination.Create(d);
            }
        }
        #endregion

        #region castingDestinationFromBlToDal
        public Task<Destination> castingDestinationFromBlToDal(BlDestination item) =>
            Task.FromResult(new Destination()
            {
                Id = item.Id,
                Destination1 = item.Destination,
                Path = item.Path,
            });
        #endregion

        #region castingDestinationListFromDalToBl
        public async Task<List<BlDestination>> castingDestinationListFromDalToBl(List<Destination> item)
        {
            var tasks = item.Select(d => castingDestinationFromDalToBl(d));
            var results = await Task.WhenAll(tasks);
            return results.ToList();
        }
        #endregion

        #region castOver
        public async Task<BlDestination?> castOver(int id) => (await GetAll()).Find(x => x.Id == id);
        #endregion

        #region Delete
        public async Task Delete(int destination)
        {
            await dal.Destination.Delete(destination);
        }
#endregion

        #region GetAll
        public async Task<List<BlDestination>> GetAll()
        {
            var d = await dal.Destination.GetAll();
            List<BlDestination> list = new();
            d.ForEach(async d => list.Add(await castingDestinationFromDalToBl(d)));
            return list;
        }
        #endregion

        #region GetById
        public async Task<BlDestination?> GetById(string destination)
        {
            Destination? d = await dal.Destination.GetById(destination);
            if(d == null)
                return null;
            return await castingDestinationFromDalToBl(d);
        }
        #endregion

        #region Update
        public async Task<List<BlDestination>> Update(BlDestination item)
        {
            Destination? d = await dal.Destination.Update(await castingDestinationFromBlToDal(item));
            return await GetAll();
        }
        #endregion

        #region castingDestinationFromDalToBl
        public Task<BlDestination> castingDestinationFromDalToBl(Destination d) =>
            Task.FromResult(new BlDestination()
            {
                Id = d.Id,
                Destination = d.Destination1,
                Path = d.Path,
            });
        #endregion
    }
}
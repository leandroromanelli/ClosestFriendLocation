using AutoMapper;
using ClosestFriendLocation.Api.Controllers;
using ClosestFriendLocation.Api.Models;
using ClosestFriendLocation.Domain.Entities;
using ClosestFriendLocation.Domain.Interfaces.Services;
using ClosestFriendLocation.Domain.Repositories;
using ClosestFriendLocation.Domain.Services;
using ClosestFriendLocation.Infra.Context;
using ClosestFriendLocation.Infra.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ClosestFriendLocation.Test
{
    [TestClass]
    public class ServiceTest
    {
        private IFriendRepository _friendRepository;
        private IFriendService _locationService;
        private IMapper _mapper;

        private ClosestFriendLocationContext _closestFriendLocationContext;

        public ServiceTest()
        {
            var opb = new DbContextOptionsBuilder();
            opb.UseInMemoryDatabase("ClosestFriendLocation");

            _closestFriendLocationContext = new ClosestFriendLocationContext(opb.Options);

            _friendRepository = new FriendRepository(_closestFriendLocationContext);

            _locationService = new FriendService(_friendRepository);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddressModel, Address>();
                cfg.CreateMap<FriendModel, Friend>();
                cfg.CreateMap<LocationModel, Location>();

                cfg.CreateMap<Address, AddressModel>();
                cfg.CreateMap<Friend, FriendModel>();
                cfg.CreateMap<Location, LocationModel>();
            });

            _mapper = config.CreateMapper();
        }

        [TestMethod]
        public void FindClosestFriendsTest()
        {
            var retorno = new FriendController(_mapper, _friendRepository, _locationService)
                .FindClosestFriends(new LocationModel() { Latitude = 0.0, Longitude = 0.0 });

            Assert.IsInstanceOfType(retorno.Result, typeof(OkObjectResult));

        }

        [TestMethod]
        public void Populate()
        {
            try
            {
                var friends = new List<Friend>();
                for (int i = 0; i < new Random().Next(10, 20); i++)
                {
                    friends.Add(new Friend()
                    {
                        Name = string.Format("Amigo_{0}", i),
                        Address = new Address()
                        {
                            Street = string.Format("Rua {0}", i),
                            Number = i.ToString(),
                            Location = new Location()
                            {
                                Latitude = ((float)new Random().NextDouble() * 10.0),
                                Longitude = ((float)new Random().NextDouble() * 10.0)
                            }
                        }
                    });
                }

                foreach (var f in friends)
                {
                    _friendRepository.Add(f);
                }

                Assert.IsTrue(true);
            }
            catch  (Exception ex)
            {
                Assert.Fail();
            }
        }
    }
}

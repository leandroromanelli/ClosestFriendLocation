using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using Infra.Context;
using Infra.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    [TestClass]
    public class ServiceTest
    {
        private IFriendRepository _friendRepository;
        private ClosestFriendLocationContext _closestFriendLocationContext;
        private string _connectionString;
        private LocationService _locationService;

        public ServiceTest()
        {
            var opb = new DbContextOptionsBuilder();
            _connectionString = @"Server=localhost\sqlexpress;Database=ClosestFriendLocation;User Id=sa;Password=123456;";
            opb.UseSqlServer(_connectionString);

            _closestFriendLocationContext = new ClosestFriendLocationContext(opb.Options);

            _friendRepository = new FriendRepository(_closestFriendLocationContext);

            _locationService = new LocationService(_friendRepository);
        }

        [TestMethod]
        public void FindClosestFriendsTest()
        {
            var retorno = new FriendController(_friendRepository, _locationService)
                .FindClosestFriends(new Location() { Latitude = 0.0, Longitude = 0.0 });

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

using ClosestFriendLocation.Domain.Entities;
using ClosestFriendLocation.Domain.Interfaces.Services;
using ClosestFriendLocation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClosestFriendLocation.Domain.Services
{
    public class FriendService: Service<Friend>, IFriendService
    {
        private IFriendRepository _repository;

        public FriendService(IFriendRepository repository) : base (repository)
        {
            _repository = repository;
        }

        public List<Friend> GetClosestFriends(Location location)
        {
            var result = new List<Friend>();

            foreach (var friend in _repository.GetComplete()
                .OrderBy(x => getDistance(location, x.Address.Location)))
            {
                result.Add(friend);

                if (result.Count >= 3)
                    break;
            }

            return result;
        }

        private double getDistance(Location baseLocation, Location destinyLocation)
        {
            return Math.Sqrt(
                Math.Pow((baseLocation.Latitude - destinyLocation.Latitude), 2) +
                Math.Pow((baseLocation.Longitude - destinyLocation.Longitude), 2)
                );
        }

    }
}

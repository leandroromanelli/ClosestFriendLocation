using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Services
{
    public class LocationService: IDisposable
    {
        private IFriendRepository _friendRepository;

        public LocationService(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        public void Dispose()
        {
        }

        public IEnumerable<Friend> GetClosestFriends(Location location)
        {
            var result = new List<Friend>();

            foreach (var friend in _friendRepository.GetComplete()
                .OrderBy(x => getDistance(location, x.Address.Location)))
            {
                result.Add(friend);

                if (result.Count >= 3)
                    break;
            }

            return result;
        }

        public void DeleteFriend(Friend friend)
        {
            _friendRepository.Delete(friend);
        }

        public void AddFriend(Friend friend)
        {
            if (validateFriend(friend))
                _friendRepository.Add(friend);
            else
                throw new ArgumentException();
        }

        public void UpdateFriend(Friend friend)
        {
            if (validateFriend(friend))
                _friendRepository.Update(friend);
            else
                throw new ArgumentException();
        }

        private bool validateFriend(Friend friend)
        {
            if (string.IsNullOrWhiteSpace(friend.Name))
                return false;

            if (friend.Address == null)
                return false;

            if (!validateAddress(friend.Address))
                return false;

            if (!validateLocation(friend.Address.Location))
                return false;

            return true;
        }
        
        private bool validateAddress(Address address)
        {
            if (string.IsNullOrWhiteSpace(address.Street))
                return false;

            if (string.IsNullOrWhiteSpace(address.Number))
                return false;

            if (address.Location == null)
                return false;

            return true;
        }

        private bool validateLocation(Location location)
        {
            if (location.Latitude < 0)
                return false;

            if (location.Longitude < 0)
                return false;

            return true;
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

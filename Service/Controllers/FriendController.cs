using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers
{
    [Produces("application/json")]
    [Route("api/Friend")]
    public class FriendController : Controller
    {
        private IFriendRepository _friendRepository;
        private LocationService _locationService;

        public FriendController(IFriendRepository friendRepository, LocationService locationService)
        {
            _friendRepository = friendRepository;
            _locationService = locationService;
        }


        [HttpPost]
        [ActionName("FindClosestFriends")]
        public IActionResult FindClosestFriends([FromBody]Location location)
        {
            try
            {
                return Ok(_locationService.GetClosestFriends(location));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost]
        [ActionName("AddFriend")]
        public IActionResult AddFriend([FromBody]Friend friend)
        {
            try
            {
                _locationService.AddFriend(friend);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost]
        [ActionName("DeleteFriend")]
        public IActionResult DeleteFriend([FromBody]Friend friend)
        {
            try
            {
                _locationService.DeleteFriend(friend);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost]
        [ActionName("UpdateFriend")]
        public IActionResult UpdateFriend([FromBody]Friend friend)
        {
            try
            {
                _locationService.UpdateFriend(friend);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Friend")]
    //[Authorize]
    public class FriendController : Controller
    {
        private IFriendRepository _friendRepository;
        private LocationService _locationService;

        public FriendController(IFriendRepository friendRepository, LocationService locationService)
        {
            _friendRepository = friendRepository;
            _locationService = locationService;
        }

        [HttpGet("")]
        [ActionName("")]
        public ActionResult<IEnumerable<Friend>> Get()
        {
            try
            {
                return Ok(_friendRepository.GetComplete());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        [HttpPost("FindClosestFriends")]
        [ActionName("FindClosestFriends")]
        public ActionResult<IEnumerable<Friend>> FindClosestFriends([FromBody] Location location)
        {
            try
            {
                return Ok(_locationService.GetClosestFriends(location));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost("AddFriend")]
        [ActionName("AddFriend")]
        public ActionResult AddFriend([FromBody] Friend friend)
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

        [HttpPost("DeleteFriend")]
        [ActionName("DeleteFriend")]
        public ActionResult DeleteFriend([FromBody] Friend friend)
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

        [HttpPost("UpdateFriend")]
        [ActionName("UpdateFriend")]
        public ActionResult UpdateFriend([FromBody] Friend friend)
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

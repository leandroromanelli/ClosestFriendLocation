using AutoMapper;
using ClosestFriendLocation.Api.Models;
using ClosestFriendLocation.Domain.Entities;
using ClosestFriendLocation.Domain.Interfaces.Services;
using ClosestFriendLocation.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ClosestFriendLocation.Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/friend")]
    public class FriendController : Controller
    {
        private IFriendRepository _friendRepository;
        private IFriendService _locationService;
        private IMapper _mapper;

        public FriendController(IMapper mapper,
                                IFriendRepository friendRepository, 
                                IFriendService locationService)
        {
            _mapper = mapper;
            _friendRepository = friendRepository;
            _locationService = locationService;
        }

        [HttpGet]
        public ActionResult<List<FriendModel>> Get()
        {
            try
            {
                return Ok(_mapper.Map<List<FriendModel>>(_friendRepository.GetComplete()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        [HttpPost("locate")]
        public ActionResult<List<FriendModel>> FindClosestFriends([FromBody] LocationModel location)
        {
            try
            {
                return Ok(_mapper.Map<List<FriendModel>>(_locationService.GetClosestFriends(_mapper.Map<Location>(location))));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost]
        public ActionResult AddFriend([FromBody] FriendModel friend)
        {
            try
            {
                _locationService.Add(_mapper.Map<Friend>(friend));
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete]
        public ActionResult DeleteFriend([FromBody] FriendModel friend)
        {
            try
            {
                _locationService.Delete(_mapper.Map<Friend>(friend));
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut]
        public ActionResult UpdateFriend([FromBody] FriendModel friend)
        {
            try
            {
                _locationService.Update(_mapper.Map<Friend>(friend));
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}

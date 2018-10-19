using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Recycling.Data.Abstract;
using UserHubEntity = Recycling.Model.Entities.UserHub;
using Recycling.API.Models;
using AutoMapper;


// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Recycling.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserHubsController : Controller
    {
        private readonly IUserHubRepository _userHubRepository;

        public UserHubsController(IUserHubRepository userHubRepository)
        {
            _userHubRepository = userHubRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<UserHubEntity> users = _userHubRepository
                .GetAll()
                .OrderBy(u => u.Id)
                .ToList();

            IEnumerable<UserHub> usersDto = Mapper.Map<IEnumerable<UserHubEntity>, IEnumerable<UserHub>>(users);

            return new OkObjectResult(usersDto);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult Get(int id)
        {
            UserHubEntity userHub = _userHubRepository.GetSingle(u => u.Id == id);

            if (userHub != null)
            {
                UserHub userHubDto = Mapper.Map<UserHubEntity, UserHub>(userHub);
                return new OkObjectResult(userHubDto);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody]UserHubEntity userHub)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserHubEntity newUserHub = new UserHubEntity { User = userHub.User, Hub = userHub.Hub };

            _userHubRepository.Add(newUserHub);
            _userHubRepository.Commit();

            userHub = Mapper.Map<UserHubEntity, UserHubEntity>(newUserHub);

            CreatedAtRouteResult result = CreatedAtRoute("GetUser", new { controller = "UserHubs", id = userHub.Id }, userHub);
            return result;
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody]UserHub userHub)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserHubEntity userHubEntity = _userHubRepository.GetSingle(id);

            if (userHubEntity == null)
            {
                return NotFound();
            }
            else
            {
                userHubEntity.UserId = userHub.UserId;
                userHubEntity.HubId = userHub.HubId;
                _userHubRepository.Commit();
            }

            var userDto = Mapper.Map<UserHubEntity, UserHub>(userHubEntity);

            return new OkObjectResult(userDto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            UserHubEntity userHubEntity = _userHubRepository.GetSingle(id);
            
            if (userHubEntity == null)
            {
                return new NotFoundResult();
            }
            else
            {
                _userHubRepository.Delete(userHubEntity);
            
                _userHubRepository.Commit();
            
                return new NoContentResult();
            }
        }
    }
}

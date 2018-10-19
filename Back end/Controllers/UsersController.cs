using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Recycling.Data.Abstract;
using UserEntity = Recycling.Model.Entities.User;
using Recycling.API.Models;
using AutoMapper;


// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Recycling.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IFractionRepository _fractionRepository;
        private readonly IUserHubRepository _userHubRepository;

        public UsersController(IUserRepository userRepository,
                                IFractionRepository fractionRepository,
                                IUserHubRepository userHubRepository)
        {
            _userRepository = userRepository;
            _fractionRepository = fractionRepository;
            _userHubRepository = userHubRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<UserEntity> users = _userRepository
                .AllIncluding(u => u.TrashDeliveries)
                .OrderBy(u => u.Id)
                .ToList();

            IEnumerable<User> usersDto = Mapper.Map<IEnumerable<UserEntity>, IEnumerable<User>>(users);

            return new OkObjectResult(usersDto);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult Get(int id)
        {
            UserEntity user = _userRepository.GetSingle(u => u.Id == id, u => u.TrashDeliveries);

            if (user != null)
            {
                User userDto = Mapper.Map<UserEntity, User>(user);
                return new OkObjectResult(userDto);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody]UserEntity user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserEntity newUser = new UserEntity { FirstName = user.FirstName, LastName = user.LastName };

            _userRepository.Add(newUser);
            _userRepository.Commit();

            user = Mapper.Map<UserEntity, UserEntity>(newUser);

            CreatedAtRouteResult result = CreatedAtRoute("GetUser", new { controller = "Users", id = user.Id }, user);
            return result;
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody]User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserEntity userDb = _userRepository.GetSingle(id);

            if (userDb == null)
            {
                return NotFound();
            }
            else
            {
                userDb.FirstName = user.FirstName;
                userDb.LastName = user.LastName;
                _userRepository.Commit();
            }

            var userDto = Mapper.Map<UserEntity, User>(userDb);

            return new OkObjectResult(userDto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            UserEntity userDb = _userRepository.GetSingle(id);
            
            if (userDb == null)
            {
                return new NotFoundResult();
            }
            else
            {
                IEnumerable<Model.Entities.Fraction> existingFraction = _fractionRepository.FindBy(uh => uh.User.Id == id);
                if (existingFraction != null)
                {
                    throw new Exception("Cannot delete user because it has fractions tied to it!");
                }
                IEnumerable<Recycling.Model.Entities.UserHub> userHubs = _userHubRepository.FindBy(a => a.UserId == id);
                
                foreach (var userHub in userHubs)
                {
                    _userHubRepository.Delete(userHub);
                }
            
                _userRepository.Delete(userDb);
            
                _userRepository.Commit();
            
                return new NoContentResult();
            }
        }
    }
}

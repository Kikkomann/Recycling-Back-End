using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Recycling.Data.Abstract;
using WasteManagementEntity = Recycling.Model.Entities.WasteManagement;
using Recycling.API.Models;
using AutoMapper;


// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Recycling.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WasteManagementsController : Controller
    {
        private readonly IWasteManagementRepository _wasteManagementRepository;

        public WasteManagementsController(IWasteManagementRepository wasteManagementRepository)
        {
            _wasteManagementRepository = wasteManagementRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<WasteManagementEntity> wasteManagementEntity = _wasteManagementRepository
                .AllIncluding(u => u.Hubs)
                .OrderBy(u => u.Id)
                .ToList();

            IEnumerable<WasteManagement> wasteManagementsDto = Mapper.Map<IEnumerable<WasteManagementEntity>, IEnumerable<WasteManagement>>(wasteManagementEntity);

            return new OkObjectResult(wasteManagementsDto);
        }

        [HttpGet("{id}", Name = "GetWasteManagement")]
        public IActionResult Get(int id)
        {
            WasteManagementEntity wasteManagementEntity = _wasteManagementRepository.GetSingle(u => u.Id == id, u => u.Hubs);

            if (wasteManagementEntity != null)
            {
                WasteManagement wasteManagementDto = Mapper.Map<WasteManagementEntity, WasteManagement>(wasteManagementEntity);
                return new OkObjectResult(wasteManagementDto);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody]WasteManagementEntity wasteMgmt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            WasteManagementEntity newWasteManagement = new WasteManagementEntity { Name = wasteMgmt.Name };

            _wasteManagementRepository.Add(newWasteManagement);
            _wasteManagementRepository.Commit();

            wasteMgmt = Mapper.Map<WasteManagementEntity, WasteManagementEntity>(newWasteManagement);

            CreatedAtRouteResult result = CreatedAtRoute("GetWasteManagement", new { controller = "WasteManagements", id = wasteMgmt.Id }, wasteMgmt);
            return result;
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody]WasteManagement wasteManagement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            WasteManagementEntity wasteManagementEntity = _wasteManagementRepository.GetSingle(id);

            if (wasteManagementEntity == null)
            {
                return NotFound();
            }
            else
            {
                wasteManagementEntity.Name = wasteManagement.Name;
                _wasteManagementRepository.Commit();
            }

            var wasteManagementDto = Mapper.Map<WasteManagementEntity, WasteManagement>(wasteManagementEntity);

            return new OkObjectResult(wasteManagementDto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            WasteManagementEntity wasteManagementEntity = _wasteManagementRepository.GetSingle(id);
            
            if (wasteManagementEntity == null)
            {
                return new NotFoundResult();
            }
            else
            {
                _wasteManagementRepository.Delete(wasteManagementEntity);
            
                _wasteManagementRepository.Commit();
            
                return new NoContentResult();
            }
        }
    }
}

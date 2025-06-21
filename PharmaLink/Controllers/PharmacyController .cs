using AutoMapper;
using BusinessLayer.Interfaces;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using PharmaLink.DTOs;
using PharmaLink.DTOs.Pharmacy;

namespace PharmaLink.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PharmacyController : ControllerBase
    {
        private readonly IPharmacyService _service;
        private readonly IMapper _mapper;

        public PharmacyController(IPharmacyService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PharmacyReadDto>>> GetAll()
        {
            var pharmacies = await _service.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<PharmacyReadDto>>(pharmacies));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PharmacyReadDto>> GetById(int id)
        {
            var pharmacy = await _service.GetByIdAsync(id);
            if (pharmacy == null)
                return NotFound();

            return Ok(_mapper.Map<PharmacyReadDto>(pharmacy));
        }

        [HttpPost]
        public async Task<ActionResult<PharmacyReadDto>> Create(PharmacyCreateDto dto)
        {
            var pharmacy = _mapper.Map<Pharmacy>(dto);
            var created = await _service.CreateAsync(pharmacy);

            var readDto = _mapper.Map<PharmacyReadDto>(created);
            return CreatedAtAction(nameof(GetById), new { id = readDto.Id }, readDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PharmacyReadDto>> Update(int id, PharmacyCreateDto dto)
        {
            var pharmacy = _mapper.Map<Pharmacy>(dto);
            var updated = await _service.UpdateAsync(id, pharmacy);

            if (updated == null)
                return NotFound();

            return Ok(_mapper.Map<PharmacyReadDto>(updated));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult<PharmacyReadDto>> UpdateStatus(int id, PharmacyUpdateStatusDto dto)
        {
            var updated = await _service.UpdateStatusAsync(id, dto.Status, dto.ApprovedUntil);
            if (updated == null)
                return NotFound();

            return Ok(_mapper.Map<PharmacyReadDto>(updated));
        }

        [HttpGet("pending")]
        public async Task<ActionResult<IEnumerable<PharmacyResponseDto>>> GetPending()
        {
            var pending = await _service.GetByStatusAsync(PharmacyStatus.Pending);
            return Ok(_mapper.Map<IEnumerable<PharmacyResponseDto>>(pending));
        }

        [HttpGet("approved")]
        public async Task<ActionResult<IEnumerable<PharmacyResponseDto>>> GetApproved()
        {
            var approved = await _service.GetByStatusAsync(PharmacyStatus.Approved);
            return Ok(_mapper.Map<IEnumerable<PharmacyResponseDto>>(approved));
        }

        [HttpGet("rejected")]
        public async Task<ActionResult<IEnumerable<PharmacyResponseDto>>> GetRejected()
        {
            var rejected = await _service.GetByStatusAsync(PharmacyStatus.Rejected);
            return Ok(_mapper.Map<IEnumerable<PharmacyResponseDto>>(rejected));
        }

    }
}

using AutoMapper;
using BussinesLayer.Interfaces;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using PharmaLink.DTOs.Request;

[Route("api/[controller]")]
[ApiController]
public class RequestController : ControllerBase
{
    private readonly IRequestService _service;
    private readonly IMapper _mapper;

    public RequestController(IRequestService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RequestResponseDto>>> GetAll()
    {
        var requests = await _service.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<RequestResponseDto>>(requests));
    }

    [HttpPost]
    public async Task<ActionResult<RequestResponseDto>> Create(RequestCreateDto dto)
    {
        var request = _mapper.Map<Request>(dto);
        var created = await _service.CreateAsync(request);
        return Ok(_mapper.Map<RequestResponseDto>(created));
    }

    [HttpGet("post/{postId}")]
    public async Task<ActionResult<IEnumerable<RequestResponseDto>>> GetByPostId(int postId)
    {
        var requests = await _service.GetByPostIdAsync(postId);
        return Ok(_mapper.Map<IEnumerable<RequestResponseDto>>(requests));
    }

    [HttpGet("pharmacy/{pharmacyId}")]
    public async Task<ActionResult<IEnumerable<RequestResponseDto>>> GetByPharmacyId(int pharmacyId)
    {
        var requests = await _service.GetByPharmacyIdAsync(pharmacyId);
        return Ok(_mapper.Map<IEnumerable<RequestResponseDto>>(requests));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }

    [HttpPut("{id}/status")]
    public async Task<ActionResult<RequestResponseDto>> UpdateStatus(int id, [FromBody] string status)
    {
        if (!Enum.TryParse<RequestStatus>(status, true, out var newStatus))
            return BadRequest("Invalid status");

        try
        {
            var updated = await _service.UpdateStatusAsync(id, newStatus);
            if (updated == null)
                return NotFound();

            return Ok(_mapper.Map<RequestResponseDto>(updated));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message); // مثلاً: الكمية غير كافية
        }
    }

}

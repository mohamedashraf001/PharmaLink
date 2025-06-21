using AutoMapper;
using BusinessLayer.Interfaces;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using PharmaLink.DTOs.Post;

namespace PharmaLink.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _service;
        private readonly IMapper _mapper;

        public PostController(IPostService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostResponseDto>>> GetAll()
        {
            var posts = await _service.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<PostResponseDto>>(posts));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostResponseDto>> Get(int id)
        {
            var post = await _service.GetByIdAsync(id);
            if (post == null) return NotFound();
            return Ok(_mapper.Map<PostResponseDto>(post));
        }

        [HttpPost]
        public async Task<ActionResult<PostResponseDto>> Create(PostCreateDto dto)
        {
            var post = _mapper.Map<Post>(dto);
            var created = await _service.CreateAsync(post);
            return Ok(_mapper.Map<PostResponseDto>(created));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PostResponseDto>> Update(int id, PostCreateDto dto)
        {
            var post = _mapper.Map<Post>(dto);
            var updated = await _service.UpdateAsync(id, post);
            if (updated == null) return NotFound();
            return Ok(_mapper.Map<PostResponseDto>(updated));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}

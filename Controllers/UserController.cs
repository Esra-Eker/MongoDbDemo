using Microsoft.AspNetCore.Mvc;
using MongoDbDemo.Models;
using MongoDbDemo.Services;

namespace MongoDbDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<List<User>> Get() =>
            await _service.GetAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(string id)
        {
            var user = await _service.GetAsync(id);
            if (user is null) return NotFound();
            return user;
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            await _service.CreateAsync(user);
            return CreatedAtAction(
            nameof(Get),                     // 1. Hangi action'a referans verdiğini söyler (yani "Get")
            new { id = user.Id },      // 2. URL içinde yer alacak route parametresi (örneğin GET/{id})
            user                       // 3. Response body'de dönecek veri
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, User user)
        {
            var existing = await _service.GetAsync(id);
            if (existing is null) return NotFound();
            user.Id = id;
            await _service.UpdateAsync(id, user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _service.GetAsync(id);
            if (existing is null) return NotFound();
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}

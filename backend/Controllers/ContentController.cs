using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RBACAssignment;
using System.Security.Claims;

namespace RbacBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContentController : ControllerBase
    {
        private static readonly List<Content> _content = new()
        {
            new Content { Id = 1, Title = "Article 1", Body = "This is first article", CreatedBy = "system", CreatedDate = DateTime.UtcNow },
            new Content { Id = 2, Title = "Article 2", Body = "Another article", CreatedBy = "system", CreatedDate = DateTime.UtcNow },
            new Content { Id = 3, Title = " Article 3", Body = "This is new article", CreatedBy = "system", CreatedDate = DateTime.UtcNow }
        };

        [HttpGet("viewer")]
        [Authorize]
        public IActionResult GetViewerContent()
        {
            return Ok(_content);
        }
        [HttpPost("editor/create")]
        [Authorize(Roles = "Admin,Editor")]
        public IActionResult CreateContent([FromBody] Content payload)
        {
            if (string.IsNullOrWhiteSpace(payload.Title) || string.IsNullOrWhiteSpace(payload.Body))
                return BadRequest(new { message = "Title and Body are required" });

            payload.Id = _content.Count > 0 ? _content.Max(c => c.Id) + 1 : 1;
            payload.CreatedBy = User.Identity?.Name ?? "unknown"; // get username from JWT
            payload.CreatedDate = DateTime.UtcNow;

            _content.Add(payload);

            return Ok(new { message = "Content created", payload });
        }
        [HttpDelete("admin/delete/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteContent(int id)
        {
            var item = _content.FirstOrDefault(c => c.Id == id);
            if (item == null)
                return NotFound(new { message = $"Content with Id {id} not found" });

            _content.Remove(item);
            return Ok(new { message = $"Content with Id {id} deleted successfully" });
        }

        [HttpPut("editor/edit/{id}")]
        [Authorize(Roles = "Admin,Editor")]
        public IActionResult EditContent(int id, [FromBody] Content payload)
        {
            var item = _content.FirstOrDefault(c => c.Id == id);
            if (item == null)
                return NotFound(new { message = $"Content with Id {id} not found" });

            item.Title = payload.Title;
            item.Body = payload.Body;
            item.CreatedBy = User.Identity?.Name ?? "unknown";
            item.CreatedDate = DateTime.UtcNow;

            return Ok(new { message = $"Content with Id {id} updated successfully" });
        }
    }
}

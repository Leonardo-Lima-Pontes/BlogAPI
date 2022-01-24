using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("v1/categories")]
        public async Task<IActionResult> GetAsync([FromServices] BlogDataContext context)
        {
            var categories = await context.Categories.ToListAsync();
            return Ok(categories);
        }

        [HttpGet]
        [Route("v1/categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id, 
            [FromServices] BlogDataContext context
            )
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category is null) return NotFound("categoria não encontrada");
            return Ok();
        }

        [HttpPost]
        [Route("v1/categories")]
        public async Task<IActionResult> PostAsync([FromBody] Category category, [FromServices] BlogDataContext context)
        {
            var contegory = await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            return Created($"v1/categories/{category.Id}", contegory);
        }

        [HttpPut]
        [Route("v1/categories/{id:int")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] Category model,
            [FromServices] BlogDataContext context)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category is null) return NotFound();

            category.Name = model.Name;
            category.Slug = model.Slug;
            context.Categories.Update(category);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] BlogDataContext context)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category is null) return NotFound("Esta categoria não existe");
            context.Remove(category);
            return Ok();
        }

    }
}

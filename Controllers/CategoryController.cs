using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    [ApiController]
    [Route("")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("v1/categories")]
        public async Task<IActionResult> GetAsync([FromServices] BlogDataContext context)
        {
            try
            {
                var categories = await context.Categories.ToListAsync();
                return Ok(new ResultViewModel<List<Category>>(categories));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("Falha interna no servidor"));
            }
        }

        [HttpGet]
        [Route("v1/categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category is null) return NotFound(new ResultViewModel<Category>("Não foi possível encontrar a categoria"));
                return Ok(new ResultViewModel<Category>(category));
            } 
            catch
            {
                return StatusCode(500, new ResultViewModel<Category>("Houve uma falha interna no servidor"));
            }
        }

        [HttpPost]
        [Route("v1/categories")]
        public async Task<IActionResult> PostAsync([FromBody] ChangeCategoryModel model, [FromServices] BlogDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Category>(ModelState.GetErros()));

            try
            {
                var category = new Category
                {
                    Id = 0,
                    Name = model.Name,
                    Slug = model.Slug.ToLower()
                };

                var newCategory = await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
                return Created($"v1/categories/{category.Id}", newCategory);
            } 
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("Não foi possível gravar esta categoria"));
            } 
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("Falha interna no servidor"));
            }
        }

        [HttpPut]
        [Route("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] ChangeCategoryModel model, [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category is null) return NotFound(new ResultViewModel<List<Category>>("Categoria não encontrada"));

                category.Name = model.Name;
                category.Slug = model.Slug.ToLower();

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Category>(category));
            } 
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("Não foi possível atualizar esta categoria"));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("Falha interna no servidor"));
            }
        }

        [HttpDelete]
        [Route("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category is null) return NotFound(new ResultViewModel<Category>("Categoria não encontrada"));
                context.Remove(category);
                return Ok(new ResultViewModel<Category>(category));
            } 
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("Não foi possível deletar esta categoria, ela não existe"));
            } 
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("Falha interna no servidor"));
            }
        }

    }
}

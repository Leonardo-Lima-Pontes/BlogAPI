using Blog.Data;
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
                return Ok(categories);
            }
            catch(DbUpdateException ex)
            {
                return StatusCode(500, "Não foi possível encontrar as categorias");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro no servidor");
            }
        }

        [HttpGet]
        [Route("v1/categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id, 
            [FromServices] BlogDataContext context
            )
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category is null) return NotFound("categoria não encontrada");
                return Ok(category);
            } 
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Não foi possível encontrar a categoria");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro no servidor");
            }
        }

        [HttpPost]
        [Route("v1/categories")]
        public async Task<IActionResult> PostAsync([FromBody] CreateCategoryModel model, [FromServices] BlogDataContext context)
        {
            try
            {
                var category = new Category
                {
                    Name = model.Name,
                    Slug = model.Slug.ToLower()
                };

                var newCategory = await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
                return Created($"v1/categories/{category.Id}", newCategory);
            } 
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Não foi possível criar a categoria");
            } 
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro no servidor");
            }
        }

        [HttpPut]
        [Route("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] CreateCategoryModel model,
            [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category is null) return NotFound();

                category.Name = model.Name;
                category.Slug = model.Slug.ToLower();

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return Ok();
            } 
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Não foi possível atualizar as categorias");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro no servidor");
            }
        }

        [HttpDelete]
        [Route("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category is null) return NotFound("Esta categoria não existe");
                context.Remove(category);
                return Ok();
            } 
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Não foi possível deletar a categoria");
            } 
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro no servidor");
            }
        }

    }
}

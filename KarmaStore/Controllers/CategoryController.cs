using KarmaStore.DTO;
using KarmaStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarmaStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ShopDbContext _context;
        public CategoryController(ShopDbContext context)
        {
            _context = context;

        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var cate = _context.Category.ToList();
            return Ok(cate);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            var cate = _context.Category.SingleOrDefault(c => c.CategoryID == id);

            if(cate != null)
            {
                return Ok(cate);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult CreateNew(Category_Model model)
        {
            try
            {
                var cate = new DTO_Category
                {
                    Name = model.Name
                };
            _context.Add(cate);
            _context.SaveChanges();
                return Ok(cate);

            }catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateByID(int id, Category_Model model)
        {
            var cate = _context.Category.SingleOrDefault(c => c.CategoryID == id);
            if (cate != null)
            {
                cate.Name = model.Name;
                _context.SaveChanges();
                return Ok(cate);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeteleById(int id)
        {
            try
            {
                var category = _context.Category.SingleOrDefault(c => c.CategoryID == id);
                if(category == null)
                { 
                    return NotFound(); 
                }
                _context.Category.Remove(category);
                _context.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}

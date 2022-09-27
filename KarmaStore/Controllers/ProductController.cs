using DocumentFormat.OpenXml.Wordprocessing;
using KarmaStore.DTO;
using KarmaStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace KarmaStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ShopDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        

        public ProductController(ShopDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
           
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var pro = _context.Products.ToList();
            return Ok(pro);
        }
        
        
        [HttpGet ("{id}")]
        public IActionResult GetById( string id)
        {
            var pro = _context.Products.SingleOrDefault(p => p.ProductID == Guid.Parse(id));
            if (pro != null)
            {
                return Ok(pro);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost("[Action]")]

        public async Task<IActionResult> Create([FromForm] Product_Model model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string uniqueFileName = ProcessUploadFile(model);
                    DTO_Products pro = new DTO_Products
                    {
                        Name = model.Name,
                        Price = model.Price,
                        Description = model.Description,
                        color = model.color,
                        Size = model.Size,
                        Sale = model.Sale,
                        Quantity = model.Quantity,
                        CategoryID = model.CategoryID,
                        Images = uniqueFileName
                    };
                    _context.Add(pro);
                    await _context.SaveChangesAsync();
                    return Ok(pro);
                }
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }


        [HttpPut("{id}")]

        public IActionResult EditProduct(string id, Product_Model model)
        {
            try
            {
                var pro = _context.Products.SingleOrDefault(p => p.ProductID == Guid.Parse(id));
                if (pro == null) 
                { 
                    return NotFound();
                }
                 if(id != pro.ProductID.ToString())
                {
                    return BadRequest();
                }
                //update
                pro.Name = model.Name;
                pro.Price = model.Price;
                pro.Description = model.Description;
                pro.color = model.color;
                pro.Size = model.Size;
                pro.Sale = model.Sale;
                pro.Quantity = model.Quantity;
                pro.CategoryID = model.CategoryID;
                return Ok(pro);
            }
            catch
            {
                return BadRequest();
            }
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UploadImages(string id, [FromForm]UploadImages model)
        //{
        //    Dictionary<string, string> resp = new Dictionary<string, string>();
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {
        //        //getting user from the database
        //        var userobj = _context.Products.SingleOrDefault(p => p.ProductID == Guid.Parse(id));
        //        if (userobj != null)
        //        {
        //            //Get the complete folder path for storing the profile image inside it.  
        //            var path = Path.Combine(Path.Combine(_webHostEnvironment.WebRootPath, "uploads"));

        //            //checking if "images" folder exist or not exist then create it
        //            if ((!Directory.Exists(path)))
        //            {
        //                Directory.CreateDirectory(path);
        //            }
        //            //getting file name and combine with path and save it
        //            string filename = model.Images.FileName;
        //            using (var fileStream = new FileStream(Path.Combine(path, filename), FileMode.Create))
        //            {
        //                await model.Images.CopyToAsync(fileStream);
        //            }
        //            //save folder path 
        //            userobj.Images = "uploads/" + filename;
        //            //userobj.UpdatedAt = DateTime.UtcNow;
        //            await _context.SaveChangesAsync();
        //            //return api with response
        //            resp.Add("status ", "success");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    return Ok(resp);
        //}

        private string ProcessUploadFile(Product_Model model)
        {
            string uniqueFileName = null;
            if (model.Images != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Images.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Images.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
    //[HttpPost("{id}")]

    //public IActionResult UploadImage(string id, UploadImages img)
    //{
    //    string uniqueFileName = ProcessUploadFile(img);
    //    var pro = _context.Products.SingleOrDefault(p => p.ProductID == Guid.Parse(id));
    //    if(pro == null)
    //    { 
    //        return NotFound();
    //    }
    //    if(id != pro.ProductID.ToString())
    //    {
    //        return BadRequest();
    //    }
    //    var product = new DTO_Products
    //    {
    //        Images = uniqueFileName
    //    };
    //    _context.Add(product);
    //    _context.SaveChanges();
    //    return Ok(product);

    //    string directoryPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
    //    foreach (var file in files)
    //    {
    //        string filePath = Path.Combine(directoryPath, file.FileName);
    //        using (var stream = new FileStream(filePath, FileMode.Create))
    //        {
    //            file.CopyTo(stream);
    //        }
    //    }
    //}




   

    //[HttpPut("{id}")]
    //public IActionResult UpdateById(string id, Product_Model model)
    //{
    //    var pro = _context.Products.SingleOrDefault(p => p.ProductID == Guid.Parse(id));
    //    if(pro == null)
    //    { 
    //        return NotFound(); 
    //    }
    //    if(id != pro.ProductID.ToString())
    //    {
    //        return BadRequest();
    //    }

    //   // Update
    //   pro.Name = model.Name;
    //    pro.Price = model.Price;
    //    pro.Description = model.Description;
    //    pro.color = model.Color;
    //    pro.Size = model.Size;
    //    pro.Quantity = model.Quantity;


    //}

}

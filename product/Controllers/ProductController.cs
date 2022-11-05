using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productController : ControllerBase
    {

  
        private readonly DataContext _context;


        public productController(DataContext context)
        {
            _context = context;
        }



        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            return Ok(await _context.Products.ToListAsync());
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {   
            var product = await _context.Products.FindAsync(id);
            if(product == null)
                return NotFound("Product not found!");
            return Ok(product);
        }



        [HttpPost]
        public async Task<ActionResult<List<Product>>> AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync(); 
            

            return Created(new Uri($"{Request.Path}/{product.Id}", UriKind.Relative), product);
            // return StatusCode(StatusCodes.Status201Created, product);
        }




        [HttpPut("{id}")]
        public async Task<ActionResult<List<Product>>> UpdateProduct(int id, Product request)
        {

            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound("Product not found!");

            product.Name = request.Name;    
            product.Category = request.Category;
            product.Description = request.Description;

            await _context.SaveChangesAsync();

            return Ok(product);
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound("Product not found!");

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            return Ok("Deleted successfully");
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productController : ControllerBase
    {

        private static List<Product> products = new List<Product>
            {
                new Product {
                    Id = 1,
                    Name = "IPhone",
                    Category = "Phones",
                    Description = "Iphone. This is madeby Apple."
               }
           };






        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            return Ok(products);
        }






        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {   
            var product = products.Find(x => x.Id == id);
            if(product == null)
                return NotFound("Product not found!");
            return Ok(product);
        }






        [HttpPost]
        public async Task<ActionResult<List<Product>>> AddProduct(Product product)
        {
            products.Add(product);
            return Ok(product);
        }




        [HttpPut("{id}")]
        public async Task<ActionResult<List<Product>>> UpdateProduct(int id, Product request)
        {

            var product = products.Find(x => x.Id == id);
            if (product == null)
                return NotFound("Product not found!");

            product.Name = request.Name;    
            product.Category = request.Category;
            product.Description = request.Description;

            return Ok(product);
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = products.Find(x => x.Id == id);
            if (product == null)
                return NotFound("Product not found!");

            products.Remove(product);
            return Ok(product);
        }

    }
}

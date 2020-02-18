namespace Shop.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Data;
    using Data.Entities;
    using Helper;
    using System.IO;
    using Shop.Web.Models;

    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IUserHelper userHelper;

        //public IRepositoy Repositoy { get; }

        public ProductsController(IProductRepository productRepository, IUserHelper userHelper)
        {
            this.productRepository = productRepository;
            //Repositoy = repositoy;
            this.userHelper = userHelper;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Products.ToListAsync());
            return View(this.productRepository.GetAll());
        }

        // GET: Products/Details/5
        //public async Task<IActionResult> Details(int? id)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var product = await _context.Products
            var product = await this.productRepository.GetByIdAsync(id.Value);
                //.FirstOrDefaultAsync(m => m.id == id);

            if (product == null) 
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [AutoValidateAntiforgeryToken]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(ProductViewModel view)
        {
            if (ModelState.IsValid)
             {
                //    _context.Add(product);
                //    await _context.SaveChangesAsync();
                var path = string.Empty;

                if (view.ImageFile != null && view.ImageFile.Length > 0)
                {
                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\Products", view.ImageFile.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await view.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/images/Products/{view.ImageFile.FileName}";
                }

                var product = this.ToProduct(view, path);
                //TODO:Cambiar por usuario login
                view.User = await this.userHelper.GetUserbyEmailAsync("jcaraballo74@hotmail.com");

                await this.productRepository.CreateAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }

        private Product ToProduct(ProductViewModel view, string path)
        {
            return new Product
            {
                Id = view.Id,
                ImagenUrl = path,
                IsAvailabe = view.IsAvailabe,
                LastPurchase = view.LastPurchase,
                LastSale = view.LastSale,
                Name = view.Name,
                Price = view.Price,
                Stock = view.Stock,
                User = view.User
            };

        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var product = await _context.Products.FindAsync(id);
            var product = await this.productRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {
           
            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(product);
                    //await _context.SaveChangesAsync();
                    //TODO: cAMBIAR UUSAURO
                    product.User = await this.userHelper.GetUserbyEmailAsync("jcaraballo74@hotmail.com");
                    await this.productRepository.UpdateAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await productRepository.ExistsAsync(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await this.productRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await this.productRepository.GetByIdAsync(id);
            await this.productRepository.DeleteAsync(product);
            return RedirectToAction(nameof(Index));
        }


        //private bool ProductExists(int id)
        //{
        //    return _context.Products.Any(e => e.id == id);
        //}
    }
}

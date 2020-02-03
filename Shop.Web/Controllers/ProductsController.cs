namespace Shop.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Shop.Web.Data;
    using Shop.Web.Data.Entities;

    public class ProductsController : Controller
    {
        public IRepositoy Repositoy { get; }

        public ProductsController(IRepositoy repositoy)
        {
            Repositoy = repositoy;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Products.ToListAsync());
            return View(this.Repositoy.GetProducts());
        }

        // GET: Products/Details/5
        //public async Task<IActionResult> Details(int? id)
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var product = await _context.Products
            var product = this.Repositoy.GetProduct(id.Value);
                //.FirstOrDefaultAsync(m => m.id == id);;

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

        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
             {
                //    _context.Add(product);
                //    await _context.SaveChangesAsync();
                this.Repositoy.AddProducts(product);
                await this.Repositoy.SaveAllAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var product = await _context.Products.FindAsync(id);
            var product = this.Repositoy.GetProduct(id.Value);
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
                    this.Repositoy.UpdateProducts(product);
                    await this.Repositoy.SaveAllAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.Repositoy.ProductExists(product.id))
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
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var product = await _context.Products
            //    .FirstOrDefaultAsync(m => m.id == id);
            var product = this.Repositoy.GetProduct(id.Value);
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
            //var product = await _context.Products.FindAsync(id);
            //_context.Products.Remove(product);
            var product = this.Repositoy.GetProduct(id);
            this.Repositoy.RemoveProducts(product);
            await this.Repositoy.SaveAllAsync();
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //private bool ProductExists(int id)
        //{
        //    return _context.Products.Any(e => e.id == id);
        //}
    }
}

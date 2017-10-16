using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lucykids_v2.DAL;
using Lucykids_v2.Models;
using Lucykids_v2.Models.ViewModels;
using PagedList;
using PagedList.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;

namespace Lucykids_v2.Controllers
{
    //[Authorize(Roles ="Admin")]
    public class ProductsController : Controller
    {
        private StoreDbContext _storeDbContext;
        //private IHostingEnvironment _hostingEnvironment;
        //IProductRepository _productRepository;
        //private readonly ICategoryRepository _categoryRepository;
        //private readonly IBrandRepository _brandRepository;
        //private readonly ISizeRepository _sizeRepository;

        public ProductsController(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
           
        }

        //public ProductsController(StoreDbContext storeDbContext
        // //   , IProductRepository productRepository,
        // //   ICategoryRepository categoryRepository,
        // //IBrandRepository brandRepository,
        // //ISizeRepository sizeRepository)
        //{
        //    _storeDbContext = storeDbContext;
        //    //_productRepository = productRepository;
        //    //_categoryRepository = categoryRepository;
        //    //_brandRepository = brandRepository;
        //    //_sizeRepository = sizeRepository;
        //}

        // GET: Products
        [AllowAnonymous]
        public ViewResult Index(string category, string size, string brand, int? page)
        {
            
            ProductListViewModel viewModel = new ProductListViewModel();

            var products = _storeDbContext.Products
             .Include(p => p.Category)
             .Include(p => p.Size)
             .Include(p => p.Brand)
            .Include(p => p.ProductImageMappings)
            .Include(p =>p.ProductImages);

            var productImageMappings = _storeDbContext.ProductImageMappings;

            string currentCategory = string.Empty;
            string currentSize = string.Empty;
            string currentBrand = string.Empty;


            if (string.IsNullOrEmpty(category))
            {
                products = products.OrderBy(p => p.ProductId)
                .Include(p => p.Category)
                .Include(p => p.Size)
                .Include(p => p.Brand)
                .Include(p => p.ProductImageMappings)
                .Include(p => p.ProductImages);

                currentCategory = "all items";
            }
            else
            {
                products = products.Where(p => p.Category.Name == category)
                   .OrderBy(p => p.ProductId)
                   .Include(p => p.Category)
                    .Include(p => p.Size)
                    .Include(p => p.Brand)
                    .Include(p => p.ProductImageMappings)
                    .Include(p => p.ProductImages);

                currentCategory = category;
            }

            if (!string.IsNullOrEmpty(size))
            {
                products = products.Where(p => p.Size.Name == size && p.Category.Name == category)
                    .OrderBy(p => p.ProductId)
                     .Include(p => p.Category)
                 .Include(p => p.Size)
                 .Include(p => p.Brand)
               .Include(p => p.ProductImageMappings)
               .Include(p => p.ProductImages);
               
                currentSize = size;
            }

            if (!string.IsNullOrEmpty(brand))
            {
                products = products.Where(p => p.Brand.Name == brand)
                    .OrderBy(p => p.ProductId)
                     .Include(p => p.Category)
                .Include(p => p.Size)
                .Include(p => p.Brand)
               .Include(p => p.ProductImageMappings)
               .Include(p => p.ProductImages);
            }
            //var productImageMappings = products.Where(p => p.ProductImageMappings) _storeDbContext.ProductImageMappings.Where(pim => pim.ProductId== produ)    ;


            // create variable for productImage Mappings for products, so 2 tables are joined below and only
            //matching product.productId and productImageMapping.productId
            //var groupedImagesByProduct = from pm in productImageMappings
            //                             group pm by pm.ProductId into newGroupedImagesByProduct
            //                             select newGroupedImagesByProduct;



            //var productsWithImages = from p in products
            //                         join m in productImageMappings on p.ProductId equals m.ProductId
            //                         into newMapping from mapping in newMapping.DefaultIfEmpty()
            //                         select new 
            //                         {
            //                             mapping.ProductId,
            //                             mapping.ProductImageId,
            //                             mapping.ImageNumber,

            //                         };
            var productImgs = _storeDbContext.ProductImages;

            int currentPage = (page ?? 1);
            viewModel.Products = products;
            viewModel.CurrentCategory = category;
            viewModel.CurrentSize = size;
            viewModel.CurrentBrand = brand;
            viewModel.ProductImageMappings = productImageMappings;
            viewModel.ProductImages = productImgs;

            return View(viewModel);
        }
    

// GET: Products/Details/5
[AllowAnonymous]

public async Task<IActionResult> Details(int? id)
{
    if (id == null)
    {
        return NotFound();
    }
            var product = await _storeDbContext.Products
                .Include(p => p.ProductImageMappings)
                .Include(p=>p.ProductImages)
                .Include(p => p.Category)
                 .Include(p => p.Size)
                 .Include(p => p.Brand)
                 .SingleOrDefaultAsync(m => m.ProductId == id);

    return View(product);
}


// GET: Products/Create
public ActionResult Create()
{
    ProductViewModel viewModel = new ProductViewModel();
    viewModel.CategoryList = new SelectList(_storeDbContext.Categories, "CategoryId", "Name");
    viewModel.BrandList = new SelectList(_storeDbContext.Brands, "BrandId", "Name");
    viewModel.SizeList = new SelectList(_storeDbContext.Sizes, "SizeId", "Name");
    viewModel.ImageLists = new List<SelectList>();
    for (int i = 0; i < Constants.NumberOfProductImages; i++)
    {
        viewModel.ImageLists.Add(new SelectList(_storeDbContext.ProductImages, "ProductImageId", "FileName"));
    }
    return View(viewModel);
}

// POST: Products/Create
// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
[HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Create(ProductViewModel viewModel)
{
    Product product = new Product();
    product.Name = viewModel.Name;
    product.Description = viewModel.Description;
    product.Price = viewModel.Price;
    product.CategoryId = viewModel.CategoryId;
    product.SizeId = viewModel.SizeId;
    product.BrandId = viewModel.BrandId;
    product.ProductImageMappings = new List<ProductImageMapping>();

    //get a list of selected images without any blanks
    string[] productImages = viewModel.ProductImages.Where(pi => !string.IsNullOrEmpty(pi)).ToArray();
    for (int i = 0; i < productImages.Length; i++)
    {
        product.ProductImageMappings.Add(new ProductImageMapping
        {
            ProductImage = _storeDbContext.ProductImages.Find(int.Parse(productImages[i])),
            ImageNumber = i
        });
    }

    if (ModelState.IsValid)
    {
        _storeDbContext.Products.Add(product);
        _storeDbContext.SaveChanges();
        return RedirectToAction("Index");
    }

    viewModel.CategoryList = new SelectList(_storeDbContext.Categories, "CategoryId", "Name", product.CategoryId);
    viewModel.BrandList = new SelectList(_storeDbContext.Brands, "BrandId", "Name", product.BrandId);
    viewModel.SizeList = new SelectList(_storeDbContext.Sizes, "SizeId", "Name", product.SizeId);
    viewModel.ImageLists = new List<SelectList>();
    for (int i = 0; i < Constants.NumberOfProductImages; i++)
    {
        viewModel.ImageLists.Add(new SelectList(_storeDbContext.ProductImages, "ProductImageId", "FileName"));
            //viewModel.ProductImages[i]));
    }
            // Shows how many files and how many bytes they contain all together
            ViewBag.Message = $"{product.Name} was uploaded successfully!";
            return View(viewModel);
}
//// GET: Products/Create
//public IActionResult Create()
//{
//    ProductViewModel viewModel = new ProductViewModel();
//    viewModel.CategoryList = new SelectList(_storeDbContext.Categories, "CategoryId", "Name");
//    viewModel.BrandList = new SelectList(_storeDbContext.Brands, "BrandId", "Name");
//    viewModel.SizeList = new SelectList(_storeDbContext.Sizes, "SizeId", "Name");

//    ViewData["Brands"] = viewModel.BrandList;
//    ViewData["Categories"] = viewModel.CategoryList;
//    ViewData["Sizes"] = viewModel.SizeList;

//    //ViewData["Brands"] = new SelectList(_context.Brands, "BrandId", "Name");
//    //ViewData["Categories"] = new SelectList(_context.Categories, "CategoryId", "Name");
//    //ViewData["Sizes"] = new SelectList(_context.Sizes, "SizeId", "Name");
//    return View(viewModel);
//}

////POST: Products/Create
////To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//[HttpPost]
//[ValidateAntiForgeryToken]
//// I had to rewrite the Create action method to create new ProductViewModel
////[Bind("ProductId,Name,Price,BrandId,SizeId,CategoryId")]
//public async Task<IActionResult> Create(ProductViewModel viewModel)
//{
//    Product product = new Product();
//    product.Name = viewModel.Name;
//    product.Price = viewModel.Price;
//    product.CategoryId = viewModel.CategoryId;
//    product.BrandId = viewModel.BrandId;
//    product.SizeId = viewModel.SizeId;
//    if (ModelState.IsValid)
//    {
//        _storeDbContext.Products.Add(product);
//        await _storeDbContext.SaveChangesAsync();
//        return RedirectToAction("Index");
//    }
//    viewModel.CategoryList = new SelectList(_storeDbContext.Categories, "CategoryId", "Name", product.CategoryId);
//    viewModel.BrandList = new SelectList(_storeDbContext.Brands, "BrandId", "Name", product.BrandId);
//    viewModel.SizeList = new SelectList(_storeDbContext.Sizes, "SizeId", "Name", product.SizeId);

//    //ViewData["Brands"] = new SelectList(_context.Brands, "BrandId", "Name", product.BrandId);
//    //ViewData["Categories"] = new SelectList(_context.Categories, "CategoryId", "Name", product.CategoryId);
//    //ViewData["Sizes"] = new SelectList(_context.Sizes, "SizeId", "Name", product.SizeId);
//    return View(viewModel);
//}

//// GET: Products/Create
//public async Task<IActionResult> Create([Bind("ProductId,Name,Price,BrandId,SizeId,CategoryId")] Product product)
//{
//    if (ModelState.IsValid)
//    {
//        _storeDbContext.Add(product);
//        await _storeDbContext.SaveChangesAsync();
//        return RedirectToAction("Index");
//    }
//    ViewData["Brands"] = new SelectList(_storeDbContext.Brands, "BrandId", "Name", product.BrandId);
//    ViewData["Categories"] = new SelectList(_storeDbContext.Categories, "CategoryId", "Name", product.CategoryId);
//    ViewData["Sizes"] = new SelectList(_storeDbContext.Sizes, "SizeId", "Name", product.SizeId);
//    return View(product);
//}

// GET: Products/Edit/5
public async Task<IActionResult> Edit(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var product = _storeDbContext.Products.SingleOrDefault(m => m.ProductId == id);
    if (product == null)
    {
        return NotFound();
    }

    ProductViewModel viewModel = new ProductViewModel();
    viewModel.CategoryList = new SelectList(_storeDbContext.Categories, "CategoryId", "Name");
    viewModel.BrandList = new SelectList(_storeDbContext.Brands, "BrandId", "Name");
    viewModel.SizeList = new SelectList(_storeDbContext.Sizes, "SizeId", "Name");
    viewModel.ImageLists = new List<SelectList>();

    foreach (var imageMapping in product.ProductImageMappings.OrderBy(pim => pim.ImageNumber))
    {
        viewModel.ImageLists.Add(new SelectList(_storeDbContext.ProductImages, "ProductImageId", "FileName", imageMapping.ProductImageMappingId));
    }

    for (int i = viewModel.ImageLists.Count; i < Constants.NumberOfProductImages; i++)
    {
        viewModel.ImageLists.Add(new SelectList(_storeDbContext.ProductImages, "ProductImageId", "FileName"));
    }

    viewModel.ProductViewModelId = product.ProductId;
    viewModel.Name = product.Name;
    viewModel.Description = product.Description;
    viewModel.Price = product.Price;

    return View(viewModel);
    //ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "Name", product.BrandId);
    //ViewData["CategoryId"] = new SelectList(_context.Categories, "Name", "CategoryId", product.CategoryId);
    //ViewData["SizeId"] = new SelectList(_context.Sizes, "SizeId", "Name", product.SizeId);
    //return View(product);
}

// POST: Products/Edit/5
// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(ProductViewModel viewModel)
{
    var productToUpdate = await _storeDbContext.Products.Include(p => p.ProductImageMappings).SingleOrDefaultAsync(p => p.ProductId == viewModel.ProductViewModelId);

    if (await TryUpdateModelAsync<Product>(productToUpdate, "", p => p.Name, p => p.Description, p => p.Price, p => p.CategoryId, p => p.SizeId, p => p.BrandId))
    {
        if (productToUpdate.ProductImageMappings == null)
        {
            productToUpdate.ProductImageMappings = new List<ProductImageMapping>();
        }
        //get a list of selected images without any blanks
        string[] productImages = viewModel.ProductImages.Where(pi => !string.IsNullOrEmpty(pi)).ToArray();
        for (int i = 0; i < productImages.Length; i++)
        {
            //get the image currently stored
            var imageMappingToEdit = productToUpdate.ProductImageMappings.Where(pim => pim.ImageNumber == i).FirstOrDefault();
            //find the new image
            var image = _storeDbContext.ProductImages.Find(int.Parse(productImages[i]));
            //if there is nothing stored then we need to add a new mapping
            if (imageMappingToEdit == null)
            {
                //add image to the imagemappings
                productToUpdate.ProductImageMappings.Add(new ProductImageMapping
                {
                    ImageNumber = i,
                    ProductImage = image,
                    ProductImageId = image.ProductImageId
                });
            }
            //else it's not a new file so edit the current mapping
            else
            {
                //if they are not the same
                if (imageMappingToEdit.ProductImageId != int.Parse(productImages[i]))
                {
                    //assign image property of the image mapping
                    imageMappingToEdit.ProductImage = image;
                }
            }
        }
        //delete any other imagemappings that the user did not include in their selections for the product
        for (int i = productImages.Length; i < Constants.NumberOfProductImages; i++)
        {
            var imageMappingToEdit = productToUpdate.ProductImageMappings.Where(pim => pim.ImageNumber == i).FirstOrDefault();
            //if there is something stored in the mapping
            if (imageMappingToEdit != null)
            {
                //delete the record from the mapping table directly. 
                //just calling productToUpdate.ProductImageMappings.Remove(imageMappingToEdit) results in a FK error
                _storeDbContext.ProductImageMappings.Remove(imageMappingToEdit);
            }
        }
        _storeDbContext.SaveChanges();
        return RedirectToAction("Index");
    }
    return View(viewModel);
    //    if (id != product.ProductId)
    //    {
    //        return NotFound();
    //    }

    //    if (ModelState.IsValid)
    //    {
    //        try
    //        {
    //            _context.Update(product);
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!ProductExists(product.ProductId))
    //            {
    //                return NotFound();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }
    //        return RedirectToAction("Index");
    //    }
    //    ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandId", product.BrandId);
    //    ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
    //    ViewData["SizeId"] = new SelectList(_context.Sizes, "SizeId", "Name", product.SizeId);
    //    return View(product);
    //}


}

// GET: Products/Delete/5
public async Task<IActionResult> Delete(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var product = await _storeDbContext.Products
        .Include(p => p.Brand)
        .Include(p => p.Category)
        .Include(p => p.Size)
        .SingleOrDefaultAsync(m => m.ProductId == id);
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
    var product = await _storeDbContext.Products.SingleOrDefaultAsync(m => m.ProductId == id);
    _storeDbContext.Products.Remove(product);
    await _storeDbContext.SaveChangesAsync();
    return RedirectToAction("Index");
}

private bool ProductExists(int id)
{
    return _storeDbContext.Products.Any(e => e.ProductId == id);
}
    }
}

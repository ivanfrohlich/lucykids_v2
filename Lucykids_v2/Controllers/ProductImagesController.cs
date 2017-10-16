
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lucykids_v2.DAL;
using Lucykids_v2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Net.Http.Headers;
using ImageMagick;
namespace Lucykids_v2.Controllers
{
    public class ProductImagesController : Controller
    {
        private readonly StoreDbContext _storeDbContext;
        private IHostingEnvironment _environment;

        public ProductImagesController(StoreDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _storeDbContext = context;
            _environment = hostingEnvironment;
           
        }

        // GET: ProductImages
        public async Task<IActionResult> Index()
        {
            return View(await _storeDbContext.ProductImages.OrderBy(p => p.FileName).ToListAsync());
        }

       
        // GET: ProductImages/Create
        public ActionResult Create() 
        {
            return View();
        }

        // POST: ProductImages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind("ProductImageId,FileName")]
        public async Task<IActionResult> Create(IList<IFormFile> files)       
        {
            long size = files.Sum(f => f.Length);
            // full path to file in temporary location
            var filePath = Path.GetTempFileName();

            // files is a List of IFormFile, these are files to upload
            foreach (var file in files)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition)
                    .FileName
                    .Trim('"');
               
                // String to the full path to the image file name
               var  fileNameOriginal = _environment.WebRootPath + $@"\productImages\{fileName}";

               
                // writes image file to the location of fileNameOriginal by initializing new instance of FileStream class
                using (var stream = new FileStream(fileNameOriginal, FileMode.CreateNew))
                {
                    await file.CopyToAsync(stream);
                }

                // manipulates the image width and height with MAgickImage class, Magick.NET.COre-Q8 added to the project dependencies
                using (var image = new MagickImage(fileNameOriginal))
                {
                    image.Resize(Constants.maxWidth, Constants.maxHeight);
                    image.Strip();
                    image.Write(fileNameOriginal);
                }

                var fileNameThumbnail = _environment.WebRootPath + $@"\productImages\thumbnails\{fileName}";

                using (var image = new MagickImage(fileNameOriginal))
                {
                    image.Resize(Constants.width, Constants.height);
                    image.Strip();
                    image.Write(fileNameThumbnail);
                }
            }

            if (ModelState.IsValid)
            {
                foreach (var file in files)
                {
                    //save each file
                    var imageToAdd = new ProductImage { FileName = file.FileName };
                    _storeDbContext.ProductImages.Add(imageToAdd);
                    _storeDbContext.SaveChanges();
                }
            }
            // Shows how many files and how many bytes they contain all together
            ViewBag.Message = $"{files.Count} file(s)/ {size} bytes uploaded successfully!";           
            return View();
        }

       

        //public async Task<IActionResult> Upload(IFormFileCollection files) // IFormFileCollection files
        //{
        //    var uploads = Path.Combine(_environment.WebRootPath, "productImages");
        //    foreach (var file in files)
        //    {
        //        if (file.Length>0)
        //        {
        //            using (var fileStream= new FileStream(Path.Combine(uploads,file.FileName), FileMode.Create))
        //            {
        //                await file.CopyToAsync(fileStream);
        //            }
        //        }
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        foreach (var file in files)
        //        {
        //            //save file do disk
        //            //SaveFileToDisk(file);

        //            //save each file
        //            var imageToAdd = new ProductImage { FileName = file.FileName };
        //            _context.ProductImages.Add(imageToAdd);
        //            _context.SaveChanges();
        //        }

        //        //    for (int i = 0; i < files.Count; i++)
        //        //    {
        //        //        _context.Add(files[i]);
        //        //    }

        //        //await _context.SaveChangesAsync();
        //        //    return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        //private void SaveFileToDisk(IFormFile file)
        //{
        //    WebImage img = new WebImage(file.InputStream);
        //    if (img.Width > 190)
        //    {
        //        img.Resize(190, img.Height);
        //    }
        //    img.Save(Constants.ProductImagePath + file.FileName);
        //    if (img.Width > 100)
        //    {
        //        img.Resize(100, img.Height);
        //    }
        //    img.Save(Constants.ProductThumbnailPath + file.FileName);
        //}

        // GET: ProductImages/Edit/5
        //    public async Task<IActionResult> Edit(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var productImage = await _context.ProductImages.SingleOrDefaultAsync(m => m.ProductImageId == id);
        //        if (productImage == null)
        //        {
        //            return NotFound();
        //        }
        //        return View(productImage);
        //    }

        //    // POST: ProductImages/Edit/5
        //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Edit(int id, [Bind("ProductImageId,FileName")] ProductImage productImage)
        //    {
        //        if (id != productImage.ProductImageId)
        //        {
        //            return NotFound();
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                _context.Update(productImage);
        //                await _context.SaveChangesAsync();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!ProductImageExists(productImage.ProductImageId))
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }
        //            return RedirectToAction("Index");
        //        }
        //        return View(productImage);
        //    }

        //    // GET: ProductImages/Delete/5
        //    public async Task<IActionResult> Delete(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var productImage = await _context.ProductImages
        //            .SingleOrDefaultAsync(m => m.ProductImageId == id);
        //        if (productImage == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(productImage);
        //    }

        //    // POST: ProductImages/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> DeleteConfirmed(int id)
        //    {
        //        var productImage = await _context.ProductImages.SingleOrDefaultAsync(m => m.ProductImageId == id);
        //        _context.ProductImages.Remove(productImage);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    private bool ProductImageExists(int id)
        //    {
        //        return _context.ProductImages.Any(e => e.ProductImageId == id);
        //    }

        //public void SaveFileToDisk(IFormFile file)
        //{
        //    WebImage img = new WebImage(file.InputStream);
        //    if (img.Width > 190)
        //    {
        //        img.Resize(190, img.Height);
        //    }
        //    img.Save(Constants.ProductImagePath + file.FileName);
        //    if (img.Width > 100)
        //    {
        //        img.Resize(100, img.Height);
        //    }
        //    img.Save(Constants.ProductThumbnailPath + file.FileName);
        //}


    }
}

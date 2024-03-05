using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Models;
using ShopManagement.ViewModel;
using System.Diagnostics;

namespace ShopManagement.Controllers
{
    public class HomeController : Controller
    {
        private IWebHostEnvironment _web;
        private readonly ShopDB _db;

        public HomeController(IWebHostEnvironment web, ShopDB db)
        {
            _web = web;
            _db = db;
        }

        [HttpGet]
        public IActionResult Index(int? id, bool resetCustomerId = false)
        {
            VmShop oShop = new VmShop();
            var oCus = _db.Customers.Where(x => x.CustomerId == id).FirstOrDefault();
            if (oCus != null)
            {
                oShop.CustomerId = oCus.CustomerId;
                oShop.PurchaseDate = oCus.PurchaseDate;
                oShop.CustomerName = oCus.CustomerName;
                oShop.Phone = oCus.Phone;
                oShop.Address = oCus.Address;
                oShop.Gender = oCus.Gender;
                var listProduct = new List<VmShop.VmProduct>();
                var listPro = _db.Products.Where(x => x.CustomerId == id).ToList();
                foreach (var oPro in listPro)
                {
                    var oProduct = new VmShop.VmProduct();
                    oProduct.ProductId = oPro.ProductId;
                    oProduct.ProductName = oPro.ProductName;
                    oProduct.UnitPrice = (int)oPro.UnitPrice;
                    oProduct.Quantity = (int)oPro.Quantity;
                    oProduct.TotalPrice = (int)oPro.TotalPrice;
                    listProduct.Add(oProduct);

                }
                oShop.Products = listProduct;
            }
            oShop = oShop == null ? new VmShop() : oShop;
            ViewData["Counter"] = new SelectList(_db.Counters, "CounterId", "PosName");
            ViewData["ListC"] = _db.Customers.ToList();
            ViewData["ListP"] = _db.Products.ToList();
            return View(oShop);
        }

        [HttpPost]
        public async Task<IActionResult> Index(VmShop vmShop, IFormFile[] img)
        {

            string[] filename = new string[vmShop.ProductName.Length];
            for (var i = 0; i < vmShop.ProductName.Length; i++)
            {
                if (img[i] != null)
                {
                    filename[i] = img[i].FileName;
                    var filepath = Path.Combine(_web.WebRootPath, "Pic", filename[i]);
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        await img[i].CopyToAsync(stream);
                    }
                }
            }

            var oCustomer = _db.Customers.Find(vmShop.CustomerId);
            if (oCustomer == null)
            {
                oCustomer = new Customer();
                oCustomer.PurchaseDate = vmShop.PurchaseDate;
                oCustomer.CustomerName = vmShop.CustomerName;
                oCustomer.Phone = vmShop.Phone;
                oCustomer.Address = vmShop.Address;
                oCustomer.Gender = vmShop.Gender;
                oCustomer.CounterId = vmShop.CounterId;
                _db.Customers.Add(oCustomer);
            }
            else
            {
                oCustomer.PurchaseDate = vmShop.PurchaseDate;
                oCustomer.CustomerName = vmShop.CustomerName;
                oCustomer.Phone = vmShop.Phone;
                oCustomer.Address = vmShop.Address;
                oCustomer.Gender = vmShop.Gender;
                oCustomer.CounterId = vmShop.CounterId;
                var listProductRemove = _db.Products.Where(x => x.CustomerId == vmShop.CustomerId).ToList();
                _db.Products.RemoveRange(listProductRemove);
            }
            _db.SaveChanges();
            var listProduct = new List<Product>();
            if (vmShop.ProductName != null && vmShop.ProductName.Length > 0)
            {
                for (var i = 0; i < vmShop.ProductName.Length; i++)
                {
                    if (!string.IsNullOrEmpty(vmShop.ProductName[i]))
                    {
                        var oProduct = new Product();
                        oProduct.CustomerId = oCustomer.CustomerId;
                        oProduct.ProductName = vmShop.ProductName[i];
                        oProduct.Photo = (filename != null) ? "/Pic/" + filename[i] : oProduct.Photo;
                        oProduct.UnitPrice = vmShop.UnitPrice[i];
                        oProduct.Quantity = vmShop.Quantity[i];
                        oProduct.TotalPrice = vmShop.TotalPrice[i];
                        listProduct.Add(oProduct);

                    }
                }
            }
            ViewData["Counter"] = new SelectList(_db.Counters, "CounterId", "PosName", oCustomer.CounterId);
            _db.Products.AddRange(listProduct);
            _db.SaveChanges();
            return RedirectToAction("Index", new { resetCustomerId = true });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var oCustomer = (from c in _db.Customers
                             where c.CustomerId == id
                             select
            c).FirstOrDefault();
            var oProduct = (from p in _db.Products where p.CustomerId == id select p).ToList();
            if (oProduct != null)
            {
                _db.Customers.Remove(oCustomer);
                _db.Products.RemoveRange(oProduct);
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

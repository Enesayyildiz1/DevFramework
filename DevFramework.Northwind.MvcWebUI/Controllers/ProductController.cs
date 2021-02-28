using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Business.Concrete;
using DevFramework.Northwind.DataAccess.Concrete.EfProductDal;
using DevFramework.Northwind.Entities.Concrete;
using DevFramework.Northwind.MvcWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevFramework.Northwind.MvcWebUI.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        ProductManager _productService = new ProductManager(new ProductDal());
       
        public ActionResult Index()
        {
            var model = new ProductListViewModel
            {
                Products = _productService.GetAll()
            };
            return View(model);
        }
        public string Add()
        {
            var product = new Product()
            {
                CategoryId=1,
                ProductName="Klavye",
                QuantityPerUnit="5",
                UnitPrice=59
            };
            _productService.Add(product);
            return "Added";
        }
        public string AddUpdate()
        {
           _productService.TransactionalOperation( new Product
            {
                CategoryId = 1,
                ProductName = "Bardak",
                QuantityPerUnit = "5",
                UnitPrice = 59
            }, new Product
            {
                CategoryId = 2,
                ProductName = "Klavye",
                QuantityPerUnit = "5",
                UnitPrice = 22
            });
            return "Done";
          

        }
    }
}
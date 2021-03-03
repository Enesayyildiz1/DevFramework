using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Business.ValidationRules.FluentValidation;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevFramework.Core.Aspects.PostSharp.ValidationAspects;
using System.Threading.Tasks;
using DevFramework.Core.Aspects.PostSharp.TransactionAspects;
using DevFramework.Core.Aspects.PostSharp.CacheAspects;
using DevFramework.Core.CrossCuttingConcerns.Caching.Microsoft;
using DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using DevFramework.Core.Aspects.PostSharp.LogAspects;
using DevFramework.Core.Aspects.PostSharp.ExceptionAspects;
using DevFramework.Core.Aspects.PostSharp.PerformanceAspects;
using System.Threading;
using DevFramework.Core.Aspects.PostSharp.AuthorizationAspects;

namespace DevFramework.Northwind.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        [FluentValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(FileLogger))]
        public Product Add(Product product) 
        {
            return _productDal.Add(product);
        }
        [CacheAspect(typeof(MemoryCacheManager),60)]
        [LogAspect(typeof(DatabaseLogger))]
        [LogAspect(typeof(FileLogger))]
        [PerformanceCounterAspect(2)]
        [SecuredOperation(Roles ="Admin,Editor")]
        public List<Product> GetAll()
        { Thread.Sleep(5000);
            return _productDal.GetList();
           
        }

        public Product GetById(int id)
        {
            return _productDal.Get(p => p.ProductID == id);
        }
        [TransactionScopeAspect]
        [ExceptionLogAspect(typeof(DatabaseLogger))]
        [FluentValidationAspect(typeof(ProductValidator))]
        public void TransactionalOperation(Product product1, Product product2)
        {
            _productDal.Add(product1);
            _productDal.Update(product2);
        }

        public Product Update(Product product)
        {
            return _productDal.Update(product);
        }
    }
}

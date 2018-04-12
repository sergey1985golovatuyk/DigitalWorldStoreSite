using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DigitalWorldStore.Domain.Abstract_Repository;
using DigitalWorldStore.Domain.Entities;
using Moq;
using Ninject;
using System.Web.Routing;
using DigitalWorldStore.Domain.Real_Repository;

namespace DigitalWorldStore.WebUI.Infrastructure
{
    public class NinjectControllerFactory: DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }
        protected override IController GetControllerInstance(RequestContext requestContext,
        Type controllerType)
        {
            return controllerType == null
            ? null
            : (IController)ninjectKernel.Get(controllerType);
        }
        private void AddBindings()
        {
            // Initialize new mocking object
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product> {
            new Product { Name = "Sumsung Galaxy S8", Price = 60000 },
            new Product { Name = "HP Envy 360", Price = 70000 },
            new Product { Name = "Iphone X 64 Gb White", Price = 100000 }
            }.AsQueryable());

            // Binding IRepository interface to mocking object
           // ninjectKernel.Bind<IProductRepository>().ToConstant(mock.Object);

            ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();
            ninjectKernel.Bind < ICategoryRepository>().To<EFCategoryRepository>();
            ninjectKernel.Bind<IOrderRepository>().To<EFOrderRepository>();
        }
    }
}
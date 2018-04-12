using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DigitalWorldStore.Domain.Abstract_Repository;

using DigitalWorldStore.Domain.Entities;
using DigitalWorldStore.WebUI.Models;


namespace DigitalWorldStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
       
        private IProductRepository productRepository;
        private ICategoryRepository categoryRepository;
        public int PageSize = 4;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }




        // Method for displaying of products list
            public ViewResult List(string category, int page = 1)
            {

            var categories = new List<Category>(categoryRepository.Categories);
            var products = new List<Product>(productRepository.Products);

            var productsJoined = products.Join(categories.Where(c=>c.CategoryName == category),
                                 p => p.CategoryId,
                                 c => c.CategoryId,                
                                 (p,c) => new Product
                                 {
                                     ProductId = p.ProductId,
                                     CategoryId = p.CategoryId,
                                     Name = p.Name,
                                     Weight = p.Weight,
                                     Price = p.Price,
                                     Description = p.Description,
                                     ImagePath = p.ImagePath

                                 }
                                 );

            ProductListViewModel model = new ProductListViewModel()
            {

                Products = productsJoined                   
                
                            .OrderBy(p => p.ProductId)
                            .Skip((page - 1) * PageSize)
                            .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    //TotalItems = productRepository.Products.Count()
                    TotalItems = category == null ?
                        productRepository.Products.Count() : productsJoined.Count()
                },
                CurrentCategory = category
                


            };       
            return View(model);
            }

        public ViewResult ProductInfo(Product product)
        {

            var productInf = productRepository.Products.Single(p => p.ProductId == product.ProductId);

            return View(productInf);
        }

        }
    }
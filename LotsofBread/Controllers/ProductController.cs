﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LotsofBread.Models;
using LotsofBread.Models.ViewModels;

namespace LotsofBread.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 6; //number of products shown on each page
        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category, int page = 1)
            => View(new ProductsListViewModel
            {
                Products = repository.Products
                            .Where(p => category == null || p.Category == category)
                            .OrderBy(p => p.ProductID)
                            .Skip((page - 1) * PageSize)
                            .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                    repository.Products.Count() :
                    repository.Products.Where(e =>
                        e.Category == category).Count()
                },
                CurrentCategory = category
            });
    }
}

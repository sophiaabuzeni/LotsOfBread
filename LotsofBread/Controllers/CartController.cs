﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LotsofBread.Models;
using LotsofBread.Models.ViewModels;

namespace LotsofBread.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;

        private Cart cart;

        public CartController(IProductRepository repo, Cart cartService) //constructor to set variables
        {
            repository = repo;
            cart = cartService;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productId,
        string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        } 
    }
}
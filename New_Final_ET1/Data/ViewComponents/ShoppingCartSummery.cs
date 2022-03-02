using Microsoft.AspNetCore.Mvc;
using New_Final_ET1.Data.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace New_Final_ET1.Data.ViewComponents
{
    public class ShoppingCartSummery:ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;
        public ShoppingCartSummery(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            var items = _shoppingCart.GetShoppingCartItems();

            return View(items.Count);
        }

    }
}

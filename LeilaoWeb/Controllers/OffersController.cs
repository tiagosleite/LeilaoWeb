using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LeilaoWeb.Models;
using LeilaoWeb.Models.ViewModels;
using LeilaoWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeilaoWeb.Controllers
{
    public class OffersController : Controller
    {
        private readonly OfferService _offerService;
        private readonly PeopleService _peopleService;
        private readonly ProductService _productService;

        public OffersController(PeopleService peopleService, ProductService productService, OfferService offerService)
        {
            _peopleService = peopleService;
            _productService = productService;
            _offerService = offerService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await _offerService.FindAllByProductIdAsync();
                return View(result);
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }
        public async Task<IActionResult> SimpleSearch(string Name = null)
        {
            try
            {
                List<Offer> resultOffer = null;

                if (!string.IsNullOrEmpty(Name))
                {
                    ViewData["name"] = Name;

                    int id = await _peopleService.FindByNameAsync(Name);
                    if (id > -1)
                    {
                        var result = await _offerService.FindAllByPeopleIdAsync(id);
                        return View(result);
                    }
                    else
                    {
                        ViewBag.Message = ("Name not found.");
                        resultOffer = ViewSearch();
                        return View(resultOffer);
                    }
                }
                else
                {
                    var result = await _offerService.FindAllByProductIdAsync();
                    return View(result);
                }
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }


        public async Task<IActionResult> Create()
        {
            try
            {
                var people = await _peopleService.FindAllAsync();
                var product = await _productService.FindAllAsync();
                var viewModel = new OfferFormViewModels { Peoples = people, Products = product };
                return View(viewModel);
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Offer offer)
        {
            try
            {
                OfferFormViewModels result = null;

                if (!ModelState.IsValid)
                {
                    result = await ViewCreate();
                    return View(result);
                }
                //

                Product prod = await _productService.FindByIdAsync(offer.ProductId);
                if (prod.CheckValue(offer.Value) == true)
                {
                    decimal value = await _offerService.FindBigValueByProductIdAsync(offer.ProductId);

                    if (value < offer.Value)
                    {
                        await _offerService.InsertAsync(offer);
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Message = ("It was not possible to offer, below value.");
                        result = await ViewCreate();
                        return View(result);
                    }
                }

                ViewBag.Message = ("It was not possible to offer, below value.");
                result = await ViewCreate();
                return View(result);
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        private async Task<OfferFormViewModels> ViewCreate()
        {
                        var people = await _peopleService.FindAllAsync();
            var product = await _productService.FindAllAsync();
            var viewModel = new OfferFormViewModels { Peoples = people, Products = product };

            return (viewModel);
        }

        private List<Offer> ViewSearch()
        {
            var viewModel = new List<Offer>();
            return viewModel;
        }


        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}
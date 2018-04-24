using SalesAppExample.Entities;
using SalesAppExample.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAppExample.Services.DiscountCalculator;
using SalesAppExample.Models;
using SalesAppExample.Factories;
using AutoMapper;

namespace SalesAppExample.Controllers
{
    public class CalculatorController : Controller
    {

        private readonly SalesAppExampleDbContext _db;
        private readonly IDiscountCalculatorFactory _calculatorFactory;
        public CalculatorController(IDiscountCalculatorFactory calculatorFactory, SalesAppExampleDbContext db)
        {
            _calculatorFactory = calculatorFactory;
            _db = db;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = new CalculatorViewModel();

            //Get customer and discount type lists

            var customers = _db.Customers.ToList();
            model.Customers = new List<CustomerViewModel>();

            Mapper.Map(customers, model.Customers);

            var discountTypes = _db.DiscountTypes.ToList();
            model.DiscountTypes = new List<DiscountTypeViewModel>();

            Mapper.Map(discountTypes, model.DiscountTypes);

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(CalculatorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var pickedDiscount = model.DiscountTypes.FirstOrDefault(d => d.Id == model.PickedDiscountTypeId);
                var discountCalculationModel = new DiscountCalculationModel();
                Mapper.Map(model, discountCalculationModel);

                // The calculcator factory gets the necessary Calculator service implementation based on the provided discount type name
                model.DiscountedSum = _calculatorFactory.Get(pickedDiscount.Name).Calculate(discountCalculationModel);
                ModelState.Clear();
            }

            return View(model);
        }
    }
}
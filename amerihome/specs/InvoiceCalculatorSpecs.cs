﻿using System;
using System.Collections.Generic;

using Autofac;
using Xunit;

namespace specs
{
    using Calculator.Models;
    using Calculator.Repository;
    using Calculator.Services;
    using Recipe = Dictionary<string, decimal>;

    public partial class InvoiceCalculatorSpecs
    {
        public InvoiceCalculatorSpecs() {
            var builder = new ContainerBuilder();

            var repo = new InMemoryIngredientRepository();

            Ingredients.ForEach(i => repo.Add(i));

            builder.RegisterInstance(repo)
                   .As<IIngredientRepository>()
                   .SingleInstance();

            builder.RegisterType<InvoiceCalculator>()
                   .As<IInvoiceCalculator>();

            _container = builder.Build();
        }

        [Theory]
        [InlineData("cup of organic olive oil", 1.92, 20, 3.35)]
        [InlineData("ounce of pasta", 0.31, 100, 2.70)]
        [InlineData("cup of corn", 0.87, 2, 0)]
        [InlineData("clove of organic garlic", 0.67, 3, 0)]
        public void Verify_tax_on_produce_ingredients(string name,
                                                      decimal price,
                                                      decimal quantity,
                                                      decimal expected)
        {
            var invoiceCalculator = _container.Resolve<IInvoiceCalculator>();

            var recipe = new Recipe
            {
                { name, quantity }
            };

            var invoice = invoiceCalculator.CalculateCost(recipe);
            Assert.Equal(expected, invoice.Tax);
        }

        [Theory]
        [InlineData("clove of organic garlic", 0.67, 1, 0.04)]
        [InlineData("cup of organic olive oil", 1.92, 1, 0.10)]
        [InlineData("chicken breast", 2.19, 3, 0)]
        [InlineData("Lemon", 2.03, 4, 0)]
        [InlineData("cup of corn", 0.87, 2, 0)]
        public void Verify_discount_on_organic_ingredients(string name, 
                                                           decimal price,
                                                           decimal quantity,
                                                           decimal expected)
        {
            var invoiceCalculator = _container.Resolve<IInvoiceCalculator>();

            var recipe = new Recipe
            {
                { name, quantity }
            };

            var invoice = invoiceCalculator.CalculateCost(recipe);
            Assert.Equal(expected, invoice.Discount);
        }

        [Fact]
        public void Verify_total_on_recipe_3()
        {
            var invoiceCalculator = _container.Resolve<IInvoiceCalculator>();

            var recipe = new Recipe
            {
                { "clove of organic garlic", 1m },
                { "cup of corn", 4m },
                { "slice of bacon", 4m },
                { "ounce of pasta", 8m },
                { "cup of organic olive oil", 0.3333333m },
                { "teaspoon of salt", 1.25m },
                { "teaspoon of pepper", 0.75m }
            };

            var invoice = invoiceCalculator.CalculateCost(recipe);
            Assert.Equal("8.91", invoice.Total.ToString("#.##"));
        }

        [Fact]
        public void Should_return_invoice_object_when_recipe_is_empty()
        {
            var invoiceCalculator = _container.Resolve<IInvoiceCalculator>();

            Recipe recipe = new Recipe();

            var invoice = invoiceCalculator.CalculateCost(recipe);
            Assert.NotNull(invoice);
            Assert.IsType<Invoice>(invoice);
        }

        [Fact]
        public void Should_throw_exception_when_recipe_is_null()
        {
            var invoiceCalculator = _container.Resolve<IInvoiceCalculator>();
            Recipe recipe = null;

            ArgumentNullException thrownException = 
                Assert.Throws<ArgumentNullException>(
                    () => invoiceCalculator.CalculateCost(recipe));

            Assert.Equal("recipe", thrownException.ParamName);
        }

        readonly IContainer _container;
    }
}

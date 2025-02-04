﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Request.Entities.Interfaces;
using Domain.Stock.ValueObjects;
using Validations.Request;
using Validations.Request.DomainValidation.Item;

namespace Domain.Stock.Entities.Item
{
    public class ItemEntity : Entity<int>, IAgregateRoot
    {
        //Props
        public string Name { get; set; }
        public PriceItem PriceItem { get; set; }
        public QuantityItem QuantityItemStock { get; set; }

        //Ctor
        public ItemEntity() { } //EF

        public ItemEntity(string name, int quantityItemStock, decimal priceItem)
        {
            Validations(name);//Valitadions
            Name = name;
            QuantityItemStock = new QuantityItem(quantityItemStock);
            PriceItem = new PriceItem(priceItem);
        }

        public void StockReduceQuantity(int quantityReduce)
        {
            if (QuantityItemStock.Quantity < quantityReduce)
                StockValidationException.InsufficientStockException(QuantityItemStock.Quantity, quantityReduce);

            int newQuantity = QuantityItemStock.Quantity - quantityReduce;

            QuantityItemStock = new QuantityItem(newQuantity);
        }

        public void Validations(string name)
        {
            DefaultValidationsException.IsNullOrEmpty(name, nameof(name));
        }
    }
}

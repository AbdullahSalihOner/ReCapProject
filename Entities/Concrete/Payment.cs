﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Payment : IEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
        public string ExpirationDate { get; set; }
        public string CVV { get; set; }
    }
}
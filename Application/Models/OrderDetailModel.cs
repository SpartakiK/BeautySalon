﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class OrderDetailModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public ServiceModel Service { get; set; }
        public int Comission { get; set; }
        public decimal Price { get; set; }
        public DateTime CreationDate { get; set; }
    }
}

﻿using Meseum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Meseum.ViewModel
{
    public class InventoryDetails
    {
        public Inventory  Inventory { get; set; }
        public List<Files> Files { get; set; }
    }
}
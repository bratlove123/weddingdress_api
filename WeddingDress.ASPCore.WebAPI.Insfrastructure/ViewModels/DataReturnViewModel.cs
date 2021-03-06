﻿using System;
using System.Collections.Generic;
using System.Text;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Common;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.ViewModels
{
    public class DataReturnViewModel
    {
        public IEnumerable<EntityBase> Data { get; set; }
        public int CountAll { get; set; }
    }
}

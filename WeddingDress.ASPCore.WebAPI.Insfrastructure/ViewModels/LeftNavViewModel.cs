﻿using System;
using System.Collections.Generic;
using System.Text;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.ViewModels
{
    public class LeftNavViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string IconClass { get; set; }
        public bool IsHasBadge { get; set; }
        public string BadgeClass { get; set; }
        public int BadgeNumber { get; set; }
        public ICollection<LeftNavItem> Childs { get; set; }
    }
}

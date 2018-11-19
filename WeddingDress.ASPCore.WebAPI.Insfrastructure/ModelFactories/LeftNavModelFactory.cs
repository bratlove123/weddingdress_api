using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Interfaces.ModelFactories;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.ViewModels;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.ModelFactories
{
    public class LeftNavModelFactory: ILeftNavModelFactory
    {
        public LeftNav CreateLeftNavEntity(LeftNavViewModel leftNavViewModel)
        {
            return Mapper.Map<LeftNavViewModel, LeftNav>(leftNavViewModel);
        }
    }
}

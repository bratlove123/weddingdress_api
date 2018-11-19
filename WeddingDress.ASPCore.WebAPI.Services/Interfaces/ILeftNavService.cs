using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.ViewModels;

namespace WeddingDress.ASPCore.WebAPI.Services.Interfaces
{
    public interface ILeftNavService
    {
        void AddNewLeftNav(LeftNavViewModel leftNavViewModel);
        IEnumerable<LeftNav> GetLeftNavs();
        void DeleteLeftNav(int id);
        void EditLeftNav(LeftNavViewModel leftNav);
        Task<LeftNav> GetLeftNav(int id);
        LeftNavReturnViewModel GetLeftNavsWithPagingAndSorting(int pageSize, int pageNumber, string orderBy, bool sort, string search);
    }
}

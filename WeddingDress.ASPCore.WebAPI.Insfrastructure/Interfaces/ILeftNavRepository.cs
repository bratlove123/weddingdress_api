using System.Collections.Generic;
using System.Threading.Tasks;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.ViewModels;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.Interfaces
{
    public interface ILeftNavRepository
    {
        void AddNewLeftNav(LeftNavViewModel leftNav);
        IEnumerable<LeftNav> GetLeftNavs();
        void DeleteLeftNav(int id);
        void EditLeftNav(LeftNavViewModel leftNav);
        Task<LeftNav> GetLeftNav(int id);
        IEnumerable<LeftNav> GetLeftNavsWithPagingAndSorting(int pageSize, int pageNumber, string orderBy, bool sort, string search);
        int CountWithSearch(string searchStr);
    }
}

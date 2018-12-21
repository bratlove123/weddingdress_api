using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Interfaces;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.ViewModels;
using WeddingDress.ASPCore.WebAPI.Services.Interfaces;

namespace WeddingDress.ASPCore.WebAPI.Services.Services
{
    public class LeftNavService:ILeftNavService
    {
        private ILeftNavRepository _leftNavRepository;

        public LeftNavService(ILeftNavRepository leftNavRepository)
        {
            _leftNavRepository = leftNavRepository;
        }

        public void AddNewLeftNav(LeftNavViewModel leftNavViewModel)
        {
            _leftNavRepository.AddNewLeftNav(leftNavViewModel);
        }

        public void DeleteLeftNav(int id)
        {
            _leftNavRepository.DeleteLeftNav(id);
        }

        public void EditLeftNav(LeftNavViewModel leftNav)
        {
            _leftNavRepository.EditLeftNav(leftNav);
        }

        public Task<LeftNav> GetLeftNav(int id)
        {
            return _leftNavRepository.GetLeftNav(id);
        }

        public IEnumerable<LeftNav> GetLeftNavs()
        {
            return _leftNavRepository.GetLeftNavs();
        }

        public DataReturnViewModel GetLeftNavsWithPagingAndSorting(int pageSize, int pageNumber, string orderBy, bool sort, string search)
        {
            DataReturnViewModel returnModel = new DataReturnViewModel()
            {
                Data = _leftNavRepository.GetLeftNavsWithPagingAndSorting(pageSize, pageNumber, orderBy, sort, search),
                CountAll = _leftNavRepository.CountWithSearch(search)
            };
            return returnModel;
        }
    }
}

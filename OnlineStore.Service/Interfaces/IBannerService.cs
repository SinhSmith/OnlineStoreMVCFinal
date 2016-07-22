using OnlineStore.Model.Context;
using OnlineStore.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Interfaces
{
    public interface IBannerService
    {
        IList<BannerViewModel> GetBannersForHomePage();
        IList<BannerViewModel> GetBanners(int pageNumber, int pageSize, out int totalItems);
        bool AddBanner(BannerViewModel bannerViewModel);
        bool EditBanner(BannerViewModel bannerViewModel);
        bool DeleteBanner(int id);
        BannerViewModel GetBannerById(int? id);
    }
}

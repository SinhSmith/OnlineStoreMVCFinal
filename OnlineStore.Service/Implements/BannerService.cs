using OnlineStore.Model.Context;
using OnlineStore.Model.Repository;
using OnlineStore.Model.ViewModel;
using OnlineStore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Model.Mapper;

namespace OnlineStore.Service.Implements
{
    public class BannerService : IBannerService
    {
        public IList<BannerViewModel> GetBannersForHomePage()
        {
            int month = DateTime.Now.Month;
            int brandType = 1;
            switch (month)
            {
                case 1:
                case 2:
                case 3:
                    brandType = 1;//Spring
                    break;
                case 4:
                case 5:
                case 6:
                    brandType = 2;//Summer
                    break;
                case 7:
                case 8:
                case 9:
                    brandType = 3;//Autumn
                    break;
                case 10:
                case 11:
                case 12:
                    brandType = 4;//Winter
                    break;
            }

            using (var db = new OnlineStoreMVCEntities())
            {
                return db.system_Banners.Where(x => x.Status == (int)OnlineStore.Infractructure.Utility.Define.Status.Active && x.Type == brandType)
                    .Select(x => new BannerViewModel
                    {
                        ImageName = x.share_Images.ImageName,
                        ImagePath = x.share_Images.ImagePath,
                    }).ToList();
            }
        }

        public IList<BannerViewModel> GetBanners(int pageNumber, int pageSize, out int totalItems)
        {
            using (var db = new OnlineStoreMVCEntities())
            {
                totalItems = db.system_Banners.Count(x => x.Status != (int)OnlineStore.Infractructure.Utility.Define.Status.Delete);

                return db.system_Banners.Where(x => x.Status != (int)OnlineStore.Infractructure.Utility.Define.Status.Delete)
                    .OrderBy(x => x.SortOrder).ThenBy(x => x.Id)
                    .Skip(pageSize * (pageNumber - 1)).Take(pageSize)
                    .Select(x => new BannerViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Type = x.Type,
                        Status = x.Status
                    }).ToList();
            }
        }

        public bool AddBanner(BannerViewModel bannerViewModel)
        {
            try
            {
                using (var db = new OnlineStoreMVCEntities())
                {
                    var banner = new system_Banners
                    {
                        ImageId = bannerViewModel.ImageId,
                        Name = bannerViewModel.Name,
                        Type = bannerViewModel.Type,
                        Status = (int)OnlineStore.Infractructure.Utility.Define.Status.Active,
                        SortOrder = bannerViewModel.SortOrder,
                        CreatedDate = DateTime.Now
                    };
                    db.system_Banners.Add(banner);
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EditBanner(BannerViewModel bannerViewModel)
        {
            try
            {
                using (var db = new OnlineStoreMVCEntities())
                {
                    var banner = db.system_Banners.Find(bannerViewModel.Id);
                    banner.ImageId = bannerViewModel.ImageId;
                    banner.Name = bannerViewModel.Name;
                    banner.Type = bannerViewModel.Type;
                    banner.SortOrder = bannerViewModel.SortOrder;
                    banner.Status = bannerViewModel.Status;
                    banner.ModifiedDate = DateTime.Now;
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BannerViewModel GetBannerById(int? bannerId)
        {
            if (bannerId == null)
                return null;

            using (var db = new OnlineStoreMVCEntities())
            {
                var banner = db.system_Banners.Find(bannerId.Value);
                return new BannerViewModel
                {
                    Id = banner.Id,
                    ImageId = banner.ImageId,
                    ImageName = banner.share_Images.ImageName,
                    ImagePath = banner.share_Images.ImagePath,
                    Name = banner.Name,
                    Type = banner.Type,
                    Status = banner.Status,
                    SortOrder = banner.SortOrder
                };
            }
        }

        public bool DeleteBanner(int id)
        {
            try
            {
                using (var db = new OnlineStoreMVCEntities())
                {
                    var banner = db.system_Banners.Find(id);
                    banner.Status = (int)OnlineStore.Infractructure.Utility.Define.Status.Delete;
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

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
    public class ProfileService : IProfileService
    {
        public IList<ProfileViewModel> GetProfiles(int pageNumber, int pageSize, out int totalItems)
        {
            using (var db = new OnlineStoreMVCEntities())
            {
                totalItems = db.system_Profiles.Count(x => x.Status != (int)OnlineStore.Infractructure.Utility.Define.Status.Delete);

                return db.system_Profiles.Where(x => x.Status != (int)OnlineStore.Infractructure.Utility.Define.Status.Delete)
                    .OrderBy(x => x.UserName)
                    .Skip(pageSize * (pageNumber - 1)).Take(pageSize)
                    .Select(x => new ProfileViewModel
                    {
                        UserId = x.UserId,
                        UserName = x.UserName,
                        Emaill = x.Emaill,
                        Password = x.Password,
                        Phone = x.Phone,
                        Address = x.Address,
                        Status = x.Status
                    }).ToList();
            }
        }

        public bool AddProfile(ProfileViewModel profileViewModel)
        {
            try
            {
                using (var db = new OnlineStoreMVCEntities())
                {
                    var profile = new system_Profiles
                    {
                        UserId = profileViewModel.UserId,
                        UserName = profileViewModel.UserName,
                        Emaill = profileViewModel.Emaill,
                        Password = profileViewModel.Password,
                        Phone = profileViewModel.Phone,
                        Address = profileViewModel.Address
                    };
                    db.system_Profiles.Add(profile);
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EditCMSNews(CMSNewsView cmsNewsView)
        {
            try
            {
                using (var db = new OnlineStoreMVCEntities())
                {
                    var news = db.cms_News.Find(cmsNewsView.Id);
                    news.CategoryId = cmsNewsView.CategoryId;
                    news.Title = cmsNewsView.Title;
                    news.SubTitle = cmsNewsView.SubTitle;
                    news.ContentNews = cmsNewsView.ContentNews;
                    news.Authors = cmsNewsView.Authors;
                    news.Tags = cmsNewsView.Tags;
                    news.TotalView = cmsNewsView.TotalView;
                    news.SortOrder = cmsNewsView.SortOrder;
                    news.Status = cmsNewsView.Status;
                    news.ModifiedDate = DateTime.Now;
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CMSNewsView GetCMSNewsById(int? newsId)
        {
            if (newsId == null)
                return null;

            using (var db = new OnlineStoreMVCEntities())
            {
                var news = db.cms_News.Find(newsId.Value);
                return new CMSNewsView
                {
                    Id = news.Id,
                    CategoryId = news.CategoryId,
                    Title = news.Title,
                    SubTitle = news.SubTitle,
                    ContentNews = news.ContentNews,
                    Authors = news.Authors,
                    Tags = news.Tags,
                    TotalView = news.TotalView,
                    Status = news.Status,
                    SortOrder = news.SortOrder
                };
            }
        }

        public bool DeleteProfile(Guid userId)
        {
            try
            {
                using (var db = new OnlineStoreMVCEntities())
                {
                    //Lock
                    var user = db.AspNetUsers.FirstOrDefault(x => x.Id == userId.ToString());
                    user.LockoutEndDateUtc = DateTime.Parse("1/1/9999");

                    var profile = db.system_Profiles.Find(userId);
                    profile.Status = (int)OnlineStore.Infractructure.Utility.Define.Status.Delete;
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

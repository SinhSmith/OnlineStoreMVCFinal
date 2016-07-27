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
    public class CMSNewsService : ICMSNewsService
    {
        public IList<CMSNewsView> GetCMSNews(int pageNumber, int pageSize, out int totalItems)
        {
            using (var db = new OnlineStoreMVCEntities())
            {
                totalItems = db.cms_News.Count(x => x.Status != (int)OnlineStore.Infractructure.Utility.Define.Status.Delete);

                return db.cms_News.Where(x => x.Status != (int)OnlineStore.Infractructure.Utility.Define.Status.Delete)
                    .OrderByDescending(x => x.SortOrder).ThenByDescending(x => x.CreatedDate)
                    .Skip(pageSize * (pageNumber - 1)).Take(pageSize)
                    .Select(x => new CMSNewsView
                    {
                        Id = x.Id,
                        CategoryId = x.CategoryId,
                        CategoryTitle = x.cms_Categories.Title,
                        Title = x.Title,
                        SubTitle = x.SubTitle,
                        ContentNews = x.ContentNews,
                        Authors = x.Authors,
                        Tags = x.Tags,
                        TotalView = x.TotalView,
                        Status = x.Status,
                        CreatedDate = x.CreatedDate
                    }).ToList();
            }
        }

        private IList<CMSNewsView> GetCMSNewsRecursive(int categoryId)
        {
            using (var db = new OnlineStoreMVCEntities())
            {
                var childCategories = db.cms_Categories.Where(x => x.ParentId == categoryId);
                var news = new List<CMSNewsView>();

                if (childCategories.Count() == 0)
                {
                    return news;
                }

                foreach (var category in childCategories)
                {
                    news.AddRange(db.cms_News.Where(x => x.CategoryId == category.Id && x.Status == (int)OnlineStore.Infractructure.Utility.Define.Status.Active)
                        .Select(x => new CMSNewsView
                    {
                        Id = x.Id,
                        CategoryId = x.CategoryId,
                        CategoryTitle = x.cms_Categories.Title,
                        CoverImageId = x.CoverImageId,
                        CoverImagePath = x.share_Images.ImagePath,
                        Title = x.Title,
                        SubTitle = x.SubTitle,
                        ContentNews = x.ContentNews,
                        Authors = x.Authors,
                        Tags = x.Tags,
                        TotalView = x.TotalView,
                        Status = x.Status
                    }));

                    GetCMSNewsRecursive(category.Id);
                }

                return news;
            }
        }

        public IList<CMSNewsView> GetCMSNewsByCategoryId(int categoryId, int pageNumber, int pageSize, out int totalItems)
        {
            using (var db = new OnlineStoreMVCEntities())
            {
                var news = db.cms_News.Where(x => x.CategoryId == categoryId && x.Status == (int)OnlineStore.Infractructure.Utility.Define.Status.Active)
                    .Select(x => new CMSNewsView
                    {
                        Id = x.Id,
                        CategoryId = x.CategoryId,
                        CategoryTitle = x.cms_Categories.Title,
                        CoverImageId = x.CoverImageId,
                        CoverImagePath = x.share_Images.ImagePath,
                        Title = x.Title,
                        SubTitle = x.SubTitle,
                        ContentNews = x.ContentNews,
                        Authors = x.Authors,
                        Tags = x.Tags,
                        TotalView = x.TotalView,
                        Status = x.Status
                    }).ToList();

                news.AddRange(GetCMSNewsRecursive(categoryId));
                totalItems = news.Count();

                return news.OrderByDescending(x => x.SortOrder).ThenByDescending(x => x.CreatedDate).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            }
        }

        public IList<CMSNewsView> GetRecentCMSNews()
        {
            using (var db = new OnlineStoreMVCEntities())
            {
                return db.cms_News.Where(x => x.Status == (int)OnlineStore.Infractructure.Utility.Define.Status.Active)
                    .OrderByDescending(x => x.CreatedDate)
                    .Take(3)
                    .Select(x => new CMSNewsView
                    {
                        Id = x.Id,
                        CategoryId = x.CategoryId,
                        CategoryTitle = x.cms_Categories.Title,
                        CoverImageId = x.CoverImageId,
                        CoverImagePath = x.share_Images.ImagePath,
                        Title = x.Title,
                        SubTitle = x.SubTitle,
                        ContentNews = x.ContentNews,
                        Authors = x.Authors,
                        Tags = x.Tags,
                        TotalView = x.TotalView,
                        Status = x.Status,
                        CreatedDate = x.CreatedDate
                    }).ToList();
            }
        }

        public IList<CMSNewsView> GetRelatedCMSNews(int id)
        {
            using (var db = new OnlineStoreMVCEntities())
            {
                var news = db.cms_News.Find(id);
                return db.cms_News.Where(x => x.Status == (int)OnlineStore.Infractructure.Utility.Define.Status.Active && x.CategoryId == news.CategoryId && x.Id != news.Id)
                    .OrderByDescending(x => x.CreatedDate)
                    .Take(3)
                    .Select(x => new CMSNewsView
                    {
                        Id = x.Id,
                        CategoryId = x.CategoryId,
                        CategoryTitle = x.cms_Categories.Title,
                        CoverImageId = x.CoverImageId,
                        CoverImagePath = x.share_Images.ImagePath,
                        Title = x.Title,
                        SubTitle = x.SubTitle,
                        ContentNews = x.ContentNews,
                        Authors = x.Authors,
                        Tags = x.Tags,
                        TotalView = x.TotalView,
                        Status = x.Status,
                        CreatedDate = x.CreatedDate
                    }).ToList();
            }
        }

        public IList<CMSNewsView> GetCMSNewsForHomePage()
        {
            using (var db = new OnlineStoreMVCEntities())
            {
                return db.cms_News.Where(x => x.Status == (int)OnlineStore.Infractructure.Utility.Define.Status.Active && x.DisplayHomePage == true)
                    .OrderByDescending(x => x.CreatedDate)
                    .Take(3)
                    .Select(x => new CMSNewsView
                    {
                        Id = x.Id,
                        CategoryId = x.CategoryId,
                        CategoryTitle = x.cms_Categories.Title,
                        CoverImageId = x.CoverImageId,
                        CoverImagePath = x.share_Images.ImagePath,
                        Title = x.Title,
                        SubTitle = x.SubTitle,
                        ContentNews = x.ContentNews,
                        Authors = x.Authors,
                        Tags = x.Tags,
                        TotalView = x.TotalView,
                        Status = x.Status,
                        CreatedDate = x.CreatedDate
                    }).ToList();
            }
        }
        public bool AddCMSNews(CMSNewsView cmsNewsView)
        {
            try
            {
                using (var db = new OnlineStoreMVCEntities())
                {
                    var cmsNews = new cms_News
                    {
                        CategoryId = cmsNewsView.CategoryId,
                        CoverImageId = cmsNewsView.CoverImageId,
                        Title = cmsNewsView.Title,
                        SubTitle = cmsNewsView.SubTitle,
                        ContentNews = cmsNewsView.ContentNews,
                        Authors = cmsNewsView.Authors,
                        Tags = cmsNewsView.Tags,
                        TotalView = cmsNewsView.TotalView,
                        DisplayHomePage = cmsNewsView.DisplayHomePage,
                        Status = (int)OnlineStore.Infractructure.Utility.Define.Status.Active,
                        SortOrder = cmsNewsView.SortOrder,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    };
                    db.cms_News.Add(cmsNews);
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
                    news.CoverImageId = cmsNewsView.CoverImageId;
                    news.Title = cmsNewsView.Title;
                    news.SubTitle = cmsNewsView.SubTitle;
                    news.ContentNews = cmsNewsView.ContentNews;
                    news.Authors = cmsNewsView.Authors;
                    news.Tags = cmsNewsView.Tags;
                    news.TotalView = cmsNewsView.TotalView;
                    news.DisplayHomePage = cmsNewsView.DisplayHomePage;
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

        public bool UpdateCMSNewsCountView(int? newsId)
        {
            try
            {
                using (var db = new OnlineStoreMVCEntities())
                {
                    var news = db.cms_News.Find(newsId);
                    news.TotalView++;
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
                    CategoryTitle = news.cms_Categories.Title,
                    CoverImageId = news.CoverImageId,
                    CoverImageName = news.share_Images.ImageName,
                    CoverImagePath = news.share_Images.ImagePath,
                    Title = news.Title,
                    SubTitle = news.SubTitle,
                    ContentNews = news.ContentNews,
                    Authors = news.Authors,
                    Tags = news.Tags,
                    DisplayHomePage = news.DisplayHomePage,
                    TotalView = news.TotalView,
                    Status = news.Status,
                    SortOrder = news.SortOrder,
                    CreatedDate = news.CreatedDate
                };
            }
        }

        public bool DeleteCMSNews(int id)
        {
            try
            {
                using (var db = new OnlineStoreMVCEntities())
                {
                    var news = db.cms_News.Find(id);
                    news.Status = (int)OnlineStore.Infractructure.Utility.Define.Status.Delete;
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

using OnlineStore.Model.Context;
using OnlineStore.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Interfaces
{
    public interface ICMSNewsService
    {
        IList<CMSNewsView> GetCMSNews(int pageNumber, int pageSize, out int totalItems);
        IList<CMSNewsView> GetCMSNewsByCategoryId(int categoryId, int pageNumber, int pageSize, out int totalItems);
        IList<CMSNewsView> GetRecentCMSNews();
        IList<CMSNewsView> GetCMSNewsForHomePage();
        IList<CMSNewsView> GetRelatedCMSNews(int id);
        bool AddCMSNews(CMSNewsView cmsNewsView);
        bool EditCMSNews(CMSNewsView cmsNewsView);
        bool DeleteCMSNews(int id);
        bool UpdateCMSNewsCountView(int? newsId);
        CMSNewsView GetCMSNewsById(int? newsId);
    }
}

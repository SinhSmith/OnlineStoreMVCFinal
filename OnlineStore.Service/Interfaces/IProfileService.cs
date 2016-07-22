using OnlineStore.Model.Context;
using OnlineStore.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Interfaces
{
    public interface IProfileService
    {
        IList<ProfileViewModel> GetProfiles(int pageNumber, int pageSize, out int totalItems);
        bool AddProfile(ProfileViewModel profileViewModel);
        //bool EditCMSNews(CMSNewsView cmsNewsView);
        bool DeleteProfile(Guid userId);
        //CMSNewsView GetCMSNewsById(int? newsId);
    }
}

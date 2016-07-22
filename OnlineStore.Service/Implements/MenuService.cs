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
    public class MenuService : IMenuService
    {
        public IList<MenuView> GetMenuByType(int typeId)
        {
            using (var db = new OnlineStoreMVCEntities())
            {
                return db.system_Menu.Where(x => x.Type == typeId && x.Status == (int)OnlineStore.Infractructure.Utility.Define.Status.Active).OrderBy(x => x.SortOrder)
                    .Select(x => new MenuView
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Url = x.Url,
                        Type = x.Type,
                        Icon = x.Icon
                    }).ToList();
            }
        }
    }
}

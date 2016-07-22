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
    public class SystemConfigService : ISystemConfigService
    {
        public int GetTotalVisitors()
        {
            using (var db = new OnlineStoreMVCEntities())
            {
                var config = db.system_Config.FirstOrDefault(x => x.Name == OnlineStore.Infractructure.Utility.Define.SystemConfig.TotalVisitors.ToString());
                if (config != null)
                {
                    return Convert.ToInt32(config.Value);
                }
                return 0;
            }
        }

        public bool UpdateTotalVisitors()
        {
            try
            {
                using (var db = new OnlineStoreMVCEntities())
                {
                    var config = db.system_Config.FirstOrDefault(x => x.Name == OnlineStore.Infractructure.Utility.Define.SystemConfig.TotalVisitors.ToString());
                    if (config != null)
                    {
                        config.Value = (Convert.ToInt32(config.Value) + 1).ToString();
                    }
                    else
                    {
                        var TrackingConfig = new system_Config
                        {
                            Name = OnlineStore.Infractructure.Utility.Define.SystemConfig.TotalVisitors.ToString(),
                            Value = "1",
                            Status = (int)OnlineStore.Infractructure.Utility.Define.Status.Active
                        };
                        db.system_Config.Add(TrackingConfig);
                    }
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

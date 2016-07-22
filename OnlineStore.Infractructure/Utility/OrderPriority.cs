using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infractructure.Utility
{
    public enum OrderPriorityEnum
    {
        High,
        Medium,
        Low
    }

    public class OrderPriority
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public OrderPriority(string Name, int Value)
        {
            this.Name = Name;
            this.Value = Value;
        }

    }

    public static class OrderPriorityHelper
    {
        public static IEnumerable<OrderPriority> getListOrderPriority()
        {
            List<OrderPriority> reVal = new List<OrderPriority>(){
                new OrderPriority("High",(int)OrderPriorityEnum.High),
                new OrderPriority("Medium",(int)OrderPriorityEnum.Medium),
                new OrderPriority("Low",(int)OrderPriorityEnum.Low)
            };

            return reVal;
        }

        public static string GetOrderPriorityName(int? order)
        {
            switch (order)
            {
                case (int)OrderPriorityEnum.High: return "High";
                case (int)OrderPriorityEnum.Medium: return "Medium";
                case (int)OrderPriorityEnum.Low: return "Low";
                default: return "";
            }
        }
    }
}

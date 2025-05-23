using JewerlyGala.Domain.Common;
using JewerlyGala.Domain.Entities;

namespace JewerlyGala.Domain.Events
{
    public class SalesOrderUpdateCostsEvent : BaseEvent
    {
        public SalesOrderUpdateCostsEvent(SalesOrder order)
        {
            Order = order;
        }

        public SalesOrder Order { get; }
    }
}

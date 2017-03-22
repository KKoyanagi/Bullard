using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public interface IViewRepository
    {
        List<PendingView> GetPendingViews(int week_id);
        List<ApprovedView> GetApprovedViews(int week_id);
    }
}

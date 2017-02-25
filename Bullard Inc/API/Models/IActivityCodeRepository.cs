using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public interface IActivityCodeRepository
    {
        IEnumerable<ActivityCode> GetActivityCodes();
    }
}

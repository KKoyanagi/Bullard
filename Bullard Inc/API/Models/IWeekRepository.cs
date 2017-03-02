﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public interface IWeekRepository
    {
        IEnumerable<WorkWeek> GetWorkWeeks();
        WorkWeek GetWeekById(int week_id);
        WorkWeek InsertWeek(WorkWeek week);
        WorkWeek RemoveWeek(int week_id);
        void UpdateWeek(WorkWeek week);
    }
}

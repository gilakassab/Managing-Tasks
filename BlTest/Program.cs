using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlTest;

    internal class Program
    {
     static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    BO.Engineer? boEngineer = s_bl.Engineer.Read(3);
    BO.Task? boTask = s_bl.Task.Read(3);
    BO.Engineer? boMilestone = s_bl.Milestone.Read(3);
}


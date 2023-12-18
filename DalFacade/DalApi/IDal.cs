﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface IDal
    {
        IDependency Dependency { get; }
        IEngineer Engineer { get; }
        ITask Task { get; }

        DateTime? startProject { get; }
        DateTime? deadlineProject { get; }

        public void Reset()
        {
            Task.Reset();
            Engineer.Reset();
            Dependency.Reset();
        }
    }
}

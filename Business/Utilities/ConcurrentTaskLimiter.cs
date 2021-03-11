using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Utilities
{
    public class ConcurrentTaskLimiter
    {
        public static int ParalelTasks { get { return _paralelTasks; } set { _paralelTasks = value; } }
        private static int _paralelTasks = 3;
        public static SemaphoreSlim GetSemaphoreSlim()
        {
            return new SemaphoreSlim(_paralelTasks, _paralelTasks);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralServiceHost.Model
{
    public  enum JobStatusType
    {
       Suspend, //挂起中
	   Running, //执行中
	   Rerunning, //守护重新执行中
	   Stop,//停止中
	   Obsolete //已结束
    }
}

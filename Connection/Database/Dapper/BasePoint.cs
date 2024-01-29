using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connection.Database.Dapper
{
    public class BasePoint
    {
        protected IDatabaseHub DatabaseHub = new DatabaseHub();
    }
}

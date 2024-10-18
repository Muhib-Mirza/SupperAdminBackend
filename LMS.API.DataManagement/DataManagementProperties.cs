using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace LMS.API.DataManagement
{
    public class DataManagementProperties
    {
        public DataManagementProperties()
        {

        }

        public IConfiguration config { get; set; }
    }
}

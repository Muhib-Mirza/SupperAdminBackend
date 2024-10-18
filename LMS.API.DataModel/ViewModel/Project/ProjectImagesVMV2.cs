using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.API.DataModel.ViewModel.Project
{
    public class ProjectImagesVMV2 : BaseViewModel
    {
        public long id { get; set; }
        public long project_id { get; set; }
        public string image { get; set; }
    }
}

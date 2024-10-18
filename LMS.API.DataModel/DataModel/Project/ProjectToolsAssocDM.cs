using LMS.API.DataModel.ViewModel.Project;
using LMS.API.Extensions.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.API.DataModel.DataModel.Project
{
    public class ProjectToolsAssocDM:BaseDataModel
    {
        public ProjectToolsAssocDM()
        {
            
        }
        public ProjectToolsAssocDM(ProjectToolsAssocVMV2 obj)
        {
            this.project_week_tool_id=obj.project_week_tool_id;
            this.project_id=obj.project_id;
            this.week_id=obj.week_id;
            this.tool_id=obj.tool_id;
        }
        public int project_week_tool_id { get; set; }
        public int project_id { get; set; }
        public int week_id { get; set; }
        public int tool_id { get; set; }
        public DateTime creation_date { get; set; }
        public DateTime modification_date { get; set; }
        public int created_by { get; set; }
        public int modified_by { get; set; }
    }
}

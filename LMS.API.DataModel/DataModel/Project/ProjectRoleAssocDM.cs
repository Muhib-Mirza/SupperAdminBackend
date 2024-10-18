using LMS.API.DataModel.ViewModel.Project;
using LMS.API.Extensions.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.API.DataModel.DataModel.Project
{
    public class ProjectRoleAssocDM:BaseDataModel
    {
        public ProjectRoleAssocDM()
        {
            
        }
        public ProjectRoleAssocDM(ProjectRoleAssocVMV2 obj)
        {
            this.project_role_id = obj.project_role_id;
            this.project_id = obj.project_id;
            this.role_id = obj.role_id;
        }
        public int project_role_id { get; set; }
        public long project_id { get; set; }
        public int role_id { get; set; }
        public DateTime creation_date { get; set; }
        public DateTime modification_date { get; set; }
        public int created_by { get; set; }
        public int modified_by { get; set; }
    }
}

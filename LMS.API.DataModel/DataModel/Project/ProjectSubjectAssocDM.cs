using LMS.API.DataModel.ViewModel.Project;
using LMS.API.Extensions.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.API.DataModel.DataModel.Project
{
    public class ProjectSubjectAssocDM:BaseDataModel
    {
        public ProjectSubjectAssocDM()
        {
            
        }
        public ProjectSubjectAssocDM(ProjectSubjectAssocVMV2 obj)
        {
            this.project_subject_id = obj.project_subject_id;
            this.project_id = obj.project_id;
            this.subject_id = obj.subject_id;
            
        }
        public int project_subject_id { get; set; }
        public long project_id { get; set; }
        public int subject_id { get; set; }
        public DateTime creation_date { get; set; }
        public DateTime modification_date { get; set; }
        public int created_by { get; set; }
        public int modified_by { get; set; }
    }
}

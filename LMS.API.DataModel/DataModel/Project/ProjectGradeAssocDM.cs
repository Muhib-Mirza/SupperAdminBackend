using LMS.API.DataModel.ViewModel.Project;
using LMS.API.Extensions.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.API.DataModel.DataModel.Project
{
    public class ProjectGradeAssocDM:BaseDataModel
    {
        public ProjectGradeAssocDM()
        {
            
        }
        public ProjectGradeAssocDM(ProjectGradeAssocVM obj)
        {
            this.project_grade_id = obj.project_grade_id;
            this.project_id = obj.project_id;
            this.grade_id=obj.grade_id;
            
        }
        public int project_grade_id { get; set; }
        public long project_id { get; set; }
        public int grade_id { get; set; }
        public DateTime creation_date { get; set; }
        public DateTime modification_date { get; set; }
        public int created_by { get; set; }
        public int modified_by { get; set; }
    }
}

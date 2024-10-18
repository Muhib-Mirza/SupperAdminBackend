using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.API.DataModel.ViewModel.Project
{
    public class ProjectGradeAssocVM:BaseViewModel
    {
        public int project_grade_id { get; set; }
        public int project_id { get; set; }
        public int grade_id { get; set; }
        public string grade_name { get; set; }
        public DateTime creation_date { get; set; }
        public DateTime modification_date { get; set; }
        public int created_by { get; set; }
        public int modified_by { get; set; }
    }
}

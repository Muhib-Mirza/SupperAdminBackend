using LMS.API.DataModel.ViewModel;
using LMS.API.Extensions.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.API.DataModel.DataModel.Project
{
    public class ProjectsFiltersDM:BaseDataModel
    {
        public int roleId { get; set; }
        public int subjectID { get; set; }
        public int industryID { get; set; }
        public int GradeID { get; set; }
        public string roleName { get; set; }
        public string subjectName { get; set; }
        public string industryName { get; set; }
        public string GradeName { get; set; }
    }
}

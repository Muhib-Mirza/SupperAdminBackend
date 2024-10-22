using LMS.API.Extensions.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.API.DataModel.ViewModel.Project
{
    public class ProjectFiltersVM 
    {
        public List<string> lstRoleFilter { get; set; }
        public List<string> lstSubjectFilter { get; set; }
        public List<string> lstIndustryFilter { get; set; }
        public List<string> lstGradeFilter { get; set; }
    }

    public class RoleFilterVM 
    {
        //public int Id { get; set; }
        public string Name { get; set; }

    }

    public class SubjectFilterVM 
    {
        //public int Id { get; set; }
        public string Name { get; set; }
    }

    public class IndustryFilterVM
    {
       // public int Id { get; set; }
        public string Name { get; set; }
    }

    public class GradeFilterVM 
    {
        //public int Id { get; set; }
        public string Name { get; set; }
    }
}

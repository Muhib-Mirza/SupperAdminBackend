using LMS.API.Extensions.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.API.DataModel.ViewModel.Project
{
    public class ProjectFiltersVM : BaseViewModel
    {
        public List<RoleFilterVM> lstRoleFilter { get; set; }
        public List<SubjectFilterVM> lstSubjectFilter { get; set; }
        public List<IndustryFilterVM> lstIndustryFilter { get; set; }
        public List<GradeFilterVM> lstGradeFilter { get; set; }
    }

    public class RoleFilterVM : BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class SubjectFilterVM : BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class IndustryFilterVM : BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class GradeFilterVM : BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

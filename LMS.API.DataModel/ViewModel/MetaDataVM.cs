using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.API.DataModel.ViewModel
{
    public class MetaDataVM
    {
       public IEnumerable<GradeVM> gradeList { get; set;}
        public IEnumerable<RoleVM> roleList { get; set;}
        public IEnumerable<SubjectVM> subjectList { get; set;}
        public IEnumerable<IndustryVM> industryList { get; set;}
        public IEnumerable<ToolMetaVM> toolList { get; set; }
    }
}

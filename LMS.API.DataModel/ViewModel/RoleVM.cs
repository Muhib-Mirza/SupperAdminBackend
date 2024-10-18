using APL.API.Extensions.DataModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.API.DataModel.ViewModel
{
    public class RoleVM : BaseViewModel, IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RoleDescription { get; set; }
        public string ToolsSkills { get; set; }
        public string AssessmentCriteria { get; set; }
    }
}

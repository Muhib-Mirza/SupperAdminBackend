using APL.API.Extensions.DataModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.API.DataModel.ViewModel
{
    public class ProjectGradeAssociationVM : BaseViewModel, IModel
    {
        public int ProjectGradeId { get; set; }
        public int ProjectId { get; set; }
        public int GradeId { get; set; }
    }
}

using APL.API.Extensions.DataModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.API.DataModel.ViewModel
{
    public class ProjectRolesVM : BaseViewModel, IModel
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public int? RoleId { get; set; }
    }
}

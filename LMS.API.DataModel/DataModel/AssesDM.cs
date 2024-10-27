using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APL.API.Extensions.DataModel;
using LMS.API.DataModel.ViewModel;
using LMS.API.Extensions.DataModel;

namespace LMS.API.DataModel.DataModel
{
    public partial class AssesDM : BaseDataModel, IModel
    {
        public string Code { get; set; }
        public int UserId { get; set; }
        public AssesDM(AssesmentModel Obj)
        {
            this.Code = Obj.Code;
            this.UserId = Obj.UserId;
        }

        public AssesDM()
        {

        }
    }
}

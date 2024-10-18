using LMS.API.DataModel.ViewModel;
using APL.API.Extensions.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using LMS.API.Extensions.DataModel;

namespace LMS.API.DataModel
{
    public class ActiveUserSessionDM : BaseDataModel, IModel
    {
        public ActiveUserSessionDM(ActiveUserSessionVM activeUserSessionVM) 
        {
            this.actvSessionId = activeUserSessionVM.actvSessionId;
            this.userId = activeUserSessionVM.userId;
            this.token = activeUserSessionVM.token;
            this.systemIp = activeUserSessionVM.systemIp;
            this.UserAgent = activeUserSessionVM.UserAgent;
        }
        public ActiveUserSessionDM() 
        {

        }
        public long actvSessionId { get; set; }

        public long userId { get; set; }

        public string token { get; set; }

        public string systemIp { get; set; }

        public string UserAgent { get; set; }
    }
}

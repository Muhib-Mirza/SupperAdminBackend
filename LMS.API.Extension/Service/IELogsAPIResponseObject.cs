using LMS.API.Extensions.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.API.Extensions.Service
{
    public class IELogsAPIResponseObject<T>
    {
        public IELogsAPIResponseObject()
        {
            statusCode = StatusCodeEnum.FAIL_IE_LOGS_API.GetDescription();
            messageCode = MessageCodeEnum.BIZ_RULE_FAIL_IE_LOGS_API.GetDescription();
            message = ExceptionMessagesEnum.SERVER_ISSUE.GetDescription();
        }
        public IELogsAPIResponseObject(string statusCode)
        {
            statusCode = statusCode;
            messageCode = MessageCodeEnum.BIZ_RULE_FAIL_IE_LOGS_API.GetDescription();
            message = ExceptionMessagesEnum.SERVER_ISSUE.GetDescription();
        }
        public string statusCode { get; set; }
        public string messageCode { get; set; }
        public string message { get; set; }
    }
}

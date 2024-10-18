using LMS.API.Extensions.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.API.Extensions.DataTypes
{
    public class ExtendedException: Exception
    {
       

        public ExtendedException()
        {
        }

        public ExtendedException(string messageCode)
            : base(messageCode+PostPrefixEnum.ExtendedException.GetDescription())
        {

            messageCode = messageCode + PostPrefixEnum.ExtendedException.GetDescription();
        }

        public ExtendedException(string message, Exception innerEx)
            : base(message, innerEx)
        {
                
        }

        public ExtendedException(string messageCode, string message, Exception innerEx)
           : base(messageCode + PostPrefixEnum.ExtendedException.GetDescription() + message , innerEx)
        {

            
        }

        public ExtendedException(string statusCode , string messageCode, string message, Exception innerEx)
           : base(statusCode + PostPrefixEnum.ExtendedException.GetDescription()  + messageCode + PostPrefixEnum.ExtendedException.GetDescription() + message, innerEx)
        {


        }


    }
}

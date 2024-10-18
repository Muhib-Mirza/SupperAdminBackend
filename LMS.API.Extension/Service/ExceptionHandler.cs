using LMS.API.Extensions.DataTypes;
using LMS.API.Extensions.Enums;
using LMS.API.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.API.Extensions.Service
{
    public class ExceptionHandler
    {

    public static IELogsAPIResponseObject<ExceptionObject> ProceedIELogs(Exception ex)
    {
        IELogsAPIResponseObject<ExceptionObject> response = new IELogsAPIResponseObject<ExceptionObject>();
        response.message = ex.Message.Replace(PostPrefixEnum.ExtendedException_IELogsAPI.GetDescription(), String.Empty);
        if(response.message== ExceptionMessagesEnum.MANDATORY_MISSINGA_IELogs_API.GetDescription())
        {
            response.statusCode = StatusCodeEnum.MANDATORY_MISSINGA_IELogs_API.GetDescription();
            response.messageCode = StatusCodeEnum.MANDATORY_MISSINGA_IELogs_API.GetDescription();
        }
        if (response.message == ExceptionMessagesEnum.NOT_ALLOWED_ROW_COUNT.GetDescription())
        {
            response.statusCode = StatusCodeEnum.WARNING_IE_LOGS_API.GetDescription();
            response.messageCode = StatusCodeEnum.WARNING_IE_LOGS_API.GetDescription();
        }
        if (response.message == ExceptionMessagesEnum.INVALID_IE_REPORT_CONFIGUATION.GetDescription())
        {
            response.statusCode = StatusCodeEnum.FAIL_IE_LOGS_API.GetDescription();
            response.messageCode = StatusCodeEnum.FAIL_IE_LOGS_API.GetDescription();
        }

            Logger.LogException(ex);
        return response;
    }

    public static ResponseObject<ExceptionObject> Proceed(Exception ex )
    {


        ResponseObject<ExceptionObject> response = new ResponseObject<ExceptionObject>();
        response.messageCode = ex.Message.Replace(PostPrefixEnum.ExtendedException.GetDescription(),String.Empty);   
        response.message = ex.Message;
        switch (ex.GetType().Name.ToLower())
        {
                case "sqlexception":
                    if (!response.messageCode.Contains("timeout")) { response.statusCode = StatusCodeEnum.SQL_EXCEPTION.GetDescription(); }
                    else
                    {
                        response.statusCode = StatusCodeEnum.TIMEOUT_SQL_EXCEPTION.GetDescription();
                    }
                    break;
                case "objectdisposedexception":
               response.statusCode = StatusCodeEnum.DIAPER_EXCEPTION.GetDescription();
               break;
           case "dividebyzeroexception":
               response.statusCode = StatusCodeEnum.DIVIDE_BY_ZERO.GetDescription();
               break;
           case "overflowexception":
               response.statusCode = StatusCodeEnum.OVERFLOW.GetDescription();
               break;
           case "arithmeticexception":
               response.statusCode = StatusCodeEnum.ARITHMATIC_EXCEPTION.GetDescription();
               break;
           case "indexoutofrangeexception":
               response.statusCode = StatusCodeEnum.INDEX_OUT_OF_RANGE_EXCEPTION.GetDescription();
               break;
           case "arraytypemismatchexception":
               response.statusCode = StatusCodeEnum.ARRAY_TYPE_MISMATCH_EXCEPTION.GetDescription();
               break;
           case "invalidcastexception":
               response.statusCode = StatusCodeEnum.INVALID_CAST_EXCEPTION.GetDescription();
               break;
           case "nullreferenceexception":
               response.statusCode = StatusCodeEnum.NULL_REFERENCE_EXCEPTION.GetDescription();
               break;
           case "outofmemoryexception":
               response.statusCode = StatusCodeEnum.OUT_OF_MEMORY_EXCEPTION.GetDescription();
               break;
           case "timeoutexception":
               response.statusCode = StatusCodeEnum.TIMEOUT_EXCEPTION.GetDescription();
               break;
           case "argumentoutofrangeexception":
               response.statusCode = StatusCodeEnum.ARGUMENT_OUT_OF_EXCEPTION.GetDescription();
               break;
          case "extendedexception":
              response.statusCode = StatusCodeEnum.CUSTOM_ERROR.GetDescription();
              break;
          case "invalidoperationexception":
              response.statusCode = StatusCodeEnum.CUSTOM_ERROR.GetDescription();
              break;
          default:
               response.statusCode = StatusCodeEnum.EXCEPTION.GetDescription();
               break;
        }
        
        Logger.LogException(ex);
        return response;
    }

    /// <summary>
    /// Proceed for Exception in case from repository with logging exception but not raising it.
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    public static void Proceed(Exception ex, string Message)
    {
        Logger.LogInformation(Message);
        Logger.LogException(ex);
    }

    public static void RaiseException(ExceptionTypeEnum type, ExceptionMessagesEnum message, Exception innerException = null)
        {
            if (type == ExceptionTypeEnum.Business)
            {
                Logger.LogWarning(message.GetDescription());
            }
            ExtendedException exp = new ExtendedException(message.GetDescription(),innerException);
            
            throw exp;
        }
        public static void Proceed(ExtendedException ex)
        {

        }

    }
}

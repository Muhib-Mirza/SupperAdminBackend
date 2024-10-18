using System;
using System.Collections.Generic;
using System.Text;
    
namespace LMS.API.Extensions.Enums
{
    [DescriptiveEnumEnforcement(DescriptiveEnumEnforcementAttribute.EnforcementTypeEnum.ThrowException)]

    public enum StatusCodeEnum
    {



        [Description("111")]
        SUCCESS,

        [Description("112")]
        INVALID_SESSION,


        [Description("113")]
        INVALID_USER,

        [Description("114")]
        INVALID_CODE,


        [Description("115")]
        EXCEPTION,

        [Description("116")]
        CUSTOM_ERROR,

        [Description("117")]
        ALREADY_LOGOUT_ERROR,

        [Description("118")]
        FAIL,

        #region DIS API
        [Description("114")]
        DIS_SUCCESS_STATUS_CODE,

        [Description("115")]
        DIS_ERROR_STATUS_CODE,

        [Description("119")]
        DIS_RUNID_NOT_ANNC_STATUS_CODE,

        [Description("122")]
        DIS_FILE_ALREADY_ANNOUNCE_STATUS_CODE,

        [Description("116")]
        DIS_FILE_NOT_SENT_STATUS_CODE ,
        [Description("117")]
        DIS_FILE_UPLOADED_SUCCESS_STATUS_CODE,
        [Description("127")]
        DIS_ERROR_UPGRADE_APP_VERSION,
        [Description("128")]
        DIS_ERROR_CONTACT_ADMINISTRATOR,

        #endregion
        [Description("129")]
        SQL_EXCEPTION,
        [Description("130")]
        DIAPER_EXCEPTION,
        [Description("131")]
        DIVIDE_BY_ZERO,
        [Description("132")]
        OVERFLOW,
        [Description("133")]
        ARITHMATIC_EXCEPTION,
        [Description("134")]
        INDEX_OUT_OF_RANGE_EXCEPTION,
        [Description("135")]
        ARRAY_TYPE_MISMATCH_EXCEPTION,
        [Description("136")]
        INVALID_CAST_EXCEPTION,
        [Description("137")]
        NULL_REFERENCE_EXCEPTION,
        [Description("138")]
        OUT_OF_MEMORY_EXCEPTION,
        [Description("139")]
        TIMEOUT_EXCEPTION,
        [Description("140")]
        ARGUMENT_OUT_OF_EXCEPTION,
        [Description("141")]
        TIMEOUT_SQL_EXCEPTION,

        [Description("21117")]
        CONFIG_ALREADY_EXIST,

            //AZURE QUEUE STATUS CODES
        [Description("142")]
        IMAGE_PROCESS_SUCCESS,
        [Description("143")]
        IMAGE_PROCESS_FAILED,
        [Description("144")]
        IMAGE_ALGORITHM_CRASHED,
        #region IELogsAPI
        [Description("11114")]
        SUCCESS_IE_LOGS_API,
        [Description("21114")]
        FAIL_IE_LOGS_API,
        [Description("21115")]
        WARNING_IE_LOGS_API,
        [Description("21116")]
        MANDATORY_MISSINGA_IELogs_API
       ,


        
        #endregion



    }
}

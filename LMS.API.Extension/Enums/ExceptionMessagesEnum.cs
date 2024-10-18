using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.API.Extensions.Enums
{
    [DescriptiveEnumEnforcement(DescriptiveEnumEnforcementAttribute.EnforcementTypeEnum.ThrowException)]

    public enum ExceptionMessagesEnum
    {
        [Description("Invalid file, Key Field not found.")]
        KEYFIELD_NOT_FOUND,
        [Description("User already exist in the system.")]
        USER_EXIST,
        [Description("An issue happened at server end.")]
        SERVER_ISSUE,
        [Description("An issue happened while sending email.")]
        EMAIL_ISSUE,
        [Description("Sorry, we don’t recognize these credentials.")]
        AUTH_FAIL,


        [Description("No user found with given email.")]
        USER_NOT_FOUND,

        [Description("This user is blocked.")]
        USER_BLOCKED,
        [Description("Harvest Date should be greater than Planting Date")]
        HARVEST,
        [Description("Multispectral Batch should not be started yet. Process the RGB Normal Flight first.")]
        H3_INDEX_AREA,
        [Description("Dont found H3 Index Area")]
        STATUS,

        [Description("Plan Id is Required.")]
        PLANID_NOT_PROVIDED,

        [Description("Farm Id is Required.")]
        FARMID_NOT_PROVIDED,

        [Description("Farmer Id is Required.")]
        FARMERID_NOT_PROVIDED,
        [Description("This link has expired, please contact Agrilift Support at support@agrilift.ai")]
        PASSWORD_LINK_EXPIRED,
        [Description("This report is already associated with a report group.")]
        REPORT_EXIST,
        [Description("This Sample Matrix already exist.")]
        SAMPLE_MATRIX_EXIST,
        [Description("Default complex cycling configuration already exist.")]
        DEFAULT_COMPLEX_CYCLING_CONFIG_EXIST,
        [Description("Complex cycling configuration for the vaccine and feed program already exist.")]
        COMPLEX_CYCLING_CONFIG_EXIST,
        [Description("Complex cycling configuration for the vaccine already exist.")]
        COMPLEX_CYCLING_CONFIG_EXIST_VACCINE,
        [Description("Complex cycling configuration for the feed program already exist.")]
        COMPLEX_CYCLING_CONFIG_EXIST_FEED_PROGRAM,


        #region DIS API
        [Description("Failure Response.")]
        DIS_FAILURE_RESPONSE,
        [Description("No file provided in request object.")]
        DIS_DATASET_FILES_EMPTY,
        [Description("Run id is already announced.")]
        DIS_DATASET_DUPLICATE_ANNOUNCEMENT,
        [Description("Run id is not announced.")]
        DIS_DATASET_RUNID_NO_ANNOUNCEMENT,
        [Description("Run id is not provided in request object.")]
        DIS_DATASET_EMPTY_RUNID,
        [Description("Request id is not provided in request object.")]
        DIS_DATASET_EMPTY_REQUESTID,
        [Description("DateTime is not provided in request object.")]
        DIS_DATASET_EMPTY_DATETIME,
        [Description("Piper ID is not provided in request object.")]
        DIS_DATASET_EMPTY_PIPERID,
        [Description("File type is not provided in request object.")]
        DIS_DATASET_EMPTY_FILETYPE,
        [Description("Log type is not provided in request object.")]
        DIS_DATASET_EMPTY_LOGTYPE,
        [Description("File name is not provided in request object.")]
        DIS_DATASET_EMPTY_FILENAME,
        [Description("Checksum is not provided in request object.")]
        DIS_DATASET_EMPTY_CHECKSUM,
        [Description("File is already uploaded.")]
        DIS_DATASET_DUPLICATE_FILE_UPLOAD,
        [Description("Checksum of the uploaded file does not match with announced checksum.")]
        DIS_DATASET_MISMATCH_FILE_CHECKSUM,
        [Description("Uploaded file was not announced.")]
        DIS_DATASET_UNANNOUNCED_FILE_UPLOAD,
        [Description("Invalid request id is provided.")]
        DIS_DATASET_INVALID_REQUESTID,
        [Description("Invalid request type is provided.")]
        DIS_DATASET_INVALID_REQUEST_TYPE,
        [Description("Invalid piper id is provided.")]
        DIS_DATASET_INVALID_PATHOGEN_NAME,
        [Description("Invalid Pathogen Name provided.")]       
        DIS_DATASET_INVALID_PIPER_ID,
        [Description("Outcome List Is Empty.")]
        DIS_DATASET_EMPTY_OUTCOME_LIST,
        [Description("Version Upgrade Is False.")]
        DIS_DATASET_VERSION_UPGRADE_FALSE,
        [Description("Invalid datetime is provided.")]
        DIS_DATASET_INVALID_DATETIME,
        [Description("Invalid status is provided.")]
        DIS_DATASET_INVALID_HEARTBEAT_STATUS,
        [Description("Invalid status is provided.")]
        DIS_DATASET_INVALID_PIPER_VERSION,
        [Description("File name in input and in DB are different.")]
        DIS_DATASET_FILE_NAME_MISMATCH,
        [Description("This file cannot be downloaded. Either there is no upgrade or this file has already been downloaded. ")]
        DIS_FILE_DOWNLOADED,
        #endregion
        [Description("The maximum number of users for this organization have already been created.")]
        MAX_ORGAN_USER_LIMIT_CROSSED,
        [Description("Invalid file provided.")]
        INVALID_FILE,
        [Description("Invalid file format, please upload an excel file.")]
        INVALID_FILETYPE,
        [Description("Invalid file, repetition in coulmn headers.")]
        COLUMN_REPEATED,
        [Description("Invalid file, mandatory columns are missing.")]
        MANDATORY_MISSING,
        [Description("Invalid file type provided in Request object.")]
        DIS_DATASET_INVALID_FILETYPE,
        [Description("Uploaded file contains no rows.")]
        INVALID_FILE_ROWS,
        [Description("Invalid improc provided in request object.")]
        DIS_INVALID_IMPROC,
        [Description("Invalid package name provided in request object.")]
        DIS_INVALID_PACKAGE_NAME,
        [Description("Invalid package version provided in request object.")]
        DIS_INVALID_PACKAGE_VERSION,
        [Description("Invalid userListVersion is provided in request object.")]
        DIS_DATASET_INVALID_USERLIST_VERSION,
        [Description("Duplicate checksum is not provided in request object.")]
        DIS_DATASET_DUPLICATE_CHECKSUM,
        [Description("Improc is not provided in request object.")]
        DIS_EMPTY_IMPROC,
        [Description("Request type is not provided according to defined sequence.")]
        DIS_DATASET_INVALID_REQUEST_TYPE_SEQUENCE,
        //Memo: AhmadSaud Bajwa:20200507 :Start
        [Description("Invalid MPNSettingVersion is provided in request object.")]
        DIS_DATASET_INVALID_MPNSettingVersion_VERSION,
       
        [Description("Version Mismatch.")]
        VERSION_MISMATCH,

        //Memo: AhmadSaud Bajwa:20200507 :End

        //MEMO: Usman Shahid: 20200717 DIS RAW IMAGE: Start //
        [Description("File related fields can be provided only with empty count outcome.")]
        DIS_DATASET_RAWIMAGE_EXTRA_FIELDS,
        //MEMO: Usman Shahid: 20200717 DIS RAW IMAGE: END //
        [Description("Upgrade the App Version.")]
        UPGRADE_APP_VERSION,


        #region Architecture Control Exceptions
        [Description("Please provide the configuration parameter.")]
        CONFIGURATION_NULL,
        [Description("The provided Sql query for Mark Deletion is not correct.")]
        SQL_QUERY_MARK_DELETE_ERROR,

        [Description("Protocol broken, contact administrator.")]
        TENANT_PARAMETER_NEEDED,

        [Description("Sql Select statement is not valid.")]
        SQL_SELECT_QUERY_NOT_VALID,
        [Description("Sql Update statement is not valid.")]
        SQL_UPDATE_QUERY_NOT_VALID,
        [Description("Sql Delete statement is not valid.")]
        SQL_DELETE_QUERY_NOT_VALID,
        [Description("Sql Insert statement is not valid.")]
        SQL_Insert_QUERY_NOT_VALID,


        #endregion

        #region EULA
        [Description("This user agreement is already assigned. It cannot be deleted.")]
        EULA_ASSIGNED_TO_USER_OR_ORGN,

        #endregion

        #region IELogsAPI
        [Description("Mandatory field(s) is/are missing")]
        MANDATORY_MISSINGA_IELogs_API,
        [Description("Accessing not authorized logs")]
        ACCESS_NOT_AUTHORIZED_LOGS,
        [Description("Row count other than 100,250 or 500")]
        NOT_ALLOWED_ROW_COUNT,
        [Description("Invalid Report Configuration Id")]
        INVALID_IE_REPORT_CONFIGUATION,
        #endregion

        #region Rule Engine Business Exceptions
        [Description("Harvested crops not Found")]
        SQL_SELECT_ALL_CROPS_HARVEST_NOT_FOUND,
        [Description("Tier information not Found")]
        SQL_SELECT_FARM_WISE_CROP_PROVINCE_TIER_INFO_NOT_FOUND,
        #endregion
    }
}

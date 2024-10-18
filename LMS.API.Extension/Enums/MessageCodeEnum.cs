using System;
using System.Collections.Generic;
using System.Text;
    
namespace LMS.API.Extensions.Enums
{
    [DescriptiveEnumEnforcement(DescriptiveEnumEnforcementAttribute.EnforcementTypeEnum.ThrowException)]

    public enum MessageCodeEnum
    {
        [Message("Data saved successfully.")]
        [Description("00001")]
        SAVE_SUCCESS,
        [Description("00002")]
        SAVE_FAIL,
        [Message("Data details updated.")]
        [Description("00003")]
        EDIT_SUCCESS,
        [Description("00004")]
        EDIT_FAIL,
        [Description("00005")]
        [Message("Data details deleted.")]
        DELETE_SUCCESS,
        [Description("00006")]
        DELETE_FAIL,

        [Message("Data loaded successfully.")]
        [Description("00007")]
        SELECT_SUCCESS,

        

        [Description("00008")]
        SELECT_FAIL,
        [Description("00009")]
        BIZ_RULE_FAIL ,
        [Message("Email sent successfully.")]
        [Description("00010")]
        EMAIL_SENT_SUCCESS,
        [Message("Data processed successfully.")]
        [Description("00011")]
        API_CALL_SUCCESS,
        [Message("Data processing failed.")]
        [Description("00012")]
        API_CALL_Fail,
        [Message("Login is successfull.")]
        [Description("00013")]
        LOGIN_SUCCESS,

        [Message("Unable to activate the alert, the user no longer exists.")]
        [Description("00014")]
        UserDeleted,
        [Message("Unable to activate the alert, the associated organization no longer exists.")]
        [Description("00015")]
        OrgnDeleted,
        [Message("Unable to activate the alert, the client organization changed.")]
        [Description("00016")]
        ClientOrgUpdated,
        [Message("Unable to activate the alert, the organization of the user changed.")]
        [Description("00017")]
        UserOrgnUpdated,
        [Message("Unable to activate the alert, the associated sites no longer exists.")]
        [Description("00018")]
        SiteDeleted,
        [Message("Unable to activate the alert, the user does not have access to associated sites.")]
        [Description("00019")]
        SiteUpdated,
        [Message("Piper details updated successfully.")]
        [Description("00020")]
        PiperUpdated,
        [Message("File already exists.")]
        [Description("00021")]
        FileAlreadyExist,


        [Message("A Flock with given Farm Site ID and Placement Date already exist in system.")]
        [Description("21118")]
        FLOCK_WITH_SAME_DATE_EXIST,

        #region DIS API Message Codes
        [Message("Successful Response.")]
        [Description("00114")]
        DIS_SUCCESS_MESSAGE_CODE,
        [Message("Failure Response.")]
        [Description("00115")]
        DIS_ERROR_MESSAGE_CODE,
   


        [Message("File Not Sent.")]
        [Description("00116")]
        DIS_FILE_NOT_SENT_STATUS_CODE,
        [Message("File Uploading Successful.")]
        [Description("00117")]
        DIS_FILE_UPLOADED_SUCCESS_STATUS_CODE,
        [Message("File Uploading Failed.")]
        [Description("00118")]
        DIS_FILE_UPLOADED_FAILED_STATUS_CODE,
        [Message("Invalid Token.")]
        [Description("00119")]
        DIS_INVALID_TOKEN,
        [Message("Expired Token.")]
        [Description("00120")]
        DIS_EXPIRED_TOKEN,
        [Message("Run Id is not announced.")]
        [Description("00121")]
        DIS_DATASET_RUNID_NO_ANNOUNCEMENT,
        [Description("00122")]
        [Message("File is already uploaded.")]
        DIS_DATASET_DUPLICATE_FILE_UPLOAD,
        [Message("Please check your e-mail for instructions.")]
        [Description("00123")]
        RESET_PASSWORD_SUCCESS,

        [Message("New Data created.")]
        [Description("00124")]
        CREATE_SUCCESS,

        [Message("Threshold Alert Email Sent Successfully.")]
        [Description("00125")]
        THRESHOLD_ALERT_EMAIL_SUCCESS,

        [Message("Aggreggate Alert Email Sent Successfully.")]
        [Description("00126")]
        AGGREGATE_ALERT_EMAIL_SUCCESS,

        [Message("Absence Alert Email Sent Sucessfully.")]
        [Description("00126")]
        NO_DATA_RECEIVED_ALERT_EMAIL_SUCCESS,
        [Message("UPGRADE APP VERSION.")]
        [Description("00127")]
        DIS_ERROR_UPGRADE_APP_VERSION,
        [Message("Please contact Administrator.")]
        [Description("00128")]
        DIS_ERROR_CONTACT_ADMINISTRATOR,
        [Message("Testing sites details updated.")]
        [Description("00130")]
        testingSitesUpdated,

        #endregion
        [Message("Report Settings Changed.")]
        [Description("00129")]
        REPORT_SETTINGS_CHANGED,
        [Message("Installation Run Config already exists")]
        [Description("000131")]
        InstallationRunExist,
        [Message("Listeria Configuration already exists")]
        [Description("000132")]
        ListeriaConfigExist,
        #region IELogsAPI
        [Message("Successful Response.")]
        [Description("11114")]
        SELECT_SUCCESS_IE_LOGS_API,
        [Description("21114")]
        BIZ_RULE_FAIL_IE_LOGS_API,

        [Message("Configuration exists against this Sample Matrix.")]
        [Description("21117")]
        CONFIG_ALREADY_EXIST,

        #endregion
    }
}

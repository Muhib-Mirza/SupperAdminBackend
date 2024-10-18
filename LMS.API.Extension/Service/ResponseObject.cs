using LMS.API.Extensions.Enums;
using System.Collections;

namespace LMS.API.Extensions.Service
{
    public class ResponseObject<T> 
    {
        public ResponseObject()
        {
            statusCode = StatusCodeEnum.FAIL.GetDescription();
            messageCode = MessageCodeEnum.BIZ_RULE_FAIL.GetDescription();
            message = ExceptionMessagesEnum.SERVER_ISSUE.GetDescription();
        }
        public string statusCode { get; set; }
        public string messageCode { get; set; }
        public string message { get; set; }
        public T dataObject { get; set; }

        public string fileName { get; set; }


        public void setDataObject(MessageCodeEnum messageCode, T dataObject, PlaceHoldersEnum placeHolder = PlaceHoldersEnum.Default, string placeholdermessage = "",string fileName = "")
        {
            if (messageCode == MessageCodeEnum.SAVE_SUCCESS)
            {
                this.statusCode = StatusCodeEnum.SUCCESS.GetDescription();
                this.messageCode = messageCode.GetDescription();
                this.message = messageCode.GetMessage();
                this.dataObject = dataObject;
                if (placeHolder == PlaceHoldersEnum.CompleteMessage && string.IsNullOrEmpty(placeholdermessage))
                {
                    this.message = this.message = placeholdermessage;

                }
                else if (placeHolder != PlaceHoldersEnum.Default)
                {
                    if (fileName != string.Empty)
                    {
                        this.messageCode = this.messageCode + "_file";
                        this.fileName = fileName;
                    }
                    else
                    {
                        string _msgCode = placeholdermessage.Replace(" ", "_");
                        this.messageCode = this.messageCode + "_" + _msgCode;
                    }
                    this.message = this.message.Replace(placeHolder.GetDescription(), placeholdermessage);
                }
            }
            else if (messageCode == MessageCodeEnum.SAVE_FAIL)
            {
                this.statusCode = StatusCodeEnum.FAIL.GetDescription();
                this.messageCode = messageCode.GetDescription();
                this.message = messageCode.GetMessage();
                this.dataObject = dataObject;
            }
            else if (messageCode == MessageCodeEnum.SELECT_SUCCESS)
            {
                this.statusCode = StatusCodeEnum.SUCCESS.GetDescription();
                this.messageCode = messageCode.GetDescription();
                this.message = messageCode.GetMessage();
                this.dataObject = dataObject;
                if (placeHolder == PlaceHoldersEnum.CompleteMessage && placeholdermessage != string.Empty)
                {
                    if(fileName != string.Empty)
                    {
                        this.messageCode = this.messageCode + "_file_error";
                        this.fileName = fileName;
                    }
                    this.message = this.message = placeholdermessage;

                }
                else if (placeHolder != PlaceHoldersEnum.Default)
                {
                    if (fileName != string.Empty)
                    {
                        
                        this.messageCode = this.messageCode + "_file"; 
                        this.message = this.message.Replace(placeHolder.GetDescription(), placeholdermessage);
                        this.fileName = fileName;
                    }
                    else
                    {
                        string _msgCode = placeholdermessage.Replace(" ", "_");
                        this.messageCode = this.messageCode + "_" + _msgCode;
                        this.message = this.message.Replace(placeHolder.GetDescription(), placeholdermessage);
                    }
                }
            }
            else if (messageCode == MessageCodeEnum.CREATE_SUCCESS)
            {
                this.statusCode = StatusCodeEnum.SUCCESS.GetDescription();
                this.messageCode = messageCode.GetDescription();
                this.message = messageCode.GetMessage();
                this.dataObject = dataObject;
                if (placeHolder == PlaceHoldersEnum.CompleteMessage && placeholdermessage != string.Empty)
                {
                    this.message = this.message = placeholdermessage;

                }
                else if (placeHolder != PlaceHoldersEnum.Default)
                {
                    string _msgCode = placeholdermessage.Replace(" ", "_");
                    this.messageCode = this.messageCode + "_" + _msgCode;
                    this.message = this.message.Replace(placeHolder.GetDescription(), placeholdermessage);
                }
            }
            else if (messageCode == MessageCodeEnum.EDIT_SUCCESS)
            {
                this.statusCode = StatusCodeEnum.SUCCESS.GetDescription();
                this.messageCode = messageCode.GetDescription();
                this.message = messageCode.GetMessage();
                this.dataObject = dataObject;
                if (placeHolder == PlaceHoldersEnum.CompleteMessage && placeholdermessage != string.Empty)
                {
                    this.message = this.message = placeholdermessage;

                }
                else if (placeHolder != PlaceHoldersEnum.Default)
                {
                    string _msgCode = placeholdermessage.Replace(" ","_");
                    this.messageCode = this.messageCode + "_" + _msgCode;
                    this.message = this.message.Replace(placeHolder.GetDescription(), placeholdermessage);
                }
            }
            else if (messageCode == MessageCodeEnum.EDIT_FAIL)
            {
                this.statusCode = StatusCodeEnum.CUSTOM_ERROR.GetDescription();
                this.messageCode = messageCode.GetDescription();
                this.message = messageCode.GetMessage();
                this.dataObject = dataObject;
                if (placeHolder == PlaceHoldersEnum.CompleteMessage && placeholdermessage != string.Empty)
                {
                    this.message = placeholdermessage;
                }
                else if (placeHolder != PlaceHoldersEnum.Default)
                {
                    string _msgCode = placeholdermessage.Replace(" ", "_");
                    this.messageCode = this.messageCode + "_" + _msgCode;
                    this.message = placeholdermessage;
                }
            }
            else if (messageCode == MessageCodeEnum.DELETE_SUCCESS)
            {
                this.statusCode = StatusCodeEnum.SUCCESS.GetDescription();
                this.messageCode = messageCode.GetDescription();
                this.message = messageCode.GetMessage();
                this.dataObject = dataObject;
                if (placeHolder == PlaceHoldersEnum.CompleteMessage && placeholdermessage != string.Empty)
                {
                    this.message = this.message = placeholdermessage;

                }
                else if (placeHolder != PlaceHoldersEnum.Default)
                {
                    string _msgCode = placeholdermessage.Replace(" ", "_");
                    this.messageCode = this.messageCode + "_" + _msgCode;
                    this.message = this.message.Replace(placeHolder.GetDescription(), placeholdermessage);
                }
            }
            else if (messageCode == MessageCodeEnum.FileAlreadyExist)
            {
                this.statusCode = StatusCodeEnum.CUSTOM_ERROR.GetDescription();
                this.messageCode = messageCode.GetDescription();
                this.message = messageCode.GetMessage();
                this.dataObject = dataObject;
            }
            else if (messageCode == MessageCodeEnum.SELECT_SUCCESS_IE_LOGS_API)
            {
                this.statusCode = StatusCodeEnum.SUCCESS_IE_LOGS_API.GetDescription();
                this.messageCode = messageCode.GetDescription();
                this.message = messageCode.GetMessage();
                this.dataObject = dataObject;

            }
            else if (messageCode == MessageCodeEnum.CONFIG_ALREADY_EXIST)
            {
                this.statusCode = StatusCodeEnum.CUSTOM_ERROR.GetDescription();
                this.messageCode = messageCode.GetDescription();
                this.message = messageCode.GetMessage();
                this.dataObject = dataObject;

            }
            else
            {
                this.statusCode = StatusCodeEnum.SUCCESS.GetDescription();
                this.messageCode = messageCode.GetDescription();
                this.message = messageCode.GetMessage();
                this.dataObject = dataObject;

            }
        }

       
    }
}
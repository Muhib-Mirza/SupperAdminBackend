using NLog;
using NLog.Layouts;
using NLog.Targets;
using System;
using Microsoft.Extensions.Configuration;

namespace LMS.API.Logging
{
    public  static class Logger
    {
        static IConfiguration Configuration;
        static ILogger _logger;

        // Method to set the logger instance
        public static void SetLogger(ILogger logger)
        {
            _logger = logger;
        }

        public static void LogException(Exception ex)
        {
            try
            {

                _logger.Factory.Configuration.Variables["exceptionMessage"] = ex.Message;
                if (ex.InnerException != null)
                {
                    _logger.Factory.Configuration.Variables["innerExceptionMessage"] = ex.InnerException.Message;
                }
                else
                {
                    _logger.Factory.Configuration.Variables["innerExceptionMessage"] = string.Empty;
                }

                //LogManager.LoadConfiguration("nlog.config");
                ////var log = LogManager.GetCurrentClassLogger();
                ////LogManager.Configuration.Variables["DeploymentEnv"] = Configuration.GetSection("DISSettings")["DeploymentEnv"];
                //LogManager.Configuration.Variables["exceptionMessage"] = ex.Message; //LogManager.Configuration.Variables["variable1"] = ex.Message;
                //if (ex.InnerException != null)
                //{
                //    LogManager.Configuration.Variables["innerExceptionMessage"] = ex.InnerException.Message;
                //}
                //else
                //{
                //    LogManager.Configuration.Variables["innerExceptionMessage"] = string.Empty;

                //}
                _logger.Error(ex);
            }
            catch (Exception exx)
            {

            }
            //finally
            //{
            //    NLog.LogManager.Shutdown();
            //}
        }


        public static void LogException(Exception ex, string batchNo)
        {
            try
            {
                _logger.Factory.Configuration.Variables["batchNo"] = batchNo;
                _logger.Factory.Configuration.Variables["exceptionMessage"] = ex.Message;
                if (ex.InnerException != null)
                {
                    _logger.Factory.Configuration.Variables["innerExceptionMessage"] = ex.InnerException.Message;
                }
                else
                {
                    _logger.Factory.Configuration.Variables["innerExceptionMessage"] = string.Empty;
                }
                _logger.Warn(ex);
            }
            catch (Exception exx)
            {

            }
            //finally
            //{
            //    NLog.LogManager.Shutdown();
            //}
        }

        public static void LogException(Exception ex, string structuredData, string stepId = "1", string batchNo = "")
        {
            try
            {
                _logger.Factory.Configuration.Variables["AppName"] = "IE-API";
                _logger.Factory.Configuration.Variables["batchNo"] = batchNo;
                _logger.Factory.Configuration.Variables["stepId"] = stepId;
                _logger.Factory.Configuration.Variables["structuredData"] = structuredData;
                _logger.Factory.Configuration.Variables["messageData"] = ex.ToString();

                _logger.Warn(ex);
            }
            catch (Exception exx)
            {

            }
            //finally
            //{
            //    NLog.LogManager.Shutdown();
            //}

            //log.Debug("AnceraInputRepository: FileStatus : Start");
            //log.Debug("AnceraInputRepository: FileStatus : End");
        }


        public static void LogInformation(string message)
        {
            try
            {
                _logger.Factory.Configuration.Variables["exceptionMessage"] = message;
                _logger.Factory.Configuration.Variables["innerExceptionMessage"] = string.Empty;
                _logger.Error(message);
            }
            catch (Exception ex)
            {

            }
            //finally
            //{
            //    NLog.LogManager.Shutdown();
            //}

        }
        public static void LogError(string message)
        {
            try
            {
                _logger.Factory.Configuration.Variables["messageData"] = message;
                _logger.Factory.Configuration.Variables["exceptionMessage"] = message;
                _logger.Factory.Configuration.Variables["innerExceptionMessage"] = string.Empty;
                _logger.Error(message);
            }
            catch (Exception ex)
            {

            }
        }
        public static void LogWarning(string message)
        {
            try
            {
                _logger.Factory.Configuration.Variables["messageData"] = message;
                _logger.Factory.Configuration.Variables["exceptionMessage"] = string.Empty;
                _logger.Factory.Configuration.Variables["innerExceptionMessage"] = string.Empty;
                _logger.Warn(message);
            }
            catch (Exception ex)
            {

            }
            //finally
            //{
            //    NLog.LogManager.Shutdown();
            //}
        }

        public static void LogServerActivity(string message)
        {
            try
            {
                _logger.Factory.Configuration.Variables["AppName"] = "IE-API";
                _logger.Factory.Configuration.Variables["stepId"] = "";
                _logger.Factory.Configuration.Variables["messageData"] = message;
                _logger.Info(message);
            }
            catch (Exception ex)
            {

            }
            //finally
            //{
            //    NLog.LogManager.Shutdown();
            //}

        }

        public static void LogServerActivity(string message, string structuredData, string stepId = "")
        {
            try
            {
                _logger.Factory.Configuration.Variables["AppName"] = "IE-API";
                _logger.Factory.Configuration.Variables["stepId"] = stepId;
                _logger.Factory.Configuration.Variables["structuredData"] = structuredData;
                _logger.Factory.Configuration.Variables["messageData"] = message;
                _logger.Info(message);
            }
            catch (Exception ex)
            {

            }
            //finally
            //{
            //    NLog.LogManager.Shutdown();
            //}

        }

        //public static void LogResponseActivityOnConsole(string message,string actionName,string structuredData, string logLevel,string correlationId="",string stepId="1")
        //{
        //    if (Configuration.GetSection("DISSettings")["DeploymentEnv"] == "HIPIR")
        //    {
        //        var envVar = "";
        //        try
        //        {
        //            envVar = Environment.GetEnvironmentVariable("SYSID");
        //        } catch (Exception e)
        //        {
        //            envVar = "1";
        //        }

        //        if (logLevel == "Error")
        //        {
        //            Console.Error.Write(System.Net.Dns.GetHostName() + "-DISAPI-" +actionName + " : " + envVar + " - " +  logLevel + " - " + correlationId +  " - Message: " + message +" => "+ stepId + " => " + structuredData);
        //        }
        //        else
        //        {
        //            Console.WriteLine(System.Net.Dns.GetHostName() + "-DISAPI-" + actionName + " : " + envVar + " - " + logLevel + " - " + correlationId + " - Message: " + message + " => " + stepId + " => " + structuredData);
        //        }

        //    }
        //}




        public static void LogIngestion(string message, string TypeCode, string GPU)
        {
            try
            {
                _logger.Factory.Configuration.Variables["variable1"] = message;
                _logger.Factory.Configuration.Variables["variable2"] = TypeCode;
                _logger.Factory.Configuration.Variables["variable3"] = GPU;
                _logger.Trace(message);
            }
            catch (Exception ex)
            {

            }
            //finally
            //{
            //    NLog.LogManager.Shutdown();
            //}

        }

        private static void LogRequestTracking(Exception ex)
        {

            // LogManager.LoadConfiguration("nlog.config");
            //var log = LogManager.GetCurrentClassLogger();
            //log.
            //log.FatalException("Exception", ex);

            //log.Debug("AnceraInputRepository: FileStatus : Start");
            //log.Debug("AnceraInputRepository: FileStatus : End");
        }


    }
}

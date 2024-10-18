using LMS.API.Extensions.DataModel;
using LMS.API.Extensions.Enums;
using LMS.API.Extensions.Service;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using static Dapper.SqlMapper;

namespace LMS.API.DataManagement
{
    public static class QueryExecuter
    {

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {

            return source?.IndexOf(toCheck, comp) >= 0;
        }

        static QueryExecuter()
        {
        }

        private static List<string> getListOfTablesEligibleForTenantFilter(IConfiguration config)
        {
            return config.GetSection("tables").Get<List<string>>();
            //return UserSessionBean.lstOfTablesCompulsoryforTenantFilter;
        }

        private static List<string> getTableInQuery(IDbConnection cnn, IDbTransaction transaction = null, string sql = "")
        {
            //get table names from database
            List<String> tables = cnn.Query<string>("show Tables", null, transaction).ToList();

            List<String> parsedQuery = sql.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<string> usedTables = parsedQuery.Select(x => x).Where(x => tables.Select(t => t.ToLower()).Contains(x.ToLower())).ToList();

            return usedTables;
        }

        //private static Boolean validate(IConfiguration config)
        //{


        //    return true;
        //}

        private static Boolean doQueryHaveValidTenantFilter(IConfiguration config, string sql)
        {


            return true;
        }


        /// <summary>
        /// This Method will Execute the Query and Return The First of Default Value of Type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cnn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static T APLQuerySelectSingle<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null,
            DataManagementProperties dataManagementProperties = null, bool skipQueryValidation = false)
        {

            Boolean implementQueryValidation = dataManagementProperties.config.GetSection("AppSettings").GetValue<bool>("implementQueryValidation");

            if (implementQueryValidation)
            {
                if (!QueryValidator.ValidateSelect(sql, param))
                {
                    ExceptionHandler.RaiseException(ExceptionTypeEnum.Database, ExceptionMessagesEnum.SQL_SELECT_QUERY_NOT_VALID);
                }
            }
            if (dataManagementProperties == null)
            {
                ExceptionHandler.RaiseException(ExceptionTypeEnum.Business, ExceptionMessagesEnum.CONFIGURATION_NULL);
            }
            
            bool result = true;
            if (!skipQueryValidation)
            {
                result = doQueryHaveValidTenantFilter(dataManagementProperties.config, sql);
            }
            if (!result)
            {
                ExceptionHandler.RaiseException(ExceptionTypeEnum.Database, ExceptionMessagesEnum.TENANT_PARAMETER_NEEDED);
            }

            //string result = validateQueryForTenantFilter(dataManagementProperties.config,sql);


            //sql = addTenantWhereClause(cnn, sql, transaction);




            IEnumerable<T> returnedList = cnn.Query<T>(sql, param, transaction);
            return returnedList.FirstOrDefault();
        }

        /// <summary>
        /// This Method will Execute the Query and Return all the result Type IEnumerable<T>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cnn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static IEnumerable<T> APLQuerySelectAll<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, DataManagementProperties dataManagementProperties = null)
        {

            //string a = UserSessionBean.isSuperTenant.ToString();
            //Boolean implementQueryValidation = dataManagementProperties.config.GetSection("AppSettings").GetValue<bool>("implementQueryValidation");

            //if (implementQueryValidation)
            //{
            //    if (!QueryValidator.ValidateSelect(sql, param))
            //    {
            //        ExceptionHandler.RaiseException(ExceptionTypeEnum.Database, ExceptionMessagesEnum.SQL_SELECT_QUERY_NOT_VALID);
            //    }
            //}
            //if (dataManagementProperties == null)
            //{
            //    ExceptionHandler.RaiseException(ExceptionTypeEnum.Business, ExceptionMessagesEnum.CONFIGURATION_NULL);
            //}


            //Boolean result = doQueryHaveValidTenantFilter(dataManagementProperties.config, sql);
            //if (!result)
            //{
            //    ExceptionHandler.RaiseException(ExceptionTypeEnum.Database, ExceptionMessagesEnum.TENANT_PARAMETER_NEEDED);
            //}

            IEnumerable<T> returnedList = cnn.Query<T>(sql, param, transaction);
            return returnedList;
        }


        /// <summary>
        /// This Method will Execute the MarkDelete Query.
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns> The number of rows affected.</returns>
        public static int APLQueryDeleteSingle(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, DataManagementProperties dataManagementProperties = null)
        {
            Boolean implementQueryValidation = dataManagementProperties.config.GetSection("AppSettings").GetValue<bool>("implementQueryValidation");

            if (implementQueryValidation)
            {
                if (!QueryValidator.ValidateDelete(sql, param))
                {
                    ExceptionHandler.RaiseException(ExceptionTypeEnum.Database, ExceptionMessagesEnum.SQL_DELETE_QUERY_NOT_VALID);
                }
            }
            if (dataManagementProperties == null)
            {
                ExceptionHandler.RaiseException(ExceptionTypeEnum.Business, ExceptionMessagesEnum.CONFIGURATION_NULL);
            }


            return cnn.Execute(sql, param, transaction);
        }


        /// <summary>
        /// This Method will Execute the MarkDelete Query for Many Records.
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns> The number of rows affected.</returns>
        public static int APLQueryDeleteAll(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, DataManagementProperties dataManagementProperties = null)
        {
            Boolean implementQueryValidation = dataManagementProperties.config.GetSection("AppSettings").GetValue<bool>("implementQueryValidation");

            if (implementQueryValidation)
            {
                if (!QueryValidator.ValidateDelete(sql, param))
                {
                    ExceptionHandler.RaiseException(ExceptionTypeEnum.Database, ExceptionMessagesEnum.SQL_DELETE_QUERY_NOT_VALID);
                }
            }
            if (dataManagementProperties == null)
            {
                ExceptionHandler.RaiseException(ExceptionTypeEnum.Business, ExceptionMessagesEnum.CONFIGURATION_NULL);
            }

            //Boolean result = doQueryHaveValidTenantFilter(dataManagementProperties.config, sql);
            //if (!result)
            //{
            //    ExceptionHandler.RaiseException(ExceptionTypeEnum.Database, ExceptionMessagesEnum.TENANT_PARAMETER_NEEDED);
            //}

            return cnn.Execute(sql, param, transaction);
        }


        /// <summary>
        /// This Method will Execute the Insert Query and Return The First of Default Value of Type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cnn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static long APLQueryInsertSingle<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, DataManagementProperties dataManagementProperties = null)
        {
            Boolean implementQueryValidation = dataManagementProperties.config.GetSection("AppSettings").GetValue<bool>("implementQueryValidation");

            if (implementQueryValidation)
            {
                if (!QueryValidator.ValidateInsert(sql, param))
                {
                    ExceptionHandler.RaiseException(ExceptionTypeEnum.Database, ExceptionMessagesEnum.SQL_Insert_QUERY_NOT_VALID);
                }
            }
            if (dataManagementProperties == null)
            {
                ExceptionHandler.RaiseException(ExceptionTypeEnum.Business, ExceptionMessagesEnum.CONFIGURATION_NULL);
            }

          
            

            return cnn.Query<long>(sql, param, transaction).FirstOrDefault();

        }
        public static long APLQueryInsertSingle<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, DataManagementProperties dataManagementProperties = null, int commandTimeOut = 300)
        {

            if (dataManagementProperties == null)
            {
                ExceptionHandler.RaiseException(ExceptionTypeEnum.Business, ExceptionMessagesEnum.CONFIGURATION_NULL);
            }

            Boolean result = doQueryHaveValidTenantFilter(dataManagementProperties.config, sql);
            if (!result)
            {
                ExceptionHandler.RaiseException(ExceptionTypeEnum.Database, ExceptionMessagesEnum.TENANT_PARAMETER_NEEDED);
            }

            return cnn.Query<long>(sql, param, transaction, commandTimeout: commandTimeOut).FirstOrDefault();

        }




        /// <summary>
        /// This Method will Execute the Insert Query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cnn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns>The number of rows affected.</returns>
        public static int APLQueryInsertAll<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, DataManagementProperties dataManagementProperties = null)
        {
            Boolean implementQueryValidation = dataManagementProperties.config.GetSection("AppSettings").GetValue<bool>("implementQueryValidation");

            if (implementQueryValidation)
            {
                if (!QueryValidator.ValidateInsert(sql, param))
                {
                    ExceptionHandler.RaiseException(ExceptionTypeEnum.Database, ExceptionMessagesEnum.SQL_Insert_QUERY_NOT_VALID);
                }
            }
            if (dataManagementProperties == null)
            {
                ExceptionHandler.RaiseException(ExceptionTypeEnum.Business, ExceptionMessagesEnum.CONFIGURATION_NULL);
            }

            return cnn.Execute(sql, param, transaction);
        }

        /// <summary>
        /// This Method will Execute the Insert Query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cnn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns>The number of rows affected.</returns>
        public static int APLQueryInsertAllSP<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, DataManagementProperties dataManagementProperties = null)
        {
            Boolean implementQueryValidation = dataManagementProperties.config.GetSection("AppSettings").GetValue<bool>("implementQueryValidation");

            if (implementQueryValidation)
            {
                if (!QueryValidator.ValidateInsert(sql, param))
                {
                    ExceptionHandler.RaiseException(ExceptionTypeEnum.Database, ExceptionMessagesEnum.SQL_Insert_QUERY_NOT_VALID);
                }
            }
            if (dataManagementProperties == null)
            {
                ExceptionHandler.RaiseException(ExceptionTypeEnum.Business, ExceptionMessagesEnum.CONFIGURATION_NULL);
            }

            Boolean result = doQueryHaveValidTenantFilter(dataManagementProperties.config, sql);
            if (!result)
            {
                ExceptionHandler.RaiseException(ExceptionTypeEnum.Database, ExceptionMessagesEnum.TENANT_PARAMETER_NEEDED);
            }

            return cnn.Execute(sql, param, transaction, commandType: CommandType.StoredProcedure);
        }
        public static IEnumerable<T> APLQueryAllSP<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, DataManagementProperties dataManagementProperties = null)
        {
            if (dataManagementProperties == null)
            {
                ExceptionHandler.RaiseException(ExceptionTypeEnum.Business, ExceptionMessagesEnum.CONFIGURATION_NULL);
            }
            return cnn.Query<T>(sql, param, transaction, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// This Method will Execute the Update Query and Return The First of Default Value of Type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cnn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static int APLQueryUpdateSingle<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, DataManagementProperties dataManagementProperties = null)
        {
            Boolean implementQueryValidation = dataManagementProperties.config.GetSection("AppSettings").GetValue<bool>("implementQueryValidation");

            if (implementQueryValidation)
            {
                if (!QueryValidator.ValidateUpdate(sql, param))
                {
                    ExceptionHandler.RaiseException(ExceptionTypeEnum.Database, ExceptionMessagesEnum.SQL_UPDATE_QUERY_NOT_VALID);
                }
            }
            if (dataManagementProperties == null)
            {
                ExceptionHandler.RaiseException(ExceptionTypeEnum.Business, ExceptionMessagesEnum.CONFIGURATION_NULL);
            }

            //Boolean result = doQueryHaveValidTenantFilter(dataManagementProperties.config, sql);
            //if (!result)
            //{
            //    ExceptionHandler.RaiseException(ExceptionTypeEnum.Database, ExceptionMessagesEnum.TENANT_PARAMETER_NEEDED);
            //}

            return cnn.Execute(sql, param, transaction);
        }

        /// <summary>
        /// This Method will Execute the Update Query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cnn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns>The number of rows affected.</returns>
        public static int APLQueryUpdateAll<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, DataManagementProperties dataManagementProperties = null)
        {
            Boolean implementQueryValidation = Convert.ToBoolean(dataManagementProperties.config.GetSection("AppSettings").GetValue<bool>("implementQueryValidation"));

            if (implementQueryValidation)
            {
                if (!QueryValidator.ValidateUpdate(sql, param))
                {
                    ExceptionHandler.RaiseException(ExceptionTypeEnum.Database, ExceptionMessagesEnum.SQL_UPDATE_QUERY_NOT_VALID);
                }
            }
            if (dataManagementProperties == null)
            {
                ExceptionHandler.RaiseException(ExceptionTypeEnum.Business, ExceptionMessagesEnum.CONFIGURATION_NULL);
            }

            //Boolean result = doQueryHaveValidTenantFilter(dataManagementProperties.config, sql);
            //if (!result)
            //{
            //    ExceptionHandler.RaiseException(ExceptionTypeEnum.Database, ExceptionMessagesEnum.TENANT_PARAMETER_NEEDED);
            //}

            return cnn.Execute(sql, param, transaction);
        }

        /// <summary>
        /// This Method will Execute the Query for Report and Return all the result Type IEnumerable<T>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cnn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="dataManagementProperties"></param>
        /// <returns></returns>
        //public static IEnumerable<T> APLQuerySelectReportData<T>(this IDbConnection cnn, string sql, object param = null, DataManagementProperties dataManagementProperties = null, DataManagementProperties dataManagementProperties1 = null)
        /// 
        public static IEnumerable<T> APLQuerySelectReportData<T>(this IDbConnection cnn, string sql, object param = null, DataManagementProperties dataManagementProperties = null)
        {
            if (dataManagementProperties == null)
            {
                ExceptionHandler.RaiseException(ExceptionTypeEnum.Business, ExceptionMessagesEnum.CONFIGURATION_NULL);
            }

            IEnumerable<T> returnedList = cnn.Query<T>(sql, param);
            return returnedList;
        }


    }
}

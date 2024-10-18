using LMS.API.Extensions.DataModel;
using LMS.API.Extensions.Enums;
using LMS.API.Extensions.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMS.API.DataManagement
{
    public class QueryValidator
    {
       

        /// <summary>
        /// This method will confirm if the query is correct for Select Sql Statement.
        /// </summary>
        /// <returns></returns>
        public static bool ValidateSelect(string SQLQuery, object param)
        {
            List<String> parsedQuery = SQLQuery.Trim().Replace(",", " , ").Replace("=", " = ").Replace("\r\n", " ").Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            bool haveSelectWord = parsedQuery.Any(x => x.ToLower() == "select");
            bool haveDeleteWord = parsedQuery.Any(x => x.ToLower() == "delete");
            bool haveUpdateWord = parsedQuery.Any(x => x.ToLower() == "update");
            bool haveInsertWord = parsedQuery.Any(x => x.ToLower() == "insert");

            if (!haveSelectWord)
            {
                return false;
            }

            if (haveUpdateWord || haveDeleteWord || haveInsertWord)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// This method will confirm if the query is correct for Insert Sql Statement.
        /// </summary>
        /// <returns></returns>
        public static bool ValidateInsert(string SQLQuery, object param)
        {
            List<String> parsedQuery = SQLQuery.Trim().Replace(",", " , ").Replace("=", " = ").Replace("\r\n", " ").Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            bool haveSelectWord = parsedQuery.Any(x => x.ToLower() == "select");
            bool haveDeleteWord = parsedQuery.Any(x => x.ToLower() == "delete");
            bool haveUpdateWord = parsedQuery.Any(x => x.ToLower() == "update");
            bool haveInsertWord = parsedQuery.Any(x => x.ToLower() == "insert");

            //LAST_INSERT_ID

            if (haveSelectWord)
            {
                if (parsedQuery.Any(x => x.ToLower().Contains("LAST_INSERT_ID()".ToString().ToLower())))
                {
                    haveSelectWord = false;
                }
            }

            if (!haveInsertWord)
            {
                return false;
            }

            if (haveUpdateWord || haveDeleteWord || haveSelectWord)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// This method will confirm if the query is correct for Select Sql Statement.
        /// </summary>
        /// <returns></returns>
        public static bool ValidateUpdate(string SQLQuery, object param)
        {
            List<String> parsedQuery = SQLQuery.Trim().Replace(",", " , ").Replace("=", " = ").Replace("\r\n", " ").Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            bool haveSelectWord = parsedQuery.Any(x => x.ToLower() == "select");
            bool haveDeleteWord = parsedQuery.Any(x => x.ToLower() == "delete");
            bool haveUpdateWord = parsedQuery.Any(x => x.ToLower() == "update");
            bool haveInsertWord = parsedQuery.Any(x => x.ToLower() == "insert");

            if (!haveUpdateWord)
            {
                return false;
            }

            if (haveSelectWord || haveDeleteWord || haveInsertWord)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// This method will confirm if the query is correct for Select Sql Statement.
        /// </summary>
        /// <returns></returns>
        public static bool ValidateDelete(string SQLQuery, object param)
        {
            List<String> parsedQuery = SQLQuery.Trim().Replace(",", " , ").Replace("=", " = ").Replace("\r\n", " ").Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            bool haveSelectWord = parsedQuery.Any(x => x.ToLower() == "select");
            bool haveDeleteWord = parsedQuery.Any(x => x.ToLower() == "delete");
            bool haveUpdateWord = parsedQuery.Any(x => x.ToLower() == "update");
            bool haveInsertWord = parsedQuery.Any(x => x.ToLower() == "insert");

            if (!haveUpdateWord)
            {
                return false;
            }

            if (haveSelectWord || haveDeleteWord || haveInsertWord)
            {
                return false;
            }

            return true;
        }
    }
}

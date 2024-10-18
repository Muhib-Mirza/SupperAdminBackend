
using LMS.API.Extensions.DataModel;
using LMS.API.Extensions.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ancera.API.DataManagement
{
    public class QueryBuilder
    {

        /// <summary>
        /// This method will return sql statement of tenant filter as string.
        /// </summary>
        /// <returns></returns>


        //public static string getTenantFilterStatement(string alias = "", Boolean addIsActive = false, Boolean addUserFilter = false, Boolean addIsDeleted = false)
        //{

        //    string tenantFilter = " tenantId in @tenantIds ";

        //    if (alias != "")
        //    {
        //        tenantFilter = " " + alias + ".tenantId in @tenantIds ";
        //    }

        //    if (UserSessionBean.isSuperTenant && UserSessionBean.isSuperUser)
        //    {
        //        if (alias != "")
        //        {
        //            tenantFilter = " " + alias + ".tenantId = " + alias + ".tenantId ";
        //        }
        //        else
        //        {
        //            tenantFilter = " tenantId = tenantId ";
        //        }
        //    }

        //    string filter = @"";

        //    if (alias != "")
        //    {
        //        filter = " and " + tenantFilter + " ";
        //        if (addIsActive)
        //        {
        //            filter += " and " + alias + ".IsActive = 1 ";
        //        }
        //        if (addIsDeleted)
        //        {
        //            filter += " and " + alias + ".isDeleted = @isDeleted ";
        //        }
        //        if (addUserFilter)
        //        {
        //            filter += " and " + alias + ".userId = @userId ";
        //        }
        //    }
        //    else
        //    {
        //        filter = " and " + tenantFilter + " ";
        //        if (addIsActive)
        //        {
        //            filter += " and IsActive = 1 ";
        //        }
        //        if (addIsDeleted)
        //        {
        //            filter += " and isDeleted = @isDeleted ";
        //        }
        //        if (addUserFilter)
        //        {
        //            filter += " and userId = @userId ";
        //        }
        //    }
        //    return filter;
        //}


        //public static string tenantFilterAsString = "alias.{tenantFilter}";

        private const string sqlAnd = "And";
        private const string equalsTO = "=";
        private const string space = " ";


        public static string AppendMultiSelect(params string[] queries)
        {
            string completeQuery = "";
            for (int i = 0; i < queries.Length; i++)
            {
                completeQuery = completeQuery + queries[i] + ";";
            }
            return completeQuery;
        }

        public static string AppendEnumValue(string query, string Field, LookUpGroupEnum LookupVal)
        {

            string completeQuery = query + sqlAnd + space + Field + equalsTO + LookupVal.GetDescription();

            return completeQuery;
        }

        public static string AppendEnumValueForUserOrgan(string query, string Field, LookUpGroupEnum LookupVal, int organTypeID)
        {

            string completeQuery = query + sqlAnd + space + Field + equalsTO + LookupVal.GetDescription();

            return completeQuery;
        }

        public static string getTenantFilterStatement(string v, bool v1)
        {
            throw new NotImplementedException();
        }

        public static string getTenantFilterStatement(string v)
        {
            throw new NotImplementedException();
        }
    }
}

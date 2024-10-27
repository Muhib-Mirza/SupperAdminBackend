using System.Data;
using System.Net;
using System.Security.Cryptography;
using System.Text;

using Ancera.API.DataManagement;

using APL.API.Extensions.Repository;
using APL.API.Extensions.Service.GenericRequestObjects;

using Dapper;

using LMS.API.DataManagement;
using LMS.API.DataModel;
using LMS.API.DataModel.DataModel;
using LMS.API.DataModel.ViewModel;
using LMS.API.Extensions.Enums;
using LMS.API.Extensions.Service;
using LMS.API.ViewModel;
using LMS.Repository.Business.Interfaces;

using Microsoft.Extensions.Configuration;


namespace LMS.Repository.Business
{
    public class UserRepository : IUserRepository
    {
        private IConfiguration _config;

        public UserRepository(IConfiguration config)
        {
            _config = config;
            //qv = new QueryValidator2();
        }

        string SQL_Insert_Asses()
        {
            return @"Insert into UserPersonality (
                   UserID,
                   PersonalityTypeID
                   ) VALUES (
                   @UserId,
                   (Select PersonalityTypeID from PersonalityTypes where Code = @Code)
                   )";
        }
        string SQL_INSERT_ROW()
        {
            return @" INSERT INTO Users (
                user_name, 
                phone_number, 
                email_id, 
                password, 
                created_on,
                isAssesed
            ) VALUES (
                @UserName, 
                @PhoneNumber, 
                @EmailId, 
                @Password, 
                NOW(),
                0
            );

            " +
           "SELECT LAST_INSERT_ID();";
        }
        string SQL_SELECT_BY_EMAIL__2()
        {
            return @" SELECT 
                user_id AS UserId,
                isAssesed
            FROM 
                Users
            WHERE 
                email_id = @EmailId AND 
                password = @Password;
            ";
        }
        private readonly string SQL_UPDATE_TOKEN_BY_SESSIONID = @"UPDATE tokenDetails SET token=@token 
                                                                 WHERE actvSessionId = @actvSessionId ";
        private readonly string SQL_UPDATE_ISACTIVE_BY_ACTIVESESSIONID = "UPDATE active_user_session SET " +
                "isActive = 0, isDeleted = 1, " +
                "sesnId = @sesnid, " +
                "modifiedSesnId = @modifiedsesnid, modifiedOn = @modifiedon " +
                "where actvSessionId =   (SELECT actvSessionId from tokenDetails WHERE token=@token)  ";
        private readonly string SQL_SELECT_TOKEN_BY_SESSIONID = "SELECT * from tokenDetails WHERE actvSessionId = @actvSessionId";

        public UserDM Add(UserModel input)
        {
            UserDM resultObj = new UserDM();
            using (IDbConnection conn = DBMSConnection.GetConnection(_config))
            {
                DataManagementProperties dataManagementProperties = new DataManagementProperties();
                dataManagementProperties.config = _config;


                conn.Open();

                using (var transaction = conn.BeginTransaction())
                {
                    //MEMO: trim Email for Further comparison of existing user.
                    input.EmailId = input.EmailId.Trim().ToLower();
                    //formObjClientSiteAssDM(ref input, input.userId, input.lstClientIDS, conn, transaction, dataManagementProperties);
                    UserDM userDMInput = new UserDM(input);




                    // getTenant Id of current selected organization 


                    long insertedId = Add(userDMInput, conn, transaction, dataManagementProperties, 20, 20);

                    resultObj.ID = insertedId;
                    //InsertAllClientSites(input.orgSites, insertedId, conn, transaction);
                    transaction.Commit();
                }

            }
            return resultObj;

        }
        private long Add(UserDM input, IDbConnection conn, IDbTransaction transaction, DataManagementProperties dataManagementProperties, int expiryMinutes, int expiryMinutesPIN)
        {
            long resultObj = new long();
            resultObj = QueryExecuter.APLQueryInsertSingle<long>(conn, SQL_INSERT_ROW(), input, transaction, dataManagementProperties);
            return resultObj;
        }
        public UserDM Login(AuthenticationObject authenticationObject)
        {
            DataManagementProperties dataManagementProperties = new DataManagementProperties();
            dataManagementProperties.config = _config;



            using (IDbConnection conn = DBMSConnection.GetConnection(_config))
            {
                conn.Open();
               return  QueryExecuter.APLQuerySelectSingle<UserDM>(conn, SQL_SELECT_BY_EMAIL__2(), authenticationObject, null, dataManagementProperties);

               

            }

            return null;
        }
        public TokenDetailsDM UpdateTokenDetails(TokenDetailsDM input)
        {
            DataManagementProperties dataManagementProperties = new DataManagementProperties();
            dataManagementProperties.config = _config;

            using (IDbConnection conn = DBMSConnection.GetConnection(_config))
            {
                conn.Open();

                using (IDbTransaction transaction = conn.BeginTransaction())
                {

                    QueryExecuter.APLQueryUpdateSingle<long>(conn, SQL_UPDATE_TOKEN_BY_SESSIONID, input, transaction, dataManagementProperties);

                    transaction.Commit();
                }
            }
            return input;
        }
        public TokenDetailsDM LogOut(TokenDetailsDM input)
        {
            DataManagementProperties dataManagementProperties = new DataManagementProperties();
            dataManagementProperties.config = _config;



            using (IDbConnection conn = DBMSConnection.GetConnection(_config))
            {
                conn.Open();



                using (IDbTransaction transaction = conn.BeginTransaction())
                {
                    Console.WriteLine(input.token);
                    var abc = input.token;
                    QueryExecuter.APLQueryUpdateSingle<long>(conn, SQL_UPDATE_ISACTIVE_BY_ACTIVESESSIONID, input, transaction, dataManagementProperties);
                    transaction.Commit();
                }
            }




            return input;
        }
        public TokenDetailsDM GetTokenDetailsBySessionID(TokenDetailsDM input)
        {
            DataManagementProperties dataManagementProperties = new DataManagementProperties();
            dataManagementProperties.config = _config;
            using (IDbConnection conn = DBMSConnection.GetConnection(_config))
            {
                conn.Open();
                TokenDetailsDM result = QueryExecuter.APLQuerySelectSingle<TokenDetailsDM>(conn, SQL_SELECT_TOKEN_BY_SESSIONID, input, null, dataManagementProperties);
                return result;
            }
        }
        public bool AssesMe(AssesmentModel Obj)
        {
            bool resultObj = true;
            using (IDbConnection conn = DBMSConnection.GetConnection(_config))
            {
                DataManagementProperties dataManagementProperties = new DataManagementProperties();
                dataManagementProperties.config = _config;


                conn.Open();

                using (var transaction = conn.BeginTransaction())
                {
                    //MEMO: trim Email for Further comparison of existing user.
                    //formObjClientSiteAssDM(ref input, input.userId, input.lstClientIDS, conn, transaction, dataManagementProperties);
                    AssesDM userDMInput = new AssesDM(Obj);
                    // getTenant Id of current selected organization 
                    bool insertedId = AddAssesMe(userDMInput, conn, transaction, dataManagementProperties, 20, 20);
                    transaction.Commit();
                }

            }
            return true;
        }
        private bool AddAssesMe(AssesDM input, IDbConnection conn, IDbTransaction transaction, DataManagementProperties dataManagementProperties, int expiryMinutes, int expiryMinutesPIN)
        {
            bool result = false;
            try
            {
                QueryExecuter.APLQueryInsertSingle<long>(conn, SQL_Insert_Asses(), input, transaction, dataManagementProperties);
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

    }
}





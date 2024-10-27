using APL.API.Extensions.Repository;
using APL.API.Extensions.Service.GenericRequestObjects;

using LMS.API.DataModel;
using LMS.API.DataModel.ViewModel;
using LMS.API.ViewModel;

namespace LMS.Repository.Business.Interfaces
{
    public interface IUserRepository 
    {
        UserDM Add(UserModel input);
        UserDM Login(AuthenticationObject authenticationObject);
        TokenDetailsDM GetTokenDetailsBySessionID(TokenDetailsDM input);
        TokenDetailsDM UpdateTokenDetails(TokenDetailsDM input);
        TokenDetailsDM LogOut(TokenDetailsDM input);
        bool AssesMe(AssesmentModel Obj);
    }
}

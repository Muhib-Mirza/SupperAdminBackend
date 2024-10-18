using APL.API.Extensions.DataModel;

using LMS.API.DataModel.ViewModel;

namespace LMS.API.ViewModel
{
    public class UserVM : BaseViewModel, IModel
    {


        public UserVM()
        {

        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; } // Make sure this is hashed before storing
        public bool Extrovert { get; set; }
        public bool Sensing { get; set; }
        public bool Thinker { get; set; }
        public bool Judger { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; } // Nullable in case it's not provided initially
        public int? ModifiedBy { get; set; }


    }
}
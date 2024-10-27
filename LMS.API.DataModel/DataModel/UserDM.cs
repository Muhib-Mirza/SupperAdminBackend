using APL.API.Extensions.DataModel;
using LMS.API.DataModel.ViewModel;
using LMS.API.Extensions.DataModel;
using LMS.API.ViewModel;


namespace LMS.API.DataModel
{
    public partial class UserDM : BaseDataModel, IModel
    {

        // Properties corresponding to your database columns
        public long ID { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public bool isAssesed { get; set; }
        public DateTime CreatedOn { get; set; }

        // Constructor to initialize UserDM using a UserVM object
        public UserDM(UserModel userVM)
        {
            // Copying properties from the VM to the DM
            this.UserName = userVM.UserName;
            this.PhoneNumber = userVM.PhoneNumber;
            this.EmailId = userVM.EmailId;
            this.Password = userVM.Password;
            this.CreatedOn = userVM.CreatedOn;
        }

        // Optional: Parameterless constructor
        public UserDM() { }

    }
}

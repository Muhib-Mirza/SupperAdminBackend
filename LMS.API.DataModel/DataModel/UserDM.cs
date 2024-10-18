using APL.API.Extensions.DataModel;

using LMS.API.Extensions.DataModel;
using LMS.API.ViewModel;


namespace LMS.API.DataModel
{
    public partial class UserDM : BaseDataModel, IModel
    {
       
            // Properties corresponding to your database columns
            public long ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string EmailId { get; set; }
            public string Password { get; set; }
            public bool Extrovert { get; set; }
            public bool Sensing { get; set; }
            public bool Thinker { get; set; }
            public bool Judger { get; set; }
            public DateTime CreatedOn { get; set; }
            public int CreatedBy { get; set; }
            public DateTime? ModifiedOn { get; set; }
            public int? ModifiedBy { get; set; }

            // Constructor to initialize UserDM using a UserVM object
            public UserDM(UserVM userVM)
            {
                // Copying properties from the VM to the DM
                this.FirstName = userVM.FirstName;
                this.LastName = userVM.LastName;
                this.EmailId = userVM.EmailId;
                this.Password = userVM.Password;
                this.Extrovert = userVM.Extrovert;
                this.Sensing = userVM.Sensing;
                this.Thinker = userVM.Thinker;
                this.Judger = userVM.Judger;
                this.CreatedOn = userVM.CreatedOn;
                this.CreatedBy = userVM.CreatedBy;
                this.ModifiedOn = userVM.ModifiedOn;
                this.ModifiedBy = userVM.ModifiedBy;
            }

            // Optional: Parameterless constructor
            public UserDM() { }
        
    }
}

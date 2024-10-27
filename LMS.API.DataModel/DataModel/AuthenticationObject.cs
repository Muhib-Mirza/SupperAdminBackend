namespace APL.API.Extensions.Service.GenericRequestObjects
{
    public class AuthenticationObject
    {

        public AuthenticationObject() {

           

        }

       

        public long userId { get; set; }

    
        public string EmailId { get; set; }
        public string Password { get; set; }

        public bool isAssesed { get; set; }

        public string? token { get; set; }
           
  
    }
}

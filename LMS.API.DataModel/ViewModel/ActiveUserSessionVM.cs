namespace LMS.API.DataModel.ViewModel
{
    public class ActiveUserSessionVM : BaseViewModel 
    {
        public long actvSessionId { get; set; }

        public long userId { get; set; }

        public string token { get; set; }

        public string systemIp { get; set; }

        public string UserAgent { get; set; }
    }
}

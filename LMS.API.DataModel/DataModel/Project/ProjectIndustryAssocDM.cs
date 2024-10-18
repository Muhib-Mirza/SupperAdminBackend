using LMS.API.DataModel.ViewModel.Project;
using LMS.API.Extensions.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.API.DataModel.DataModel.Project
{
    public class ProjectIndustryAssocDM:BaseDataModel
    {
        public ProjectIndustryAssocDM()
        {
            
        }
        public ProjectIndustryAssocDM(ProjectIndustryAssocVM obj)
        {
            this.project_industry_id = obj.project_industry_id;
            this.project_id = obj.project_id;
            this.industry_id = obj.industry_id;
            
        }
        public int project_industry_id { get; set; }
        public long project_id { get; set; }
        public int industry_id { get; set; }
        public DateTime creation_date { get; set; }
        public DateTime modification_date { get; set; }
        public int created_by { get; set; }
        public int modified_by { get; set; }
    }
}

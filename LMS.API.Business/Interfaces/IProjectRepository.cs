using LMS.API.DataModel.DataModel.Project;
using LMS.API.DataModel.ViewModel;
using LMS.API.DataModel.ViewModel.LMS.API.ViewModel;
using LMS.API.DataModel.ViewModel.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.API.Business.Interfaces
{
    public interface IProjectRepository
    {
        IEnumerable<ProjectVMV2> GetALLProjects();
        void SaveProject(CreateProjectVM project);
        void EditProject(CreateProjectVM project);
        bool DeleteFarm(FarmDeleteVM input);
        IEnumerable<ProjectVMV2> GetProjectDetailV2(FarmDeleteVM id);
        MetaDataVM GetMedaData();
        long AddMedaData(ConfigurtionVm input);
        ProjectFiltersVM GetProjectFilters();
    }
}

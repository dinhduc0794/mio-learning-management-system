using Mio.LMS.Web.Models.ViewModels;

namespace Mio.LMS.Web.Services;

public interface ISectionService
{
    Task<ResultViewModel<List<SectionViewModel>>> GetAllSectionsAsync(int courseId);
    Task<ResultViewModel<SectionViewModel>> GetSectionByIdAsync(int id);
    Task<ResultViewModel<SectionViewModel>> AddSectionAsync(SectionViewModel model);
    Task<ResultViewModel<SectionViewModel>> UpdateSectionAsync(SectionViewModel model);
    Task<ResultViewModel<bool>> DeleteSectionAsync(int id);
}
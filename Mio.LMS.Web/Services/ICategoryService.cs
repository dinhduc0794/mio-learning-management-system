using Mio.LMS.Web.Models;

namespace Mio.LMS.Web.Services;

using Mio.LMS.Web.Models.ViewModels;

public interface ICategoryService
{
    Task<ResultViewModel<List<CategoryViewModel>>> GetAllCategoriesAsync();
    Task<ResultViewModel<CategoryViewModel>> GetCategoryByIdAsync(int id);
    Task<ResultViewModel<CategoryViewModel>> CreateCategoryAsync(CategoryViewModel viewModel);
    Task<ResultViewModel<CategoryViewModel>> UpdateCategoryAsync(CategoryViewModel viewModel);
    Task<ResultViewModel<bool>> DeleteCategoryAsync(int id);
}

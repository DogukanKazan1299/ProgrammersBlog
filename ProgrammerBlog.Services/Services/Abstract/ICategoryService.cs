using ProgrammerBlog.Entities.Concrete;
using ProgrammerBlog.Entities.Dtos;
using ProgrammerBlog.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Services.Services.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult<IList<Category>>> GetAll();
        Task<IDataResult<Category>> Get(int categoryId);
        Task<IDataResult<IList<Category>>> GetAllByNonDeleted();//silinmeyen kategorileri getir.
        Task<IResult> Add(CategoryAddDto categoryAddDto);
        Task<IResult> Update(CategoryUpdateDto categoryUpdateDto);
        Task<IResult> Delete(int categoryId, string modifiedByName);
        Task<IResult> HardDelete(int categoryId);
    }
}

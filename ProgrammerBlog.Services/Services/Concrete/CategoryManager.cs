﻿using AutoMapper;
using ProgrammerBlog.Data.Abstract;
using ProgrammerBlog.Entities.Concrete;
using ProgrammerBlog.Entities.Dtos;
using ProgrammerBlog.Services.Services.Abstract;
using ProgrammerBlog.Shared.Utilities.Results.Abstract;
using ProgrammerBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammerBlog.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Services.Services.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryManager(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IDataResult<CategoryDto>> Add(CategoryAddDto categoryAddDto,string createdByName)
        {
            var category = _mapper.Map<Category>(categoryAddDto);
            category.CreatedByName = createdByName;
            category.ModifiedByName = createdByName;
            var addedCategory = await _unitOfWork.Categories.AddAsync(category);
            //.ContinueWith(t => _unitOfWork.SaveAsync());
            await _unitOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Success, $"{categoryAddDto.Name} adlı kategori eklendi", new CategoryDto
            {
                Category = addedCategory,
                ResultStatus = ResultStatus.Success,
                Message =$"{categoryAddDto.Name} adlı kategori eklendi" 
            });
        }

        public async Task<IDataResult<CategoryDto>> Delete(int categoryId, string modifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                category.IsDeleted = true;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;
                var deletedCategory = await _unitOfWork.Categories.UpdateAsync(category);
                await _unitOfWork.SaveAsync();
                //.ContinueWith(t => _unitOfWork.SaveAsync());

                return new DataResult<CategoryDto>(ResultStatus.Success, $"{deletedCategory.Name} kategorisi silindi", new CategoryDto
                {
                    Category = deletedCategory,
                    ResultStatus = ResultStatus.Success,
                    Message = $"{deletedCategory.Name} kategorisi silindi"
                });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, $"Kategori bulunamadı", new CategoryDto
            {
                Category = null,
                ResultStatus = ResultStatus.Error,
                Message = $"Kategori bulunamadı"
            });
        }

        public async Task<IDataResult<CategoryDto>> Get(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId, c => c.Articles);
            if (category != null)
            {
                return new DataResult<CategoryDto>(ResultStatus.Success,new CategoryDto 
                {
                    Category=category,
                    ResultStatus=ResultStatus.Success
                });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error,"Kategori bulunamadı",new CategoryDto 
            {
                Category=null,
                ResultStatus=ResultStatus.Error,
                Message="Kategori Yok"
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAll()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(null, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success,new CategoryListDto 
                {
                    Categories=categories,
                    ResultStatus=ResultStatus.Success
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error,"Kategori bulunamdı",new CategoryListDto 
            {
                Categories=null,
                ResultStatus=ResultStatus.Error,
                Message="Kategori Bulunamadı"
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeleted()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => !c.IsDeleted, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Kategori bulunamdı", new CategoryListDto
            {
                Categories = null,
                ResultStatus = ResultStatus.Error,
                Message = "Kategori Bulunamadı"
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActive()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => !c.IsDeleted && c.IsActive, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories=categories,
                    ResultStatus=ResultStatus.Success
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Kategori bulunamadı", null);
        }

        public async Task<IResult> HardDelete(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                await _unitOfWork.Categories.DeleteAsync(category);
                //.ContinueWith(t => _unitOfWork.SaveAsync());
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{category.Name} adlı kategori databaseden silindi");
            }
            return new Result(ResultStatus.Error, "Kategori bulunamadı");
        }

        public async Task<IDataResult<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto,string modifiedByName)
        {
            var category = _mapper.Map<Category>(categoryUpdateDto);
            category.ModifiedByName = modifiedByName;
            var updatedCategory = await _unitOfWork.Categories.UpdateAsync(category);
            await _unitOfWork.SaveAsync();
            //.ContinueWith(t => _unitOfWork.SaveAsync());

            return new DataResult<CategoryDto>(ResultStatus.Success, $"{categoryUpdateDto.Name} kategoeisi güncellendi",new CategoryDto 
            {
                Category=updatedCategory,
                ResultStatus=ResultStatus.Success,
                Message=$"{categoryUpdateDto.Name} kategorisi güncellendi"
            });
        }
    }
}

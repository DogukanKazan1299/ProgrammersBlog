using Microsoft.Extensions.DependencyInjection;
using ProgrammerBlog.Data.Abstract;
using ProgrammerBlog.Data.Concrete;
using ProgrammerBlog.Data.Concrete.EntityFramework.Contexts;
using ProgrammerBlog.Services.Services.Abstract;
using ProgrammerBlog.Services.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<ProgrammersBlogContext>();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
            serviceCollection.AddScoped<IArticleService, ArticleManager>();
            return serviceCollection;
        }
    }
}

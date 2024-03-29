﻿using ProgrammerBlog.Entities.Concrete;
using ProgrammerBlog.Shared.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Data.Abstract
{
    public interface ICategoryRepository : IEntityRepository<Category>
    {
    }
}

﻿using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ElevenNote.Services
{
    public class CategoryService
    {
        private readonly Guid _userId;

        public CategoryService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateCategory(CategoryCreate model)
        {
            var entity =
                new Category()
                {
                    OwnerId = _userId,
                    Name = model.Name,
                    CategoryCreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Categories.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<SelectListItem> CategoriesListItems()
        {
            using (var ctx = new ApplicationDbContext())
            {
                IEnumerable<SelectListItem> catList = ctx.Categories.Select(category => new SelectListItem
                {
                    Text = category.Name,
                    Value = category.CategoryId.ToString()
                });
                return catList;
            }
        }

        public IEnumerable<CategoryListItem> GetCategories()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Categories
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new CategoryListItem
                                {
                                    CategoryId = e.CategoryId,
                                    Name = e.Name,
                                    CategoryCreatedUtc = e.CategoryCreatedUtc
                                }
                       );

                return query.ToArray();
            }
        }

        public CategoryDetail GetCategoryById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categories
                        .Single(e => e.CategoryId == id && e.OwnerId == _userId);
                return
                    new CategoryDetail
                    {
                        CategoryId = entity.CategoryId,
                        Name = entity.Name,
                        CategoryCreatedUtc = entity.CategoryCreatedUtc,
                        CategoryModifiedUtc = entity.CategoryModifiedUtc
                    };
            }
        }

        public bool UpdateCategory(CategoryEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categories
                        .Single(e => e.CategoryId == model.CategoryId && e.OwnerId == _userId);

                entity.Name = model.Name;
                entity.CategoryModifiedUtc = DateTimeOffset.Now;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteCategory(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categories
                        .Single(e => e.CategoryId == id && e.OwnerId == _userId);

                ctx.Categories.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

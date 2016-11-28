﻿namespace App.Test.Inventory.Category
{
    using App.Common.UnitTest;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using App.Common.DI;
    using System;
    using App.Common.Validation;
    using Service.Inventory;

    [TestClass]
    public class DeleteCategory : BaseUnitTest
    {
        [TestMethod]
        public void Inventory_Category_DeleteCategory_ShouldGetException_WithInValidCategoryId()
        {
            try
            {
                Guid id = Guid.Empty;
                ICategoryService categoryService = IoC.Container.Resolve<ICategoryService>();
                categoryService.Delete(id);
                Assert.IsTrue(false);
            }
            catch (ValidationException ex)
            {
                Assert.IsTrue(ex.HasExceptionKey("inventory.categories.validation.categoryIsInvalid"));
            }
        }

        [TestMethod]
        public void Inventory_Category_DeleteCategory_ShouldBeSuccess_WithValidRequest()
        {
            string name = "Name of category";
            string description = "Description of category";
            ICategoryService categoryService = IoC.Container.Resolve<ICategoryService>();
            App.Entity.Inventory.Category categoryItem = this.CreateCategoryItem(name, description);
            categoryService.Delete(categoryItem.Id);
            GetCategoryResponse categoryItemAfterDelete = categoryService.GetCategory(categoryItem.Id);
            Assert.IsNull(categoryItemAfterDelete);
        }

        private App.Entity.Inventory.Category CreateCategoryItem(string name, string desc)
        {
            CreateCategoryRequest request = new CreateCategoryRequest(name, desc);
            ICategoryService service = IoC.Container.Resolve<ICategoryService>();
            App.Entity.Inventory.Category category = service.Create(request);
            return category;
        }
    }
}

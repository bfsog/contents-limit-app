using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContentsLimit.Business;
using System;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace Business.Tests
{
    /// <summary>
    /// Unit tests related to the item object.
    /// I follow the subject/state/result method for naming unit tests, and assemble/act/assert for implementation.
    /// </summary>
    [TestClass]
    public class ItemTests
    {
        /// <summary>
        /// Test to assert that items with no valid category will fail validation.
        /// <see cref="ContentsLimit.Business.Models.Category.CategoryType"/>
        /// </summary>
        [TestMethod]
        public void ValidateItem_ItemWithNoCategory_FailsValidation()
        {
            // Arrange - the current validation only looks for CategoryId mapping to a enum value inside Business.Models.Category.CategoryTypes
            var testItem = new ContentsLimit.Business.Models.ContentItem()
            {
                CategoryId = 0
            };

            // Act
            var validationResult = new ContentsLimit.Business.ItemValidator().CreateItemIsValid(testItem);

            // Assert.
            Assert.IsFalse(validationResult.IsValid, "Validation of item category id == 0 failed.");
        }

        /// <summary>
        /// Test to assert that items with no valid category will fail validation.
        /// </summary>
        [TestMethod]
        public void ValidateItem_ITemWithNoCategory_ReturnsProperErrorMessage()
        {
            var testItem = new ContentsLimit.Business.Models.ContentItem()
            {
                CategoryId = 0
            };

            var validationResult = new ContentsLimit.Business.ItemValidator().CreateItemIsValid(testItem);

            Assert.AreEqual(LibBus.ERROR_CATEGORY_NOT_SET, "Choose a category.", "Validation of item category id == 0 did not return correct error message.");
        }


        /// <summary>
        /// Test to assert that category 1 equals the corresponding value in the category type enum.
        /// <see cref="ContentsLimit.Business.Models.Category.CategoryType"/>
        /// These unit tests will validate that the enum has not changed.
        /// </summary>
        [TestMethod]
        [TestCategory("CategoryEnum")]
        public void Category_One_EqualsClothing()
        {
            var categoryId = 1;

            var category = (ContentsLimit.Business.Models.Category.CategoryType)categoryId;

            Assert.AreEqual(ContentsLimit.Business.Models.Category.CategoryType.Clothing, category, $"Value {categoryId} does not map to expected enum type.");
        }

        /// <summary>
        /// Test to assert that category 12equals the corresponding value in the category type enum.
        /// <see cref="ContentsLimit.Business.Models.Category.CategoryType"/>
        /// </summary>
        [TestMethod]
        [TestCategory("CategoryEnum")]
        public void Category_Two_EqualsElectronics()
        {
            var categoryId = 2;

            var category = (ContentsLimit.Business.Models.Category.CategoryType)categoryId;

            Assert.AreEqual(ContentsLimit.Business.Models.Category.CategoryType.Electronics, category, $"Value {categoryId} does not map to expected enum type.");
        }

        /// <summary>
        /// Test to assert that category 3 equals the corresponding value in the category type enum.
        /// <see cref="ContentsLimit.Business.Models.Category.CategoryType"/>
        /// </summary>
        [TestMethod]
        [TestCategory("CategoryEnum")]
        public void Category_Three_EqualsKitchen()
        {
            var categoryId = 3;

            var category = (ContentsLimit.Business.Models.Category.CategoryType)categoryId;

            Assert.AreEqual(ContentsLimit.Business.Models.Category.CategoryType.Kitchen, category, $"Value {categoryId} does not map to expected enum type.");
        }

        /// <summary>
        /// Test to assert that category 4 equals the corresponding value in the category type enum.
        /// <see cref="ContentsLimit.Business.Models.Category.CategoryType"/>
        /// </summary>
        [TestMethod]
        [TestCategory("CategoryEnum")]
        public void Category_Four_EqualsSports()
        {
            var categoryId = 4;

            var category = (ContentsLimit.Business.Models.Category.CategoryType)categoryId;

            Assert.AreEqual(ContentsLimit.Business.Models.Category.CategoryType.Sports, category, $"Value {categoryId} does not map to expected enum type.");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Category_OutsideOfEnum_ThrowsException()
        {
            var categoryId = 5;
            var notDefined = Enum.IsDefined(typeof(ContentsLimit.Business.Models.Category.CategoryType), categoryId);
            if (!notDefined)
            {
                throw new InvalidOperationException();
            }
        }

    }
}

using ContentsLimit.Business;
using ContentsLimit.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ContentsLimit.Web.Api
{
    [ApiController, Route("api/v1/category")]
    public class CategoryController : BaseController
    {
        public CategoryController(DataContext db) : base(db) { }

        /// <summary>
        /// Deletes all items assigned to the category from the persistent database. This is called via an Ajax JS call. Another option would be to have a get() view and have the confirmation on there.
        /// Benefit of deleting via JS is quicker for the user and can be tested via API testing tools like Postman.
        /// Note that this and delete item both reuse the same model as the key is the same native type.
        /// This uses .ExecuteSqlRaw as there could be multiple (1-to-n) items under a specific category.
        /// Category is not deleted as we map the category to an enum.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("delete")]
        public IActionResult DeleteCategory(GenericDeleteItemModel model)
        {
            var categoryExists = Db.Categories.Where(q => q.Id == model.Id).Any();
            if (!categoryExists)
            {
                return NotFound();
            }

            var safeIdParameter = new SqlParameter("categoryId", model.Id);
            var deleteCommand = "DELETE FROM dbo.Items Where CategoryId = @categoryId;";
            var rowsDeleted = Db.Database.ExecuteSqlRaw(deleteCommand, safeIdParameter);
            var success = (rowsDeleted > 0);
            if(success)
            {
                this.AddMessage(MessageType.Success, string.Format(LibBus.SUCCESS_CATEGORY_DELETED, rowsDeleted, (Business.Models.Category.CategoryType)model.Id));
            }
            return Ok(new { success, deletedCategoryId = model.Id });
        }

    }
}
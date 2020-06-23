using ContentsLimit.Business;
using ContentsLimit.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ContentsLimit.Web.Api
{
    [ApiController, Route("api/v1/item")]
    public class ItemController : BaseController
    {
        public ItemController(DataContext db) : base(db) { }

        /// <summary>
        /// Deletes the selected item from the persistent database. This is called via an Ajax JS call. Another option would be to have a get() view and have the confirmation on there.
        /// Benefit of deleting via JS is quicker for the user and can be tested via API testing tools like Postman.
        /// Note that this and the delete category both reuse the same model as the key is the same native type.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteItem(GenericDeleteItemModel model)
        {
            var itemExists = Db.Items.Where(q => q.Id == model.Id).Any();
            if(!itemExists)
            {
                return NotFound();
            }
            // I could put this First() call in the Remove() but I want a reference to it so the user knows their last action.
            var itemToDelete = Db.Items.First(q => q.Id == model.Id);
            Db.Items.Remove(itemToDelete);
            var success = (await Db.SaveChangesAsync() == 1);
            if(success)
            {
                this.AddMessage(MessageType.Success, string.Format(LibBus.SUCCESS_ITEM_DELETED, itemToDelete.Name, (Business.Models.Category.CategoryType)itemToDelete.CategoryId));
            }
            return Ok(new { success, deletedItemId = model.Id });
        }
    }
}

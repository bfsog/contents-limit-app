using ContentsLimit.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ContentsLimit.Web.Controllers
{
    public class HomeController : BaseController
    {

        public HomeController(DataContext db) : base(db) { }

        public async Task<IActionResult> Index()
        {
            var viewModel = new ViewModels.HomeIndexViewModel();
            var items = await Db.Items.Select(i => new ViewModels.ContentItemModel { Id = i.Id, Name = i.Name, Cost = i.Cost, Category = (Business.Models.Category.CategoryType)i.CategoryId }).ToListAsync();
            viewModel.ExistingItems = items;
            return View(viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ViewModels.ContentItemModel model)
        {
            // I do my validations up front so I know after this line we are good to go, no application issue should occur after this. 
            // if something breaks it is infrastructure (memory, db connection etc).
            var createItemValidation = new Business.ItemValidator().CreateItemIsValid(new Business.Models.ContentItem() { CategoryId =(int) model.Category });
            if(!createItemValidation.IsValid)
            {
                this.AddMessage(MessageType.Error, createItemValidation.Message);
                return RedirectToAction("Index");
            }

            var isItemCreated = await CreateItem(model);
            if(isItemCreated)
            {
                this.AddMessage(MessageType.Success, string.Format(Business.LibBus.SUCCESS_ITEM_CREATED, model.Name, model.Category));
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Creates an item to our persistent database. Returns true if affected record count is gt 0.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private async Task<bool> CreateItem(ViewModels.ContentItemModel item)
        {
            // Creating an entity here so we cast the enum to it's data type so our datastore can process.
            Db.Items.Add(new Business.Models.ContentItem()
            {
                CategoryId = (int)item.Category,
                Name = item.Name,
                Cost = item.Cost
            });
            return (await Db.SaveChangesAsync() > 0);
        }
    }
}

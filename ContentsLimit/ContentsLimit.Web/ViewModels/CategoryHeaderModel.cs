using System.Collections.Generic;

namespace ContentsLimit.Web.ViewModels
{
    /// <summary>
    /// Class which represents a specific category.
    /// </summary>
    public class CategoryHeaderModel
    {
        public List<ViewModels.ContentItemModel> CategoryItems { get; set; }
        public Business.Models.Category.CategoryType CurrentCategory { get; set; }
    }
}

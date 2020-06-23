using ContentsLimit.Business.Models;
using System.Collections.Generic;

namespace ContentsLimit.Web.ViewModels
{
    /// <summary>
    /// Class which represents the items under a specific category.
    /// </summary>
    public class CategoryListModel
    {
        public List<ViewModels.ContentItemModel> CategoryItems { get; set; }
        public Category.CategoryType CategoryType { get; set; }
    }
}

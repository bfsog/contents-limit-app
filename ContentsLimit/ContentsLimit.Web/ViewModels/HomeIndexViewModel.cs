using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContentsLimit.Web.ViewModels
{
    /// <summary>
    /// View model for the index view of the home controller. Includes all of the existing items.
    /// </summary>
    public class HomeIndexViewModel
    {
        public List<ContentItemModel> ExistingItems { get; set; }
    }

    /// <summary>
    /// This is a view model representing our entity. Differs from entity as we map the category to an enum.
    /// Use this when displaying data.
    /// </summary>
    public class ContentItemModel
    {
        [Key]
        public int Id { get; set; }
        public Business.Models.Category.CategoryType Category { get; set; }
        public string Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Cost { get; set; }
    }

}

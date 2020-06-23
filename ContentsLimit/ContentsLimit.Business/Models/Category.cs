using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContentsLimit.Business.Models
{
    /// <summary>
    /// Class which represents the entity dbo.Categories.
    /// Enum is tightly linked to the data within the PostDeployment.sql script.
    /// </summary>
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public enum CategoryType
        {
            [Display(Name = "Clothing")]
            Clothing = 1,
            [Display(Name = "Electronics")]
            Electronics = 2,
            [Display(Name = "Kitchen")]
            Kitchen = 3,
            [Display(Name = "Sports Equipment")]
            Sports = 4,
        }
    }
}

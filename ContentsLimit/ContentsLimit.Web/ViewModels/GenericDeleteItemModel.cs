using System.Text.Json.Serialization;

namespace ContentsLimit.Web.ViewModels
{
    /// <summary>
    /// Class which implements the base item class. Used for deleting item and category.
    /// </summary>
    public class GenericDeleteItemModel : IBaseItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}

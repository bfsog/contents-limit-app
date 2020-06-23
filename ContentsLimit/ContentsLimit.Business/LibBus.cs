namespace ContentsLimit.Business
{
    /// <summary>
    /// Helper class for messages displayed to the user.
    /// </summary>
    public class LibBus
    {
        /// <summary>
        /// This property is used when the user never enters a category whilst creating new item.
        /// </summary>
        public const string ERROR_CATEGORY_NOT_SET = "Choose a category.";
        /// <summary>
        /// This property is used when the user creates a new item.
        /// </summary>
        public const string SUCCESS_ITEM_CREATED = "Item {0} created in {1} category.";
        /// <summary>
        /// This property is used when an item is deleted.
        /// </summary>
        public const string SUCCESS_ITEM_DELETED = "Item {0} deleted from {1} category.";
        /// <summary>
        /// This property is used when all items under a specific category are deleted.
        /// </summary>
        public const string SUCCESS_CATEGORY_DELETED = "All {0} item(s) in {1} category have been deleted.";
    }
}

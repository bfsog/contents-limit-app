namespace ContentsLimit.Business
{
    /// <summary>
    /// Business validation rules class for create item model.
    /// </summary>
    public class ItemValidator : Validation
    {
        /// <summary>
        /// Returns object.IsValid true if the create item model is in a state to be added.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public ItemValidator CreateItemIsValid(Business.Models.ContentItem item)
        {
            ItemValidator result = new ItemValidator
            {
                IsValid = true
            };
            if (item.CategoryId == 0)
            {
                result.IsValid = false;
                result.Message = LibBus.ERROR_CATEGORY_NOT_SET;
            }
            return result;
        }
    }
}

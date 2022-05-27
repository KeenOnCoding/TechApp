using System.Collections.Generic;

namespace TechApp
{
    public static class ProductExtentions
    {
        public static void ChangeUriPlaceholder(this IEnumerable<Product> items, string baseUrl)
        {
            if (items != null)
            {
                foreach (var product in items)
                {
                    product.FillProductUrl(baseUrl);
                }
            }
        }
        public static void FillProductUrl(this Product item, string picBaseUrl)
        {
            if (item != null)
            {
                foreach (var product in item.Pictures)
                {
                    product.Name = picBaseUrl.Replace("[0]", product.Id.ToString());
                }

            }
        }
    }
}

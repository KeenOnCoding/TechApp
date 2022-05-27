using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechApp
{
    public class Product
    {
        [JsonProperty("id")]
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("salePrice")]
        public decimal SalePrice { get; set; }

        [JsonProperty("discount")]
        public int Discount { get; set; }

        [JsonProperty("pictures")]
        public IEnumerable<Picture> Pictures { get; set; }

        [JsonProperty("shortDetails")]
        public string ShortDetails { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("stock")]
        public int Stock { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("newPro")]
        public bool NewPro { get; set; }

        [JsonProperty("sale")]
        public bool Sale { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("tags")]
        public IEnumerable<Tag> Tags { get; set; }

        [JsonProperty("colors")]
        public IEnumerable<Color> Colors { get; set; }
    }
}

namespace ActionFilterAndHandler.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [MetadataType(typeof(ProductMD))]
    public partial class Product
    {
        public class ProductMD
        {
            public int ProductID { get; set; }

            [Required]
            [StringLength(40)]
            public string ProductName { get; set; }

            public int? SupplierID { get; set; }

            public int? CategoryID { get; set; }

            [StringLength(20)]
            public string QuantityPerUnit { get; set; }

            [Column(TypeName = "money")]
            public decimal? UnitPrice { get; set; }

            public short? UnitsInStock { get; set; }

            public short? UnitsOnOrder { get; set; }

            public short? ReorderLevel { get; set; }

            public bool Discontinued { get; set; }

            [JsonIgnore]
            public virtual ICollection<Order_Detail> Order_Details { get; set; }
        }      
    }
}

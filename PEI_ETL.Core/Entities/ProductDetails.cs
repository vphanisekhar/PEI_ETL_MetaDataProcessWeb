﻿

namespace PEI_ETL.Core.Entities
{
    public class ProductDetails
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public int ProductPrice { get; set; }
        public int ProductStock { get; set; }

        public bool? IsDeleted { get; set; }         
    }
}

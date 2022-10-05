using System;
using System.Collections.Generic;

#nullable disable

namespace FirstprojectAspWebApi.Models
{
    public partial class DetailsOfItem
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}

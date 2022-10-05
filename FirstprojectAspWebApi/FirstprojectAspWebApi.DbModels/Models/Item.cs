using System;
using System.Collections.Generic;

#nullable disable

namespace FirstprojectAspWebApi.Models
{
    public partial class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Archived { get; set; }
        public int SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }
    }
}

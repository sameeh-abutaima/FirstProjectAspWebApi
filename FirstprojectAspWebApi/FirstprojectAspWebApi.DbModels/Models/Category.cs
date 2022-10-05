using System;
using System.Collections.Generic;

#nullable disable

namespace FirstprojectAspWebApi.Models
{
    public partial class Category
    {
        public Category()
        {
            SubCategories = new HashSet<SubCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Archived { get; set; }

        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}

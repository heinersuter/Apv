using System;
using System.ComponentModel.DataAnnotations;

namespace Apv.Data.Model
{
    public class Item
    {
        public Item()
        {
            LastModified = DateTime.UtcNow;
        }

        [Key]
        public long Id { get; set; }

        public DateTime LastModified { get; set; }
    }
}

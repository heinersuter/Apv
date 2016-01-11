using System;
using System.ComponentModel.DataAnnotations;

namespace Apv.Data.Model
{
    public abstract class Item
    {
        protected Item()
        {
            LastModified = DateTime.UtcNow;
        }

        [Key]
        public long Id { get; set; }

        public DateTime LastModified { get; set; }
    }
}

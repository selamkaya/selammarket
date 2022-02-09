using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SelamMarket.Data.Base
{
    public abstract class EntityBase<TId>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TId Id { get; set; }

        public int? CreatedUserId { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}

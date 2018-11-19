using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.Common
{
    public abstract class EntityBase
    {
        public EntityBase()
        {
            CreatedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
            UserIdCreated = 0;
            UserIdUpdated = 0;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserIdCreated { get; set; }

        [Required]
        public int UserIdUpdated { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}

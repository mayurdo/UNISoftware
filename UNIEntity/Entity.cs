using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UNIEntity
{
    public abstract class Entity : IRequest
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long SrNo { get; set; }

        public string ExecutiveName { get; set; }

        public DateTime? ModifiedDateTime { get; set; }

        public bool IsDeleted { get; set; }

        public string DeletedReason { get; set; }
    }
}
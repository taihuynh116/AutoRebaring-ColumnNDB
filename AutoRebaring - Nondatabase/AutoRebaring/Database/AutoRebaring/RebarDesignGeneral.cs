namespace AutoRebaring.Database.AutoRebaring
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RebarDesignGeneral")]
    public partial class RebarDesignGeneral
    {
        public long ID { get; set; }

        public long ProjectID { get; set; }

        [Required]
        [StringLength(20)]
        public string Mark { get; set; }

        [Required]
        [StringLength(50)]
        public string StartLevel { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        [StringLength(50)]
        public string EndLevel { get; set; }

        public bool IsLockHead { get; set; }

        public bool IsStartRebar { get; set; }

        public double RebarStartZ1 { get; set; }

        public double RebarStartZ2 { get; set; }
    }
}

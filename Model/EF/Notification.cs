namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int billid { get; set; }

        public int? userid { get; set; }

        [StringLength(255)]
        public string title { get; set; }

        public string content { get; set; }

        public int? status { get; set; }

        public DateTimeOffset? createdAt { get; set; }

        public DateTimeOffset? updatedAt { get; set; }

        public virtual Bill Bill { get; set; }
    }
}

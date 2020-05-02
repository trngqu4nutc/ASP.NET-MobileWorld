namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Basket
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int catalogid { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int userid { get; set; }

        public int unit { get; set; }

        public DateTimeOffset createdAt { get; set; }

        public DateTimeOffset? updatedAt { get; set; }

        public virtual User User { get; set; }

        public virtual Catalog Catalog { get; set; }
    }
}

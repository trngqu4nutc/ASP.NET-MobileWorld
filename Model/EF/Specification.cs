namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Specification
    {
        public int id { get; set; }

        [StringLength(100)]
        public string cpu { get; set; }

        [StringLength(100)]
        public string ram { get; set; }

        [StringLength(100)]
        public string screen { get; set; }

        [StringLength(100)]
        public string os { get; set; }

        public DateTimeOffset? createdAt { get; set; }

        public DateTimeOffset? updatedAt { get; set; }

        public int catalogid { get; set; }

        public virtual Catalog Catalog { get; set; }
    }
}

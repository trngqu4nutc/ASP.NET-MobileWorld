namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SpecificationsLaptop
    {
        public int id { get; set; }

        [StringLength(100)]
        public string cardscreen { get; set; }

        [StringLength(100)]
        public string connector { get; set; }

        [StringLength(100)]
        public string harddrive { get; set; }

        [StringLength(100)]
        public string design { get; set; }

        [StringLength(100)]
        public string size { get; set; }

        [StringLength(100)]
        public string release { get; set; }

        public DateTimeOffset? createdAt { get; set; }

        public DateTimeOffset? updatedAt { get; set; }

        public int catalogid { get; set; }

        public virtual Catalog Catalog { get; set; }
    }
}

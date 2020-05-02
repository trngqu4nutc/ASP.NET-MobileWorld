namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SpecificationsMobile
    {
        public int id { get; set; }

        [StringLength(100)]
        public string backcamera { get; set; }

        [StringLength(100)]
        public string frontcamera { get; set; }

        [StringLength(100)]
        public string internalmemory { get; set; }

        [StringLength(100)]
        public string memorystick { get; set; }

        [StringLength(100)]
        public string sim { get; set; }

        [StringLength(100)]
        public string batery { get; set; }

        public DateTimeOffset? createdAt { get; set; }

        public DateTimeOffset? updatedAt { get; set; }

        public int catalogid { get; set; }

        public virtual Catalog Catalog { get; set; }
    }
}

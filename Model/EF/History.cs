namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class History
    {
        public int id { get; set; }

        public decimal inputprice { get; set; }

        public int unit { get; set; }

        public int catalogid { get; set; }

        public DateTimeOffset createdAt { get; set; }

        public DateTimeOffset? updatedAt { get; set; }

        public virtual Catalog Catalog { get; set; }
    }
}

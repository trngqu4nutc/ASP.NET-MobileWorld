namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bill
    {
        public int id { get; set; }

        public decimal price { get; set; }

        public int unit { get; set; }

        public int catalogid { get; set; }

        public string pictureuri { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        public int status { get; set; }

        public DateTimeOffset? createdAt { get; set; }

        public DateTimeOffset? updatedAt { get; set; }

        public int userid { get; set; }

        public virtual User User { get; set; }
    }
}

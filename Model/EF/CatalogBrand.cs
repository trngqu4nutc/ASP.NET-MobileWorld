namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CatalogBrand
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CatalogBrand()
        {
            Catalogs = new HashSet<Catalog>();
        }

        public int id { get; set; }

        [StringLength(100)]
        public string brand { get; set; }
        [StringLength(100)]
        public string pictureurl { get; set; }

        public DateTimeOffset? createdAt { get; set; }

        public DateTimeOffset? updatedAt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Catalog> Catalogs { get; set; }
    }
}

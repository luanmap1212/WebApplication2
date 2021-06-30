namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Book")]
    public partial class Book
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string Title { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(255)]
        public string Description { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(255)]
        public string Author { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(255)]
        public string Images { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Price { get; set; }
    }
}

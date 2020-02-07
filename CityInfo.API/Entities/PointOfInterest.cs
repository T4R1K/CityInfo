using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Entities
{
    public class PointOfInterest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // will be generated upon creating entity

        public int Id { get; set; }
        [Required]
        [MaxLength (50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [ForeignKey("CityId")]

        public City City { get; set; } // navigation to Source

        public int CityId { get; set; } // Foreign key

    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MovieAPI_Project.Entity
{
    public class Franchise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //Navigation property:character has many movies
        //Navigation property:character has many movies
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}

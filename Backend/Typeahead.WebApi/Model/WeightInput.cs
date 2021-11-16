using System.ComponentModel.DataAnnotations;
using static System.Int32;

namespace Typeahead.WebApi.Model
{
    public class WeightInput
    {
        [Required]
        [Range(1, MaxValue)]
        public int TermId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Input { get; set; }
    }
}
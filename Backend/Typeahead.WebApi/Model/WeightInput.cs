using System.ComponentModel.DataAnnotations;

namespace Typeahead.WebApi.Model
{
    public class WeightInput
    {
        [Required]
        public int TermId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Input { get; set; }
    }
}
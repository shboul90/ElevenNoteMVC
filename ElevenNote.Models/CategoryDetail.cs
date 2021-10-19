using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    public class CategoryDetail
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        [Display(Name = "Created")]
        public DateTimeOffset CategoryCreatedUtc { get; set; }

        [Display(Name = "Modified")]
        public DateTimeOffset? CategoryModifiedUtc { get; set; }
    }
}

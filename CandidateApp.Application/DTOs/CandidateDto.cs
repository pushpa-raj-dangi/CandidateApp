using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.Application.DTOs
{
    public class CandidateDto
    {
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage= "Last Name is required.")]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string CallTimeInterval { get; set; }
        public string LinkedInUrl { get; set; }
        public string GitHubUrl { get; set; }
        [Required(ErrorMessage = "Comment is required.")]
        public string Comment { get; set; }
    }
}

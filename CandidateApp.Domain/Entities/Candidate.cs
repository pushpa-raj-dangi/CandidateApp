using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.Domain.Entities
{
    public sealed class Candidate(string firstName, string lastName, string email, string comment, string phoneNumber = null!,
                     string bestTimeToCall = null!, string linkedInUrl = null!, string gitHubUrl = null!)
    {
        public string FirstName { get; private set; } = firstName;
        public string LastName { get; private set; } = lastName;
        public string Email { get; private set; } = email;
        public string PhoneNumber { get; private set; } = phoneNumber;
        public string BestTimeToCall { get; private set; } = bestTimeToCall;
        public string LinkedInUrl { get; private set; } = linkedInUrl;
        public string GitHubUrl { get; private set; } = gitHubUrl;
        public string Comment { get; private set; } = comment;
    }
}

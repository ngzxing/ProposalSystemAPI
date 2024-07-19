using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ProposalSystem.Data.Enum;

namespace ProposalSystem.Dtos.Proposal
{
    public class CreateProposal
    {
        
        [Required]
        required public string StudentId  {get; set;}

        [Required]
        required public IFormFile ProposalFile  {get; set;}

        [Required]
        required public IFormFile FormFile  {get; set;}

        [Required]
        required public string Title {get; set;}

        [Required]
        required public Domain Domain {get; set;}

    }
}
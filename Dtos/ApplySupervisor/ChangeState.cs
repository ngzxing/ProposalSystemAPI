using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProposalSystem.Data.Enum;

namespace ProposalSystem.Dtos.ApplySupervisor
{
    public class ChangeState
    {
        public ApplySupervisorStatus? status {get; set;}
    }
}
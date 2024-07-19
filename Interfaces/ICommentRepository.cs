using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProposalSystem.Dtos.Comment;

namespace ProposalSystem.Interfaces
{
    public interface ICommentRepository
    {
        Task< IEnumerable<GetCommentResult>? > GetByProposalIdAsync( string ProposalId );

        Task<GetCommentResult?> GetByIdAsync( string CommentId );

        Task< (GetCommentResult?, string?) > CreateAsync(CreateComment dto );

        Task<string?> DeleteCommentAsync(string id);
    }
}
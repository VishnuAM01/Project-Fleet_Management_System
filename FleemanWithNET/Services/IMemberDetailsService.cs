using fleeman_with_dot_net.Models;
using System.Collections.Generic;

namespace fleeman_with_dot_net.Services
{
    public interface IMemberDetailsService
    {
        IEnumerable<MemberDetails> GetAllMembers();
        MemberDetails GetMemberById(int id);
        MemberDetails AddMember(MemberDetails member);
        MemberDetails UpdateMember(MemberDetails member);
        bool DeleteMember(int id);
    }
}

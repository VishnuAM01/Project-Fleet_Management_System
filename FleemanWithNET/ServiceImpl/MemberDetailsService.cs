using fleeman_with_dot_net.Models;
using fleeman_with_dot_net.Repositories;
using fleeman_with_dot_net.Services;
using System.Collections.Generic;
using System.Linq;

namespace fleeman_with_dot_net.ServiceImpl
{
    public class MemberDetailsService : IMemberDetailsService
    {
        private readonly FleetDBContext context;

        public MemberDetailsService(FleetDBContext context)
        {
            this.context = context;
        }

        public IEnumerable<MemberDetails> GetAllMembers()
        {
            return context.member_details.ToList();
        }

        public MemberDetails GetMemberById(int id)
        {
            return context.member_details.FirstOrDefault(m => m.Member_Id == id);
        }

        public MemberDetails AddMember(MemberDetails member)
        {
            context.member_details.Add(member);
            context.SaveChanges();
            return member;
        }

        public MemberDetails UpdateMember(MemberDetails member)
        {
            var existingMember = context.member_details.FirstOrDefault(m => m.Member_Id == member.Member_Id);
            if (existingMember == null)
                return null;

            existingMember.Address = member.Address;
            existingMember.City = member.City;
            existingMember.CreditCard = member.CreditCard;
            existingMember.Dob = member.Dob;
            existingMember.DrivingLicenseId = member.DrivingLicenseId;
            existingMember.DrivingLicenseIssuedBy = member.DrivingLicenseIssuedBy;
            existingMember.DrivingLicenseValidThru = member.DrivingLicenseValidThru;
            existingMember.Email = member.Email;
            existingMember.MemberFirstName = member.MemberFirstName;
            existingMember.Idp = member.Idp;
            existingMember.IdpIssuedBy = member.IdpIssuedBy;
            existingMember.IdpValidThru = member.IdpValidThru;
            existingMember.MemberLastName = member.MemberLastName;
            existingMember.MobileNumber = member.MobileNumber;
            existingMember.PassportIssuedBy = member.PassportIssuedBy;
            existingMember.PassportNo = member.PassportNo;
            existingMember.PassportValidate = member.PassportValidate;
            existingMember.State = member.State;
            existingMember.ZipCode = member.ZipCode;

            context.SaveChanges();
            return existingMember;
        }

        public bool DeleteMember(int id)
        {
            var member = context.member_details.FirstOrDefault(m => m.Member_Id == id);
            if (member == null)
                return false;

            context.member_details.Remove(member);
            context.SaveChanges();
            return true;
        }
    }
}

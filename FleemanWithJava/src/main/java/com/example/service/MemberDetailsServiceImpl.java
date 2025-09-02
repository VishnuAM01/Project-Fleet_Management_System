package com.example.service;


import com.example.models.MemberDetails;
import com.example.repository.MemberDetailsRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.web.bind.annotation.RequestBody;

import java.util.List;
import java.util.Optional;

@Service
public class MemberDetailsServiceImpl implements MemberDetailsService {

    @Autowired
    private MemberDetailsRepository memberRepo;

    @Override
    public MemberDetails registerMember(@RequestBody MemberDetails member) {
    	String newMemberId = generateNextMemberId();
        member.setMemberId(newMemberId);
        member.setRegistered(true);
        return memberRepo.save(member);
    }

    @Override
    public List<MemberDetails> getAllMembers() {
        return memberRepo.findAll();
    }

    @Override
    public Optional<MemberDetails> getMemberById(String memberId) {
        return memberRepo.findById(memberId);
    }

    @Override
    public MemberDetails updateMember(String memberId, MemberDetails updatedMember) {
        return memberRepo.findById(memberId).map(member -> {
            member.setFirstName(updatedMember.getFirstName());
            member.setLastName(updatedMember.getLastName());
            member.setUsername(updatedMember.getUsername());
            member.setEmail(updatedMember.getEmail());
            member.setMobileNumber(updatedMember.getMobileNumber());
            member.setDob(updatedMember.getDob());
            member.setAddress(updatedMember.getAddress());
            member.setCity(updatedMember.getCity());
            member.setState(updatedMember.getState());
            member.setZipCode(updatedMember.getZipCode());
            member.setDrivingLicenseId(updatedMember.getDrivingLicenseId());
            member.setDrivingLicenseIssuedBy(updatedMember.getDrivingLicenseIssuedBy());
            member.setDrivingLicenseValidThru(updatedMember.getDrivingLicenseValidThru());
            member.setIdp(updatedMember.getIdp());
            member.setIdpIssuedBy(updatedMember.getIdpIssuedBy());
            member.setIdpValidThru(updatedMember.getIdpValidThru());
            member.setPassportNo(updatedMember.getPassportNo());
            member.setPassportIssuedBy(updatedMember.getPassportIssuedBy());
            member.setPassportValidate(updatedMember.getPassportValidate());
            member.setCreditCardType(updatedMember.getCreditCardType());
            member.setRole(updatedMember.getRole());
            return memberRepo.save(member);
        }).orElseThrow(() -> new RuntimeException("Member not found"));
    }

    @Override
    public void deleteMember(String memberId) {
        memberRepo.deleteById(memberId);
    }

    @Override
    public boolean isRegistered(String memberId) {
        return memberRepo.findById(memberId).map(MemberDetails::isRegistered).orElse(false);
    }
    private String generateNextMemberId() {
        Optional<MemberDetails> lastMemberOpt = memberRepo.findTopByOrderByMemberIdDesc();

        if (lastMemberOpt.isPresent()) {
            String lastId = lastMemberOpt.get().getMemberId(); 
            int numericPart = Integer.parseInt(lastId.substring(2)); 
            int nextId = numericPart + 1;
            return String.format("M%05d", nextId); 
        } else {
            return "M00001";
        }
    }

}


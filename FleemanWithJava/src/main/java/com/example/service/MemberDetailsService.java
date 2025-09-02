package com.example.service;

import com.example.models.MemberDetails;

import java.util.List;
import java.util.Optional;

public interface MemberDetailsService {

    MemberDetails registerMember(MemberDetails member);

    List<MemberDetails> getAllMembers();

    Optional<MemberDetails> getMemberById(String memberId);

    MemberDetails updateMember(String memberId, MemberDetails updatedMember);

    void deleteMember(String memberId);

    boolean isRegistered(String memberId);
}

package com.example.repository;

import com.example.models.MemberDetails;

import java.util.Optional;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface MemberDetailsRepository extends JpaRepository<MemberDetails, String> {
    MemberDetails findByMemberId(String memberId);
    Optional<MemberDetails> findTopByOrderByMemberIdDesc();
    Optional<MemberDetails> findByEmail(String email);
}

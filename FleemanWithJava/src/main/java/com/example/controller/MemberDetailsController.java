package com.example.controller;

import com.example.models.MemberDetails;
import com.example.service.MemberDetailsService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@CrossOrigin(origins ="*")
@RestController
@RequestMapping("/members")
public class MemberDetailsController {

    @Autowired
    private MemberDetailsService memberService;

    @PostMapping("/register")
    public MemberDetails registerMember(@RequestBody MemberDetails member) {
        return memberService.registerMember(member);
    }

    @GetMapping
    public List<MemberDetails> getAllMembers() {
        return memberService.getAllMembers();
    }

    @GetMapping("/{memberId}")
    public MemberDetails getMember(@PathVariable("memberId") String memberId) {
        return memberService.getMemberById(memberId).orElse(null);
    }


    @PutMapping("/{memberId}")
    public MemberDetails updateMember(@PathVariable String memberId, @RequestBody MemberDetails member) {
        return memberService.updateMember(memberId, member);
    }

    @DeleteMapping("/{memberId}")
    public void deleteMember(@PathVariable String memberId) {
        memberService.deleteMember(memberId);
    }
}

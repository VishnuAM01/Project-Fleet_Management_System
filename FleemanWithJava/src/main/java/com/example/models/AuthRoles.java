package com.example.models;

import jakarta.persistence.*;
import java.util.List;

import com.fasterxml.jackson.annotation.JsonBackReference;

@Entity
@Table(name = "Auth_Role")
public class AuthRoles {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int role_id;

    @Column(nullable=false)
    private String role_name;

    @OneToMany(mappedBy = "role")
    @JsonBackReference
    private List<MemberDetails> members;

    public int getRoleid() {
        return role_id;
    }

    public void setRoleid(int roleid) {
        this.role_id = roleid;
    }

    public String getRolename() {
        return role_name;
    }

    public void setRolename(String rolename) {
        this.role_name = rolename;
    }

    public List<MemberDetails> getMembers() {
        return members;
    }

    public void setMembers(List<MemberDetails> members) {
        this.members = members;
    }
}

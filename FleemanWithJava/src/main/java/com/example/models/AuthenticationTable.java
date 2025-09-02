package com.example.models;

import jakarta.persistence.*;

@Entity
@Table(name = "AuthenticationTable")
public class AuthenticationTable {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer Auth_Id;

    @Column(name = "email", nullable = false)
    private String email;

    @Column(length = 18, nullable=false)
    private String Password;

    @Column(name = "role_id")
    private Integer role_id;
    
    @Column(name = "member_id", length = 6, nullable = false, unique = true)
    private String memberId;

    public String getEmail() {
		return email;
	}

	public void setEmail(String email) {
		this.email = email;
	}

	public String getMemberId() {
		return memberId;
	}

	public void setMemberId(String memberId) {
		this.memberId = memberId;
	}

	public Integer getAuth_Id() {
        return Auth_Id;
    }

    public void setAuth_Id(Integer auth_Id) {
        Auth_Id = auth_Id;
    }


    public String getPassword() {
        return Password;
    }

    public void setPassword(String password) {
        Password = password;
    }

    public Integer getRole_id() {
        return role_id;
    }

    public void setRole_id(Integer role_id) {
        this.role_id = role_id;
    }
}

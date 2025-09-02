package com.example.models;

import jakarta.persistence.*;
import java.util.Date;

import com.fasterxml.jackson.annotation.JsonManagedReference;

@Entity
@Table(name = "Member_Details")
public class MemberDetails {

    @Id
    @Column(name = "member_id", length = 6, nullable = false, unique = true)
    private String memberId;

    @Column(name = "member_user_name", length = 60, nullable = false, unique = true)
    private String username;

    @Column(name = "member_first_name", length = 60, nullable = false)
    private String firstName;

    @Column(name = "member_last_name", length = 60, nullable = false)
    private String lastName;

    @ManyToOne
    @JoinColumn(
        name = "role_id",
        referencedColumnName = "role_id",
        nullable = false,
        foreignKey = @ForeignKey(ConstraintMode.NO_CONSTRAINT)
    )
    @JsonManagedReference
    private AuthRoles role;

    @Column(name = "email", length = 100, nullable = false, unique = true)
    private String email;

    @Column(name = "mobile_number", nullable = false, unique=true)
    private Long mobileNumber;

    @Temporal(TemporalType.DATE)
    private Date dob;

    @Column(length = 100)
    private String address;

    @Column(length = 60)
    private String city;

    @Column(length = 60)
    private String state;

    private Integer zipCode;

    @Column(name = "driving_license_id", length = 20, nullable = false)
    private String drivingLicenseId;

    private Date drivingLicenseIssuedBy;
    private Date drivingLicenseValidThru;

    @Column(length = 60)
    private String idp;

    private Date idpIssuedBy;
    private Date idpValidThru;

    @Column(name = "passport_no", length = 10)
    private String passportNo;

    @Column(name = "passport_issued_by", length = 60)
    private String passportIssuedBy;

    private Date passportValidate;

    @Enumerated(EnumType.STRING)
    private CreditCardType creditCardType;

    public enum CreditCardType {
        VISA,
        MASTERCARD,
        AMEX,
        DISCOVER
    }
    @Column(nullable = false)
    private boolean registered = false; 

    
    public boolean isRegistered() {
        return registered;
    }

    public void setRegistered(boolean registered) {
        this.registered = registered;
    }
    public String getMemberId() {
        return memberId;
    }

    public void setMemberId(String memberId) {
        this.memberId = memberId;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public AuthRoles getRole() {
        return role;
    }

    public void setRole(AuthRoles role) {
        this.role = role;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public Long getMobileNumber() {
        return mobileNumber;
    }

    public void setMobileNumber(Long mobileNumber) {
        this.mobileNumber = mobileNumber;
    }

    public Date getDob() {
        return dob;
    }

    public void setDob(Date dob) {
        this.dob = dob;
    }

    public String getAddress() {
        return address;
    }

    public void setAddress(String address) {
        this.address = address;
    }

    public String getCity() {
        return city;
    }

    public void setCity(String city) {
        this.city = city;
    }

    public String getState() {
        return state;
    }

    public void setState(String state) {
        this.state = state;
    }

    public Integer getZipCode() {
        return zipCode;
    }

    public void setZipCode(Integer zipCode) {
        this.zipCode = zipCode;
    }

    public String getDrivingLicenseId() {
        return drivingLicenseId;
    }

    public void setDrivingLicenseId(String drivingLicenseId) {
        this.drivingLicenseId = drivingLicenseId;
    }

    public Date getDrivingLicenseIssuedBy() {
        return drivingLicenseIssuedBy;
    }

    public void setDrivingLicenseIssuedBy(Date drivingLicenseIssuedBy) {
        this.drivingLicenseIssuedBy = drivingLicenseIssuedBy;
    }

    public Date getDrivingLicenseValidThru() {
        return drivingLicenseValidThru;
    }

    public void setDrivingLicenseValidThru(Date drivingLicenseValidThru) {
        this.drivingLicenseValidThru = drivingLicenseValidThru;
    }

    public String getIdp() {
        return idp;
    }

    public void setIdp(String idp) {
        this.idp = idp;
    }

    public Date getIdpIssuedBy() {
        return idpIssuedBy;
    }

    public void setIdpIssuedBy(Date idpIssuedBy) {
        this.idpIssuedBy = idpIssuedBy;
    }

    public Date getIdpValidThru() {
        return idpValidThru;
    }

    public void setIdpValidThru(Date idpValidThru) {
        this.idpValidThru = idpValidThru;
    }

    public String getPassportNo() {
        return passportNo;
    }

    public void setPassportNo(String passportNo) {
        this.passportNo = passportNo;
    }

    public String getPassportIssuedBy() {
        return passportIssuedBy;
    }

    public void setPassportIssuedBy(String passportIssuedBy) {
        this.passportIssuedBy = passportIssuedBy;
    }

    public Date getPassportValidate() {
        return passportValidate;
    }

    public void setPassportValidate(Date passportValidate) {
        this.passportValidate = passportValidate;
    }

    public CreditCardType getCreditCardType() {
        return creditCardType;
    }

    public void setCreditCardType(CreditCardType creditCardType) {
        this.creditCardType = creditCardType;
    }
}

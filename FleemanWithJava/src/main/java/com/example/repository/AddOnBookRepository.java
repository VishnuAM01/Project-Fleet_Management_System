package com.example.repository;


import org.springframework.data.jpa.repository.JpaRepository;

import com.example.models.AddonBook;

public interface AddOnBookRepository extends JpaRepository<AddonBook, Integer> {

}
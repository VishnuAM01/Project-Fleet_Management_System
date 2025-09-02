package com.example.service;

import com.example.dto.InvoiceRequestDTO;
import com.example.models.InvoiceHeaderTable;

import java.util.List;


import com.example.dto.InvoiceResponseDTO;

public interface InvoiceHeaderTableService {
    //List<InvoiceHeaderTable> getByBookingId(Integer bookingId);
    //List<InvoiceHeaderTable> listAll();
    //InvoiceHeaderTable update(Integer invoiceId, InvoiceHeaderTable updated);
    //void delete(Integer invoiceId);
    InvoiceResponseDTO create(InvoiceRequestDTO request);
   // InvoiceHeaderTable getById(Integer invoiceId);

    // Add this method:
   // InvoiceResponseDTO getInvoiceDetailsById(Integer invoiceId);
    InvoiceResponseDTO getById(Integer invoiceId);
    InvoiceResponseDTO getByBookingId(Integer bookingId); 

}

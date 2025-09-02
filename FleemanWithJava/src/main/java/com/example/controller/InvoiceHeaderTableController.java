package com.example.controller;

import com.example.dto.InvoiceRequestDTO;
import com.example.dto.InvoiceResponseDTO;
import com.example.models.BookingDetails;
import com.example.models.VehicleDetails;
import com.example.service.InvoiceHeaderTableService;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

@CrossOrigin(origins ="*")
@RestController
@RequestMapping("/api/invoices")
public class InvoiceHeaderTableController {

    @Autowired
    private InvoiceHeaderTableService service;

    @PostMapping  // No need to repeat /api/invoices here
    public InvoiceResponseDTO createInvoice(@RequestBody InvoiceRequestDTO request) {
        return service.create(request);  // use 'service', not 'invoiceHeaderTableService'
    }
    
    @GetMapping("/{invoiceId}")
    public InvoiceResponseDTO getInvoiceById(@PathVariable("invoiceId") Integer invoiceId) {
        InvoiceResponseDTO invoice = service.getById(invoiceId);
        if (invoice == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Invoice not found");
        }
        return invoice;
    }
    @GetMapping("/booking/{bookingId}")
    public InvoiceResponseDTO getInvoiceByBookingId(@PathVariable("bookingId") Integer bookingId) {
        return service.getByBookingId(bookingId);
    }



}

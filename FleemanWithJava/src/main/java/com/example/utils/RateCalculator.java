package com.example.utils;

import java.time.LocalDate;
import java.time.temporal.ChronoUnit;

public class RateCalculator {

    public static float calculateRate(LocalDate pickupDate, LocalDate returnDate,
                                      float dailyRate, float weeklyRate, float monthlyRate) {
        long totalDays = ChronoUnit.DAYS.between(pickupDate, returnDate);
        float totalRate = 0;

        if (totalDays < 7) {
            totalRate = totalDays * dailyRate;
        } else if (totalDays < 30) {
            long weeks = totalDays / 7;
            long remainingDays = totalDays % 7;
            totalRate = (weeks * weeklyRate) + (remainingDays * dailyRate);
        } else {
            long months = totalDays / 30;
            long remainingDays = totalDays % 30;
            long weeks = remainingDays / 7;
            long days = remainingDays % 7;
            totalRate = (months * monthlyRate) + (weeks * weeklyRate) + (days * dailyRate);
        }

        return totalRate;
    }
}


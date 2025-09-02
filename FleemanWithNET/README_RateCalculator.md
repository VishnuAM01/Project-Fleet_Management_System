# RateCalculator Utility

The `RateCalculator` utility class provides methods for calculating rental rates and costs based on time periods and pricing tiers.

## Methods

### 1. CalculateRate (Main Method)
**`CalculateRate(DateTime pickup, DateTime dropoff, double dailyRate, double weeklyRate, double monthlyRate)`**

Calculates the total rental cost using tiered pricing (daily, weekly, monthly rates).

**Parameters:**
- `pickup`: Start date of the rental
- `dropoff`: End date of the rental
- `dailyRate`: Daily rental rate
- `weeklyRate`: Weekly rental rate (7 days)
- `monthlyRate`: Monthly rental rate (30 days)

**Logic:**
1. **Monthly Tier**: If rental is 30+ days, apply monthly rate
2. **Weekly Tier**: For remaining days ≥ 7, apply weekly rate
3. **Daily Tier**: For remaining days, apply daily rate

**Example:**
```csharp
var cost = RateCalculator.CalculateRate(
    pickup: new DateTime(2024, 1, 10),
    dropoff: new DateTime(2024, 1, 25),
    dailyRate: 30.00,
    weeklyRate: 180.00,
    monthlyRate: 720.00
);
// Result: 15 days = 2 weeks + 1 day = (2 × 180) + (1 × 30) = $390.00
```

### 2. CalculateDailyRate
**`CalculateDailyRate(DateTime pickup, DateTime dropoff, double dailyRate)`**

Simple daily rate calculation without tiered pricing.

**Example:**
```csharp
var cost = RateCalculator.CalculateDailyRate(
    pickup: new DateTime(2024, 1, 10),
    dropoff: new DateTime(2024, 1, 15),
    dailyRate: 25.00
);
// Result: 5 days × $25 = $125.00
```

### 3. CalculateAddonRate
**`CalculateAddonRate(DateTime startDate, DateTime endDate, double addonPrice)`**

Calculates addon costs based on duration and daily price.

**Example:**
```csharp
var addonCost = RateCalculator.CalculateAddonRate(
    startDate: new DateTime(2024, 1, 10),
    endDate: new DateTime(2024, 1, 15),
    addonPrice: 5.00
);
// Result: 5 days × $5 = $25.00
```

### 4. GetTotalDays
**`GetTotalDays(DateTime startDate, DateTime endDate)`**

Utility method to calculate total days between two dates (inclusive).

**Example:**
```csharp
var days = RateCalculator.GetTotalDays(
    startDate: new DateTime(2024, 1, 10),
    endDate: new DateTime(2024, 1, 15)
);
// Result: 6 days (inclusive of both start and end dates)
```

## Usage in Invoice Service

The `RateCalculator` is used in the `InvoiceService` for:

### Car Rental Calculation
```csharp
var carRentalAmount = RateCalculator.CalculateRate(
    booking.PickupDate ?? DateTime.MinValue,
    currentDateTime,
    vehicleDetails.DailyRate,
    vehicleDetails.WeeklyRate,
    vehicleDetails.MonthlyRate
);
```

### Addon Rental Calculation
```csharp
addonRentalAmount += RateCalculator.CalculateAddonRate(
    booking.BookingDate ?? currentDateTime,
    currentDateTime,
    addonDetails.addOnPrice
);
```

## Business Logic

### Tiered Pricing Strategy
- **Monthly Rate**: Best value for long-term rentals (30+ days)
- **Weekly Rate**: Competitive pricing for medium-term rentals (7-29 days)
- **Daily Rate**: Standard pricing for short-term rentals (1-6 days)

### Date Calculation
- **Inclusive Counting**: Both start and end dates are counted
- **Minimum 1 Day**: Even same-day rentals count as 1 day
- **Date Precision**: Uses date portion only (ignores time)

## Examples

### Short Rental (3 days)
```csharp
// 3 days × $30 = $90.00
var cost = RateCalculator.CalculateRate(
    new DateTime(2024, 1, 10),
    new DateTime(2024, 1, 12),
    30.00, 180.00, 720.00
);
```

### Medium Rental (10 days)
```csharp
// 1 week × $180 + 3 days × $30 = $180 + $90 = $270.00
var cost = RateCalculator.CalculateRate(
    new DateTime(2024, 1, 10),
    new DateTime(2024, 1, 19),
    30.00, 180.00, 720.00
);
```

### Long Rental (35 days)
```csharp
// 1 month × $720 + 5 days × $30 = $720 + $150 = $870.00
var cost = RateCalculator.CalculateRate(
    new DateTime(2024, 1, 10),
    new DateTime(2024, 2, 13),
    30.00, 180.00, 720.00
);
```

## Error Handling

- **Null Dates**: Method handles null dates gracefully
- **Invalid Ranges**: Ensures end date is not before start date
- **Zero Rates**: Handles zero or negative rates (though not recommended)

## Performance Notes

- **Efficient Calculations**: Uses simple arithmetic operations
- **No Database Calls**: All calculations performed in memory
- **Scalable**: Handles any date range efficiently

## Integration Points

The `RateCalculator` is integrated with:
1. **InvoiceService**: For car rental and addon calculations
2. **Booking System**: For upfront cost estimates
3. **Pricing Engine**: For dynamic rate calculations
4. **Reporting System**: For cost analysis and reporting

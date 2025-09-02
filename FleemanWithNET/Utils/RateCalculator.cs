namespace fleeman_with_dot_net.Utils
{
    public static class RateCalculator
    {
        public static double CalculateRate(DateTime pickup, DateTime dropoff, double dailyRate, double weeklyRate, double monthlyRate)
        {
            var totalDays = (dropoff.Date - pickup.Date).Days + 1;
            double totalCost = 0;

            if (totalDays >= 30)
            {
                int months = totalDays / 30;
                totalCost += months * monthlyRate;
                totalDays %= 30;
            }
            if (totalDays >= 7)
            {
                int weeks = totalDays / 7;
                totalCost += weeks * weeklyRate;
                totalDays %= 7;
            }
            totalCost += totalDays * dailyRate;

            return totalCost;
        }

        public static double CalculateDailyRate(DateTime pickup, DateTime dropoff, double dailyRate)
        {
            var totalDays = (dropoff.Date - pickup.Date).Days + 1;
            return totalDays * dailyRate;
        }

        public static double CalculateAddonRate(DateTime startDate, DateTime endDate, double addonPrice)
        {
            var totalDays = (endDate.Date - startDate.Date).Days + 1;
            return totalDays * addonPrice;
        }

        public static int GetTotalDays(DateTime startDate, DateTime endDate)
        {
            return (endDate.Date - startDate.Date).Days + 1;
        }
    }
}

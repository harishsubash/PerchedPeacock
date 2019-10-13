using System;

namespace PerchedPeacock.Domain
{
    public class ParkingCharge
    {
        private readonly int _dailyRate;
        private readonly int _hourlyRate;

        public ParkingCharge(int dailyRate, int hourlyRate)
        {
            _dailyRate = dailyRate;
            _hourlyRate = hourlyRate;
        }
        public int CalculateCharge(DateTime startDateTime, DateTime endDateTime)
        {
            var hours = Math.Ceiling((endDateTime - startDateTime).TotalHours);
            int amount = _hourlyRate;

            if (hours <= 8)
            {
                amount = (int)hours * _hourlyRate;
            }
            else if (hours > 8 && hours <= 24)
            {
                amount = _dailyRate;
            }
            else if (hours > 24)
            {
                var numberOfDays = Math.Ceiling((endDateTime - startDateTime).TotalDays);
                amount = (int)numberOfDays * _dailyRate;
            }

            return amount;

        }
    }
}

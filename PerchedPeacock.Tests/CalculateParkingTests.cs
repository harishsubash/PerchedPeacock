using NUnit.Framework;
using PerchedPeacock.Domain;
using System;
using System.Globalization;
using TechTalk.SpecFlow;

namespace PerchedPeacock.Tests
{
    [Binding]
    public class CalculateParkingTests
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Hourly { get; set; }
        public int Daily { get; set; }

        private ParkingCharge parkingCharge;

        CultureInfo culture = new CultureInfo("en-IN");

        [Given(@"Start Date Time is (.*)")]
        public void GivenStartDateTimeIsAM(string dateTime)
        {
            StartTime = Convert.ToDateTime(dateTime, culture);
        }

        [Given(@"End Date Time is (.*)")]
        public void GivenEndDateTimeIsAM(string dateTime)
        {
            EndTime = Convert.ToDateTime(dateTime, culture);
        }

        [Given(@"hourly rate is (.*) and daily rate is (.*)")]
        public void GivenHourlyRateIsAndDailyRateIs(int hourly, int daily)
        {
            Hourly = hourly;
            Daily = daily;
        }

        [When(@"I calculate")]
        public void WhenICalculate()
        {
            parkingCharge = new ParkingCharge(Daily, Hourly);
        }

        [Then(@"the result should be (.*)")]
        public void ThenTheResultShouldBe(int expected)
        {
            var actual = parkingCharge.CalculateCharge(StartTime, EndTime);
            Assert.AreEqual(expected, actual);
        }


    }
}

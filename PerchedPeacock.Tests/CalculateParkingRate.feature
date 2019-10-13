Feature: CalculateParkingRate
	Feature file to caculate the parking rate 

Scenario: Calculate Parking charges for less than 8 hours
	Given Start Date Time is 1/1/2019 08:00:00 AM
	And End Date Time is 1/1/2019 11:00AM
	And hourly rate is 10 and daily rate is 100
	When I calculate
	Then the result should be 30

Scenario: Calculate Parking charges for less than 8 hours and different parking rate
	Given Start Date Time is 1/1/2019 08:00:00 AM
	And End Date Time is 1/1/2019 2:00:00PM
	And hourly rate is 15 and daily rate is 120
	When I calculate
	Then the result should be 90

Scenario: Calculate Parking charges for greater then 8 hours and less than 24 hours
	Given Start Date Time is 1/1/2019 08:00:00 AM
	And End Date Time is 1/1/2019 08:00PM
	And hourly rate is 10 and daily rate is 100
	When I calculate
	Then the result should be 100

Scenario: Calculate Parking charges for greater 24 hours is daily rate 100
	Given Start Date Time is 1/1/2019 08:00:00 AM
	And End Date Time is 2/1/2019 11:00AM
	And hourly rate is 10 and daily rate is 100
	When I calculate
	Then the result should be 200


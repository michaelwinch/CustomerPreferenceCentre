# CustomerPreferenceCentre

This is a command line tool that takes a list of customers and generates a report on which customers to send marketing materials to based on their configured schedules.

Run this with two parameters;
1. the file path of the input file
2. the output directory

N.B. these paths cannot contain spaces due to time constraints, will be fixed in the future.

The input file should be a json file of a list of customers.
A customer has the properties:
- Name (string)
- EmailAddress (string)
- MarketingSchedule (string)

The marketing schedule has the valid values:
- "Never"
- "Daily"
- "DayOfMonth-x" where x is a number between 1 and 28 inclusive
- "DaysOfWeek-x" where x is a number between 0 and 6 inclusive, 0 being Sunday and 6 being Saturday

Entering an invalid marketing schedule will output validations in the console and the report will not be generated.

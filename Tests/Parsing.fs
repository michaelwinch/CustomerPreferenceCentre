module Tests.Parsing
open System
open NUnit.Framework
open CustomerPreferenceCentre

[<Test>]
let ``Schedule should correctly parse never`` () =
    Assert.AreEqual (Some Never, Schedule.tryParse "Never")
[<Test>]
let ``Schedule should correctly parse daily`` () =
    Assert.AreEqual (Some Daily, Schedule.tryParse "Daily")

let validDaysOfMonth = [1..28]
[<TestCaseSource(nameof(validDaysOfMonth))>]
let ``Schedule should correctly parse day of month`` (day: int) =
    Assert.AreEqual (Some (DayOfMonth (DayOfMonth.create day)), Schedule.tryParse ("DayOfMonth-" + day.ToString()))

[<Test>]
let ``Day of month must not be greater than 28`` () =
    Assert.AreEqual (None, Schedule.tryParse "DayOfMonth-29")

[<Test>]
let ``Day of month must not be less than 1`` () =
    Assert.AreEqual (None, Schedule.tryParse "DayOfMonth-0")

let validDaysOfWeek = [int DayOfWeek.Sunday .. int DayOfWeek.Saturday]
[<TestCaseSource(nameof(validDaysOfWeek))>]
let ``Schedule should correctly parse day of week`` (day: int) =
    Assert.AreEqual (Some (DaysOfWeek (Set [enum<DayOfWeek> day])), Schedule.tryParse ("DaysOfWeek-" + day.ToString()))

[<Test>]
let ``Schedule should correctly parse multiple days of the week`` () =
    let expected = Some (DaysOfWeek (Set [DayOfWeek.Tuesday; DayOfWeek.Saturday]))
    Assert.AreEqual (expected, Schedule.tryParse "DaysOfWeek-2,6")
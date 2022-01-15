module Tests.Projections
open System
open NUnit.Framework
open CustomerPreferenceCentre

let allDates =
    [0..30]
    |> Seq.map (fun x -> DateTime(2022, 1, 1).AddDays(float x))

[<TestCaseSource(nameof(allDates))>]
let ``Schedule daily should always send`` (date: DateTime) =
    Projections.shouldSend date Schedule.Daily
    |> Assert.IsTrue

[<TestCaseSource(nameof(allDates))>]
let ``Schedule never should never send`` (date: DateTime) =
    Projections.shouldSend date Schedule.Never
    |> Assert.IsFalse


let dayOfMonthDates =
    allDates
    |> Seq.map (fun x ->
        [|
            x :> obj
            x = DateTime(2022, 1, 7) :> obj
        |])

[<TestCaseSource(nameof(dayOfMonthDates))>]
let ``Schedule day of month should only send on given day`` (date: DateTime) is7th =
    let actual = Projections.shouldSend date (Schedule.DayOfMonth (DayOfMonth.create 7))
    Assert.AreEqual (is7th, actual)


let daysOfWeekDates =
    allDates
    |> Seq.map (fun x ->
        [|
            x :> obj
            (x.DayOfWeek = DayOfWeek.Tuesday || x.DayOfWeek = DayOfWeek.Saturday) :> obj
        |])

[<TestCaseSource(nameof(daysOfWeekDates))>]
let ``Schedule days of week should send on all given days`` (date: DateTime) isTueOrSat =
    let daysOfWeek = Set [DayOfWeek.Tuesday; DayOfWeek.Saturday]
    let actual = Projections.shouldSend date (Schedule.DaysOfWeek daysOfWeek)
    Assert.AreEqual (isTueOrSat, actual)

[<TestCaseSource(nameof(daysOfWeekDates))>]
let ``Schedule days of week should not send on non selected days`` (date: DateTime) isTueOrSat =
    let daysOfWeek = Set [DayOfWeek.Monday; DayOfWeek.Wednesday; DayOfWeek.Thursday; DayOfWeek.Friday; DayOfWeek.Sunday]
    let actual = Projections.shouldSend date (Schedule.DaysOfWeek daysOfWeek)
    Assert.AreEqual (not isTueOrSat, actual)
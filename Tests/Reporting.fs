module Tests.Reporting
open System
open NUnit.Framework
open CustomerPreferenceCentre


[<Test>]
let ``Should generate correct number of records`` () =
    Reporting.getDatesInRange (DateTime(2022, 1, 1)) (DateTime(2022, 1, 7))
    |> List.length
    |> fun count -> Assert.AreEqual(7, count)

[<Test>]
let ``Should generate no records if end date is before start date`` () =
    Reporting.getDatesInRange (DateTime(2022, 1, 2)) (DateTime(2022, 1, 1))
    |> List.length
    |> fun count -> Assert.AreEqual(0, count)
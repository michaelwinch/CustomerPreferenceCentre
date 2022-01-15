module Tests.Domain
open System
open NUnit.Framework
open CustomerPreferenceCentre

[<Test>]
let ``Day of month must not be greater than 28`` () =
    Assert.Throws<ArgumentException>(fun () -> DayOfMonth.create 29 |> ignore)
    |> ignore

[<Test>]
let ``Day of month must not be less than 1`` () =
    Assert.Throws<ArgumentException>(fun () -> DayOfMonth.create 0 |> ignore)
    |> ignore

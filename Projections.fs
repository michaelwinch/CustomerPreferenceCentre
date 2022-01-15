module CustomerPreferenceCentre.Projections
open System


let shouldSend (date: DateTime) =
    function
    | DayOfMonth selectedDay -> date.Day = DayOfMonth.unwrap selectedDay
    | DaysOfWeek selectedDays -> selectedDays |> Set.contains date.DayOfWeek
    | Daily -> true
    | Never -> false
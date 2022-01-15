module CustomerPreferenceCentre.Reporting
open System
open CustomerPreferenceCentre.Domain


type Record =
    {
        Date: DateTime
        Customers: string list
    }

type ReportData =
    {
        Name: string
        Content: Record list
    }

let getDatesInRange (startDate: DateTime) (endDate: DateTime) =
    List.unfold
        (fun currentDate ->
            if currentDate <= endDate then
                Some (currentDate, currentDate.AddDays 1.)
            else
                None
        )
        startDate

let generateContent (startDate: DateTime) (endDate: DateTime) customers : Record list =
    [
        for date in getDatesInRange startDate endDate do
            {
                Date = date
                Customers =
                    customers
                    |> List.filter (fun x -> Projections.shouldSend date x.MarketingSchedule)
                    |> List.map (fun x -> x.Name)
            }
    ]

let generate90DayReport (customers) =
    {
        Name = "90 Day Report"
        Content = generateContent DateTime.Today.Date (DateTime.Today.AddDays(90.)) customers
    }
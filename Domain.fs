[<AutoOpen>]
module CustomerPreferenceCentre.Domain
open System


type DayOfMonth = private DayOfMonth of int

module DayOfMonth =
    let unwrap = function DayOfMonth x -> x

    let create dayOfMonth =
        if dayOfMonth < 1 || dayOfMonth > 28 then
            invalidArg "dayOfMonth" "day of month must be between 1 - 28"
        else
            DayOfMonth dayOfMonth


type Schedule =
    | DayOfMonth of DayOfMonth
    | DaysOfWeek of DayOfWeek Set
    | Daily
    | Never

module Schedule =
    let private getPayload (str: string) =
        str.IndexOf '-'
        |> (+) 1
        |> str.Substring

    let (|DAY_OF_MONTH|_|) (str: string) =
        if str.StartsWith "DayOfMonth-" then
            let t =
                getPayload str
                |> Int32.TryParse
                |> function
                    | true, x -> Some x
                    | _ -> None
            t
            |> Option.bind (fun x -> if x >= 1 && x <= 28 then Some (DayOfMonth.create x) else None)
        else None

    let (|DAYS_OF_WEEK|_|) (str: string) =
        if str.StartsWith "DaysOfWeek-" then
            let parsedInts =
                getPayload str
                |> fun x -> x.Split ','
                |> Seq.map Int32.TryParse

            if parsedInts |> Seq.exists (fun x -> fst x = false) then None
            else
                parsedInts
                |> Seq.map (snd >> enum<DayOfWeek>)
                |> Set
                |> Some
        else None

    let tryParse =
        function
        | DAY_OF_MONTH day -> Some (DayOfMonth day)
        | DAYS_OF_WEEK days -> Some (DaysOfWeek days)
        | "Daily" -> Some Daily
        | "Never" -> Some Never
        | _ -> None


type UnvalidatedCustomer =
    {
        Name: string
        EmailAddress: string
        MarketingSchedule: string
    }

type Customer =
    {
        Name: string
        EmailAddress: string
        MarketingSchedule: Schedule
    }
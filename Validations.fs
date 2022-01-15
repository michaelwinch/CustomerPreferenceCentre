module CustomerPreferenceCentre.Validations

// Would usually do ROP here but I did something basic for the sake of time
let validateCustomer (customer: UnvalidatedCustomer) : Result<Customer, string * string> =
    let schedule = Schedule.tryParse customer.MarketingSchedule
    match schedule with
    | Some x ->
        Result.Ok {
            Name = customer.Name
            EmailAddress = customer.EmailAddress
            MarketingSchedule = x
        }
    | None -> Result.Error (customer.Name, sprintf "schedule '%s' not valid" customer.MarketingSchedule)

let validateCustomers (customers: UnvalidatedCustomer list) : Result<Customer list, (string * string) list> =
    let results = customers |> List.map validateCustomer
    List.fold
        (fun acc res ->
            match acc, res with
            | Result.Ok customers, Result.Ok customer -> Result.Ok (customer::customers)
            | Result.Error _, Result.Ok _ -> acc
            | Result.Ok _, Result.Error error -> Result.Error [error]
            | Result.Error errors, Result.Error error -> Result.Error (error::errors)
        )
        (Result.Ok [])
        results
open System
open CustomerPreferenceCentre

type Args =
    {
        InputFilePath: string
        OutputDirectory: string
    }


[<EntryPoint>]
let main argv =
    let args =
        {
            InputFilePath = argv.[0]
            OutputDirectory = argv.[1]
        }

    let inputResult =
        IO.readObjectFromFile<UnvalidatedCustomer list> args.InputFilePath
        |> Validations.validateCustomers

    match inputResult with
    | Result.Ok customers ->
        customers
        |> Reporting.generate90DayReport
        |> (fun x -> IO.outputFile args.OutputDirectory x.Name x.Content)
    | Result.Error errors ->
        Console.WriteLine("There were errors with the input file, outputted below")
        errors
        |> List.iter (fun (name, errorMessage) -> Console.WriteLine (sprintf "%s: %s" name errorMessage))

    0 // return an integer exit code
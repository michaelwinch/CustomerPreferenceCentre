module CustomerPreferenceCentre.IO
open System.IO
open Newtonsoft.Json


let (^^) x y = Path.Combine(x,y)

let readObjectFromFile<'a> filePath =
    File.ReadAllText filePath
    |> JsonConvert.DeserializeObject<'a>

let outputFile outputDirectory fileName content =
    let filePath = outputDirectory ^^ fileName + ".json"

    let serializer = JsonSerializer()
    use sw = new StreamWriter(filePath)
    use jw = new JsonTextWriter(sw)
    serializer.Serialize(jw, content)
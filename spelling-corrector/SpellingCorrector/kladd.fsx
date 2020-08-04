open System.IO
open System.Text.RegularExpressions

let words str =
    let rx = Regex @"\w+"
    rx.Matches str

let alltext = File.ReadAllText(@"../Data/small.txt")
let matches = words alltext
let matchToWord (wordMatch:Match) = wordMatch.Value
let s = Seq.map matchToWord (Seq.cast matches)
// val it : seq<string> = seq ["The"; "Project"; "Gutenberg"; "EBook"; ...]

let lower = (fun (str:string) -> str.ToLower()) 

let lowerSeq = Seq.map lower s


let l = ["a";"bad";"bad";"idea";"bad";"idea"]
List.filter (fun x -> x = "bad") l |> List.length

let countwords l word =
    List.filter (fun w -> w = word) l
    |> List.length

let count w =
    countwords 

let dict l =
    let count = countwords l
    List.distinct l
    |> List.map (fun w -> (w, count w))
    |> Map.ofList


let tst = seq { "i"; "am"; "a"; "a"; "and"; "i"; "am"; "also"; "a"; "sequence" } 
let testU = Seq.distinct tst

let wordmap words =
    let count = (fun word -> Seq.filter (fun w -> w = word) words |> Seq.length)
    Seq.distinct words
    |> Seq.map (fun w -> (w, count w))
    |> Map.ofSeq

// > wordmap tst;;                                                                    
// val it : Map<string,int> =
//   map
//     [("a", 3); ("also", 1); ("am", 2); ("and", 1); ("i", 2); ("sequence", 1)]

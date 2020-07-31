open System.IO
open System.Text.RegularExpressions

let words str =
    let rx = Regex @"\w+"
    rx.Matches str

let alltext = File.ReadAllText(@"../Data/micro.txt")
let matches = words alltext
let matchToWord (wordMatch:Match) = wordMatch.Value
let s = Seq.map matchToWord (Seq.cast matches)
// val it : seq<string> = seq ["The"; "Project"; "Gutenberg"; "EBook"; ...]

let lower = (fun (str:string) -> str.ToLower()) 
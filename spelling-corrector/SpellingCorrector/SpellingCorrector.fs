// Norvigs spelling corrector
// Mats Holm Summer 2020

namespace SpellingCorrector

open System.IO
open System.Text.RegularExpressions

module SC =

    let read path:string =
        File.ReadAllText(path)

    let words str =
        let rx = Regex @"\w+"
        rx.Matches str
        |> Seq.cast
        |> Seq.map (fun (w:Match) -> w.Value)

    let lower s =
        Seq.map (fun (str:string) -> str.ToLower()) s

    // tally occurrences of words in a sequence
    // [("am", 2); ("12", 1); ("a", 62); ...]
    let wordmap words =
        let count word = (Seq.filter (fun w -> w = word) words |> Seq.length)
        Seq.distinct words
        |> Seq.map (fun w -> (w, count w))
        |> Map.ofSeq

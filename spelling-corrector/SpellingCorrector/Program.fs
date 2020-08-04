// Norvigs spelling corrector
// Mats Holm Summer 2020

open System.IO
open System.Text.RegularExpressions

module SpellingCorrector =

    let read path:string =
        File.ReadAllText(path)

    let words str =
        let rx = Regex @"\w+"
        rx.Matches str
        |> Seq.cast
        |> Seq.map (fun (w:Match) -> w.Value)

    let path = @"../Data/micro.txt"

    let lower (str:string) =
        str.ToLower()

    // tally occurrences of words in a sequence
    // [("am", 2); ("12", 1); ("a", 62); ...]
    let wordmap words =
        let count = (fun word -> Seq.filter (fun w -> w = word) words |> Seq.length)
        Seq.distinct words
        |> Seq.map (fun w -> (w, count w))
        |> Map.ofSeq


[<EntryPoint>]
let main argv =    
    for str in argv do
        let words = SpellingCorrector.words str
        let lowered = Seq.map SpellingCorrector.lower words
        let WORDS = SpellingCorrector.wordmap lowered
        printfn "Words in '%s' is:\n" str
        for w in lowered do
            printfn "- %s\n" w
        Seq.iter (fun item -> printfn "%O" item) WORDS
        
    0

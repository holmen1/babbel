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


[<EntryPoint>]
let main argv =    
    for str in argv do
        let words = SpellingCorrector.words str
        let lowered = Seq.map SpellingCorrector.lower words
        printfn "Words in '%s' is:\n" str
        for w in lowered do
            printfn "- %s\n" w
    0

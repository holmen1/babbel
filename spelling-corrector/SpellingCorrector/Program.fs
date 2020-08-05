// Norvigs spelling corrector
// Mats Holm Summer 2020

open System.IO
open SpellingCorrector // SC

[<EntryPoint>]
let main argv =    
    for str in argv do
        let words = SC.words str
        let lowered = SC.lower words
        let WORDS = SC.wordmap lowered
        printfn "Words in '%s' is:\n" str
        for w in lowered do
            printfn "- %s\n" w
        Seq.iter (fun item -> printfn "%O" item) WORDS
        
    0

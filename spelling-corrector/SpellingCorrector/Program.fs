﻿// Norvigs spelling corrector
// Mats Holm Summer 2020

open System.IO
open SpellingCorrector // SC

[<EntryPoint>]
let main argv =    
    let wseq = SC.readfile (Array.item 0 argv)
    let WORDS = SC.wordmap wseq
    let e1 = SC.edit1 (Array.item 1 argv)
    let known = SC.known WORDS e1

    printfn "Known words is:\n"
    for w in known do
        printfn "- %s\n" w

    0

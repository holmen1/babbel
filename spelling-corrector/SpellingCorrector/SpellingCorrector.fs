// Norvigs spelling corrector
// Mats Holm Summer 2020

namespace SpellingCorrector

open System.IO
open System.Text.RegularExpressions

module SC =

    let letters = seq { for c in 'a'..'z' -> c }


// Utils

    let read path:string =
        File.ReadAllText(path)

    let words str =
        let rx = Regex @"\w+"
        rx.Matches str
        |> Seq.cast
        |> Seq.map (fun (w:Match) -> w.Value)

    let lower strings =
        Seq.map (fun (str:string) -> str.ToLower()) strings

    let readfile path =
        read path
        |> words
        |> lower

    // tally occurrences of words in a sequence
    // [("am", 2); ("12", 1); ("a", 62); ...]
    let wordmap words =
        words
        |> Seq.groupBy id
        |> Map.ofSeq
        |> Map.map (fun _ words -> words |> Seq.length)

    // word count in word dictionary (Map)
    let wc wmap = 
        Map.fold (fun acc _ value -> acc + value) 0 wmap

    let probability wmap word =
        let N = wmap |> wc
        float wmap.[word] / float N

    let known wmap words =
        wmap |> Map.toSeq |> Seq.map fst |> Set.ofSeq
        |> Set.intersect words

    let split (word:string) =
        let l = word.Length
        seq { for i in 0..l -> (word.[..i-1], word.[i..]) }

    // helper fcn
    // swaps first 2 chars in string
    let swap word =
        List.ofSeq word
        |> function |a::b::t -> b::a::t |a -> failwith "not enough elements" 
        |> List.toArray
        |> System.String


// Simple edits

    // remove one letter)
    let delete (wsplits:(string*string) seq) =
        Seq.zip wsplits (Seq.tail wsplits) // Seq.zip truncates longer seq
        |> Seq.map (fun z -> fst (fst z) + snd (snd z))
    
    // swap two adjacent letters
    let transpose (wsplits:(string*string) seq) =
        Seq.filter (fun wtuple -> String.length (snd wtuple) > 1) wsplits
        |> Seq.map (fun wtuple -> (fst wtuple) + (swap (snd wtuple)))

    // change one letter to another
    let replace wsplits =
        let z = Seq.zip wsplits (wsplits |> Seq.tail)
        let insertchar = (fun z c ->  z |> Seq.map (fun i -> fst (fst i) + (string c) + snd (snd i)))
        Seq.map (fun c -> insertchar z c |> Set.ofSeq) letters
        |> Set.unionMany
    
    // add a letter
    let insert wsplits =
        let insertchar = (fun w c ->  w |> Seq.map (fun i -> (fst i) + (string c) + (snd i)))
        Seq.map (fun c -> insertchar wsplits c |> Set.ofSeq) letters
        |> Set.unionMany


    // returns a set of all the edited strings that can be made with one simple edit:
    let edit1 word =
        let splits = split word
        delete splits
        |> Seq.append (transpose splits)
        |> Set.ofSeq
        |> Set.union (replace splits)
        |> Set.union (insert splits)
        


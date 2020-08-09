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

    // let P xs = raise (System.NotImplementedException("You haven't written a test yet!"))

    // word count in word dictionary (Map)
    let wc wmap = 
        Map.fold (fun acc _ value -> acc + value) 0 wmap

    let probability wmap word =
        let N = wmap |> wc
        float wmap.[word] / float N

    let known wmap words =
        let vocab = wmap |> Map.toSeq |> Seq.map fst |> Set.ofSeq
        words |> Set.ofSeq
        |> Set.intersect vocab
        |> Set.toSeq

    let split (word:string) =
        let l = word.Length
        seq { for i in 0..l -> (word.[..i-1], word.[i..]) }

    let deletes (wsplit:(string*string) seq) =
        Seq.zip wsplit (Seq.tail wsplit) // Seq.zip truncates longer seq
        |> Seq.map (fun z -> fst (fst z) + snd (snd z))

    // helper fcn in transpose
    // swaps first 2 chars in string
    let swap word =
        List.ofSeq word
        |> function |a::b::t -> b::a::t |a -> failwith "not enough elements" 
        |> List.toArray
        |> System.String

    // swap two adjacent letters
    let transpose (wseq:(string*string) seq) =
        Seq.filter (fun wtuple -> String.length (snd wtuple) > 1) wseq
        |> Seq.map (fun wtuple -> (fst wtuple) + (swap (snd wtuple)))

    let replace wseq =
        let letters = seq { for c in 'a'..'z' -> c }
        let z = Seq.zip wseq (wseq |> Seq.tail)
        let insertchar = (fun z c ->  z |> Seq.map (fun i -> fst (fst i) + (string c) + snd (snd i)))
        Seq.map (fun c -> insertchar z c |> Set.ofSeq) letters
        |> Set.unionMany

    let insert wseq =
        let letters = seq { for c in 'a'..'z' -> c }
        let insertchar = (fun w c ->  w |> Seq.map (fun i -> (fst i) + (string c) + (snd i)))
        Seq.map (fun c -> insertchar wseq c |> Set.ofSeq) letters
        |> Set.unionMany

    let replace word = raise (System.NotImplementedException("You haven't written a test yet!"))

    let insert word = raise (System.NotImplementedException("You haven't written a test yet!"))

    let edit1 word = raise (System.NotImplementedException("You haven't written a test yet!"))


// Norvigs spelling corrector
// Mats Holm Summer 2020

namespace SpellingCorrector

open System.IO
open System.Text.RegularExpressions

module SC =

    type WordMap = WordMap of Map<string,int>

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
    let wc (WordMap wm) = 
        Map.toSeq wm
        |> Seq.sumBy (fun kvp -> (snd kvp))

    let probability (WordMap WORDS) word =
        let N = WordMap WORDS |> wc
        float WORDS.[word] / float N
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
    let count word = (Seq.filter (fun w -> w = word) words |> Seq.length)
    Seq.distinct words
    |> Seq.map (fun w -> (w, count w))
    |> Map.ofSeq

// > wordmap tst;;                                                                    
// val it : Map<string,int> =
//   map
//     [("a", 3); ("also", 1); ("am", 2); ("and", 1); ("i", 2); ("sequence", 1)]


let m = wordmap tst
let ss = Map.toSeq m
Seq.map (fun kvp -> (snd kvp)) ss |> Seq.fold (+) 0 |> float // alt 1
Seq.map (fun kvp -> (snd kvp)) ss |> Seq.sum |> float // alt 2
Seq.sumBy (fun kvp -> (snd kvp)) ss |> float // alt 3 :)

wordmap tst |> Map.toSeq |> Seq.sumBy (fun kvp -> (snd kvp)) 
m |> Map.toSeq |> Seq.sumBy (fun kvp -> (snd kvp)) 

// Compute the sum of the squares of the elements of a list by using List.sumBy.
let sum2 = List.sumBy (fun elem -> elem*elem) [1 .. 10]

let wc myMap =
    Map.toSeq myMap
    |> Seq.sumBy (fun kvp -> (snd kvp))

// word count in word dictionary (Map)
type MapFn = Map<string,int> -> int
let wc:MapFn = (function wm ->
                            Map.toSeq wm
                            |> Seq.sumBy (fun kvp -> (snd kvp)))



type WordMap = WordMap of Map<string,int>

let wc2 = (function (WordMap wm) ->
                            Map.toSeq wm
                            |> Seq.sumBy (fun kvp -> (snd kvp)))

let wc3 (WordMap wm) = 
    Map.toSeq wm
    |> Seq.sumBy (fun kvp -> (snd kvp))


m |> Map.fold (fun state _ value -> state + value)  0
let wc4 wm = 
    Map.fold (fun state key value -> state + value)  0 wm

let WORDS = wordmap (seq { "i"; "am"; "a"; "a"; "and"; "i"; "am"; "also"; "a"; "sequence" })
            |> WordMap

let Probability (WordMap WORDS) word =
    float WORDS.[word] / float (wc WORDS)

let P = Probability WORDS

P "am"
// val it : float = 0.2
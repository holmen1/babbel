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

// error FS0071
(* let wc myMap =
    Map.toSeq myMap
    |> Seq.sumBy (fun kvp -> (snd kvp)) *)

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

let ws = seq { "i"; "am"; "a"; "mats"; "a" }

m |> Map.exists (fun key value -> key = "mats")
let known0 wmap word =
    Map.exists (fun key _ -> key = word) wmap

Seq.filter (fun w -> Map.exists (fun key _ -> key = w) m) ws
let known wmap words =
    let isKnown map word = Map.exists (fun key _ -> key = word) map
    Seq.filter (fun w -> isKnown wmap w) words

Set.ofSeq ws
let known2 wmap words =
    let vocab = wmap |> Map.toSeq |> Seq.map fst |> Set.ofSeq
    words |> Set.ofSeq
    |> Set.intersect vocab
    |> Set.toSeq


let str1 = "abcdef"
str1.[..2]
str1.[2..]
str1.Length
str1 |> String.iter (fun x->printf "%c" x)
str1 |> String.iteri (fun i x->printfn "i=%d, %c" i x)
str1 |> String.iteri (fun i s -> printfn "i=%d, %A" i (str1.[..i]))
// i=0, "a"
// i=1, "ab"
// i=2, "abc"
// i=3, "abcd"
// i=4, "abcde"
// i=5, "abcdef"

str1 |> String.iteri (fun i _ -> printfn "i=%d, %A" i (str1.[..i], str1.[i..]))
Seq.mapi (fun i _ -> (str1.[..i], str1.[i..])) str1

seq { for i in 0 .. str1.Length -> (str1.[..i-1], str1.[i-1..]) }

let str2 = "mats" 

str2 + string 'a'

let split (word:string) =
    let l = word.Length
    seq { for i in 0..l -> (word.[..i-1], word.[i..]) }

let splits = split "mats";;
splits |> Seq.map (fun t -> printfn "%O" t)


seq { for c in 'a'..'z' -> c }
let letters = seq { for c in 'a'..'z' -> c }

Seq.iter (fun s -> printfn "%s,%s" (fst s) (snd s)) splits
// ,mats
// m,ats
// ma,ts
// mat,s
// mats,

Seq.map (fun s -> printfn "%s,%s" (fst s) (snd s)) splits

Seq.iteri  (fun i s -> printfn "%d=   %s,%s" i (fst s) (snd s)) splits

splits |> Seq.item 0
// val it : string * string = ("", "mats")
Seq.item 0 splits

Seq.iteri  (fun i s -> printfn "%d=   %s" i ((fst s) + (snd s))) splits

splits |> Seq.tail

Seq.iteri  (fun i s -> printfn "%d=   %s" i ((fst (Seq.item i splits)) + (fst (Seq.item i splits)))) splits
// 0=   
// 1=   mm
// 2=   mama
// 3=   matmat
// 4=   matsmats

// Seq.iteri  (fun i s -> printfn "%d=   %s" i ((fst (Seq.item i splits)) + (fst (Seq.item (i+1) splits)))) splits
// index out of range

let l = Seq.length splits
seq { for i in 0..(l-2) -> (fst (Seq.item i splits)) + (snd (Seq.item (i+1) splits)) }

let deletes0 (s:(string*string) seq) =
    let l = Seq.length s
    seq { for i in 0..(l-2) -> (string (fst (Seq.item i s))) + (string (snd (Seq.item (i+1) s))) }

// better
splits
//  seq [("", "mats"); ("m", "ats"); ("ma", "ts"); ("mat", "s"); ...]
splits |> Seq.tail
//  seq [("m", "ats"); ("ma", "ts"); ("mat", "s"); ("mats", "")]
Seq.map2 (fun i1 i2 -> fst i1 + snd i2) splits (Seq.tail splits)

let deletes01 (wseq:(string*string) seq) =
    Seq.map2 (fun split nextsplit -> fst split + snd nextsplit) wseq (Seq.tail wseq)

Seq.zip splits (Seq.tail splits)

Seq.zip splits (Seq.tail splits)
|> Seq.map (fun i -> fst (fst i) + snd (snd i))


let deletes s =
    Seq.zip splits (Seq.tail splits)
    |> Seq.map (fun i -> fst (fst i) + snd (snd i))




['a';'b';'c']
|> function |a::b::t -> b::a::t |a -> failwith "not enough elements" 

List.ofSeq "abcd" |> List.toArray |> (fun s -> System.String s) |> printfn "%A"

['a';'b';'c'] |> List.toArray |> (fun s -> System.String s) |> printfn "%A"

let swap2 = function |a::b::t -> b::a::t |a -> failwith "not enough elements" 
// > swap2 ['m'; 'a'; 't'];;
// val it : char list = ['a'; 'm'; 't']

let swap word =
    List.ofSeq word
    |> function |a::b::t -> b::a::t |a -> failwith "not enough elements" 
    |> List.toArray
    |> System.String

let transpose (wseq:(string*string) seq) =
    Seq.filter (fun wtuple -> String.length (snd wtuple) > 1) wseq
    |> Seq.map (fun wtuple -> (fst wtuple) + (swap (snd wtuple)))



transpose splits
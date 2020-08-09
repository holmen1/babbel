namespace SpellingCorrector.Tests

open System
open NUnit.Framework
open SpellingCorrector


[<TestFixture>]
type TestClass () =

    [<Test>]
    member this.TestWords() =
        let hello = "Hello Mats Holm! Um, huh."
        let expected = seq { "Hello"; "Mats"; "Holm"; "Um"; "huh" }
        let actual = SC.words hello
        Assert.That(actual, Is.EqualTo(expected))

    [<Test>]
    member this.TestLower() =
        let hello = seq { "Hello"; "Mats"; "Holm"; "Um"; "huh" }
        let expected = seq { "hello"; "mats"; "holm"; "um"; "huh" }
        let actual = SC.lower hello
        Assert.That(actual, Is.EqualTo(expected))

    [<Test>]
    member this.TestWordMap() =
        let hello = seq { "hello"; "mats"; "holm"; "hello"; "hello"; "mats" }
        let expected = Map.empty.
                                Add("hello", 3).
                                Add("mats", 2).
                                Add("holm", 1)
        let actual = SC.wordmap hello
        Assert.That(actual, Is.EqualTo(expected))

    [<Test>]
    member this.TestP() =
        let wordmap = Map.empty.
                               Add("hello", 3).
                               Add("mats", 2).
                               Add("holm", 1)
        let P = SC.probability wordmap
        let expected = 2.0 / 6.0
        let actual = P "mats"
        Assert.That(actual, Is.EqualTo(expected))

    [<Test>]
    member this.TestKnown() =
        let wordmap = Map.empty.
                               Add("hello", 3).
                               Add("mats", 2).
                               Add("holm", 1)
        let hello = seq { "hello"; "mats"; "holmlund"; "hello"; "holm"; "mats" }
        let expected = seq { "hello"; "mats"; "holm"; }
        let actual = SC.known wordmap hello
        Assert.That(Set.ofSeq actual,Is.EqualTo(Set.ofSeq expected))

    [<Test>]
    member this.TestSplit() =
        let word = "abc"
        let expected = seq { ("","abc"); ("a", "bc"); ("ab", "c"); ("abc", "") }
        let actual = SC.split word
        Assert.That(actual, Is.EqualTo(expected))

    [<Test>]
    member this.TestDeletes() =
        let wordseq = seq { ("","mats"); ("m", "ats"); ("ma", "ts"); ("mat", "s"); ("mats", "") }
        let expected = seq { "ats"; "mts"; "mas"; "mat" }
        let actual = SC.deletes wordseq
        Assert.That(actual, Is.EqualTo(expected))

    [<Test>]
    member this.TestTranspose() =
        let wordseq = seq { ("","mats"); ("m", "ats"); ("ma", "ts"); ("mat", "s"); ("mats", "") }
        let expected = seq { "amts"; "mtas"; "mast" }
        let actual = SC.transpose wordseq
        Assert.That(actual, Is.EqualTo(expected))

    [<Test>]
    member this.TestEdit1() =
        let word = "somthing"
        let expected = 442
        let (ed1:int seq) = SC.edit1 word
        let actual = 0 //SC.edit1 word |> Seq.Length
        Assert.That(actual, Is.EqualTo(expected))

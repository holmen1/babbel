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


# spelling-corrector

## How It Works: Some Probability Theory

The call correction(w) tries to choose the most likely spelling correction for w. There is no way to know for sure (for example, should "lates" be corrected to "late" or "latest" or "lattes" or ...?), which suggests we use probabilities. We are trying to find the correction c, out of all possible candidate corrections, that maximizes the probability that c is the intended correction, given the original word w:

    argmaxc ∈ candidates P(c|w) 

By Bayes' Theorem this is equivalent to:

    argmaxc ∈ candidates P(c) P(w|c) / P(w) 

Since P(w) is the same for every possible candidate c, we can factor it out, giving:

    argmaxc ∈ candidates P(c) P(w|c) 

The four parts of this expression are:

*   Selection Mechanism: argmax
    We choose the candidate with the highest combined probability.

*   Candidate Model: c ∈ candidates
    This tells us which candidate corrections, c, to consider.

*   Language Model: P(c)
    The probability that c appears as a word of English text. For example, occurrences of "the" make up about 7% of English text, so we should have P(the) = 0.07.

*   Error Model: P(w|c)
    The probability that w would be typed in a text when the author meant c. For example, P(teh|the) is relatively high, but P(theeexyz|the) would be very low. 

https://norvig.com/spell-correct.html
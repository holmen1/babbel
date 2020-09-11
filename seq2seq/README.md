## NLP From Scratch: Translation with a Sequence to Sequence Network and Attention
https://pytorch.org/tutorials/intermediate/seq2seq_translation_tutorial.html#nlp-from-scratch-translation-with-a-sequence-to-sequence-network-and-attention
Original author: Sean Robertson


Attention allows the decoder network to “focus” on a different part of the encoder’s outputs for every step of the decoder’s own outputs.

![Alt Text](https://github.com/holmen1/babbel/blob/holmen1/seq/seq2seq/images/attention.png)

Calculating the attention weights is done with another feed-forward layer attn, using the decoder’s input and hidden state as inputs. 

![attention-decoder-network](https://github.com/holmen1/babbel/blob/holmen1/seq/seq2seq/images/attention-decoder-network.png)


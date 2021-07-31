# multiclass-sentiment-analysis

Exploring Amazon SageMaker Studio, to build, train and debug models, track experiments, deploy models in production.

Starting with the raw [Women's Clothing Reviews](https://www.kaggle.com/nicapotato/womens-ecommerce-clothing-reviews) dataset and prepare it to train a BERT-based natural language processing (NLP) model. The model will be used to classify customer reviews into positive (1), neutral (0) and negative (-1) sentiment.


{"features": ["I love this product!"]} -> Predicted class 1 with probability 0.9653520584106445

{"features": ["OK, but not great."]} -> Predicted class 0 with probability 0.5175402164459229

{"features": ["This is not the right product."]} -> Predicted class -1 with probability 0.7823373079299927

## Background
BERT: Pre-training of Deep Bidirectional Transformers for Language Understanding
Jacob Devlin, Ming-Wei Chang, Kenton Lee, Kristina Toutanova

https://arxiv.org/abs/1810.04805


## Notebooks

* 1.explore.ipynb

    We will analyze bias on the dataset, generate and analyze bias report, and prepare the dataset for the model training


* 2.transform.ipynb

    Converting the original review text into machine-readable features used by BERT


* 3.train.ipynb

    Train, Debug, and Profile a Machine Learning Model


* pipeline.ipynb (Putting it all together)

    Deploy an End-To-End Machine Learning pipeline


![Alt Text](https://github.com/holmen1/babbel/blob/master/multiclass-sentiment-analysis/images/bert_pipeline.png)



Pruned and modified version of programming assignment from 'Build, Train, and Deploy ML Pipelines using BERT' an online non-credit course authorized by DeepLearning.AI & Amazon Web Services and offered through Coursera
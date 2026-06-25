using System;
using System.Collections.Generic;

namespace CybersecurityChatbotGUI
{
    public enum Sentiment   
    {
        Neutral,
        Worried,
        Curious,
        Frustrated,
        Happy
    }

    public class SentimentDetector
    {
        private readonly Dictionary<Sentiment, List<string>> _triggerWords;

        public SentimentDetector()
        {
            _triggerWords = new Dictionary<Sentiment, List<string>>
            {
                [Sentiment.Worried] = new List<string>
                {
                    "worried", "scared", "afraid", "anxious", "nervous",
                    "unsafe", "concerned", "frightened", "panic", "fear"
                },
                [Sentiment.Curious] = new List<string>
                {
                    "curious", "wondering", "interested", "want to know",
                    "how does", "why", "what is", "tell me about", "learn"
                },
                [Sentiment.Frustrated] = new List<string>
                {
                    "frustrated", "annoyed", "confused", "don't understand",
                    "not working", "stupid", "hate", "angry", "fed up"
                },
                [Sentiment.Happy] = new List<string>
                {
                    "great", "thanks", "helpful", "awesome", "love it",
                    "excellent", "good", "fantastic", "perfect", "amazing"
                }
            };
        }

        public Sentiment Detect(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Sentiment.Neutral;

            string lowerInput = input.ToLower();

            foreach (var sentiment in _triggerWords)
            {
                foreach (string trigger in sentiment.Value)
                {
                    if (lowerInput.Contains(trigger))
                    {
                        return sentiment.Key;
                    }
                }
            }

            return Sentiment.Neutral;
        }

        public string GetSentimentOpener(Sentiment sentiment)
        {
            switch (sentiment)
            {
                case Sentiment.Worried:
                    return "😟 I understand your concern about online safety. It's normal to feel this way. Here's something that can help:";
                case Sentiment.Curious:
                    return "🤔 That's a great question! I'm glad you're curious about cybersecurity. Let me share this with you:";
                case Sentiment.Frustrated:
                    return "😤 I hear your frustration. Cybersecurity can feel overwhelming sometimes. Let me make this simple for you:";
                case Sentiment.Happy:
                    return "😊 I'm so glad you're finding this helpful! Here's more to keep you safe online:";
                case Sentiment.Neutral:
                default:
                    return "";
            }
        }
    }
}
using System;
using System.Collections.Generic;

namespace CybersecurityChatbotGUI
{
    public class ChatBot
    {
        private readonly KeywordResponder _keywordResponder;
        private readonly SentimentDetector _sentimentDetector;
        private readonly MemoryStore _memory;
        private bool _awaitingName;
        private string _lastTopic;
        private readonly Random _random;
        private readonly List<string> _fallbackResponses;
        private readonly List<string> _followUpPhrases;

        public ChatBot()
        {
            _keywordResponder = new KeywordResponder();
            _sentimentDetector = new SentimentDetector();
            _memory = new MemoryStore();
            _awaitingName = true;
            _lastTopic = string.Empty;
            _random = new Random();

            _fallbackResponses = new List<string>
            {
                "I'm not sure I understand. Could you rephrase that?",
                "I didn't quite catch that. Try asking about password safety, phishing, or privacy.",
                "Hmm, I'm still learning. Can you ask me about cybersecurity topics like passwords or scams?",
                "Not sure about that one! I specialise in cybersecurity - try 'Tell me about phishing' or 'How do I create a strong password?'"
            };

            _followUpPhrases = new List<string>
            {
                "tell me more", "explain more", "more tips", "another tip",
                "continue", "elaborate", "go on", "what else"
            };
        }

        public string GetGreeting()
        {
            string greeting = @"Hello! I'm your Cybersecurity Awareness Bot. 😊

🔒 I'm here to help you stay safe online!

What's your name?";
            return greeting;
        }

        public string ProcessInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "Please type something. I'm here to help you with cybersecurity!";

            if (_awaitingName)
            {
                string name = input.Trim();
                _memory.Store("name", name);
                _awaitingName = false;
                return $"Nice to meet you, {name}! 🎉\n\nI can help you with:\n• Password safety\n• Phishing protection\n• Spotting scams\n• Privacy tips\n• Safe browsing\n• Malware prevention\n• Software updates\n\nWhat would you like to learn about?";
            }

            foreach (string phrase in _followUpPhrases)
            {
                if (input.IndexOf(phrase, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    if (!string.IsNullOrWhiteSpace(_lastTopic))
                    {
                        string anotherTip = _keywordResponder.GetResponse(_lastTopic);
                        return $"Here's another tip about {_lastTopic}:\n\n{anotherTip}";
                    }
                    else
                    {
                        return "Ask me about a specific topic first like 'Tell me about passwords' or 'What is phishing?', then I can give you more tips on that!";
                    }
                }
            }

            Sentiment detectedSentiment = _sentimentDetector.Detect(input);
            string sentimentOpener = _sentimentDetector.GetSentimentOpener(detectedSentiment);

            string keywordResponse = _keywordResponder.GetResponse(input);

            if (keywordResponse != null)
            {
                string newTopic = _keywordResponder.GetLastTopicFromKeyword(input);
                if (!string.IsNullOrWhiteSpace(newTopic))
                {
                    _lastTopic = newTopic;

                    if (input.IndexOf("interested in", StringComparison.OrdinalIgnoreCase) >= 0 ||
                        input.IndexOf("I like", StringComparison.OrdinalIgnoreCase) >= 0 ||
                        input.IndexOf("tell me about", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        _memory.Store("topic", newTopic);
                    }
                }

                string personalisation = _memory.GetPersonalisedOpener();
                if (!string.IsNullOrWhiteSpace(sentimentOpener))
                {
                    return $"{sentimentOpener}\n\n{personalisation}{keywordResponse}";
                }
                else if (!string.IsNullOrWhiteSpace(personalisation))
                {
                    return $"{personalisation}{keywordResponse}";
                }
                else
                {
                    return keywordResponse;
                }
            }

            string lowerInput = input.ToLower();

            if (lowerInput.Contains("how are you"))
            {
                return "I'm doing great, thanks for asking! 🤖 I'm always ready to help you stay safe online. What cybersecurity topic would you like to learn about today?";
            }

            if (lowerInput.Contains("purpose") || lowerInput.Contains("what can you do") || lowerInput.Contains("what do you do"))
            {
                return "My purpose is to help you stay safe online! 🌐\n\nI can teach you about:\n• Creating strong passwords 🔐\n• Spotting phishing emails 🎣\n• Avoiding online scams ⚠️\n• Protecting your privacy 🛡️\n• Safe browsing habits 🌍\n• Preventing malware infections 🦠\n\nJust ask me about any of these topics!";
            }

            if (lowerInput.Contains("what can i ask") || lowerInput.Contains("what questions") || lowerInput.Contains("help"))
            {
                List<string> keywords = _keywordResponder.GetAllKeywords();
                string keywordList = string.Join(", ", keywords);
                return $"You can ask me about: {keywordList}\n\nOr try:\n• 'How are you?'\n• 'What is your purpose?'\n• 'Tell me more' (after I give you a tip)\n• 'Give me another tip'";
            }

            if (lowerInput.Contains("thank") || lowerInput.Contains("thanks"))
            {
                string name = _memory.UserName;
                if (!string.IsNullOrWhiteSpace(name))
                    return $"You're very welcome, {name}! 😊 Stay safe online, and come back anytime you need cybersecurity tips.";
                else
                    return $"You're very welcome! 😊 Stay safe online, and come back anytime you need cybersecurity tips.";
            }

            int fallbackIndex = _random.Next(_fallbackResponses.Count);
            return _fallbackResponses[fallbackIndex];
        }
    }
}
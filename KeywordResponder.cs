using System;
using System.Collections.Generic;
using System.Linq;

namespace CybersecurityChatbotGUI
{
    public class KeywordResponder   
    {
        private readonly Dictionary<string, List<string>> _responses;
        private readonly Random _random;

        public KeywordResponder()
        {
            _random = new Random();
            _responses = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

            _responses["password"] = new List<string>
            {
                "🔐 Use strong passwords with at least 12 characters, including numbers, symbols, and both cases.",
                "🔐 Never reuse passwords across different accounts. Use a password manager like Bitwarden or LastPass!",
                "🔐 Enable two-factor authentication (2FA) whenever possible for an extra layer of security.",
                "🔐 Avoid using personal info like birthdays or pet names in your passwords."
            };

            _responses["phishing"] = new List<string>
            {
                "🎣 Never click links in suspicious emails. Hover over links first to see the real URL.",
                "🎣 Legitimate companies won't ask for personal info via email. When in doubt, contact them directly.",
                "🎣 Check sender email addresses carefully - scammers use addresses that look almost real but have small typos.",
                "🎣 Look for urgent language like 'act now' or 'account suspended' - that's a major red flag."
            };

            _responses["scam"] = new List<string>
            {
                "⚠️ If an offer seems too good to be true, it probably is. Trust your instincts.",
                "⚠️ Never send money or gift cards to someone you haven't met in person.",
                "⚠️ Scammers create fake urgency. Always take time to verify before acting.",
                "⚠️ Report scams to the FTC or your local authorities to help protect others."
            };

            _responses["privacy"] = new List<string>
            {
                "🛡️ Review app permissions regularly - delete access for apps you no longer use.",
                "🛡️ Use a VPN on public Wi-Fi to encrypt your data and protect your privacy.",
                "🛡️ Regularly clear your browser cookies and use private browsing mode.",
                "🛡️ Be careful what you share on social media - oversharing helps attackers build profiles on you."
            };

            _responses["browsing"] = new List<string>
            {
                "🌐 Look for 'https://' and the padlock icon in the address bar before entering any personal info.",
                "🌐 Use ad blockers and avoid clicking pop-up ads - they often lead to malware infections.",
                "🌐 Keep your browser updated - security patches fix known vulnerabilities quickly.",
                "🌐 Consider using privacy-focused browsers like Firefox or Brave for better protection."
            };

            _responses["malware"] = new List<string>
            {
                "🦠 Don't download software from untrusted sources. Stick to official app stores like Microsoft Store.",
                "🦠 Run regular antivirus scans and keep your virus definitions updated weekly.",
                "🦠 Be careful with email attachments - even from people you know if they seem unexpected.",
                "🦠 Enable ransomware protection features if your security software offers it."
            };

            _responses["update"] = new List<string>
            {
                "📱 Enable automatic updates for your operating system and all applications.",
                "📱 Outdated software is a top target for hackers. Update immediately when prompted.",
                "📱 Don't ignore update notifications - they often fix critical security flaws being actively exploited.",
                "📱 Check for firmware updates on your router and smart devices too."
            };
        }

        public string GetResponse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            foreach (var keyword in _responses.Keys)
            {
                if (input.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    var responses = _responses[keyword];
                    int index = _random.Next(responses.Count);
                    return responses[index];
                }
            }

            return null;
        }

        public List<string> GetAllKeywords()
        {
            return _responses.Keys.ToList();
        }

        public string GetLastTopicFromKeyword(string input)
        {
            foreach (var keyword in _responses.Keys)
            {
                if (input.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    return keyword;
            }
            return null;
        }
    }
}

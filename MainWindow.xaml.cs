using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Input;

namespace CybersecurityChatbotGUI    
{
    public partial class MainWindow : Window
    {
        private ChatBot _chatBot;
        private MediaPlayer _mediaPlayer;
        private bool _awaitingNameResponse;

        public MainWindow()
        {
            InitializeComponent();

            _chatBot = new ChatBot();
            _awaitingNameResponse = true;

            LoadAsciiLogo();
            PlayVoiceGreeting();

            string greeting = _chatBot.GetGreeting();
            AppendBotMessage(greeting);
        }

        private void LoadAsciiLogo()
        {
            string asciiLogo = @"
   ██████╗██╗   ██╗██████╗ ███████╗██████╗ 
  ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗
  ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝
  ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗
  ╚██████╗   ██║   ██████╔╝███████╗██║  ██║
   ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝";

            AsciiLogo.Text = asciiLogo;
            AsciiLogo.FontFamily = new FontFamily("Consolas");
            AsciiLogo.FontSize = 8;
            AsciiLogo.Foreground = new SolidColorBrush(Color.FromRgb(255, 105, 180));
        }

        private void PlayVoiceGreeting()
        {
            try
            {
                string[] possiblePaths = {
                    "greeting.wav",
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Audio", "greeting.wav"),
                    Path.Combine(Directory.GetCurrentDirectory(), "greeting.wav")
                };

                string audioPath = null;
                foreach (string path in possiblePaths)
                {
                    if (File.Exists(path))
                    {
                        audioPath = path;
                        break;
                    }
                }

                if (audioPath != null)
                {
                    _mediaPlayer = new MediaPlayer();
                    _mediaPlayer.Open(new Uri(audioPath, UriKind.RelativeOrAbsolute));
                    _mediaPlayer.Play();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Voice greeting error: {ex.Message}");
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage();
            }
        }

        private void SendMessage()
        {
            string userMessage = UserInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(userMessage))
            {
                return;
            }

            string timestamp = DateTime.Now.ToString("HH:mm");

            // Add user message WITHOUT extra line break if it's the first message after bot question
            AppendUserMessage($"[{timestamp}] You: {userMessage}");

            UserInput.Text = "";

            string botResponse = _chatBot.ProcessInput(userMessage);
            string botTimestamp = DateTime.Now.ToString("HH:mm");

            AppendBotMessage($"[{botTimestamp}] 🤖 BOT: {botResponse}");

            ScrollToBottom();
        }

        private void QuickTopic_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string topic = button.Tag.ToString();

            string userMessage = $"Tell me about {topic}";
            string timestamp = DateTime.Now.ToString("HH:mm");

            AppendUserMessage($"[{timestamp}] You: {userMessage}");

            string botResponse = _chatBot.ProcessInput(userMessage);
            string botTimestamp = DateTime.Now.ToString("HH:mm");
            AppendBotMessage($"[{botTimestamp}] 🤖 BOT: {botResponse}");

            ScrollToBottom();
        }

        private void SaveChatHistory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string fileName = $"CyberGuard_Chat_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                string filePath = Path.Combine(desktopPath, fileName);

                string chatContent = "";
                foreach (var inline in ChatDisplay.Inlines)
                {
                    if (inline is Run run)
                    {
                        chatContent += run.Text + Environment.NewLine;
                    }
                }

                File.WriteAllText(filePath, chatContent);

                string timestamp = DateTime.Now.ToString("HH:mm");
                AppendBotMessage($"[{timestamp}] ✅ Chat history saved to Desktop as {fileName}");
                ScrollToBottom();
            }
            catch (Exception ex)
            {
                string timestamp = DateTime.Now.ToString("HH:mm");
                AppendBotMessage($"[{timestamp}] ❌ Failed to save chat: {ex.Message}");
                ScrollToBottom();
            }
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            string helpMessage = @"📚 CYBERGUARD HELP 📚

You can ask me about:
• Password safety
• Phishing scams
• Malware protection
• Safe browsing
• Online scams
• Privacy tips
• Software updates

Try these commands:
• 'tell me more' - Get another tip on the same topic
• 'what can I ask' - List all topics
• 'how are you' - Chat with me
• 'I'm worried about...' - I'll respond with empathy

Click any topic button on the left sidebar to get started!";

            string timestamp = DateTime.Now.ToString("HH:mm");
            AppendBotMessage($"[{timestamp}] 🤖 BOT: {helpMessage}");
            ScrollToBottom();
        }

        private void AppendUserMessage(string message)
        {
            // Add a single line break only if there's existing content
            if (ChatDisplay.Inlines.Count > 0)
            {
                ChatDisplay.Inlines.Add(new LineBreak());
            }

            Run userText = new Run(message);
            userText.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)); // White
            ChatDisplay.Inlines.Add(userText);
        }

        private void AppendBotMessage(string message)
        {
            // Add a single line break only if there's existing content
            if (ChatDisplay.Inlines.Count > 0)
            {
                ChatDisplay.Inlines.Add(new LineBreak());
            }

            Run botText = new Run(message);
            botText.Foreground = new SolidColorBrush(Color.FromRgb(255, 182, 193)); // Light pink
            ChatDisplay.Inlines.Add(botText);
        }

        private void ScrollToBottom()
        {
            ChatScrollViewer.ScrollToBottom();
        }
    }
}
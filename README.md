# 🛡️ CyberGuard Chatbot - Cybersecurity Awareness

A **WPF desktop application** that helps users learn about cybersecurity through an interactive chatbot, task management, a quiz, and an activity log.

---

## 📋 Table of Contents

- [Features](#-features)
- [Technologies Used](#-technologies-used)
- [Project Structure](#-project-structure)
- [Database Setup](#-database-setup)
- [How to Run](#-how-to-run)
- [Demo Commands](#-demo-commands)
- [Author](-Makwinja Tumelo)

---

## ✨ Features

### 🤖 Chatbot (Parts 1 & 2)
- Welcomes users and asks for their name
- Remembers user name and preferences
- Keyword recognition for cybersecurity topics (passwords, phishing, malware, etc.)
- Sentiment detection (worried, curious, frustrated, happy)
- Dynamic responses with personalization

### 📋 Task Assistant (Task 1)
- Add, view, delete, and complete cybersecurity tasks
- MySQL database integration for persistent storage
- **Reminder system** supporting:
  - `"in X days"` (e.g., remind me in 3 days)
  - `"in X weeks"` (e.g., remind me in 2 weeks)
  - `"tomorrow"`
  - `"next week"`
  - `"on YYYY-MM-DD"` (specific date)
  - `"day after tomorrow"`

### 🎮 Cybersecurity Quiz (Task 2)
- **12 questions** covering phishing, passwords, malware, social engineering, and 2FA
- Mix of **multiple-choice** and **True/False** questions
- **Immediate feedback** with explanations
- **Score tracking** with percentage-based feedback:
  - 90%+ → "🏆 Excellent! You're a cybersecurity pro!"
  - 70%+ → "🌟 Great job!"
  - 50%+ → "📚 Not bad!"
  - <50% → "💪 Keep learning!"

### 🧠 NLP Simulation (Task 3)
- Keyword detection using `string.Contains()` for flexible user input
- Understands variations like:
  - `"add task"`, `"add a task"`, `"new task"`, `"remind me"`
  - `"show tasks"`, `"view tasks"`, `"list tasks"`
  - `"complete task"`, `"mark done"`, `"mark completed"`
  - `"delete task"`, `"remove task"`
  - `"show log"`, `"activity log"`, `"what have you done"`
- Falls back to the original chatbot if no keyword matches

### 📊 Activity Log (Task 4)
- Logs every significant action with timestamps
- Tracks: tasks added/completed/deleted, quiz attempts, NLP interpretations, reminders, unknown input
- Displays last 10 actions in the side panel

### 🎵 Greeting Sound
- Plays `greeting.wav` on application startup

---

## 🛠️ Technologies Used

| Technology | Purpose |
| :--- | :--- |
| **C# / .NET Framework** | Core programming language |
| **WPF (XAML)** | GUI framework |
| **MySQL** | Database for task storage |
| **MySQL Connector/NET** | Database connectivity |
| **Regex (Regular Expressions)** | Reminder date parsing |

---

## 📂 Project Structure

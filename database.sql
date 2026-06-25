# Create database export file
echo "CREATE DATABASE IF NOT EXISTS CyberGuardDB;
USE CyberGuardDB;

CREATE TABLE IF NOT EXISTS Tasks (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Title VARCHAR(255) NOT NULL,
    Description TEXT,
    ReminderDate DATETIME,
    IsCompleted BOOLEAN DEFAULT FALSE
);" > database.sql

git add database.sql
git commit -m "Add database.sql for MySQL setup"

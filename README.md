# Weather Chatbot using Dialogflow and .NET C#

This is an intelligent chatbot that provides real-time weather updates based on user queries. It is built using **Google Dialogflow** as the NLP engine and **.NET C#** as the backend webhook. The chatbot integrates with the **OpenWeatherMap API** to fetch live weather data based on user location inputs.

## 💡 Features

- Understands natural language queries like:
  - "What's the weather in Karachi?"
  - "Tell me the temperature in London."
- Fetches real-time weather using OpenWeatherMap API
- NLP-based intent recognition using Google Dialogflow
- Backend implemented in ASP.NET Web API (C#)
- JSON parsing and structured response messages

## 🛠 Tech Stack

- Google Dialogflow
- .NET C# (ASP.NET Web API)
- OpenWeatherMap API
- RESTful Webhook Integration
- JSON

## ⚙️ How It Works

1. User sends a weather-related message via chatbot.
2. Dialogflow recognizes the intent and triggers webhook.
3. Webhook (C# API) calls OpenWeatherMap API using city parameter.
4. Weather data is parsed and returned to Dialogflow.
5. Dialogflow responds to the user with temperature, humidity, and conditions.

## 🔐 Prerequisites

- Dialogflow Agent (Importable from `/dialogflow-agent`)
- OpenWeatherMap API Key (Free at https://openweathermap.org/api)
- .NET Framework or .NET Core environment

## ✍️ Author

Sarim Ali

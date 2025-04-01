Overview

This project is a Math Challenge Game designed with a scalable and maintainable architecture. It follows software engineering best practices such as low coupling, high cohesion, event-driven programming, and interoperability. The system is structured into three tiers:

Backend API (Handles game logic and authentication)

Frontend MVC Application (User interface and interactions)

Database (Stores user data and scores)

Features

Math Challenge Game: Players solve math problems within 60 seconds, earning 100 points per correct answer.

Leaderboard: Displays top scores, keeping the highest-scoring player at the top.

Authentication & Authorization: Secure user authentication using .NET Identity Framework and JWT Tokens.

Event-Driven Frontend: Uses JavaScript to handle:

Button clicks and form validations dynamically.

AJAX and Fetch API for seamless communication.

Client-side validation before sending requests to the backend.

Interoperability:

Integrates with Banana API for additional functionalities.

Future-ready for integration with other frontends (Angular, React, etc.).

User Experience Enhancements:

Retro background music for immersion.

Modern UI elements (popups, alerts, interactive components).

Software Architecture

1. Low Coupling & High Cohesion

The system is divided into distinct layers to minimize dependencies.

Dependency Injection (DI) is used in the backend to decouple components.

Implements a base service class (IBaseService) to modularize API calls.

2. Event-Driven Programming

The project follows OOP principles (Encapsulation, Inheritance, Polymorphism).

Frontend JavaScript enhances interactivity and reduces full-page reloads.

3. Interoperability

Backend API is decoupled from the frontend to support multiple clients.

Designed for future front-end integrations such as an admin panel in React or Angular.

4. Virtual Identity & Security

Implements JWT Token Authentication.

Uses .NET Identity Framework for managing user authentication and authorization.

User data is securely stored in browser cookies.

Technologies Used

Backend: .NET Core API

Frontend: MVC (Razor Views, JavaScript, Bootstrap)

Database: MySQL

Authentication: .NET Identity Framework, JWT Tokens

API Integration: Banana API

Installation & Setup

Prerequisites

.NET Core SDK

MySQL Database

Steps to Run

Clone the repository:

git clone https://github.com/your-username/math-challenge-game.git
cd math-challenge-game

Set up the database and update the connection string in appsettings.json.

Run the backend API:

dotnet run

Start the frontend application:

dotnet run --project YourFrontendProject

Open the browser and navigate to http://localhost:5000.

Contributions

Feel free to fork this repository and submit pull requests with enhancements or bug fixes.

License

This project is licensed under the MIT License - see the LICENSE file for details.

Contact

For any questions or support, please open an issue on GitHub or reach out via email.

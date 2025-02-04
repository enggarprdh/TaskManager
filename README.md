# ETA Project

## Overview
The ETA project is a task management application built using .NET Framework 4.8 and C# 7.3. It allows users to create, update, and manage tasks with different statuses and priorities. The application includes features such as background task updates, file operations for task persistence, and a user-friendly interface for task management.

## Features
- **Task Management**: Create, update, and delete tasks.
- **Task Status Updates**: Automatically update task statuses to "In Progress" and "Completed" using background tasks.
- **File Operations**: Read from and write to JSON files for task persistence.
- **User Interface**: Windows Forms application for managing tasks.

## Project Structure
- **Core.cs**: Contains the core logic for reading, writing, and managing tasks in JSON files.
- **frmMain.cs**: The main form of the application, handles user interactions and background task updates.
- **frmModal.cs**: A modal form for adding tasks.
- **Properties/AssemblyInfo.cs**: Assembly information for the project.

## Getting Started
1. **Clone the repository**:
2. **Open the solution**: Open the project in Visual Studio.
3. **Build the project**: Build the solution to restore dependencies and compile the project.
4. **Run the application**: Start the application from Visual Studio.

## Usage
- **Add Task**: Click the "Add Task" button to open the modal form and add a new task.
- **Update Task**: Select a task from the list and update its status or details.
- **Delete Task**: Select a task from the list and click the "Delete" button to remove it.
- **Load Tasks**: Click the "Load" button to load tasks from a JSON file.

## Dependencies
- **Newtonsoft.Json**: Used for JSON serialization and deserialization.
- **.NET Framework 4.8**: The target framework for the project.



## License
This project is licensed under the MIT License.

## Authors
- **enggarprdh** - Initial work



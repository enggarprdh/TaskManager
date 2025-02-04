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
  ![image](https://github.com/user-attachments/assets/597f5f5d-3afc-4b36-8efb-36b184876cd3)

- **Mark as Completed Task**: Select a task from the list and update its status when the status is not completed.
![image](https://github.com/user-attachments/assets/4da43078-fd8d-49b7-beeb-5ed11d8b3732)
![image](https://github.com/user-attachments/assets/345a54a1-2c7f-4acc-9862-8faf5d4b1ab6)

- **Delete Task**: Select a task from the list and click the "Delete" button to remove it.
![image](https://github.com/user-attachments/assets/13da721b-d3c4-4483-b663-cba6bd2c8d0f)

- **Load Tasks**: Click the "Load" button to load tasks from a JSON file.
![image](https://github.com/user-attachments/assets/03fd453d-5d1a-4607-9345-1f9574f8767e)


## Dependencies
- **Newtonsoft.Json**: Used for JSON serialization and deserialization.
- **.NET Framework 4.8**: The target framework for the project.



## License
This project is licensed under the MIT License.

## Authors
- **enggarprdh** - Initial work



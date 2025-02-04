using ETA.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System;

namespace ETA
{
    public static class Core
    {
        private static string FILE_NAME = "taskList.json";
        /// <summary>
        /// Writes a TaskInfo object to the JSON file. If a task with the same TaskID exists, it updates the existing task.
        /// </summary>
        /// <param name="data">The TaskInfo object to write to the file.</param>
        public static void WriteToFile(TaskInfo data)
        {
            string json = File.ReadAllText(FILE_NAME);
            var taskList = JsonConvert.DeserializeObject<List<TaskInfo>>(json) ?? new List<TaskInfo>();
            if (taskList.Count > 1)
            {
                var existTask = taskList.FirstOrDefault((x) => x.TaskID == data.TaskID);
                if (existTask != null)
                {
                    existTask.TaskID = data.TaskID;
                    existTask.TaskName = data.TaskName;
                    existTask.Status = data.Status;
                    existTask.Priority = data.Priority;
                }
                else

                {
                    taskList.Add(data);
                }
            }
            else
            {
                taskList.Add(data);
            }
            string output = JsonConvert.SerializeObject(taskList, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(FILE_NAME, output);

        }
        /// <summary>
        /// Rewrites the entire JSON file with a new list of TaskInfo objects.
        /// </summary>
        /// <param name="data">The list of TaskInfo objects to write to the file.</param>
        public static void ReWriteToFile(List<TaskInfo> data)
        {
            string output = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(FILE_NAME, output);
        }
        /// <summary>
        /// Reads the list of TaskInfo objects from the JSON file.
        /// </summary>
        /// <returns>A list of TaskInfo objects read from the file.</returns>
        public static List<TaskInfo> ReadFromFile()
        {
            Core.CheckFile();
            string json = File.ReadAllText(FILE_NAME);
            var taskList = JsonConvert.DeserializeObject<List<TaskInfo>>(json) ?? new List<TaskInfo>();
            return taskList;
        }

        /// <summary>
        /// Loads a list of TaskInfo objects from a specified JSON file path. Validates the file and its contents.
        /// </summary>
        /// <param name="pathFile">The path of the JSON file to load.</param>
        /// <returns>A list of TaskInfo objects loaded from the file.</returns>
        /// <exception cref="Exception">Thrown if the file does not exist, is not a JSON file, or contains invalid data.</exception>
        public static List<TaskInfo> LoadFromFile(string pathFile)
        {
            if (!System.IO.File.Exists(pathFile))
                throw new Exception("File not found");

            if (Path.GetExtension(pathFile) != ".json")

                throw new Exception("Only can load from JSON file");

            Core.CheckFile();
            string json = File.ReadAllText(pathFile);
            var taskList = JsonConvert.DeserializeObject<List<TaskInfo>>(json);

            if (taskList == null)
                throw new Exception("Can not load file");

            var countInvalidTaskId = taskList.Count(x => string.IsNullOrEmpty(x.TaskID));
            if (countInvalidTaskId > 0)
                throw new Exception("Can not load file. Cause some task id is empty or null");

            var countInvalidTaskName = taskList.Count(x => string.IsNullOrWhiteSpace(x.TaskName));
            if (countInvalidTaskName > 0)
                throw new Exception("Can not load file. Cause some task name is empty or null");

            Core.ReWriteToFile(taskList);
            return taskList;
        }

        /// <summary>
        /// Checks if the JSON file exists; if not, creates it with an empty JSON array.
        /// </summary>
        public static void CheckFile()
        {
            if (!System.IO.File.Exists(FILE_NAME))
            {
                using (System.IO.FileStream fs = System.IO.File.Create(FILE_NAME))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("[]");
                    fs.Write(info, 0, info.Length);
                }
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    //summary
    //hanterar logiken för att lägga till, ta bort och visa todo items
    public class TodoList
    {
        private List<string> tasks = new List<string>();
        //summary
        public void AddTask(string task)
        {
            tasks.Add(task);
        }

        public void RemoveTask(int index)
        {
            if (index >= 0 && index < tasks.Count)
            {
                tasks.RemoveAt(index);
            }  
        }

        public List<string> GetAllTasks()
        {
            return tasks;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoAppWPF2
{
   ///<summary>
   /// hanterar logiken för att lägga till, ta bort och visa  all todo items
   /// </summary>
    public class TodoList
    {
        private List<string> tasks = new List<string>();

        //adding tasks to the list
        public void AddTask(string task)
        {
            tasks.Add(task);
        }

        //removing tasks from the list
        public void RemoveTask(int index)
        {
            if(index >= 0 && index < tasks.Count)
            {
                tasks.RemoveAt(index);
            }
        
        }

        //hämta alla items och returnera dem för oss
        public List<string> GetAllTasks()
        {
            return tasks;
        }
    }
}

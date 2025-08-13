using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TodoAppWPF2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private TodoList _todoList;
        public MainWindow()
        {
            InitializeComponent();
            _todoList = new TodoList();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if(TasksListBox.SelectedIndex >= 0)
            {
                _todoList.RemoveTask(TasksListBox.SelectedIndex);
                updateTaskList();
            }
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string task = TaskTextBox.Text;
            if (!string.IsNullOrEmpty(task))
            {
                _todoList.AddTask(task);
                updateTaskList();
                TaskTextBox.Clear();
            }
        }

        /// <summary>
        /// updating the list with todo items.
        /// </summary>
        private void updateTaskList()
        {
            TasksListBox.Items.Clear();
            foreach (var task in _todoList.GetAllTasks())
            {
                TasksListBox.Items.Add(task);
            }
        }
    }
}
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ToDo.Model;

namespace ToDo.Data
{
    internal class ToDoDatabase
    {
        private static ToDoDatabase instance;
        private static SQLiteAsyncConnection database;

        private static readonly string databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ToDo.db3");

        private ToDoDatabase()
        {
            database = new SQLiteAsyncConnection(databasePath);
            database.CreateTableAsync<ToDoItem>().Wait();
        }

        public static ToDoDatabase GetDatabase()
        {
            if (instance == null)
            {
                instance = new ToDoDatabase();
            }

            return instance;
        }


        public Task<List<ToDoItem>> GetToDoItemsAsync()
        {
            //Get all ToDo items.
            return database.Table<ToDoItem>().ToListAsync();
        }

        public Task<ToDoItem> GetToDoItemAsync(int id)
        {
            // Get a specific ToDo item.
            return database.Table<ToDoItem>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveToDoItemAsync(ToDoItem toDoItem)
        {
            if (toDoItem.Id != -1)
            {
                // Update an existing ToDo item.
                return database.UpdateAsync(toDoItem);
            }
            else
            {
                // Save a new ToDo item.
                return database.InsertAsync(toDoItem);
            }
        }

        public Task<int> DeleteToDoItemAsync(ToDoItem toDoItem)
        {
            // Delete a note.
            return database.DeleteAsync(toDoItem);
        }






    }
}

using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ToDo.Model
{
    public class ToDoItem : INotifyPropertyChanged
    {


        private string title;
        private string content;
        private bool completed;
        private bool deleted;

        public event PropertyChangedEventHandler PropertyChanged;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Title"));
                    }
                }
            }
        }
        public string Content
        {
            get { return content; }
            set
            {
                if (content != value)
                {
                    content = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Content"));
                    }
                }
            }
        }
        public DateTime Date { get; set; }

        public bool Completed
        {
            get { return completed; }
            set
            {
                if (completed != value)
                {
                    completed = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Completed"));
                    }
                }
            }
        }
        public bool Deleted
        {
            get { return deleted; }
            set
            {
                if (deleted != value)
                {
                    deleted = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Deleted"));
                    }
                }
            }
        }

        public ToDoItem()
        {
            Id = -1;
            Date = DateTime.Now;
        }

        public ToDoItem(int id, string title, string content, DateTime date, bool completed, bool deleted)
        {
            Id = id;
            Title = title;
            Content = content;
            Date = date;
            Completed = completed; 
            Deleted = deleted;
        }

        

        public ToDoItem ShallowCopy()
        {
            return (ToDoItem)this.MemberwiseClone();
        }

    }
}

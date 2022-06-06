using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToDoPage : ContentPage, INotifyPropertyChanged
    {

        public event EventHandler ToDoSaved;
        public event EventHandler ToDoDeleted;
        public event EventHandler ToDoCompleted;

        private void OnToDoSaved()
        {
            if (ToDoSaved != null)
            {
                ToDoSaved(this, EventArgs.Empty);
            }
        }

        private void OnToDoDeleted()
        {
            if (ToDoDeleted != null)
            {
                ToDoDeleted(this, EventArgs.Empty);
            }
        }

        private void OnToDoCompleted()
        {
            if (ToDoCompleted != null)
            {
                ToDoCompleted(this, EventArgs.Empty);
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private bool isEditMode;

        public bool IsEditMode
        {
            set
            {
                if (isEditMode != value)
                {
                    isEditMode = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("IsEditMode"));
                    }
                }
            }
            get
            {
                return isEditMode;
            }
        }



        public ToDoPage()
        {
            InitializeComponent();
            MarkAsCompletedButton.BindingContext = this;
            DeleteButton.BindingContext = this;
        }

        public ToDoPage(bool isEditMode)
        {
            InitializeComponent();
            this.isEditMode = isEditMode;
            MarkAsCompletedButton.BindingContext = this;
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            OnToDoSaved();
            Navigation.RemovePage(this);
        }
        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            OnToDoDeleted();
            Navigation.RemovePage(this);
        }

        private void MarkAsCompletedButton_Clicked(object sender, EventArgs e)
        {
            OnToDoCompleted();
            Navigation.RemovePage(this);
        }

    }
}
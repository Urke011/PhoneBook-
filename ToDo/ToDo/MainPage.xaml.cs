using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Data;
using ToDo.Model;
using Xamarin.Forms;

namespace ToDo
{
    public partial class MainPage : ContentPage
    {

        ToDoPage toDoPage;

        ObservableCollection<ToDoItem> data;

        public ObservableCollection<ToDoItem> Data
        {
            get { return data; }
            set { data = value; }
        }


        public MainPage()
        {
            InitializeComponent();

            toDoPage = new ToDoPage();

            toDoPage.ToDoCompleted += ToDoPage_ToDoCompleted;
            toDoPage.ToDoDeleted += ToDoPage_ToDoDeleted;
            toDoPage.ToDoSaved += ToDoPage_ToDoSaved;

            /*data = new ObservableCollection<ToDoItem> {
                new ToDoItem ( 1, "Drive car to service", "spark plugs, engine oil, filters, brake pads", new DateTime(2022, 2, 19), false, false ),
                new ToDoItem ( 2, "Call John", "044585524996", new DateTime(2022, 1, 12), false, false ),
                new ToDoItem ( 3, "Kitchen renewal stuff", "paint, appliances, Extend cabinets up to the ceiling, doors, windows...", new DateTime(2022, 3, 1), false, false )
            };*/

        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (data == null)
            {
                List<ToDoItem> toDoItem = await ToDoDatabase.GetDatabase().GetToDoItemsAsync();
                data = new ObservableCollection<ToDoItem>(toDoItem);

                this.BindingContext = this;
            }


        }



        private async void ToDoPage_ToDoSaved(object sender, EventArgs e)
        {
            ToDoPage toDoPage = (ToDoPage)sender;
            ToDoItem toDoItem = (ToDoItem)toDoPage.BindingContext;

            if (toDoItem.Id == -1)
            {
                int rowsUpdated = await ToDoDatabase.GetDatabase().SaveToDoItemAsync(toDoItem);

                if(rowsUpdated != 0)
                {
                    data.Add(toDoItem);
                }
       
            }
            else
            {

                int rowsUpdated = await ToDoDatabase.GetDatabase().SaveToDoItemAsync(toDoItem);

                if(rowsUpdated != 0)
                {
                    ToDoItem toDoItem1 = data.Where(x => x.Id == toDoItem.Id).First();
                    if (toDoItem1 != null)
                    {
                        toDoItem1.Title = toDoItem.Title;
                        toDoItem1.Content = toDoItem.Content;
                    }
                }
            }

        }

        private async void ToDoPage_ToDoDeleted(object sender, EventArgs e)
        {
            ToDoPage toDoPage = (ToDoPage)sender;
            ToDoItem toDoItem = (ToDoItem)toDoPage.BindingContext;

            int rowsUpdated = await ToDoDatabase.GetDatabase().DeleteToDoItemAsync(toDoItem);

            if(rowsUpdated != 0)
            {
                ToDoItem toDoItem1 = data.Where(x => x.Id == toDoItem.Id).First();
                data.Remove(toDoItem1);
            }



        }

        private async void ToDoPage_ToDoCompleted(object sender, EventArgs e)
        {
            ToDoPage toDoPage = (ToDoPage)sender;
            ToDoItem toDoItem = (ToDoItem)toDoPage.BindingContext;
            toDoItem.Completed = true;

            int rowsUpdated = await ToDoDatabase.GetDatabase().SaveToDoItemAsync(toDoItem);

            if(rowsUpdated != 0)
            {
                ToDoItem toDoItem1 = data.Where(x => x.Id == toDoItem.Id).First();
                toDoItem1.Completed = true;
            }


        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            toDoPage.BindingContext = new ToDoItem();
            toDoPage.IsEditMode = false;
            await Navigation.PushAsync(toDoPage);
        }



        private async void MyListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            ToDoItem toDoItemEdit = (ToDoItem)(e.Item);
            toDoItemEdit = toDoItemEdit.ShallowCopy();

            toDoPage.BindingContext = toDoItemEdit;
            toDoPage.IsEditMode = true;
            await Navigation.PushAsync(toDoPage);
        }
    }
}

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDo
{
    public partial class App : Application
    {

        NavigationPage mainNavigationPage;
        ContentPage mainContentPage;
    

        public App()
        {
            InitializeComponent();

            mainContentPage = new MainPage();

            mainNavigationPage = new NavigationPage(mainContentPage);
            mainNavigationPage.BarBackgroundColor = Color.DarkSlateGray;

            MainPage = mainNavigationPage;

        }


    }
}

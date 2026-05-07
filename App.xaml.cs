namespace LectureMobileApp;

public partial class App : Application
{
    public App(LectureMobileApp.Data.LectureDatabase database)
    {
        InitializeComponent();

        MainPage = new NavigationPage(new MainPage(database));
    }
}
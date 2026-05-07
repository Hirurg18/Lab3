using LectureMobileApp.Models;
using LectureMobileApp.Data;

namespace LectureMobileApp;

public partial class MainPage : ContentPage
{
    private readonly LectureDatabase _database;

    public MainPage(LectureDatabase database)
    {
        InitializeComponent();
        _database = database;
    }

    // Оновлюємо список кожного разу, коли відкриваємо це вікно
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        LecturesListView.ItemsSource = await _database.GetLecturesAsync();
    }

    // Кнопка створення
    private async void OnAddClicked(object sender, EventArgs e)
    {
        // Переходимо на вікно деталей з новим об'єктом
        await Navigation.PushAsync(new LectureDetailPage(new Lecture(), _database));
    }

    // Кнопка редагування
    private async void OnEditClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var lecture = (Lecture)button.CommandParameter;

        // Переходимо на вікно деталей з існуючим об'єктом
        await Navigation.PushAsync(new LectureDetailPage(lecture, _database));
    }

    // Кнопка видалення
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var lecture = (Lecture)button.CommandParameter;

        bool answer = await DisplayAlert("Видалення", $"Видалити лекцію '{lecture.Title}'?", "Так", "Ні");
        if (answer)
        {
            await _database.DeleteLectureAsync(lecture);
            // Оновлюємо список
            LecturesListView.ItemsSource = await _database.GetLecturesAsync();
        }
    }
}
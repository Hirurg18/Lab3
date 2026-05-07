using LectureMobileApp.Models;
using LectureMobileApp.Data;

namespace LectureMobileApp;

public partial class LectureDetailPage : ContentPage
{
    private Lecture _lecture;
    private readonly LectureDatabase _database;

    // Конструктор приймає об'єкт лекції (новий або існуючий) та посилання на базу
    public LectureDetailPage(Lecture lecture, LectureDatabase database)
    {
        InitializeComponent();
        _database = database;
        _lecture = lecture;

        // Якщо ми редагуємо, заповнюємо поля даними з бази
        TitleEntry.Text = _lecture.Title;
        AuthorEntry.Text = _lecture.AuthorName;
        SubjectEntry.Text = _lecture.Subject;
        ContentEditor.Text = _lecture.Content;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // Збираємо дані з полів введення
        _lecture.Title = TitleEntry.Text;
        _lecture.AuthorName = AuthorEntry.Text;
        _lecture.Subject = SubjectEntry.Text;
        _lecture.Content = ContentEditor.Text;

        if (string.IsNullOrWhiteSpace(_lecture.Title))
        {
            await DisplayAlert("Помилка", "Назва лекції не може бути порожньою", "OK");
            return;
        }

        // Зберігаємо (метод Save сам зробить Insert або Update)
        await _database.SaveLectureAsync(_lecture);

        // Повертаємося до попереднього вікна (списку)
        await Navigation.PopAsync();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
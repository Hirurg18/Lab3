using SQLite;
using System;

namespace LectureMobileApp.Models
{
    public class Lecture
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } // 1. Ідентифікатор

        [Indexed]
        public string Title { get; set; } // 2. Назва лекції

        public string Content { get; set; } // 3. Текст (контент)

        public string AuthorName { get; set; } // 4. ПІБ викладача

        public string Subject { get; set; } // 5. Навчальна дисципліна

        public DateTime CreatedAt { get; set; } // 6. Дата створення
    }
}
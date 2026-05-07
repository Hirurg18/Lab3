using SQLite;
using LectureMobileApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LectureMobileApp.Data
{
    public class LectureDatabase
    {
        SQLiteAsyncConnection Database;

        public LectureDatabase()
        {
        }

        async Task Init()
        {
            if (Database is not null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Lectures.db3");
            Database = new SQLiteAsyncConnection(databasePath);
            await Database.CreateTableAsync<Lecture>();
        }

        // --- READ: Отримати всі лекції ---
        public async Task<List<Lecture>> GetLecturesAsync()
        {
            await Init();
            return await Database.Table<Lecture>().ToListAsync();
        }

        // --- READ: Отримати одну лекцію за ID ---
        public async Task<Lecture> GetLectureAsync(int id)
        {
            await Init();
            return await Database.Table<Lecture>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        // --- CREATE & UPDATE: Зберегти лекцію ---
        public async Task<int> SaveLectureAsync(Lecture item)
        {
            await Init();
            if (item.Id != 0)
            {
                // Якщо ID вже є, оновлюємо існуючий запис (Update)
                return await Database.UpdateAsync(item);
            }
            else
            {
                // Якщо ID немає, створюємо новий запис (Create)
                item.CreatedAt = DateTime.Now; // Автоматично ставимо дату створення
                return await Database.InsertAsync(item);
            }
        }

        // --- DELETE: Видалити лекцію ---
        public async Task<int> DeleteLectureAsync(Lecture item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }
    }
}
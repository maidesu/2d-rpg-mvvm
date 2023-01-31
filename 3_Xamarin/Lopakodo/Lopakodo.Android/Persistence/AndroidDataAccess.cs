using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Lopakodo.Droid.Persistence;
using Lopakodo.Persistence;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidDataAccess))]
namespace Lopakodo.Droid.Persistence
{
    public class AndroidDataAccess : ILopakodoData
    {
        public async Task InitMaps()
        {
            List<Int32> basementList = new List<Int32> { 12, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 2, 0, 1, 0, 1, 0, 1, 0, 0, 2, 1, 0, 0, 0, 1, 0, 1, 2, 1, 0, 0, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 3, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0};
            await SaveAsync("basement", LoadDefault(basementList));

            List<Int32> showersList = new List<Int32> { 14, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 1, 0, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 2, 0, 0, 0, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 2, 1, 2, 1, 0, 1, 1, 0, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 1, 0, 1, 2, 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 4, 1, 0, 1, 0, 1, 3, 1, 1, 1, 1, 1, 1 };
            await SaveAsync("showers", LoadDefault(showersList));

            List<Int32> schoolList = new List<Int32> { 18, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 2, 0, 0, 2, 0, 1, 0, 0, 1, 0, 2, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 2, 1, 0, 1, 1, 1, 0, 1, 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 2, 0, 0, 1 };
            await SaveAsync("school", LoadDefault(schoolList));
        }

        public async Task<LopakodoGameField> LoadAsync(String path)
        {
            String filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), path);
            String[] values = (await Task.Run(() => File.ReadAllText(filePath))).Split(' ');
            


            Int32 fieldSize = Int32.Parse(values[0]);
            LopakodoGameField gameField = new LopakodoGameField(fieldSize);

            for (Int32 i = 0; i < fieldSize; ++i)
            {
                for (Int32 j = 0; j < fieldSize; ++j)
                {
                    gameField.SetValue(i, j, Int32.Parse(values[1 + ((i*fieldSize) + j)].ToString()));
                }
            }

            return gameField;
        }

        public async Task SaveAsync(String path, LopakodoGameField gameField)
        {
            String text = gameField.Size.ToString() + " ";

            for (Int32 i = 0; i < gameField.Size; ++i)
            {
                for (Int32 j = 0; j < gameField.Size; ++j)
                {
                    text += gameField[i, j] + " ";
                }
            }
            String filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), path);

            await Task.Run(() => File.WriteAllText(filePath, text));
        }

        private LopakodoGameField LoadDefault(List<Int32> values)
        {
            Int32 fieldSize = values[0];
            LopakodoGameField gameField = new LopakodoGameField(fieldSize);

            for (Int32 i = 0; i < fieldSize; ++i)
            {
                for (Int32 j = 0; j < fieldSize; ++j)
                {
                    gameField.SetValue(i, j, values[1 + (i * fieldSize) + j]);
                }
            }

            return gameField;
        }
    }
}
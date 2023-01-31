using System;
using System.Threading.Tasks;
using System.IO;

namespace Lopakodo.Persistence
{
    public class LopakodoFileData : ILopakodoData
    {
        public async Task<LopakodoGameField> LoadAsync(String path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    Int32 fieldSize = Int32.Parse(await reader.ReadLineAsync());
                    LopakodoGameField gameField = new LopakodoGameField(fieldSize);

                    for (Int32 i = 0; i < fieldSize; ++i)
                    {
                        String line = await reader.ReadLineAsync();
                        String[] values = line.Split(' ');

                        for (Int32 j = 0; j < fieldSize; ++j)
                        {
                            gameField.SetValue(i, j, Int32.Parse(values[j]));
                        }
                    }

                    if (!gameField.HasEntry || !gameField.HasExit) throw new LopakodoDataException();

                    return gameField;
                }
            }
            catch
            {
                throw new LopakodoDataException();
            }
        }

        public async Task SaveAsync(String path, LopakodoGameField gameField)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    await writer.WriteLineAsync(gameField.Size.ToString());

                    for (Int32 i = 0; i < gameField.Size; ++i)
                    {
                        for (Int32 j = 0; j < gameField.Size; ++j)
                        {
                            await writer.WriteAsync(gameField[i, j] + " ");
                        }
                        await writer.WriteLineAsync();
                    }
                }
            }
            catch
            {
                throw new LopakodoDataException();
            }
        }
    }
}

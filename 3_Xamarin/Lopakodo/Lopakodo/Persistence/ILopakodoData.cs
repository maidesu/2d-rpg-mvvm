using System;
using System.Threading.Tasks;

namespace Lopakodo.Persistence
{
    public interface ILopakodoData
    {
        Task InitMaps();

        Task<LopakodoGameField> LoadAsync(String path);
        Task SaveAsync(String path, LopakodoGameField gameField);
    }
}

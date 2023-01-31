using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Lopakodo.Droid.Persistence;
using Lopakodo.Persistence;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidStore))]
namespace Lopakodo.Droid.Persistence
{
    public class AndroidStore : IStore
    {
        public async Task<IEnumerable<String>> GetFiles()
        {
            return await Task.Run(() => Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Personal)).Select(file => Path.GetFileName(file)));
        }

        public async Task<DateTime> GetModifiedTime(String name)
        {
            FileInfo info = new FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), name));

            return await Task.Run(() => info.LastWriteTime);
        }
    }
}
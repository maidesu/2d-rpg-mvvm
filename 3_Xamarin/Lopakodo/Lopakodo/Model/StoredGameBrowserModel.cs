using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lopakodo.Persistence;

namespace Lopakodo.Model
{
    public class StoredGameBrowserModel
    {
        private IStore _store;

        public event EventHandler StoreChanged;

        public StoredGameBrowserModel(IStore store)
        {
            _store = store;

            StoredGames = new List<StoredGameModel>();
        }

        public List<StoredGameModel> StoredGames { get; private set; }

        public async Task UpdateAsync()
        {
            if (_store == null)
                return;

            StoredGames.Clear();

            foreach (String name in await _store.GetFiles())
            {
                if (name == "Suspended")
                    continue;

                StoredGames.Add(new StoredGameModel
                {
                    Name = name,
                    Modified = await _store.GetModifiedTime(name)
                });
            }

            StoredGames = StoredGames.OrderByDescending(item => item.Modified).ToList();
            
            OnSavesChanged();
        }

        private void OnSavesChanged()
        {
            if (StoreChanged != null)
                StoreChanged(this, EventArgs.Empty);
        }
    }
}

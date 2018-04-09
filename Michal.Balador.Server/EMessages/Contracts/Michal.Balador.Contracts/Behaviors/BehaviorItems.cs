using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts.Behaviors
{
    public  class BehaviorItems<T> where T : Behavior
    {
        List<Behavior> _List = new List<Behavior>();
        public void Add(T behavior)
        {
            _List.Add(behavior);
        }
        public IEnumerable<TItem> Get<TItem>() where TItem : Behavior
        {
            foreach (var item in _List)
            {
                if (item.GetType() == typeof(TItem))
                {
                    yield return (TItem)item;
                }
            }
        }
    }
}

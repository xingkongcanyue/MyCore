using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Core
{
    public class SafeDictionary<TKey,TValue>:Dictionary<TKey,TValue>
    {
        private object _lock = new object();

        public virtual TValue GetOrAdd(TKey key, TValue value)
        {
            return GetOrAdd(key, () => value);
        }
        public virtual TValue GetOrAdd(TKey key, Func<TValue> funcValue)
        {
            return GetOrAdd(key, x => funcValue());
        }
        public virtual TValue GetOrAdd(TKey key, Func<TKey,TValue> funcValue)
        {
            if (!ContainsKey(key)) 
            {
                lock (_lock) {
                    if (!ContainsKey(key)) {
                        Add(key, funcValue(key));
                    }  
                } 
            }
            return this[key];
        }

        public new TValue this[TKey key]
        {
            get { return base[key]; }
            set {
                base[key] = value;
            }
        }
        public new void Add(TKey key, TValue value)
        {
            if (!ContainsKey(key)) {
                lock (_lock) {
                    if (!ContainsKey(key))
                        base.Add(key, value);
                }
            
            }
        }

         
    }
}

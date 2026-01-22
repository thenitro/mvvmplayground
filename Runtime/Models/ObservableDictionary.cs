using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MVVM.Models
{
    public class ObservableDictionary<TKey, TValue> : BaseObservable<ObservableDictionary<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>
    {
        public IEnumerable<TKey> Keys => _dict.Keys;
        public IObservableValue<IEnumerable<TValue>> Values => _values;
        public int Count => _dict.Count;
        
        private Dictionary<TKey, TValue> _dict = new();
        private Dictionary<TKey, HashSet<Action<TKey, TValue>>> _observers = new();

        private readonly ObservableValue<IEnumerable<TValue>> _values = new();
        
        public void Add(TKey key, TValue value)
        {
            var changed = !_dict.ContainsKey(key) || !_dict[key].Equals(value);
            if (!changed)
            {
                return;
            }

            _dict[key] = value;
            _values.Setup(_dict.Values.ToArray());
            NotifyObservers(key, value);
        }

        public TValue this[TKey key]
        {
            get => _dict[key];
            set
            {
                _dict[key] = value;
                _values.Setup(_dict.Values.ToArray());
                NotifyObservers(key, value);
            }
        }

        public void Remove(TKey key)
        {
            if (!_dict.ContainsKey(key))
            {
                return;
            }

            _dict.Remove(key);
            _values.Setup(_dict.Values.ToArray());

            if (_observers.ContainsKey(key))
            {
                NotifyObservers(_observers[key], key, default);
            }
            
            NotifyObservers(this);
        }
        
        public void Clear() {
            _observers.Clear();
            _dict.Clear();
            _values.Setup(Array.Empty<TValue>());
            
            NotifyObservers(this);
        }

        public TValue Get(TKey key) => _dict.GetValueOrDefault(key);
        
        public bool ContainsKey(TKey key) => _dict.ContainsKey(key);

        public void Observe(TKey key, Action<TKey, TValue> onValueChanged)
        {
            if (_observers.ContainsKey(key) && _observers[key].Contains(onValueChanged))
            {
                return;
            }

            if (!_observers.TryGetValue(key, out HashSet<Action<TKey, TValue>> observers))
            {
                observers = new HashSet<Action<TKey, TValue>>();
                _observers[key] = observers;
            }

            observers.Add(onValueChanged);
        }

        public void RemoveObservation(TKey key, Action<TKey, TValue> onValueChanged)
        {
            if (!_observers.ContainsKey(key) || !_observers[key].Contains(onValueChanged))
            {
                return;
            }

            _observers[key].Remove(onValueChanged);
        }
        
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _dict.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void NotifyObservers(TKey key, TValue value)
        {
            if (_observers.ContainsKey(key))
            {
                NotifyObservers(_observers[key], key, value);
            }
            
            NotifyObservers(this);
        }
        
        private void NotifyObservers(HashSet<Action<TKey, TValue>> observers, TKey key, TValue value)
        {
            foreach (var observer in observers)
            {
                observer.Invoke(key, value);
            }
        }
    }
}
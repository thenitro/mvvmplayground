using System.Collections;
using System.Collections.Generic;

namespace MVVM.Models {
    public interface IObservableList<T> : IObservable<IObservableList<T>>, IEnumerable<T> {
        T this[int index] { get; }
    }
    
    public class ObservableList<T> : BaseObservable<IObservableList<T>>, IObservableList<T> {
        private readonly List<T> _values = new();
        public int Count => _values.Count;

        public IEnumerator<T> GetEnumerator() => _values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public T this[int index] {
            get => _values[index];
            set {
                _values[index] = value;
                NotifyObservers(this);
            }
        }

        public void Add(T value, bool notify = true) {
            _values.Add(value);

            if (notify) {
                NotifyObservers(this);
            }
        }

        public void AddRange(IEnumerable<T> range, bool notify = true) {
            _values.AddRange(range);

            if (notify) {
                NotifyObservers(this);
            }
        }

        public void Clear(bool notify = true) {
            _values.Clear();

            if (notify) {
                NotifyObservers(this);
            }
        }

        public void NotifyObservers() {
            NotifyObservers(this);
        }
    }
}

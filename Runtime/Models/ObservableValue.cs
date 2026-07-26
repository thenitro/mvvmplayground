using System.Collections.Generic;

namespace MVVM.Models
{
    public class ObservableValue<T> : BaseObservable<T>, IObservableValue<T>
    {
        public T Value { get; private set; }
        public bool IsDefault() => EqualityComparer<T>.Default.Equals(Value, default);
        
        public ObservableValue(T value = default)
        {
            Value = value;
        }

        public void Setup(T value)
        {
            if (EqualityComparer<T>.Default.Equals(Value, value))
            {
                return;
            }
            
            Value = value;
            NotifyObservers(value);
        }

        public void ForceNotify() {
            NotifyObservers(Value);
        }

        public override string ToString()
        {
            return $"[ObservableValue contents={Value}]";
        }
    }
}
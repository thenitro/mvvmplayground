namespace MVVM.Models
{
    public class ObservableValue<T> : BaseObservable<T>, IObservableValue<T>
    {
        public T Value { get; private set; }

        public ObservableValue(T value = default)
        {
            Value = value;
        }

        public void Setup(T value)
        {
            if (Value != null && Value.Equals(value))
            {
                return;
            }
            
            Value = value;
            NotifyObservers(value);
        }

        public void ForceNotify() {
            NotifyObservers(Value);
        }

        public override string ToString() {
            return $"[ObservableValue: {Value}]";
        }
    }
}
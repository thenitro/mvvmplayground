namespace MVVM.Models
{
    public class ObservableProxy<T> : BaseObservable<T>, IObservableValue<T>
        where T : IObservable<T>
    {
        public T Value { get; private set; }

        public ObservableProxy(T value)
        {
            Setup(value);
        }

        public void Setup(T value)
        {
            if (Value != null && Value.Equals(value))
            {
                return;
            }
            
            Value?.RemoveObservation(OnValueUpdated);
            Value = value;
            Value?.Observe(OnValueUpdated);
            
            NotifyObservers(value);
        }

        private void OnValueUpdated(T obj)
        {
            NotifyObservers(obj);
        }
    }
}
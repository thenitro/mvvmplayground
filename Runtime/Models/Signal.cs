namespace MVVM.Models {
    public interface ISignal : IObservable { }
    public interface ISignal<T> : IObservable<T> { }
    
    public class Signal : BaseObservable, ISignal {
        public void Notify() {
            NotifyObservers();
        }
    }
    
    public class Signal<T> : BaseObservable<T>, ISignal<T> {
        public void Notify(T data) {
            NotifyObservers(data);
        }
    }
}

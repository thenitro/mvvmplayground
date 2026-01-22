using System;

namespace MVVM.Bindings.Base
{
    public class ObservableBinding : IDestroyableBinding
    {
        private readonly Models.IObservable _observable;
        private readonly Action _onUpdate;
        
        public ObservableBinding(Models.IObservable observable, Action onUpdate)
        {
            _observable = observable;
            _onUpdate = onUpdate;
            
            observable.Observe(onUpdate);
        }

        public void OnDestroy()
        {
            _observable.RemoveObservation(_onUpdate);
        }
    }
    
    public class ObservableBinding<T> : IDestroyableBinding
    {
        private readonly Models.IObservable<T> _observable;
        private readonly Action<T> _onUpdate;
        
        public ObservableBinding(Models.IObservable<T> observable, Action<T> onUpdate)
        {
            _observable = observable;
            _onUpdate = onUpdate;
            
            observable.Observe(onUpdate);
        }

        public void OnDestroy()
        {
            _observable.RemoveObservation(_onUpdate);
        }
    }
}
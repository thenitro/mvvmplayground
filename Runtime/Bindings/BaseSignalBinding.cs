using MVVM.Bindings.Base;
using MVVM.Models;

namespace MVVM.Bindings {
    public abstract class BaseSignalBinding : ILifecycleBinding {
        
        private readonly ISignal _signal;

        public BaseSignalBinding(ISignal signal)
        {
            _signal = signal;
            _signal.Observe(OnNotify);
        }
        
        public void OnEnable()
        {
            OnEnableImpl();
            _signal?.Observe(OnNotify);
        }

        public void OnDisable()
        {
            _signal?.RemoveObservation(OnNotify);
            OnDisableImpl();
        }

        public void OnDestroy()
        {
            _signal?.RemoveObservation(OnNotify);
            OnDestroyImpl();
        }
        
        protected abstract void OnNotify();
        
        protected virtual void OnEnableImpl() { }
        protected virtual void OnDisableImpl() { }
        protected virtual void OnDestroyImpl() { }
    }
    
    public abstract class BaseSignalBinding<T> : ILifecycleBinding {
        
        private readonly ISignal<T> _signal;

        public BaseSignalBinding(ISignal<T> signal)
        {
            _signal = signal;
            _signal.Observe(OnNotify);
        }
        
        public void OnEnable()
        {
            OnEnableImpl();
            _signal?.Observe(OnNotify);
        }

        public void OnDisable()
        {
            _signal?.RemoveObservation(OnNotify);
            OnDisableImpl();
        }

        public void OnDestroy()
        {
            _signal?.RemoveObservation(OnNotify);
            OnDestroyImpl();
        }
        
        protected abstract void OnNotify(T value);
        
        protected virtual void OnEnableImpl() { }
        protected virtual void OnDisableImpl() { }
        protected virtual void OnDestroyImpl() { }
    }
}
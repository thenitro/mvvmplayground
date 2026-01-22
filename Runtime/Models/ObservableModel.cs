using System;
using System.Collections.Generic;
using MVVM.Bindings.Base;

namespace MVVM.Models
{
    public interface IDestructibleModel {
        public void OnDestroy();
    }
    
    public class ObservableModel<T> : BaseObservable<T>, IDestructibleModel
        where T : class
    {
        private readonly List<IDestroyableBinding> _bindingsToDestroy = new();
        
        protected void RegisterChildObservable<V>(IObservable<V> observable)
        {
            _bindingsToDestroy.Add(new ObservableBinding<V>(observable, v => NotifyObservers(this as T)));
        }
        
        protected void Observe(IObservable observable, Action onNotify, bool updateImmediately = false) {
            _bindingsToDestroy.Add(new ObservableBinding(observable, onNotify));
            
            if (updateImmediately) {
                onNotify?.Invoke();
            }
        }
        
        protected void Observe<A>(Models.IObservable<A> observable, Action<A> onUpdate, bool updateImmediately = false)
        {
            _bindingsToDestroy.Add(new ObservableBinding<A>(observable, onUpdate));
            
            if (updateImmediately) {
                onUpdate?.Invoke((A)observable);
            }
        }
        
        protected void Observe<A>(IObservableValue<A> observable, Action<A> onUpdate, bool updateImmediately = false)
        {
            _bindingsToDestroy.Add(new ObservableBinding<A>(observable, onUpdate));

            if (updateImmediately)
            {
                onUpdate?.Invoke(observable.Value);
            }
        }

        protected void ObserveAny<A, B>(IObservableValue<A> observableA, IObservableValue<B> observableB, Action<A, B> onUpdate, bool updateImmediately = false)
        {
            void Notify()
            {
                onUpdate(observableA.Value, observableB.Value);
            }
            
            _bindingsToDestroy.Add(new ObservableBinding<A>(observableA, a => Notify()));
            _bindingsToDestroy.Add(new ObservableBinding<B>(observableB, b => Notify()));

            if (updateImmediately)
            {
                Notify();
            }
        }
        
        protected void ObserveAny<A, B, C>(IObservableValue<A> observableA, IObservableValue<B> observableB, IObservableValue<C> observableC, Action<A, B, C> onUpdate, bool updateImmediately = false)
        {
            void Notify()
            {
                onUpdate(observableA.Value, observableB.Value, observableC.Value);
            }
            
            _bindingsToDestroy.Add(new ObservableBinding<A>(observableA, a => Notify()));
            _bindingsToDestroy.Add(new ObservableBinding<B>(observableB, b => Notify()));
            _bindingsToDestroy.Add(new ObservableBinding<C>(observableC, c => Notify()));

            if (updateImmediately)
            {
                Notify();
            }
        }

        public void OnDestroy()
        {
            foreach (var binding in _bindingsToDestroy)
            {
                binding.OnDestroy();
            }
            _bindingsToDestroy.Clear();
            
            ClearObservers();
            OnDestroyImplementation();
        }
        
        protected virtual void OnDestroyImplementation() { }
    }
}
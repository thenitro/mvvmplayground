using System;
using System.Collections.Generic;
using MVVM.Bindings.Base;
using MVVM.Models;
using UnityEngine;

namespace Controllers
{
    public class BaseEntityController : MonoBehaviour
    {
        private readonly List<IDestroyableBinding> _bindingsToDestroy = new();
        
        protected void Observe(IObservable observable, Action onNotify, bool updateImmediately = false) {
            _bindingsToDestroy.Add(new ObservableBinding(observable, onNotify));

            if (updateImmediately) {
                onNotify?.Invoke();
            }
        }
        
        protected void Observe<T>(MVVM.Models.IObservable<T> observable, Action<T> onUpdate, bool updateImmediately = false)
        {
            _bindingsToDestroy.Add(new ObservableBinding<T>(observable, onUpdate));

            if (updateImmediately) {
                onUpdate?.Invoke((T)observable);
            }
        }
        
        protected void Observe<T>(IObservableValue<T> observable, Action<T> onUpdate, bool updateImmediately = false)
        {
            _bindingsToDestroy.Add(new ObservableBinding<T>(observable, onUpdate));

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
        
        protected virtual void OnDestroyImplementation() { }

        protected void CleanupBindings() {
            foreach (var destroyableBinding in _bindingsToDestroy)
            {
                destroyableBinding.OnDestroy();
            }
            
            _bindingsToDestroy.Clear();
        }
        
        protected void OnDestroy()
        {
            CleanupBindings();
            OnDestroyImplementation();
        }
    }
}
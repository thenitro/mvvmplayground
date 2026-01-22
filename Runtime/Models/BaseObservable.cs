using System;
using System.Collections.Generic;
using System.Linq;

namespace MVVM.Models
{
    public abstract class BaseObservable : IObservable
    {
        private HashSet<Action> _callbacks = new();
        
        public int CallbackCount => _callbacks.Count; 

        public void Observe(Action OnUpdate)
        {
            if (_callbacks.Contains(OnUpdate))
            {
                return;
            }

            _callbacks.Add(OnUpdate);
        }

        public void RemoveObservation(Action OnUpdate)
        {
            if (!_callbacks.Contains(OnUpdate))
            {
                return;
            }

            _callbacks.Remove(OnUpdate);
        }

        protected void NotifyObservers()
        {
            /*
             * TODO: copying array here is not cool
             * and there is few things that needed to be considered:
             * 1. adding/removing callback while iterating
             * 2. several NotifyObservers call during iteration
             * 3. growing call stack if chain of observers is long
             * 4. we double checking callbacks because they could be removed during iteration
             *
             * Solution 1: try to use linked list?
             */
            foreach (var callback in _callbacks.ToArray())
            {
                if (_callbacks.Contains(callback)) {
                    callback.Invoke();
                }
            }
        }

        protected void ClearObservers() {
            _callbacks.Clear();
        }
    }
    
    public abstract class BaseObservable<T> : IObservable<T>
    {
        private HashSet<Action<T>> _callbacks = new();
        
        public int CallbackCount => _callbacks.Count; 

        public void Observe(Action<T> OnUpdate)
        {
            if (_callbacks.Contains(OnUpdate))
            {
                return;
            }

            _callbacks.Add(OnUpdate);
        }

        public void RemoveObservation(Action<T> OnUpdate)
        {
            if (!_callbacks.Contains(OnUpdate))
            {
                return;
            }

            _callbacks.Remove(OnUpdate);
        }

        protected void NotifyObservers(T instance)
        {
            /*
             * TODO: copying array here is not cool
             * and there is few things that needed to be considered:
             * 1. adding/removing callback while iterating
             * 2. several NotifyObservers call during iteration
             * 3. growing call stack if chain of observers is long
             * 4. we double checking callbacks because they could be removed during iteration
             *
             * Solution 1: try to use linked list?
             */
            foreach (var callback in _callbacks.ToArray())
            {
                if (_callbacks.Contains(callback)) {
                    callback.Invoke(instance);
                }
            }
        }
        
        protected void ClearObservers() {
            _callbacks.Clear();
        }
    }
}
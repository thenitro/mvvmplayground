using System;

namespace MVVM.Models
{
    public interface IObservable
    {
        void Observe(Action OnUpdate);
        void RemoveObservation(Action OnUpdate);
    }
    
    public interface IObservable<T>
    {
        void Observe(Action<T> OnUpdate);
        void RemoveObservation(Action<T> OnUpdate);
    }
}
using System;
using System.Collections.Generic;
using MVVM.Bindings.Base;
using MVVM.Models;

namespace MVVM.ViewModels
{
    public abstract class BaseViewModel<T> : ObservableModel<T> where T : class
    {
        protected virtual void OnEnableImplementation() { }
        protected virtual void OnDisableImplementation() { }

        internal void OnEnable()
        {
            OnEnableImplementation();
        }

        internal void OnDisable()
        {
            OnDisableImplementation();
        }
    }
}
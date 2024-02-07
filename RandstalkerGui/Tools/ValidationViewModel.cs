using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace RandstalkerGui.Tools
{
    public class ValidationViewModel : ObservableObject, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> propertyErrors = new Dictionary<string, List<string>>();

        public bool HasErrors => propertyErrors.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if(string.IsNullOrWhiteSpace(propertyName))
            {
                return propertyErrors.Values;
            }

            List<string> list;
            return propertyErrors.TryGetValue(propertyName, out list) ? list : null;
        }

        public void AddError(string propertyName, string errorMessage)
        {
            if (!propertyErrors.ContainsKey(propertyName))
            {
                propertyErrors.Add(propertyName, new List<string>());
            }

            propertyErrors[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);
        }

        public void ClearErrors(string propertyName)
        {
            if (propertyErrors.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);
            }
        }

        protected void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(HasErrors));
        }
    }
}

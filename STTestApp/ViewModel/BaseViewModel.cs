using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace STTestApp.ViewModel
{
    /// <summary>
    /// Базовый класс для остальных вьюМоделей
    /// </summary>
    class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Запуск эвента, обновляющего свойство
        /// </summary>
        /// <param name="propertyName"> имя свойства (nameof(<name>)</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

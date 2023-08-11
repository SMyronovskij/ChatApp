using System.ComponentModel;
using System.Runtime.CompilerServices;
using ChatApplication.Annotations;

namespace ChatApplication.ViewModels;

public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
            handler(this, new PropertyChangedEventArgs(propertyName));
    }

    public bool SetProperty(ref object backingStore, object value, [CallerMemberName] string propertyName = "",
        Action onChanged = null)
    {
        if (Equals(backingStore, value))
            return false;

        backingStore = value;
        onChanged?.Invoke();
        OnPropertyChanged(propertyName);
        return true;
    }

    public bool SetProperty(ref string backingStore, string value, [CallerMemberName] string propertyName = "",
        Action onChanged = null)
    {
        if (Equals(backingStore, value))
            return false;

        backingStore = value;
        onChanged?.Invoke();
        OnPropertyChanged(propertyName);
        return true;
    }

    public bool SetProperty(ref bool backingStore, bool value, [CallerMemberName] string propertyName = "",
        Action onChanged = null)
    {
        if (Equals(backingStore, value))
            return false;

        backingStore = value;
        onChanged?.Invoke();
        OnPropertyChanged(propertyName);
        return true;
    }
}
using System;


/// <summary>
/// Used to make weak references easier to use
/// call variable.Value to get the value
/// </summary>
/// <typeparam name="T"></typeparam>
public class SimpleWeakRef<T> where T : class
{
    private WeakReference<T> _ref;
    public T Value 
    {
        get
        {
            T value = null;
            _ref.TryGetTarget(out value);
            return value;
        } 
        private set
        {
            _ref = new WeakReference<T>(value);
        }
    }

    public SimpleWeakRef(T obj){
        Value = obj;
    }
}


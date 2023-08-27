// This interface is designed to work with ProgressBar UI. 
// This interface shall only be used 
using System;

public interface IHasProgress
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs {
        public float progressNormalized;
    }
    
    
}

﻿namespace A3ServerTool.Models;

public interface ICloneable<T>
{
    T Clone();
}

/// <summary>
/// Represents game mod.
/// </summary>
public class Modification : INotifyPropertyChanged, ICloneable<Modification>
{     
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is selected.
    /// </summary>
    public bool IsClientMod
    {
        get => _isClientMod;
        set
        {
            if (Equals(_isClientMod, value))
            {
                return;
            }
            _isClientMod = value;
            OnPropertyChanged();
        }
    }

    private bool _isClientMod;

    /// <summary>
    /// Gets or sets a value indicating whether this instance is whitelisted.
    /// </summary>
    public bool IsServerMod
    {
        get => _isServerMod;
        set
        {
            if (Equals(_isServerMod, value))
            {
                return;
            }
            _isServerMod = value;
            OnPropertyChanged();
        }
    }

    private bool _isServerMod;
    
    /// <summary>
    /// Gets or sets a value indicating whether this instance is absolute path.
    /// </summary>
    public bool IsAbsolutePathMod
    {
        get => _isAbsolutePathMod;
        set
        {
            if (Equals(_isAbsolutePathMod, value))
            {
                return;
            }
            _isAbsolutePathMod = value;
            OnPropertyChanged();
        }
    }

    private bool _isAbsolutePathMod;


    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion

    public Modification Clone()
    {
        return new Modification
        {
            Name = this.Name,
            IsAbsolutePathMod = this._isAbsolutePathMod,
            IsClientMod = this._isClientMod,
            IsServerMod = this._isServerMod
        };
    }
}

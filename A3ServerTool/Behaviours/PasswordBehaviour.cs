namespace A3ServerTool.Behaviours;

public class PasswordBehaviour : Behavior<PasswordBox>
{
    public static readonly DependencyProperty PasswordProperty =
        DependencyProperty.Register("Password", typeof(string), typeof(PasswordBehaviour)
            , new FrameworkPropertyMetadata(default(string)));

    private bool _skipUpdate;
    private object _value;

    public string Password
    {
        get
        {
            return (string)GetValue(PasswordProperty);
        }
        set
        {
            SetValue(PasswordProperty, value);
        }
    }

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.PasswordChanged += PasswordBox_PasswordChanged;

        if (_value != null)
        {
            AssociatedObject.Password = _value as string;
        }
    }

    protected override void OnDetaching()
    {
        if(AssociatedObject != null)
        {
            AssociatedObject.PasswordChanged -= PasswordBox_PasswordChanged;
        }
        base.OnDetaching();
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (AssociatedObject == null)
        {
            _value = e.NewValue as string;
            return;
        }


        if (e.Property == PasswordProperty)
        {
            if (!_skipUpdate)
            {
                _skipUpdate = true;
                AssociatedObject.Password = e.NewValue as string;
                _skipUpdate = false;
            }
            _value = e.NewValue as string;
        }
    }

    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        _skipUpdate = true;
        Password = AssociatedObject.Password;
        _skipUpdate = false;
    }
}

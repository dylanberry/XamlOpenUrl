using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamlOpenUrl.Extensions
{
    [ContentProperty(nameof(Url))]
    public class OpenUrlExtension : BindableObject, IMarkupExtension<ICommand>, ICommand
    {
        public static readonly BindableProperty UrlProperty =
            BindableProperty.Create(nameof(Url), typeof(string), typeof(OpenUrlExtension), null);

        public string Url
        {
            get => (string)GetValue(UrlProperty);
            set => SetValue(UrlProperty, value);
        }

        protected internal bool IsNavigating { get; private set; }

        public bool CanExecute(object parameter) => !IsNavigating;

        public event EventHandler CanExecuteChanged;

        public async void Execute(object parameter)
        {
            IsNavigating = true;
            try
            {
                RaiseCanExecuteChanged();
                var url = parameter as string ?? Url;
                await HandleNavigation(url);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
            finally
            {
                IsNavigating = false;
                RaiseCanExecuteChanged();
            }
        }

        protected virtual void Log(Exception ex)
        {
            Xamarin.Forms.Internals.Log.Warning("Warning", $"{GetType().Name} threw an exception");
            Xamarin.Forms.Internals.Log.Warning("Exception", ex.ToString());
        }

        protected virtual Task HandleNavigation(string url) => Launcher.OpenAsync(url);

        protected void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public ICommand ProvideValue(IServiceProvider serviceProvider) => this;

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) =>
            ProvideValue(serviceProvider);
    }
}

using Shiny.Hosting;
using Shiny.Push;
using Shiny;

namespace ShinyTestNotification
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);

            try
            {
                var push = Host.Current.Services.GetService<IPushManager>();
                var result = await push.RequestAccess();

                if (result.Status == AccessState.Available)
                {
                    // Get the registration token
                    var token = result.RegistrationToken;

                    // Display the token in an alert
                    await Application.Current.MainPage.DisplayAlert("Push Token", token, "OK");

                    // Copy the token to the clipboard
                    await Clipboard.SetTextAsync(token);

                    // Optionally, notify the user that the token has been copied
                    await Application.Current.MainPage.DisplayAlert("Copied", "Token copied to clipboard", "OK");
                }
            }
            catch (Exception ex)
            {
                // Handle exception (optional logging or alert)
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

    }

}

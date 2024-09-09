using Android.App;
using Android.Content.PM;
using Android.OS;

namespace ShinyTestNotification
{
    [Activity(
    LaunchMode = LaunchMode.SingleTop, // TODO: if using local notifications or push
    Theme = "@style/Maui.SplashTheme",
    MainLauncher = true,
    ConfigurationChanges =
        ConfigChanges.ScreenSize |
        ConfigChanges.Orientation |
        ConfigChanges.UiMode |
        ConfigChanges.ScreenLayout |
        ConfigChanges.SmallestScreenSize |
        ConfigChanges.Density
)]
    [IntentFilter(
    new[] {
Shiny.ShinyPushIntents.NotificationClickAction
    },
    Categories = new[] {
          "android.intent.category.DEFAULT"
    }
)]
    public class MainActivity : MauiAppCompatActivity
    {
    }
}

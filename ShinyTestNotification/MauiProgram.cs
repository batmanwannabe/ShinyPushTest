using Microsoft.Extensions.Logging;
using Shiny;
using ShinyTestNotification.Delegates;

#if ANDROID
using Android.App;
#endif

namespace ShinyTestNotification
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseShiny()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif


            builder.Services.AddPush<MyPushDelegate>(
#if ANDROID
                new Shiny.Push.FirebaseConfig(
                    false, // false to ignore embedded configuration
                    "", // appid
                    "", // sender id/project_number
                    "", // project_id
                    "", // api_key
                    DefaultChannel: DefaultChannel()
                )
#endif
            );

            return builder.Build();
        }
        #if ANDROID
        static NotificationChannel? DefaultChannel()
        {

            return new NotificationChannel("default_channel", "default Notification Channel", NotificationImportance.Default)
            {
                LockscreenVisibility = NotificationVisibility.Public
            };


            //return null;
        }
#endif
    }
}

using Shiny.Push;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


#if ANDROID
using Android.Content;
using Android.App;
using AndroidX.Core.App;
#endif

namespace ShinyTestNotification.Delegates
{
    public class MyPushDelegate : IPushDelegate
    {

        public async Task OnReceived(IDictionary<string, string> data)
        {
            // fires when a push notification is received
            // iOS: set content-available: 1  or this won't fire
            // Android: Set data portion of payload
        }

        public async Task OnNewToken(string token)
        {
            // fires when a push registration token change is set by the operating system or provider
            // also fires with RequestAccess value changes (or initial request)
        }


        public async Task OnUnRegistered(string token)
        {
            // fires when IPushManager.UnRegister is called
            // or on startup when permissions are denied to push
        }

        public Task OnEntry(PushNotification notification)
        {
            Console.WriteLine("**** Hi mate");
            return Task.CompletedTask;
        }

        public Task OnReceived(PushNotification notification)
        {
#if ANDROID
            //! By default android doesn't display notifications when the app is in the foreground, and leaves it down to the developer to handle - https://github.com/shinyorg/shiny/issues/1464#issuecomment-2098505394
            AndroidPushNotification androidNotification = (AndroidPushNotification)notification;

            NotificationCompat.Builder builder = androidNotification.CreateBuilder();
            builder.SetAutoCancel(true);

            using NotificationManager service = androidNotification.Platform.GetSystemService<NotificationManager>(Context.NotificationService);
            service.Notify(100, builder.Build());
#endif

            //WeakReferenceMessenger.Default.Send(new NotificationReceivedMessage(true));

            //_pushNotificationFeature.HandleNotificationActivity.UpdateBackend(NotificationActionType.Received, notification.Data).SafeFireAndForget();

            return Task.CompletedTask;

        }
    }

#if APPLE
public partial class MyPushDelegate : IApplePushDelegate
{
    // return null for default value
    public UNNotificationPresentationOptions? GetPresentationOptions(PushNotification notification)
        // show notification when UI is up
        => UNNotificationPresentationOptions.Banner | UNNotificationPresentationOptions.Sound; 

    public UIBackgroundFetchResult? GetFetchResult(PushNotification notification) => null;
}
#endif
}

using Android.Content;
using Android.Views.InputMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foremaui.Platforms.Android.Resources
{
    public static partial class KeyboardHelper
    {
        public static void HideKeyboard()
        {
            var context = Platform.AppContext;
            if (context.GetSystemService(Context.InputMethodService) is InputMethodManager inputMethodManager)
            {
                var activity = Platform.CurrentActivity;
                var token = activity.CurrentFocus?.WindowToken;
                inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);
                activity.Window.DecorView.ClearFocus();
            }
        }
    }
}

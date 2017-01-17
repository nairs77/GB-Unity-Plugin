using System;

namespace GB
{
	using GB.Callback;

	internal interface IPermissionHelper
	{
		
		void SetPermissionCallback (JoypleDelegate<GBPermissionResult> callback);

		void SetAutoOpenPermissionView (bool enabled);

		bool IsAutoOpenPermissionView ();

		bool IsPermissionGranted (string permission);

		bool IsPermissionGranted (string[] permissions);

		bool ShouldShowRequestPermissionRationale (string permission);

		void RequestPermission (string permission, JoypleDelegate<GBPermissionResult> callback);

		void RequestPermission (string[] permissions, JoypleDelegate<GBPermissionResult> callback);

		void ShowDetailPermissionView(bool isSnack, string permission, JoypleDelegate<GBPermissionResult> callback);

		void ShowDetailPermissionView(bool isSnack, string[] permissions, JoypleDelegate<GBPermissionResult> callback);

		void ShowPermissionSnack (string permission, JoypleDelegate<GBPermissionResult> callback);

		void ShowPermissionSnack (string permission, long lDuration, JoypleDelegate<GBPermissionResult> callback);

		void ShowPermissionSnack (string permission, SnackbarDuration duration, JoypleDelegate<GBPermissionResult> callback);

	}
}


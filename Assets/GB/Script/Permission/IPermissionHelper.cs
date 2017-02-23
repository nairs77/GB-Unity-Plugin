using System;

namespace GB
{
	using GB.Callback;

	internal interface IPermissionHelper
	{
		
		void SetPermissionCallback (GBDelegate<GBPermissionResult> callback);

		void SetAutoOpenPermissionView (bool enabled);

		bool IsAutoOpenPermissionView ();

		bool IsPermissionGranted (string permission);

		bool IsPermissionGranted (string[] permissions);

		bool ShouldShowRequestPermissionRationale (string permission);

		void RequestPermission (string permission, GBDelegate<GBPermissionResult> callback);

		void RequestPermission (string[] permissions, GBDelegate<GBPermissionResult> callback);

		void ShowDetailPermissionView(bool isSnack, string permission, GBDelegate<GBPermissionResult> callback);

		void ShowDetailPermissionView(bool isSnack, string[] permissions, GBDelegate<GBPermissionResult> callback);

		void ShowPermissionSnack (string permission, GBDelegate<GBPermissionResult> callback);

		void ShowPermissionSnack (string permission, long lDuration, GBDelegate<GBPermissionResult> callback);

		void ShowPermissionSnack (string permission, SnackbarDuration duration, GBDelegate<GBPermissionResult> callback);

	}
}


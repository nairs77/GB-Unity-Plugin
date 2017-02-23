
namespace GB
{
	using System;
	using GB.Callback;

	public class GBPermissionManager
	{
		
		private static IPermissionHelper _permissionHelper = null;
		private static IPermissionHelper PermissionHelper {
			get {
				if (_permissionHelper == null) {
					#if UNITY_ANDROID
					_permissionHelper = new GBPermissionAndroidHelper();
					#endif
				}

				return _permissionHelper;
			}
		}


		public static void SetPermissionCallback (GBDelegate<GBPermissionResult> callback){
			
			if(PermissionHelper != null)
				PermissionHelper.SetPermissionCallback(callback);
		}

		public static void SetAutoOpenPermissionView (bool enabled){
			
			if(PermissionHelper != null)
				PermissionHelper.SetAutoOpenPermissionView(enabled);
		}

		public static bool IsAutoOpenPermissionView (){

			if (PermissionHelper != null)
				return PermissionHelper.IsAutoOpenPermissionView ();

			return true;
		}

		public static bool IsPermissionGranted (string permission){

			if(PermissionHelper != null)
				return PermissionHelper.IsPermissionGranted(permission);
			
			return true;
		}

		public static bool IsPermissionGranted (string[] permissions){

			if(PermissionHelper != null)
				return PermissionHelper.IsPermissionGranted(permissions);
			
			return true;
		}

		public static bool ShouldShowRequestPermissionRationale (string permission){

			if(PermissionHelper != null)
				return PermissionHelper.ShouldShowRequestPermissionRationale(permission);
			
			return true;
		}

		public static void RequestPermission (string permission, GBDelegate<GBPermissionResult> callback){

			if(PermissionHelper != null)
				PermissionHelper.RequestPermission(permission, callback);
		}

		public static void RequestPermission (string[] permissions, GBDelegate<GBPermissionResult> callback){

			if(PermissionHelper != null)
				PermissionHelper.RequestPermission(permissions, callback);
		}

		public static void ShowDetailPermissionView(bool isSnack, string permission, GBDelegate<GBPermissionResult> callback){
			
			if (PermissionHelper != null)
				PermissionHelper.ShowDetailPermissionView (isSnack, permission, callback);
		}

		public static void ShowDetailPermissionView(bool isSnack, string[] permission, GBDelegate<GBPermissionResult> callback){
			
			if (PermissionHelper != null)
				PermissionHelper.ShowDetailPermissionView (isSnack, permission, callback);
		}

		public static void ShowPermissionSnack (string permission, GBDelegate<GBPermissionResult> callback){

			if(PermissionHelper != null)
				PermissionHelper.ShowPermissionSnack(permission, callback);
		}

		public static void ShowPermissionSnack (string permission, long lDuration, GBDelegate<GBPermissionResult> callback){

			if(PermissionHelper != null)
				PermissionHelper.ShowPermissionSnack(permission, lDuration, callback);
		}

		public static void ShowPermissionSnack (string permission, SnackbarDuration duration, GBDelegate<GBPermissionResult> callback){

			if(PermissionHelper != null)
				PermissionHelper.ShowPermissionSnack(permission, duration, callback);
		}

	}
}
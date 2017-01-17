
namespace GB
{
	using System;
	using System.Collections.Generic;
	using GB.Callback;
	using SimpleJSON;

	public class GBPermissionResult : BaseResult
	{
		
		public GBPermissionStatus PermissionStatus { get; private set; }
		public GBPermission[] Permissions { get; private set; }


		public GBPermissionResult (string result) : base (result){

			Permissions = new GBPermission[]{};

			if (this.Data != null) {
				if(this.Data["permisison_status"] != null)
					PermissionStatus = (JoyplePermissionStatus)Enum.Parse(typeof(JoyplePermissionStatus), this.Data ["permisison_status"]);

				if (this.Data ["permissions"] != null) {
					JSONClass jsonPermissions = this.Data["permissions"].AsObject;
					Permissions = new GBPermission[jsonPermissions.Count];
					int i = 0;
					foreach (string key in jsonPermissions.Keys)
						Permissions [i++] = new JoyplePermission (key, jsonPermissions [key]);	
				}
			}

		}

		public override string ToString (){
			
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			foreach(GBPermission permission in Permissions){
				sb.Append (permission);
				sb.Append (", ");
			}
			return string.Format ("[GBPermissionResult: Status={0}, Permissions={1}]", PermissionStatus, sb);
		}


		public class GBPermission {

			public string PermissionName { get; private set; }
			public GBPermissionStatus Status { get; private set; }


			public GBPermission(string permissionName, string status){

				PermissionName = permissionName;
				Status = (GBPermissionStatus)Enum.Parse(typeof(GBPermissionStatus), status);
			}

			public override string ToString (){
				return string.Format ("[JoyplePermission: PermissionName={0}, Status={1}]", PermissionName, Status);
			}
		}

	}

}


using UnityEngine;
using System;
using SimpleJSON;
using GB.Account;

namespace GB
{
    internal interface IParseObject {
		void parseJSON(JSONNode root);
	}

	public sealed class GBSession : IParseObject {		
		
		public string userKey { get; private set; }			

        public AuthType authType { get; private set; }

		public string userId { get; private set; }

		public SessionState state { get; private set; }
		public GBSession(JSONNode root)
		{
			this.parseJSON(root);
		}
		
		public void parseJSON(JSONNode root)
		{
			userKey = root["ACCOUNT_SEQ"]; 
            authType = (AuthType)root["CHANNEL_TYPE"].AsInt;
			userId = root["CHANNEL_USER_ID"];
		}

		public bool isConnectedChannel() {
			return authType == AuthType.FACEBOOK ? true : false;
		}

		public string getUserKey() {
			return userKey;
		}
		
		public override string ToString() {
			
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append("\n userKey=").Append(userKey);
			// sb.Append("\n country_code").Append(countryCode);
			// sb.Append("\n nickName=").Append(nickName);
			// sb.Append("\n emailCert=").Append(emailCert);
			// sb.Append("\n profileImage=").Append(profileImage);
			// sb.Append("\n greetingMessage=").Append(greetingMessage);
			// sb.Append("\n joinType=").Append(joinType);
			// sb.Append("\n joinDate=").Append(joinDate);
			// sb.Append("\n quit=").Append(quit);
			// sb.Append("\n quitDate=").Append(quitDate);
			// sb.Append("\n blocked=").Append(blocked);
			// sb.Append("\n blockedDate=").Append(blockedDate);
			// sb.Append("\n phoneCert=").Append(phoneCert);		
			return sb.ToString();
		}
	}
}
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
		
		public int userKey { get; private set; }			

        public AuthType authType { get; private set; }
/*
		public string nickName { get; private set; }		
		public int emailCert { get; private set; }			
		public string profileImage { get; private set; }
		public string greetingMessage { get; private set; }
		public int joinType { get; private set; }
		public long joinDate { get; private set; }
		public int quit { get; private set; }
		public long quitDate { get; private set; }
		public int blocked { get; private set; }
		public long blockedDate { get; private set; }
		public int policyAgree { get; private set; }
		public int phoneCert { get; private set; }	
		public string countryCode { get; private set; }
*/		
		public GBSession(JSONNode root)
		{
			this.parseJSON(root);
		}
		
		public void parseJSON(JSONNode root)
		{
			userKey = root["userkey"].AsInt;
            authType = root["authtype"].AsInt;
			// nickName = root["nickname"];
			// emailCert = root["email_cert"].AsInt;
			// profileImage = root["profile_img"];
			// greetingMessage = root["greeting_msg"];
			// countryCode = root["country_code"];
			// joinType = root["join_type"].AsInt;
			// joinDate = System.Int64.Parse(root["join_date"]);
			// quit = root["quit"].AsInt;
			// quitDate = System.Int64.Parse(root["quit_date"]);
			// blocked = root["blocked"].AsInt;
			// blockedDate = System.Int64.Parse(root["blocked_date"]);
			// policyAgree = root["policy_agree"].AsInt;
			// phoneCert = root["phone_cert"].AsInt;		
		}
		
		public override string ToString() {
			
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append("\n userKey=").Append(userKey);
			sb.Append("\n country_code").Append(countryCode);
			sb.Append("\n nickName=").Append(nickName);
			sb.Append("\n emailCert=").Append(emailCert);
			sb.Append("\n profileImage=").Append(profileImage);
			sb.Append("\n greetingMessage=").Append(greetingMessage);
			sb.Append("\n joinType=").Append(joinType);
			sb.Append("\n joinDate=").Append(joinDate);
			sb.Append("\n quit=").Append(quit);
			sb.Append("\n quitDate=").Append(quitDate);
			sb.Append("\n blocked=").Append(blocked);
			sb.Append("\n blockedDate=").Append(blockedDate);
			sb.Append("\n phoneCert=").Append(phoneCert);		
			return sb.ToString();
		}
	}
}
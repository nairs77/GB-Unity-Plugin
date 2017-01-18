using UnityEngine;
using System.Collections;
using SimpleJSON;

namespace GB {
	
	public interface IGBObject {
		void parseJSON(JSONNode root);
	}

	public sealed class LocalUser : IGBObject {		
		
		public int userKey { get; private set; }			
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
		
		public LocalUser(JSONNode root)
		{
			this.parseJSON(root);
		}
		
		public void parseJSON(JSONNode root)
		{
			userKey = root["userkey"].AsInt;
			nickName = root["nickname"];
			emailCert = root["email_cert"].AsInt;
			profileImage = root["profile_img"];
			greetingMessage = root["greeting_msg"];
			countryCode = root["country_code"];
			joinType = root["join_type"].AsInt;
			joinDate = System.Int64.Parse(root["join_date"]);
			quit = root["quit"].AsInt;
			quitDate = System.Int64.Parse(root["quit_date"]);
			blocked = root["blocked"].AsInt;
			blockedDate = System.Int64.Parse(root["blocked_date"]);
			policyAgree = root["policy_agree"].AsInt;
			phoneCert = root["phone_cert"].AsInt;		
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
	
	public sealed class Device : IGBObject {
		
		public int idx { get; private set; }	
		public int deviceType { get; private set; }
		public string phoneNumber { get; private set; }
		public string uid { get; private set; }
		
		public Device(JSONNode root)
		{
			this.parseJSON(root);
		}
		
		public void parseJSON(JSONNode root)
		{
			idx = root["idx"].AsInt;
			deviceType = root["device_type"].AsInt;
			phoneNumber = root["phone_number"];
			uid = root["uid"];		
		}
		
		public override string ToString() {
			
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append("\n idx=").Append(idx);
			sb.Append("\n deviceType=").Append(deviceType);
			sb.Append("\n phoneNumber=").Append(phoneNumber);
			sb.Append("\n uid=").Append(uid);
			return sb.ToString();
		}
	}
	
	public sealed class Service : IGBObject {
		
		public string serviceType { get; private set; }
		public string serviceId { get; private set; }
		public int status { get; private set; }	
		
		public Service(JSONNode root)
		{
			this.parseJSON(root);
		}
		
		public void parseJSON(JSONNode root)
		{
			serviceType = root["service_type"];
			serviceId = root["service_id"];
			status = root["status"].AsInt;	
		}
		
		public override string ToString() {
			
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append("\n serviceType=").Append(serviceType);
			sb.Append("\n serviceId=").Append(serviceId);
			sb.Append("\n status=").Append(status);
			return sb.ToString();
		}
	}
	
	public class Game : IGBObject {	
		
		public int blocked{ get; private set; }
		public long blockedDate{ get; private set; }
		public int gameCode{ get; private set; }
		public int quit{ get; private set; }
		public long quitDate{ get; private set; }
		public long lastLoginTime{ get; private set; }
		public int deviceType{ get; private set; }
		
		public Game(JSONNode root)
		{
			this.parseJSON(root);
		}
		
		public void parseJSON(JSONNode root)
		{
			blocked = root["blocked"].AsInt;
			blockedDate = System.Int64.Parse(root["blocked_date"]);
			gameCode = root["game_code"].AsInt;	
			quit = root["game_quit"].AsInt;	
			quitDate = System.Int64.Parse(root["game_quit_date"]);
			lastLoginTime = System.Int64.Parse(root["last_logintime"]);
			deviceType = root["device_type"].AsInt;
		}
		
		public override string ToString() {
			
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append("\n blocked=").Append(blocked);
			sb.Append("\n blockedDate=").Append(blockedDate);
			sb.Append("\n gameCode=").Append(gameCode);
			sb.Append("\n quit=").Append(quit);
			sb.Append("\n quitDate=").Append(quitDate);
			sb.Append("\n lastLoginTime=").Append(lastLoginTime);
			sb.Append("\n deviceType=").Append(deviceType);
			return sb.ToString();
		}
	}

	public sealed class Friend : IGBObject {
		
		public int idx { get; private set; }
		public int userKey { get; private set; }
		public int relativeType { get; private set; }
		public int joinType { get; private set; }
		public string nickName { get; private set; }
		public string greetingMessage { get; private set; }
		public long joinDate {get; private set; }
		public long regDate { get; private set; }
		public string profileImage { get; private set; }
		
		public Friend(JSONNode root) 
		{
			this.parseJSON(root);
		}
		
		public void parseJSON(JSONNode root)
		{
			idx = root["idx"].AsInt;
			userKey = root["f_userkey"].AsInt;
			relativeType = root["f_type"].AsInt;	
			joinType = root["join_type"].AsInt;	
			nickName = root["nickname"];
			greetingMessage = root["greeting_msg"];
			joinDate = System.Int64.Parse(root["join_date"]);
			regDate = System.Int64.Parse(root["regdate"]);
			profileImage = root["profile_img"];
		}
		
		public override string ToString() {
			
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append("\n idx=").Append(idx);
			sb.Append("\n userKey=").Append(userKey);
			sb.Append("\n relativeType=").Append(relativeType);
			sb.Append("\n joinType=").Append(joinType);
			sb.Append("\n nickName=").Append(nickName);
			sb.Append("\n greetingMessage=").Append(greetingMessage);
			sb.Append("\n joinDate=").Append(joinDate);
			sb.Append("\n regDate=").Append(regDate);
			sb.Append("\n profileImage=").Append(profileImage);
			return sb.ToString();
		}
	}
}

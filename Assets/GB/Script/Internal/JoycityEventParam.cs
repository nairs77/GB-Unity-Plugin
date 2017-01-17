using UnityEngine;
using System.Collections;
using SimpleJSON;
 using GB;

public sealed class GBEventParam {
	
	private readonly GBEvent GBEvent;
	private readonly JSONClass json;
	
	public class Builder {
		
		public GBEvent GBEvent;
		public JSONClass json = new JSONClass();	
		
		public Builder(GBEvent GBEvent) 
		{
			this.GBEvent = GBEvent;			
		}
		
		public Builder Add(string key, System.Object paramValue) 
		{			
			if(paramValue is string)
				json[key] = (string) paramValue;
			else if(paramValue is int) {					
				int param = (int) paramValue;
				json[key] = System.Convert.ToString(param);				
			}
			return this;
		}
		
		public GBEventParam Build() {
			return new GBEventParam(this);
		}
	}
	
	private GBEventParam(Builder builder) 
	{		
		GBEvent = builder.GBEvent;
		json = builder.json;
	}
	
	public override string ToString() 
	{	
		json["event"] = GBEvent.ToString();					
		return json.ToString();				
	}
	
	public GBEvent currentEvent()
	{
		return GBEvent;
	}
}
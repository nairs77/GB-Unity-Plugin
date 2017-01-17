using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;
using GB;

public sealed class GBInAppItem : IGBObject {

	public string price { get; private set;}
	
	public string currency_symbol { get; private set;}
	
	public string currency { get; private set;}

	public string description { get; private set;}

	public string title { get; private set;}

	public string productId { get; private set;}

	public GBInAppItem(JSONNode root) {
		this.parseJSON(root);
	}

	public void parseJSON(JSONNode root) {
		price = root["price"];
		currency = root["currency"];
		currency_symbol = root["currency_symbol"];
		description = root["description"];
		title = root["title"];
		productId = root["product_id"];
	}
	
	public override string ToString() {
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		sb.Append("\n Title =").Append(title);
		sb.Append("\n product id =").Append(productId);
		sb.Append("\n price =").Append(price);
		sb.Append("\n currency =").Append(currency);
		sb.Append("\n currency_symbol =").Append(currency_symbol);
		sb.Append("\n description =").Append(description);
		return sb.ToString();
	}		
}


public sealed class GBInventory {
	private List<GBInAppItem> MyInventory = new List<GBInAppItem>();

	public GBInventory(JSONNode root) {
		int count = root["count"].AsInt;
	
		List<GBInAppItem> inventory = new List<GBInAppItem>();
		
		for (int i = 0; i < count; i++) {
			string key = "item" + i.ToString();
			inventory.Add(new GBInAppItem(root[key]));
		}
		
		this.MyInventory = inventory;
	}
	
	public List<GBInAppItem> getInventory() {
		return this.MyInventory;
	}
	
	public override string ToString() {
		System.Text.StringBuilder sb = new System.Text.StringBuilder();

		foreach(GBInAppItem item in MyInventory) {
			sb.Append("\n item").Append(item.ToString());
		}
		
		return sb.ToString();
	}
}


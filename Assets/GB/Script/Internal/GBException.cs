using UnityEngine;
using System.Collections;
using SimpleJSON;

public class GBException : System.Exception {

	private int errorCode;
	private string errorMessage;

	/**
	 *  From GB server error
	 */
	private static readonly string API_RESPONSE_ERRORCODE_KEY = "errorCode";
	private static readonly string API_RESPONSE_ERRORMESSAGE_KEY = "errorType";
	/**
	 * From GB SDK error
	 **/
	 
	private static readonly string API_RESPONSE_ERROR_CODE_KEY = "error_code";
	private static readonly string API_RESPONSE_ERROR_MESSAGE_KEY = "error_message";

	public GBException()
	{
	}
	
	public GBException(string message)
		: base(message)
	{
		JSONNode root = JSON.Parse(message);

		if (message.Contains(API_RESPONSE_ERRORCODE_KEY)) {
			errorCode = System.Int32.Parse (root[API_RESPONSE_ERRORCODE_KEY]);
			errorMessage = root [API_RESPONSE_ERRORMESSAGE_KEY];						
		} else if (message.Contains(API_RESPONSE_ERROR_CODE_KEY)) {
			errorCode = System.Int32.Parse (root[API_RESPONSE_ERROR_CODE_KEY]);
			errorMessage = root [API_RESPONSE_ERROR_MESSAGE_KEY];			
		} else {
			Debug.Log("[Exception No Search Key] : " + message);
		}
	}
	
	public GBException(JSONNode root) {
		errorCode = System.Int32.Parse(root["errorCode"]);
		errorMessage = root["errorType"];
	}
	
	public int getErrorCode() {
		return errorCode;
	}

	public string getErrorMessage() {
		return errorMessage;
	}
}

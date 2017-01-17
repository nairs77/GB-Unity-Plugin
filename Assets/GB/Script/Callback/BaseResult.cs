﻿

namespace GB.Callback
{

	using System;
	using SimpleJSON;
	using System.Collections.Generic;

	public abstract class BaseResult : IResult
	{

		public static readonly string API_RESPONSE_CALLBACKID_KEY = "callback_id";
//		private static readonly string API_RESPONSE_NAME_KEY = "name";
//		private static readonly string API_RESPONSE_ERROR_KEY = "error";
//		private static readonly string APT_RESPONSE_ERROR_MESSAGE_KEY = "error_name";
//		private static readonly string API_RESPONSE_ERROR_CODE = "error_code";

//		protected static readonly string API_RESPONSE_RESULT_KEY = "result";
//		protected static readonly string API_RESPONSE_DATA_KEY = "data";

		public int Status { get; private set; }
		public string CallbackId { get; private set; }
		public bool IsKeepCallback { get; protected set; }
		public string Name { get; private set; }

		public string ErrorMessage { get; private set; }
		public int ErrorCode { get; private set; }

		public string RawResult { get; private set; }

		public JSONNode Error { get; private set; }
		public JSONNode Data { get; private set; }

		/** Response Format
		 *  {
		 * 		"status : 1" - 0, 1
		 *		"is_keep_callback": false,
		 *		"method_name": "permission",
		 *		"callback_id": "ag",
		 *		"result": {
		 *			"data": {},
		 *			"error": {
		 *				"error_code": 12,
		 *				"error_message": "permission is null"
		 *			}
		 *		}
		 *  }
		 */ 

		public BaseResult (string result){

			IsKeepCallback = false;

			RawResult = result;
			JSONNode root = JSON.Parse(result);


			Status = root ["status"].AsInt;
			IsKeepCallback = root ["is_keep_callback"].AsBool;

			if (root [API_RESPONSE_CALLBACKID_KEY] != null)
				CallbackId = root [API_RESPONSE_CALLBACKID_KEY];
			

			if (root ["method_name"] != null)
				Name = root ["method_name"];
			
			var response = root ["result"];
			if (response != null) {
				
				if(response["data"] != null)
					Data = response ["data"];

				var response_error = response ["error"];
				if (response_error!= null) {
					Error = response_error;
					if (response_error ["error_message"] != null)
						ErrorMessage = response_error ["error_message"];
					if (response_error ["error_code"] != null)
						ErrorCode = response_error ["error_code"].AsInt;
				}
			}
		}


		public override string ToString (){
			return this.RawResult;
		}

	}

}


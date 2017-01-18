

namespace GB
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using SimpleJSON;
	using System.Text;

	public class JoypleMethodArguments
	{
		
		// Fields
		private IDictionary<string, object> arguments = new Dictionary<string, object> ();


		// Constructors
		public JoypleMethodArguments () : this (new Dictionary<string, object> ()) { }

		public JoypleMethodArguments (JoypleMethodArguments methodArgs) : this (methodArgs.arguments) { }

		private JoypleMethodArguments (IDictionary<string, object> arguments){
			this.arguments = arguments;
		}
			
		// Static Methods
		private static Dictionary<string, string> ToStringDict (IDictionary<string, object> dict){
			if (dict == null) {
				return null;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string> ();
			foreach (KeyValuePair<string, object> current in dict) {
				dictionary [current.Key] = current.Value.ToString ();
			}
			return dictionary;
		}
			
		// Methods
		public void AddCommaSeparatedList (string argumentName, IEnumerable<string> value){
			if (value != null) {
				this.arguments [argumentName] =  string.Join (",", (new List<string>(value)).ToArray());
			}
		}

		public void AddDictionary (string argumentName, IDictionary<string, object> dict){
			if (dict != null) {
//				this.arguments [argumentName] = JoypleMethodArguments.ToStringDict (dict);
				this.arguments [argumentName] = dict;
			}
		}

		public void AddList<T> (string argumentName, IEnumerable<T> list){
			if (list != null) {
				this.arguments [argumentName] = list;
			}
		}

		public void AddNullablePrimitive<T> (string argumentName, T? nullable) where T : struct{
			if (nullable.HasValue) {
				this.arguments [argumentName] = nullable.Value;
			}
		}

		public void AddPrimative<T> (string argumentName, T value) where T : struct{
			this.arguments [argumentName] = value;
		}

		public void AddString (string argumentName, string value){
			if (!string.IsNullOrEmpty (value)) {
				this.arguments [argumentName] = value;
			}
		}

		public void AddUri (string argumentName, Uri uri){
			if (uri != null && !string.IsNullOrEmpty (uri.AbsoluteUri)) {
				this.arguments [argumentName] = uri.ToString ();
			}
		}

		public string ToJsonString (){
			return Serializer.Serialize (this.arguments);
		}


		private sealed class Serializer{

			private StringBuilder builder;

			private Serializer (){
				this.builder = new StringBuilder ();
			}

			public static string Serialize (object obj){
				Serializer serializer = new Serializer ();
				serializer.SerializeValue (obj);
				return serializer.builder.ToString ();
			}

			private void SerializeValue (object value){
				
				if (value == null) {
					this.builder.Append ("null");
				}
				else {
					string str;
					if ((str = (value as string)) != null) {
						this.SerializeString (str);
					}
					else {
						if (value is bool) {
							this.builder.Append (value.ToString ().ToLower ());
						}
						else {
							IList array;
							if ((array = (value as IList)) != null) {
								this.SerializeArray (array);
							}
							else {
								IDictionary obj;
								if ((obj = (value as IDictionary)) != null) {
									this.SerializeObject (obj);
								}
								else {
									if (value is char) {
										this.SerializeString (value.ToString ());
									}
									else {
										this.SerializeOther (value);
									}
								}
							}
						}
					}
				}
			}

			private void SerializeObject (IDictionary obj){
				
				bool flag = true;
				this.builder.Append ('{');
				IEnumerator enumerator = obj.Keys.GetEnumerator ();
				try {
					while (enumerator.MoveNext ()) {
						object current = enumerator.Current;
						if (!flag) {
							this.builder.Append (',');
						}
						this.SerializeString (current.ToString ());
						this.builder.Append (':');
						this.SerializeValue (obj [current]);
						flag = false;
					}
				}
				finally {
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null) {
						disposable.Dispose ();
					}
				}
				this.builder.Append ('}');
			}

			private void SerializeArray (IList array){
				
				this.builder.Append ('[');
				bool flag = true;
				IEnumerator enumerator = array.GetEnumerator ();
				try {
					while (enumerator.MoveNext ()) {
						object current = enumerator.Current;
						if (!flag) {
							this.builder.Append (',');
						}
						this.SerializeValue (current);
						flag = false;
					}
				}
				finally {
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null) {
						disposable.Dispose ();
					}
				}
				this.builder.Append (']');
			}

			private void SerializeString (string str){
				
				this.builder.Append('"');
				this.builder.Append (str);
				this.builder.Append('"');

//				private void SerializeString (string str)
//				{
//					this.builder.Append ('"');
//					char[] array = str.ToCharArray ();
//					char[] array2 = array;
//					for (int i = 0; i < array2.Length; i++) {
//						char c = array2 [i];
//						switch (c) {
//						case '':
//							this.builder.Append ("\b");
//							goto IL_149;
//						case '	':
//							this.builder.Append ("\t");
//							goto IL_149;
//						case '
//							':
//							this.builder.Append ("\n");
//							goto IL_149;
//						case '
//							':
//							IL_42:
//							if (c == '"') {
//								this.builder.Append ("\"");
//								goto IL_149;
//							}
//							if (c != '\') {
//								int num = Convert.ToInt32 (c);
//								if (num >= 32 && num <= 126) {
//									this.builder.Append (c);
//								}
//								else {
//									this.builder.Append ("\u" + Convert.ToString (num, 16).PadLeft (4, '0'));
//								}
//								goto IL_149;
//								}
//								this.builder.Append ("\\");
//								goto IL_149;
//								case '
//								':
//								this.builder.Append ("\f");
//								goto IL_149;
//								case '
//								':
//								this.builder.Append ("\r");
//								goto IL_149;
//								}
//								goto IL_42;
//								IL_149:;
//								}
//								this.builder.Append ('"');
//								}

			}

			private void SerializeOther (object value){
				if (value is float || value is int || value is uint || value is long || value is double || value is sbyte || value is byte || value is short || value is ushort || value is ulong || value is decimal) {
					this.builder.Append (value.ToString ());
				}
				else {
					this.SerializeString (value.ToString ());
				}
			}
		}
			

	}
}



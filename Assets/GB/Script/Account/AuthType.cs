/**
 * @brief Specify AuthType
 * @author nairs77@GB.com
 */

public struct AuthType 
{
	public int TypeValue { get; set; }
	#if UNITY_ANDROID
	public static readonly int NONE = 0;
	public static readonly int GUEST = 1;
	public static readonly int FACEBOOK = 2;
	public static readonly int GOOGLE = 3;
	#else 
	public static readonly int GUEST = 0;
	public static readonly int EMAIL = 1;
	public static readonly int NEST = 2;
	public static readonly int JOIN = 3;
	public static readonly int FACEBOOK = 4;
	public static readonly int TWITTER = 5;
	public static readonly int GOOGLE_PLUS = 6;
	public static readonly int NAVER = 7;
	public static readonly int NONE = 8;
	#endif
		
	// Equals, HashCode 				
	public override bool Equals(object obj) {

		AuthType compareObject = (AuthType)obj;
		return compareObject.TypeValue.Equals(this.TypeValue);
	}

	public override int GetHashCode() {
		return base.GetHashCode();
	}

	public static bool operator ==(AuthType left, AuthType right) {
		return left.Equals(right);
	}

	public static bool operator !=(AuthType left, AuthType right) {
		return !left.Equals(right);
	}

	public static bool operator >(AuthType left, AuthType right)
	{
		return (left.TypeValue > right.TypeValue);
	}

	public static bool operator <(AuthType left, AuthType right)
	{
		return (left.TypeValue < right.TypeValue);
	}            

	public static implicit operator AuthType(int intValue)
	{
		return new AuthType
		{
			TypeValue = intValue
		};
	}
}
/*
#if UNITY_ANDROID
public enum AuthType {
	NONE,
	GUEST,
	NEST, 
	GB,
	FACEBOOK,
	GOOGLE_PLUS,
	TWITTER,
	NAVER,
	GOOGLE_PLAY,
	REFRESH_TOKEN,
	JOIN,
}

public enum LoginUIType {
	NONE,
	LOGIN_UI,
	OTHER_LOGIN_UI,
}
/*
#elif UNITY_IPHONE
public enum AuthType {
	GUEST,
	EMAIL,
	NEST,
	JOIN,
	FACEBOOK,
	TWITTER,
	GOOGLE_PLUS,
	NAVER,
	NONE,
}
*/
public enum LoginUIType {
	NONE,
	LOGIN_UI,
	OTHER_LOGIN_UI,
}
/*
#else
public enum AuthType {
	GUEST,
	EMAIL,
	NEST,
	JOIN,
	FACEBOOK,
	TWITTER,
	GOOGLE_PLUS,
	NAVER,
	NONE,    
}
#endif
*/

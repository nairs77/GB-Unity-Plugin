/**
 * @brief Specify AuthType
 * @author nairs77@GB.com
 */

public struct AuthType 
{
	public int TypeValue { get; set; }

	public static readonly int GUEST = 0;
	public static readonly int GOOGLE = 1;	
	public static readonly int FACEBOOK = 2;
	public static readonly int KAKAO = 3;
		
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
using System;

namespace GB.Callback
{
	public delegate void JoypleDelegate<T> (T result) where T : IResult;
}


using System;

namespace GB.Callback
{
	public delegate void GBDelegate<T> (T result) where T : IResult;
}


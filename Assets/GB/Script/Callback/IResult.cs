﻿using System;
using System.Collections.Generic;

namespace GB.Callback
{
	public interface IResult
	{

		int Status { get; }
		string CallbackId { get; }
		// 콜백유지 여부
		bool IsKeepCallback { get; }
		string Name { get; }

		string ErrorMessage { get; }
		int ErrorCode { get; }

		string RawResult { get; }

	}
}


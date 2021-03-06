﻿using NUnit.Framework;
using Rocks.Exceptions;
using System;

namespace Rocks.Tests.Exceptions
{
	public sealed class ExpectationExceptionTests
		: ExceptionTests<ExpectationException, Exception>
	{
		[Test]
		public void Create() => base.CreateExceptionTest();

		[Test]
		public void CreateWithMessage() =>
			base.CreateExceptionWithMessageTest(Guid.NewGuid().ToString("N"));

		[Test]
		public void CreateWithMessageAndInnerException() =>
			base.CreateExceptionWithMessageAndInnerExceptionTest(Guid.NewGuid().ToString("N"));

		[Test]
		public void Roundtrip() =>
			base.RoundtripExceptionTest(Guid.NewGuid().ToString("N"));
	}
}

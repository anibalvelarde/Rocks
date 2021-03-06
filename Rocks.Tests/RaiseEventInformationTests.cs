﻿using NUnit.Framework;
using System;

namespace Rocks.Tests
{
	public static class RaiseEventInformationTests
	{
		[Test]
		public static void Create()
		{
			var args = new EventArgs();
			var information = new RaiseEventInformation("a", args);

			Assert.That(information.Name, Is.EqualTo("a"), nameof(information.Name));
			Assert.That(information.Args, Is.EqualTo(args), nameof(information.Args));
		}
	}
}

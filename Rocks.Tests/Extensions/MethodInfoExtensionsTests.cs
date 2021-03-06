﻿using NUnit.Framework;
using Rocks.Extensions;
using System;
using static Rocks.Extensions.MethodInfoExtensions;

namespace Rocks.Tests.Extensions
{
	public sealed class MethodInfoExtensionsTests
	{
		[Test]
		public void ContainsOutInitializers() =>
			Assert.That(this.GetType().GetMethod(
				nameof(this.TargetWithOutArgument))!.GetOutInitializers(), 
				Is.EqualTo("a = default!;"));

		[Test]
		public void ContainsOutInitializersWhenArgumentTypeIsArray() =>
			Assert.That(this.GetType().GetMethod(
				nameof(this.TargetWithOutArrayArgument))!.GetOutInitializers(),
				Is.EqualTo("a = default!;"));

		public void TargetWithOutArgument(out int a) => a = 0; 
		public void TargetWithOutArrayArgument(out int[] a) => a = Array.Empty<int>(); 
	}
}

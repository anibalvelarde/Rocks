﻿using NUnit.Framework;
using Rocks.Templates;

namespace Rocks.Tests.Templates
{
	public static class PropertyTemplatesTests
	{
		[Test]
		public static void GetProperty() =>
			Assert.That(PropertyTemplates.GetProperty("a", "b", "c", "d", "e", "f"),
				Is.EqualTo("fd a eb { c }"));

		[Test]
		public static void GetPropertyIndexer() =>
			Assert.That(PropertyTemplates.GetPropertyIndexer("a", "b", "c", "d", "e", "f"),
				Is.EqualTo("fd a ethis[b] { c }"));

		[Test]
		public static void GetPropertyGetWithReferenceTypeReturnValueAndHasEventsIsTrue() =>
			Assert.That(PropertyTemplates.GetPropertyGetWithReferenceTypeReturnValue(1, "b", "c", "d", "e", "f", "g", true), Is.EqualTo(
@"g get
{
	if (this.handlers.TryGetValue(1, out var methodHandlers))
	{
		foreach(var methodHandler in methodHandlers)
		{
			if(d)
			{
#pragma warning disable CS8604
				var result = methodHandler.Method != null ?
					(c)((e)methodHandler.Method)(b) :
					((R.HandlerInformation<c>)methodHandler).ReturnValue;
#pragma warning restore CS8604
				methodHandler.RaiseEvents(this);
				methodHandler.IncrementCallCount();
				return result;
			}
		}
	}

	throw new RE.ExpectationException($""No handlers were found for f"");
}"));

		[Test]
		public static void GetPropertyGetWithReferenceTypeReturnValueAndHasEventsIsFalse() =>
			Assert.That(PropertyTemplates.GetPropertyGetWithReferenceTypeReturnValue(1, "b", "c", "d", "e", "f", "g", false), Is.EqualTo(
@"g get
{
	if (this.handlers.TryGetValue(1, out var methodHandlers))
	{
		foreach(var methodHandler in methodHandlers)
		{
			if(d)
			{
#pragma warning disable CS8604
				var result = methodHandler.Method != null ?
					(c)((e)methodHandler.Method)(b) :
					((R.HandlerInformation<c>)methodHandler).ReturnValue;
#pragma warning restore CS8604
				
				methodHandler.IncrementCallCount();
				return result;
			}
		}
	}

	throw new RE.ExpectationException($""No handlers were found for f"");
}"));

		[Test]
		public static void GetPropertyGetWithReferenceTypeReturnValueAndNoIndexersAndHasEventsIsTrue() =>
			Assert.That(PropertyTemplates.GetPropertyGetWithReferenceTypeReturnValueAndNoIndexers(1, "b", "c", "d", "e", true), Is.EqualTo(
@"e get
{
	if (this.handlers.TryGetValue(1, out var methodHandlers))
	{
		var methodHandler = methodHandlers[0];
#pragma warning disable CS8604
		var result = methodHandler.Method != null ?
			(c)((d)methodHandler.Method)(b) :
			((R.HandlerInformation<c>)methodHandler).ReturnValue;
#pragma warning restore CS8604
		methodHandler.RaiseEvents(this);
		methodHandler.IncrementCallCount();
		return result;
	}

	throw new RE.ExpectationException(""No handlers were found."");
}"));

		[Test]
		public static void GetPropertyGetWithReferenceTypeReturnValueAndNoIndexersAndHasEventsIsFalse() =>
			Assert.That(PropertyTemplates.GetPropertyGetWithReferenceTypeReturnValueAndNoIndexers(1, "b", "c", "d", "e", false), Is.EqualTo(
@"e get
{
	if (this.handlers.TryGetValue(1, out var methodHandlers))
	{
		var methodHandler = methodHandlers[0];
#pragma warning disable CS8604
		var result = methodHandler.Method != null ?
			(c)((d)methodHandler.Method)(b) :
			((R.HandlerInformation<c>)methodHandler).ReturnValue;
#pragma warning restore CS8604
		
		methodHandler.IncrementCallCount();
		return result;
	}

	throw new RE.ExpectationException(""No handlers were found."");
}"));

		[Test]
		public static void GetPropertyGetWithValueTypeReturnValueAndHasEventsIsTrue() =>
			Assert.That(PropertyTemplates.GetPropertyGetWithValueTypeReturnValue(1, "b", "c", "d", "e", "f", "g", true), Is.EqualTo(
@"g get
{
	if (this.handlers.TryGetValue(1, out var methodHandlers))
	{
		foreach(var methodHandler in methodHandlers)
		{
			if(d)
			{
#pragma warning disable CS8604
				var result = methodHandler.Method != null ?
					(c)((e)methodHandler.Method)(b) :
					((R.HandlerInformation<c>)methodHandler).ReturnValue;
#pragma warning restore CS8604
				methodHandler.RaiseEvents(this);
				methodHandler.IncrementCallCount();
				return result;
			}
		}
	}

	throw new RE.ExpectationException($""No handlers were found for f"");
}"));

		[Test]
		public static void GetPropertyGetWithValueTypeReturnValueAndHasEventsIsFalse() =>
			Assert.That(PropertyTemplates.GetPropertyGetWithValueTypeReturnValue(1, "b", "c", "d", "e", "f", "g", false), Is.EqualTo(
@"g get
{
	if (this.handlers.TryGetValue(1, out var methodHandlers))
	{
		foreach(var methodHandler in methodHandlers)
		{
			if(d)
			{
#pragma warning disable CS8604
				var result = methodHandler.Method != null ?
					(c)((e)methodHandler.Method)(b) :
					((R.HandlerInformation<c>)methodHandler).ReturnValue;
#pragma warning restore CS8604
				
				methodHandler.IncrementCallCount();
				return result;
			}
		}
	}

	throw new RE.ExpectationException($""No handlers were found for f"");
}"));

		[Test]
		public static void GetPropertyGetWithValueTypeReturnValueAndNoIndexersAndHasEventsIsTrue() =>
			Assert.That(PropertyTemplates.GetPropertyGetWithValueTypeReturnValueAndNoIndexers(1, "b", "c", "d", "e", true), Is.EqualTo(
@"e get
{
	if (this.handlers.TryGetValue(1, out var methodHandlers))
	{
		var methodHandler = methodHandlers[0];
#pragma warning disable CS8604
		var result = methodHandler.Method != null ?
			(c)((d)methodHandler.Method)(b) :
			((R.HandlerInformation<c>)methodHandler).ReturnValue;
#pragma warning restore CS8604
		methodHandler.RaiseEvents(this);
		methodHandler.IncrementCallCount();
		return result;
	}

	throw new RE.ExpectationException(""No handlers were found."");
}"));

		[Test]
		public static void GetPropertyGetWithValueTypeReturnValueAndNoIndexersAndHasEventsIsFalse() =>
			Assert.That(PropertyTemplates.GetPropertyGetWithValueTypeReturnValueAndNoIndexers(1, "b", "c", "d", "e", false), Is.EqualTo(
@"e get
{
	if (this.handlers.TryGetValue(1, out var methodHandlers))
	{
		var methodHandler = methodHandlers[0];
#pragma warning disable CS8604
		var result = methodHandler.Method != null ?
			(c)((d)methodHandler.Method)(b) :
			((R.HandlerInformation<c>)methodHandler).ReturnValue;
#pragma warning restore CS8604
		
		methodHandler.IncrementCallCount();
		return result;
	}

	throw new RE.ExpectationException(""No handlers were found."");
}"));

		[Test]
		public static void GetPropertyGetForMake() =>
			Assert.That(PropertyTemplates.GetPropertyGetForMake("a"), Is.EqualTo(
@"a get => default;"));

		[Test]
		public static void GetPropertySetAndHasEventsIsTrue() =>
			Assert.That(PropertyTemplates.GetPropertySet(1, "b", "c", "d", "e", "f", true), Is.EqualTo(
@"f set
{
	if (this.handlers.TryGetValue(1, out var methodHandlers))
	{
		var foundMatch = false;

		foreach(var methodHandler in methodHandlers)
		{
			if(c)
			{
				foundMatch = true;

				if(methodHandler.Method != null)
				{
#pragma warning disable CS8604
					((d)methodHandler.Method)(b);
#pragma warning restore CS8604
				}
	
				methodHandler.RaiseEvents(this);
				methodHandler.IncrementCallCount();
				break;
			}
		}

		if(!foundMatch)
		{
			throw new RE.ExpectationException($""No handlers were found for e"");
		}
	}
	else
	{
		throw new RE.ExpectationException($""No handlers were found for e"");
	}
}"));

		[Test]
		public static void GetPropertySetAndHasEventsIsFalse() =>
			Assert.That(PropertyTemplates.GetPropertySet(1, "b", "c", "d", "e", "f", false), Is.EqualTo(
@"f set
{
	if (this.handlers.TryGetValue(1, out var methodHandlers))
	{
		var foundMatch = false;

		foreach(var methodHandler in methodHandlers)
		{
			if(c)
			{
				foundMatch = true;

				if(methodHandler.Method != null)
				{
#pragma warning disable CS8604
					((d)methodHandler.Method)(b);
#pragma warning restore CS8604
				}
	
				
				methodHandler.IncrementCallCount();
				break;
			}
		}

		if(!foundMatch)
		{
			throw new RE.ExpectationException($""No handlers were found for e"");
		}
	}
	else
	{
		throw new RE.ExpectationException($""No handlers were found for e"");
	}
}"));

		[Test]
		public static void GetPropertySetAndNoIndexersAndHasEventsIsTrue() =>
			Assert.That(PropertyTemplates.GetPropertySetAndNoIndexers(1, "b", "c", "d", true), Is.EqualTo(
@"d set
{
	if (this.handlers.TryGetValue(1, out var methodHandlers))
	{
		var methodHandler = methodHandlers[0];

		if(methodHandler.Method != null)
		{
#pragma warning disable CS8604
			((c)methodHandler.Method)(b);
#pragma warning restore CS8604
		}
	
		methodHandler.RaiseEvents(this);
		methodHandler.IncrementCallCount();
	}
	else
	{
		throw new RE.ExpectationException(""No handlers were found."");
	}
}"));

		[Test]
		public static void GetPropertySetAndNoIndexersAndHasEventsIsFalse() =>
			Assert.That(PropertyTemplates.GetPropertySetAndNoIndexers(1, "b", "c", "d", false), Is.EqualTo(
@"d set
{
	if (this.handlers.TryGetValue(1, out var methodHandlers))
	{
		var methodHandler = methodHandlers[0];

		if(methodHandler.Method != null)
		{
#pragma warning disable CS8604
			((c)methodHandler.Method)(b);
#pragma warning restore CS8604
		}
	
		
		methodHandler.IncrementCallCount();
	}
	else
	{
		throw new RE.ExpectationException(""No handlers were found."");
	}
}"));

		[Test]
		public static void GetPropertySetForMake() =>
			Assert.That(PropertyTemplates.GetPropertySetForMake("a"),
				Is.EqualTo("a set { }"));
	}
}

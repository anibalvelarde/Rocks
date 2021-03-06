﻿using NUnit.Framework;
using System;

namespace Rocks.Tests
{
	public static class HandleAction5ArgumentTests
	{
		[Test]
		public static void Make()
		{
			var rock = Rock.Create<IHandleAction5ArgumentTests>();
			rock.Handle(_ => _.Target(1, 2, 3, 4, 5));

			var chunk = rock.Make();
			chunk.Target(1, 2, 3, 4, 5);

			rock.Verify();
		}

		[Test]
		public static void MakeAndRaiseEvent()
		{
			var rock = Rock.Create<IHandleAction5ArgumentTests>();
			rock.Handle(_ => _.Target(1, 2, 3, 4, 5))
				.Raises(nameof(IHandleAction5ArgumentTests.TargetEvent), EventArgs.Empty);

			var wasEventRaised = false;
			var chunk = rock.Make();
			chunk.TargetEvent += (s, e) => wasEventRaised = true;
			chunk.Target(1, 2, 3, 4, 5);

			Assert.That(wasEventRaised, Is.True);
			rock.Verify();
		}

		[Test]
		public static void MakeWithHandler()
		{
			var argumentA = 0;
			var argumentB = 0;
			var argumentC = 0;
			var argumentD = 0;
			var argumentE = 0;

			var rock = Rock.Create<IHandleAction5ArgumentTests>();
			rock.Handle<int, int, int, int, int>(_ => _.Target(1, 2, 3, 4, 5),
				(a, b, c, d, e) => { argumentA = a; argumentB = b; argumentC = c; argumentD = d; argumentE = e; });

			var chunk = rock.Make();
			chunk.Target(1, 2, 3, 4, 5);
			Assert.That(argumentA, Is.EqualTo(1), nameof(argumentA));
			Assert.That(argumentB, Is.EqualTo(2), nameof(argumentB));
			Assert.That(argumentC, Is.EqualTo(3), nameof(argumentC));
			Assert.That(argumentD, Is.EqualTo(4), nameof(argumentD));
			Assert.That(argumentE, Is.EqualTo(5), nameof(argumentE));

			rock.Verify();
		}

		[Test]
		public static void MakeWithExpectedCallCount()
		{
			var rock = Rock.Create<IHandleAction5ArgumentTests>();
			rock.Handle(_ => _.Target(1, 2, 3, 4, 5), 2);

			var chunk = rock.Make();
			chunk.Target(1, 2, 3, 4, 5);
			chunk.Target(1, 2, 3, 4, 5);

			rock.Verify();
		}

		[Test]
		public static void MakeWithHandlerAndExpectedCallCount()
		{
			var argumentA = 0;
			var argumentB = 0;
			var argumentC = 0;
			var argumentD = 0;
			var argumentE = 0;

			var rock = Rock.Create<IHandleAction5ArgumentTests>();
			rock.Handle<int, int, int, int, int>(_ => _.Target(1, 2, 3, 4, 5),
				(a, b, c, d, e) => { argumentA = a; argumentB = b; argumentC = c; argumentD = d; argumentE = e; }, 2);

			var chunk = rock.Make();
			chunk.Target(1, 2, 3, 4, 5);
			Assert.That(argumentA, Is.EqualTo(1), nameof(argumentA));
			Assert.That(argumentB, Is.EqualTo(2), nameof(argumentB));
			Assert.That(argumentC, Is.EqualTo(3), nameof(argumentC));
			Assert.That(argumentD, Is.EqualTo(4), nameof(argumentD));
			Assert.That(argumentE, Is.EqualTo(5), nameof(argumentE));
			argumentA = 0;
			argumentB = 0;
			argumentC = 0;
			argumentD = 0;
			argumentE = 0;
			chunk.Target(1, 2, 3, 4, 5);
			Assert.That(argumentA, Is.EqualTo(1), nameof(argumentA));
			Assert.That(argumentB, Is.EqualTo(2), nameof(argumentB));
			Assert.That(argumentC, Is.EqualTo(3), nameof(argumentC));
			Assert.That(argumentD, Is.EqualTo(4), nameof(argumentD));
			Assert.That(argumentE, Is.EqualTo(5), nameof(argumentE));

			rock.Verify();
		}
	}

	public interface IHandleAction5ArgumentTests
	{
		event EventHandler TargetEvent;
		void Target(int a, int b, int c, int d, int e);
	}
}
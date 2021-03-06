﻿using NUnit.Framework;
using Rocks.Exceptions;

namespace Rocks.Tests
{
	public static class ExpectationsTests
	{
		[Test]
		public static void HandleWithConstant()
		{
			var rock = Rock.Create<IExpectationsTests>();
			rock.Handle(_ => _.Target(1));

			var chunk = rock.Make();
			chunk.Target(1);
		}

		[Test]
		public static void HandleWithConstantAndInvalidValue()
		{
			var rock = Rock.Create<IExpectationsTests>();
			rock.Handle(_ => _.Target(1));

			var chunk = rock.Make();
			Assert.That(() => chunk.Target(2), Throws.TypeOf<ExpectationException>());
		}

		[Test]
		public static void HandleWithIs()
		{
			var rock = Rock.Create<IExpectationsTests>();
			rock.Handle(_ => _.Target(Arg.Is<int>(a => a % 2 == 0)));

			var chunk = rock.Make();
			chunk.Target(2);
		}

		[Test]
		public static void HandleWithIsAndInvalidValue()
		{
			var rock = Rock.Create<IExpectationsTests>();
			rock.Handle(_ => _.Target(Arg.Is<int>(a => a % 2 == 0)));

			var chunk = rock.Make();
			Assert.That(() => chunk.Target(1), Throws.TypeOf<ExpectationException>());
		}

		[Test]
		public static void HandleWithIsAny()
		{
			var rock = Rock.Create<IExpectationsTests>();
			rock.Handle(_ => _.Target(Arg.IsAny<int>()));

			var chunk = rock.Make();
			chunk.Target(1);
		}

		[Test]
		public static void HandleWithCall()
		{
			var rock = Rock.Create<IExpectationsTests>();
			rock.Handle(_ => _.Target(ExpectationsTests.Create()));

			var chunk = rock.Make();
			chunk.Target(44);
		}

		[Test]
		public static void HandleWithCallAndInvalidValue()
		{
			var rock = Rock.Create<IExpectationsTests>();
			rock.Handle(_ => _.Target(ExpectationsTests.Create()));

			var chunk = rock.Make();
			Assert.That(() => chunk.Target(1), Throws.TypeOf<ExpectationException>());
		}

		[Test]
		public static void HandleWithExpression()
		{
			var rock = Rock.Create<IExpectationsTests>();
			rock.Handle(_ => _.Target(44 + ExpectationsTests.Create()));

			var chunk = rock.Make();
			chunk.Target(88);
		}

		[Test]
		public static void HandleWithExpressionAndInvalidValue()
		{
			var rock = Rock.Create<IExpectationsTests>();
			rock.Handle(_ => _.Target(44 + ExpectationsTests.Create()));

			var chunk = rock.Make();
			Assert.That(() => chunk.Target(1), Throws.TypeOf<ExpectationException>());
		}

		public static int Create() => 44;
	}

	public interface IExpectationsTests
	{
		void Target(int a);
	}
}

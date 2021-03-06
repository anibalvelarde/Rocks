﻿using Rocks.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Rocks.Construction.InMemory
{
	internal sealed class InMemoryMethodInformationBuilder :
		MethodInformationBuilder
	{
		internal InMemoryMethodInformationBuilder(SortedSet<string> namespaces,
			ReadOnlyDictionary<int, ReadOnlyCollection<HandlerInformation>> handlers)
			: base(namespaces) => this.Handlers = handlers;

		protected override string GetDelegateCast(MethodInfo baseMethod)
		{
			var key = baseMethod.MetadataToken;

			if (this.Handlers.ContainsKey(key))
			{
				var delegateType = this.Handlers[key][0].Method!.GetType();

				if (baseMethod.IsGenericMethodDefinition)
				{
					delegateType = delegateType.GetGenericTypeDefinition();
				}

				return $"{delegateType.GetFullName(this.Namespaces)}";
			}
			else
			{
				return string.Empty;
			}
		}

		internal ReadOnlyDictionary<int, ReadOnlyCollection<HandlerInformation>> Handlers { get; }
	}
}
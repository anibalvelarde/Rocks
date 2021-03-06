﻿using Rocks.Construction;
using Rocks.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Rocks.Extensions
{
	internal static class MethodInfoExtensions
	{
		internal static bool CanBeSeenByMockAssembly(this MethodInfo @this, NameGenerator generator)
		{
			if(!@this.IsPublic && (@this.IsPrivate || (!@this.IsFamily && !@this.DeclaringType.CanBeSeenByMockAssembly(generator))))
			{
				return false;
			}

			foreach(var parameter in @this.GetParameters())
			{
				if(!parameter.ParameterType.CanBeSeenByMockAssembly(generator))
				{
					return false;
				}
			}

			if(@this.ReturnType != typeof(void))
			{
				if(!@this.ReturnType.CanBeSeenByMockAssembly(generator))
				{
					return false;
				}
			}

			return true;
		}

		internal static MethodMatch Match(this MethodInfo @this, MethodInfo other)
		{
			if(@this.Name != other.Name)
			{
				return MethodMatch.None;
			}
			else
			{
				var thisParameters = @this.GetParameters().ToList();
				var otherParameters = other.GetParameters().ToList();

				if(thisParameters.Count != otherParameters.Count)
				{
					return MethodMatch.None;
				}

				for (var i = 0; i < thisParameters.Count; i++)
				{
					if ((thisParameters[i].ParameterType != otherParameters[i].ParameterType) ||
						(thisParameters[i].GetModifier() != otherParameters[i].GetModifier()))
					{
						return MethodMatch.None;
					}
				}

				return @this.ReturnType != other.ReturnType ? MethodMatch.DifferByReturnTypeOnly : MethodMatch.Exact;
			}
		}

		internal static bool IsSpanLike(this MethodInfo @this) =>
			(@this.ReturnType.IsSpanLike() ||
				@this.GetParameters().Where(param => param.ParameterType.IsSpanLike()).Any());

		internal static bool IsUnsafeToMock(this MethodInfo @this) =>
			@this.IsUnsafeToMock(true);

		internal static bool IsUnsafeToMock(this MethodInfo @this, bool checkForSpecialName)
		{
			var specialNameFlag = checkForSpecialName ? @this.IsSpecialName : false;

			return !specialNameFlag && ((@this.IsPublic && @this.IsVirtual && !@this.IsFinal) || 
				((@this.IsAssembly || @this.IsFamily) && @this.IsAbstract)) &&
				(@this.ReturnType.IsPointer ||
					@this.GetParameters().Where(param => param.ParameterType.IsPointer).Any());
		}

		internal static bool ContainsDelegateConditions(this MethodInfo @this) =>
			(from parameter in @this.GetParameters()
				let parameterType = parameter.ParameterType
				where parameter.IsOut || parameterType.IsByRef ||
					typeof(TypedReference).IsAssignableFrom(parameterType) ||
					typeof(RuntimeArgumentHandle).IsAssignableFrom(parameterType) ||
					TypeDissector.Create(parameterType).IsPointer
				select parameter).Any() || TypeDissector.Create(@this.ReturnType).IsPointer;

		internal static string GetOutInitializers(this MethodInfo @this) =>
			string.Join(Environment.NewLine,
				from parameter in @this.GetParameters()
				where parameter.IsOut
				select $"{parameter.Name} = default!;");

		internal static string GetDelegateCast(this MethodInfo @this)
		{
			var parameters = @this.GetParameters();
			var methodKind = @this.ReturnType != typeof(void) ? "Func" : "Action";

			if (parameters.Length == 0)
			{
				return @this.ReturnType != typeof(void) ?
					$"{methodKind}<{@this.ReturnType.GetFullName(@this.ReturnParameter)}>" : $"{methodKind}";
			}
			else
			{
				var genericArgumentTypes = string.Join(", ", 
					parameters.Select(_ => $"{_.ParameterType.GetFullName(_)}"));
				return @this.ReturnType != typeof(void) ?
					$"{methodKind}<{genericArgumentTypes}, {@this.ReturnType.GetFullName(@this.ReturnParameter)}>" : $"{methodKind}<{genericArgumentTypes}>";
			}
		}

		internal static string GetExpectationChecks(this MethodInfo @this) =>
			string.Join(" && ",
				@this.GetParameters()
				.Where(_ => !TypeDissector.Create(_.ParameterType).IsPointer)
				.Select(_ => CodeTemplates.GetExpectation(_.Name, $"{_.ParameterType.GetFullName(_)}")));

		internal static string GetMethodDescription(this MethodInfo @this) =>
			@this.GetMethodDescription(new SortedSet<string>(), false);

		internal static void AddNamespaces(this MethodInfo @this, SortedSet<string> namespaces)
		{
			namespaces.Add(@this.ReturnType.Namespace);
			@this.GetParameters(namespaces);

			if (@this.IsGenericMethodDefinition)
			{
				@this.GetGenericArguments(namespaces);
			}
		}

		internal static string GetMethodDescription(this MethodInfo @this, SortedSet<string> namespaces) =>
			@this.GetMethodDescription(namespaces, false, RequiresExplicitInterfaceImplementation.No);

		internal static string GetMethodDescription(this MethodInfo @this, SortedSet<string> namespaces, bool includeOverride) =>
			@this.GetMethodDescription(namespaces, includeOverride, RequiresExplicitInterfaceImplementation.No);

		internal static string GetMethodDescription(this MethodInfo @this, SortedSet<string> namespaces, bool includeOverride,
			RequiresExplicitInterfaceImplementation requiresExplicitInterfaceImplementation)
		{
			if (@this.IsGenericMethod)
			{
				@this = @this.GetGenericMethodDefinition();
			}

			@this.ReturnType.AddNamespaces(namespaces);

			var isOverride = includeOverride ? (@this.DeclaringType.IsClass ? "override " : string.Empty) : string.Empty;
			var returnType = @this.ReturnType == typeof(void) ?
				"void" : $"{@this.ReturnType.GetFullName(namespaces, @this.ReturnParameter)}";

			var methodName = @this.Name;
			var generics = string.Empty;
			var constraints = string.Empty;

			if (@this.IsGenericMethodDefinition)
			{
				(generics, constraints) = @this.GetGenericArguments(namespaces);
				constraints = constraints.Length == 0 ? string.Empty : $" {constraints}";
			}

			var parameters = @this.GetParameters(namespaces);
			var explicitInterfaceName = requiresExplicitInterfaceImplementation == RequiresExplicitInterfaceImplementation.Yes ?
				$"{@this.DeclaringType.GetFullName(namespaces)}." : string.Empty;

			return $"{isOverride}{returnType} {explicitInterfaceName}{methodName}{generics}({parameters}){constraints}";
		}
	}
}
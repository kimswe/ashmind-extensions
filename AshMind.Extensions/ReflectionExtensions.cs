﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Contracts = System.Diagnostics.Contracts;
using PureAttribute = JetBrains.Annotations.PureAttribute;

#if !TypeInfo
using TypeInfo = System.Type;
#endif

namespace AshMind.Extensions {
    /// <summary>
    /// Provides a set of extension methods for operations on reflection classes.
    /// </summary>
    public static class ReflectionExtensions {
        #if ICustomAttributeProvider
        /// <summary>
        /// Gets the custom attributes of the specified type defined on this member.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to search for. Only attributes that are assignable to this type are returned.</typeparam>
        /// <param name="member">The member which attributes will be retrieved.</param>
        /// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
        /// <returns>An array of custom attributes applied to this member, or an array with zero (0) elements if no attributes have been applied.</returns>
        /// <exception cref="TypeLoadException">A custom attribute type cannot be loaded.</exception>
        /// <exception cref="InvalidOperationException">This member belongs to a type that is loaded into the reflection-only context.</exception>
        [Contracts.Pure] [Pure] [NotNull]
        public static TAttribute[] GetCustomAttributes<TAttribute>([NotNull] this ICustomAttributeProvider member, bool inherit) 
            where TAttribute : Attribute
        {
            if (member == null) throw new ArgumentNullException("member");
            Contract.EndContractBlock();

            return (TAttribute[])member.GetCustomAttributes(typeof(TAttribute), inherit);
        }

        /// <summary>
        /// Gets the custom attributes of the specified type defined on this member.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to search for. Only attributes that are assignable to this type are returned.</typeparam>
        /// <param name="provider">The member which attributes will be retrieved.</param>
        /// <returns>An array of custom attributes applied to this member, or an array with zero (0) elements if no attributes have been applied.</returns>
        /// <exception cref="TypeLoadException">A custom attribute type cannot be loaded.</exception>
        /// <exception cref="InvalidOperationException">This member belongs to a type that is loaded into the reflection-only context.</exception>
        [Contracts.Pure] [Pure] [NotNull]
        public static TAttribute[] GetCustomAttributes<TAttribute>([NotNull] this ICustomAttributeProvider provider) 
            where TAttribute : Attribute
        {
            if (provider == null) throw new ArgumentNullException("provider");
            Contract.EndContractBlock();

            return (TAttribute[])provider.GetCustomAttributes(typeof(TAttribute), true);
        }
        
        /// <summary>
        /// Gets the custom attributes of the specified type defined on this member.
        /// </summary>
        /// <param name="provider">The provider which attributes will be retrieved.</param>
        /// <param name="attributeType">The type of attribute to search for. Only attributes that are assignable to this type are returned.</param>
        /// <returns>An array of custom attributes applied to this member, or an array with zero (0) elements if no attributes have been applied.</returns>
        /// <exception cref="TypeLoadException">A custom attribute type cannot be loaded.</exception>
        /// <exception cref="NullReferenceException">If <paramref name="attributeType" /> is a null reference (Nothing in Visual Basic).</exception>
        /// <exception cref="InvalidOperationException">This provider is a type loaded into the reflection-only context or a member of such type.</exception>
        [Contracts.Pure] [Pure] [NotNull]
        public static object[] GetCustomAttributes([NotNull] this ICustomAttributeProvider provider, [NotNull] Type attributeType) {
            if (provider == null) throw new ArgumentNullException("provider");
            Contract.EndContractBlock();

            return provider.GetCustomAttributes(attributeType, true);
        }

        /// <summary>
        /// Returns an array containing all the custom attributes defined on this member.
        /// </summary>
        /// <param name="provider">The member which attributes will be retrieved.</param>
        /// <returns>An array that contains all the custom attributes, or an array with zero elements if no attributes are defined.</returns>
        /// <exception cref="TypeLoadException">A custom attribute type cannot be loaded.</exception>
        /// <exception cref="InvalidOperationException">This member belongs to a type that is loaded into the reflection-only context.</exception>
        [Contracts.Pure] [Pure] [NotNull]
        public static object[] GetCustomAttributes([NotNull] this ICustomAttributeProvider provider) {
            if (provider == null) throw new ArgumentNullException("provider");
            Contract.EndContractBlock();

            return provider.GetCustomAttributes(true);
        }

        [Contracts.Pure] [Pure] [NotNull]
        public static TAttribute GetCustomAttribute<TAttribute>([NotNull] this ICustomAttributeProvider provider, bool inherit)
            where TAttribute : Attribute
        {
            if (provider == null) throw new ArgumentNullException("provider");
            Contract.EndContractBlock();

            return provider.GetCustomAttributes<TAttribute>(inherit).Single();
        }

        [Contracts.Pure] [Pure] [NotNull]
        public static TAttribute GetCustomAttribute<TAttribute>([NotNull] this ICustomAttributeProvider provider)
            where TAttribute : Attribute
        {
            if (provider == null) throw new ArgumentNullException("provider");
            Contract.EndContractBlock();

            return provider.GetCustomAttributes<TAttribute>(false).Single();
        }

        [Contracts.Pure] [Pure]
        public static bool IsDefined<TAttribute>([NotNull] this ICustomAttributeProvider provider, bool inherit)
            where TAttribute : Attribute
        {
            if (provider == null) throw new ArgumentNullException("provider");
            Contract.EndContractBlock();

            return provider.IsDefined(typeof(TAttribute), inherit);
        }
        #endif

        [Contracts.Pure] [Pure]
        public static bool IsSameAsOrSubclassOf([NotNull] this TypeInfo type, [NotNull] Type otherType)
        {
            if (type == null)      throw new ArgumentNullException("type");
            if (otherType == null) throw new ArgumentNullException("otherType");
            Contract.EndContractBlock();

            #if TypeInfo
            return type == otherType.GetTypeInfo() || type.IsSubclassOf(otherType);
            #else
            return type == otherType || type.IsSubclassOf(otherType);
            #endif
        }

        [Contracts.Pure] [Pure]
        public static bool IsSameAsOrSubclassOf<TClass>([NotNull] this TypeInfo type)
        {
            if (type == null) throw new ArgumentNullException("type");
            Contract.EndContractBlock();

            return type.IsSameAsOrSubclassOf(typeof(TClass));
        }

        [Contracts.Pure] [Pure]
        public static bool IsSubclassOf<T>([NotNull] this TypeInfo type) 
            where T : class
        {
            if (type == null) throw new ArgumentNullException("type");
            Contract.EndContractBlock();

            return type.IsSubclassOf(typeof(T));
        }

        [Contracts.Pure] [Pure]
        public static bool IsGenericTypeDefinedAs([NotNull] this TypeInfo type, [NotNull] Type otherType) {
            if (type == null) throw new ArgumentNullException("type");
            Contract.EndContractBlock();

            if (!type.IsGenericType)
                return false;

            return type.GetGenericTypeDefinition() == otherType;
        }

        #if MethodInfo_CreateDelegate
        /// <summary>
        /// Creates a delegate of the specified type from a specified method.
        /// </summary>
        /// <typeparam name="TDelegate">The type of the delegate to create.</typeparam>
        /// <param name="method">The method to create the delegate for.</param>
        /// <returns>The delegate for this method.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="method"/> is null.</exception>
        [Contracts.Pure] [Pure] [NotNull]
        public static TDelegate CreateDelegate<TDelegate>([NotNull] this MethodInfo method) {
            if (method == null) throw new ArgumentNullException("method");
            Contract.EndContractBlock();

            return (TDelegate)(object)method.CreateDelegate(typeof(TDelegate));
        }

        /// <summary>
        /// Creates a delegate of the specified type with the specified target from a specified method.
        /// </summary>
        /// <typeparam name="TDelegate">The type of the delegate to create.</typeparam>
        /// <param name="method">The method to create the delegate for.</param>
        /// <param name="target">The object targeted by the delegate.</param>
        /// <returns>The delegate for this method.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="method"/> is null.</exception>
        [Contracts.Pure] [Pure] [NotNull]
        public static TDelegate CreateDelegate<TDelegate>([NotNull] this MethodInfo method, object target) {
            if (method == null) throw new ArgumentNullException("method");
            Contract.EndContractBlock();

            return (TDelegate)(object)method.CreateDelegate(typeof(TDelegate), target);
        }
        #endif

        #if !TypeInfo
        /// <summary>
        /// Determines whether the specified interface is implemented by the specified type.
        /// </summary>
        /// <typeparam name="TInterface">The interface that might be implemented by the <paramref name="type"/>.</typeparam>
        /// <param name="type">The type for which the fact of implementation will be dermined.</param>
        /// <returns><c>true</c> if <paramref name="type"/> implements <typeparamref name="TInterface"/>; otherwise, <c>false</c>.</returns>
        [Contracts.Pure] [Pure]
        public static bool HasInterface<TInterface>([NotNull] this TypeInfo type)
            where TInterface : class
        {
            if (type == null) throw new ArgumentNullException("type");
            Contract.EndContractBlock();

            return type.HasInterface(typeof(TInterface));
        }

        /// <summary>
        /// Determines whether a given interface is implemented by a specified type.
        /// </summary>
        /// <param name="type">The type for which the fact of implementation will be dermined.</param>
        /// <param name="interfaceType">The interface that might be implemented by the <paramref name="type"/>.</param>
        /// <returns><c>true</c> if <paramref name="type"/> implements <paramref name="interfaceType"/>; otherwise, <c>false</c>.</returns>
        [Contracts.Pure] [Pure]
        public static bool HasInterface([NotNull] this TypeInfo type, [NotNull] Type interfaceType) {
            if (type == null)          throw new ArgumentNullException("type");
            if (interfaceType == null) throw new ArgumentNullException("interfaceType");
            Contract.EndContractBlock();

            return type.GetInterfaces().Contains(interfaceType);
        }
        #endif

        #if !Net45_Property_SetValue
        public static void SetValue([NotNull] this PropertyInfo property, [CanBeNull] object obj, [CanBeNull] object value) {
            if (property == null) throw new ArgumentNullException("property");
            Contract.EndContractBlock();

            property.SetValue(obj, value, null);
        }

        [Contracts.Pure] [Pure] [CanBeNull]
        public static object GetValue([NotNull] this PropertyInfo property, [CanBeNull] object obj) {
            if (property == null) throw new ArgumentNullException("property");
            Contract.EndContractBlock();

            return property.GetValue(obj, null);
        }
        #endif
    }
}

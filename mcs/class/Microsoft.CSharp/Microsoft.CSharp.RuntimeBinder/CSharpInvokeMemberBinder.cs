//
// CSharpInvokeMemberBinder.cs
//
// Authors:
//	Marek Safar  <marek.safar@gmail.com>
//
// Copyright (C) 2009 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.CSharp.RuntimeBinder
{
	public class CSharpInvokeMemberBinder : InvokeMemberBinder
	{
		CSharpCallFlags flags;
		IList<CSharpArgumentInfo> argumentInfo;
		IList<Type> typeArguments;
		Type callingContext;
		
		public CSharpInvokeMemberBinder (CSharpCallFlags flags, string name, Type callingContext, IEnumerable<Type> typeArguments, IEnumerable<CSharpArgumentInfo> argumentInfo)
			: base (name, false, CreateCallInfo (argumentInfo))
		{
			this.flags = flags;
			this.callingContext = callingContext;
			this.argumentInfo = new ReadOnlyCollectionBuilder<CSharpArgumentInfo> (argumentInfo);
			this.typeArguments = new ReadOnlyCollectionBuilder<Type> (typeArguments);
		}
		
		static CallInfo CreateCallInfo (IEnumerable<CSharpArgumentInfo> argumentInfo)
		{
			var named = from arg in argumentInfo where arg.IsNamed select arg.Name;
			return new CallInfo (argumentInfo.Count (), named);
		}
		
		public IList<CSharpArgumentInfo> ArgumentInfo {
			get {
				return argumentInfo;
			}
		}

		public Type CallingContext {
			get {
				return callingContext;
			}
		}
		
		public override bool Equals (object obj)
		{
			var other = obj as CSharpInvokeMemberBinder;
			return other != null && base.Equals (obj) && other.flags == flags && other.callingContext == callingContext && 
				other.argumentInfo.SequenceEqual (argumentInfo) && other.typeArguments.SequenceEqual (typeArguments);
		}

		public CSharpCallFlags Flags {
			get {
				return flags;
			}
		}
		
		public override int GetHashCode ()
		{
			return base.GetHashCode ();
		}
		
		[MonoTODO]
		public override DynamicMetaObject FallbackInvoke (DynamicMetaObject target, DynamicMetaObject[] args, DynamicMetaObject errorSuggestion)
		{
			throw new NotImplementedException ();
		}
		
		[MonoTODO]
		public override DynamicMetaObject FallbackInvokeMember (DynamicMetaObject target, DynamicMetaObject[] args, DynamicMetaObject errorSuggestion)
		{
			throw new NotImplementedException ();			
		}
		
		public IList<Type> TypeArguments {
			get {
				return typeArguments;
			}
		}
	}
}

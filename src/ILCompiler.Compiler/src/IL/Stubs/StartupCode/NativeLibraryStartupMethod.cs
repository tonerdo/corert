// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

using Internal.TypeSystem;

namespace Internal.IL.Stubs.StartupCode
{
    /// <summary>
    /// Startup code that does initialization, Main invocation
    /// and shutdown of the runtime.
    /// </summary>
    public sealed partial class NativeLibraryStartupMethod : ILStubMethod
    {
        private TypeDesc _owningType;
        private MethodSignature _signature;
        private IList<MethodDesc> _libraryInitializers;

        public NativeLibraryStartupMethod(TypeDesc owningType, IList<MethodDesc> libraryInitializers)
        {
            _owningType = owningType;
            _libraryInitializers = libraryInitializers;
        }

        public override TypeSystemContext Context
        {
            get
            {
                return _owningType.Context;
            }
        }

        public override TypeDesc OwningType
        {
            get
            {
                return _owningType;
            }
        }

        public override string Name
        {
            get
            {
                return "NativeLibraryStartup";
            }
        }

        public override MethodIL EmitIL()
        {
            ILEmitter emitter = new ILEmitter();
            ILCodeStream codeStream = emitter.NewCodeStream();

            // Allow the class library to run explicitly ordered class constructors first thing in start-up.
            if (_libraryInitializers != null)
            {
                foreach (MethodDesc method in _libraryInitializers)
                {
                    codeStream.Emit(ILOpcode.call, emitter.NewToken(method));
                }
            }

            codeStream.Emit(ILOpcode.ret);
            return emitter.Link(this);
        }

        public override MethodSignature Signature
        {
            get
            {
                if (_signature == null)
                {
                    _signature = new MethodSignature(MethodSignatureFlags.Static, 0,
                            Context.GetWellKnownType(WellKnownType.Void),
                            new TypeDesc[0]);
                }

                return _signature;
            }
        }

        public override bool IsNativeCallable
        {
            get
            {
                return true;
            }
        }
    }
}

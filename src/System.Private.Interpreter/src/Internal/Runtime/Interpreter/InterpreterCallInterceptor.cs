using Internal.IL;
using Internal.Runtime.CallConverter;
using Internal.Runtime.CallInterceptor;
using Internal.TypeSystem;
using Internal.TypeSystem.Ecma;

namespace Internal.Runtime.Interpreter
{
    public class InterpreterCallInterceptor : CallInterceptor.CallInterceptor
    {
        private MethodDesc _method;
        private MethodIL _methodIL;

        public InterpreterCallInterceptor(MethodDesc method) : base(false)
        {
            _method = method;
            _methodIL = EcmaMethodIL.Create(method as EcmaMethod);
        }

        public override LocalVariableType[] ArgumentAndReturnTypes
        {
            get
            {
                LocalVariableType[] localVariableTypes = new LocalVariableType[_method.Signature.Length + 1];
                localVariableTypes[0] = new LocalVariableType(_method.Signature.ReturnType.RuntimeTypeHandle, false, _method.Signature.ReturnType.IsByRef);
                for (int i = 0; i < _method.Signature.Length; i++)
                {
                    var argument = _method.Signature[i];
                    localVariableTypes[i + 1] = new LocalVariableType(argument.RuntimeTypeHandle, false, argument.IsByRef);
                }

                return localVariableTypes;
            }
        }

        public override CallingConvention CallingConvention
        {
            get
            {
                return _method.Signature.IsStatic ? CallingConvention.ManagedStatic : CallingConvention.ManagedInstance;
            }
        }

        public override LocalVariableType[] LocalVariableTypes
        {
            get
            {
                LocalVariableDefinition[] locals = _methodIL.GetLocals();
                LocalVariableType[] localVariableTypes = new LocalVariableType[locals.Length];
                for (int i = 0; i < locals.Length; i++)
                {
                    var variable = locals[i];
                    localVariableTypes[i] = new LocalVariableType(variable.Type.RuntimeTypeHandle, variable.IsPinned, variable.Type.IsByRef);
                }

                return localVariableTypes;
            }
        }

        public override void ThunkExecute(ref CallInterceptorArgs callInterceptorArgs)
        {
            throw new System.NotImplementedException();
        }
    }
}

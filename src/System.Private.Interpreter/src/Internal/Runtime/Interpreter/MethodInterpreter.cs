using Internal.Runtime.CallConverter;
using Internal.Runtime.CallInterceptor;

namespace Internal.Runtime.Interpreter
{
    public class MethodInterpreter : CallInterceptor.CallInterceptor
    {
        protected MethodInterpreter(bool nativeToManaged) : base(nativeToManaged) { }

        public override LocalVariableType[] ArgumentAndReturnTypes => throw new System.NotImplementedException();

        public override CallingConvention CallingConvention => throw new System.NotImplementedException();

        public override LocalVariableType[] LocalVariableTypes => throw new System.NotImplementedException();

        public override void ThunkExecute(ref CallInterceptorArgs callInterceptorArgs)
        {
            throw new System.NotImplementedException();
        }
    }
}

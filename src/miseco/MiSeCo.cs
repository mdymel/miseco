using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace MiSeCo
{
    public class MiSeCo
    {
        private readonly string _myName;
        private readonly ModuleBuilder _moduleBuilder;

        public MiSeCo()
        {
            Type myType = GetType();
            _myName = myType.Name;

            string guid = Guid.NewGuid().ToString();
            var assemblyName = new AssemblyName(string.Concat(myType.Namespace, ".", myType.Name, "_", guid));

            AssemblyBuilder ab = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
            _moduleBuilder = ab.DefineDynamicModule(assemblyName.Name, string.Concat(assemblyName.Name, ".dll"));
        }

        public T CreateServiceObject<T>() where T:IContractInterface
        {
            Type type = typeof (T);

            string typeName = string.Concat(_myName, "+", type.FullName);

            TypeBuilder typeBuilder = _moduleBuilder.DefineType(typeName, TypeAttributes.Public);
            Type parent = typeof(DynamicProxy);

            typeBuilder.SetParent(parent);
            typeBuilder.AddInterfaceImplementation(type);

            var baseConstructor = parent.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).First();
            var constructor = typeBuilder.DefineConstructor(MethodAttributes.Public, baseConstructor.CallingConvention, null);

            ILGenerator ilGenerator = constructor.GetILGenerator();
            ilGenerator.Emit(OpCodes.Nop);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Call, baseConstructor);
            ilGenerator.Emit(OpCodes.Ret);

            Type newType = typeBuilder.CreateType();

            return (T) Activator.CreateInstance(newType);
        }
    }
}
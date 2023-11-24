using RestTemplate.attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RestTemplate.proxy
{
   public class RestInterfaceFactory
   {
        /// <summary>
        /// 创建动态类型
        /// </summary>
        /// <param name="_type"></param>
        /// <returns></returns>
        public static Type createType(Type _type)
        {
            Type result = null;
            if (!_type.IsInterface)
            {
                throw new Exception("不能创建非接口类型的实现类");
            }
            // 创建程序集
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("CustomerDynamic"), AssemblyBuilderAccess.Run);
            // 创建模块
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("DynamicModule");

            if (_type.GetCustomAttribute(typeof(FeginAttribute)) == null)
            {
                throw new Exception("没有标识RestTemplateAttribute");
            }
            // 创建类型
            TypeBuilder typeBuilder = moduleBuilder.DefineType(_type.Name+"_Proxy", TypeAttributes.Public,null,new Type[] { _type });

            var customAttributeBuilder = new CustomAttributeBuilder(typeof(FeginAttribute).GetConstructor(new Type[]{ typeof(string) }), new object[] { ((FeginAttribute)_type.GetCustomAttribute(typeof(FeginAttribute))).requestUrl });

            typeBuilder.SetCustomAttribute(customAttributeBuilder);
            MethodInfo[] methods = _type.GetMethods();

            foreach(MethodInfo info in methods)
            {
               Type t = info.ReturnType;
                ParameterInfo[] inParams = info.GetParameters();
                // 创建方法
                MethodBuilder methodBuilder = typeBuilder.DefineMethod(info.Name, MethodAttributes.Public|MethodAttributes.Virtual , t, 
                    inParams.AsEnumerable().Select(x => { return x.ParameterType; }).ToArray());

                // 获取 ILGenerator
                ILGenerator ilGenerator = methodBuilder.GetILGenerator();
                var returnVar = ilGenerator.DeclareLocal(t);
                ilGenerator.Emit(OpCodes.Ldstr, "Hello, dynamic method!");
                ilGenerator.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
                ilGenerator.Emit(OpCodes.Ldloc, returnVar);
                ilGenerator.Emit(OpCodes.Ret); // 返回
            }
            // 构建动态类型
            result = typeBuilder.CreateType();
           return result;

        }
    }
}

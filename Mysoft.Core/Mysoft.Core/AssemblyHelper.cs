using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Mysoft.Core
{
    public class AssemblyHelper
    {
        private static SafeDictionary<string, Assembly> _assemblyCache = new SafeDictionary<string, Assembly>();

        public static SafeDictionary<string, Assembly> AssemblyDictionary { get { return _assemblyCache; } }
        public static List<Assembly> All { get { return _assemblyCache.Values.ToList(); } }
        static AssemblyHelper()
        {
           var asms =  AppDomain.CurrentDomain.GetAssemblies();
           foreach (var asm in asms) {
               _assemblyCache.Add(asm.GetName().Name, asm);           
           }
        
        }

        public static Assembly GetOrLoad(string asmName)
        {
            return _assemblyCache.GetOrAdd(asmName, Load);
        }

        /// <summary>
        /// 加载bin目录下的程序集
        /// </summary>
        /// <param name="asmName"></param>
        /// <returns></returns>
        public static Assembly Load(string asmName)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "bin\\";
            var asmFile = path + asmName + ".dll";
            return Assembly.LoadFrom(asmFile);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using BepInEx;
using Modding.Humankind.DevTools.Core;

namespace Modding.Humankind.DevTools.DeveloperTools
{
    public static class AssemblyHelper
    {

        private static AssemblyName ParseName(string fullName)
        {
            try
            {
                return new AssemblyName(fullName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void PrintAppdomainAssemblies()
        {
            // In some cases there could be multiple versions of the same assembly loaded
            // In that case we decide to simply load only the latest one as it's easiest to handle
            var dedupedAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Select(a => new {ass = a, name = ParseName(a.FullName)})
                .Where(a => a.name != null)
                .GroupBy(a => a.name.Name)
                .Select(g => g.OrderByDescending(a => a.name.Version).First());
            
            foreach (var ass in dedupedAssemblies)
            {
                
                Loggr.Log("\tASSEMBLY:%DarkYellow% " + ass.name.Name + " %White%VERSION: " + ass.name.Version + " FULL_NAME: " + ass.name.FullName, ConsoleColor.DarkMagenta);
            }
        }
    }
}
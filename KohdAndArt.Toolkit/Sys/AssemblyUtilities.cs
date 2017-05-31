using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KohdAndArt.Toolkit.Extensions;

namespace KohdAndArt.Toolkit.Sys
{
    public class AssemblyUtilities
    {
        public List<ClassDetailsStruct> ClassDetailList = new List<ClassDetailsStruct>();
        public List<ReferencedAssembly> ReferencedAssemblies = null;

        private Assembly _assembly;
        public Assembly RootAssembly
        {
            get
            {
                return _assembly;
            }
            set
            {
                _assembly = value;
            }
        }

        #region Constructors
        public AssemblyUtilities(Assembly a)
        {
            this.RootAssembly = a;
            GetAssemblyItems();
        }

        public AssemblyUtilities(string filename)
        {
            this.RootAssembly = Assembly.Load(filename);
            GetAssemblyItems();
        }
        #endregion

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = _assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(_assembly.CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return _assembly.GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = _assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = _assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = _assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = _assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        private void GetAssemblyItems()
        {
            try
            {
                // Assembly Contents (Classes, etc.)
                foreach (Type type in RootAssembly.GetTypes())
                {
                    
                    ClassDetailsStruct details = new ClassDetailsStruct();

                    if (type.IsClass)
                    {
                        details.Name = type.Name;
                        MethodInfo[] methodInfoArray = type.GetMethods(BindingFlags.Public |
                                                                       BindingFlags.NonPublic |
                                                                       BindingFlags.Instance |
                                                                       BindingFlags.Static |
                                                                       BindingFlags.DeclaredOnly);

                        for (int i = 0; i < methodInfoArray.Length; i++)
                        {
                            MethodOrPropertyDetails methodOrPropDetails = new MethodOrPropertyDetails();
                            MethodInfo mi = (MethodInfo)methodInfoArray[i];
                            methodOrPropDetails.Name = mi.Name;
                            methodOrPropDetails.IsProperty = IsProperty(mi);
                            methodOrPropDetails.IsMethod = !IsProperty(mi);
                            methodOrPropDetails.ProtectionLevel = GetProtectionlevelAsString(mi);
                            methodOrPropDetails.IsStatic = mi.IsStatic;
                            methodOrPropDetails.ReturnType = mi.ReturnType.ToString();

                            ParameterInfo[] parameters = mi.GetParameters();
                            foreach (var p in parameters)
                            {
                                ParameterDetails pd = new ParameterDetails();
                                pd.Name = p.Name;
                                pd.IsOut = p.IsOut;
                                pd.IsIn = p.IsIn;
                                pd.IsOptional = p.IsOptional;
                                pd.IsRetVal = p.IsRetval;
                                pd.Type = p.ParameterType.Name;

                                methodOrPropDetails.Parameters.Add(pd);
                            }

                            details.MethodOrPropertyDetailsList.Add(methodOrPropDetails);
                        }

                        ClassDetailList.Add(details);
                    }
                }

                // Now, get any referenced assemblies
                this.ReferencedAssemblies = RootAssembly.GetReferencedAssemblies().Select(a =>
                new ReferencedAssembly
                {
                    Name = a.Name,
                    Version = a.Version.ToString()
                }).ToList();
            }
            catch (ReflectionTypeLoadException ex)
            {
                //sb.AppendLine($"<tr><td colspan='2' style='color: #f00;'>{ex.Message}</tr>");
            }
        }

        private bool IsProperty(MethodInfo mi)
        {
            bool status = false;

            var name = mi.Name;
            if (name.Length >= 4)
            {
                if (mi.Name.Substring(0, 4) == "get_" ||
                    mi.Name.Substring(0, 4) == "set_")
                {
                    status = true;
                }
            }

            return status;
        }

        private string GetProtectionlevelAsString(MethodInfo mi)
        {
            var level = string.Empty;
            if (mi.IsPublic)
                level = "public";
            else if (mi.IsPrivate)
                level = "private";

            return level;
        }

        public struct ParameterDetails
        {
            public string Type;
            public string Name;
            public bool IsOut;
            public bool IsIn;
            public bool IsOptional;
            public bool IsRetVal;
        }

        public class MethodOrPropertyDetails
        {
            public MethodOrPropertyDetails()
            {
                Parameters = new List<ParameterDetails>();
            }

            public string   Name;
            public bool     IsMethod;
            public bool     IsProperty;
            public string   ReturnType;
            public bool     IsStatic;
            public bool     IsInstance;
            public string   ProtectionLevel;
            public List<ParameterDetails> Parameters;

            public string GetParameterList()
            {
                string result = string.Empty;
                foreach (var p in Parameters) 
                {
                    result += $"{p.Type} {p.Name}, ";
                }

                if (result.Length > 0)
                {
                    // Remove trailing comma
                    result = result.RemoveLast(2);
                }
                return result;
            }
        }

        public class ClassDetailsStruct
        {
            public ClassDetailsStruct()
            {
                MethodOrPropertyDetailsList = new List<MethodOrPropertyDetails>();
            }

            public string Name;
            public List<MethodOrPropertyDetails> MethodOrPropertyDetailsList;
        }

        public class ReferencedAssembly
        {
            public ReferencedAssembly()
            {
            }

            public string Name;
            public string Version;
        }
    }
}

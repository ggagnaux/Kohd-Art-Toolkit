#region Copyright (c) 2017 G. Gagnaux, https://github.com/ggagnaux/Kohd-Art-Toolkit
/*
Kohd & Art Toolkit - A toolkit of general classes/methods for .NET and C#

Copyright (c) 2017 G. Gagnaux, https://github.com/ggagnaux/Kohd-Art-Toolkit

Permission is hereby granted, free of charge, to any person obtaining a copy of 
this software and associated documentation files (the "Software"), to deal in the 
Software without restriction, including without limitation the rights to use, copy, 
modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
and to permit persons to whom the Software is furnished to do so, subject to the 
following conditions:

The above copyright notice and this permission notice shall be included in 
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE 
OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KohdAndArt.Toolkit.Extensions;

namespace KohdAndArt.Toolkit.Sys
{
    public class AssemblyUtilities
    {
        public List<ClassDetail> ClassDetailList = new List<ClassDetail>();
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


        private string AssemblyVersion2
        {
            get
            {
                return _assembly.GetName().Version.ToString();
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
                    
                    ClassDetail classDetail = new ClassDetail();

                    if (type.IsClass)
                    {
                        classDetail.Name = type.Name;

                        //
                        // Methods
                        //
                        MethodInfo[] methodInfoArray = type.GetMethods(BindingFlags.Public |
                                                                       BindingFlags.NonPublic |
                                                                       BindingFlags.Instance |
                                                                       BindingFlags.Static |
                                                                       BindingFlags.DeclaredOnly);

                        for (int i = 0; i < methodInfoArray.Length; i++)
                        {
                            MethodDetail methodDetail = new MethodDetail();
                            MethodInfo mi = (MethodInfo)methodInfoArray[i];
                            methodDetail.Name = mi.Name;
                            methodDetail.IsPublic = mi.IsPublic;
                            methodDetail.IsPrivate = mi.IsPrivate;
                            methodDetail.ProtectionLevel = GetProtectionlevelAsString(mi);
                            methodDetail.IsStatic = mi.IsStatic;
                            methodDetail.IsVirtual = mi.IsVirtual;
                            methodDetail.ReturnType = mi.ReturnType.ToString();

                            ParameterInfo[] parameters = mi.GetParameters();
                            foreach (var p in parameters)
                            {
                                ParameterDetail pd = new ParameterDetail();
                                pd.Name = p.Name;
                                pd.IsOut = p.IsOut;
                                pd.IsIn = p.IsIn;
                                pd.IsOptional = p.IsOptional;
                                pd.IsRetVal = p.IsRetval;
                                pd.Type = p.ParameterType.Name;

                                methodDetail.Parameters.Add(pd);
                            }

                            classDetail.MethodDetails.Add(methodDetail);
                        }

                        //
                        // Properties
                        //
                        PropertyInfo[] propertyInfoArray = type.GetProperties(BindingFlags.Public |
                                                                              BindingFlags.NonPublic |
                                                                              BindingFlags.Instance |
                                                                              BindingFlags.Static |
                                                                              BindingFlags.DeclaredOnly);

                        for (int i = 0; i < propertyInfoArray.Length; i++)
                        {
                            PropertyDetail propertyDetail = new PropertyDetail();
                            PropertyInfo pi = (PropertyInfo)propertyInfoArray[i];
                            PropertyAttributes pa = pi.Attributes;
                            propertyDetail.Name = pi.Name;
                            classDetail.PropertyDetails.Add(propertyDetail);
                        }


                        //
                        // Constructors
                        //
                        ConstructorInfo[] constructorInfoArray = type.GetConstructors(BindingFlags.Public |
                                                                              BindingFlags.NonPublic |
                                                                              BindingFlags.Instance |
                                                                              BindingFlags.Static |
                                                                              BindingFlags.DeclaredOnly);

                        for (int i = 0; i < constructorInfoArray.Length; i++)
                        {
                            string name = constructorInfoArray[i].Name;
                            classDetail.Constructors.Add(name);
                        }


                        //
                        // Fields
                        //
                        FieldInfo[] fieldInfoArray = type.GetFields(BindingFlags.Public |
                                                                    BindingFlags.NonPublic |
                                                                    BindingFlags.Instance |
                                                                    BindingFlags.Static |
                                                                    BindingFlags.DeclaredOnly);

                        for (int i = 0; i < fieldInfoArray.Length; i++)
                        {
                            string name = fieldInfoArray[i].Name;
                            classDetail.Fields.Add(name);
                        }

                        //
                        // Nested Types
                        //
                        Type[] nestedTypeArray = type.GetNestedTypes(BindingFlags.Public |
                                                                    BindingFlags.NonPublic |
                                                                    BindingFlags.Instance |
                                                                    BindingFlags.Static |
                                                                    BindingFlags.DeclaredOnly);

                        for (int i = 0; i < nestedTypeArray.Length; i++)
                        {
                            string name = nestedTypeArray[i].Name;
                            classDetail.NestedTypes.Add(name);
                        }


                        //
                        // Events
                        //
                        EventInfo[] eventInfoArray = type.GetEvents(BindingFlags.Public |
                                                                    BindingFlags.NonPublic |
                                                                    BindingFlags.Instance |
                                                                    BindingFlags.Static |
                                                                    BindingFlags.DeclaredOnly);

                        for (int i = 0; i < eventInfoArray.Length; i++)
                        {
                            string name = eventInfoArray[i].Name;
                            classDetail.Events.Add(name);
                        }


                        ClassDetailList.Add(classDetail);
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
            var level = "protected";
            if (mi.IsPublic)
                level = "public";
            else if (mi.IsPrivate)
                level = "private";

            return level;
        }

        public struct ParameterDetail
        {
            public string Type;
            public string Name;
            public bool IsOut;
            public bool IsIn;
            public bool IsOptional;
            public bool IsRetVal;
        }

        public class MethodDetail
        {
            public MethodDetail()
            {
                Parameters = new List<ParameterDetail>();
            }

            public string Name;
            public string ReturnType;
            public bool IsStatic;
            public bool IsInstance;
            public string ProtectionLevel;
            public bool IsPublic;
            public bool IsPrivate;
            public bool IsVirtual;
            public List<ParameterDetail> Parameters;

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

        public class PropertyDetail
        {
            public PropertyDetail()
            {
            }

            public string Name;
            public string ReturnType;
            public bool IsStatic;
            public bool IsInstance;
            public string ProtectionLevel;
        }

        public class ClassDetail
        {
            public ClassDetail()
            {
                NestedTypes = new List<string>();
                Fields = new List<string>();
                Constructors = new List<string>();
                Events = new List<string>();
                MethodDetails = new List<MethodDetail>();
                PropertyDetails = new List<PropertyDetail>();
            }

            public string Name;
            public bool IsAbstract;
            public bool IsAutoLayout;
            public bool IsClass;
            public bool IsNested;
            public bool IsNotPublic;
            public bool IsPrimitive;
            public bool IsPublic;
            public bool IsSerializable;
            public bool IsSubclassOf;
            public bool IsValueType;
            public bool IsVisible;
            public string Module;
            public string Namespace;
            public List<string> NestedTypes;
            public List<string> Fields;
            public List<string> Constructors;
            public List<string> Events;
            public List<MethodDetail> MethodDetails;
            public List<PropertyDetail> PropertyDetails;
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

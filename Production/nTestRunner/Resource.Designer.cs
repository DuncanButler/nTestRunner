﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace nTestRunner {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("nTestRunner.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Will build solution using.
        /// </summary>
        internal static string AvailableBuilders {
            get {
                return ResourceManager.GetString("AvailableBuilders", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Will display results using .
        /// </summary>
        internal static string AvailableDisplays {
            get {
                return ResourceManager.GetString("AvailableDisplays", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Will run tests using .
        /// </summary>
        internal static string AvailableTestRunners {
            get {
                return ResourceManager.GetString("AvailableTestRunners", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Running build using.
        /// </summary>
        internal static string Building {
            get {
                return ResourceManager.GetString("Building", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Displaying results using.
        /// </summary>
        internal static string DisplayRunner {
            get {
                return ResourceManager.GetString("DisplayRunner", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (?&lt;=\&quot;)[^\&quot;]+\.[0-9a-z]+proj(?=\&quot;,\s*\&quot;{[0-9a-z]{8}-[0-9a-z]{4}-[0-9a-z]{4}-[0-9a-z]{4}-[0-9a-z]{12}}\&quot;\s*EndProject).
        /// </summary>
        internal static string ExtractProjectRegEx {
            get {
                return ResourceManager.GetString("ExtractProjectRegEx", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Running tests using .
        /// </summary>
        internal static string TestRunner {
            get {
                return ResourceManager.GetString("TestRunner", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///******************************************
        ///****                                  ****
        ///****   nTestRunner version {0}.{1}        ****
        ///****                                  ****
        ///******************************************.
        /// </summary>
        internal static string TitleVersion {
            get {
                return ResourceManager.GetString("TitleVersion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Waiting.
        /// </summary>
        internal static string WatcherIsOn {
            get {
                return ResourceManager.GetString("WatcherIsOn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Found files to watch:.
        /// </summary>
        internal static string WatchingFiles {
            get {
                return ResourceManager.GetString("WatchingFiles", resourceCulture);
            }
        }
    }
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CaducaRest.Resources {
    using System;
    
    
    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile su proyecto de VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class mensajes {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal mensajes() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CaducaRest.Resources.mensajes", typeof(mensajes).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Reemplaza la propiedad CurrentUICulture del subproceso actual para todas las
        ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
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
        ///   Busca una cadena traducida similar a {0} que deseas borrar ya no existe u otro usuario la borró, por favor vuelve a consultar la información..
        /// </summary>
        internal static string NotFound {
            get {
                return ResourceManager.GetString("NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El campo {0} solo admite valores de {1} a {2}. Por favor teclea un número dentro de este rango..
        /// </summary>
        internal static string Range {
            get {
                return ResourceManager.GetString("Range", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Ya existe {0} con este {1}. Por favor teclea {1} diferente..
        /// </summary>
        internal static string Repeteaded {
            get {
                return ResourceManager.GetString("Repeteaded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Por favor registra el campo {0}..
        /// </summary>
        internal static string Required {
            get {
                return ResourceManager.GetString("Required", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El campo {0} debe tener como máximo {1} caracteres. Por favor abrevia el campo..
        /// </summary>
        internal static string StringLength {
            get {
                return ResourceManager.GetString("StringLength", resourceCulture);
            }
        }
    }
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GeekyGift.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Texts {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Texts() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("GeekyGift.Resources.Texts", typeof(Texts).Assembly);
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
        ///   Looks up a localized string similar to Инструкция, как воспользоваться средствами.
        ///
        ///Подготовка:
        ///Создать где удобно Bitcoin кошелек. Он будет нужен для вывода на него разблокированных средств.
        ///На текущий момент вполне рекомендую мобильный TrustWallet.
        ///
        ///1. Получаем приватный ключ.
        ///Заходим на https://iancoleman.io/bip39/
        ///В поле BIP39 Mnemonic вводим 12 слов Андрея, следом 12 слов Люды.
        ///Derivation Path выбираем BIP84.
        ///Внизу, с таблицы Derived Addresses копируем Private Key из первой строки таблицы
        ///(первая запись, её Path = m/84&apos;/0&apos;/0&apos;/0/0 [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string InstructionsText {
            get {
                return ResourceManager.GetString("InstructionsText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Инструкция к подарку
        ///
        ///Вы получили по конверту с секретными словами.
        ///У Андрея первые 12 слов, у Люды вторые 12 слов.
        ///24 слова вместе представляют собой только что созданный биткоин ключ.
        ///
        ///Этот ключ позволяет вывести средства с кошелька, на который вам были отправлены 
        ///$300 в крипто эквиваленте по сегодняшнему курсу.
        ///
        ///Использовать вы их сможете только вместе и не раньше чем через 3 года (03.07.2024)
        ///
        ///Детальная инструкция будет в ваших конвертах.
        ///Храните их в безопасном месте и никому не показывайт [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string MainPageText {
            get {
                return ResourceManager.GetString("MainPageText", resourceCulture);
            }
        }
    }
}

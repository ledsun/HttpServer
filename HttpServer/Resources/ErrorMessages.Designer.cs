﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:2.0.50727.3634
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace HttpServer.Resources {
    using System;
    
    
    /// <summary>
    ///   ローカライズされた文字列などを検索するための、厳密に型指定されたリソース クラスです。
    /// </summary>
    // このクラスは StronglyTypedResourceBuilder クラスが ResGen
    // または Visual Studio のようなツールを使用して自動生成されました。
    // メンバを追加または削除するには、.ResX ファイルを編集して、/str オプションと共に
    // ResGen を実行し直すか、または VS プロジェクトをビルドし直します。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessages() {
        }
        
        /// <summary>
        ///   このクラスで使用されているキャッシュされた ResourceManager インスタンスを返します。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("HttpServer.Resources.ErrorMessages", typeof(ErrorMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   厳密に型指定されたこのリソース クラスを使用して、すべての検索リソースに対し、
        ///   現在のスレッドの CurrentUICulture プロパティをオーバーライドします。
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
        ///   フォルダが存在しません。：{0} に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string EF001 {
            get {
                return ResourceManager.GetString("EF001", resourceCulture);
            }
        }
        
        /// <summary>
        ///   ファイルが存在しません。：{0} に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string EF002 {
            get {
                return ResourceManager.GetString("EF002", resourceCulture);
            }
        }
        
        /// <summary>
        ///   HTTP Listenerの例外が発生しました。：{0} に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string EL001 {
            get {
                return ResourceManager.GetString("EL001", resourceCulture);
            }
        }
        
        /// <summary>
        ///   使用可能なメモリが不足しています。ページを読み込めませんでした。 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string EM001 {
            get {
                return ResourceManager.GetString("EM001", resourceCulture);
            }
        }
        
        /// <summary>
        ///   許可されていないリクエストです。ページを表示できません。 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string EP401 {
            get {
                return ResourceManager.GetString("EP401", resourceCulture);
            }
        }
        
        /// <summary>
        ///   アクセス権限がないため、ページを表示できません。 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string EP403 {
            get {
                return ResourceManager.GetString("EP403", resourceCulture);
            }
        }
        
        /// <summary>
        ///   ページが存在しません。 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string EP404 {
            get {
                return ResourceManager.GetString("EP404", resourceCulture);
            }
        }
        
        /// <summary>
        ///   サーバー内で問題が発生しました。管理者に問い合わせてください。 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string EP500 {
            get {
                return ResourceManager.GetString("EP500", resourceCulture);
            }
        }
    }
}
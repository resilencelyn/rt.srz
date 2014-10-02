﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace rt.srz.model.barcode.Properties {
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
    public class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("rt.srz.model.barcode.Properties.Resource", typeof(Resource).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
        ///&lt;xsl:stylesheet version=&quot;1.0&quot; xmlns:xsl=&quot;http://www.w3.org/1999/XSL/Transform&quot;
        ///    xmlns:msxsl=&quot;urn:schemas-microsoft-com:xslt&quot; exclude-result-prefixes=&quot;msxsl&quot;
        ///    xmlns:user=&quot;urn:user&quot;&gt;
        ///  &lt;msxsl:script language=&quot;C#&quot; implements-prefix=&quot;user&quot;&gt;
        ///    &lt;![CDATA[
        ///    public string PadPolicyNumber(string number)
        ///    {
        ///      return number.PadLeft(16, &apos;0&apos;);
        ///    }
        ///    public string SplitFLP(string value, int index)
        ///    {
        ///      string[] parts = value.Split(new char[] { [rest of string was truncated]&quot;;.
        /// </summary>
        public static string BarcodeDenormilizer {
            get {
                return ResourceManager.GetString("BarcodeDenormilizer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
        ///&lt;xsl:stylesheet version=&quot;1.0&quot; xmlns:xsl=&quot;http://www.w3.org/1999/XSL/Transform&quot;
        ///    xmlns:msxsl=&quot;urn:schemas-microsoft-com:xslt&quot; exclude-result-prefixes=&quot;msxsl&quot;
        ///    xmlns:user=&quot;urn:user&quot;&gt;
        ///  &lt;msxsl:script language=&quot;C#&quot; implements-prefix=&quot;user&quot;&gt;
        ///    &lt;![CDATA[
        ///    public string Concat(string FirstName, string LastName, string Patronymic, byte barcode_type){
        ///      return string.Format(&quot;{0}|{1}|{2}&quot;, FirstName.TrimEnd(), LastName.TrimEnd(), Patronymic.TrimEnd()).PadRi [rest of string was truncated]&quot;;.
        /// </summary>
        public static string BarcodeNormilizer {
            get {
                return ResourceManager.GetString("BarcodeNormilizer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
        ///&lt;xs:schema attributeFormDefault=&quot;unqualified&quot; elementFormDefault=&quot;qualified&quot; xmlns:xs=&quot;http://www.w3.org/2001/XMLSchema&quot;&gt;
        ///  &lt;xs:simpleType name=&quot;barcodeString24&quot;&gt;
        ///    &lt;xs:restriction base=&quot;xs:string&quot;&gt;
        ///      &lt;xs:maxLength value=&quot;24&quot;/&gt;
        ///      &lt;xs:minLength value=&quot;0&quot;/&gt;
        ///    &lt;/xs:restriction&gt;
        ///  &lt;/xs:simpleType&gt;
        ///  &lt;xs:simpleType name=&quot;barcodeString20&quot;&gt;
        ///    &lt;xs:restriction base=&quot;xs:string&quot;&gt;
        ///      &lt;xs:maxLength value=&quot;20&quot;/&gt;
        ///      &lt;xs:minLength value=&quot;0&quot;/&gt;
        ///    &lt;/xs:r [rest of string was truncated]&quot;;.
        /// </summary>
        public static string BarcodeSchema {
            get {
                return ResourceManager.GetString("BarcodeSchema", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (0[1-9]|[12][0-9]|3[01])\.(0[1-9]|1[012])\.(19|20)\d\d.
        /// </summary>
        public static string Date {
            get {
                return ResourceManager.GetString("Date", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 99.
        /// </summary>
        public static string FaultBirthdateLargerDateFillingExceptionCode {
            get {
                return ResourceManager.GetString("FaultBirthdateLargerDateFillingExceptionCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Дата рождения больше даты заявления.
        /// </summary>
        public static string FaultBirthdateLargerDateFillingExceptionMessage {
            get {
                return ResourceManager.GetString("FaultBirthdateLargerDateFillingExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 99.
        /// </summary>
        public static string FaultDateFutureExceptionCode {
            get {
                return ResourceManager.GetString("FaultDateFutureExceptionCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} больше чем текущая дата.
        /// </summary>
        public static string FaultDateFutureExceptionMessage {
            get {
                return ResourceManager.GetString("FaultDateFutureExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 99.
        /// </summary>
        public static string FaultDateIssueDocumentUdlCode {
            get {
                return ResourceManager.GetString("FaultDateIssueDocumentUdlCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не корректно введена дата выдачи документа.
        /// </summary>
        public static string FaultDateIssueDocumentUdlMessage {
            get {
                return ResourceManager.GetString("FaultDateIssueDocumentUdlMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 109.
        /// </summary>
        public static string FaultDateRegistrationCode {
            get {
                return ResourceManager.GetString("FaultDateRegistrationCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не заполнена дата регистрации.
        /// </summary>
        public static string FaultDateRegistrationMessage {
            get {
                return ResourceManager.GetString("FaultDateRegistrationMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 99.
        /// </summary>
        public static string FaultDocumentDateExpEmptyExceptionCode {
            get {
                return ResourceManager.GetString("FaultDocumentDateExpEmptyExceptionCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не заполнен срок окончания действия документа.
        /// </summary>
        public static string FaultDocumentDateExpEmptyExceptionMessage {
            get {
                return ResourceManager.GetString("FaultDocumentDateExpEmptyExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 99.
        /// </summary>
        public static string FaultDocumentDateIssueFutureExceptionCode {
            get {
                return ResourceManager.GetString("FaultDocumentDateIssueFutureExceptionCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Дата выдачи документа больше текущей даты.
        /// </summary>
        public static string FaultDocumentDateIssueFutureExceptionMessage {
            get {
                return ResourceManager.GetString("FaultDocumentDateIssueFutureExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 99.
        /// </summary>
        public static string FaultEnpAbsentPrevPolicyExceptionCode {
            get {
                return ResourceManager.GetString("FaultEnpAbsentPrevPolicyExceptionCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Если новый полис выдавать не требуется, то ЕНП должен быть введен.
        /// </summary>
        public static string FaultEnpAbsentPrevPolicyExceptionMessage {
            get {
                return ResourceManager.GetString("FaultEnpAbsentPrevPolicyExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 99.
        /// </summary>
        public static string FaultGenderConformityMiddleNameExceptionCode {
            get {
                return ResourceManager.GetString("FaultGenderConformityMiddleNameExceptionCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Введённое Отчество отсутствует в справочнике Отчеств..
        /// </summary>
        public static string FaultGenderConformityMiddleNameExceptionMessage {
            get {
                return ResourceManager.GetString("FaultGenderConformityMiddleNameExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to rt109.
        /// </summary>
        public static string FaultPoliceCertificateDateNotNeyPolisExceptionCode {
            get {
                return ResourceManager.GetString("FaultPoliceCertificateDateNotNeyPolisExceptionCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Дата выдачи полиса должна быть меньше даты подачи заявления.
        /// </summary>
        public static string FaultPoliceCertificateDateNotNeyPolisExceptionMessage {
            get {
                return ResourceManager.GetString("FaultPoliceCertificateDateNotNeyPolisExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Дата выдачи полиса больше текущей даты..
        /// </summary>
        public static string FaultPoliceCertificateFutureExceptionMessage {
            get {
                return ResourceManager.GetString("FaultPoliceCertificateFutureExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
        ///&lt;xs:schema attributeFormDefault=&quot;unqualified&quot; elementFormDefault=&quot;qualified&quot; xmlns:xs=&quot;http://www.w3.org/2001/XMLSchema&quot;&gt;
        ///  &lt;xs:simpleType name=&quot;barcodeString56&quot;&gt;
        ///    &lt;xs:restriction base=&quot;xs:string&quot;&gt;
        ///      &lt;xs:maxLength value=&quot;56&quot;/&gt;
        ///      &lt;xs:minLength value=&quot;3&quot;/&gt;
        ///    &lt;/xs:restriction&gt;
        ///  &lt;/xs:simpleType&gt;
        ///  &lt;xs:simpleType name=&quot;barcodeString68&quot;&gt;
        ///    &lt;xs:restriction base=&quot;xs:string&quot;&gt;
        ///      &lt;xs:maxLength value=&quot;68&quot;/&gt;
        ///      &lt;xs:minLength value=&quot;3&quot;/&gt;
        ///    &lt;/xs:r [rest of string was truncated]&quot;;.
        /// </summary>
        public static string NormalizedBarcode {
            get {
                return ResourceManager.GetString("NormalizedBarcode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
        ///&lt;xs:schema attributeFormDefault=&quot;unqualified&quot; elementFormDefault=&quot;qualified&quot; xmlns:xs=&quot;http://www.w3.org/2001/XMLSchema&quot;&gt;
        ///  &lt;xs:simpleType name=&quot;barcodeString56&quot;&gt;
        ///    &lt;xs:restriction base=&quot;xs:string&quot;&gt;
        ///      &lt;xs:maxLength value=&quot;56&quot;/&gt;
        ///      &lt;xs:minLength value=&quot;3&quot;/&gt;
        ///    &lt;/xs:restriction&gt;
        ///  &lt;/xs:simpleType&gt;
        ///  &lt;xs:simpleType name=&quot;barcodePolicy&quot;&gt;
        ///    &lt;xs:restriction base=&quot;xs:unsignedLong&quot;&gt;
        ///      &lt;xs:totalDigits value=&quot;20&quot;/&gt;
        ///    &lt;/xs:restriction&gt;
        ///  &lt;/xs:simpleT [rest of string was truncated]&quot;;.
        /// </summary>
        public static string NormalizedBarcode1 {
            get {
                return ResourceManager.GetString("NormalizedBarcode1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
        ///        &lt;xs:schema attributeFormDefault=&quot;unqualified&quot; elementFormDefault=&quot;qualified&quot; xmlns:xs=&quot;http://www.w3.org/2001/XMLSchema&quot;&gt;
        ///        &lt;xs:simpleType name=&quot;barcodeString24&quot;&gt;
        ///        &lt;xs:restriction base=&quot;xs:string&quot;&gt;
        ///        &lt;xs:maxLength value=&quot;24&quot;/&gt;
        ///        &lt;xs:minLength value=&quot;0&quot;/&gt;
        ///        &lt;/xs:restriction&gt;
        ///        &lt;/xs:simpleType&gt;
        ///        &lt;xs:simpleType name=&quot;barcodeString16&quot;&gt;
        ///        &lt;xs:restriction base=&quot;xs:string&quot;&gt;
        ///        &lt;xs:maxLength value=&quot;16&quot;/&gt;
        /// </summary>
        public static string PolicyXSD1 {
            get {
                return ResourceManager.GetString("PolicyXSD1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
        ///&lt;xs:schema attributeFormDefault=&quot;unqualified&quot; elementFormDefault=&quot;qualified&quot; xmlns:xs=&quot;http://www.w3.org/2001/XMLSchema&quot;&gt;
        ///&lt;xs:simpleType name=&quot;barcodeString24&quot;&gt;
        ///&lt;xs:restriction base=&quot;xs:string&quot;&gt;
        ///&lt;xs:maxLength value=&quot;24&quot;/&gt;
        ///&lt;xs:minLength value=&quot;0&quot;/&gt;
        ///&lt;/xs:restriction&gt;
        ///&lt;/xs:simpleType&gt;
        ///&lt;xs:simpleType name=&quot;barcodeString16&quot;&gt;
        ///&lt;xs:restriction base=&quot;xs:string&quot;&gt;
        ///&lt;xs:maxLength value=&quot;16&quot;/&gt;
        ///&lt;xs:minLength value=&quot;0&quot;/&gt;
        ///&lt;/xs:restriction&gt;
        ///&lt;/xs:simpleType&gt;
        ///&lt;xs:simpleType  [rest of string was truncated]&quot;;.
        /// </summary>
        public static string PolicyXSD2 {
            get {
                return ResourceManager.GetString("PolicyXSD2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^(?i)[\d,‚.:№  _\-–—/\\|¦\(\[\{\)\]\}&apos;`‘“&quot;’”&lt;‹«&gt;›»„а-яёa-z-]+$.
        /// </summary>
        public static string RegexBirthplace {
            get {
                return ResourceManager.GetString("RegexBirthplace", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DocNum=&quot;[09]+&quot;.
        /// </summary>
        public static string RegexDocomentNumberBlok {
            get {
                return ResourceManager.GetString("RegexDocomentNumberBlok", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DocSer=&quot;[RБS9 -]+&quot;.
        /// </summary>
        public static string RegexDocomentSeriaBlok {
            get {
                return ResourceManager.GetString("RegexDocomentSeriaBlok", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$.
        /// </summary>
        public static string RegexEmail {
            get {
                return ResourceManager.GetString("RegexEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^(?i)[\d.\t_\-–—  &apos;`‘“&quot;’”&lt;‹«&gt;›»„а-яёa-z-]+$.
        /// </summary>
        public static string RegexFio {
            get {
                return ResourceManager.GetString("RegexFio", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^(?i)[\d,‚.:№  _\-–—/\\|¦\(\[\{\)\]\}&apos;`‘“&quot;’”&lt;‹«&gt;›»„а-яёa-z-]+$.
        /// </summary>
        public static string RegexIssuingAuthority {
            get {
                return ResourceManager.GetString("RegexIssuingAuthority", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^\d+$.
        /// </summary>
        public static string RegexOnlyNumber {
            get {
                return ResourceManager.GetString("RegexOnlyNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [-.&apos;] *[-.&apos;].
        /// </summary>
        public static string RegexReplaceDoubleSymb {
            get {
                return ResourceManager.GetString("RegexReplaceDoubleSymb", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to \s\s.
        /// </summary>
        public static string RegexReplaceSpaces {
            get {
                return ResourceManager.GetString("RegexReplaceSpaces", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 108.
        /// </summary>
        public static string SetParameterSearchExceptionCode {
            get {
                return ResourceManager.GetString("SetParameterSearchExceptionCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не задан ни один параметр поиска.
        /// </summary>
        public static string SetParameterSearchExceptionMessage {
            get {
                return ResourceManager.GetString("SetParameterSearchExceptionMessage", resourceCulture);
            }
        }
    }
}
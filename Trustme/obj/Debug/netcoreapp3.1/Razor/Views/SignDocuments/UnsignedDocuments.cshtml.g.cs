#pragma checksum "C:\Users\Marina Rusu\Desktop\Trustme\Trustme\Trustme\Views\SignDocuments\UnsignedDocuments.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b8eb4d3c70708baeee26a2bc970eee413c0457d1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_SignDocuments_UnsignedDocuments), @"mvc.1.0.view", @"/Views/SignDocuments/UnsignedDocuments.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Marina Rusu\Desktop\Trustme\Trustme\Trustme\Views\_ViewImports.cshtml"
using Trustme;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Marina Rusu\Desktop\Trustme\Trustme\Trustme\Views\_ViewImports.cshtml"
using Trustme.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Marina Rusu\Desktop\Trustme\Trustme\Trustme\Views\_ViewImports.cshtml"
using Trustme.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b8eb4d3c70708baeee26a2bc970eee413c0457d1", @"/Views/SignDocuments/UnsignedDocuments.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"72b90e9542d3d8e5acfb65a75d80cf9e64289297", @"/Views/_ViewImports.cshtml")]
    public class Views_SignDocuments_UnsignedDocuments : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<UnsignedDocument>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Marina Rusu\Desktop\Trustme\Trustme\Trustme\Views\SignDocuments\UnsignedDocuments.cshtml"
   foreach (var udoc in Model)
   {


#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"card\">\r\n        <div class=\"card-header\">\r\n           ");
#nullable restore
#line 8 "C:\Users\Marina Rusu\Desktop\Trustme\Trustme\Trustme\Views\SignDocuments\UnsignedDocuments.cshtml"
      Write(udoc.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
        </div>
        <div class=""card-body"">
            <h5 class=""card-title"">Special title treatment</h5>
            <p class=""card-text"">With supporting text below as a natural lead-in to additional content.</p>
            <a href=""#"" class=""btn btn-primary"">Go somewhere</a>
        </div>
    </div>
");
#nullable restore
#line 16 "C:\Users\Marina Rusu\Desktop\Trustme\Trustme\Trustme\Views\SignDocuments\UnsignedDocuments.cshtml"
    
   }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<UnsignedDocument>> Html { get; private set; }
    }
}
#pragma warning restore 1591

#pragma checksum "C:\Users\Marina Rusu\Desktop\Trustme\Trustme\Trustme\Views\Administration\Secret.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "25a25905ab385d4200d62a71b408b79304ac3f01"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Administration_Secret), @"mvc.1.0.view", @"/Views/Administration/Secret.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"25a25905ab385d4200d62a71b408b79304ac3f01", @"/Views/Administration/Secret.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6b450d8e4c3bdf78a3af6b230fe2b1a4ad1228e7", @"/Views/_ViewImports.cshtml")]
    public class Views_Administration_Secret : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\Marina Rusu\Desktop\Trustme\Trustme\Trustme\Views\Administration\Secret.cshtml"
  
    IEnumerable<Key> Keys = ViewData["keys"] as IEnumerable<Key>;
    User CurrentUser = ViewData["user"] as User;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container emp-profile\">\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "25a25905ab385d4200d62a71b408b79304ac3f013886", async() => {
                WriteLiteral("\r\n        <div class=\"row\">\r\n            <div class=\"col-md-4\">\r\n                <div class=\"profile-img\">\r\n                    <img src=\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS52y5aInsxSm31CvHOFHWujqUx_wWTS9iM6s7BAm21oEN_RiGoog\"");
                BeginWriteAttribute("alt", " alt=\"", 432, "\"", 438, 0);
                EndWriteAttribute();
                WriteLiteral(@" />
                    <div class=""file btn btn-lg btn-primary"">
                        Change Photo
                        <input type=""file"" name=""file"" />
                    </div>
                </div>
            </div>
            <div class=""col-md-6"">
                <div class=""profile-head"">
                    <h5>
                        Kshiti Ghelani
                    </h5>
                    <h6>
                        Web Developer and Designer
                    </h6>
                    <p class=""proile-rating"">RANKINGS : <span>8/10</span></p>
                    <ul class=""nav nav-tabs"" id=""myTab"" role=""tablist"">
                        <li class=""nav-item"">
                            <a class=""nav-link active"" id=""home-tab"" data-toggle=""tab"" href=""#home"" role=""tab"" aria-controls=""home"" aria-selected=""true"">About</a>
                        </li>
                        <li class=""nav-item"">
                            <a class=""nav-link"" id=""profile-tab"" dat");
                WriteLiteral(@"a-toggle=""tab"" href=""#profile"" role=""tab"" aria-controls=""profile"" aria-selected=""false"">Timeline</a>
                        </li>
                    </ul>
                </div>
            </div>
            <div class=""col-md-2"">
                <input type=""submit"" class=""profile-edit-btn"" name=""btnAddMore"" value=""Edit Profile"" />
            </div>
        </div>
        <div class=""row"">
            <div class=""col-md-4"">
                <div class=""profile-work"">
                    <p>WORK LINK</p>
                    <a");
                BeginWriteAttribute("href", " href=\"", 2011, "\"", 2018, 0);
                EndWriteAttribute();
                WriteLiteral(">Website Link</a><br />\r\n                    <a");
                BeginWriteAttribute("href", " href=\"", 2066, "\"", 2073, 0);
                EndWriteAttribute();
                WriteLiteral(">Bootsnipp Profile</a><br />\r\n                    <a");
                BeginWriteAttribute("href", " href=\"", 2126, "\"", 2133, 0);
                EndWriteAttribute();
                WriteLiteral(">Bootply Profile</a>\r\n                    <p>SKILLS</p>\r\n                    <a");
                BeginWriteAttribute("href", " href=\"", 2213, "\"", 2220, 0);
                EndWriteAttribute();
                WriteLiteral(">Web Designer</a><br />\r\n                    <a");
                BeginWriteAttribute("href", " href=\"", 2268, "\"", 2275, 0);
                EndWriteAttribute();
                WriteLiteral(">Web Developer</a><br />\r\n                    <a");
                BeginWriteAttribute("href", " href=\"", 2324, "\"", 2331, 0);
                EndWriteAttribute();
                WriteLiteral(">WordPress</a><br />\r\n                    <a");
                BeginWriteAttribute("href", " href=\"", 2376, "\"", 2383, 0);
                EndWriteAttribute();
                WriteLiteral(">WooCommerce</a><br />\r\n                    <a");
                BeginWriteAttribute("href", " href=\"", 2430, "\"", 2437, 0);
                EndWriteAttribute();
                WriteLiteral(@">PHP, .Net</a><br />
                </div>
            </div>
            <div class=""col-md-8"">
                <div class=""tab-content profile-tab"" id=""myTabContent"">
                    <div class=""tab-pane fade show active"" id=""home"" role=""tabpanel"" aria-labelledby=""home-tab"">
                        <div class=""row"">
                            <div class=""col-md-6"">
                                <label>User Id</label>
                            </div>
                            <div class=""col-md-6"">
                                <p>Kshiti123</p>
                            </div>
                        </div>
                        <div class=""row"">
                            <div class=""col-md-6"">
                                <label>Name</label>
                            </div>
                            <div class=""col-md-6"">
                                <p>Kshiti Ghelani</p>
                            </div>
                        </div>
                   ");
                WriteLiteral(@"     <div class=""row"">
                            <div class=""col-md-6"">
                                <label>Email</label>
                            </div>
                            <div class=""col-md-6"">
                                <p>kshitighelani@gmail.com</p>
                            </div>
                        </div>
                        <div class=""row"">
                            <div class=""col-md-6"">
                                <label>Phone</label>
                            </div>
                            <div class=""col-md-6"">
                                <p>123 456 7890</p>
                            </div>
                        </div>
                        <div class=""row"">
                            <div class=""col-md-6"">
                                <label>Profession</label>
                            </div>
                            <div class=""col-md-6"">
                                <p>Web Developer and Designer</p>
       ");
                WriteLiteral(@"                     </div>
                        </div>
                    </div>
                    <div class=""tab-pane fade"" id=""profile"" role=""tabpanel"" aria-labelledby=""profile-tab"">
                        <div class=""row"">
                            <div class=""col-md-6"">
                                <label>Experience</label>
                            </div>
                            <div class=""col-md-6"">
                                <p>Expert</p>
                            </div>
                        </div>
                        <div class=""row"">
                            <div class=""col-md-6"">
                                <label>Hourly Rate</label>
                            </div>
                            <div class=""col-md-6"">
                                <p>10$/hr</p>
                            </div>
                        </div>
                        <div class=""row"">
                            <div class=""col-md-6"">
                 ");
                WriteLiteral(@"               <label>Total Projects</label>
                            </div>
                            <div class=""col-md-6"">
                                <p>230</p>
                            </div>
                        </div>
                        <div class=""row"">
                            <div class=""col-md-6"">
                                <label>English Level</label>
                            </div>
                            <div class=""col-md-6"">
                                <p>Expert</p>
                            </div>
                        </div>
                        <div class=""row"">
                            <div class=""col-md-6"">
                                <label>Availability</label>
                            </div>
                            <div class=""col-md-6"">
                                <p>6 months</p>
                            </div>
                        </div>
                        <div class=""row"">
              ");
                WriteLiteral(@"              <div class=""col-md-12"">
                                <label>Your Bio</label><br />
                                <p>Your detail description</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591

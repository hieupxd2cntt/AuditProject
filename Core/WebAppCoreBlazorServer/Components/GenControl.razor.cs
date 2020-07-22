using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.TextEditor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using WebAppCoreBlazorServer.BUS;
using WebCore.Entities;
using WebModelCore;
using WebModelCore.CodeInfo;

namespace WebAppCoreBlazorServer.Components
{
    public partial class GenControl
    {
        [Parameter]
        public ModuleFieldInfo field { get; set; }
        [Parameter]
        public bool IsEdit { get; set; }
        [Parameter] 
        public string keyEdit { get; set; }
        [Parameter]
        public List<ModuleFieldInfo> fields { get; set; }
        [Parameter]
        public List<ModuleFieldInfo> FieldParent { get; set; }
        [Parameter]
        public List<CodeInfoModel> dataComboBoxs { get; set; }
        [Parameter]
        public List<LanguageInfo> languageInfos { get; set; }
        [Parameter]
        public ModuleInfoModel ModuleInfo { get; set; }
        [Parameter]
        public string subMod { get; set; }
        [Parameter]
        public List<dynamic> dataEdit { get; set; }
        [Parameter]
        public string modName { get; set; } = "";
        [Parameter]
        public int pageSlide { get; set; } = 0;
        [Parameter]
        public List<ModuleFieldInfo> fieldSubmited { get; set; } = null;
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }
        [JSInvokable()]
        public Task invokeFromJS()
        {

            //field.Value = DateTime.Now.ToString("dd/MM/yyyy");
            //field.Value = "New value";
            //StateHasChanged();
            //return CompletedTask;
            return null;
        }
        public async Task ControlOnchange(ChangeEventArgs e)
        {
            field.Value = (e.Value ?? "").ToString();
            HomeBus homeBus = new HomeBus(moduleService, iConfiguration, distributedCache);
            var codeInfoParrams = new List<CodeInfoParram>();
            if (!string.IsNullOrEmpty(field.Callback))
            {
                var codeInfoParram = new CodeInfoParram
                {
                    Name = field.FieldName,
                    CtrlType = field.ControlType,
                    ListSource = field.Callback,
                    Parrams = (e.Value ?? "").ToString()
                };
                codeInfoParrams.Add(codeInfoParram);
                var loadCallBacks = await homeBus.LoadDataListSourceControl(codeInfoParrams);
                foreach (var item in loadCallBacks)
                {
                    if (!String.IsNullOrEmpty(item.DataCallBack))
                    {
                        var dataCallBackControls = JsonConvert.DeserializeObject<List<dynamic>>(item.DataCallBack);
                        foreach (var controlCallBack in dataCallBackControls)
                        {
                            var name = ((Newtonsoft.Json.Linq.JProperty)((Newtonsoft.Json.Linq.JContainer)controlCallBack).First).Name;
                            var value = ((Newtonsoft.Json.Linq.JProperty)((Newtonsoft.Json.Linq.JContainer)controlCallBack).First).Value;
                            var arr = new string[2];
                            arr[0] = name;
                            arr[1] = value == null ? "" : value.ToString();
                            await JSRuntime.InvokeVoidAsync("SetValueControl", arr);
                        }
                    }
                }
            }
        }

        #region Control Editor
        BlazoredTextEditor QuillHtml;
        BlazoredTextEditor QuillNative;
        BlazoredTextEditor QuillReadOnly;

        string QuillHTMLContent;
        string QuillContent;
        string QuillReadOnlyContent =@"<span><b>Read Only</b> <u>Content</u></span>";

        bool mode = false;

        public async void GetHTML()
        {
            QuillHTMLContent = await this.QuillHtml.GetHTML();
            StateHasChanged();
        }

        public async void SetHTML()
        {
            string QuillContent =@"<a href='http://BlazorHelpWebsite.com/'>" +
                "<img src='images/BlazorHelpWebsite.gif' /></a>";

            await this.QuillHtml.LoadHTMLContent(QuillContent);
            StateHasChanged();
        }

        public async void GetContent()
        {
            QuillContent = await this.QuillNative.GetContent();
            StateHasChanged();
        }
        public async void LoadContent()
        {
            await this.QuillNative.LoadContent(QuillContent);
            StateHasChanged();
        }

        public async void InsertImage()
        {
            await this.QuillNative.InsertImage("images/BlazorHelpWebsite.gif");
            StateHasChanged();
        }

        async Task ToggleQuillEditor()
        {
            mode = (mode) ? false : true;
            await this.QuillReadOnly.EnableEditor(mode);
        }
        #endregion
        #region Event CBC
        IEnumerable<string> multipleValues;
        
        void CBC_Change(object value, string name)
        {
            var str = value is IEnumerable<object> ? string.Join(",", (IEnumerable<object>)value) : value;
            field.Value = (str??"").ToString();
            StateHasChanged();
        }
        #endregion
    }
}

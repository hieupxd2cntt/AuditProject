using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AppClient.Utils;
using Core.Common;
using Core.Entities;
using Core.Entities.Extensions;
using Core.Utils;
using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid;
using DevExpress.XtraLayout;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Menu;
using DevExpress.XtraSpellChecker;

namespace AppClient.Controls
{
    public partial class ucModule
    {

        //Add by TrungTT - 22.12.2011 - Add popup Menu for RichEditControl
        static List<char> wordSeparators = CreateWordSeparators();
        SpellChecker spellChecker1;
        RichEditControl rtControl;
        //OptionsSpelling optionsSpelling1 = new OptionsSpelling();
        static List<char> CreateWordSeparators()
        {
            List<char> result = new List<char>();
            result.Add(' ');
            result.Add('\t');
            result.Add('\n');
            result.Add('\r');
            result.Add(',');
            result.Add('.');
            result.Add('-');
            result.Add('(');
            result.Add(')');
            result.Add('{');
            result.Add('}');
            result.Add('[');
            result.Add(']');
            result.Add('"');
            result.Add('\'');
            result.Add('<');
            result.Add('>');
            result.Add(':');
            result.Add(';');
            result.Add('\\');
            result.Add('/');
            return result;
        }
        //End TrungTT
        protected virtual void LoadCommandFields()
        {
            CommonFields =
                FieldUtils.GetModuleFields(
                    ModuleInfo.ModuleType,
                    Core.CODES.DEFMODFLD.FLDGROUP.COMMON
                );

            CommonFields.AddRange(
                FieldUtils.GetModuleFields(
                    ModuleInfo.ModuleID,
                    Core.CODES.DEFMODFLD.FLDGROUP.COMMON
                ));
        }
        protected virtual void BuildCommonFields(LayoutControl commonLayout)
        {
            LoadCommandFields();
            CommonControlByID = new Dictionary<string, BaseEdit>();
            _CommonControlByID = new Dictionary<string, Control>();
            CommonLayoutItemByID = new Dictionary<string, BaseLayoutItem>();

            foreach (var field in CommonFields)
            {
                if (field.ControlType == Core.CODES.DEFMODFLD.CTRLTYPE.DEFINEDGROUP)
                    continue;

                var control = CreateControl(field);
                control.Name = field.FieldName;
                if (control is BaseEdit)
                {
                    if (!CommonControlByID.ContainsKey(field.FieldID))
                    {
                        CommonControlByID.Add(field.FieldID, (BaseEdit)control);
                        if (!string.IsNullOrEmpty(field.Callback))
                        {
                            (control as BaseEdit).Properties.EditValueChangedFiringMode = EditValueChangedFiringMode.Buffered;
                            (control as BaseEdit).Properties.EditValueChanged += MaintainControl_Callback;
                            control.Leave += MaintainControl_Callback;

                        }
                    }
                    else
                    {
                        throw ErrorUtils.CreateError(ERR_SYSTEM.ERR_SYSTEM_MODULE_FIELD_NOT_FOUND_OR_DUPLICATE,
                            "GetModuleFieldByID", field.ModuleID, field.FieldGroup, field.FieldID);
                    }
                }
                else
                {
                    if (!_CommonControlByID.ContainsKey(field.FieldID))
                    {
                        _CommonControlByID.Add(field.FieldID, control);
                        SetupControlListSourceControl(field, _CommonControlByID[field.FieldID]);
                    }
                    else
                    {
                        throw ErrorUtils.CreateError(ERR_SYSTEM.ERR_SYSTEM_MODULE_FIELD_NOT_FOUND_OR_DUPLICATE,
                            "GetModuleFieldByID", field.ModuleID, field.FieldGroup, field.FieldID);
                    }
                }

                commonLayout.Controls.Add(control);
            }

            foreach (var field in CommonFields)
            {
                if (field.ControlType == Core.CODES.DEFMODFLD.CTRLTYPE.DEFINEDGROUP)
                    continue;

                if (CommonControlByID.ContainsKey(field.FieldID))
                {
                    SetupControlListSource(field, CommonControlByID[field.FieldID]);
                }
            }
        }

        //private void view_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        //{
        //    MessageBox.Show("shit");
        //}
        //ORG: BaseEdit --> Control
        public virtual Control CreateControl(ModuleFieldInfo fieldInfo)
        {
            var baseEdit = _CreateControl(fieldInfo);
#if DEBUG
            if (baseEdit is ButtonEdit && fieldInfo.FieldGroup == Core.CODES.DEFMODFLD.FLDGROUP.COMMON)
            {
                var buttonEdit = baseEdit as ButtonEdit;
                var button = new EditorButton(ButtonPredefines.Up) { Tag = "DEBUG_EDIT" };
                buttonEdit.Properties.Buttons.Add(button);

                var context = new ContextMenuStrip();
                context.Items.Add("Edit Field", ThemeUtils.Image16.Images["EDIT"]).Click +=
                    delegate {
                        var ucModule = MainProcess.CreateModuleInstance("02904", "MED");
                        ucModule["P01"] = fieldInfo.ModuleID;
                        ucModule["C01"] = fieldInfo.FieldID;
                        ucModule.ShowDialogModule(this);
                    };

                if (!string.IsNullOrEmpty(fieldInfo.ListSource))
                {
                    var match = Regex.Match(fieldInfo.ListSource, "^:([^.]+).([^.]+)$");
                    context.Items.Add("Translate Code", ThemeUtils.Image16.Images["CODE"]).Click +=
                        delegate {
                            var ucModule = (ucEditLanguage)MainProcess.CreateModuleInstance(STATICMODULE.EDITLANG, "MMN");
                            ucModule.mainView.ActiveFilterString = string.Format("LANGNAME LIKE 'DEFCODE${0}${1}.%'", match.Groups[1].Value, match.Groups[2].Value);
                            ucModule.Execute();
                            ucModule.ShowDialogModule(this);
                        };
                }

                context.Items.Add("List Common Field", ThemeUtils.Image16.Images["COMMON"]).Click +=
                    delegate {
                        var ucModule = MainProcess.CreateModuleInstance("03905", "MMN");
                        ucModule["C01"] = fieldInfo.ModuleID;
                        ucModule.ShowDialogModule(this);
                        ucModule.Execute();
                    };

                buttonEdit.ButtonClick +=
                    delegate (object sender, ButtonPressedEventArgs e) {
                        if (e.Button == button)
                            context.Show(new Point(MousePosition.X, MousePosition.Y));
                    };
            }
#endif
            if (baseEdit is BaseEdit)
            {
                if (fieldInfo.ControlType == Core.CODES.DEFMODFLD.CTRLTYPE.LABEL)
                {
                    SetReadOnly((BaseEdit)baseEdit, true);
                }
                else
                {
                    if (ModuleInfo.SubModule == Core.CODES.DEFMOD.SUBMOD.MAINTAIN_ADD &&
                        fieldInfo.ReadOnlyOnAdd == Core.CODES.DEFMODFLD.READONLYMODE.READONLY)
                    {
                        SetReadOnly((BaseEdit)baseEdit, true);
                    }

                    if (ModuleInfo.SubModule == Core.CODES.DEFMOD.SUBMOD.MAINTAIN_EDIT &&
                        fieldInfo.ReadOnlyOnEdit == Core.CODES.DEFMODFLD.READONLYMODE.READONLY)
                    {
                        SetReadOnly((BaseEdit)baseEdit, true);
                    }

                    if ((ModuleInfo.SubModule == Core.CODES.DEFMOD.SUBMOD.MAINTAIN_VIEW ||
                        ModuleInfo.SubModule == Core.CODES.DEFMOD.SUBMOD.MODULE_MAIN) &&
                        fieldInfo.ReadOnlyOnView == Core.CODES.DEFMODFLD.READONLYMODE.READONLY)
                    {
                        SetReadOnly((BaseEdit)baseEdit, true);
                    }
                }

                if (fieldInfo.TabStop == Core.CODES.DEFMODFLD.TABSTOP.NO)
                {
                    (baseEdit as BaseEdit).Properties.AllowFocused = false;
                    baseEdit.TabStop = false;
                }

                if (fieldInfo.PopupMode == Core.CODES.DEFMODFLD.POPUPMODE.ONFOCUS)
                {
                    var popupRepo = (baseEdit as BaseEdit).Properties as RepositoryItemPopupBaseAutoSearchEdit;
                    if (popupRepo != null) popupRepo.ImmediatePopup = true;
                }

                ApplyFormatInfo(fieldInfo, (BaseEdit)baseEdit);
            }

            return baseEdit;
        }

        public virtual void SetupControlListSource(ModuleFieldInfo fieldInfo, BaseEdit edit)
        {
            switch (fieldInfo.ControlType)
            {
                case Core.CODES.DEFMODFLD.CTRLTYPE.COMBOBOX:
                    SetControlListSource(edit);
                    break;
                case Core.CODES.DEFMODFLD.CTRLTYPE.COMBOBOXCHECK:
                    SetControlListSource(edit);
                    break;
                case Core.CODES.DEFMODFLD.CTRLTYPE.SUGGESTIONTEXTBOX:
                    SetControlListSource(edit);
                    break;
                case Core.CODES.DEFMODFLD.CTRLTYPE.CHECKEDCOMBOBOX:
                    SetControlListSource((PopupContainerEdit)edit, (FlowLayoutPanel)edit.Properties.Tag);
                    break;
                //TuDQ them
                case Core.CODES.DEFMODFLD.CTRLTYPE.RADIOGROUP:
                    SetControlListSource(edit);
                    break;
                case Core.CODES.DEFMODFLD.CTRLTYPE.CHECKBOX:
                    SetControlListSource(edit);
                    break;
                case Core.CODES.DEFMODFLD.CTRLTYPE.LOOKUPEDIT:
                    SetControlListSource(edit);
                    break;
                    //End
            }
        }
        public virtual void SetupControlListSourceControl(ModuleFieldInfo fieldInfo, Control edit)
        {
            SetControlListSource(edit);
        }
        //ORG: BaseEdit --> Control
        public virtual Control _CreateControl(ModuleFieldInfo fieldInfo)
        {
            switch (fieldInfo.ControlType)
            {
                case Core.CODES.DEFMODFLD.CTRLTYPE.TEXTBOX:
                    return CreateTextBoxControl(fieldInfo);
                case Core.CODES.DEFMODFLD.CTRLTYPE.LABEL:
                    return CreateLabelControl(fieldInfo);
                case Core.CODES.DEFMODFLD.CTRLTYPE.FILESAVE:
                    return CreateFileSaveControl(fieldInfo);
                case Core.CODES.DEFMODFLD.CTRLTYPE.SPINEDITOR:
                    return CreateSpinEditControl(fieldInfo);
                case Core.CODES.DEFMODFLD.CTRLTYPE.COMBOBOX:
                    var comboBoxEdit = CreateComboBoxControl(fieldInfo);
                    if (fieldInfo.Nullable == Core.CODES.DEFMODFLD.NULLABLE.YES)
                    {
                        var editButton = new EditorButton(ButtonPredefines.Ellipsis);
                        comboBoxEdit.Properties.Buttons.Add(editButton);

                        comboBoxEdit.ButtonClick += delegate (object sender, ButtonPressedEventArgs e) {
                            if (e.Button == editButton)
                            {
                                comboBoxEdit.EditValue = null;
                                comboBoxEdit.ClosePopup();
                            }
                        };
                    }
                    return comboBoxEdit;
                case Core.CODES.DEFMODFLD.CTRLTYPE.COMBOBOXCHECK:
                    var comboBoxCheckEdit = CreateCheckComboBoxControl(fieldInfo);
                    if (fieldInfo.Nullable == Core.CODES.DEFMODFLD.NULLABLE.YES)
                    {
                        var editButton = new EditorButton(ButtonPredefines.Ellipsis);
                        comboBoxCheckEdit.Properties.Buttons.Add(editButton);

                        comboBoxCheckEdit.ButtonClick += delegate (object sender, ButtonPressedEventArgs e) {
                            if (e.Button == editButton)
                            {
                                comboBoxCheckEdit.EditValue = null;
                                comboBoxCheckEdit.ClosePopup();
                            }
                        };
                    }
                    return comboBoxCheckEdit;
                case Core.CODES.DEFMODFLD.CTRLTYPE.PASSWORD:
                    return CreatePasswordControl(fieldInfo);
                case Core.CODES.DEFMODFLD.CTRLTYPE.TEXTAREA:
                    return CreateTextAreaControl(fieldInfo);
                case Core.CODES.DEFMODFLD.CTRLTYPE.SUGGESTIONTEXTBOX:
                    var suggestionTextEdit = CreateSuggestionTextBoxControl(fieldInfo);
                    return suggestionTextEdit;
                case Core.CODES.DEFMODFLD.CTRLTYPE.DATETIME:
                    var dateEdit = CreateDateEdit(fieldInfo);
                    if (string.IsNullOrEmpty(fieldInfo.FieldFormat))
                    {
                        fieldInfo.FieldFormat = CONSTANTS.DEFAULT_DATETIME_FORMAT;
                    }
                    return dateEdit;
                case Core.CODES.DEFMODFLD.CTRLTYPE.CHECKEDCOMBOBOX:
                    var ccbControl = CreateCheckedComboBoxControl(fieldInfo);
                    return ccbControl;
                case Core.CODES.DEFMODFLD.CTRLTYPE.LOOKUPVALUES:
                    var editLookUp = CreateLookUpEditControl(fieldInfo);
                    return editLookUp;
                case Core.CODES.DEFMODFLD.CTRLTYPE.LOOKUPEDIT:
                    var lookupEdit = CreateLookUpEditControlBase(fieldInfo);
                    return lookupEdit;
                case Core.CODES.DEFMODFLD.CTRLTYPE.UPLOADFILE:
                    return CreateOpenFileControl(fieldInfo);
                case Core.CODES.DEFMODFLD.CTRLTYPE.RICHTEXTEDITOR:
                    return CreateRichTextEdit(fieldInfo);
                case Core.CODES.DEFMODFLD.CTRLTYPE.GRIDVIEW:
                    return CreateGridViewControl(fieldInfo);
                case Core.CODES.DEFMODFLD.CTRLTYPE.GRIDVIEWEXT:
                    return CreateGridViewExtControl(fieldInfo);

                case Core.CODES.DEFMODFLD.CTRLTYPE.RADIOGROUP:
                    var radioGroupEdit = CreateRadioGroupControl(fieldInfo);
                    return radioGroupEdit;
                case Core.CODES.DEFMODFLD.CTRLTYPE.CHECKBOX:
                    var checkBoxEdit = CreateCheckBoxControl(fieldInfo);
                    return checkBoxEdit;
                case Core.CODES.DEFMODFLD.CTRLTYPE.PICTUREBOX:
                    var pictureBoxEdit = CreatePictureEditBoxControl(fieldInfo);
                    return pictureBoxEdit;
                case Core.CODES.DEFMODFLD.CTRLTYPE.CALCEDITBOX:
                    var calcBoxEdit = CreateCalcEditBoxControl(fieldInfo);
                    return calcBoxEdit;
                case Core.CODES.DEFMODFLD.CTRLTYPE.RATING:
                    var ratingControl = CreateRatingControl(fieldInfo);
                    return ratingControl;
                case Core.CODES.DEFMODFLD.CTRLTYPE.TIMEEDIT:
                    var timeEdit = CreateTimeEditControl(fieldInfo);
                    return timeEdit;
                case Core.CODES.DEFMODFLD.CTRLTYPE.TOGGLESWITCH:
                    var toggleSwitch = CreateToggleSwitchControl(fieldInfo);
                    return toggleSwitch;
            }

            throw ErrorUtils.CreateError(ERR_SYSTEM.ERR_SYSTEM_CONTROL_TYPE_NOT_FOUND);
        }

        public BaseEdit CreateLookUpEditControl(ModuleFieldInfo fieldInfo)
        {
            //
            var expression = ExpressionUtils.ParseScript(fieldInfo.ListSource);

            if (expression.Operands.Count != 1 && expression.Operands.Count != 2) ErrorUtils.CreateError(ERR_SYSTEM.ERR_SYSTEM_LOOKUP_EXPRESSION_REQUIRE_ONE_OR_TWO_ARGUMENTS);

            foreach (var operand in expression.Operands)
            {
                if (operand.Type != OperandType.VALUE)
                {
                    ErrorUtils.CreateError(ERR_SYSTEM.ERR_SYSTEM_LOOKUP_EXPRESSION_CAN_NOT_CONTAIN_NAME);
                }
            }
            //
            var moduleLookUp = (SearchModuleInfo)((ICloneable)ModuleUtils.GetModuleInfo(expression.StoreProcName, Core.CODES.DEFMOD.SUBMOD.MODULE_MAIN)).Clone();
            if (expression.Operands.Count == 1)
                moduleLookUp.SetAsLookUpWindow(expression.Operands[0].NameOrValue);
            else
                moduleLookUp.SetAsLookUpWindow(expression.Operands[0].NameOrValue, expression.Operands[1].NameOrValue);
            //
            var btnEdit = new ButtonEdit();
            btnEdit.Properties.Buttons[0].Tag = moduleLookUp;
            btnEdit.Properties.Buttons[0].Shortcut = new DevExpress.Utils.KeyShortcut(Keys.F5);
            btnEdit.Properties.Buttons.Add(new EditorButton(ButtonPredefines.Delete));

            btnEdit.ButtonClick +=
                delegate (object sender, ButtonPressedEventArgs e) {
                    if (e.Button.Kind == ButtonPredefines.Delete)
                    {
                        btnEdit.EditValue = string.Empty;
                    }
                    else if (e.Button.Tag is ModuleInfo)
                    {
                        var module = (ucSearchMaster)MainProcess.CreateModuleInstance((ModuleInfo)e.Button.Tag);
                        module.ShowDialogModule(this);

                        if (!string.IsNullOrEmpty(module.LookUpValues))
                        {
                            if (btnEdit.EditValue is string && (string)btnEdit.EditValue != string.Empty)
                                btnEdit.EditValue = string.Format("{0},{1}", btnEdit.EditValue, module.LookUpValues);
                            else
                                btnEdit.EditValue = module.LookUpValues;
                        }
                    }
                };
            return btnEdit;
        }

        protected virtual DateEdit CreateDateEdit(ModuleFieldInfo fieldInfo)
        {
            var dateEdit = new DateEdit
            {
                Name = string.Format(CONSTANTS.DATETIME_NAME_FORMAT, fieldInfo.FieldName),
                Tag = fieldInfo,
                EnterMoveNextControl = true,
                EditValue = null
            };

            return dateEdit;
        }

        protected virtual TextEdit CreateFileSaveControl(ModuleFieldInfo fieldInfo)
        {
            var txtButtonEdit = new ButtonEdit
            {
                Name = string.Format(CONSTANTS.TEXTINPUT_NAME_FORMAT, fieldInfo.FieldName),
                Tag = fieldInfo,
                EnterMoveNextControl = true
            };

            var button = txtButtonEdit.Properties.Buttons[0];


            button.Tag = fieldInfo.ListSource;

            txtButtonEdit.ButtonClick += delegate (object sender, ButtonPressedEventArgs e) {
                if (e.Button == button)
                {
                    var dialog = new SaveFileDialog { Filter = (string)button.Tag };

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        txtButtonEdit.Text = dialog.FileName;
                    }
                }
            };
            return txtButtonEdit;
        }
        protected virtual ButtonEdit CreateOpenFileControl(ModuleFieldInfo fieldInfo)
        {
            var txtButtonEdit = new ButtonEdit
            {
                Name = string.Format(CONSTANTS.TEXTINPUT_NAME_FORMAT, fieldInfo.FieldName),
                Tag = fieldInfo,
                EnterMoveNextControl = true
            };
            //trungtt - 6.12.2013 - follow SSC's sugestion
            //Disable textbox.
            //txtButtonEdit.Text = "(Chọn đường dẫn)";
            txtButtonEdit.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            //end trungtt
            var button = txtButtonEdit.Properties.Buttons[0];
            button.Tag = fieldInfo.ListSource;

            txtButtonEdit.ButtonClick += delegate (object sender, ButtonPressedEventArgs e) {
                if (e.Button == button)
                {
                    try
                    {
                        Program.strExecMod = ModuleInfo.ExecuteMode.ToString();
                        //var targetModule = (ucSearchMaster)MainProcess.CreateModuleInstance(STATICMODULE.UPFILE_MODID);
                        var targetModule =
                            (ucUploadFile)MainProcess.CreateModuleInstance(STATICMODULE.UPFILE_MODULE);

                        //if (targetModule is ucSearchMaster)
                        if (targetModule is ucUploadFile)
                            targetModule.ShowDialogModule(this);
                        if (Program.blCheckFile)
                            //txtButtonEdit.Text = "(File attached)";
                            txtButtonEdit.Text = Program.FileName;
                    }
                    catch (Exception ex)
                    {
                        ShowError(ex);
                    }

                }
                // }
            };

            return txtButtonEdit;
        }

        protected virtual TextEdit CreateTextBoxControl(ModuleFieldInfo fieldInfo)
        {
#if DEBUG
            var txtControl = new ButtonEdit
            {
                Name = string.Format(CONSTANTS.TEXTINPUT_NAME_FORMAT, fieldInfo.FieldName),
                Tag = fieldInfo,
                EnterMoveNextControl = true
            };

            switch (fieldInfo.TextCase)
            {
                case CONSTANTS.TEXTCASE_U:
                    txtControl.Properties.CharacterCasing = CharacterCasing.Upper;
                    break;
                case CONSTANTS.TEXTCASE_L:
                    txtControl.Properties.CharacterCasing = CharacterCasing.Lower;
                    break;
                default:
                    txtControl.Properties.CharacterCasing = CharacterCasing.Normal;
                    break;
            }

            txtControl.Properties.Buttons.RemoveAt(0);
            if (fieldInfo.MaxLength > 0)
            {
                txtControl.Properties.MaxLength = fieldInfo.MaxLength;
            }
#else
            var txtControl = new TextEdit
            {
                Name = string.Format(CONSTANTS.TEXTINPUT_NAME_FORMAT, fieldInfo.FieldName),
                Tag = fieldInfo,
                EnterMoveNextControl = true
            };
            switch (fieldInfo.TextCase)
            {
                case CONSTANTS.TEXTCASE_U:
                    txtControl.Properties.CharacterCasing = CharacterCasing.Upper;
                    break;
                case CONSTANTS.TEXTCASE_L:
                    txtControl.Properties.CharacterCasing = CharacterCasing.Lower;
                    break;
                default:
                    txtControl.Properties.CharacterCasing = CharacterCasing.Normal;
                    break;
            }            
            if (fieldInfo.MaxLength > 0)
            {
                txtControl.Properties.MaxLength = fieldInfo.MaxLength;
            }
#endif
            return txtControl;
        }

        protected virtual TextEdit CreateLabelControl(ModuleFieldInfo fieldInfo)
        {
#if DEBUG
            var lbControl = new ButtonEdit
            {
                Name = string.Format(CONSTANTS.TEXTINPUT_NAME_FORMAT, fieldInfo.FieldName),
                Tag = fieldInfo,
                EnterMoveNextControl = true
            };
            lbControl.Properties.ReadOnly = true;
            lbControl.BorderStyle = BorderStyles.NoBorder;
            lbControl.Properties.Buttons.RemoveAt(0);
#else
            var lbControl = new TextEdit
            {
                Name = string.Format(CONSTANTS.TEXTINPUT_NAME_FORMAT, fieldInfo.FieldName),
                Tag = fieldInfo,
                EnterMoveNextControl = true
            };
#endif
            return lbControl;
        }

        protected virtual CalcEdit CreateSpinEditControl(ModuleFieldInfo fieldInfo)
        {
            var calcEdit = new CalcEdit
            {
                Name = string.Format(CONSTANTS.TEXTINPUT_NAME_FORMAT, fieldInfo.FieldName),
                Tag = fieldInfo,
                EnterMoveNextControl = true
            };
            calcEdit.Properties.AllowNullInput = DefaultBoolean.True;
            return calcEdit;
        }

        protected virtual ImageComboBoxEdit CreateComboBoxControl(ModuleFieldInfo fieldInfo)
        {
            var comboBoxEdit = new ImageComboBoxEdit
            {
                Name = string.Format(CONSTANTS.COMBOX_NAME_FORMAT, fieldInfo.FieldName),
                Tag = fieldInfo,
                EnterMoveNextControl = true
            };
            comboBoxEdit.Properties.LargeImages = ThemeUtils.Image16;
            comboBoxEdit.Properties.SmallImages = ThemeUtils.Image16;

            return comboBoxEdit;
        }

        protected virtual LookUpEdit CreateLookUpEditControlBase(ModuleFieldInfo fieldInfo)
        {
            var lookUpEdit = new LookUpEdit
            {
                Name = string.Format(CONSTANTS.COMBOX_NAME_FORMAT, fieldInfo.FieldName),
                Tag = fieldInfo,
                EnterMoveNextControl = true
            };
            lookUpEdit.Properties.TextEditStyle = TextEditStyles.Standard;
            lookUpEdit.Properties.NullText = string.Empty;
            lookUpEdit.Properties.SearchMode = SearchMode.AutoFilter;
            lookUpEdit.Properties.ShowHeader = true;
            lookUpEdit.Properties.ShowFooter = true;
            lookUpEdit.Properties.ShowLines = true;
            lookUpEdit.Properties.HotTrackItems = true;
            lookUpEdit.Properties.HeaderClickMode = HeaderClickMode.Sorting;
            return lookUpEdit;
        }

        protected virtual CheckedComboBoxEdit CreateCheckComboBoxControl(ModuleFieldInfo fieldInfo)
        {
            var checkedcomboBoxEdit = new CheckedComboBoxEdit
            {
                Name = string.Format(CONSTANTS.COMBOX_NAME_FORMAT, fieldInfo.FieldName),
                Tag = fieldInfo,
                EnterMoveNextControl = true
            };

            checkedcomboBoxEdit.Properties.IncrementalSearch = true;
            checkedcomboBoxEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            checkedcomboBoxEdit.Properties.ShowButtons = false;
            return checkedcomboBoxEdit;
        }

        //add by TrungTT - 28.11.2011 - Create GridView Control
        protected virtual GridControl CreateGridViewControl(ModuleFieldInfo fieldInfo)
        {
            var gridControl = new GridControl
            {
                Name = string.Format(CONSTANTS.GRIDVIEW_NAME_FORMAT, fieldInfo.FieldName),
                Tag = fieldInfo
            };

            return gridControl;
        }

        protected virtual ucGridViewExt CreateGridViewExtControl(ModuleFieldInfo fieldInfo)
        {
            var gridControl = new ucGridViewExt
            {
                Name = string.Format(CONSTANTS.GRIDVIEW_NAME_FORMAT, fieldInfo.FieldName),
                Tag = fieldInfo
            };
            return gridControl;
        }

        //End TrungTT
        protected virtual TextEdit CreatePasswordControl(ModuleFieldInfo fieldInfo)
        {
#if DEBUG
            var txtPassword = new ButtonEdit
            {
                Name = string.Format(CONSTANTS.TEXTINPUT_NAME_FORMAT, fieldInfo.FieldName),
                Tag = fieldInfo,
                EnterMoveNextControl = true
            };
            txtPassword.Properties.Buttons.RemoveAt(0);
#else
            var txtPassword = new TextEdit
            {
                Name = string.Format(CONSTANTS.TEXTINPUT_NAME_FORMAT, fieldInfo.FieldName),
                Tag = fieldInfo,
                EnterMoveNextControl = true
            };
#endif

            txtPassword.Properties.PasswordChar = '*';

            return txtPassword;
        }

        protected virtual MemoEdit CreateTextAreaControl(ModuleFieldInfo fieldInfo)
        {
            var txtControl = new MemoEdit
            {
                Name = string.Format(CONSTANTS.TEXTINPUT_NAME_FORMAT, fieldInfo.FieldName),
                Tag = fieldInfo
            };

            txtControl.Properties.AcceptsReturn = true;
            txtControl.Properties.WordWrap = true;
            txtControl.Properties.ScrollBars = ScrollBars.None;
            txtControl.TextChanged += MemoEdit_TextChanged;
            if (fieldInfo.MaxLength > 0)
            {
                txtControl.Properties.MaxLength = fieldInfo.MaxLength;
            }
            return txtControl;
        }
        /// <summary>
        /// create rich editor
        /// </summary>
        /// <param name="fieldInfo"></param>
        /// <returns></returns>
        /// 
        protected virtual RichEditControl CreateRichTextEdit(ModuleFieldInfo fieldInfo)
        {
            //var rtControl = new RichEditControl
            //{
            //    Name = string.Format(CONSTANTS.RICHTEXT_NAME_FORMAT, fieldInfo.FieldName),
            //    Tag = fieldInfo
            //};
            rtControl = new RichEditControl();
            spellChecker1 = new SpellChecker();
            rtControl.SpellChecker = spellChecker1;
            //spellChecker1.SetShowSpellCheckMenu(rtControl, false);
            //spellChecker1.SetSpellCheckerOptions(this.rtControl, optionsSpelling1);
            CultureInfo usCulture = new CultureInfo("en-US");
            SpellCheckerISpellDictionary dictionary1 = new SpellCheckerISpellDictionary(@"..\..\american.xlg", @"..\..\english.aff", usCulture);
            dictionary1.AlphabetPath = @"..\..\EnglishAlphabet.txt";
            dictionary1.Load();
            spellChecker1.Dictionaries.Add(dictionary1);
            rtControl.PopupMenuShowing += new DevExpress.XtraRichEdit.PopupMenuShowingEventHandler(rtControl_PopupMenuShowing);
            return rtControl;
        }


        //protected ComboBoxEdit CreateSuggestionTextBoxControl(ModuleFieldInfo fieldInfo)
        //{
        //    var cboControl = new ComboBoxEdit
        //    {
        //        Name = string.Format(CONSTANTS.COMBOX_NAME_FORMAT, fieldInfo.FieldName),
        //        Tag = fieldInfo,
        //        EnterMoveNextControl = true
        //    };

        //    cboControl.Properties.TextEditStyle = TextEditStyles.Standard;
        //    return cboControl;
        //}

        protected TextEdit CreateSuggestionTextBoxControl(ModuleFieldInfo fieldInfo)
        {
            var txtControl = new TextEdit
            {
                Name = string.Format(CONSTANTS.TEXTINPUT_NAME_FORMAT, fieldInfo.FieldName),
                Tag = fieldInfo,
                EnterMoveNextControl = true
            };

            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            List<string> value = new List<string>();
            var sourceList = App.Environment.GetSourceList(ModuleInfo, fieldInfo, value);

            collection.AddRange((from item in sourceList select item.Value).ToArray());
            txtControl.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtControl.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtControl.MaskBox.AutoCompleteCustomSource = collection;
            return txtControl;
        }

        protected virtual PopupContainerEdit CreateCheckedComboBoxControl(ModuleFieldInfo fieldInfo)
        {
            // 1. Create Popup Edit
            var ccbControl = new PopupContainerEdit
            {
                Name = string.Format(CONSTANTS.CHECKEDCOMBOBOX_NAME_FORMAT, fieldInfo.FieldName),
                Tag = fieldInfo,
                EnterMoveNextControl = true,
                EditValue = null
            };
            //ccbControl.Properties.TextEditStyle = TextEditStyles.Standard;
            ccbControl.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            ccbControl.Properties.PopupFormMinSize = new Size(750, 350);
            ccbControl.Properties.PopupResizeMode = ResizeMode.FrameResize;

            // 2. Create Items & Buttons Panel
            var hozItemsPanel = CreateItemsPanel();
            var hozButtonsPanel = CreateButtonsPanel(hozItemsPanel);

            // 3. Create Popup
            var ccbPopup = new PopupContainerControl
            {
                Tag = fieldInfo,
                AutoSize = true
            };

            // Create Popup --> Add Popup to Edit
            ccbControl.Properties.PopupControl = ccbPopup;
            ccbControl.Properties.Tag = hozItemsPanel;

            // 4. Create Bound Panel
            var boundPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1
            };
            // Bound Panel --> Add Items & Buttons Panel to Bound Panel
            boundPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            boundPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            boundPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            boundPanel.Controls.Add(hozButtonsPanel, 0, 0);
            boundPanel.Controls.Add(hozItemsPanel, 0, 1);
            // Bound Panel --> Add to Popup
            ccbPopup.Controls.Add(boundPanel);

            // QueryPopup => Check item which has in Text
            ccbControl.QueryPopUp += delegate {
                hozItemsPanel.SuspendLayout();
                var list = ccbControl.Text.Split(',');
                foreach (CheckEdit value in hozItemsPanel.Controls)
                {
                    value.CheckState = list.Contains(value.Tag.ToString()) ?
                    //value.CheckState = list.Contains(value.Text.ToString()) ?
                                                                               CheckState.Checked : CheckState.Unchecked;
                }
                ccbPopup.Width = ccbControl.Width;
                hozItemsPanel.ResumeLayout(true);
            };
            // QueryResultValue => Parse checked items to Text
            ccbControl.QueryResultValue += delegate (object sender, QueryResultValueEventArgs e) {
                var list = (from CheckEdit item in hozItemsPanel.Controls
                            where item.Checked
                            //select item.Text.ToString()).ToArray();
                            select item.Tag.ToString()).ToArray();
                e.Value = string.Join(",", list);
            };

            return ccbControl;
        }

        protected virtual FlowLayoutPanel CreateButtonsPanel(FlowLayoutPanel hozItemsPanel)
        {
            var panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill
            };

            #region Add Checkall button
            var checkAll = new CheckEdit { Text = "" };
            checkAll.Properties.AutoWidth = true;

            checkAll.CheckedChanged += delegate {
                hozItemsPanel.SuspendLayout();
                foreach (CheckEdit item in hozItemsPanel.Controls)
                {
                    if (item.Visible)
                        item.Checked = checkAll.Checked;
                }
                hozItemsPanel.ResumeLayout(true);
            };

            panel.Controls.Add(checkAll);
            #endregion

            //var txtFilter = new TextEdit
            //                    {
            //                        Width = 200,
            //                        Height = 22,
            //                        Margin = new Padding(0)
            //                    };
            //panel.Controls.Add(txtFilter);
            #region 0-9 button
            var btnNum = new SimpleButton
            {
                Width = 44,
                Height = 22,
                Margin = new Padding(0),
                Text = "0-9"
            };
            btnNum.Click += delegate {
                hozItemsPanel.SuspendLayout();
                foreach (CheckEdit item in hozItemsPanel.Controls)
                {
                    item.Visible = Regex.IsMatch(item.Text, "^[0-9]");
                }
                hozItemsPanel.ResumeLayout(true);
            };

            panel.Controls.Add(btnNum);
            #endregion

            #region A .. Z buttons
            for (var c = 'A'; c <= 'Z'; c++)
            {
                var button = new SimpleButton
                {
                    Width = 22,
                    Height = 22,
                    Margin = new Padding(0),
                    Text = c.ToString()
                };
                button.Click += delegate {
                    hozItemsPanel.SuspendLayout();
                    foreach (CheckEdit item in hozItemsPanel.Controls)
                    {
                        item.Visible = item.Text.StartsWith(button.Text);
                    }
                    hozItemsPanel.ResumeLayout(true);
                };
                panel.Controls.Add(button);
            }
            #endregion

            #region 0-Z buttons
            // All Button
            var btnAll = new SimpleButton
            {
                Width = 44,
                Height = 22,
                Margin = new Padding(0),
                Text = "0-Z"
            };

            btnAll.Click += delegate {
                hozItemsPanel.SuspendLayout();
                foreach (CheckEdit item in hozItemsPanel.Controls)
                {
                    item.Visible = true;
                }
                hozItemsPanel.ResumeLayout(true);
            };

            panel.Controls.Add(btnAll);
            #endregion
            return panel;
        }

        protected virtual FlowLayoutPanel CreateItemsPanel()
        {
            var panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(0, 0, 15, 0),
                AutoScroll = true
            };
            return panel;
        }

        #region Events
        private static void MemoEdit_TextChanged(object sender, EventArgs e)
        {
            var memoEdit = sender as MemoEdit;
            if (memoEdit != null)
            {
                var vi = memoEdit.GetViewInfo() as MemoEditViewInfo;
                if (vi != null)
                {
                    var cache = new GraphicsCache(memoEdit.CreateGraphics());
                    var h = ((IHeightAdaptable)vi).CalcHeight(cache, vi.MaskBoxRect.Width);
                    var args = new ObjectInfoArgs
                    {
                        Bounds = new Rectangle(0, 0, vi.ClientRect.Width, h)
                    };
                    var rect = vi.BorderPainter.CalcBoundsByClientRectangle(args);
                    cache.Dispose();
                    memoEdit.Properties.ScrollBars = rect.Height > memoEdit.Height ? ScrollBars.Vertical : ScrollBars.None;
                }
            }
        }
        private void rtControl_PopupMenuShowing(object sender, DevExpress.XtraRichEdit.PopupMenuShowingEventArgs e)
        {
            if (spellChecker1.SpellCheckMode == SpellCheckMode.OnDemand)
            {
                try
                {
                    DocumentPosition pos = rtControl.Document.CaretPosition;
                    int wordEnd = GetWordEndIndex(pos);
                    int wordStart = GetWordStartIndex(pos);
                    if (wordEnd <= wordStart)
                        return;
                    DocumentRange range = rtControl.Document.CreateRange(wordStart, wordEnd - wordStart);
                    string word = rtControl.Document.GetText(range);
                    if (spellChecker1.IsMisspelledWord(word, spellChecker1.Culture))
                        CreateMenuItems(e.Menu, range, word);
                }
                catch (Exception ex)
                { }
            }
        }
        #endregion
        void CreateMenuItems(RichEditPopupMenu menu, DocumentRange range, string word)
        {
            SuggestionCollection suggestions = this.spellChecker1.GetSuggestions(word);
            int count = suggestions.Count;
            if (count > 0)
            {
                int lastIndex = Math.Min(count - 1, 5);
                for (int i = lastIndex; i >= 0; i--)
                {
                    SuggestionBase suggestion = suggestions[i];
                    SuggestionMenuItem item =
                        new SuggestionMenuItem(rtControl.Document, suggestion.Suggestion, range);
                    menu.Items.Insert(0, item);
                }
            }
            else
            {
                DXMenuItem emptyItem = new DXMenuItem("no spelling suggestions");
                emptyItem.Enabled = false;
                menu.Items.Insert(0, emptyItem);
            }
        }
        //----------------------------------------------------------------------------------
        char GetCharacter(int position)
        {
            DocumentRange range = rtControl.Document.CreateRange(position, 1);
            return rtControl.Document.GetText(range)[0];

        }
        int GetWordEndIndex(DocumentPosition position)
        {
            int currentPosition = position.ToInt();
            //int result = currentPosition;
            int endPosition = rtControl.Document.Range.End.ToInt() - 1;
            while (currentPosition <= endPosition && !wordSeparators.Contains(GetCharacter(currentPosition)))
            {
                currentPosition++;
                //result = currentPosition;
            }
            return currentPosition;
        }
        int GetWordStartIndex(DocumentPosition position)
        {
            int currentPosition = position.ToInt();
            int result = currentPosition;
            while (currentPosition >= 0 && !wordSeparators.Contains(GetCharacter(currentPosition)))
            {
                result = currentPosition;
                currentPosition--;
            }
            return result;
        }

        protected virtual RadioGroup CreateRadioGroupControl(ModuleFieldInfo fieldInfo)
        {
            var radioGroup = new RadioGroup
            {
                Name = string.Format(CONSTANTS.RG_NAME_FORMAT, fieldInfo.FieldName),
                Tag = fieldInfo,
                EnterMoveNextControl = true
            };

            radioGroup.Properties.Columns = 1;
            radioGroup.BackColor = Color.Transparent;
            radioGroup.BorderStyle = BorderStyles.NoBorder;
            return radioGroup;
        }

        protected virtual CheckEdit CreateCheckBoxControl(ModuleFieldInfo fieldInfo)
        {
            var checkEdit = new CheckEdit
            {
                Name = string.Format(CONSTANTS.CHKBOX_NAME_FORMAT, fieldInfo.FieldName),
                Tag = fieldInfo,
                EnterMoveNextControl = true,
            };
            // Add Style for CheckEdit
            //checkEdit.Properties.CheckBoxOptions.Style = CheckBoxStyle.CheckBox;
            checkEdit.Properties.Images = ThemeUtils.Image16;
            checkEdit.Text = null;
            checkEdit.Properties.ValueChecked = CONSTANTS.CHECKBOX_CHECKED;
            checkEdit.Properties.ValueUnchecked = CONSTANTS.CHECKBOX_UBCHECK;
            return checkEdit;
        }

        protected virtual PictureEdit CreatePictureEditBoxControl(ModuleFieldInfo fieldInfo)
        {
            var pictureEdit = new PictureEdit
            {
                Name = string.Format(CONSTANTS.PIC_EDIT_BOX_NAME_FORMAT, fieldInfo.FieldName),
                Tag = fieldInfo,
                EnterMoveNextControl = true
            };

            //pictureEdit.Image = ThemeUtils.Image16;
            pictureEdit.Properties.SizeMode = PictureSizeMode.Stretch;
            pictureEdit.Properties.PictureAlignment = ContentAlignment.MiddleCenter;
            pictureEdit.Text = null;
            return pictureEdit;
        }

        protected virtual CalcEdit CreateCalcEditBoxControl(ModuleFieldInfo fieldInfo)
        {
            var calcEdit = new CalcEdit
            {
                Name = string.Format(CONSTANTS.CALC_EDIT_BOX_NAME_FORMAT, fieldInfo.FieldName),
                Tag = fieldInfo,
                EnterMoveNextControl = true
            };

            calcEdit.Properties.Precision = 3;
            calcEdit.Text = null;
            return calcEdit;
        }
        protected virtual RatingControl CreateRatingControl(ModuleFieldInfo fieldInfo)
        {
            var ratingControl = new RatingControl
            {
                Name = string.Format(CONSTANTS.RATING_NAME_FORMAT, fieldInfo.FieldName),
                Tag = fieldInfo,
                EnterMoveNextControl = true
            };

            ratingControl.Properties.RatingOrientation = Orientation.Horizontal;
            ratingControl.Properties.ItemCount = 5;
            return ratingControl;
        }
        protected virtual TimeEdit CreateTimeEditControl(ModuleFieldInfo fieldInfo)
        {
            var timeEdit = new TimeEdit
            {
                Name = string.Format(CONSTANTS.TIME_EDIT_NAME_FORMAT, fieldInfo.FieldName),
                Tag = fieldInfo,
                EnterMoveNextControl = true
            };

            timeEdit.Properties.TimeEditStyle = TimeEditStyle.TouchUI;
            timeEdit.Properties.Mask.EditMask = "hh:mm:ss tt";
            return timeEdit;
        }
        protected virtual ToggleSwitch CreateToggleSwitchControl(ModuleFieldInfo fieldInfo)
        {
            var toggleSwitch = new ToggleSwitch
            {
                Name = string.Format(CONSTANTS.TIME_EDIT_NAME_FORMAT, fieldInfo.FieldName),
                Tag = fieldInfo,
                EnterMoveNextControl = true
            };

            toggleSwitch.Properties.AllowThumbAnimation = true;
            toggleSwitch.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center; // Không hiển thị Text On/Off            
            //toggleSwitch.Properties.OnText = "On";
            //toggleSwitch.Properties.OffText = "Off";            
            toggleSwitch.IsOn = !toggleSwitch.IsOn; // default is ON
            return toggleSwitch;
        }
    }
    public class SuggestionMenuItem : DXMenuItem
    {
        readonly Document document;
        readonly DocumentRange range;

        public SuggestionMenuItem(Document document, string suggestion, DocumentRange range)
            : base(suggestion)
        {
            this.document = document;
            this.range = range;
        }

        protected override void OnClick()
        {
            document.Replace(range, Caption);
        }
    }

}

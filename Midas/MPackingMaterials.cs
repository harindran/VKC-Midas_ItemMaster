using System;
using System.Collections.Generic;
using System.Text;

namespace VKC
{
    class MPackingMaterials
    {
        General gen = new General();

        #region Singleton

        private static MPackingMaterials instance;

        public  static  MPackingMaterials Instance
        {
            get
            {
                if (instance == null) instance = new MPackingMaterials();

                return instance;
            }
        }

        #endregion

        public MPackingMaterials()
        {
            VPackingMaterials vw = VPackingMaterials.Instance;
        }
        public void Classification()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                //oForm.Freeze(true);
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbClass").Specific;
                oComboItem.Select("7", SAPbouiCOM.BoSearchKey.psk_ByValue);
               // oForm.Freeze(false);

            }
            catch { }
        }
        #region GetCombos
        public void GetCombos()
        {
            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;

            try
            {
                oForm.Freeze(true);
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbClass").Specific;
                SAPbouiCOM.ComboBox oComboGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbGroup").Specific;
                SAPbouiCOM.ComboBox oComboItemGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbPItgp").Specific;
                SAPbouiCOM.ComboBox oComboSubGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("54B").Specific;

                gen.FillCombo(oForm, oComboItem, "@CLASSIFICATION", "Code", "Name", true, true);
                gen.FillCombo(oForm, oComboGroup, "@PACKGROUP", "Code", "Name", true, true);
                gen.FillCombo(oForm, oComboSubGroup, "@PACKSUBGRP", "Code", "Name", true, true);
                gen.FillCombo(oComboItemGroup, true);

                SAPbouiCOM.EditText oEditBoxDescriptn = (SAPbouiCOM.EditText)oForm.Items.Item("txtPDescri").Specific;

                SAPbouiCOM.EditText oEditBoxName = (SAPbouiCOM.EditText)oForm.Items.Item("txtPkName").Specific;
                SAPbouiCOM.EditText oEditBoxCode = (SAPbouiCOM.EditText)oForm.Items.Item("txtPkCode").Specific;

               oComboItem.Select("7", SAPbouiCOM.BoSearchKey.psk_ByValue);
               oComboGroup.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
               oComboSubGroup.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
               oEditBoxDescriptn.Value = "";
                oEditBoxCode.Value ="";
                oEditBoxName.Value ="";

               oForm.Freeze(false);
               
            }

            catch { oForm.Freeze(false); }

        }
        #endregion
        # region Validations
        public bool Validation()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;

                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbClass").Specific;
                SAPbouiCOM.ComboBox oComboGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbGroup").Specific;
                SAPbouiCOM.EditText oEditBoxDescriptn = (SAPbouiCOM.EditText)oForm.Items.Item("txtPDescri").Specific;
                SAPbouiCOM.ComboBox oComboItemGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbPItgp").Specific;

                if (oComboItem.Selected.Value != "7")
                {
                    Global.SapApplication.StatusBar.SetText("Classification Not Correct !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    return false;
                }
                else if (oComboGroup.Selected.Value.Trim() == "-1" || oComboGroup.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Group !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    return false;

                }
                else if (oEditBoxDescriptn.Value =="")
                {
                    Global.SapApplication.StatusBar.SetText("Please Enter Description !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    return false;

                }
                else if (oComboItemGroup.Value == "-1" || oComboItemGroup.Value == "")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Item Group !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    return false;

                }
               

            }
            catch { return false; }
            return true;
        }
         #endregion
        #region GenerateCode

        public void GenerateCode()
        {
            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;

            try
            {
                string sequence = "";
                string SubGroup = "";
                oForm.Freeze(true);
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbClass").Specific;
                SAPbouiCOM.ComboBox oComboGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbGroup").Specific;
                SAPbouiCOM.ComboBox oComboSubGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("54B").Specific;
                SAPbouiCOM.EditText oEditBoxDescriptn = (SAPbouiCOM.EditText)oForm.Items.Item("txtPDescri").Specific;

                SAPbouiCOM.EditText oEditBoxName = (SAPbouiCOM.EditText)oForm.Items.Item("txtPkName").Specific;
                SAPbouiCOM.EditText oEditBoxCode = (SAPbouiCOM.EditText)oForm.Items.Item("txtPkCode").Specific;
                if (oComboSubGroup.Selected.Value.Trim() == "-1" || oComboSubGroup.Selected.Value.Trim() == "-999")
                {
                    SubGroup = "";
                }
                else
                {
                    SubGroup = oComboSubGroup.Selected.Value;
                }
                sequence = gen.GetNextItemCode(oComboGroup.Selected.Value + SubGroup);

                //string itemCode = oComboItem.Selected.Value + "-" + oComboGroup.Selected.Value  + "-" + sequence;
                //string itemName = oComboItem.Selected.Description + "-" + oComboGroup.Selected.Description  + "-" + oEditBoxDescriptn.Value;
                string itemCode = "";
                string itemName = "";

                if (SubGroup == "")
                {
                    itemCode = oComboItem.Selected.Value + "-" + oComboGroup.Selected.Value  + "-" + sequence;
                    itemName = oComboItem.Selected.Description + "-" + oComboGroup.Selected.Description + "-" + oEditBoxDescriptn.Value;
                    
                }
                else
                {
                    itemCode = oComboItem.Selected.Value + "-" + oComboGroup.Selected.Value + "-" + oComboSubGroup.Selected.Value + "-" + sequence;
                    itemName = oComboItem.Selected.Description + "-" + oComboGroup.Selected.Description + "-" + oComboSubGroup.Selected.Description + "-" + oEditBoxDescriptn.Value;
                }

                oEditBoxName.Value = itemName;
                oEditBoxCode.Value = itemCode;
                oForm.Freeze(false);
            }
            catch { oForm.Freeze(false); }
        }
        #endregion
        #region ClearCombo
        public void ClearCombo(SAPbouiCOM.ItemEvent val)
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.Item(val.FormUID);
                SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(oForm.DataSources.UserDataSources.Item("PFormID").Value);

                SAPbouiCOM.ComboBox oComboGroup = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbGroup").Specific;
                SAPbouiCOM.EditText oEditBoxDescriptn = (SAPbouiCOM.EditText)PForm.Items.Item("txtPDescri").Specific;
                SAPbouiCOM.ComboBox oComboItemGroup = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbPItgp").Specific;


                SAPbouiCOM.EditText oEditBoxName = (SAPbouiCOM.EditText)PForm.Items.Item("txtPkName").Specific;
                SAPbouiCOM.EditText oEditBoxCode = (SAPbouiCOM.EditText)PForm.Items.Item("txtPkCode").Specific;

                MItemMasterData.Instance.InitializeCombo(oComboGroup);
                MItemMasterData.Instance.InitializeCombo(oComboItemGroup);
  
                oEditBoxCode.Value = "";
                oEditBoxName.Value = "";
                oEditBoxDescriptn.Value = "";
            }

            catch
            {
                Global.SapApplication.StatusBar.SetText("Error!!", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

            }
        }
          #endregion
        #region Define New
        public void DefinePackingGrp(string Item)
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item(Item).Specific;
                if (Item == "cmbGroup")
                {
                    if ((oComboItem.Value.ToString() == "")|| (oComboItem.Selected.Value == "-999"))
                    {
                        MItemMasterData.Instance.DifineNew("PACKGROUP", oForm.UniqueID);

                    }
                    //else if (oComboItem.Selected.Value == "-999")
                    //{
                    //    MItemMasterData.Instance.DifineNew("PACKGROUP", oForm.UniqueID);
                    //}
                }
                else if (Item == "54B")
                {
                    if ((oComboItem.Value.ToString() == "") || (oComboItem.Selected.Value == "-999"))
                    {
                        MItemMasterData.Instance.DifineNew("PACKSUBGRP", oForm.UniqueID);

                    }
                    //else if (oComboItem.Selected.Value == "-999")
                    //{
                    //    MItemMasterData.Instance.DifineNew("PACKSUBGRP", oForm.UniqueID);
                    //}
                }

                else
                {
                    if ((oComboItem.Value.ToString() == "") || (oComboItem.Selected.Value == "-999"))
                    {
                        MItemMasterData.Instance.DifineNew("MODEL", oForm.UniqueID);

                    }
                }


            }

            catch { }
        }
        #endregion

        #region Refresh Combo Box
        public void RefreshCombos(string FormID, string ComboName)
        {
            SAPbouiCOM.Form CForm = Global.SapApplication.Forms.ActiveForm;
            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.Item(FormID);
            SAPbouiCOM.ComboBox oComboGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbGroup").Specific;
            SAPbouiCOM.ComboBox oComboModel = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfmodel").Specific;

            SAPbouiCOM.ComboBox oComboSubGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("54B").Specific;
            if (ComboName == "PACKGROUP")
            {
                gen.FillCombo(oForm, oComboGroup, "@PACKGROUP", "Code", "Name", true, true);
                oComboGroup.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }
            else if (ComboName == "PACK SUB GROUP")
            {
                gen.FillCombo(oForm, oComboSubGroup, "@PACKSUBGRP", "Code", "Name", true, true);
                oComboSubGroup.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }
            else
            {
                //gen.FillCombo(oForm, oComboModel, "@MODEL", "Code", "Name", true, true);
             }
        


        }
        #endregion
    }
}

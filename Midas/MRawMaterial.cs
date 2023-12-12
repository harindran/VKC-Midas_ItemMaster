using System;
using System.Collections.Generic;
using System.Text;

namespace VKC
{
    class MRawMaterial
    {
          General gen = new General();

        #region Singleton

        private static MRawMaterial instance;

        public  static  MRawMaterial Instance
        {
            get
            {
                if (instance == null) instance = new MRawMaterial();

                return instance;
            }
        }

        #endregion

        public MRawMaterial()
        {
            VRawMaterials vw = VRawMaterials.Instance;
        }
        public void Classification()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
               // oForm.Freeze(true);
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbRwClass").Specific;
                oComboItem.Select("5", SAPbouiCOM.BoSearchKey.psk_ByValue);
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
                   SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbRwClass").Specific;
                SAPbouiCOM.ComboBox oComboGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbRawGrp").Specific;
                SAPbouiCOM.ComboBox oComboSubGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbRSubGrp").Specific;
                SAPbouiCOM.ComboBox oComboItemGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbRItgp").Specific;


                gen.FillCombo(oForm, oComboItem, "@CLASSIFICATION", "Code", "Name", true, true);
                gen.FillCombo(oForm, oComboGroup, "@RAWGROUP", "Code", "Name", true, true);
                gen.FillCombo(oForm, oComboSubGroup, "@RAWSUBGROUP", "Code", "Name", true, true);
                gen.FillCombo(oComboItemGroup, true);
                SAPbouiCOM.EditText oEditBoxDescriptn = (SAPbouiCOM.EditText)oForm.Items.Item("txtDescrip").Specific;

                SAPbouiCOM.EditText oEditBoxName = (SAPbouiCOM.EditText)oForm.Items.Item("txtRawName").Specific;
                SAPbouiCOM.EditText oEditBoxCode = (SAPbouiCOM.EditText)oForm.Items.Item("txtRawCode").Specific;

                oComboItem.Select("5", SAPbouiCOM.BoSearchKey.psk_ByValue);
                oComboGroup.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                oComboSubGroup.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                oEditBoxCode.Value = "";
                oEditBoxDescriptn.Value ="";
                oEditBoxName.Value="";
               
                oForm.Freeze(false);
              
            }

            catch { oForm.Freeze(false); }

        }
        #endregion

        #region ChemicalMixChange
        public void ChemicalMixChange()
        {
            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;

            try
            {
                oForm.Freeze(true);
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbRwClass").Specific;
                SAPbouiCOM.ComboBox oComboGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbRawGrp").Specific;
                SAPbouiCOM.ComboBox oComboSubGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbRSubGrp").Specific;
                SAPbouiCOM.ComboBox oComboItemGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbRItgp").Specific;
                SAPbouiCOM.CheckBox oCbxMix = (SAPbouiCOM.CheckBox)oForm.Items.Item("cbxMix").Specific;

                string abc = oForm.DataSources.UserDataSources.Item("CbxMix").ValueEx;

                if (oCbxMix.Checked == false)
                oComboItem.Select("4", SAPbouiCOM.BoSearchKey.psk_ByValue);
                else
                oComboItem.Select("5", SAPbouiCOM.BoSearchKey.psk_ByValue);
              
             

                oForm.Freeze(false);

            }

            catch { oForm.Freeze(false); }

        }
        #endregion

        #region Validations
        public bool Validation()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;

                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbRwClass").Specific;
                SAPbouiCOM.ComboBox oComboGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbRawGrp").Specific;
                SAPbouiCOM.ComboBox oComboSubGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbRSubGrp").Specific;
                SAPbouiCOM.EditText oEditBoxDescriptn = (SAPbouiCOM.EditText)oForm.Items.Item("txtDescrip").Specific;

                SAPbouiCOM.ComboBox oComboItemGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbRItgp").Specific;

                SAPbouiCOM.CheckBox oCbxMix = (SAPbouiCOM.CheckBox)oForm.Items.Item("cbxMix").Specific;

                if (oCbxMix.Checked == false)
                {
                    if (oComboItem.Selected.Value != "5")
                    {
                        Global.SapApplication.StatusBar.SetText("Classification Not Correct !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                        return false;
                    }
                }
                else if (oCbxMix.Checked == true)
                {
                    if (oComboItem.Selected.Value != "4" )
                    {
                        Global.SapApplication.StatusBar.SetText("Classification Not Correct !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                        return false;
                    }
                }
                else if (oComboGroup.Selected.Value.Trim() == "-1" || oComboGroup.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Group !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    return false;

                }
                else if (oComboSubGroup.Selected.Value.Trim() == "-1" || oComboSubGroup.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Sub Group !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    return false;

                }
                else if (oEditBoxDescriptn.Value == "")
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
        #region Fill SubGroup
        public void FillSubGroupCombo()
        {
            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;

            try
            {
                oForm.Freeze(true);
                SAPbouiCOM.ComboBox oCombogroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbRawGrp").Specific;
                SAPbouiCOM.ComboBox oComboSubGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbRSubGrp").Specific;
                string code = oCombogroup.Selected.Value;
                gen.FillCombo(oForm, oComboSubGroup, "@RAWSUBGROUP", "Code", "Name", " where [U_Group]='" + code + "'", true, true);
                oComboSubGroup.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                oForm.Freeze(false);
            }
            catch { oForm.Freeze(false); }
        }
        #endregion
        #region Generate Code
        public void GenerateCode()
        {
            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;

            try
            {
                oForm.Freeze(true);
               SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbRwClass").Specific;
                SAPbouiCOM.ComboBox oComboGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbRawGrp").Specific;
                SAPbouiCOM.ComboBox oComboSubGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbRSubGrp").Specific;
                SAPbouiCOM.EditText oEditBoxDescriptn = (SAPbouiCOM.EditText)oForm.Items.Item("txtDescrip").Specific;

                SAPbouiCOM.EditText oEditBoxName = (SAPbouiCOM.EditText)oForm.Items.Item("txtRawName").Specific;
                SAPbouiCOM.EditText oEditBoxCode = (SAPbouiCOM.EditText)oForm.Items.Item("txtRawCode").Specific;

                SAPbobsCOM.Recordset rsItem = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string strQry = "",sequence ="";

                //sequence = gen.GetNextItemCode(oComboGroup.Selected.Value + oComboSubGroup.Selected.Value, oComboItem.Selected.Value + "-" + oComboGroup.Selected.Value + oComboSubGroup.Selected.Value);
                sequence = gen.GetNextItemCode(oComboGroup.Selected.Value + oComboSubGroup.Selected.Value, oComboItem.Selected.Value + "-" +  oComboSubGroup.Selected.Value);

                //-------------- modified by Tamizh-------------------------//
                //string itemCode = oComboItem.Selected.Value + "-" + oComboGroup.Selected.Value +oComboSubGroup.Selected.Value + "-" + sequence;
                //string itemName = oComboItem.Selected.Description + "-" + oComboGroup.Selected.Description + "-" + oComboSubGroup.Selected.Description + "-" + oEditBoxDescriptn.Value;
                string itemCode = oComboItem.Selected.Value + "-"+ oComboSubGroup.Selected.Value + "-" + sequence;
                string itemName = oComboItem.Selected.Description + "-" + oComboSubGroup.Selected.Description + "-" + oEditBoxDescriptn.Value;
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

                SAPbouiCOM.ComboBox oComboGroup = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbRawGrp").Specific;
                SAPbouiCOM.ComboBox oComboSubGroup = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbRSubGrp").Specific;
                SAPbouiCOM.EditText oEditBoxDescriptn = (SAPbouiCOM.EditText)PForm.Items.Item("txtDescrip").Specific;
                SAPbouiCOM.ComboBox oComboItemGroup = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbRItgp").Specific;
                SAPbouiCOM.CheckBox oCbxMix = (SAPbouiCOM.CheckBox)PForm.Items.Item("cbxMix").Specific;
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbRwClass").Specific;

                SAPbouiCOM.EditText oEditBoxName = (SAPbouiCOM.EditText)PForm.Items.Item("txtRawName").Specific;
                SAPbouiCOM.EditText oEditBoxCode = (SAPbouiCOM.EditText)PForm.Items.Item("txtRawCode").Specific;

                MItemMasterData.Instance.InitializeCombo(oComboGroup);
                MItemMasterData.Instance.InitializeCombo(oComboSubGroup);
                MItemMasterData.Instance.InitializeCombo(oComboItemGroup);

                oEditBoxCode.Value = "";
                oEditBoxName.Value = "";
                oEditBoxDescriptn.Value = "";
                oComboItem.Select("5", SAPbouiCOM.BoSearchKey.psk_ByValue);
                oCbxMix.Checked = false;
            }

            catch
            {
                Global.SapApplication.StatusBar.SetText("Error!!", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

            }
        }
         #endregion
        #region Define Group
        public void DefineRawGrp()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbRawGrp").Specific;
                if (oComboItem.Value.ToString() == "")
                {
                    MItemMasterData.Instance.DifineNew("RAWGROUP",oForm.UniqueID);

                }
                else if (oComboItem.Selected.Value == "-999")
                {
                    MItemMasterData.Instance.DifineNew("RAWGROUP",oForm.UniqueID);
                }

            }

            catch { }
        }
        public void DefineRawSubGrp()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbRSubGrp").Specific;
                if (oComboItem.Value.ToString() == "")
                {
                    MItemMasterData.Instance.DifineNew("RAWSUBGROUP",oForm.UniqueID);

                }
                else if (oComboItem.Selected.Value == "-999")
                {
                    MItemMasterData.Instance.DifineNew("RAWSUBGROUP",oForm.UniqueID);
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
            SAPbouiCOM.ComboBox oComboGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbRawGrp").Specific;
            SAPbouiCOM.ComboBox oComboSubGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbRSubGrp").Specific;

            SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbRwClass").Specific;
            SAPbouiCOM.CheckBox oCbxMix = (SAPbouiCOM.CheckBox)oForm.Items.Item("cbxMix").Specific;


            if (ComboName == "RAWGROUP")
            {
                gen.FillCombo(oForm, oComboGroup, "@RAWGROUP", "Code", "Name", true, true);
                oComboGroup.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }
            else if (ComboName == "RAWSUBGROUP")
            {
                gen.FillCombo(oForm, oComboSubGroup, "@RAWSUBGROUP", "Code", "Name", true, true);
                oComboSubGroup.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }

            oComboItem.Select("5", SAPbouiCOM.BoSearchKey.psk_ByValue);
            oCbxMix.Checked = false;

        }
          #endregion
       
    }
}

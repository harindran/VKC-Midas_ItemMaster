using System;
using System.Collections.Generic;
using System.Text;

namespace VKC
{
    class MFixedAssets
    {
          General gen = new General();

        #region Singleton

        private static MFixedAssets instance;

        public  static  MFixedAssets Instance
        {
            get
            {
                if (instance == null) instance = new MFixedAssets();

                return instance;
            }
        }

        #endregion

        public MFixedAssets()
        {
            VFixedAssests vw = VFixedAssests.Instance;
        }

        public void Classification()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
               // oForm.Freeze(true);
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbClassi").Specific;
                oComboItem.Select("1", SAPbouiCOM.BoSearchKey.psk_ByValue);
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

                // oForm.Freeze(true);
                SAPbouiCOM.ComboBox oComboClass = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbClassi").Specific;
                SAPbouiCOM.ComboBox oCombogroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbAstGrp").Specific;
                SAPbouiCOM.ComboBox oComboCat1 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbCat1").Specific;
                SAPbouiCOM.ComboBox oComboCat2 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbCat2").Specific;
                SAPbouiCOM.ComboBox oComboCat3 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbCat3").Specific;
                SAPbouiCOM.ComboBox oComboCat4 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbCat4").Specific;
                SAPbouiCOM.ComboBox oComboItemGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbFItgp").Specific;

                gen.FillCombo(oForm, oComboClass, "@CLASSIFICATION", "Code", "Name", true, true);
                gen.FillCombo(oForm, oCombogroup, "@GROUP", "Code", "Name", true, true);
                gen.FillCombo(oForm, oComboCat1, "@CATEGORY1", "Code", "Name", true, true);
                gen.FillCombo(oForm, oComboCat2, "@CATEGORY2", "Code", "Name", true, true);
                gen.FillCombo(oForm, oComboCat3, "@CATEGORY3", "Code", "Name", true, true);
                gen.FillCombo(oForm, oComboCat4, "@CATEGORY4", "Code", "Name", true, true);
                gen.FillCombo(oComboItemGroup, true);

                ////SAPbouiCOM.EditText oEditBoxCode = (SAPbouiCOM.EditText)oForm.Items.Item("txtAstCode").Specific;
                ////SAPbouiCOM.EditText oEditBoxName = (SAPbouiCOM.EditText)oForm.Items.Item("txtAstName").Specific;

                oComboClass.Select("1", SAPbouiCOM.BoSearchKey.psk_ByValue);
                oCombogroup.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                oComboCat1.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                oComboCat2.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                oComboCat3.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                oComboCat4.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                ////oEditBoxCode.Value = "";
                ////oEditBoxName.Value = "";

                // oForm.Freeze(false);


            }

            catch { } //oForm.Freeze(false); }

        }
        #endregion
        #region Validations
        public bool Validation()
        {
            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
            try
            {
              

                SAPbouiCOM.ComboBox oComboClass = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbClassi").Specific;
                SAPbouiCOM.ComboBox oCombogroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbAstGrp").Specific;
                SAPbouiCOM.ComboBox oComboCat1 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbCat1").Specific;
                SAPbouiCOM.ComboBox oComboCat2 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbCat2").Specific;
                SAPbouiCOM.ComboBox oComboCat3 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbCat3").Specific;
                SAPbouiCOM.ComboBox oComboCat4 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbCat4").Specific;
                SAPbouiCOM.ComboBox oComboItemGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbFItgp").Specific;

                if (oComboClass.Selected.Value != "1")
                {
                    Global.SapApplication.StatusBar.SetText("Classification Not Correct !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    return false;
                }
                else if (oCombogroup.Selected.Value.Trim() == "-1" || oCombogroup.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Group !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    return false;

                }
                else if (oComboCat1.Selected.Value.Trim() == "-1" || oComboCat1.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Sub Group !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    return false;

                }
                else if (oComboCat2.Selected.Value.Trim() == "-1" || oComboCat2.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Category 2!!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    return false;

                }
                else if (oComboCat3.Selected.Value.Trim() == "-1" || oComboCat3.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Category 3 !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    return false;

                }
                else if (oComboCat4.Selected.Value.Trim() == "-1" || oComboCat4.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Category 4 !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    return false;

                }
                else if (oComboItemGroup.Value == "-1" || oComboItemGroup.Value == "")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Item Group !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    return false;

                }


            }
            catch {

                return false; }
            return true;
        }
         #endregion
        #region Sub Group Combo
        public void FillSubGroupCombo()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                oForm.Freeze(true);
                SAPbouiCOM.ComboBox oCombogroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbAstGrp").Specific;
                SAPbouiCOM.ComboBox oComboCat1 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbCat1").Specific;
                string code= oCombogroup.Selected.Value;
                gen.FillCombo(oForm, oComboCat1, "@CATEGORY1", "Code", "Name", " where [U_Group]='" + code + "'", true,true);
                oComboCat1.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                oForm.Freeze(false);
            }
            catch { }
        }
        #endregion
        #region Generate Code
        public void GenerateCode()
        {
            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
            try
            {
                SAPbobsCOM.Recordset rsItem = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                SAPbobsCOM.Recordset rsItem1 = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string strQry = "", strCat1Code = "", strCat4Code = "" ;
                
                oForm.Freeze(true);
                SAPbouiCOM.ComboBox oComboClass = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbClassi").Specific;
                SAPbouiCOM.ComboBox oCombogroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbAstGrp").Specific;
                SAPbouiCOM.ComboBox oComboCat1 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbCat1").Specific;
                SAPbouiCOM.ComboBox oComboCat2 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbCat2").Specific;
                SAPbouiCOM.ComboBox oComboCat3 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbCat3").Specific;
                SAPbouiCOM.ComboBox oComboCat4 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbCat4").Specific;

                SAPbouiCOM.EditText oEditBoxCode = (SAPbouiCOM.EditText)oForm.Items.Item("txtAstCode").Specific;
                SAPbouiCOM.EditText oEditBoxName = (SAPbouiCOM.EditText)oForm.Items.Item("txtAstName").Specific;

               
                string itemCode = oComboClass.Selected.Value + "-" + oComboCat1.Selected.Value + "-" + oComboCat2.Selected.Value + oComboCat3.Selected.Value + "-" + oComboCat4.Selected.Value;
                string itemName = oComboClass.Selected.Description + "-" + oCombogroup.Selected.Description + "-" + oComboCat1.Selected.Description + "-" + oComboCat2.Selected.Description + "-" + oComboCat3.Selected.Description + "-" + oComboCat4.Selected.Description;
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

                SAPbouiCOM.ComboBox oCombogroup = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbAstGrp").Specific;
                SAPbouiCOM.ComboBox oComboCat1 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbCat1").Specific;
                SAPbouiCOM.ComboBox oComboCat2 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbCat2").Specific;
                SAPbouiCOM.ComboBox oComboCat3 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbCat3").Specific;
                SAPbouiCOM.ComboBox oComboCat4 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbCat4").Specific;
                SAPbouiCOM.ComboBox oComboItemGroup = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbFItgp").Specific;


                ////SAPbouiCOM.EditText oEditBoxCode = (SAPbouiCOM.EditText)PForm.Items.Item("txtAstCode").Specific;
                ////SAPbouiCOM.EditText oEditBoxName = (SAPbouiCOM.EditText)PForm.Items.Item("txtAstName").Specific;

                MItemMasterData.Instance.InitializeCombo(oCombogroup);
                MItemMasterData.Instance.InitializeCombo(oComboCat1);
                MItemMasterData.Instance.InitializeCombo(oComboCat2);
                MItemMasterData.Instance.InitializeCombo(oComboCat3);
                MItemMasterData.Instance.InitializeCombo(oComboCat4);
                MItemMasterData.Instance.InitializeCombo(oComboItemGroup);

                ////oEditBoxCode.Value = "";
                ////oEditBoxName.Value = "";
            }

            catch
            {
                Global.SapApplication.StatusBar.SetText("Error!!", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

            }
        }
         #endregion
        #region Define New
        public void DefineGroup()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbAstGrp").Specific;
                if (oComboItem.Value.ToString() == "")
                {
                    MItemMasterData.Instance.DifineNew("GROUP",oForm.UniqueID);

                }
                else if (oComboItem.Selected.Value == "-999")
                {
                    MItemMasterData.Instance.DifineNew("GROUP",oForm.UniqueID);
                }

            }

            catch { }
        }
       
        public void DefineCategory1()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbCat1").Specific;
                if (oComboItem.Value.ToString() == "")
                {
                    MItemMasterData.Instance.DifineNew("CATEGORY1",oForm.UniqueID);

                }
                else if (oComboItem.Selected.Value == "-999")
                {
                    MItemMasterData.Instance.DifineNew("CATEGORY1",oForm.UniqueID);
                }

            }

            catch { }
        }
        public void DefineCategory2()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbCat2").Specific;
                if (oComboItem.Value.ToString() == "")
                {
                    MItemMasterData.Instance.DifineNew("CATEGORY2",oForm.UniqueID);

                }
                else if (oComboItem.Selected.Value == "-999")
                {
                    MItemMasterData.Instance.DifineNew("CATEGORY2",oForm.UniqueID);
                }

            }

            catch { }
        }
        public void DefineCategory3()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbCat3").Specific;
                if (oComboItem.Value.ToString() == "")
                {
                    MItemMasterData.Instance.DifineNew("CATEGORY3",oForm.UniqueID);

                }
                else if (oComboItem.Selected.Value == "-999")
                {
                    MItemMasterData.Instance.DifineNew("CATEGORY3",oForm.UniqueID);
                }

            }

            catch { }
        }
        public void DefineCategory4()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbCat4").Specific;
                if (oComboItem.Value.ToString() == "")
                {
                    MItemMasterData.Instance.DifineNew("CATEGORY4",oForm.UniqueID);

                }
                else if (oComboItem.Selected.Value == "-999")
                {
                    MItemMasterData.Instance.DifineNew("CATEGORY4",oForm.UniqueID);
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
            SAPbouiCOM.ComboBox oCombogroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbAstGrp").Specific;
            SAPbouiCOM.ComboBox oComboCat1 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbCat1").Specific;
            SAPbouiCOM.ComboBox oComboCat2 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbCat2").Specific;
            SAPbouiCOM.ComboBox oComboCat3 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbCat3").Specific;
            SAPbouiCOM.ComboBox oComboCat4 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbCat4").Specific;


            if (ComboName == "GROUP")
            {
                gen.FillCombo(oForm, oCombogroup, "@GROUP", "Code", "Name", true, true);
                oCombogroup.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }
            else if (ComboName == "CATEGORY1")
            {
                gen.FillCombo(oForm, oComboCat1, "@CATEGORY1", "Code", "Name", true, true);
                oComboCat1.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }
            else if (ComboName == "CATEGORY2")
            {
                gen.FillCombo(oForm, oComboCat2, "@CATEGORY2", "Code", "Name", true, true);
                oComboCat2.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }
            else if (ComboName == "CATEGORY3")
            {
                gen.FillCombo(oForm, oComboCat3, "@CATEGORY3", "Code", "Name", true, true);
                oComboCat3.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }
            else if (ComboName == "CATEGORY4")
            {
                gen.FillCombo(oForm, oComboCat4, "@CATEGORY4", "Code", "Name", true, true);
                oComboCat4.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }

        }
         #endregion
       
    }
}

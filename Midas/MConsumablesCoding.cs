using System;
using System.Collections.Generic;
using System.Text;

namespace VKC
{
    class MConsumablesCoding
    {
         General gen = new General();

        #region Singleton

        private static MConsumablesCoding instance;

        public  static  MConsumablesCoding Instance
        {
            get
            {
                if (instance == null) instance = new MConsumablesCoding();

                return instance;
            }
        }

        #endregion

        public MConsumablesCoding()
        {
            VConsumableCoding vw = VConsumableCoding.Instance;
        }
        public void Classification()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
               // oForm.Freeze(true);
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbCsClass").Specific;
                oComboItem.Select("6", SAPbouiCOM.BoSearchKey.psk_ByValue);
               // oForm.Freeze(false);

            }
            catch { }
        }
        #region Refresh Combo Box
        public void RefreshCombos(string FormID,string ComboName)
        {
            SAPbouiCOM.Form CForm = Global.SapApplication.Forms.ActiveForm;
            SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(FormID);
            SAPbouiCOM.ComboBox oComboGroup = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbConGrp").Specific;
            SAPbouiCOM.ComboBox oComboSub = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSubCon").Specific;
            if (ComboName == "CONGROUP")
            {
                gen.FillCombo(PForm, oComboGroup, "@CONGROUP", "Code", "Name", true, true);
                oComboGroup.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }
            else if (ComboName == "CONSUBGROUP")
            {
                gen.FillCombo(PForm, oComboSub, "@CONSUBGROUP", "Code", "Name", true, true);
                oComboSub.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }
            CForm.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE;
        }
        #endregion
        public void GetCombos()
        {
            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
            try
            {
                
                oForm.Freeze(true);

                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbCsClass").Specific;
                SAPbouiCOM.ComboBox oComboGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbConGrp").Specific;
                SAPbouiCOM.ComboBox oComboSub = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSubCon").Specific;
                SAPbouiCOM.ComboBox oComboItemGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbConItgp").Specific;

                SAPbouiCOM.EditText oEditDescription = (SAPbouiCOM.EditText)oForm.Items.Item("txtConDesc").Specific;
                SAPbouiCOM.EditText oEditBoxName = (SAPbouiCOM.EditText)oForm.Items.Item("txtConName").Specific;
                SAPbouiCOM.EditText oEditBoxCode = (SAPbouiCOM.EditText)oForm.Items.Item("txtConCode").Specific;

                gen.FillCombo(oForm, oComboItem, "@CLASSIFICATION", "Code", "Name", true, true);
                gen.FillCombo(oForm, oComboGroup, "@CONGROUP", "Code", "Name", true, true);
                if( oComboGroup.Value == "CM" || oComboGroup.Value == "CA")
                gen.FillCombo(oForm, oComboSub, "@CONSUBGROUP", "Code", "Name","Where Code ='OTH'", true, true);
                else
                gen.FillCombo(oForm, oComboSub, "@CONSUBGROUP", "Code", "Name", true, true);

                gen.FillCombo(oComboItemGroup, true);

                oComboGroup.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                oComboSub.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

                oComboItem.Select("6", SAPbouiCOM.BoSearchKey.psk_ByValue);
                oEditDescription.Value = "";
                oEditBoxName.Value = "";
                oEditBoxCode.Value = "";
                oForm.Freeze(false);


            }

            catch { oForm.Freeze(false); }

        }
        public void GetSubGroup()
        {
            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
            try
            {

                oForm.Freeze(true);

                SAPbouiCOM.ComboBox oComboGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbConGrp").Specific;
                SAPbouiCOM.ComboBox oComboSub = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSubCon").Specific;

                if (oComboGroup.Value.Trim() == "CM" || oComboGroup.Value.Trim() == "CA")
                    gen.FillCombo(oForm, oComboSub, "@CONSUBGROUP", "Code", "Name", "Where Code ='OTH'", true, true);
                else
                    gen.FillCombo(oForm, oComboSub, "@CONSUBGROUP", "Code", "Name", true, true);

                oComboSub.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

                oForm.Freeze(false);


            }

            catch { oForm.Freeze(false); }

        }

        # region Validations
        public bool Validation()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
               
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbCsClass").Specific;
                SAPbouiCOM.ComboBox oComboGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbConGrp").Specific;
                SAPbouiCOM.ComboBox oComboSubGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSubCon").Specific;
                SAPbouiCOM.EditText oEditDescription = (SAPbouiCOM.EditText)oForm.Items.Item("txtConDesc").Specific;
                SAPbouiCOM.ComboBox oComboItemGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbConItgp").Specific;

                if (oComboItem.Selected.Value != "6")
                {
                    Global.SapApplication.StatusBar.SetText("Classification Not Correct !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                   
                    return false;
                }
                else if (oComboGroup.Selected.Value.Trim() == "-1" || oComboGroup.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Group !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                   
                    return false;

                }
                //else if (oComboSubGroup.Selected.Value.Trim() == "-1" ||oComboSubGroup.Selected.Value.Trim() == "-999")
                //{
                //    //Global.SapApplication.StatusBar.SetText("Please Select Sub Group !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                //    //return false;

                //}
                else if (oEditDescription.Value == "")
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
        #region ClearCombo
        public void ClearCombo(SAPbouiCOM.ItemEvent val)
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.Item(val.FormUID);
                SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(oForm.DataSources.UserDataSources.Item("PFormID").Value);

                SAPbouiCOM.ComboBox oComboGroup = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbConGrp").Specific;
                SAPbouiCOM.ComboBox oComboSubGroup = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSubCon").Specific;
                SAPbouiCOM.EditText oEditDescription = (SAPbouiCOM.EditText)PForm.Items.Item("txtConDesc").Specific;
                SAPbouiCOM.ComboBox oComboItemGroup = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbConItgp").Specific;

                SAPbouiCOM.EditText oEditBoxName = (SAPbouiCOM.EditText)PForm.Items.Item("txtConName").Specific;
                SAPbouiCOM.EditText oEditBoxCode = (SAPbouiCOM.EditText)PForm.Items.Item("txtConCode").Specific;
                MItemMasterData.Instance.InitializeCombo(oComboGroup);
                MItemMasterData.Instance.InitializeCombo(oComboSubGroup);
                MItemMasterData.Instance.InitializeCombo(oComboItemGroup);

                oEditBoxCode.Value = "";
                oEditBoxName.Value = "";
                oEditDescription.Value = "";
            }

            catch
            {
                Global.SapApplication.StatusBar.SetText("Error!!", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

            }
        }
               #endregion
        #region Code Generation
        public void GenerateCode()
        {
            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
            try
            {
                
                oForm.Freeze(true);
                string sequence = "";
                string SubGroup ="";
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbCsClass").Specific;
                SAPbouiCOM.ComboBox oComboGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbConGrp").Specific;
                SAPbouiCOM.ComboBox oComboSubGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSubCon").Specific;
                SAPbouiCOM.EditText oEditDescription = (SAPbouiCOM.EditText)oForm.Items.Item("txtConDesc").Specific;

                SAPbouiCOM.EditText oEditBoxName = (SAPbouiCOM.EditText)oForm.Items.Item("txtConName").Specific;
                SAPbouiCOM.EditText oEditBoxCode = (SAPbouiCOM.EditText)oForm.Items.Item("txtConCode").Specific;
                if (oComboSubGroup.Selected.Value.Trim() == "-1" || oComboSubGroup.Selected.Value.Trim() == "-999")
                {
                    SubGroup = "";
                }
                 else
                {
                        SubGroup=oComboSubGroup.Selected.Value;
                 }                

                //sequence = gen.GetNextItemCode(oComboGroup.Selected.Value+oComboSubGroup.Selected.Value);
                sequence = gen.GetNextItemCode(oComboGroup.Selected.Value + SubGroup);

                //string itemCode = oComboItem.Selected.Value + "-" + oComboGroup.Selected.Value + "-" + oComboSubGroup.Selected.Value + "-" + sequence ;
                //string itemName = oComboItem.Selected.Description + "-" + oComboGroup.Selected.Description + "-" + oComboSubGroup.Selected.Description + "-" + oEditDescription.Value;
                string itemCode = "";
                string itemName = "";
                if (SubGroup == "")
                {
                    itemCode = oComboItem.Selected.Value + "-" + oComboGroup.Selected.Value + "-" + sequence;
                    itemName = oComboItem.Selected.Description + "-" + oComboGroup.Selected.Description + "-" + oEditDescription.Value;
                }
                else
                {
                    itemCode = oComboItem.Selected.Value + "-" + oComboGroup.Selected.Value + "-" + SubGroup + "-" + sequence;
                    itemName = oComboItem.Selected.Description + "-" + oComboGroup.Selected.Description + "-" + oComboSubGroup.Selected.Description.Trim() + "-" + oEditDescription.Value;
                }
                               
                oEditBoxName.Value = itemName;
                oEditBoxCode.Value = itemCode;
                oForm.Freeze(false);
            }
            catch { oForm.Freeze(false); }
        }
        #endregion
        #region Define New
        public void DefineGroup()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
              
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbConGrp").Specific;
                if (oComboItem.Value.ToString() == "")
                {
                    MItemMasterData.Instance.DifineNew("CONGROUP" ,oForm.UniqueID);

                }

                else if (oComboItem.Selected.Value == "-999")
                {
                    MItemMasterData.Instance.DifineNew("CONGROUP",oForm.UniqueID);
                }
             

            }

            catch { }
        }
        public void DefineSubGroup()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSubCon").Specific;
                if (oComboItem.Value.ToString() == "")
                {
                    MItemMasterData.Instance.DifineNew("CONSUBGROUP",oForm.UniqueID);

                }
                else 
                if (oComboItem.Selected.Value == "-999")
                {
                    MItemMasterData.Instance.DifineNew("CONSUBGROUP",oForm.UniqueID);
                }

            }

            catch { }
        }
        #endregion
       
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace VKC
{
    class MScrapCoding
    {
          General gen = new General();

        #region Singleton

        private static MScrapCoding instance;

        public  static  MScrapCoding Instance
        {
            get
            {
                if (instance == null) instance = new MScrapCoding();

                return instance;
            }
        }

        #endregion

        public MScrapCoding()
        {
            VScrapCoding vw = VScrapCoding.Instance;
        }
        public void Classification()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
               // oForm.Freeze(true);
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSpClass").Specific;
                oComboItem.Select("8", SAPbouiCOM.BoSearchKey.psk_ByValue);
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
               SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSpClass").Specific;
               SAPbouiCOM.ComboBox oComboItemGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSrpItgp").Specific;

                 SAPbouiCOM.ComboBox oComboGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSrpGrp").Specific;
                SAPbouiCOM.EditText txtDescrip = (SAPbouiCOM.EditText)oForm.Items.Item("txtDescri2").Specific;
               
               gen.FillCombo(oForm, oComboItem, "@CLASSIFICATION", "Code", "Name", true, true);
               gen.FillCombo(oComboItemGroup, true);

               gen.FillCombo(oForm, oComboGroup, "@SCRAPGROUP", "Code", "Name", true, true);

                SAPbouiCOM.EditText oEditBoxName = (SAPbouiCOM.EditText)oForm.Items.Item("txtSrpName").Specific;
                SAPbouiCOM.EditText oEditBoxCode = (SAPbouiCOM.EditText)oForm.Items.Item("txtSrpCode").Specific;

               oComboItem.Select("8", SAPbouiCOM.BoSearchKey.psk_ByValue);
               oComboGroup.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
               oEditBoxName.Value = "";
               oEditBoxCode.Value = "";
               txtDescrip.Value = "";
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

                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSpClass").Specific;
                SAPbouiCOM.ComboBox oComboGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSrpGrp").Specific;
                SAPbouiCOM.EditText txtDescrip = (SAPbouiCOM.EditText)oForm.Items.Item("txtDescri2").Specific;
                SAPbouiCOM.ComboBox oComboItemGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSrpItgp").Specific;

                if (oComboItem.Selected.Value != "8")
                {
                    Global.SapApplication.StatusBar.SetText("Classification Not Correct !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    return false;
                }
                else if (oComboGroup.Selected.Value.Trim() == "-1" || oComboGroup.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Group !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    return false;

                }
                else if (txtDescrip.Value == "")
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

        #region Generate Code
        public void GenerateCode()
        {
            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;

            try
            {
                oForm.Freeze(true);
                string Sequence = "";
               SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSpClass").Specific;
               // SAPbouiCOM.ComboBox oComboDescr = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbDescrip").Specific;
               SAPbouiCOM.ComboBox oComboGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSrpGrp").Specific;
                SAPbouiCOM.EditText txtDescrip = (SAPbouiCOM.EditText)oForm.Items.Item("txtDescri2").Specific;

                SAPbouiCOM.EditText oEditBoxName = (SAPbouiCOM.EditText)oForm.Items.Item("txtSrpName").Specific;
                SAPbouiCOM.EditText oEditBoxCode = (SAPbouiCOM.EditText)oForm.Items.Item("txtSrpCode").Specific;

                string strQry = "SELECT ISNULL(MAX(CAST (SUBSTRING(ItemCode,LEN(ItemCode) -3,4) AS NUMERIC)),0)+1 FROM OITM WHERE U_GrpCode= '" + oComboGroup.Value+ "'";
                SAPbobsCOM.Recordset rsNextCode = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                rsNextCode.DoQuery(strQry);
                if (rsNextCode.EoF || rsNextCode.RecordCount == 0)
                    Sequence = "0001";
                else 
                    Sequence = rsNextCode.Fields.Item(0).Value.ToString().PadLeft(4, '0');



                string itemCode = oComboItem.Selected.Value + "-" + oComboGroup.Selected.Value + "-"+ Sequence;
                string itemName = oComboItem.Selected.Description + "-" + oComboGroup.Selected.Description + "-" + txtDescrip.Value;
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

              //  SAPbouiCOM.ComboBox oComboDescr = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbDescrip").Specific;
                SAPbouiCOM.EditText txtDescrip = (SAPbouiCOM.EditText)PForm.Items.Item("txtDescri2").Specific;
                SAPbouiCOM.ComboBox oComboItemGroup = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSrpItgp").Specific;
                SAPbouiCOM.ComboBox oComboGroup = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSrpGrp").Specific;

                SAPbouiCOM.EditText oEditBoxName = (SAPbouiCOM.EditText)PForm.Items.Item("txtSrpName").Specific;
                SAPbouiCOM.EditText oEditBoxCode = (SAPbouiCOM.EditText)PForm.Items.Item("txtSrpCode").Specific;

                MItemMasterData.Instance.InitializeCombo(oComboGroup);
                MItemMasterData.Instance.InitializeCombo(oComboItemGroup);
                txtDescrip.Value = "";
                oEditBoxCode.Value = "";
                oEditBoxName.Value = "";
              
            }

            catch
            {
                Global.SapApplication.StatusBar.SetText("Error!!", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

            }
        }
         #endregion
        #region Define Scrap
        public void DefineScrap()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                //  oForm.Freeze(true);
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSrpGrp").Specific;
                if (oComboItem.Value.ToString() == "")
                {
                    MItemMasterData.Instance.DifineNew("SCRAPGROUP", oForm.UniqueID);
                    SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;
                    string abc = frm.TypeEx;
                    gen.FillCombo(oForm, oComboItem, "@SCRAPGROUP", "Code", "Name", true, true);

                }
                else if (oComboItem.Selected.Value == "-999")
                {
                    MItemMasterData.Instance.DifineNew("SCRAPGROUP", oForm.UniqueID);
                    SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;

                    gen.FillCombo(oForm, oComboItem, "@SCRAPGROUP", "Code", "Name", true, true);
                }


                // oForm.Freeze(false);
            }

            catch { }
        }
        #endregion

        #region Refresh Combo Box
        public void RefreshCombos(string FormID, string ComboName)
        {
            SAPbouiCOM.Form CForm = Global.SapApplication.Forms.ActiveForm;
            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.Item(FormID);
            SAPbouiCOM.ComboBox oComboDescr = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSrpGrp").Specific;


            if (ComboName == "SCRAP")
            {
                gen.FillCombo(oForm, oComboDescr, "@SCRAPGROUP", "Code", "Name", true, true);
                oComboDescr.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }



        }
        #endregion
    }
}

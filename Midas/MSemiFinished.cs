using System;
using System.Collections.Generic;
using System.Text;

namespace VKC   
{
    class MSemiFinished
    {
          General gen = new General();

        #region Singleton

        private static MSemiFinished instance;

        public  static  MSemiFinished Instance
        {
            get
            {
                if (instance == null) instance = new MSemiFinished();

                return instance;
            }
        }

        #endregion

        public MSemiFinished()
        {
            VSemiFinished vw = VSemiFinished.Instance;
        }
        public void Classification()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
               // oForm.Freeze(true);
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmClass").Specific;
                oComboItem.Select("4", SAPbouiCOM.BoSearchKey.psk_ByValue);
               // oForm.Freeze(false);

            }
            catch { }
        }
        ////#region FillModel Combo
        ////public void FillModelCombo()
        ////{
        ////    try
        ////    {
        ////        SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
        ////        SAPbouiCOM.ComboBox oComboBrand = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSBrand").Specific;
        ////        SAPbouiCOM.ComboBox oComboModel = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSModel").Specific;
        ////        string code = oComboBrand.Selected.Value;
        ////        gen.FillCombo(oForm, oComboModel, "@MODEL", "Code", "Name", " where [U_Brand]='" + code + "'", true, true);
        ////        oComboModel.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

        ////    }
        ////    catch { }
        ////}
        ////#endregion
        #region GetCombos
        public void GetCombos()
        {
            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;

            try
            {
                // oForm.Freeze(true);
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmClass").Specific;
                SAPbouiCOM.ComboBox oComboBrand = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSBrand").Specific;
                SAPbouiCOM.ComboBox oComboSide = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmSide").Specific;
                SAPbouiCOM.ComboBox oComboGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSemGrp").Specific;
                SAPbouiCOM.ComboBox oComboColor = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSemClr").Specific;
                SAPbouiCOM.ComboBox oComboSizeId = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmSzeId").Specific;
                SAPbouiCOM.ComboBox oComboSize = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSemSize").Specific;
                SAPbouiCOM.ComboBox oComboModel = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSModel").Specific;
                SAPbouiCOM.ComboBox oComboItemGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmItgp").Specific;

                SAPbouiCOM.EditText oEditBoxName = (SAPbouiCOM.EditText)oForm.Items.Item("txtSemName").Specific;
                SAPbouiCOM.EditText oEditBoxCode = (SAPbouiCOM.EditText)oForm.Items.Item("txtSemCode").Specific;

                gen.FillCombo(oForm, oComboItem, "@CLASSIFICATION", "Code", "Name", true, true);
                gen.FillCombo(oForm,oComboBrand, "@BRAND", "Code", "Name", true, true);
                gen.FillCombo(oForm, oComboSide, "@SIDE", "Code", "Name", true, true);
                gen.FillCombo(oForm, oComboGroup, "@SEMIGROUP", "Code", "Name", true, true);
                gen.FillCombo(oForm, oComboColor, "@COLOR", "Code", "Name", true, true);
                gen.FillCombo(oForm, oComboSizeId, "@SIZECAT", "Code", "Name", true, true);
                gen.FillCombo(oForm, oComboSize, "@SIZESMALL", "Code", "Name", true, true);
                gen.FillCombo(oForm, oComboModel, "@MODEL", "Code", "Name", true, true);
                gen.FillCombo(oComboItemGroup, true);
                oComboItem.Select("4", SAPbouiCOM.BoSearchKey.psk_ByValue);
                oComboSide.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                oComboGroup.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                oComboColor.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                oComboSizeId.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                oComboSize.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                oComboModel.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                oComboBrand.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                oEditBoxCode.Value = "";
                oEditBoxName.Value = "";
                // oForm.Freeze(false);


            }

            catch { }//oForm.Freeze(false); }

        }
        #endregion
        #region Validations
        public bool Validation()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;

                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmClass").Specific;
                SAPbouiCOM.ComboBox oComboGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSemGrp").Specific;
                SAPbouiCOM.ComboBox oComboModel = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSModel").Specific;
                SAPbouiCOM.ComboBox oComboColor = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSemClr").Specific;
                SAPbouiCOM.ComboBox oComboSizeId = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmSzeId").Specific;
                SAPbouiCOM.ComboBox oComboSize = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSemSize").Specific;
                SAPbouiCOM.ComboBox oComboSide = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmSide").Specific;
                SAPbouiCOM.ComboBox oComboBrand = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSBrand").Specific;
                SAPbouiCOM.ComboBox oComboItemGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmItgp").Specific;

                if (oComboItem.Selected.Value != "4")
                {
                    Global.SapApplication.StatusBar.SetText("Classification Not Correct !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    return false;
                }
                else if (oComboGroup.Selected.Value.Trim() == "-1" || oComboGroup.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Group !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    return false;

                }
                else if (oComboBrand.Selected.Value.Trim() == "-1" || oComboBrand.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Brand !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return false;

                }
                else if (oComboModel.Selected.Value.Trim() == "-1" || oComboModel.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Model !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return false;

                }
                else if (oComboColor.Selected.Value.Trim() == "-1" || oComboColor.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Color !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return false;

                }
                else if (oComboSizeId.Selected.Value.Trim() == "-1" || oComboSizeId.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Size Id !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return false;

                }
                else if (oComboSize.Selected.Value.Trim() == "-1" || oComboSize.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Size !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return false;

                }
                else if (oComboSide.Selected.Value.Trim() == "-1" || oComboSide.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Side !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
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
                string Sequence = "", strGroupCode ="" ,strCheckCode="";
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmClass").Specific;
                SAPbouiCOM.ComboBox oComboSide = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmSide").Specific;
                SAPbouiCOM.ComboBox oComboGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSemGrp").Specific;
                SAPbouiCOM.ComboBox oComboColor = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSemClr").Specific;
                SAPbouiCOM.ComboBox oComboSizeId = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmSzeId").Specific;
                SAPbouiCOM.ComboBox oComboSize = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSemSize").Specific;
                SAPbouiCOM.ComboBox oComboModel = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSModel").Specific;

                SAPbouiCOM.EditText oEditBoxName = (SAPbouiCOM.EditText)oForm.Items.Item("txtSemName").Specific;
                SAPbouiCOM.EditText oEditBoxCode = (SAPbouiCOM.EditText)oForm.Items.Item("txtSemCode").Specific;
                strGroupCode = oComboGroup.Selected.Value;
                strCheckCode = strGroupCode + oComboItem.Selected.Value + "-" + oComboGroup.Selected.Value + "-" + oComboModel.Selected.Value + "-" + oComboColor.Selected.Value + "-" + oComboSizeId.Selected.Value + oComboSize.Selected.Value + oComboSide.Selected.Value;
                if (strGroupCode == "CU")
                {

                    // string strQry = "SELECT ISNULL((MAX(U_Sequence)),0)+1 FROM OITM WHERE U_GrpCode='" + strCheckCode + "'";
                    //string strQry = "SELECT ISNULL((MAX(RIGHT(U_GrpCode,2))),0)+1 from OITM where SUBSTRING(U_GrpCode,0,(LEN(U_GrpCode)-1)) ='" + strCheckCode + "'";
                    string strQry = "SELECT ISNULL((MAX(RIGHT(U_GrpCode,2))),0)+1 from OITM where CASE  when  LEN(U_GrpCode) >2 then SUBSTRING(U_GrpCode,0,(LEN(U_GrpCode))-2 ) else '11' end  ='" + strCheckCode + "'";

                    SAPbobsCOM.Recordset rsNextCode = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    rsNextCode.DoQuery(strQry);
                    int count = rsNextCode.RecordCount;
                    if (rsNextCode.EoF || rsNextCode.RecordCount == 0)
                        Sequence = "01";
                    else if (Convert.ToInt32(rsNextCode.Fields.Item(0).Value) < 10)
                        Sequence = rsNextCode.Fields.Item(0).Value.ToString().PadLeft(2, '0');
                    else
                        Sequence = rsNextCode.Fields.Item(0).Value.ToString();
                    oForm.DataSources.UserDataSources.Item("SMCU").ValueEx = Sequence;
                    //  Sequence = "-" + Sequence;

                }
                else
                {
                    Sequence = "01";
                }

                if (Sequence != "")
                {
                    Sequence = "-" + Sequence;
                }
                string itemCode = oComboItem.Selected.Value + "-" + oComboGroup.Selected.Value + "-" + oComboModel.Selected.Value + "-" + oComboColor.Selected.Value + "-" + oComboSizeId.Selected.Value + oComboSize.Selected.Value + oComboSide.Selected.Value + Sequence;
              
                string itemName = oComboItem.Selected.Description + "-" + oComboGroup.Selected.Description + "-" + oComboModel.Selected.Description + "-" + oComboColor.Selected.Description + "-" + oComboSizeId.Selected.Description +"-"+ oComboSize.Selected.Description + "-" + oComboSide.Selected.Description +  Sequence;
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

                SAPbouiCOM.ComboBox oComboSide = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmSide").Specific;
                SAPbouiCOM.ComboBox oComboGroup = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSemGrp").Specific;
                SAPbouiCOM.ComboBox oComboColor = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSemClr").Specific;
                SAPbouiCOM.ComboBox oComboSizeId = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmSzeId").Specific;
                SAPbouiCOM.ComboBox oComboSize = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSemSize").Specific;
                SAPbouiCOM.ComboBox oComboModel = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSModel").Specific;
                SAPbouiCOM.ComboBox oComboBrand = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSBrand").Specific;
                SAPbouiCOM.ComboBox oComboItemGroup = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmItgp").Specific;


                SAPbouiCOM.EditText oEditBoxName = (SAPbouiCOM.EditText)PForm.Items.Item("txtSemName").Specific;
                SAPbouiCOM.EditText oEditBoxCode = (SAPbouiCOM.EditText)PForm.Items.Item("txtSemCode").Specific;

                MItemMasterData.Instance.InitializeCombo(oComboSide);
                MItemMasterData.Instance.InitializeCombo(oComboGroup);
                MItemMasterData.Instance.InitializeCombo(oComboColor);
                MItemMasterData.Instance.InitializeCombo(oComboSizeId);
                MItemMasterData.Instance.InitializeCombo(oComboSize);
                MItemMasterData.Instance.InitializeCombo(oComboModel);
                MItemMasterData.Instance.InitializeCombo(oComboBrand);
                MItemMasterData.Instance.InitializeCombo(oComboItemGroup);


                oEditBoxCode.Value = "";
                oEditBoxName.Value = "";

            }

            catch
            {
                Global.SapApplication.StatusBar.SetText("Error!!", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

            }
        }
         #endregion
        #region Define New
        public void DefineSide()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmSide").Specific;
                if (oComboItem.Value.ToString() == "")
                {
                    MItemMasterData.Instance.DifineNew("SIDE",oForm.UniqueID);

                }
                if (oComboItem.Selected.Value == "-999")
                {
                    MItemMasterData.Instance.DifineNew("SIDE",oForm.UniqueID);
                }
             
            }

            catch { }
        }
     
        public void DefineSemiGroup()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSemGrp").Specific;
                if (oComboItem.Value.ToString() == "")
                {
                    MItemMasterData.Instance.DifineNew("SEMIGROUP",oForm.UniqueID);

                }
                else if (oComboItem.Selected.Value == "-999")
                {
                    MItemMasterData.Instance.DifineNew("SEMIGROUP",oForm.UniqueID);
                }

            }

            catch { }
        }
        public void DefineSemiColor()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSemClr").Specific;
                if (oComboItem.Value.ToString() == "")
                {
                    MItemMasterData.Instance.DifineNew("COLOR",oForm.UniqueID);

                }
                if (oComboItem.Selected.Value == "-999")
                {
                    MItemMasterData.Instance.DifineNew("COLOR",oForm.UniqueID);
                }

            }

            catch { }
        }
        public void DefineSizeId()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmSzeId").Specific;
                if (oComboItem.Value.ToString() == "")
                {
                    MItemMasterData.Instance.DifineNew("SIZECAT",oForm.UniqueID);

                }
                if (oComboItem.Selected.Value == "-999")
                {
                    MItemMasterData.Instance.DifineNew("SIZECAT",oForm.UniqueID);
                }

            }

            catch { }
        }
        public void DefineSize()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSemSize").Specific;
                if (oComboItem.Value.ToString() == "")
                {
                    MItemMasterData.Instance.DifineNew("SIZESMALL",oForm.UniqueID);

                }
                if (oComboItem.Selected.Value == "-999")
                {
                    MItemMasterData.Instance.DifineNew("SIZESMALL",oForm.UniqueID);
                }

            }

            catch { }
        }
        public void DefineModel()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSModel").Specific;
                if (oComboItem.Value.ToString() == "")
                {
                    MItemMasterData.Instance.DifineNew("MODEL",oForm.UniqueID);

                }
                if (oComboItem.Selected.Value == "-999")
                {
                    MItemMasterData.Instance.DifineNew("MODEL",oForm.UniqueID);
                }

            }

            catch { }
        }
        public void DefineBrand()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSBrand").Specific;
                if (oComboItem.Value.ToString() == "")
                {
                    MItemMasterData.Instance.DifineNew("BRAND", oForm.UniqueID);

                }
                if (oComboItem.Selected.Value == "-999")
                {
                    MItemMasterData.Instance.DifineNew("BRAND", oForm.UniqueID);
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
            SAPbouiCOM.ComboBox oComboSide = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmSide").Specific;
            SAPbouiCOM.ComboBox oComboGroup = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSemGrp").Specific;
            SAPbouiCOM.ComboBox oComboColor = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSemClr").Specific;
            SAPbouiCOM.ComboBox oComboSizeId = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmSzeId").Specific;
            SAPbouiCOM.ComboBox oComboSize = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSemSize").Specific;
            SAPbouiCOM.ComboBox oComboModel = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSModel").Specific;
            SAPbouiCOM.ComboBox oComboBrand = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSBrand").Specific;

            if (ComboName == "SIDE")
            {
                gen.FillCombo(oForm, oComboSide, "@SIDE", "Code", "Name", true, true);
                oComboSide.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }
            else if (ComboName == "SEMIGROUP")
            {
                gen.FillCombo(oForm, oComboGroup, "@SEMIGROUP", "Code", "Name", true, true);
                oComboGroup.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }
            else if (ComboName == "COLOR")
            {
                gen.FillCombo(oForm, oComboColor, "@COLOR", "Code", "Name", true, true);
                oComboColor.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }
            else if (ComboName == "SIZECAT")
            {
                gen.FillCombo(oForm, oComboSizeId, "@SIZECAT", "Code", "Name", true, true);
                oComboSizeId.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }
            else if (ComboName == "SIZESMALL")
            {
                gen.FillCombo(oForm, oComboSize, "@SIZESMALL", "Code", "Name", true, true);
                oComboSize.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }
            else if (ComboName == "MODEL")
            {
                gen.FillCombo(oForm, oComboModel, "@MODEL", "Code", "Name", true, true);
                oComboModel.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }
            else if (ComboName == "BRAND")
            {
                gen.FillCombo(oForm,oComboBrand, "@BRAND", "Code", "Name", true, true);
                oComboBrand.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }

        }
         #endregion
    }
}

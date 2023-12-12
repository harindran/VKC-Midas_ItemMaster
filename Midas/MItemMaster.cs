using System;
using System.Collections.Generic;
using System.Text;

namespace VKC
{
    class MItemMaster
    {
        General gen = new General();
      

        #region Singleton

        private static MItemMaster instance;

        public  static  MItemMaster Instance
        {
            get
            {
                if (instance == null) instance = new MItemMaster();

                return instance;
            }
        }

        #endregion

        public MItemMaster()
        {
            VItemMaster vw = VItemMaster.Instance;
        }
        public void Classification()
        {
            try
          {
                          SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                        //  oForm.Freeze(true);
                          SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbItem").Specific;
                          oComboItem.Select("2", SAPbouiCOM.BoSearchKey.psk_ByValue);
                        // oForm.Freeze(false);

        }
            catch{}
    }
    #region Define New
    public void DefineUnit()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbUnit").Specific;
                if (oComboItem.Value.ToString() == "")
                {
                    MItemMasterData.Instance.DifineNew("UNIT",oForm.UniqueID);

                }
                else if (oComboItem.Selected.Value == "-999")
                {
                   MItemMasterData.Instance.DifineNew("UNIT",oForm.UniqueID);
                }
                
                }           
         
            catch{}
        }
        public void DefineBrand()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbBrand").Specific;
                if (oComboItem.Value.ToString() == "")
                {
                    MItemMasterData.Instance.DifineNew("BRAND", oForm.UniqueID);

                }
                else if (oComboItem.Selected.Value == "-999")
                {
                    MItemMasterData.Instance.DifineNew("BRAND", oForm.UniqueID);
                }
            }

            catch { }
        }
        public void DefineModel()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbModel").Specific;
                if (oComboItem.Value.ToString() == "")
                {
                    MItemMasterData.Instance.DifineNew("MODEL",oForm.UniqueID);

                }
               else if (oComboItem.Selected.Value == "-999")
                {
                    MItemMasterData.Instance.DifineNew("MODEL",oForm.UniqueID);
                }
            }

            catch { }
        }
        public void DefineColor()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbColor").Specific;
                if (oComboItem.Value.ToString() == "")
                {
                    MItemMasterData.Instance.DifineNew("COLOR",oForm.UniqueID);

                }
                else if (oComboItem.Selected.Value == "-999")
                {
                    MItemMasterData.Instance.DifineNew("COLOR",oForm.UniqueID);
                }

            }

            catch { }
        }
        public void DefineSizeID()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSizeId").Specific;
                if (oComboItem.Value.ToString() == "")
                {
                    MItemMasterData.Instance.DifineNew("SIZECAT",oForm.UniqueID);

                }
                else if (oComboItem.Selected.Value == "-999")
                {
                    MItemMasterData.Instance.DifineNew("SIZECAT",oForm.UniqueID);
                }
            }

            catch { }
        }
        ////public void DefineSize()
        ////{
        ////    try
        ////    {
        ////        SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
        ////        SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSize").Specific;
        ////        if (oComboItem.Value.ToString() == "")
        ////        {
        ////            MItemMasterData.Instance.DifineNew("SIZE",oForm.UniqueID);

        ////        }
        ////        else if (oComboItem.Selected.Value == "-999")
        ////        {
        ////            MItemMasterData.Instance.DifineNew("SIZE",oForm.UniqueID);
        ////        }

        ////    }

        ////    catch { }
        ////}
        public void DefineDeliveryLoc()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbLoc").Specific;
                if (oComboItem.Value.ToString() == "")
                {
                    MItemMasterData.Instance.DifineNew("DELIVERYLOC",oForm.UniqueID);

                }
                else if (oComboItem.Selected.Value == "-999")
                {
                    MItemMasterData.Instance.DifineNew("DELIVERYLOC",oForm.UniqueID);
                }

            }

            catch { }
        }
    #endregion
#region GetCombos
        public void GetCombos()
        {
            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
            try
            {

                // oForm.Freeze(true);
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbItem").Specific;
                SAPbouiCOM.ComboBox oComboBrand = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbBrand").Specific;
                SAPbouiCOM.ComboBox oComboModel = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbModel").Specific;
                SAPbouiCOM.ComboBox oComboColor = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbColor").Specific;
                SAPbouiCOM.ComboBox oComboSizeId = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSizeId").Specific;
               // SAPbouiCOM.ComboBox oComboSize = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSize").Specific;
                SAPbouiCOM.ComboBox oComboLoc = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbLoc").Specific;
                gen.FillCombo(oForm, oComboItem, "@CLASSIFICATION", "Code", "Name", true, true);
                gen.FillCombo(oForm, oComboBrand, "@BRAND", "Code", "Name", true, true);
                gen.FillCombo(oForm, oComboModel, "@MODEL", "Code", "Name", true, true);
                gen.FillCombo(oForm, oComboColor, "@COLOR", "Code", "Name", true, true);
                gen.FillCombo(oForm, oComboSizeId, "@SIZECAT", "Code", "Name", true, true);
               // gen.FillCombo(oForm, oComboSize, "@SIZE", "Code", "Name", true, true);
                gen.FillCombo(oForm, oComboLoc, "@DELIVERYLOC", "Code", "Name", true, true);

                oComboBrand.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                oComboModel.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                oComboColor.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                oComboSizeId.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
               // oComboSize.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                oComboLoc.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                oComboItem.Select("2", SAPbouiCOM.BoSearchKey.psk_ByValue);
                // FillSizeCombo();
                //  oForm.Freeze(false);

            }

            catch { }// oForm.Freeze(false); }
                
        }
#endregion
        
#region ClearCombo
        public void ClearCombo(SAPbouiCOM.ItemEvent val)
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.Item(val.FormUID);
                SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(oForm.DataSources.UserDataSources.Item("PFormID").Value);

                SAPbouiCOM.ComboBox oComboBrand = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbBrand").Specific;
                SAPbouiCOM.ComboBox oComboModel = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbModel").Specific;
                SAPbouiCOM.ComboBox oComboColor = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbColor").Specific;
                SAPbouiCOM.ComboBox oComboSizeId = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSizeId").Specific;
               // SAPbouiCOM.ComboBox oComboSize = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSize").Specific;
                SAPbouiCOM.ComboBox oComboLoc = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbLoc").Specific;
                SAPbouiCOM.EditText oTxtPairs = (SAPbouiCOM.EditText)PForm.Items.Item("txtPairs").Specific;

                MItemMasterData.Instance.InitializeCombo(oComboBrand);
                MItemMasterData.Instance.InitializeCombo(oComboModel);
                MItemMasterData.Instance.InitializeCombo(oComboColor);
                MItemMasterData.Instance.InitializeCombo(oComboSizeId);
               // MItemMasterData.Instance.InitializeCombo(oComboSize);
                MItemMasterData.Instance.InitializeCombo(oComboLoc);
                oTxtPairs.Value = "";
             
            }
               
            catch
            {
                Global.SapApplication.StatusBar.SetText("Error!!", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

            }
        }
        #endregion
        # region Validations
        public bool Validation()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbItem").Specific;
                SAPbouiCOM.ComboBox oComboBrand = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbBrand").Specific;
                SAPbouiCOM.ComboBox oComboModel = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbModel").Specific;
                SAPbouiCOM.ComboBox oComboColor = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbColor").Specific;
                SAPbouiCOM.ComboBox oComboSizeId = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSizeId").Specific;
              //  SAPbouiCOM.ComboBox oComboSize = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSize").Specific;
                SAPbouiCOM.ComboBox oComboLoc = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbLoc").Specific;
                SAPbouiCOM.EditText oTxtPair = (SAPbouiCOM.EditText)oForm.Items.Item("txtPair").Specific;
                if (oComboItem.Selected.Value != "2")
                {
                    Global.SapApplication.StatusBar.SetText("Classification Not Correct !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

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
                    Global.SapApplication.StatusBar.SetText("Please Select Color!!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    return false;

                }
                else if (oComboSizeId.Selected.Value.Trim() == "-1" || oComboSizeId.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Size Id !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    return false;

                }
                ////else if (oComboSize.Selected.Value.Trim() == "-1" || oComboSize.Selected.Value.Trim() == "-999")
                ////{
                ////    Global.SapApplication.StatusBar.SetText("Please Select Size !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                ////    return false;

                ////}
                else if ( oComboLoc.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please SelectLocation !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    return false;

                }
                else if (oTxtPair.Value.Trim() == "") 
                {
                   
                    Global.SapApplication.StatusBar.SetText("Please Enter No of Pairs !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    return false;

                }


            }
            catch { return false; }
            return true;
        }
         #endregion
        #region FillSize Combo
        public void FillSizeCombo()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.ComboBox oComboSizeId = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSizeId").Specific;
               // SAPbouiCOM.ComboBox oComboSize = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSize").Specific;
              //  SAPbouiCOM.CheckBox oCbxSize = (SAPbouiCOM.CheckBox)oForm.Items.Item("cbxSize").Specific;
                string code = oComboSizeId.Selected.Value;
                ////gen.FillCombo(oForm, oComboSize, "@SIZE", "Code", "Name", " where [U_CatCode]='" + code + "'",false, true);
                ////oComboSize.ValidValues.Add("NA", "Non Standard");
                ////oComboSize.ValidValues.Add("-999", "Define New");
                ////oComboSize.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);


                 SAPbobsCOM.Recordset rsFill;
                 string strQry;
                 SAPbouiCOM.Grid _grd_Size = (SAPbouiCOM.Grid)oForm.Items.Item("grdSize").Specific;
                 SAPbouiCOM.DataTable _dt_Size = oForm.DataSources.DataTables.Item("DT_0");
                // strQry = "SELECT '' [Select], Code,Name FROM [@SIZE] where [U_CatCode]='" + code + "'";
                strQry = "SELECT '' [Select], Code,Name FROM [@SIZE] where [U_CatCode]= '" + code + "'  UNION Select '' ,'NA' ,'Non Standard'";
                 _dt_Size.ExecuteQuery(strQry);
                 _grd_Size.Columns.Item("Select").Type = SAPbouiCOM.BoGridColumnType.gct_CheckBox;

                 _grd_Size.AutoResizeColumns();
                 _grd_Size.Columns.Item("Code").Editable = false;
                 _grd_Size.Columns.Item("Name").Editable = false;
                  oForm.Items.Item("grdSize").Enabled = true;
                 _grd_Size.Columns.Item("Select").Editable = true;
                
            }
            catch { }
        }
        #endregion

        #region GST Check Box
        public void GST_CheckBOx()
        {
           try
           {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.CheckBox ochkgst= (SAPbouiCOM.CheckBox)oForm.Items.Item("chkGST").Specific;

                if (ochkgst.Checked == true)
                {
                  oForm.Items.Item("cmbMattype").Enabled = true ;
                  oForm.Items.Item("cmbchapter").Enabled = true;
                  oForm.Items.Item("cmbtaxcat").Enabled = true ;
                }
                else
                {
                  //  SAPbouiCOM.StaticText txtitemmrp = (SAPbouiCOM.StaticText)oForm.Items.Item("txtitemmrp").Specific;
                  //txtitemmrp.Item.Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                  oForm.ActiveItem = "txtitemmrp";
                  oForm.Items.Item("cmbMattype").Enabled = false;
                  oForm.Items.Item("cmbchapter").Enabled = false;
                  oForm.Items.Item("cmbtaxcat").Enabled = false;
                }
          }
            catch { }
        }
        #endregion
        
        //#region FillModel Combo
        //public void FillModelCombo()
        //{
        //    try
        //    {
        //        SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
        //        SAPbouiCOM.ComboBox oComboBrand = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbBrand").Specific;
        //        SAPbouiCOM.ComboBox oComboModel = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbModel").Specific;
        //        string code = oComboBrand.Selected.Value;
        //        gen.FillCombo(oForm, oComboModel, "@MODEL", "Code", "Name"," where [U_Brand]='" + code + "'", true, true);
        //       oComboModel.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

        //    }
        //    catch { }
        //}
        // #endregion
        
        #region Generate Code
        public void GenerateCode()
        {
            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
            try
            {
               
                oForm.Freeze(true);
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbItem").Specific;
                SAPbouiCOM.ComboBox oComboUnit = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbUnit").Specific;
                SAPbouiCOM.ComboBox oComboModel = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbModel").Specific;
                SAPbouiCOM.ComboBox oComboColor = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbColor").Specific;
                SAPbouiCOM.ComboBox oComboSizeId = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSizeId").Specific;
               // SAPbouiCOM.ComboBox oComboSize = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSize").Specific;

                SAPbouiCOM.Grid _grd_Size = (SAPbouiCOM.Grid)oForm.Items.Item("grdSize").Specific;

                SAPbouiCOM.ComboBox oComboLoc = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbLoc").Specific;

                SAPbouiCOM.EditText oEditBoxName = (SAPbouiCOM.EditText)oForm.Items.Item("txtName").Specific;
                SAPbouiCOM.EditText oEditBoxCode = (SAPbouiCOM.EditText)oForm.Items.Item("txtCode").Specific;
                string LocationID="";
                string LocDescription = "";
                if (Convert.ToString(oComboLoc.Selected.Value) == "EX")
                {
                    LocationID = "-" + Convert.ToString(oComboLoc.Selected.Value);
                    LocDescription= "-"+ oComboLoc.Selected.Description;
                }


                //string itemCode = oComboItem.Selected.Value + "-" + oComboUnit.Selected.Value + "-" + oComboModel.Selected.Value + "-" + oComboColor.Selected.Value + "-" +  oComboSize.Selected.Value  + LocationID;
                //string itemName = oComboItem.Selected.Description + "-" + oComboUnit.Selected.Description + "-" + oComboModel.Selected.Description + "-" + oComboColor.Selected.Description + "-" + oComboSize.Selected.Description  + LocDescription;
                //oEditBoxName.Value = itemName;
                //oEditBoxCode.Value = itemCode;
                oForm.Freeze(false);
            }
            catch { oForm.Freeze(false); }
        }
        #endregion


        #region Refresh Combo Box
        public void RefreshCombos(string FormID, string ComboName)
        {
            SAPbouiCOM.Form CForm = Global.SapApplication.Forms.ActiveForm;
            SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(FormID);
            SAPbouiCOM.ComboBox oComboBrand = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbBrand").Specific;
            SAPbouiCOM.ComboBox oComboModel = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbModel").Specific;
            SAPbouiCOM.ComboBox oComboColor = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbColor").Specific;
            SAPbouiCOM.ComboBox oComboSizeId = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSizeId").Specific;
           // SAPbouiCOM.ComboBox oComboSize = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSize").Specific;
            SAPbouiCOM.ComboBox oComboLoc = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbLoc").Specific;

            if (ComboName == "BRAND")
            {
                gen.FillCombo(PForm,oComboBrand, "@BRAND", "Code", "Name", true, true);
                oComboBrand.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }
            else if (ComboName == "MODEL")
            {

                //string code = oComboBrand.Selected.Value;
                //gen.FillCombo(PForm, oComboModel, "@MODEL", "Code", "Name", " where [U_Brand]='" + code + "'", true, true);
                gen.FillCombo(PForm, oComboModel, "@MODEL", "Code", "Name", true, true);

                oComboModel.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }
            else if (ComboName == "COLOR")
            {
                gen.FillCombo(PForm, oComboColor, "@COLOR", "Code", "Name", true, true);
                oComboColor.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }
            else if (ComboName == "SIZECAT")
            {
                gen.FillCombo(PForm, oComboSizeId, "@SIZECAT", "Code", "Name", true, true);
                oComboSizeId.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }
            ////else if (ComboName == "SIZE")
            ////{
            ////    gen.FillCombo(PForm, oComboSize, "@SIZE", "Code", "Name", false, true);
            ////    oComboSize.ValidValues.Add("NA", "Non Standard");
            ////    oComboSize.ValidValues.Add("-999", "Define New");
            ////    oComboSize.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            ////}
            else if (ComboName == "DELIVERYLOC")
            {
                gen.FillCombo(PForm, oComboLoc, "@DELIVERYLOC", "Code", "Name", true, true);
                oComboLoc.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }

        }
        #endregion

        
    }
}

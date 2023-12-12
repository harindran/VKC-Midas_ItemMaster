using System;
using System.Collections.Generic;
using System.Text;

namespace VKC
{
    class MFGSmallCarton
    {
         General gen = new General();

        #region Singleton

        private static MFGSmallCarton instance;

        public  static  MFGSmallCarton Instance
        {
            get
            {
                if (instance == null) instance = new MFGSmallCarton();

                return instance;
            }
        }

        #endregion

        public MFGSmallCarton()
        {
            VFGSmallCarton vw = VFGSmallCarton.Instance;
        }
        public void Classification()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                //oForm.Freeze(true);
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmGoods").Specific;
                oComboItem.Select("3", SAPbouiCOM.BoSearchKey.psk_ByValue);
               // oForm.Freeze(false);

            }
            catch { }
        }
        #region Get Combos
        public void GetCombos()
        {
            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
            try
            {
               
              //  oForm.Freeze(true);
                SAPbouiCOM.ComboBox oComboItem1 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmGoods").Specific;
                SAPbouiCOM.ComboBox oComboBrand1 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmBrand").Specific;
                SAPbouiCOM.ComboBox oComboModel1 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmModel").Specific;
                SAPbouiCOM.ComboBox oComboColor1 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmlColr").Specific;
                SAPbouiCOM.ComboBox oComboSizeId1 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmSizId").Specific;
                SAPbouiCOM.ComboBox oComboSize1 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmlSize").Specific;
                SAPbouiCOM.ComboBox oComboLoc1 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmlLoc").Specific;
               
               
                gen.FillCombo(oForm, oComboItem1, "@CLASSIFICATION", "Code", "Name", true, true);
                gen.FillCombo(oForm, oComboModel1, "@MODEL", "Code", "Name", true, true);
                gen.FillCombo(oForm, oComboBrand1, "@BRAND", "Code", "Name", true, true);
                gen.FillCombo(oForm, oComboColor1, "@COLOR", "Code", "Name", true, true);
                gen.FillCombo(oForm, oComboSizeId1, "@SIZECAT", "Code", "Name", true, true);
                gen.FillCombo(oForm, oComboSize1, "@SIZESMALL", "Code", "Name", true, true);
                gen.FillCombo(oForm, oComboLoc1, "@DELIVERYLOC", "Code", "Name", true, true);
                
               oComboItem1.Select("3", SAPbouiCOM.BoSearchKey.psk_ByValue);
               oComboBrand1.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
               oComboModel1.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
               oComboColor1.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
               oComboSizeId1.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
               oComboSize1.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
               oComboLoc1.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
              // oForm.Freeze(false);
            }

            catch {}// oForm.Freeze(false); }

        }
        #endregion
        # region Validations
        public bool Validation()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.ComboBox oComboItem1 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmGoods").Specific;
                SAPbouiCOM.ComboBox oComboModel1 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmModel").Specific;
                SAPbouiCOM.ComboBox oComboBrand1 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmBrand").Specific;
                SAPbouiCOM.ComboBox oComboColor1 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmlColr").Specific;
                SAPbouiCOM.ComboBox oComboSizeId1 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmSizId").Specific;
                SAPbouiCOM.ComboBox oComboSize1 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmlSize").Specific;
                SAPbouiCOM.ComboBox oComboLoc1 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmlLoc").Specific;
                if (oComboItem1.Selected.Value != "3")
                {
                    Global.SapApplication.StatusBar.SetText("Classification Not Correct !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    return false;
                }
                else if (oComboBrand1.Selected.Value.Trim() == "-1" || oComboBrand1.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Brand !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    return false;

                }
                else if (oComboModel1.Selected.Value.Trim() == "-1" || oComboModel1.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Model !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    return false;

                }
                else if (oComboColor1.Selected.Value.Trim() == "-1" || oComboColor1.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Color!!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    return false;

                }
                else if (oComboSizeId1.Selected.Value.Trim() == "-1" || oComboSizeId1.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Size Id !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    return false;

                }
                else if (oComboSize1.Selected.Value.Trim() == "-1" || oComboSize1.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Size !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    return false;

                }
                else if ( oComboLoc1.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please SelectLocation !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    return false;

                }


            }
            catch { return false; }
            return true;
        }
          #endregion
       
        #region FillSizeCombo
        public void FillSizeCombo()
        {
            try
            {
               //// SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
               ////// oForm.Freeze(true);
               //// SAPbouiCOM.ComboBox oComboSizeId1 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmSizId").Specific;
               //// SAPbouiCOM.ComboBox oComboSize1 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmlSize").Specific;
               //// string code = oComboSizeId1.Selected.Value;
               //// gen.FillCombo(oForm, oComboSize1, "@SIZESMAL", "Code", "Name", " where [U_CatCode]='" + code + "'", true, true);
               //// oComboSize1.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
               //// // oForm.Freeze(false);
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
        //        SAPbouiCOM.ComboBox oComboBrand = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmBrand").Specific;
        //        SAPbouiCOM.ComboBox oComboModel = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmModel").Specific;
        //        string code = oComboBrand.Selected.Value;
        //        gen.FillCombo(oForm, oComboModel, "@MODEL", "Code", "Name", " where [U_Brand]='" + code + "'", true, true);
        //        oComboModel.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

        //    }
        //    catch { }
        //}
        // #endregion

        #region Generate Code
        public void GenerateCodeForSmall()
        {
            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
            try
            {
               
                oForm.Freeze(true);
               SAPbouiCOM.ComboBox oComboItem1 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmGoods").Specific;
                SAPbouiCOM.ComboBox oComboUnit1 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmlUnit").Specific;
                SAPbouiCOM.ComboBox oComboModel1 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmModel").Specific;
                SAPbouiCOM.ComboBox oComboColor1 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmlColr").Specific;
                SAPbouiCOM.ComboBox oComboSizeId1 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmSizId").Specific;
                SAPbouiCOM.ComboBox oComboSize1 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmlSize").Specific;
                SAPbouiCOM.ComboBox oComboLoc1 = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmlLoc").Specific;

                SAPbouiCOM.EditText oEditBoxCode = (SAPbouiCOM.EditText)oForm.Items.Item("txtSmalCod").Specific;
                SAPbouiCOM.EditText oEditBoxName= (SAPbouiCOM.EditText)oForm.Items.Item("txtSmlName").Specific;
                string LocationID = "";
                string LocDescription = "";
                if (Convert.ToString(oComboLoc1.Selected.Value) == "EX")
                {
                    LocationID = "-" + Convert.ToString(oComboLoc1.Selected.Value);
                    LocDescription = "-" + oComboLoc1.Selected.Description;
                }

                
                string itemCode = oComboItem1.Selected.Value + "-" + oComboUnit1.Selected.Value + "-" + oComboModel1.Selected.Value + "-" + oComboColor1.Selected.Value + "-" + oComboSizeId1.Selected.Value + oComboSize1.Selected.Value +  LocationID;
                string itemName = oComboItem1.Selected.Description + "-" + oComboUnit1.Selected.Description + "-" + oComboModel1.Selected.Description + "-" + oComboColor1.Selected.Description + "-" + oComboSizeId1.Selected.Description + "-" + oComboSize1.Selected.Description +  LocDescription;
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

                SAPbouiCOM.ComboBox oComboBrand1 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmBrand").Specific;
                SAPbouiCOM.ComboBox oComboModel1 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmModel").Specific;
                SAPbouiCOM.ComboBox oComboColor1 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmlColr").Specific;
                SAPbouiCOM.ComboBox oComboSizeId1 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmSizId").Specific;
                SAPbouiCOM.ComboBox oComboSize1 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmlSize").Specific;
                SAPbouiCOM.ComboBox oComboLoc1 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmlLoc").Specific;


                MItemMasterData.Instance.InitializeCombo(oComboBrand1);
                MItemMasterData.Instance.InitializeCombo(oComboModel1);
                MItemMasterData.Instance.InitializeCombo(oComboColor1);
                MItemMasterData.Instance.InitializeCombo(oComboSizeId1);
                MItemMasterData.Instance.InitializeCombo(oComboSize1);
                MItemMasterData.Instance.InitializeCombo(oComboLoc1);
            }
            catch
            {
                Global.SapApplication.StatusBar.SetText("Error!!", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

            }

        }
         #endregion
        #region Define Unit
        public void DefineUnit()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
          
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmlUnit").Specific;
                if (oComboItem.Value.ToString() == "")
                {
                    MItemMasterData.Instance.DifineNew("UNIT",oForm.UniqueID);

                }
                else if (oComboItem.Selected.Value == "-999")
                {
                    MItemMasterData.Instance.DifineNew("UNIT",oForm.UniqueID);
                }

              
            }

            catch { }
        }
        public void DefineBrand()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmBrand").Specific;
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
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmModel").Specific;
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
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmlColr").Specific;
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
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmSizId").Specific;
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
        public void DefineSize()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmlSize").Specific;
                if (oComboItem.Value.ToString() == "")
                {
                    MItemMasterData.Instance.DifineNew("SIZESMALL",oForm.UniqueID);

                }
                else if (oComboItem.Selected.Value == "-999")
                {
                    MItemMasterData.Instance.DifineNew("SIZESMALL",oForm.UniqueID);
                }

            }

            catch { }
        }
        public void DefineDeliveryLoc()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSmlLoc").Specific;
                if (oComboItem.Value.ToString() == "")
                {
                    MItemMasterData.Instance.DifineNew("DELIVERYLOC",oForm.UniqueID);

                }
                else  if (oComboItem.Selected.Value == "-999")
                {
                    MItemMasterData.Instance.DifineNew("DELIVERYLOC",oForm.UniqueID);
                }

            }

            catch { }
        }
        #endregion

        #region Refresh Combo Box
        public void RefreshCombos(string FormID, string ComboName)
        {
            SAPbouiCOM.Form CForm = Global.SapApplication.Forms.ActiveForm;
            SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(FormID);
            SAPbouiCOM.ComboBox oComboBrand1 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmBrand").Specific;
            SAPbouiCOM.ComboBox oComboModel1 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmModel").Specific;
            SAPbouiCOM.ComboBox oComboColor1 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmlColr").Specific;
            SAPbouiCOM.ComboBox oComboSizeId1 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmSizId").Specific;
            SAPbouiCOM.ComboBox oComboSize1 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmlSize").Specific;
            SAPbouiCOM.ComboBox oComboLoc1 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmlLoc").Specific;


            if (ComboName == "MODEL")
            {
                gen.FillCombo(PForm, oComboModel1, "@MODEL", "Code", "Name", true, true);
                oComboModel1.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }
            if (ComboName == "BRAND")
            {
                gen.FillCombo(PForm, oComboBrand1, "@BRAND", "Code", "Name", true, true);
                oComboBrand1.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }
            else if (ComboName == "COLOR")
            {
                gen.FillCombo(PForm, oComboColor1, "@COLOR", "Code", "Name", true, true);
                oComboColor1.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }
            else if (ComboName == "SIZECAT")
            {
                gen.FillCombo(PForm, oComboSizeId1, "@SIZECAT", "Code", "Name", true, true);
                oComboSizeId1.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }
            else if (ComboName == "SIZESMALL")
            {
                gen.FillCombo(PForm, oComboSize1, "@SIZESMALL", "Code", "Name", true, true);
                oComboSize1.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }
            else if (ComboName == "DELIVERYLOC")
            {
                gen.FillCombo(PForm, oComboLoc1, "@DELIVERYLOC", "Code", "Name", true, true);
                oComboLoc1.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }

        }
        #endregion
       
       
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace VKC
{
    class MUnit
    {
         General gen = new General();

        #region Singleton

        private static MUnit instance;

        public  static  MUnit Instance
        {
            get
            {
                if (instance == null) instance = new MUnit();

                return instance;
            }
        }

        #endregion

        public MUnit()
        {
            VUnit vw = VUnit.Instance;
        }
        public void GenerateMasterItemCode(string UnitCode, string UnitName)
        {
            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
            bool boolSize = false;
            try
            {
                SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(oForm.DataSources.UserDataSources.Item("PFormID").Value);

                oForm.Freeze(true);
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbItem").Specific;
                SAPbouiCOM.ComboBox oComboBrand = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbBrand").Specific;
                SAPbouiCOM.ComboBox oComboModel = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbModel").Specific;
                SAPbouiCOM.ComboBox oComboColor = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbColor").Specific;
                SAPbouiCOM.ComboBox oComboSizeId = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSizeId").Specific;
              //  SAPbouiCOM.ComboBox oComboSize = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSize").Specific;
                SAPbouiCOM.ComboBox oComboLoc = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbLoc").Specific;
                SAPbouiCOM.EditText oTxtPair = (SAPbouiCOM.EditText)PForm.Items.Item("txtPairs").Specific;
                SAPbouiCOM.Grid ogrdSize = (SAPbouiCOM.Grid)PForm.Items.Item("grdSize").Specific;
                for (int i = 0;i<ogrdSize.DataTable.Rows.Count ;++i)
                {
                    string strSelect = ogrdSize.DataTable.Columns.Item("Select").Cells.Item(i).Value.ToString();
                    //string strSize =ogrdSize.DataTable.Columns.Item("Code").Cells.Item(i).Value.ToString();
                    //string strSizeName =ogrdSize.DataTable.Columns.Item("Name").Cells.Item(i).Value.ToString();
                    if (strSelect == "Y")
                    {
                        boolSize = true;
                        break;
                    }
                }

                if (oComboItem.Selected.Value != "2")
                {
                    Global.SapApplication.StatusBar.SetText("Classification Not Correct !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    oForm.Items.Item("btnUnitAdd").Enabled = false;

                }
                else if (oComboBrand.Selected.Value.Trim() == "-1" || oComboBrand.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Brand !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    oForm.Items.Item("btnUnitAdd").Enabled = false;
                }
                else if (oComboModel.Value == "")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Brand !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    oForm.Items.Item("btnUnitAdd").Enabled = false;
                }
                else if (oComboModel.Selected.Value.Trim() == "-1" || oComboModel.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Model !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    oForm.Items.Item("btnUnitAdd").Enabled = false;


                }
                else if (oComboColor.Selected.Value.Trim() == "-1" || oComboColor.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Color!!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    oForm.Items.Item("btnUnitAdd").Enabled = false;


                }
                else if (oComboSizeId.Selected.Value.Trim() == "-1" || oComboSizeId.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Size Id !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    oForm.Items.Item("btnUnitAdd").Enabled = false;


                }
                ////else if (oComboSize.Selected.Value.Trim() == "-1" || oComboSize.Selected.Value.Trim() == "-999")
                ////{
                ////    Global.SapApplication.StatusBar.SetText("Please Select Size !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                ////    oForm.Items.Item("btnUnitAdd").Enabled = false;


                ////}
                else if (boolSize == false)
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Size !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    oForm.Items.Item("btnUnitAdd").Enabled = false;


                }

                else if (oComboLoc.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please SelectLocation !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    oForm.Items.Item("btnUnitAdd").Enabled = false;
                }

                else if (oTxtPair.Value.Trim() == "")
                {

                    Global.SapApplication.StatusBar.SetText("Please Enter No of Pairs !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    oForm.Items.Item("btnUnitAdd").Enabled = false;
                }
                
                else
                {
                   
                    for (int i = 0; i<ogrdSize.DataTable.Rows.Count; ++i)
                    {

                        string strStringSelect = ogrdSize.DataTable.Columns.Item("Select").Cells.Item(i).Value.ToString();
                        string strSize = ogrdSize.DataTable.Columns.Item("Code").Cells.Item(i).Value.ToString();
                        string strSizeName = ogrdSize.DataTable.Columns.Item("Name").Cells.Item(i).Value.ToString();

                        if (strStringSelect == "Y")
                        {

                            string LocationID = "";
                            string LocDescription = "";
                            if (Convert.ToString(oComboLoc.Selected.Value).ToUpper() == "EX")
                            {
                                LocationID = "-" + Convert.ToString(oComboLoc.Selected.Value);
                                LocDescription = "-" + oComboLoc.Selected.Description;
                            }
                            string itemCode = "";
                            string itemName = "";
                            string billDescription = "";
                            ////if (oComboSize.Selected.Value == "NA")
                            ////{
                            ////   itemCode = oComboItem.Selected.Value + "-" + UnitCode + "-" + oComboModel.Selected.Value + "-" + oComboColor.Selected.Value + "-" + oComboSizeId.Selected.Value+  oComboSize.Selected.Value.Substring(0,1) + LocationID;
                            ////   itemName = oComboItem.Selected.Description + "-" + UnitName + "-" + oComboModel.Selected.Description + "-" + oComboColor.Selected.Description + "-" + oComboSizeId.Selected.Description + "-" + oComboSize.Selected.Description + LocDescription;
                            ////   billDescription = "Plastic Footwear-" + oComboBrand.Selected.Description + "-" + oComboModel.Selected.Description + "-" + oComboColor.Selected.Description + "-" + oComboSizeId.Selected.Description + "-" + "SP";
                            ////}
                            ////else
                            ////{
                            ////    itemCode = oComboItem.Selected.Value + "-" + UnitCode + "-" + oComboModel.Selected.Value + "-" + oComboColor.Selected.Value + "-" + oComboSize.Selected.Value + LocationID;
                            ////    itemName = oComboItem.Selected.Description + "-" + UnitName + "-" + oComboModel.Selected.Description + "-" + oComboColor.Selected.Description + "-" + oComboSize.Selected.Description + LocDescription;
                            ////    billDescription = "Plastic Footwear-" + oComboBrand.Selected.Description + "-" + oComboModel.Selected.Description + "-" + oComboColor.Selected.Description + "-" + oComboSize.Selected.Description;

                            ////}

                            if (strSize == "NA")
                            {
                                itemCode = oComboItem.Selected.Value + "-" + UnitCode + "-" + oComboModel.Selected.Value + "-" + oComboColor.Selected.Value + "-" + oComboSizeId.Selected.Value + strSize.Substring(0, 1) + LocationID;
                                itemName = oComboItem.Selected.Description + "-" + UnitName + "-" + oComboModel.Selected.Description + "-" + oComboColor.Selected.Description + "-" + oComboSizeId.Selected.Description + "-" + strSizeName + LocDescription;
                                SAPbouiCOM.EditText otxt = (SAPbouiCOM.EditText)PForm.Items.Item("tifname").Specific;
                                //billDescription = "Plastic Footwear-" + oComboBrand.Selected.Description + "-" + oComboModel.Selected.Description + "-" + oComboColor.Selected.Description + "-" + oComboSizeId.Selected.Description + "-" + "SP";
                                billDescription = Convert.ToString(otxt.Value) + oComboBrand.Selected.Description + "-" + oComboModel.Selected.Description + "-" + oComboColor.Selected.Description + "-" + oComboSizeId.Selected.Description + "-" + "SP";
                            }
                            else
                            {
                                itemCode = oComboItem.Selected.Value + "-" + UnitCode + "-" + oComboModel.Selected.Value + "-" + oComboColor.Selected.Value + "-" + strSize + LocationID;
                                itemName = oComboItem.Selected.Description + "-" + UnitName + "-" + oComboModel.Selected.Description + "-" + oComboColor.Selected.Description + "-" + strSizeName + LocDescription;
                                SAPbouiCOM.EditText otxt = (SAPbouiCOM.EditText)PForm.Items.Item("tifname").Specific;
                                //billDescription = "Plastic Footwear-" + oComboBrand.Selected.Description + "-" + oComboModel.Selected.Description + "-" + oComboColor.Selected.Description + "-" + strSizeName;
                                billDescription = Convert.ToString(otxt.Value) + oComboBrand.Selected.Description + "-" + oComboModel.Selected.Description + "-" + oComboColor.Selected.Description + "-" + strSizeName;

                            }
                            SAPbouiCOM.Matrix oMatrix = (SAPbouiCOM.Matrix)oForm.Items.Item("mtxItemCod").Specific;
                            oMatrix.AddRow(1, oMatrix.RowCount);
                            oMatrix.GetLineData(oMatrix.RowCount);

                            SAPbouiCOM.DBDataSource dbItem = (SAPbouiCOM.DBDataSource)oForm.DataSources.DBDataSources.Item("OITM");

                            dbItem.SetValue("ItemCode", 0, itemCode);
                            dbItem.SetValue("ItemName", 0, itemName);
                            dbItem.SetValue("FrgnName", 0, billDescription);
                            dbItem.SetValue("U_Unit", 0, UnitCode);
                            dbItem.SetValue("U_Size", 0, strSize);
                            oMatrix.SetLineData(oMatrix.RowCount);
                            oForm.Freeze(false);
                        }
                    }
                }
                    
                oForm.Freeze(false);
            }
                
            catch { oForm.Freeze(false); }

        }
        #region Remove Master ItemCode
        public void RemoveItemCode(string UnitCode)
        {
            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
            try
            {
      

                oForm.Freeze(true);
               
                SAPbouiCOM.Matrix oMatrix = (SAPbouiCOM.Matrix)oForm.Items.Item("mtxItemCod").Specific;
                for (int i = 1; i <= oMatrix.RowCount; ++i)
                {
                    SAPbouiCOM.EditText txtUnit = (SAPbouiCOM.EditText)oMatrix.Columns.Item("colUnit").Cells.Item(i).Specific;
                    if (txtUnit.Value == UnitCode)
                    {
                        oMatrix.DeleteRow(i);
                    }
                }
               oForm.Freeze(false);
            }
            catch { oForm.Freeze(false); }

        }
        #endregion
        #region Code for Small Carton
        public void GenerateCodeForSmall(string UnitCode, string UnitName)
        {
            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
            try
            {
               
                SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(oForm.DataSources.UserDataSources.Item("PFormID").Value);

                oForm.Freeze(true);
                SAPbouiCOM.ComboBox oComboItem1 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmGoods").Specific;
                SAPbouiCOM.ComboBox oComboBrand1 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmBrand").Specific;
                SAPbouiCOM.ComboBox oComboModel1 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmModel").Specific;
                SAPbouiCOM.ComboBox oComboColor1 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmlColr").Specific;
                SAPbouiCOM.ComboBox oComboSizeId1 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmSizId").Specific;
                SAPbouiCOM.ComboBox oComboSize1 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmlSize").Specific;
                SAPbouiCOM.ComboBox oComboLoc1 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmlLoc").Specific;

                if (oComboItem1.Selected.Value != "3")
                {
                    Global.SapApplication.StatusBar.SetText("Classification Not Correct !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    oForm.Items.Item("btnUnitAdd").Enabled = false;
                }
                else if (oComboBrand1.Selected.Value.Trim() == "-1" || oComboBrand1.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Brand !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    oForm.Items.Item("btnUnitAdd").Enabled = false;
                }
                else if (oComboModel1.Value == "")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Brand !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    oForm.Items.Item("btnUnitAdd").Enabled = false;
                }
                else if (oComboModel1.Selected.Value.Trim() == "-1" || oComboModel1.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Model !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    oForm.Items.Item("btnUnitAdd").Enabled = false;
                }
                else if (oComboColor1.Selected.Value.Trim() == "-1" || oComboColor1.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Color!!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    oForm.Items.Item("btnUnitAdd").Enabled = false;


                }
                else if (oComboSizeId1.Selected.Value.Trim() == "-1" || oComboSizeId1.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Size Id !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    oForm.Items.Item("btnUnitAdd").Enabled = false;


                }
                else if (oComboSize1.Selected.Value.Trim() == "-1" || oComboSize1.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Size !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    oForm.Items.Item("btnUnitAdd").Enabled = false;


                }
                else if (oComboLoc1.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please SelectLocation !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                    oForm.Items.Item("btnUnitAdd").Enabled = false;

                }
                else
                {

                    string LocationID = "";
                    string LocDescription = "";
                    if (Convert.ToString(oComboLoc1.Selected.Value.ToUpper()) == "EX")
                    {
                        LocationID = "-" + Convert.ToString(oComboLoc1.Selected.Value);
                        LocDescription = "-" + oComboLoc1.Selected.Description;
                    }
                    SAPbouiCOM.EditText otxt = (SAPbouiCOM.EditText)PForm.Items.Item("tifname").Specific;
                    string itemCode = oComboItem1.Selected.Value + "-" + UnitCode + "-" + oComboModel1.Selected.Value + "-" + oComboColor1.Selected.Value + "-" + oComboSizeId1.Selected.Value + oComboSize1.Selected.Value + LocationID;
                    string itemName = oComboItem1.Selected.Description + "-" + UnitName + "-" + oComboModel1.Selected.Description + "-" + oComboColor1.Selected.Description + "-" + oComboSizeId1.Selected.Description + "-" + oComboSize1.Selected.Description + LocDescription;
                    string billDescription = Convert.ToString(otxt.Value) + oComboBrand1.Selected.Description + "-" + oComboModel1.Selected.Description + "-" + oComboColor1.Selected.Description + "-" + oComboSizeId1.Selected.Description + "-" + oComboSize1.Selected.Description;

                    SAPbouiCOM.Matrix oMatrix = (SAPbouiCOM.Matrix)oForm.Items.Item("mtxItemCod").Specific;
                    oMatrix.AddRow(1, oMatrix.RowCount);
                    oMatrix.GetLineData(oMatrix.RowCount);

                    SAPbouiCOM.DBDataSource dbItem = (SAPbouiCOM.DBDataSource)oForm.DataSources.DBDataSources.Item("OITM");

                    dbItem.SetValue("ItemCode", 0, itemCode);
                    dbItem.SetValue("ItemName", 0, itemName);
                    dbItem.SetValue("U_Unit", 0, UnitCode);
                    dbItem.SetValue("FrgnName", 0, billDescription);
                    oMatrix.SetLineData(oMatrix.RowCount);
                    oForm.Freeze(false);
                }
                oForm.Freeze(false);
            }
            catch { oForm.Freeze(false); }

        }
        #endregion

        #region Code for Asset
        public void GenerateCodeForAsset(string UnitCode, string UnitName)
        {
            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
            try
            {

                SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(oForm.DataSources.UserDataSources.Item("PFormID").Value);

                oForm.Freeze(true);
                SAPbouiCOM.ComboBox oComboClass = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbClassi").Specific;
                SAPbouiCOM.ComboBox oCombogroup = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbAstGrp").Specific;
                SAPbouiCOM.ComboBox oComboCat1 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbCat1").Specific;
                SAPbouiCOM.ComboBox oComboCat2 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbCat2").Specific;
                SAPbouiCOM.ComboBox oComboCat3 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbCat3").Specific;
                SAPbouiCOM.ComboBox oComboCat4 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbCat4").Specific;

                if (oComboClass.Selected.Value != "1")
                {
                    Global.SapApplication.StatusBar.SetText("Classification Not Correct !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    oForm.Items.Item("btnUnitAdd").Enabled = false;
                }
                else if (oCombogroup.Selected.Value.Trim() == "-1" || oCombogroup.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Group !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    oForm.Items.Item("btnUnitAdd").Enabled = false;
                }
                else if (oComboCat1.Selected.Value.Trim() == "-1" || oComboCat2.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Sub Group!!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    oForm.Items.Item("btnUnitAdd").Enabled = false;
                }
                else if (oComboCat2.Selected.Value.Trim() == "-1" || oComboCat2.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Category2 !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    oForm.Items.Item("btnUnitAdd").Enabled = false;
                }
                else if (oComboCat3.Selected.Value.Trim() == "-1" || oComboCat3.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Category3!!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    oForm.Items.Item("btnUnitAdd").Enabled = false;


                }
                else if (oComboCat4.Selected.Value.Trim() == "-1" || oComboCat4.Selected.Value.Trim() == "-999")
                {
                    Global.SapApplication.StatusBar.SetText("Please Select Category4 !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    oForm.Items.Item("btnUnitAdd").Enabled = false;


                }
               
                else
                {

                    string itemCode = oComboClass.Selected.Value + UnitCode + "-" + oComboCat1.Selected.Value + "-" + oComboCat2.Selected.Value + oComboCat3.Selected.Value + "-" + oComboCat4.Selected.Value;
                    string itemName = oComboClass.Selected.Description + "-" + oCombogroup.Selected.Description + "-" + oComboCat1.Selected.Description + "-" + oComboCat2.Selected.Description + "-" + oComboCat3.Selected.Description + "-" + oComboCat4.Selected.Description;
 

                    SAPbouiCOM.Matrix oMatrix = (SAPbouiCOM.Matrix)oForm.Items.Item("mtxItemCod").Specific;
                    oMatrix.AddRow(1, oMatrix.RowCount);
                    oMatrix.GetLineData(oMatrix.RowCount);

                    SAPbouiCOM.DBDataSource dbItem = (SAPbouiCOM.DBDataSource)oForm.DataSources.DBDataSources.Item("OITM");

                    dbItem.SetValue("ItemCode", 0, itemCode);
                    dbItem.SetValue("ItemName", 0, itemName);
                    dbItem.SetValue("U_Unit", 0, UnitCode);
                    oMatrix.SetLineData(oMatrix.RowCount);
                    oForm.Freeze(false);
                }
                oForm.Freeze(false);
            }
            catch { oForm.Freeze(false); }

        }
        #endregion

        #region Generate Coding
        public void GenerateCode(string Button, string UnitCode)
        {
            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
            try
            {
              
                SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(oForm.DataSources.UserDataSources.Item("PFormID").Value);
                SAPbouiCOM.EditText oEditBoxCode;
                SAPbouiCOM.EditText oEditBoxName;
                oForm.Freeze(true);
                
                if (Button == "btnConAdd")
                {
                 oEditBoxName = (SAPbouiCOM.EditText)PForm.Items.Item("txtConName").Specific;
                   oEditBoxCode = (SAPbouiCOM.EditText)PForm.Items.Item("txtConCode").Specific;
                }
                else if (Button == "btnPkAdd")
                {
                    oEditBoxName = (SAPbouiCOM.EditText)PForm.Items.Item("txtPkName").Specific;
                    oEditBoxCode = (SAPbouiCOM.EditText)PForm.Items.Item("txtPkCode").Specific;
                }
                else if (Button == "btnRawAdd")
                {
                     oEditBoxName = (SAPbouiCOM.EditText)PForm.Items.Item("txtRawName").Specific;
                     oEditBoxCode = (SAPbouiCOM.EditText)PForm.Items.Item("txtRawCode").Specific;
                }
                else if (Button == "btnSrpAdd")
                {
                    oEditBoxName = (SAPbouiCOM.EditText)PForm.Items.Item("txtSrpName").Specific;
                    oEditBoxCode = (SAPbouiCOM.EditText)PForm.Items.Item("txtSrpCode").Specific;
                }
                else if (Button == "btnSemAdd")
                {
                   oEditBoxName = (SAPbouiCOM.EditText)PForm.Items.Item("txtSemName").Specific;
                   oEditBoxCode = (SAPbouiCOM.EditText)PForm.Items.Item("txtSemCode").Specific;
                }
                    // ----commented by Reena on 25-05-2013----------------------------------//
                //////else if (Button == "btnAstAdd")
                //////{
                //////    oEditBoxName = (SAPbouiCOM.EditText)PForm.Items.Item("txtAstName").Specific;
                //////    oEditBoxCode = (SAPbouiCOM.EditText)PForm.Items.Item("txtAstCode").Specific;
                //////}
                else
                {
                    oEditBoxName = (SAPbouiCOM.EditText)PForm.Items.Item("txtAstName").Specific;
                    oEditBoxCode = (SAPbouiCOM.EditText)PForm.Items.Item("txtAstCode").Specific;
                }
                
                string itemCode = oEditBoxCode.Value;
                string itemName = oEditBoxName.Value;
                if (itemCode == "" || itemName == "")
                {

                    Global.SapApplication.StatusBar.SetText("Enter Name and Code !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    oForm.Items.Item("btnUnitAdd").Enabled = false;

                }
                else
                {
                    SAPbouiCOM.Matrix oMatrix = (SAPbouiCOM.Matrix)oForm.Items.Item("mtxItemCod").Specific;
                    oMatrix.AddRow(1, oMatrix.RowCount);
                    oMatrix.GetLineData(oMatrix.RowCount);

                    SAPbouiCOM.DBDataSource dbItem = (SAPbouiCOM.DBDataSource)oForm.DataSources.DBDataSources.Item("OITM");

                    dbItem.SetValue("ItemCode", 0, itemCode);
                    dbItem.SetValue("ItemName", 0, itemName);
                    dbItem.SetValue("U_Unit", 0, UnitCode);
                    oMatrix.SetLineData(oMatrix.RowCount);
                    oForm.Freeze(false);
                }
                oForm.Freeze(false);
            }
            catch { oForm.Freeze(false); }
        }
        #endregion
        public void InitialSettings(SAPbouiCOM.ItemEvent val)
        {
            SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;
            SAPbouiCOM.Matrix oMatrixItem = (SAPbouiCOM.Matrix)frm.Items.Item("mtxItemCod").Specific;
            oMatrixItem.Columns.Item("colSize").Visible = false;
    
            string strQry = "select code,Name from [@UNIT]";
            SAPbobsCOM.Recordset oRsDoc = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
            oRsDoc.DoQuery(strQry);
            while (!oRsDoc.EoF)
            {
             
                SAPbouiCOM.Matrix oMatrix = (SAPbouiCOM.Matrix)frm.Items.Item("mtxUnit").Specific;
                ////SAPbouiCOM.CheckBox Chbx = (SAPbouiCOM.CheckBox)oMatrix.Columns.Item("colCheck").Cells.Item(0).Specific;
               
                
                oMatrix.AddRow(1,oMatrix.RowCount);
                oMatrix.GetLineData(oMatrix.RowCount);
                SAPbouiCOM.DBDataSource dbUNIT = (SAPbouiCOM.DBDataSource)frm.DataSources.DBDataSources.Item("@UNIT");
                
                dbUNIT.SetValue("Code", 0, oRsDoc.Fields.Item("Code").Value.ToString());
                dbUNIT.SetValue("Name", 0, oRsDoc.Fields.Item("Name").Value.ToString());
               

                //oMatrix.FlushToDataSource();
                oMatrix.SetLineData(oMatrix.RowCount);
                oRsDoc.MoveNext();
            }
        }
        #region ClearDatatable
        internal void ClearDatatable(SAPbouiCOM.ItemEvent val)
        {
            try
            {
                #region Clear Data Table
                SAPbouiCOM.Form CForm = Global.SapApplication.Forms.Item(val.FormUID);
                SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(CForm.DataSources.UserDataSources.Item("PFormID").Value);
               
                SAPbouiCOM.DataTable DtItems = (SAPbouiCOM.DataTable)PForm.DataSources.DataTables.Item("DtItems");
                DtItems.Clear();		// Clear DT
                DtItems.Rows.Clear();
                Global.bubblevalue = false;
                // DtItems.Rows.Add(1);
                #endregion
            }
            catch (Exception ex)
            {
                
            }
        }
        #endregion
        #region UnitButtonClick
        internal void UnitButtonClick(SAPbouiCOM.ItemEvent val)
        {
            try
            {
                
                SAPbouiCOM.Form CForm = Global.SapApplication.Forms.Item(val.FormUID);
                SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(CForm.DataSources.UserDataSources.Item("PFormID").Value);

               // PForm.Items.Item("btnUnit").Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                bool _bool = true;

                VItemMasterData.Instance.SapApplication_ItemEvent("frmUnit", ref val, out _bool);
                   
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
        #region Fill Datatable
        internal void FillDatatable(SAPbouiCOM.ItemEvent val)
        {
            try
            {
                SAPbouiCOM.Form CForm = Global.SapApplication.Forms.Item(val.FormUID);
                 SAPbouiCOM.Form PForm  = Global.SapApplication.Forms.Item(CForm.DataSources.UserDataSources.Item("PFormID").Value);
                

                SAPbouiCOM.DataTable DtItems = (SAPbouiCOM.DataTable)PForm.DataSources.DataTables.Item("DtItems");
                SAPbouiCOM.DBDataSource DBUnit = (SAPbouiCOM.DBDataSource)CForm.DataSources.DBDataSources.Item("@UNIT");
                //SAPbouiCOM.Matrix Mtxitem = (SAPbouiCOM.Matrix)CForm.Items.Item("mtxUnit").Specific;
                SAPbouiCOM.Matrix Mtxitem = (SAPbouiCOM.Matrix)CForm.Items.Item("mtxItemCod").Specific;
                int i = 0;

                DtItems.Rows.Clear();
                for (i = 1; i <= Mtxitem.RowCount; i++)
                {

                    //SAPbouiCOM.EditText EdtCode = (SAPbouiCOM.EditText)Mtxitem.Columns.Item("colCode").Cells.Item(i).Specific;
                    //SAPbouiCOM.EditText EdtName = (SAPbouiCOM.EditText)Mtxitem.Columns.Item("colName").Cells.Item(i).Specific;
                    //SAPbouiCOM.CheckBox ChkBx = (SAPbouiCOM.CheckBox)Mtxitem.Columns.Item("colCheck").Cells.Item(i).Specific;
                    SAPbouiCOM.EditText EdtCode = (SAPbouiCOM.EditText)Mtxitem.Columns.Item("colCode").Cells.Item(i).Specific;
                    SAPbouiCOM.EditText EdtName = (SAPbouiCOM.EditText)Mtxitem.Columns.Item("colName").Cells.Item(i).Specific;
                    SAPbouiCOM.EditText EdtBill = (SAPbouiCOM.EditText)Mtxitem.Columns.Item("colBillDes").Cells.Item(i).Specific;
                    SAPbouiCOM.EditText EdtUnit = (SAPbouiCOM.EditText)Mtxitem.Columns.Item("colUnit").Cells.Item(i).Specific;
                    SAPbouiCOM.EditText EdtSize = (SAPbouiCOM.EditText)Mtxitem.Columns.Item("colSize").Cells.Item(i).Specific;

                    //if (ChkBx.Checked == true)
                    //{
                        DtItems.Rows.Add(1);
                        

                        DtItems.SetValue("colCode", DtItems.Rows.Count-1 ,EdtCode.Value);
                        DtItems.SetValue("colName", DtItems.Rows.Count-1 , EdtName.Value);
                        DtItems.SetValue("colBillDes", DtItems.Rows.Count - 1, EdtBill.Value);
                        DtItems.SetValue("colUnit", DtItems.Rows.Count - 1, EdtUnit.Value);
                        DtItems.SetValue("colSize", DtItems.Rows.Count - 1, EdtSize.Value);
                    //}

                    

                }
                if (DtItems.Rows.Count > 0)
                {
                    Global.bubbleUnit = true;
                }



            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
            }
        }
        #endregion
        #region BeforeAddUnitChecking
        public bool CheckUnitMatrixBeforeAdd( SAPbouiCOM.ItemEvent val)
        {
            try
            {
                SAPbouiCOM.Form CForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(CForm.DataSources.UserDataSources.Item("PFormID").Value);

                SAPbouiCOM.DataTable DtItems = (SAPbouiCOM.DataTable)PForm.DataSources.DataTables.Item("DtItems");
                if (DtItems.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

                ////                SAPbouiCOM.Matrix oMatrix = (SAPbouiCOM.Matrix)frm.Items.Item("mtxUnit").Specific;
                ////                int flagChk =0;
                ////                for (int i = 1; i <= oMatrix.RowCount; i++)
                ////                {

                ////                    SAPbouiCOM.CheckBox Chbx = (SAPbouiCOM.CheckBox)oMatrix.Columns.Item("colCheck").Cells.Item(i).Specific;
                ////                    if (Chbx.Checked == true)
                ////                    {
                ////                        flagChk = 1;
                ////                        break;
                ////                    }
                ////                }
                ////                if (flagChk == 0)
                ////                {
                ////                    Global.SapApplication.MessageBox("Please Select a Unit",1, "Ok", "", "");
                ////                    return false;

                ////                }
            }
            catch
            {
                return false;
            }
                            return true;
        }
        #endregion
       

    }
}

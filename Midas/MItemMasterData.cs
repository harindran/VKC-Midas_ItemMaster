using System;
using System.Collections.Generic;
using System.Text;
using SAPbouiCOM;

namespace VKC
{
    class MItemMasterData
    {
        General gen = new General();

        #region Singleton

        private static MItemMasterData instance;
        public string group;
        public string Unit;

        public static MItemMasterData Instance
        {
            get
            {
                if (instance == null) instance = new MItemMasterData();

                return instance;
            }
        }

        #endregion

        public MItemMasterData()
        {
            VItemMasterData vw = VItemMasterData.Instance;
        }
        public void Close(SAPbouiCOM.ItemEvent val)
        {
            SAPbouiCOM.Form form = Global.SapApplication.Forms.Item(val.FormUID);
            form.Close();
        }

        public void GetCombos()
        {
            try
            {
                ////SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;               
                ////SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbItem").Specific;
                ////SAPbouiCOM.ComboBox oComboUnit = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbUnit").Specific;
                ////SAPbouiCOM.ComboBox oComboModel = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbModel").Specific;
                ////SAPbouiCOM.ComboBox oComboColor = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbColor").Specific;
                ////SAPbouiCOM.ComboBox oComboSizeId = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSizeId").Specific;
                ////SAPbouiCOM.ComboBox oComboSize = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSize").Specific;
                ////SAPbouiCOM.ComboBox oComboLoc = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbLoc").Specific;
                ////gen.FillCombo(oForm, oComboItem, "@CLASSIFICATION", "Code", "Name", true, true);
                ////gen.FillCombo(oForm, oComboUnit, "@UNIT", "Code", "Name", true, true);
                ////gen.FillCombo(oForm, oComboModel,"@MODEL", "Code", "Name", true, true);
                ////gen.FillCombo(oForm, oComboColor,"@COLOR", "Code", "Name", true, true);
                ////gen.FillCombo(oForm, oComboSizeId ,"@SIZENO", "Code", "Name", true, true);
                ////gen.FillCombo(oForm, oComboSize, "@SIZE", "Code", "Name", true, true);
                ////gen.FillCombo(oForm, oComboLoc, "@DELIVERYLOC", "Code", "Name", true, true);
            }

            catch { }

        }
        
        #region AddDatatable
        internal void AddDatatable(SAPbouiCOM.ItemEvent val)
        {
            try
            {
                #region Create Data Table
                SAPbouiCOM.Form form = Global.SapApplication.Forms.Item(val.FormUID);
                SAPbouiCOM.DataTable DtItems = (SAPbouiCOM.DataTable)form.DataSources.DataTables.Add("DtItems");
                DtItems.Clear();		// Clear DT
                DtItems.Columns.Add("colUnit", SAPbouiCOM.BoFieldsType.ft_AlphaNumeric, 10);
                DtItems.Columns.Add("colCode", SAPbouiCOM.BoFieldsType.ft_AlphaNumeric, 20); //Add Column
                DtItems.Columns.Add("colName", SAPbouiCOM.BoFieldsType.ft_AlphaNumeric, 100); //Add Column
                DtItems.Columns.Add("colBillDes", SAPbouiCOM.BoFieldsType.ft_AlphaNumeric, 100);
                DtItems.Columns.Add("colSize", SAPbouiCOM.BoFieldsType.ft_AlphaNumeric, 20);
                DtItems.Rows.Clear();
                // DtItems.Rows.Add(1);
                #endregion
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
            }
        }
        #endregion

        internal void Init()
        {
            try
            {
                SAPbouiCOM.EditText  otext;
                SAPbouiCOM.Form form = Global.SapApplication.Forms.ActiveForm;
                form.Items.Item("fldrMaster").Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                otext = (SAPbouiCOM .EditText ) form.Items.Item("tudfnoofpa").Specific;
                try
                {
                    form.DataSources.UserDataSources.Add("NofPairs", BoDataType.dt_SHORT_TEXT, 8);
                }
                catch (Exception ex)
                {
                }
                otext.DataBind.SetBound(true, "", "NofPairs");
                                    
                LoadUDFFields(form);
                load_Price(form);
                load_whsc(form);
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)form.Items.Item("cmbItem").Specific;
                oComboItem.Select("2", SAPbouiCOM.BoSearchKey.psk_ByValue);
                //Global.SapApplication.MessageBox("Done",1,"","","");
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "", "", "");

            }
        }

        public void FillCombo()
        {
            try
            {
                ////MItemMaster.Instance.GetCombos();
                ////MFGSmallCarton.Instance.GetCombos();
                ////MPackingMaterials.Instance.GetCombos();
                ////MRawMaterial.Instance.GetCombos();
                ////MScrapCoding.Instance.GetCombos();
                ////MSemiFinished.Instance.GetCombos();
                ////MFixedAssets.Instance.GetCombos();
                ////MConsumablesCoding.Instance.GetCombos();
            }
            catch (Exception ex)
            {
            }
        }

        public void GenerateCode()
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbItem").Specific;
                SAPbouiCOM.ComboBox oComboUnit = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbUnit").Specific;
                SAPbouiCOM.ComboBox oComboModel = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbModel").Specific;
                SAPbouiCOM.ComboBox oComboColor = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbColor").Specific;
                SAPbouiCOM.ComboBox oComboSizeId = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSizeId").Specific;
                /// SAPbouiCOM.ComboBox oComboSize = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSize").Specific;
                SAPbouiCOM.ComboBox oComboLoc = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbLoc").Specific;

                SAPbouiCOM.EditText oEditBoxName = (SAPbouiCOM.EditText)oForm.Items.Item("txtName").Specific;
                SAPbouiCOM.EditText oEditBoxCode = (SAPbouiCOM.EditText)oForm.Items.Item("txtCode").Specific;


                ////string itemCode = oComboItem.Selected.Value + "-" + oComboUnit.Selected.Value + "-" + oComboModel.Selected.Value + "-" + oComboColor.Selected.Value + "-" + oComboSizeId.Selected.Value + oComboSize.Selected.Value + "-" + oComboLoc.Selected.Value;
                ////string itemName = oComboItem.Selected.Description + "-" + oComboUnit.Selected.Description + "-" + oComboModel.Selected.Description + "-" + oComboColor.Selected.Description + "-" + oComboSizeId.Selected.Description + oComboSize.Selected.Description + "-" + oComboLoc.Selected.Description;
                ////oEditBoxName.Value = itemName;
                ////oEditBoxCode.Value = itemCode;
            }
            catch { }
        }

        public void DifineNew(string Table, string FormUID)
        {
            try
            {
                SAPbouiCOM.MenuItem oMenu = Global.SapApplication.Menus.Item("51200");
                for (int i = 0; i <= oMenu.SubMenus.Count - 1; ++i)
                {
                    string menu = oMenu.SubMenus.Item(i).String;
                    string[] menuItems;
                    menuItems = menu.Split('-');
                    if (menuItems[0].Trim() == Table)
                    {
                        string MenuUID = oMenu.SubMenus.Item(i).UID;
                        Global.SapApplication.ActivateMenuItem(MenuUID);
                        SAPbouiCOM.Form frmPopUp = Global.SapApplication.Forms.ActiveForm;
                        frmPopUp.DataSources.UserDataSources.Add("usdParent", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 50).ValueEx = FormUID;
                        
                    }
                }
            }
            catch { }
        }

        public void FillDtable(SAPbouiCOM.ItemEvent val)
        {
            //  bool flagCheck = MUnit.Instance.CheckUnitMatrixBeforeAdd(val);
            // int a = val.PopUpIndicator;
            //frmParent.DataSources.UserDataSources.Item("ButtonID").ValueEx = val.ItemUID;
            gen.LoadXMLForm("UnitSelection.xml");
            MUnit.Instance.ClearDatatable(val);
            Global.bubblevalue = VUnit.Instance.SapApplication_ItemEvent(val);
            SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;
            //  frm.Freeze(true);
            frm.DataSources.UserDataSources.Item("PFormID").ValueEx = val.FormUID;
            frm.DataSources.UserDataSources.Item("ButtonID").ValueEx = val.ItemUID;
            MUnit.Instance.InitialSettings(val);
            //bool flagCheck = MUnit.Instance.CheckUnitMatrixBeforeAdd(val);
            //if (flagCheck == true)
            //{
            if (Global.bubbleUnit == true)
            {
                //   MItemMasterData.Instance.AddItem("txtName", "txtCode", "", "cmbUnit");
            }
            //  frm.Freeze(false);
        }

        public void InitializeCombo(SAPbouiCOM.ComboBox ddlClear)
        {
            try
            {

                ddlClear.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
            }
        }

        #region AddItem
        public bool AddItem(string txtName, string txtCode, string Group, string SubGroup,string Description, string Button, string cmbItemGroup,bool IsMCSC)
        {
            string strSubGroup = "",strDescription ="";
            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
            try
            {

                SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(oForm.DataSources.UserDataSources.Item("PFormID").Value);
                SAPbouiCOM.DataTable DtItems = (SAPbouiCOM.DataTable)PForm.DataSources.DataTables.Item("DtItems");

                oForm.Freeze(true);
                SAPbouiCOM.EditText oEditBoxName = (SAPbouiCOM.EditText)PForm.Items.Item(txtName).Specific;
                SAPbouiCOM.EditText oEditBoxCode = (SAPbouiCOM.EditText)PForm.Items.Item(txtCode).Specific;
                SAPbouiCOM.ComboBox oCmbItemCode = (SAPbouiCOM.ComboBox)PForm.Items.Item(cmbItemGroup).Specific;
               

                if (Group != "")
                {
                    SAPbouiCOM.ComboBox cmbGroup = (SAPbouiCOM.ComboBox)PForm.Items.Item(Group).Specific;
                    group = cmbGroup.Selected.Value;
                }
                if (SubGroup != "")
                {
                    SAPbouiCOM.ComboBox cmbSubGroup = (SAPbouiCOM.ComboBox)PForm.Items.Item(SubGroup).Specific;
                    strSubGroup = cmbSubGroup.Selected.Value;
                }

                if (Description != "")
                {
                    SAPbouiCOM.EditText txtDescription = (SAPbouiCOM.EditText)PForm.Items.Item(Description).Specific;
                    strDescription = txtDescription.Value;
                }

                //if (Unitstr != "")
                //{
                //    SAPbouiCOM.ComboBox cmbUnit = (SAPbouiCOM.ComboBox)PForm.Items.Item(Unitstr).Specific;
                //    Unit = cmbUnit.Selected.Value;
                //}

                SAPbobsCOM.Recordset rsItem = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string strQry = "";
                string itemCode = oEditBoxCode.Value.Trim();
                string itemName = oEditBoxName.Value.Trim();

                SAPbobsCOM.Items boItem;
                int iErrorCode = 0;

                boItem = (SAPbobsCOM.Items)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems);
                string strItemCode = itemCode;
                boItem.ItemCode = itemCode;
                boItem.ItemName = itemName;
                UDFFields_AddItemmaster(PForm, boItem);
                boItem.UserFields.Fields.Item("U_Export").Value = "N";
                if (oCmbItemCode.Selected.Value != "")
                {
                    boItem.ItemsGroupCode = Convert.ToInt32(oCmbItemCode.Selected.Value);

                }
                if (Button == "btnAstAdd")
                {
                    boItem.PurchaseItem = SAPbobsCOM.BoYesNoEnum.tYES;
                    boItem.InventoryItem = SAPbobsCOM.BoYesNoEnum.tNO;
                    boItem.SalesItem = SAPbobsCOM.BoYesNoEnum.tNO;
                    boItem.AssetItem = SAPbobsCOM.BoYesNoEnum.tYES;
                    boItem.GLMethod = SAPbobsCOM.BoGLMethods.glm_ItemClass;
                    boItem.UserFields.Fields.Item("U_Class").Value = "1";
                    boItem.UserFields.Fields.Item("U_SFGCat").Value = group;
                }
                else
                {
                    boItem.PurchaseItem = SAPbobsCOM.BoYesNoEnum.tYES;
                    boItem.InventoryItem = SAPbobsCOM.BoYesNoEnum.tYES;
                    boItem.SalesItem = SAPbobsCOM.BoYesNoEnum.tNO;
               

                }

                if (Button == "btnRawAdd")
                {
                    boItem.CostAccountingMethod = SAPbobsCOM.BoInventorySystem.bis_FIFO;
                    string strClass = strItemCode.Substring(0, 1).Trim();
                    boItem.UserFields.Fields.Item("U_Class").Value = strClass;// "5";
                   
                    if (Group != "" & strSubGroup != "")
                    {
                        boItem.UserFields.Fields.Item("U_GrpCode").Value = group.Trim() + strSubGroup.Trim();
                        boItem.UserFields.Fields.Item("U_RawGrp").Value = group.Trim();
                        boItem.UserFields.Fields.Item("U_RawSubGrp").Value = strSubGroup.Trim();
                        boItem.UserFields.Fields.Item("U_RawDescrptn").Value =strDescription.Trim();
                    }
                }
                else
                {
                    boItem.CostAccountingMethod = SAPbobsCOM.BoInventorySystem.bis_MovingAverage;


                    if (Group != "")
                        boItem.UserFields.Fields.Item("U_GrpCode").Value = group;
                }
                if (group == "CU")
                {
                    // boItem.UserFields.Fields.Item("U_Sequence").Value = PForm.DataSources.UserDataSources.Item("SMCU").Value;
                    boItem.UserFields.Fields.Item("U_GrpCode").Value = group + itemCode;
                }
                if (Button == "btnSrpAdd")
                {
                  
                    boItem.UserFields.Fields.Item("U_Class").Value = "8";
                    boItem.UserFields.Fields.Item("U_ScrpGrp").Value = group.Trim();
                    boItem.UserFields.Fields.Item("U_ScrpDescrptn").Value = strDescription.Trim();
                }
                if (Button == "btnConAdd")
                {

                    boItem.UserFields.Fields.Item("U_GrpCode").Value = group.Trim() + strSubGroup.Trim();
                    boItem.UserFields.Fields.Item("U_Class").Value = "6";
                    boItem.UserFields.Fields.Item("U_ConGrp").Value = group.Trim();
                    boItem.UserFields.Fields.Item("U_ConSubGrp").Value = strSubGroup.Trim();
                    boItem.UserFields.Fields.Item("U_ConDescrptn").Value = strDescription.Trim();
                }
                if (Button == "btnPkAdd")
                {
                    boItem.UserFields.Fields.Item("U_Class").Value = "7";
                    boItem.UserFields.Fields.Item("U_PackGrp").Value = group.Trim();
                    boItem.UserFields.Fields.Item("U_PackDescrptn").Value = strDescription.Trim();
                    boItem.UserFields.Fields.Item("U_GrpCode").Value =  group;
                }

            

                strQry = "SELECT COUNT(*)  FROM [OITM] where [ItemCode]='" + itemCode + "' ";
                rsItem.DoQuery(strQry);


                if (Convert.ToInt32(rsItem.Fields.Item(0).Value.ToString()) > 0)
                {
                    AddToUNIT_ITEM();
                    Global.SapApplication.StatusBar.SetText("Item Code Already Exists: " + itemCode, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    oForm.Items.Item("btnUnitAdd").Enabled = false;
                    oForm.Freeze(false);
                    //return false;
                }
                else
                {
                    iErrorCode = boItem.Add();
                    if (iErrorCode == 0)
                    {

                        Global.SapApplication.StatusBar.SetText("Successfully Saved", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);

                        //----------------------------------------------New Portion for Unit DB---------------------------------------//
                        //if (Unitstr != "")
                        //{
                        ////for (int i = 0; i < DtItems.Rows.Count; i++)
                        ////{
                        ////    Unit = DtItems.GetValue("colCode", i).ToString().Trim();
                        ////    string QRY1 = "Select * from [@NOR_BRANCH_DTL] Where U_UnitId ='" + Unit + "'";

                        ////    SAPbobsCOM.Recordset rsCompany = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));

                        ////    rsCompany.DoQuery(QRY1);
                        ////    if (rsCompany.RecordCount > 0)
                        ////    {

                        ////        string server = rsCompany.Fields.Item("U_ServerName").Value.ToString();
                        ////        string DB = rsCompany.Fields.Item("U_CompanyDB").Value.ToString(); ;
                        ////        string sUser = rsCompany.Fields.Item("U_SAPUserName").Value.ToString(); ;
                        ////        string sPass = rsCompany.Fields.Item("U_SAPPassword").Value.ToString(); ;
                        ////        string sqUser = rsCompany.Fields.Item("U_ServerUser").Value.ToString(); ;
                        ////        string sqPass = rsCompany.Fields.Item("U_ServerPass").Value.ToString(); ;
                        ////        gen.connectOtherCompany(server, DB, sUser, sPass, sqUser, sqPass);


                        ////        iErrorCode = 0;
                        ////        //boItem = new SAPbobsCOM.Items();
                        ////        string company = Global.NewCompany.CompanyDB.ToString();
                        ////        boItem = (SAPbobsCOM.Items)Global.NewCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems);
                        ////        boItem.ItemCode = itemCode;
                        ////        boItem.ItemName = itemName;
                        ////        boItem.PurchaseItem = SAPbobsCOM.BoYesNoEnum.tNO;
                        ////        boItem.InventoryItem = SAPbobsCOM.BoYesNoEnum.tYES;
                        ////        boItem.SalesItem = SAPbobsCOM.BoYesNoEnum.tNO;
                        ////        boItem.CostAccountingMethod = SAPbobsCOM.BoInventorySystem.bis_FIFO;
                        ////        boItem.InventoryUOM = "KG";
                        ////        boItem.ManageStockByWarehouse = SAPbobsCOM.BoYesNoEnum.tYES;
                        ////        boItem.GLMethod = SAPbobsCOM.BoGLMethods.glm_WH;
                        ////        boItem.ManageBatchNumbers = SAPbobsCOM.BoYesNoEnum.tYES;
                        ////        boItem.SRIAndBatchManageMethod = SAPbobsCOM.BoManageMethod.bomm_OnEveryTransaction;
                        ////        if (Group != "")
                        ////            boItem.UserFields.Fields.Item("U_GrpCode").Value = group;

                        ////        iErrorCode = boItem.Add();
                        ////        if (iErrorCode == 0)
                        ////        {
                        ////            Global.SapApplication.StatusBar.SetText("DB2 Successfully Saved", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);

                        ////        }
                        ////        else
                        ////        {
                        ////            Global.SapApplication.StatusBar.SetText("DB2 Error !!", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                        ////        }

                        ////    }
                        ////}
                        //}

                        //-------------------------------------------------------------------------------------------------------------//
                        string newitemcode;
                        Global.SapCompany.GetNewObjectCode(out newitemcode);
                        if (AddToUNIT_ITEM() == true && add_Pricelist_itemmaster(PForm, newitemcode))
                        {
                            oEditBoxCode.Value = "";
                            oEditBoxName.Value = "";
                            //ClearUDF(PForm);
                        }
                        else
                        {
                            if (Global.SapCompany.InTransaction)
                                Global.SapCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                            Global.SapApplication.StatusBar.SetText("DB Error !!!", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                           // return false;

                        }

                    }
                    else
                    {
                        Global.SapApplication.StatusBar.SetText("DB Error !!", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                        return false;
                    }

                }




            }
            catch
            {
                oForm.Freeze(false);
                return false;
            }
            oForm.Freeze(false);
            return true;
        }
        #endregion

        #region AddItem For SemiFinished
        public bool AddItem(string txtName, string txtCode, string Group, string Button, string cmbItemGroup, string Class, string Brand, string Model, string Color, string SizeId, string Size)
        {

            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
            try
            {

                SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(oForm.DataSources.UserDataSources.Item("PFormID").Value);
                SAPbouiCOM.DataTable DtItems = (SAPbouiCOM.DataTable)PForm.DataSources.DataTables.Item("DtItems");

                oForm.Freeze(true);
                SAPbouiCOM.EditText oEditBoxName = (SAPbouiCOM.EditText)PForm.Items.Item(txtName).Specific;
                SAPbouiCOM.EditText oEditBoxCode = (SAPbouiCOM.EditText)PForm.Items.Item(txtCode).Specific;
                SAPbouiCOM.ComboBox oCmbItemCode = (SAPbouiCOM.ComboBox)PForm.Items.Item(cmbItemGroup).Specific;

                if (Group != "")
                {
                    SAPbouiCOM.ComboBox cmbGroup = (SAPbouiCOM.ComboBox)PForm.Items.Item(Group).Specific;
                    group = cmbGroup.Selected.Value;
                }
                //if (Unitstr != "")
                //{
                //    SAPbouiCOM.ComboBox cmbUnit = (SAPbouiCOM.ComboBox)PForm.Items.Item(Unitstr).Specific;
                //    Unit = cmbUnit.Selected.Value;
                //}

                SAPbobsCOM.Recordset rsItem = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string strQry = "";
                string itemCode = oEditBoxCode.Value.Trim();
                string itemName = oEditBoxName.Value.Trim();

                SAPbobsCOM.Items boItem;
                int iErrorCode = 0;

                boItem = (SAPbobsCOM.Items)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems);
                string strItemCode = itemCode;
                boItem.ItemCode = itemCode;
                boItem.ItemName = itemName;
                UDFFields_AddItemmaster(PForm, boItem);
                boItem.UserFields.Fields.Item("U_Export").Value = "N";
                if (oCmbItemCode.Selected.Value != "")
                {
                    boItem.ItemsGroupCode = Convert.ToInt32(oCmbItemCode.Selected.Value);

                }
                if (Button == "btnAstAdd")
                {
                    boItem.PurchaseItem = SAPbobsCOM.BoYesNoEnum.tYES;
                    boItem.InventoryItem = SAPbobsCOM.BoYesNoEnum.tNO;
                    boItem.SalesItem = SAPbobsCOM.BoYesNoEnum.tNO;
                    boItem.AssetItem = SAPbobsCOM.BoYesNoEnum.tYES;
                    boItem.GLMethod = SAPbobsCOM.BoGLMethods.glm_ItemClass;
                }
                else
                {
                    boItem.PurchaseItem = SAPbobsCOM.BoYesNoEnum.tYES;
                    boItem.InventoryItem = SAPbobsCOM.BoYesNoEnum.tYES;
                    boItem.SalesItem = SAPbobsCOM.BoYesNoEnum.tNO;
                    //  boItem.InventoryUOM = "KG";
                    // boItem.ManageStockByWarehouse = SAPbobsCOM.BoYesNoEnum.tNO;
                    //boItem.GLMethod = SAPbobsCOM.BoGLMethods.glm_WH;
                    // boItem.ManageBatchNumbers = SAPbobsCOM.BoYesNoEnum.tYES;
                    // boItem.SRIAndBatchManageMethod = SAPbobsCOM.BoManageMethod.bomm_OnEveryTransaction;

                }
                boItem.UserFields.Fields.Item("U_Color").Value = Color;
                boItem.UserFields.Fields.Item("U_Model").Value = Model;
                boItem.UserFields.Fields.Item("U_Class").Value = Class;
                boItem.UserFields.Fields.Item("U_SizeCat").Value = SizeId;
                boItem.UserFields.Fields.Item("U_PairSize").Value = Size;
                boItem.UserFields.Fields.Item("U_Brand").Value = Brand;
                boItem.CostAccountingMethod = SAPbobsCOM.BoInventorySystem.bis_MovingAverage;
                //boItem.ManageStockByWarehouse = SAPbobsCOM.BoYesNoEnum.tYES;
                //boItem.GLMethod = SAPbobsCOM.BoGLMethods.glm_WH;               
                //boItem.ManageBatchNumbers = SAPbobsCOM.BoYesNoEnum.tYES;
                // boItem.SRIAndBatchManageMethod = SAPbobsCOM.BoManageMethod.bomm_OnEveryTransaction;
                if (Group != "")
                    boItem.UserFields.Fields.Item("U_GrpCode").Value = group;
                if (group == "CU")
                {
                    // boItem.UserFields.Fields.Item("U_Sequence").Value = PForm.DataSources.UserDataSources.Item("SMCU").Value;
                    boItem.UserFields.Fields.Item("U_GrpCode").Value = group + itemCode;
                }
                if (Button == "btnSrpAdd")
                {
                    boItem.UserFields.Fields.Item("U_GrpCode").Value = "SCRAP";
                }
                if (Button == "btnConAdd")
                {
                    SAPbouiCOM.ComboBox cmbGroup = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbConGrp").Specific;
                    boItem.UserFields.Fields.Item("U_GrpCode").Value = cmbGroup.Selected.Value + group;
                }

                strQry = "SELECT COUNT(*)  FROM [OITM] where [ItemCode]='" + itemCode + "' ";
                rsItem.DoQuery(strQry);

                if (Convert.ToInt32(rsItem.Fields.Item(0).Value.ToString()) > 0)
                {
                    AddToUNIT_ITEM();
                    Global.SapApplication.StatusBar.SetText("Item Code Already Exists: " + itemCode, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    oForm.Items.Item("btnUnitAdd").Enabled = false;
                    oForm.Freeze(false);
                    //return false;
                }
                else
                {
                    iErrorCode = boItem.Add();
                    if (iErrorCode == 0)
                    {

                        Global.SapApplication.StatusBar.SetText("Successfully Saved", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);

                        //----------------------------------------------New Portion for Unit DB---------------------------------------//
                        //if (Unitstr != "")
                        //{
                        ////for (int i = 0; i < DtItems.Rows.Count; i++)
                        ////{
                        ////    Unit = DtItems.GetValue("colCode", i).ToString().Trim();
                        ////    string QRY1 = "Select * from [@NOR_BRANCH_DTL] Where U_UnitId ='" + Unit + "'";

                        ////    SAPbobsCOM.Recordset rsCompany = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));

                        ////    rsCompany.DoQuery(QRY1);
                        ////    if (rsCompany.RecordCount > 0)
                        ////    {

                        ////        string server = rsCompany.Fields.Item("U_ServerName").Value.ToString();
                        ////        string DB = rsCompany.Fields.Item("U_CompanyDB").Value.ToString(); ;
                        ////        string sUser = rsCompany.Fields.Item("U_SAPUserName").Value.ToString(); ;
                        ////        string sPass = rsCompany.Fields.Item("U_SAPPassword").Value.ToString(); ;
                        ////        string sqUser = rsCompany.Fields.Item("U_ServerUser").Value.ToString(); ;
                        ////        string sqPass = rsCompany.Fields.Item("U_ServerPass").Value.ToString(); ;
                        ////        gen.connectOtherCompany(server, DB, sUser, sPass, sqUser, sqPass);


                        ////        iErrorCode = 0;
                        ////        //boItem = new SAPbobsCOM.Items();
                        ////        string company = Global.NewCompany.CompanyDB.ToString();
                        ////        boItem = (SAPbobsCOM.Items)Global.NewCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems);
                        ////        boItem.ItemCode = itemCode;
                        ////        boItem.ItemName = itemName;
                        ////        boItem.PurchaseItem = SAPbobsCOM.BoYesNoEnum.tNO;
                        ////        boItem.InventoryItem = SAPbobsCOM.BoYesNoEnum.tYES;
                        ////        boItem.SalesItem = SAPbobsCOM.BoYesNoEnum.tNO;
                        ////        boItem.CostAccountingMethod = SAPbobsCOM.BoInventorySystem.bis_FIFO;
                        ////        boItem.InventoryUOM = "KG";
                        ////        boItem.ManageStockByWarehouse = SAPbobsCOM.BoYesNoEnum.tYES;
                        ////        boItem.GLMethod = SAPbobsCOM.BoGLMethods.glm_WH;
                        ////        boItem.ManageBatchNumbers = SAPbobsCOM.BoYesNoEnum.tYES;
                        ////        boItem.SRIAndBatchManageMethod = SAPbobsCOM.BoManageMethod.bomm_OnEveryTransaction;
                        ////        if (Group != "")
                        ////            boItem.UserFields.Fields.Item("U_GrpCode").Value = group;

                        ////        iErrorCode = boItem.Add();
                        ////        if (iErrorCode == 0)
                        ////        {
                        ////            Global.SapApplication.StatusBar.SetText("DB2 Successfully Saved", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);

                        ////        }
                        ////        else
                        ////        {
                        ////            Global.SapApplication.StatusBar.SetText("DB2 Error !!", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                        ////        }

                        ////    }
                        ////}
                        //}

                        //-------------------------------------------------------------------------------------------------------------//
                        string newitemcode;
                        Global.SapCompany.GetNewObjectCode(out newitemcode);
                        
                        if (AddToUNIT_ITEM() == true && add_Pricelist_itemmaster(PForm,newitemcode))
                        {
                            oEditBoxCode.Value = "";
                            oEditBoxName.Value = "";
                            //ClearUDF(PForm);
                        }
                        else
                        {
                            if (Global.SapCompany.InTransaction)
                                Global.SapCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                            Global.SapApplication.StatusBar.SetText("Semi Finished Error !", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                            return false;

                        }


                    }
                    else
                    {
                        Global.SapApplication.StatusBar.SetText("Semi Finished Error !!", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                        return false;
                    }

                }




            }
            catch
            {
                oForm.Freeze(false);
                return false;
            }
            oForm.Freeze(false);
            return true;
        }
        #endregion

        #region AddItem New 16-04-2012
        public bool AddItem(string strItem, string strModel, string strColor, string strSizeId, string strSize, string strPairs, string strBrand)
        {

            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
            try
            {

                SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(oForm.DataSources.UserDataSources.Item("PFormID").Value);
                SAPbouiCOM.DataTable DtItems = (SAPbouiCOM.DataTable)PForm.DataSources.DataTables.Item("DtItems");

                oForm.Freeze(true);

                SAPbobsCOM.Recordset rsItem = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                // modified on 20-06-2012----------------------------------------------//
                string strQry = ""; //,strBrand ="",strGroupCode ="",strModelCode="";
                ////strQry = "select top 1 o.ItmsGrpCod,m.U_Brand,m.Code from [@MODEL] m INNER JOIN [@BRAND] b on m.U_Brand = b.Code INNER JOIN OITB o on b.Code =o.ItmsGrpCod where m.Code ='" + strModel + "' and b.Code='" + strBrandNew + "'";
                ////rsItem.DoQuery(strQry);
                ////if (rsItem.RecordCount > 0)
                ////{
                ////   strGroupCode = rsItem.Fields.Item(0).Value.ToString();
                ////   strBrand = rsItem.Fields.Item(1).Value.ToString();
                ////   strModelCode = rsItem.Fields.Item(2).Value.ToString();
                ////}

                Global.SapApplication.StatusBar.SetText("Creating Item Please wait..." , SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                for (int i = 0; i < DtItems.Rows.Count; i++)
                {

                    string itemCode = DtItems.GetValue("colCode", i).ToString().Trim();
                    string itemName = DtItems.GetValue("colName", i).ToString().Trim();
                    string unit = DtItems.GetValue("colUnit", i).ToString().Trim();
                    string billDes = DtItems.GetValue("colBillDes", i).ToString().Trim();
                    strSize = DtItems.GetValue("colSize", i).ToString().Trim();
                    SAPbobsCOM.Items boItem;
                    int iErrorCode = 0;                    
                    boItem = (SAPbobsCOM.Items)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems);
                    string strItemCode = itemCode;
                    boItem.ItemCode = itemCode;
                    boItem.ItemName = itemName;
                    boItem.ForeignName = billDes;
                    boItem.PurchaseItem = SAPbobsCOM.BoYesNoEnum.tYES;
                    boItem.InventoryItem = SAPbobsCOM.BoYesNoEnum.tYES;
                    boItem.SalesItem = SAPbobsCOM.BoYesNoEnum.tYES;
                    boItem.CostAccountingMethod = SAPbobsCOM.BoInventorySystem.bis_FIFO;
                    UDFFields_AddItemmaster(PForm, boItem);
                    // boItem.InventoryUOM = "KG";
                    // boItem.ManageStockByWarehouse = SAPbobsCOM.BoYesNoEnum.tYES;
                    // boItem.GLMethod = SAPbobsCOM.BoGLMethods.glm_WH;
                    //boItem.ManageBatchNumbers = SAPbobsCOM.BoYesNoEnum.tYES;
                    //boItem.SRIAndBatchManageMethod = SAPbobsCOM.BoManageMethod.bomm_OnEveryTransaction;
                    boItem.UserFields.Fields.Item("U_Class").Value = strItem;
                    boItem.UserFields.Fields.Item("U_Unit").Value = unit;
                    boItem.UserFields.Fields.Item("U_SizeCat").Value = strSizeId;
                    if (strItem == "2")
                    {
                        boItem.UserFields.Fields.Item("U_Size").Value = strSize;
                    }
                    else if (strItem == "3")
                    {
                        //FG Small Carton - tamizh
                        boItem.UserFields.Fields.Item("U_PairSize").Value = strSize;
                    }
                    boItem.UserFields.Fields.Item("U_Color").Value = strColor;
                    boItem.UserFields.Fields.Item("U_Model").Value = strModel;
                    boItem.UserFields.Fields.Item("U_NofPairs").Value = strPairs;
                    boItem.UserFields.Fields.Item("ItmsGrpCod").Value = strBrand;
                    boItem.UserFields.Fields.Item("U_Brand").Value = strBrand;
                    string Loc = itemCode.Substring(itemCode.Length - 2, 2);
                    if (Loc.ToUpper() == "EX")
                    {
                        boItem.UserFields.Fields.Item("U_Export").Value = "Y";
                    }
                    else
                    {
                        boItem.UserFields.Fields.Item("U_Export").Value = "N";
                    }
                    try
                    {
                        boItem.UserFields.Fields.Item("U_UnitPrice").Value = "100";
                    }
                    catch
                    { }
                    strQry = "SELECT COUNT(*)  FROM [OITM] where [ItemCode]='" + itemCode + "' ";
                    rsItem.DoQuery(strQry);


                    if (Convert.ToInt32(rsItem.Fields.Item(0).Value.ToString()) > 0)
                    {
                        Global.SapApplication.StatusBar.SetText("Item Code Already Exists: " + itemCode, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                        oForm.Items.Item("btnUnitAdd").Enabled = false;
                        oForm.Freeze(false);
                        if (Global.SapCompany.InTransaction)
                            Global.SapCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);

                        //return false;
                    }
                    else
                    {
                        iErrorCode = boItem.Add();
                        if (iErrorCode == 0)
                        {
                            try
                            {
                                string newitemcode;
                                Global.SapCompany.GetNewObjectCode(out newitemcode);
                                if (add_Pricelist_itemmaster(PForm, newitemcode))
                                {
                                    SAPbobsCOM.Recordset rsItemUnit = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                                    SAPbobsCOM.Recordset rsDocEntry = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                                    strQry = "SELECT DocEntry  FROM [OITM] where [ItemCode]='" + itemCode + "' ";
                                    rsItemUnit.DoQuery(strQry);

                                    int DocEntry = Convert.ToInt32(rsItemUnit.Fields.Item("DocEntry").Value.ToString());
                                    try { 
                                    string strAddQry = @"INSERT INTO [@NOR_OITM_UNIT]([DocEntry],[LineId],[U_UnitCode],[U_ItemCode],[U_IsIntegrated])  
                                     VALUES ('{0}',{1},'{2}','{3}','{4}') ";
                                    // strAddQry = string.Format(strAddQry, DocEntry, i.ToString(), unit, itemCode, 'N');

                                    strAddQry = string.Format(strAddQry, DocEntry, "(select Isnull((select MAX(LineId) from [@NOR_OITM_UNIT]  where U_ItemCode= '" + itemCode + "' ),0)+1 )", unit, itemCode, 'N');
                                    rsItemUnit.DoQuery(strAddQry);
                                    }
                                    catch { }
                                    if (strItem == "2")
                                    {
                                        SAPbobsCOM.Recordset rs_IsnChk = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                                        string Code = "";
                                        string strIns = "SELECT Code FROM [@NOR_ITEM_DTLS] WHERE U_Brand='" + strBrand + "' and U_Category='" + strSizeId + "' and U_Model='" + strModel + "'";
                                        rs_IsnChk.DoQuery(strIns);
                                        Code = rs_IsnChk.Fields.Item("Code").Value.ToString();
                                        if (rs_IsnChk.RecordCount == 0)
                                        {
                                            string strQryDoc = "Select MAX(isnull(cast(Code as float),0))+1 as Code from [@NOR_ITEM_DTLS]";
                                            rsDocEntry.DoQuery(strQryDoc);
                                            string strDoc = rsDocEntry.Fields.Item("Code").Value.ToString();

                                            try
                                            { 
                                            SAPbobsCOM.Recordset rs_insert1 = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                                            string _str_sql1 = @"insert into [@NOR_ITEM_DTLS] (Code,Name,DocEntry,[U_Brand],[U_Category],[U_Model]) values ('" + strDoc + "','" + strDoc + "','" + strDoc + "','" + strBrand + "','" + strSizeId + "','" + strModel + "')";
                                            rs_insert1.DoQuery(_str_sql1);
                                            Code = strDoc;
                                            }
                                            catch { }
                                        }
                                        if (Code != "")
                                        {
                                            string strIns2 = "SELECT * FROM [@NOR_ITEM_COLOR] WHERE Code='" + Code + "' and U_Color='" + strColor + "'";
                                            rs_IsnChk.DoQuery(strIns2);

                                            if (rs_IsnChk.RecordCount == 0)
                                            {
                                                try
                                            { 
                                                SAPbobsCOM.Recordset rs_insert1 = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                                                string _str_sql1 = @"insert into [@NOR_ITEM_COLOR] (Code,LineId,[U_Color]) values ('" + Code + "',(select ISNULL((Select MAX(isnull(cast(LineId as float),0))+1 as Code from [@NOR_ITEM_COLOR]),0)),'" + strColor + "')";
                                                rs_insert1.DoQuery(_str_sql1);
                                            }
                                                catch { }

                                            }

                                            string strIns1 = "SELECT * FROM [@NOR_ITEM_SIZE] WHERE Code='" + Code + "' and U_Size='" + strSize + "'";
                                            rs_IsnChk.DoQuery(strIns1);

                                            if (rs_IsnChk.RecordCount == 0)
                                            {
                                                 try
                                            {
                                                SAPbobsCOM.Recordset rs_insert1 = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                                                string _str_sql1 = @"insert into [@NOR_ITEM_SIZE] (Code,LineId,[U_Size]) values ('" + Code + "',(select ISNULL((Select MAX(isnull(cast(LineId as float),0))+1 as Code from [@NOR_ITEM_SIZE]),0)),'" + strSize + "')";
                                                rs_insert1.DoQuery(_str_sql1);
                                            }
                                                 catch { }

                                            }
                                        }
                                    }
                                    Global.SapApplication.StatusBar.SetText("Created Item: " + itemCode, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);

                                }
                                else
                                {
                                    oForm.Freeze(false);
                                    if (Global.SapCompany.InTransaction)
                                        Global.SapCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                                    Global.SapApplication.StatusBar.SetText("DB Error !!!" + Global.SapCompany.GetLastErrorDescription(), SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                                    //return false;
                                }
                        }
                            catch (Exception ex)
                            {
                                oForm.Freeze(false);
                                if (Global.SapCompany.InTransaction)
                                    Global.SapCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                                Global.SapApplication.StatusBar.SetText("DB Error !!" + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                                //return false;
                            }
                            //ClearUDF(PForm);
                        }
                        else
                        {
                            oForm.Freeze(false);
                            if (Global.SapCompany.InTransaction)
                                Global.SapCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                            Global.SapApplication.StatusBar.SetText("DB Error !" + Global.SapCompany.GetLastErrorDescription(), SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                            //return false;

                        }

                    }

                }
                //Global.SapApplication.StatusBar.SetText("Items Created Successfully..." , SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                oForm.Freeze(false);
            }
            catch(Exception ex)
            {
                oForm.Freeze(false);
                if (Global.SapCompany.InTransaction)
                    Global.SapCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                //string sErrorMsg = Global.SapCompany.GetLastErrorDescription();
                //Global.SapApplication.MessageBox(sErrorMsg, 1, "Ok", "", "");
                Global.SapApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            return true;
        }
        #endregion

        #region Insert Item Code to Unit Integrated Table
        public bool AddToUNIT_ITEM()
        {
            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
            try
            {

                oForm.Freeze(true);
                SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(oForm.DataSources.UserDataSources.Item("PFormID").Value);
                SAPbouiCOM.DataTable DtItems = (SAPbouiCOM.DataTable)PForm.DataSources.DataTables.Item("DtItems");



                SAPbobsCOM.Recordset rsItem = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string strQry = "";

                for (int i = 0; i < DtItems.Rows.Count; i++)
                {

                    string itemCode = DtItems.GetValue("colCode", i).ToString().Trim();
                    string unitCode = DtItems.GetValue("colUnit", i).ToString().Trim();

                    strQry = "SELECT DocEntry  FROM [OITM] where [ItemCode]='" + itemCode + "' ";
                    rsItem.DoQuery(strQry);
                    if (rsItem.RecordCount > 0)
                    {
                        int DocEntry = Convert.ToInt32(rsItem.Fields.Item("DocEntry").Value.ToString());

                        SAPbobsCOM.Recordset rsItemUnit = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                        strQry = "select * from [@NOR_OITM_UNIT]  where U_ItemCode= '" + itemCode + "' and U_UnitCode ='" + unitCode + "'";
                        rsItem.DoQuery(strQry);
                        if (rsItem.RecordCount <= 0)
                        {
                            string strAddQry = @"INSERT INTO [@NOR_OITM_UNIT]([DocEntry],[LineId],[U_UnitCode],[U_ItemCode],[U_IsIntegrated])  
                                     VALUES ('{0}',{1},'{2}','{3}','{4}') ";
                            strAddQry = string.Format(strAddQry, DocEntry, "(select Isnull((select MAX(LineId) from [@NOR_OITM_UNIT]  where U_ItemCode= '" + itemCode + "' ),0)+1 )", unitCode, itemCode, 'N');
                            rsItemUnit.DoQuery(strAddQry);
                        }
                    }
                }
            }


            catch
            {
                oForm.Freeze(false);
                return false;
            }
            oForm.Freeze(false);
            return true;

        }
#endregion
        
        #region FixedAssetAdd
        public bool AddItemAsset(string txtAstName, string txtAstCode, string cmbAstGrp, string btnAstAdd, string cmbFItgp, string strGroup, string strCat1, string strCat2, string strCat3, string strCat4)
        {

            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
            try
            {

                SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(oForm.DataSources.UserDataSources.Item("PFormID").Value);
                SAPbouiCOM.DataTable DtItems = (SAPbouiCOM.DataTable)PForm.DataSources.DataTables.Item("DtItems");

                SAPbouiCOM.ComboBox oCmbItemCode = (SAPbouiCOM.ComboBox)PForm.Items.Item(cmbFItgp).Specific;

                oForm.Freeze(true);

                SAPbobsCOM.Recordset rsItem = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string strQry = "";

                for (int i = 0; i < DtItems.Rows.Count; i++)
                {

                    string itemCode = DtItems.GetValue("colCode", i).ToString().Trim();
                    string itemName = DtItems.GetValue("colName", i).ToString().Trim();
                    string unit = DtItems.GetValue("colUnit", i).ToString().Trim();
                    string billDes = DtItems.GetValue("colBillDes", i).ToString().Trim();
                    SAPbobsCOM.Items boItem;
                    int iErrorCode = 0;
                    if (cmbAstGrp != "")
                    {
                        SAPbouiCOM.ComboBox cmbGroup = (SAPbouiCOM.ComboBox)PForm.Items.Item(cmbAstGrp).Specific;
                        group = cmbGroup.Selected.Value;
                    }
                    boItem = (SAPbobsCOM.Items)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems);
                    string strItemCode = itemCode;
                    boItem.ItemCode = itemCode;
                    boItem.ItemName = itemName;
                    boItem.ForeignName = billDes;
                    boItem.PurchaseItem = SAPbobsCOM.BoYesNoEnum.tYES;
                    boItem.InventoryItem = SAPbobsCOM.BoYesNoEnum.tNO;
                    boItem.SalesItem = SAPbobsCOM.BoYesNoEnum.tNO;
                    boItem.AssetItem = SAPbobsCOM.BoYesNoEnum.tYES;
                    boItem.GLMethod = SAPbobsCOM.BoGLMethods.glm_ItemClass;
                    UDFFields_AddItemmaster(PForm, boItem);
                    boItem.UserFields.Fields.Item("U_Class").Value = "1";
                    boItem.UserFields.Fields.Item("U_SFGCat").Value = group;
                    boItem.UserFields.Fields.Item("U_FASubGrp").Value = strCat1;
                    boItem.UserFields.Fields.Item("U_FACat2").Value = strCat2;
                    boItem.UserFields.Fields.Item("U_FACat3").Value = strCat3;
                    boItem.UserFields.Fields.Item("U_FACat4").Value = strCat4;
                    if (oCmbItemCode.Selected.Value != "")
                    {
                        boItem.ItemsGroupCode = Convert.ToInt32(oCmbItemCode.Selected.Value);

                    }
                    boItem.UserFields.Fields.Item("U_Unit").Value = unit;
                    boItem.UserFields.Fields.Item("U_Export").Value = "N";
                    strQry = "SELECT COUNT(*)  FROM [OITM] where [ItemCode]='" + itemCode + "' ";
                    rsItem.DoQuery(strQry);


                    if (Convert.ToInt32(rsItem.Fields.Item(0).Value.ToString()) > 0)
                    {
                        Global.SapApplication.StatusBar.SetText("Item Code Already Exists: " + itemCode, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                        oForm.Items.Item("btnUnitAdd").Enabled = false;
                        oForm.Freeze(false);
                        if (Global.SapCompany.InTransaction)
                            Global.SapCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);

                        //return false;
                    }
                    else
                    {
                        iErrorCode = boItem.Add();
                        if (iErrorCode == 0)
                        {
                            //ClearUDF(PForm);
                             string newitemcode;
                                Global.SapCompany.GetNewObjectCode(out newitemcode);
                                if (add_Pricelist_itemmaster(PForm, newitemcode))
                                { 
                                }
                                else 
                                {
                                    oForm.Freeze(false);
                                    if (Global.SapCompany.InTransaction)
                                        Global.SapCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                                    Global.SapApplication.StatusBar.SetText("Item Asset Error !", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                                    //return false;
                                }

                        }
                        else
                        {
                            oForm.Freeze(false);
                            if (Global.SapCompany.InTransaction)
                                Global.SapCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                            Global.SapApplication.StatusBar.SetText("Item Asset Error !!", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                            //return false;

                        }

                    }



                }

                oForm.Freeze(false);

            }
            catch(Exception ex)
            {
                oForm.Freeze(false);
                if (Global.SapCompany.InTransaction)
                    Global.SapCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                //string sErrorMsg = Global.SapCompany.GetLastErrorDescription();
                //Global.SapApplication.MessageBox(sErrorMsg, 1, "Ok", "", "");
                Global.SapApplication.StatusBar.SetText("Item Asset Error !!!" + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            return true;
        }
        #endregion

        #region Load_UDF_Fields
        public void LoadUDFFields(SAPbouiCOM.Form oForm)
        {
            //SAPbouiCOM.Form oForm = Global.SapApplication.Forms.Item(formuid);
            
            //oForm.PaneLevel = 9;
            try
            {
            oForm.Freeze(true);
            Global.SapApplication.StatusBar.SetText("Loading UserDefined Fields.Please wait...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
            loadcombobox(oForm, "cudfactive", "", "", "", true,"Y");
            loadcombobox(oForm, "cudfclassi", "@CLASSIFICATION", "code", "name", false,"");
            loadcombobox(oForm, "cudfunit", "@UNIT", "code", "name", false, "");
            loadcombobox(oForm, "cudfmodel", "@MODEL", "code", "name", false, "");
            loadcombobox(oForm, "cudfcolor", "@COLOR", "code", "name", false, "");
            loadcombobox(oForm, "cudfsizeca", "@SIZECAT", "Code", "Name", false, "");
            loadcombobox(oForm, "cudfstansi", "@SIZESTD", "code", "name", false, "");
            loadcombobox(oForm, "cudfsize", "@SIZE", "code", "name", false, "");
            loadcombobox(oForm, "cudfexport", "", "", "", true, "N");
            loadcombobox(oForm, "cudfbrand", "@BRAND", "code", "name", false, "");
            loadcombobox(oForm, "cudfpairsi", "@SIZESMALL", "code", "name", false, "");
            loadcombobox(oForm, "cudfsfgcat", "@GROUP", "code", "name", false, "");
            loadcombobox(oForm, "cudfcatego", "@NOR_ITEM_CAT", "code", "name", false, "");
            loadcombobox(oForm, "cudfpackin", "@PACKGROUP", "code", "name", false, "");
            loadcombobox(oForm, "cudfrawgro", "@RAWGROUP", "code", "name", false, "");
            loadcombobox(oForm, "cudfrawsub", "@RAWSUBGROUP", "code", "name", false, "");
            loadcombobox(oForm, "cudfscrapg", "@SCRAPGROUP", "code", "name", false, "");
            loadcombobox(oForm, "cudfsemigr", "@SEMIGROUP", "code", "name", false, "");
            loadcombobox(oForm, "cudfside", "@SIDE", "code", "name", false, "");
            loadcombobox(oForm, "cudffasubg", "@CATEGORY1", "code", "name", false, "");
            loadcombobox(oForm, "cudffacat2", "@CATEGORY2", "code", "name", false, "");
            loadcombobox(oForm, "cudffacat3", "@CATEGORY3", "code", "name", false, "");
            loadcombobox(oForm, "cudffacat4", "@CATEGORY4", "code", "name", false, "");
            loadcombobox(oForm, "cudfcongro", "@CONGROUP", "code", "name", false, "");
            loadcombobox(oForm, "cudfconsub", "@CONSUBGROUP", "code", "name", false, "");
            loadcombobox(oForm, "cudfwebpri", "", "", "", true,"Y");
            loadcombobox(oForm, "cchapterid", "OCHP", "AbsEntry", "ChapterID", false, "");

            loadcombobox(oForm, "cudfprodca", "@PRODUCT_CAT", "code", "name", false, "");

            string strsql;
            strsql="select T1.FldValue Code,T1.Descr Name From CUFD T0 inner join UFD1 T1 on T0.TableID=T1.TableID and T0.FieldID=T1.FieldID where T0.AliasID='Priority' and T0.TableID='OITM'";
            loadcombobox_Query(oForm, "cudfpriori", strsql, "Code", "Name",  "");
            //SAPbouiCOM.ComboBox oCombo = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfpriori").Specific;
            //oCombo.ValidValues.Add("1", "Priority1");
            //oCombo.ValidValues.Add("2", "Priority2");
            //oCombo.ValidValues.Add("3", "Priority3");

            strsql = "select T1.FldValue Code,T1.Descr Name From CUFD T0 inner join UFD1 T1 on T0.TableID=T1.TableID and T0.FieldID=T1.FieldID where T0.AliasID='PrdTyp' and T0.TableID='OITM'";
            loadcombobox_Query(oForm, "cudfprodty", strsql, "Code", "Name", "");
            //SAPbouiCOM.ComboBox oCombo = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfprodty").Specific;
            //oCombo.ValidValues.Add("Covering", "Covering");
            //oCombo.ValidValues.Add("Sandal", "Sandal");
            //oCombo.ValidValues.Add("VStrap", "VStrap");


            strsql = "select T1.FldValue Code,T1.Descr Name From CUFD T0 inner join UFD1 T1 on T0.TableID=T1.TableID and T0.FieldID=T1.FieldID where T0.AliasID='Procestype' and T0.TableID='OITM'";
            loadcombobox_Query(oForm, "cudfprocty", strsql, "Code", "Name", ""); 
            //oCombo = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfprocty").Specific;
            //oCombo.ValidValues.Add("PU", "PU");
            //oCombo.ValidValues.Add("EVA", "EVA");
            //oCombo.ValidValues.Add("STUCKON", "STUCKON");
            //oCombo.ValidValues.Add("PVC", "PVC");

            //oCombo = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmaterialt").Specific;
            //oCombo.ValidValues.Add("1", "Raw Material");
            //oCombo.ValidValues.Add("2", "Capital Goods");
            //oCombo.ValidValues.Add("3", "Finished Goods");

            SAPbouiCOM.ComboBox oCombo = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbMattype").Specific;
            oCombo.ValidValues.Add("1", "Raw Material");
            oCombo.ValidValues.Add("2", "Capital Goods");
            oCombo.ValidValues.Add("3", "Finished Goods");

            strsql = "select '-1' Code,'' Name union all Select AbsEntry Code,convert(varchar,Chapter)+'.'+convert(varchar,Heading)+'.'+convert(varchar,SubHeading) Name from OCHP";
            loadcombobox_Query(oForm, "cmbchapter", strsql, "Code", "Name",""); 

            SAPbouiCOM.EditText otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tifname").Specific;
            //otxt.Value = "Plastic Footwear-";
            otxt.Value = "Footwear-";

            SAPbouiCOM.CheckBox ochkgst = (SAPbouiCOM.CheckBox)oForm.Items.Item("chkGST").Specific;
            ochkgst.Checked = false;

            oForm.Items.Item("cmbMattype").Enabled = false;
            oForm.Items.Item("cmbchapter").Enabled = false;
            oForm.Items.Item("cmbtaxcat").Enabled = false;

            SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbItem").Specific;
            SAPbouiCOM.ComboBox oComboBrand = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbBrand").Specific;
            SAPbouiCOM.ComboBox oComboModel = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbModel").Specific;
            SAPbouiCOM.ComboBox oComboColor = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbColor").Specific;
            SAPbouiCOM.ComboBox oComboSizeId = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSizeId").Specific;
            // SAPbouiCOM.ComboBox oComboSize = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbSize").Specific;
            SAPbouiCOM.ComboBox oComboLoc = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbLoc").Specific;
           
            oComboBrand.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            oComboModel.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            oComboColor.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            oComboSizeId.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
           oComboLoc.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
           
            oForm.Freeze(false);
            Global.SapApplication.StatusBar.SetText("UserDefined Fields loaded Successfully.", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
        }
        catch (Exception ex)
        {
            oForm.Freeze(false);
            Global.SapApplication.MessageBox(ex.Message, 1, "", "", "");
        }
        }
        #endregion

        #region combobox_fill
        private void loadcombobox(SAPbouiCOM.Form oForm, string comboname, string tablename, string Code, string Name, Boolean YESNO,string Defaultstring)
        {
            SAPbouiCOM.ComboBox oCombo = (SAPbouiCOM.ComboBox)oForm.Items.Item(comboname).Specific;
            if (YESNO == true)
            {
                oCombo.ValidValues.Add("Y", "Yes");
                oCombo.ValidValues.Add("N", "No");
            }
            else
            {
                gen.FillCombo(oForm, oCombo, tablename, Code, Name, false, true);
                                              
            }
            if (Defaultstring!="")
            { 
                oCombo.Select(Defaultstring,SAPbouiCOM.BoSearchKey.psk_ByValue);
            }
        }

        private void loadcombobox_Query(SAPbouiCOM.Form oForm, string comboname, string Query, string Code, string Name, string Defaultstring)
        {
            SAPbouiCOM.ComboBox oCombo = (SAPbouiCOM.ComboBox)oForm.Items.Item(comboname).Specific;
            gen.FillCombo_Query(oForm, oCombo, Query, Code, Name, false, true);
            
            if (Defaultstring != "")
            {
                oCombo.Select(Defaultstring, SAPbouiCOM.BoSearchKey.psk_ByValue);
            }
        }
        #endregion

        #region Add Item master UDF Field

        private void UDFFields_AddItemmaster(SAPbouiCOM.Form oForm,SAPbobsCOM.Items botiem)
        {

            try
            {

            
            //Adding UDF Field to Item Master
            SAPbouiCOM.ComboBox oCmb;
            SAPbouiCOM.EditText otxt;
            SAPbouiCOM.CheckBox ochk;

            ochk = (SAPbouiCOM.CheckBox)oForm.Items.Item("chkGST").Specific;
            if (ochk.Checked == true)
            {
                botiem.GSTRelevnt = SAPbobsCOM.BoYesNoEnum.tYES;

                oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbMattype").Specific;
                if (oCmb.Selected != null)
                {
                    if (oCmb.Selected.Value == "1") { botiem.MaterialType =  SAPbobsCOM.BoMaterialTypes.mt_FinishedGoods ;  }
                    if (oCmb.Selected.Value == "2") { botiem.MaterialType = SAPbobsCOM.BoMaterialTypes.mt_GoodsInProcess; }
                    if (oCmb.Selected.Value == "3") { botiem.MaterialType = SAPbobsCOM.BoMaterialTypes.mt_RawMaterial;  }
                }
                
                oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbchapter").Specific;
                if (oCmb.Selected != null) {
                    if (oCmb.Selected.Value !="-1") {botiem.ChapterID = Int32.Parse(oCmb.Selected.Value); }
                }

                oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbtaxcat").Specific;
                if (oCmb.Selected != null) 
                {
                    if (oCmb.Selected.Value == "N") { botiem.GSTTaxCategory= SAPbobsCOM.GSTTaxCategoryEnum.gtc_NilRated; }
                    if (oCmb.Selected.Value == "R") { botiem.GSTTaxCategory = SAPbobsCOM.GSTTaxCategoryEnum.gtc_Regular; }
                    if (oCmb.Selected.Value == "E") { botiem.GSTTaxCategory = SAPbobsCOM.GSTTaxCategoryEnum.gtc_Exempt ; }
                }

            }
            else
            {
                botiem.GSTRelevnt = SAPbobsCOM.BoYesNoEnum.tNO;
            }

             otxt = (SAPbouiCOM.EditText)oForm.Items.Item("txtitemmrp").Specific;
            botiem.UserFields.Fields.Item("U_ItmMrp").Value = otxt.Value;

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfactive").Specific;
            if (oCmb.Selected != null) {botiem.UserFields.Fields.Item("U_IsActive").Value = oCmb.Selected.Value;}

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tudfgroup").Specific;
            botiem.UserFields.Fields.Item("U_GrpCode").Value = otxt.Value;

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfclassi").Specific;
            if (oCmb.Selected != null && oCmb.Selected.Value != "-1") { botiem.UserFields.Fields.Item("U_Class").Value = oCmb.Selected.Value; }

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfunit").Specific;
            if (oCmb.Selected != null && oCmb.Selected.Value != "-1") { botiem.UserFields.Fields.Item("U_Unit").Value = oCmb.Selected.Value; }

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfmodel").Specific;
            if (oCmb.Selected != null && oCmb.Selected.Value != "-1") { botiem.UserFields.Fields.Item("U_Model").Value = oCmb.Selected.Value; }

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfcolor").Specific;
            if (oCmb.Selected != null && oCmb.Selected.Value != "-1") { botiem.UserFields.Fields.Item("U_Color").Value = oCmb.Selected.Value; }

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfsizeca").Specific;
            if (oCmb.Selected != null && oCmb.Selected.Value != "-1") { botiem.UserFields.Fields.Item("U_SizeCat").Value = oCmb.Selected.Value; }

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfstansi").Specific;
            if (oCmb.Selected != null && oCmb.Selected.Value != "-1") { botiem.UserFields.Fields.Item("U_StdSize").Value = oCmb.Selected.Value; }

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfsize").Specific;
            if (oCmb.Selected != null && oCmb.Selected.Value != "-1") { botiem.UserFields.Fields.Item("U_Size").Value = oCmb.Selected.Value; }

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfexport").Specific;
            if (oCmb.Selected != null) { botiem.UserFields.Fields.Item("U_Export").Value = oCmb.Selected.Value; }

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfbrand").Specific;
            if (oCmb.Selected != null) { botiem.UserFields.Fields.Item("U_Brand").Value = oCmb.Selected.Value; }

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfpriori").Specific;
            if (oCmb.Selected != null){botiem.UserFields.Fields.Item("U_Priority").Value = oCmb.Selected.Value;}

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfpairsi").Specific;
            if (oCmb.Selected != null) { botiem.UserFields.Fields.Item("U_PairSize").Value = oCmb.Selected.Value; }

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfsfgcat").Specific;
            if (oCmb.Selected != null && oCmb.Selected.Value != "-1") { botiem.UserFields.Fields.Item("U_SFGCat").Value = oCmb.Selected.Value; }

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tudfnofpa").Specific;
            botiem.UserFields.Fields.Item("U_NofPairs").Value = otxt.Value;

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfcatego").Specific;
            if (oCmb.Selected != null && oCmb.Selected.Value != "-1") { botiem.UserFields.Fields.Item("U_Category").Value = oCmb.Selected.Value; }

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tudfvat").Specific;
            botiem.UserFields.Fields.Item("U_VATCode").Value = otxt.Value;

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfpackin").Specific;
            if (oCmb.Selected != null && oCmb.Selected.Value != "-1") { botiem.UserFields.Fields.Item("U_PackGrp").Value = oCmb.Selected.Value; }

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tudfpadesc").Specific;
            botiem.UserFields.Fields.Item("U_PackDescrptn").Value = otxt.Value;

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfrawgro").Specific;
            if (oCmb.Selected != null && oCmb.Selected.Value != "-1") { botiem.UserFields.Fields.Item("U_RawGrp").Value = oCmb.Selected.Value; }

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfrawsub").Specific;
            if (oCmb.Selected != null && oCmb.Selected.Value != "-1") { botiem.UserFields.Fields.Item("U_RawSubGrp").Value = oCmb.Selected.Value; }

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tudfrawdes").Specific;
            botiem.UserFields.Fields.Item("U_RawDescrptn").Value = otxt.Value;

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfscrapg").Specific;
            if (oCmb.Selected != null && oCmb.Selected.Value != "-1") { botiem.UserFields.Fields.Item("U_ScrpGrp").Value = oCmb.Selected.Value; }

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tudfscrapd").Specific;
            botiem.UserFields.Fields.Item("U_ScrpDescrptn").Value = otxt.Value;

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfsemigr").Specific;
            if (oCmb.Selected != null && oCmb.Selected.Value != "-1") { botiem.UserFields.Fields.Item("U_SemiGrp").Value = oCmb.Selected.Value; }

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfside").Specific;
            if (oCmb.Selected != null && oCmb.Selected.Value != "-1") { botiem.UserFields.Fields.Item("U_Side").Value = oCmb.Selected.Value; }

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudffasubg").Specific;
            if (oCmb.Selected != null && oCmb.Selected.Value != "-1") { botiem.UserFields.Fields.Item("U_FASubGrp").Value = oCmb.Selected.Value; }

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudffacat2").Specific;
            if (oCmb.Selected != null && oCmb.Selected.Value != "-1") { botiem.UserFields.Fields.Item("U_FACat2").Value = oCmb.Selected.Value; }

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudffacat3").Specific;
            if (oCmb.Selected != null && oCmb.Selected.Value != "-1") { botiem.UserFields.Fields.Item("U_FACat3").Value = oCmb.Selected.Value; }

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudffacat4").Specific;
            if (oCmb.Selected != null && oCmb.Selected.Value != "-1") { botiem.UserFields.Fields.Item("U_FACat4").Value = oCmb.Selected.Value; }

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfcongro").Specific;
            if (oCmb.Selected != null && oCmb.Selected.Value != "-1") { botiem.UserFields.Fields.Item("U_ConGrp").Value = oCmb.Selected.Value; }

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfconsub").Specific;
            if (oCmb.Selected != null && oCmb.Selected.Value != "-1") { botiem.UserFields.Fields.Item("U_ConSubGrp").Value = oCmb.Selected.Value; }

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tudfcondes").Specific;
            botiem.UserFields.Fields.Item("U_ConDescrptn").Value = otxt.Value;

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tudfcommod").Specific;
            botiem.UserFields.Fields.Item("U_HsnCcode").Value = otxt.Value;

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tudforqty").Specific;
            botiem.UserFields.Fields.Item("U_OrdQty").Value = otxt.Value;

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfwebpri").Specific;
            if (oCmb.Selected != null) { botiem.UserFields.Fields.Item("U_WPriority").Value = oCmb.Selected.Value; }

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tudfcomqty").Specific;
            botiem.UserFields.Fields.Item("U_CommQty").Value = otxt.Value;

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tudfjobtyp").Specific;
            botiem.UserFields.Fields.Item("U_JbType").Value = otxt.Value;

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tudfvatrat").Specific;
            botiem.UserFields.Fields.Item("U_VatRate").Value = otxt.Value;

            //otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tordercode").Specific;    Commented by chitra on 11/05/2021 - udf not available
            //botiem.UserFields.Fields.Item("U_OrderCode").Value = otxt.Value;

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tpuom").Specific;
            botiem.PurchaseUnit = otxt.Value;

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tsuom").Specific;
            botiem.SalesUnit = otxt.Value;

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tiuom").Specific;
            botiem.InventoryUOM = otxt.Value;

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tudfabtmen").Specific;
            botiem.UserFields.Fields.Item("U_Abtment").Value = otxt.Value;

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfprodca").Specific;
            if (oCmb.Selected != null && oCmb.Selected.Value != "-1") { botiem.UserFields.Fields.Item("U_PrdCat").Value = oCmb.Selected.Value; }

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfprodty").Specific;
            if (oCmb.Selected != null) { botiem.UserFields.Fields.Item("U_PrdTyp").Value = oCmb.Selected.Value; }

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfprocty").Specific;
            if (oCmb.Selected != null) { botiem.UserFields.Fields.Item("U_Procestype").Value = oCmb.Selected.Value; }

            //-------------included by Tamizh--------------------------
            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tudfnofpa").Specific;
            botiem.UserFields.Fields.Item("U_NofPairs").Value = otxt.Value;


            //SAPbouiCOM.CheckBox ochcek = (SAPbouiCOM.CheckBox)oForm.Items.Item("chkexcise").Specific;
            //if (ochcek.Checked == true)
            //{
            //    botiem.WTLiable= SAPbobsCOM.BoYesNoEnum.tYES;

            //     oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmaterialt").Specific;
            //     if (oCmb.Selected != null) { botiem.MaterialType  = oCmb.Selected.Value; }

            //    //otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tassessabl").Specific;
            //    //botiem.A = "";

            //}
            add_warehouse_itemmaster(oForm, botiem);
            //add_Pricelist_itemmaster_Add(oForm, botiem);
            }
            catch (Exception ex)
            {
                Global.SapApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
            
        }

        #endregion

        #region add Warehouse itemmaster
        private void add_warehouse_itemmaster(SAPbouiCOM.Form oForm, SAPbobsCOM.Items botiem)
        {
            SAPbouiCOM.Item matItem;
            SAPbouiCOM.Matrix omatwhsc;
            SAPbouiCOM.CheckBox ocheck;
            SAPbouiCOM.EditText oedit;

            
            matItem = oForm.Items.Item("mtxwhs");
            omatwhsc = (SAPbouiCOM.Matrix)matItem.Specific;
            for (int i = 1; i <= omatwhsc.RowCount ; i++)
            {
                ocheck = (SAPbouiCOM.CheckBox)omatwhsc.GetCellSpecific("Select", i);
                oedit = (SAPbouiCOM.EditText)omatwhsc.GetCellSpecific("whscode", i);
                if (ocheck.Checked==true )
                {
                    botiem.WhsInfo.WarehouseCode = oedit.Value;
                    botiem.WhsInfo.Add();
                }
            }
        }
        #endregion


        #region add PrieList itemmaster
        private bool add_Pricelist_itemmaster(SAPbouiCOM.Form oForm,string itemcode)
        {
            try
            {
            
            SAPbouiCOM.Item matItem;
            SAPbouiCOM.Matrix omatprice;
            SAPbouiCOM.EditText oEdit;
            int opricelist;
            Double oprice;
            SAPbobsCOM.Items oitem;
            int ierrorcode;

            matItem = oForm.Items.Item("mtxprice");
            omatprice = (SAPbouiCOM.Matrix)matItem.Specific;

            oitem = (SAPbobsCOM.Items)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems);

            if (oitem.GetByKey(itemcode))
            {
                for (int i = 1; i <= omatprice.RowCount; i++)
                {
                   
                    try
                    {
                        oEdit = (SAPbouiCOM.EditText)omatprice.GetCellSpecific("price", i);
                        oprice = Convert.ToDouble(oEdit.Value);
                        oEdit = (SAPbouiCOM.EditText)omatprice.GetCellSpecific("pcode", i);
                        opricelist = Convert.ToInt32(oEdit.Value);
                        oitem.PriceList.SetCurrentLine(opricelist-1);
                        if (oprice > 0)
                        {
                            oitem.PriceList.Price = oprice;
                        }
                    }
                    catch (Exception ex)
                    {
                        Global.SapApplication.StatusBar.SetText(Convert.ToString(ex.Message), SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                                            }
                                   }
                ierrorcode = oitem.Update();
                if (ierrorcode == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
        }
        #endregion

        #region add PrieList itemmaster while adding
        private void add_Pricelist_itemmaster_Add(SAPbouiCOM.Form oForm, SAPbobsCOM.Items botiem)
        {
            try
            {

                SAPbouiCOM.Item matItem;
                SAPbouiCOM.Matrix omatprice;
                SAPbouiCOM.EditText oEdit;
                int opricelist;
                Double oprice;

                matItem = oForm.Items.Item("mtxprice");
                omatprice = (SAPbouiCOM.Matrix)matItem.Specific;

                for (int i = 1; i <= omatprice.RowCount; i++)
                {

                    try
                    {
                        oEdit = (SAPbouiCOM.EditText)omatprice.GetCellSpecific("price", i);
                        oprice = Convert.ToDouble(oEdit.Value);
                        oEdit = (SAPbouiCOM.EditText)omatprice.GetCellSpecific("pcode", i);
                        opricelist = Convert.ToInt32(oEdit.Value);
                        botiem.PriceList.SetCurrentLine(opricelist - 1);
                        if (oprice > 0)
                        {
                            botiem.PriceList.Price = oprice;
                        }
                    }
                    catch (Exception ex)
                    {
                        Global.SapApplication.StatusBar.SetText(Convert.ToString(ex.Message), SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.SapApplication.StatusBar.SetText(Convert.ToString(ex.Message), SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }
        #endregion


        #region Clear UDF Field

        public void ClearUDF(SAPbouiCOM.Form oForm)
        {
            SAPbouiCOM.ComboBox oCmb;
            SAPbouiCOM.EditText otxt;

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("txtitemmrp").Specific;
            otxt.Value = "0";

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbtaxcat").Specific;
            oCmb.Select("R", SAPbouiCOM.BoSearchKey.psk_ByValue);

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbMattype").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbchapter").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            
            //SAPbouiCOM.CheckBox ochk;
            //ochk = (SAPbouiCOM.CheckBox)oForm.Items.Item("chkGST").Specific;
            //if (ochk.Checked == true) { ochk.ValOff = "Y"; ochk.ValOn = "N"; }
            oForm.DataSources.DBDataSources.Item("OITM").SetValue("GSTRelevnt", 0, "N");

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfactive").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tudfgroup").Specific;
            otxt.Value = "";

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfclassi").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfunit").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfmodel").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfcolor").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfsizeca").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfstansi").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfsize").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfexport").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfbrand").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfpriori").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfpairsi").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfsfgcat").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tudfnoofpa").Specific;
            otxt.Value = "";

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfcatego").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tudfvat").Specific;
            otxt.Value = "";

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfpackin").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tudfpadesc").Specific;
            otxt.Value = "";

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfrawgro").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfrawsub").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tudfrawdes").Specific;
            otxt.Value = "";

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfscrapg").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tudfscrapd").Specific;
            otxt.Value = "";

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfsemigr").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfside").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudffasubg").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudffacat2").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudffacat3").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudffacat4").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfcongro").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfconsub").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tudfcondes").Specific;
            otxt.Value = "";

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tudfcommod").Specific;
            otxt.Value = "";

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tudforqty").Specific;
            otxt.Value = "";

            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfwebpri").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tudfcomqty").Specific;
            otxt.Value = "";

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tudfjobtyp").Specific;
            otxt.Value = "";

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tudfvatrat").Specific;
            otxt.Value = "";

            //otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tordercode").Specific;
            //otxt.Value = "";

            clear_Pricelist_whsc(oForm);

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tifname").Specific;
            //otxt.Value = "Plastic Footwear-";
            otxt.Value = "Footwear-";

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tiuom").Specific;
            otxt.Value = "";
            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tpuom").Specific;
            otxt.Value = "";
            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tsuom").Specific;
            otxt.Value = "";

            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tudfabtmen").Specific;
            otxt.Value = "";


            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfprodca").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfprodty").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            oCmb = (SAPbouiCOM.ComboBox)oForm.Items.Item("cudfprocty").Specific;
            oCmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            //-------------included by Tamizh----------------
            otxt = (SAPbouiCOM.EditText)oForm.Items.Item("tudfnofpa").Specific;
            otxt.Value = "";

            }

        #endregion

        #region CLear Pricelist & Warehouse

        private void clear_Pricelist_whsc(SAPbouiCOM.Form oForm)
        {
            try
            {
                oForm.Freeze(true);
            SAPbouiCOM.Item matItem ;
            SAPbouiCOM.Matrix omatprice;
            SAPbouiCOM.Matrix omatwhs;
            SAPbouiCOM.Columns oColumns;
            SAPbouiCOM.Column ocolumn;
            SAPbouiCOM.Cell ocell;
            SAPbouiCOM.EditText oedit;
            SAPbouiCOM.CheckBox ocheck;

            matItem = oForm.Items.Item("mtxprice");
            omatprice = (SAPbouiCOM.Matrix)matItem.Specific;
            oColumns = omatprice.Columns;
            //omatprice.Clear();
            //load_Price(oForm);

            for (int i = 1; i <= omatprice.RowCount ; i++)
            {
                ocolumn = oColumns.Item("price");
                ocell = ocolumn.Cells.Item(i);
                oedit = (SAPbouiCOM.EditText)ocell.Specific;
               oedit.Value = "";
            }

            matItem = oForm.Items.Item("mtxwhs");
            omatwhs = (SAPbouiCOM.Matrix)matItem.Specific;
            oColumns = omatwhs.Columns;

            omatwhs.Clear();
            load_whsc(oForm);        
            
          //for (int i = 1; i <= omatwhs.RowCount; i++)
          //  {
          //      ocheck = (SAPbouiCOM.CheckBox)omatwhs.GetCellSpecific("Select", i);//(SAPbouiCOM.CheckBox)omatwhs.Columns.Item("Select").Cells.Item(i).Specific;
          //      ocheck.Checked = false;
          //  }
            SAPbouiCOM.Grid _grd_Size = (SAPbouiCOM.Grid)oForm.Items.Item("grdSize").Specific;
            _grd_Size.DataTable.Clear();
            
            oForm.Freeze(false);
        }
        catch
        {
            oForm.Freeze(false);
        }
        }
        #endregion
        

        #region Loading Price List
    //    private void load_Price(SAPbouiCOM.Form oForm)
    //    {
    //        try
    //        {
    //            oForm.Freeze(true);
    //            Global.SapApplication.StatusBar.SetText("Loading PriceList.Please wait...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
           
    //        SAPbouiCOM.Item matItem ;
    //        SAPbouiCOM.Matrix omatprice ;
    //        SAPbouiCOM.Columns oColumns ;
    //        SAPbouiCOM.Column ocolumn ;
    //        SAPbouiCOM.Cell ocell ;
    //        SAPbouiCOM.EditText oedit;
    //        string oname;
            
    //        matItem = oForm.Items.Item("mtxprice");
    //        omatprice = (SAPbouiCOM.Matrix)matItem.Specific;
    //        oColumns = omatprice.Columns;

    //        string str = "select ListNum,ListName from OPLN order by ListName";
    //        SAPbobsCOM.Recordset objrs = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
    //        objrs.DoQuery(str);
    //        for (int i = 1; i <= objrs.RecordCount; i++)
    //        {
    //            omatprice.AddRow(1,i);

    //            ocolumn = oColumns.Item("sno");
    //            ocell = ocolumn.Cells.Item(i);
    //            oedit = (SAPbouiCOM.EditText)ocell.Specific;
    //            oname = Convert.ToString(i);
    //            oedit.Value = oname;

    //            ocolumn = oColumns.Item("pcode");
    //            ocell = ocolumn.Cells.Item(i);
    //            oedit = (SAPbouiCOM.EditText)ocell.Specific;
    //            oname= Convert.ToString(objrs.Fields.Item("ListNum").Value);
    //            oedit.Value = oname;

    //            ocolumn = oColumns.Item("pname");
    //            ocell = ocolumn.Cells.Item(i);
    //            oedit = (SAPbouiCOM.EditText) ocell.Specific;
    //            oname = Convert.ToString( objrs.Fields.Item("ListName").Value);
    //            oedit.Value = oname;

    //            objrs.MoveNext();
    //        }
    //        oForm.Freeze(false);
    //        Global.SapApplication.StatusBar.SetText("PriceList Loaded Successfully", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
    //    }
    //    catch (Exception ex)
    //    {
    //        oForm.Freeze(false);
    //        Global.SapApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
    //    }
    //}

        public void load_Price(SAPbouiCOM.Form oForm)
        {
            try
            {
                oForm.Freeze(true);
                Global.SapApplication.StatusBar.SetText("Loading PriceList.Please wait...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);

                SAPbouiCOM.Item matItem;
                SAPbouiCOM.Matrix omatprice;
                SAPbouiCOM.Columns oColumns;
                //SAPbouiCOM.Column ocolumn ;
                //SAPbouiCOM.Cell ocell ;
                //SAPbouiCOM.EditText oedit;
                //string oname;
                SAPbouiCOM.DBDataSource odbdsDetails1;
                SAPbouiCOM.DBDataSource odbdsDetails;
                matItem = oForm.Items.Item("mtxprice");
                omatprice = (SAPbouiCOM.Matrix)matItem.Specific;
                oColumns = omatprice.Columns;

                odbdsDetails1 = oForm.DataSources.DBDataSources.Item("ITM1");
                odbdsDetails = oForm.DataSources.DBDataSources.Item("OPLN");
                string str = "select ROW_NUMBER() OVER (ORDER BY ListName) Rownum, ListNum,ListName from OPLN order by ListName";
                SAPbobsCOM.Recordset objrs = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                objrs.DoQuery(str);

                while (!objrs.EoF)
                {
                    omatprice.AddRow(1, omatprice.VisualRowCount);
                    omatprice.GetLineData(omatprice.VisualRowCount);
                    odbdsDetails1.SetValue("PriceList", 0, Convert.ToString(objrs.Fields.Item("Rownum").Value));
                    odbdsDetails.SetValue("ListNum", 0, Convert.ToString(objrs.Fields.Item("ListNum").Value));
                    odbdsDetails.SetValue("ListName", 0, Convert.ToString(objrs.Fields.Item("ListName").Value));
                    omatprice.SetLineData(omatprice.VisualRowCount);
                    //((SAPbouiCOM.EditText)omatwhs.Columns.Item("sno").Cells.Item(1).Specific).Value = Convert.ToString(objrs.Fields.Item("Rownum").Value);
                    //((SAPbouiCOM.EditText)omatwhs.Columns.Item("whscode").Cells.Item(1).Specific).Value = Convert.ToString(objrs.Fields.Item("whscode").Value);
                    //((SAPbouiCOM.EditText)omatwhs.Columns.Item("whsname").Cells.Item(1).Specific).Value = Convert.ToString(objrs.Fields.Item("whsname").Value);
                    objrs.MoveNext();
                }
                //for (int i = 1; i <= objrs.RecordCount; i++)
                //{
                //    omatprice.AddRow(1,i);
                //    ((SAPbouiCOM.EditText)omatprice.Columns.Item("sno").Cells.Item(i).Specific).Value = Convert.ToString(objrs.Fields.Item("Rownum").Value);
                //    ((SAPbouiCOM.EditText)omatprice.Columns.Item("pcode").Cells.Item(i).Specific).Value = Convert.ToString(objrs.Fields.Item("ListNum").Value);
                //    ((SAPbouiCOM.EditText)omatprice.Columns.Item("pname").Cells.Item(i).Specific).Value = Convert.ToString(objrs.Fields.Item("ListName").Value);
                //    //ocolumn = oColumns.Item("sno");
                //    //ocell = ocolumn.Cells.Item(i);
                //    //oedit = (SAPbouiCOM.EditText)ocell.Specific;
                //    //oname = Convert.ToString(i);
                //    //oedit.Value = oname;

                //    //ocolumn = oColumns.Item("pcode");
                //    //ocell = ocolumn.Cells.Item(i);
                //    //oedit = (SAPbouiCOM.EditText)ocell.Specific;
                //    //oname= Convert.ToString(objrs.Fields.Item("ListNum").Value);
                //    //oedit.Value = oname;

                //    //ocolumn = oColumns.Item("pname");
                //    //ocell = ocolumn.Cells.Item(i);
                //    //oedit = (SAPbouiCOM.EditText) ocell.Specific;
                //    //oname = Convert.ToString( objrs.Fields.Item("ListName").Value);
                //    //oedit.Value = oname;

                //    objrs.MoveNext();
                //}
                oForm.Freeze(false);
                Global.SapApplication.StatusBar.SetText("PriceList Loaded Successfully", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
            }
            catch (Exception ex)
            {
                oForm.Freeze(false);
                Global.SapApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }
        #endregion

    #region Loading Warehouse
    //private void load_whsc(SAPbouiCOM.Form oForm)
    //    {
    //          try
    //        {
    //            oForm.Freeze(true);
    //            Global.SapApplication.StatusBar.SetText("Loading Warehouse List.Please wait...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
    //        SAPbouiCOM.Item matItem;
    //        SAPbouiCOM.Matrix omatwhs;
    //        SAPbouiCOM.Columns oColumns;
    //        SAPbouiCOM.Column ocolumn;
    //        SAPbouiCOM.Cell ocell;
    //        SAPbouiCOM.EditText oedit;
    //        string oname;
            
    //        matItem = oForm.Items.Item("mtxwhs");
    //        omatwhs = (SAPbouiCOM.Matrix)matItem.Specific;
    //        oColumns = omatwhs.Columns;


    //        string str = "select whscode,whsname from Owhs order by whsname";
    //        SAPbobsCOM.Recordset objrs = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
    //        objrs.DoQuery(str);
    //        for (int i = 1; i <= objrs.RecordCount; i++)
    //        {
    //            omatwhs.AddRow(1, i);

    //            ocolumn = oColumns.Item("sno");
    //            ocell = ocolumn.Cells.Item(i);
    //            oedit = (SAPbouiCOM.EditText)ocell.Specific;
    //            oname = Convert.ToString(i);
    //            oedit.Value = oname;

    //            ocolumn = oColumns.Item("whscode");
    //            ocell = ocolumn.Cells.Item(i);
    //            oedit = (SAPbouiCOM.EditText)ocell.Specific;
    //            oname = Convert.ToString(objrs.Fields.Item("whscode").Value);
    //            oedit.Value = oname;

    //            ocolumn = oColumns.Item("whsname");
    //            ocell = ocolumn.Cells.Item(i);
    //            oedit = (SAPbouiCOM.EditText)ocell.Specific;
    //            oname = Convert.ToString(objrs.Fields.Item("whsname").Value);
    //            oedit.Value = oname;

    //            objrs.MoveNext();
    //        }
    //        oForm.Freeze(false);
    //        Global.SapApplication.StatusBar.SetText("Warehouse List Loaded Successfully", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
    //    }
    //    catch (Exception ex)
    //    {
    //        oForm.Freeze(false);
    //        Global.SapApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
    //    }
    //}

        public void load_whsc(SAPbouiCOM.Form oForm)
        {
            try
            {
                oForm.Freeze(true);
                Global.SapApplication.StatusBar.SetText("Loading Warehouse List.Please wait...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                SAPbouiCOM.Item matItem;
                SAPbouiCOM.Matrix omatwhs;
                SAPbouiCOM.Columns oColumns;
                //SAPbouiCOM.Column ocolumn;
                //SAPbouiCOM.Cell ocell;
                //SAPbouiCOM.EditText oedit;
                //string oname;
                SAPbouiCOM.DBDataSource odbdsDetails;
                SAPbouiCOM.DBDataSource odbdsDetails1;
                matItem = oForm.Items.Item("mtxwhs");
                omatwhs = (SAPbouiCOM.Matrix)matItem.Specific;
                oColumns = omatwhs.Columns;

                odbdsDetails = oForm.DataSources.DBDataSources.Item("OWHS");
                odbdsDetails1 = oForm.DataSources.DBDataSources.Item("OITW");

                string str = "select ROW_NUMBER() OVER (ORDER BY whsname) Rownum,whscode,whsname from Owhs order by whsname";
                SAPbobsCOM.Recordset objrs = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                objrs.DoQuery(str);
                while (!objrs.EoF)
                {
                    omatwhs.AddRow(1, omatwhs.VisualRowCount);
                    omatwhs.GetLineData(omatwhs.VisualRowCount);
                    odbdsDetails1.SetValue("WhsCode", 0, Convert.ToString(objrs.Fields.Item("Rownum").Value));
                    odbdsDetails.SetValue("WhsCode", 0, Convert.ToString(objrs.Fields.Item("whscode").Value));
                    odbdsDetails.SetValue("WhsName", 0, Convert.ToString(objrs.Fields.Item("whsname").Value));
                    omatwhs.SetLineData(omatwhs.VisualRowCount);
                    //((SAPbouiCOM.EditText)omatwhs.Columns.Item("sno").Cells.Item(1).Specific).Value = Convert.ToString(objrs.Fields.Item("Rownum").Value);
                    //((SAPbouiCOM.EditText)omatwhs.Columns.Item("whscode").Cells.Item(1).Specific).Value = Convert.ToString(objrs.Fields.Item("whscode").Value);
                    //((SAPbouiCOM.EditText)omatwhs.Columns.Item("whsname").Cells.Item(1).Specific).Value = Convert.ToString(objrs.Fields.Item("whsname").Value);
                    objrs.MoveNext();
                }

                //for (int i = 1; i <= objrs.RecordCount; i++)
                //{
                //    omatwhs.AddRow(1, i);
                //    ((SAPbouiCOM.EditText)omatwhs.Columns.Item("sno").Cells.Item(i).Specific).Value = Convert.ToString(objrs.Fields.Item("Rownum").Value);
                //    ((SAPbouiCOM.EditText)omatwhs.Columns.Item("whscode").Cells.Item(i).Specific).Value = Convert.ToString(objrs.Fields.Item("whscode").Value);
                //    ((SAPbouiCOM.EditText)omatwhs.Columns.Item("whsname").Cells.Item(i).Specific).Value = Convert.ToString(objrs.Fields.Item("whsname").Value);
                //    //ocolumn = oColumns.Item("sno");
                //    //ocell = ocolumn.Cells.Item(i);
                //    //oedit = (SAPbouiCOM.EditText)ocell.Specific;
                //    //oname = Convert.ToString(i);
                //    //oedit.Value = oname;

                //    //ocolumn = oColumns.Item("whscode");
                //    //ocell = ocolumn.Cells.Item(i);
                //    //oedit = (SAPbouiCOM.EditText)ocell.Specific;
                //    //oname = Convert.ToString(objrs.Fields.Item("whscode").Value);
                //    //oedit.Value = oname;

                //    //ocolumn = oColumns.Item("whsname");
                //    //ocell = ocolumn.Cells.Item(i);
                //    //oedit = (SAPbouiCOM.EditText)ocell.Specific;
                //    //oname = Convert.ToString(objrs.Fields.Item("whsname").Value);
                //    //oedit.Value = oname;

                //    objrs.MoveNext();
                //}
                oForm.Freeze(false);
                Global.SapApplication.StatusBar.SetText("Warehouse List Loaded Successfully", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
            }
            catch (Exception ex)
            {
                oForm.Freeze(false);
                Global.SapApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }
    #endregion
}

    }

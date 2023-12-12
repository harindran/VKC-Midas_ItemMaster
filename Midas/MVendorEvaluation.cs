using System;
using System.Collections.Generic;
using System.Text;

namespace VKC
{
    class MVendorEvaluation
    {
        General gen = new General();

        #region Singleton

        private static MVendorEvaluation instance;

        public static MVendorEvaluation Instance
        {
            get
            {
                if (instance == null) instance = new MVendorEvaluation();

                return instance;
            }
        }

        #endregion

        public MVendorEvaluation()
        {
            VVendorEvaluation vw = VVendorEvaluation.Instance;
        }
        #region Initalsetting

        internal void Initalsetting(SAPbouiCOM.Form frmInit)
        {
            try
            {
                ////frmInit.DataSources.UserDataSources.Add("usdSlno", SAPbouiCOM.BoDataType.dt_SHORT_NUMBER, 8);
                ////frmInit.DataSources.UserDataSources.Add("usdVCode", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 20);
                ////frmInit.DataSources.UserDataSources.Add("usdVName",SAPbouiCOM.BoDataType.dt_LONG_TEXT,200);
                ////frmInit.DataSources.UserDataSources.Add("usdCapaity",SAPbouiCOM.BoDataType.dt_PRICE,5);
                ////frmInit.DataSources.UserDataSources.Add("usdQty",SAPbouiCOM.BoDataType.dt_QUANTITY,5);
                ////frmInit.DataSources.UserDataSources.Add("usdBalCpty",SAPbouiCOM.BoDataType.dt_PRICE,5);
                ////frmInit.DataSources.UserDataSources.Add("usdLeadTm",SAPbouiCOM.BoDataType.dt_SHORT_NUMBER,5);
                ////frmInit.DataSources.UserDataSources.Add("usdLPPrice",SAPbouiCOM.BoDataType.dt_PRICE,5);
                ////frmInit.DataSources.UserDataSources.Add("usdLCost",SAPbouiCOM.BoDataType.dt_PRICE,5);
               
                       
            }
            catch (Exception ex)
            {
                
            }
        }

        # endregion Initalsetting
        #region Select Vendor Data
        public void SelectVendorData(SAPbouiCOM.ItemEvent val)
        {
            try
            {
                SAPbouiCOM.Form frmVendor = Global.SapApplication.Forms.Item(val.FormUID);
                SAPbouiCOM.DBDataSource ODB_VEND = frmVendor.DataSources.DBDataSources.Item("OITM");
                string strItemCode = ODB_VEND.GetValue("ItemCode", 0);
                SAPbouiCOM.Matrix mtxVendor = (SAPbouiCOM.Matrix)frmVendor.Items.Item("mtxVendor").Specific;

                SAPbobsCOM.Recordset rdsVendor = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);


////                string strQuery = "with VendorEval As (select Itemcode,vendorCode as CardCode, 0 Price, " +
//// " isnull(U_Capacity,0)U_Capacity,isnull(U_LCost,0)U_LCost,isnull(U_LTime,0)U_LTime,0 OpenQty " +
//// " from ITM2 UNION(select itemcode , cardcode,(select top 1 Price from PCH1 inner join OPCH on PCH1.DocEntry =OPCH.DocEntry  group by  " +
////" ItemCode,PCH1.DocEntry,Price,OPCH.CardCode,PCH1.LineNum having PCH1.DocEntry = MAX(PCH1.DocEntry) and OPCH.CardCode= a.CardCode and  " +
//// " ItemCode = b.ItemCode order by PCH1.DocEntry  desc),0,0,0,0 from OPCH a inner join PCH1 b on a.DocEntry = b.DocEntry " +
//// "  group by CardCode,ItemCode  having ItemCode='" + strItemCode.ToString().Trim() + "') " +
//// "  UNION(select Itemcode,CardCode, 0,0,0,0, SUM(isnull(OpenQty,0))OpenQty from POR1 " +
//// "   inner join OPOR on POR1.DocEntry = OPOR.DocEntry group by ItemCode,CardCode,OpenQty )) " +
//// "   select CardCode,(select CardName from OCRD where CardCode = VendorEval.CardCode)CardName " +
////  "  ,SUM(U_Capacity)Capacity,SUM(OpenQty)OpenQty,SUM(U_Capacity)-SUM(OpenQty) as BalQty, " +
//// "   SUM(U_LTime)Time, SUM(Price)Price,SUM(U_LCost)Cost from VendorEval group by ItemCode, " +
//// "   CardCode having ItemCode = '" + strItemCode.ToString().Trim() + "'";
                string strQuery = @"with VendorEval As (select Itemcode,vendorCode as CardCode, 0 Price,  
                               isnull(U_Capacity,0)U_Capacity,isnull(U_LCost,0)U_LCost,isnull(U_LTime,0)U_LTime,0 OpenQty,
                               isnull(U_MaxQty,0)U_MaxQty,0 PurQty
                               from ITM2 
                               UNION(select itemcode , cardcode,(select top 1 Price from PCH1 inner join OPCH on 
                               PCH1.DocEntry =OPCH.DocEntry  group by   ItemCode,PCH1.DocEntry,Price,OPCH.CardCode
                              ,PCH1.LineNum having PCH1.DocEntry = MAX(PCH1.DocEntry) and OPCH.CardCode= a.CardCode and 
                               ItemCode = b.ItemCode order by PCH1.DocEntry  desc),0,0,0,0,0,0 from OPCH a inner join
                               PCH1 b on a.DocEntry = b.DocEntry   group by CardCode,ItemCode  having ItemCode='" + strItemCode.ToString().Trim() + "') " +

       @" UNION (  Select Itemcode,CardCode,0,0,0,0,0,0, SUM(Quantity)PurQty from PCH1 inner join OPCH on PCH1.DocEntry = OPCH.DocEntry  where OPCH.FinncPriod = ( Select AbsEntry  from OFPR where (select CONVERT(varchar(50), GETDATE(),112)) between F_RefDate and T_RefDate  ) 
       group by CardCode,ItemCode  having ItemCode='" + strItemCode.ToString().Trim() + "'   ) " +

       @" UNION(select Itemcode,CardCode, 0,0,0,0, SUM(isnull(OpenQty,0))OpenQty,0 ,0 from POR1 
       inner join OPOR on POR1.DocEntry = OPOR.DocEntry group by ItemCode,CardCode,OpenQty ))
       select CardCode,(select CardName from OCRD where CardCode = VendorEval.CardCode)CardName 
       ,SUM(U_Capacity)Capacity,SUM(OpenQty)OpenQty,SUM(U_Capacity)-SUM(OpenQty) as BalQty, 
       SUM(U_LTime)Time, SUM(Price)Price,SUM(U_LCost)Cost,SUM(U_MaxQty)MaxQty,SUM(PurQty)PurQty from VendorEval group by ItemCode,
       CardCode having ItemCode = '" + strItemCode.ToString().Trim() + "'";
                       
                       
                       

                rdsVendor.DoQuery(strQuery);
                mtxVendor.Clear();
                int i = 1;
                while (!rdsVendor.EoF)
                {
                    mtxVendor.AddRow(1, mtxVendor.RowCount);
                    mtxVendor.GetLineData(mtxVendor.RowCount);
                  frmVendor.DataSources.UserDataSources.Item("usdSlno").Value = i.ToString();
                  frmVendor.DataSources.UserDataSources.Item("usdVCode").Value = rdsVendor.Fields.Item("CardCode").Value.ToString();
                  frmVendor.DataSources.UserDataSources.Item("usdVName").Value = rdsVendor.Fields.Item("CardName").Value.ToString();
                  frmVendor.DataSources.UserDataSources.Item("usdCapaity").Value = rdsVendor.Fields.Item("Capacity").Value.ToString();
                  frmVendor.DataSources.UserDataSources.Item("usdQty").Value = rdsVendor.Fields.Item("OpenQty").Value.ToString();
                  frmVendor.DataSources.UserDataSources.Item("usdBalCpty").Value = rdsVendor.Fields.Item("BalQty").Value.ToString();
                  frmVendor.DataSources.UserDataSources.Item("usdLeadTm").Value = rdsVendor.Fields.Item("Time").Value.ToString();
                  frmVendor.DataSources.UserDataSources.Item("usdLPPrice").Value = rdsVendor.Fields.Item("Price").Value.ToString();
                  frmVendor.DataSources.UserDataSources.Item("usdLCost").Value = rdsVendor.Fields.Item("Cost").Value.ToString();
                  frmVendor.DataSources.UserDataSources.Item("usdMaxQty").Value = rdsVendor.Fields.Item("MaxQty").Value.ToString();
                  frmVendor.DataSources.UserDataSources.Item("usdPurQty").Value = rdsVendor.Fields.Item("PurQty").Value.ToString();
                    mtxVendor.SetLineData(mtxVendor.RowCount);
                    i++;
                    rdsVendor.MoveNext();
                }
                    

            }
            catch { }
        }
        #endregion
        ////public bool EnableColoums(SAPbouiCOM.Form oForm)
        ////{
        ////    try
        ////    {
        ////        SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)oForm.Items.Item("mtxVendor").Specific;
        ////        oMat.AddRow(1, oMat.RowCount);
        ////        SAPbouiCOM.Columns oColumns = oMat.Columns;
        ////        oColumns.Item("colVendor").Cells.Item(1).Click(SAPbouiCOM.BoCellClickType.ct_Regular, 0);

        ////        return true;
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        return false;
        ////    }
        ////}
        # region Choose From List
        public bool Choofromlist_Req(SAPbouiCOM.Form oForm, SAPbouiCOM.ItemEvent pVal)
        {
            try
            {

                //oForm.Freeze(true);
                SAPbouiCOM.ChooseFromListEvent OCFL_Evnt = (SAPbouiCOM.ChooseFromListEvent)(pVal);
                string strCFL_UID = OCFL_Evnt.ChooseFromListUID;
                SAPbouiCOM.ChooseFromList oCFL = oForm.ChooseFromLists.Item(strCFL_UID);
                SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)oForm.Items.Item("mtxVendor").Specific;
                SAPbouiCOM.DBDataSource ODB_VEND = oForm.DataSources.DBDataSources.Item("OITM");
                SAPbouiCOM.Columns oColumns = oMat.Columns;                
                 if (OCFL_Evnt.BeforeAction == false)
                {
                  
                    SAPbouiCOM.DataTable dtbCFL = null;
                    dtbCFL = OCFL_Evnt.SelectedObjects;
                    if (dtbCFL != null)
                    {
                        ODB_VEND.SetValue("ItemCode", 0, dtbCFL.GetValue("ItemCode", 0).ToString());

                    }
                   
                }
                oForm.Freeze(false);
                return true;
            }
            catch
            {
                oForm.Freeze(false);
                return false;
            }
        }
         #  endregion
       

        #region Check Matrix for Duplication
        internal bool ValidateSelection(SAPbouiCOM.Form oForm, string strSelectedVal)
        {
            try
            {
                string strMtxVal = "";
                bool boolVal = true;
                SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)oForm.Items.Item("mtxVendor").Specific;

                for (int i = 1; i <= oMat.RowCount; i++)
                {
                    oMat.GetLineData(i);
                    SAPbouiCOM.DBDataSource dDsVendor = oForm.DataSources.DBDataSources.Item("@NOR_VENDOREVAL");
                    strMtxVal = dDsVendor.GetValue("U_ItemCode", 0);
                    if (strMtxVal.Trim() == strSelectedVal.Trim())
                    {
                        boolVal = false;
                        break;
                    }
                    else
                    {
                        boolVal = true;
                    }
                }

                if (boolVal == false)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "ok", "", "");
                return false;
            }
        }
        #endregion

        #region Matrix Add Row
        public void Matrixrowadd(SAPbouiCOM.Matrix oMatrix, SAPbouiCOM.DBDataSource DbMat)
        {

            if (oMatrix.RowCount == 0)
            {
                oMatrix.AddRow(1, oMatrix.RowCount);
            }
            else
            {
                SAPbouiCOM.EditText oEdit = (SAPbouiCOM.EditText)oMatrix.Columns.Item("colCode").Cells.Item(oMatrix.RowCount).Specific;
                if (oEdit == null | oEdit.Value == "")
                {
                    oMatrix.AddRow(1, oMatrix.RowCount);
                }
                else if (oEdit.Value != "")
                    oMatrix.AddRow(1, oMatrix.RowCount);

                if (oMatrix.RowCount > 0)
                {
                    oMatrix.GetLineData(oMatrix.RowCount);
                    // DbMat.RemoveRecord(DbMat.Offset);
                    DbMat.InsertRecord(0);
                    oMatrix.SetLineData(oMatrix.RowCount);

                }
            }
        }
        #endregion

        //Created On 26-06-2012
        #region chagepaneVendor
        public void ChangePaneVendor(SAPbouiCOM.ItemEvent pVal)
        {
            SAPbobsCOM.Recordset oSelectOFPR = null, oSelectCINF = null, oUPDCINF = null;
            try
            {

                Global.SapApplication.Forms.ActiveForm.Freeze(true);

                Global.SapApplication.Forms.ActiveForm.PaneLevel = 45;
                Global.SapApplication.Forms.ActiveForm.Freeze(false);

            }
            catch (Exception e)
            {
                Global.SapApplication.MessageBox(e.Message, 1, "OK", "", "");
            }
        }
        #endregion
        #region click on item
        public void ClickOnItm(SAPbouiCOM.ItemEvent pVal)
        {
            SAPbouiCOM.Item oItem = null;
            try
            {
                SAPbouiCOM.Form NorForm = Global.SapApplication.Forms.Item(pVal.FormUID);
                addVendortab(NorForm);
                AddVendorMatrix(pVal);
               // AddButtonBrand(pVal);
                //oItem = NorForm.Items.Item("3");
                //oItem.Click(SAPbouiCOM.BoCellClickType.ct_Double);
                NorForm.PaneLevel = 1;

            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
            }
        }
        #endregion
        #region add Vendor tab
        public void addVendortab(SAPbouiCOM.Form oForm)
        {
            SAPbouiCOM.Folder oFolder;
            SAPbouiCOM.Item oItem;
            SAPbouiCOM.Item oItem1;
            try
            {
                oForm.Freeze(true);
                oItem = oForm.Items.Add("vendor", SAPbouiCOM.BoFormItemTypes.it_FOLDER);
                oFolder = (SAPbouiCOM.Folder)oItem.Specific;
                oItem.AffectsFormMode = false;
                oFolder.Caption = "Vendor Evaluation";
                oFolder.GroupWith("3");
                oItem.Width = 125;
                oItem1 = oForm.Items.Item("3");
                oItem.Left = oItem1.Left + oItem1.Width;
                oItem.Enabled = true;
                oItem.Visible = true;
                oForm.PaneLevel = 1;
                oForm.Freeze(false);
            }
            catch (Exception e)
            {
                Global.SapApplication.MessageBox(e.Message, 1, "Ok", "", "");
            }
        }
        #endregion
 
        #region ItemMaster Vendor Info

        internal void AddVendorMatrix(SAPbouiCOM.ItemEvent val)
        {
            try
            {
                SAPbouiCOM.Form form = Global.SapApplication.Forms.Item(val.FormUID);
                SAPbouiCOM.Form oForm = null;
                SAPbouiCOM.Item _itm_Ref1;
                SAPbouiCOM.Item itm_mat;
                SAPbouiCOM.Columns oColumns;
                SAPbouiCOM.Column oColumn;
                if (val.FormTypeEx == "150")
                {

                    SAPbouiCOM.Matrix MAT_Vendor;
                    _itm_Ref1 = form.Items.Item("16");
                    itm_mat = form.Items.Add("mtx_Vendor", SAPbouiCOM.BoFormItemTypes.it_MATRIX);
                    itm_mat.Left = 20;
                    itm_mat.Top = _itm_Ref1.Top + 40;
                    itm_mat.Width = 340;
                    itm_mat.Height = 180;
                    itm_mat.FromPane = 45;
                    itm_mat.ToPane = 45;
                    MAT_Vendor = (SAPbouiCOM.Matrix)itm_mat.Specific;
                    oColumns = MAT_Vendor.Columns;
                    oColumn = oColumns.Add("#", SAPbouiCOM.BoFormItemTypes.it_EDIT);
                    oColumn.TitleObject.Caption = " ";
                    oColumn.Width = 20;
                    oColumn.Editable = false;
                    SAPbouiCOM.Matrix _matUserFieldsPBrand = (SAPbouiCOM.Matrix)form.Items.Item("mtx_Vendor").Specific;


                    oColumns = MAT_Vendor.Columns;
                    oColumn = oColumns.Add("col_Vendor", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON);
                    SAPbouiCOM.Column _edtVendor = (SAPbouiCOM.Column)_matUserFieldsPBrand.Columns.Item("col_Vendor");
                  
                    form.DataSources.UserDataSources.Add("CVendor", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 50);
                    _edtVendor.DataBind.SetBound(true, "", "CVendor");
                    oColumn.Description = "col_Vendor";
                    oColumn.TitleObject.Caption = "Vendor";
                    oColumn.Width = 80;
                    oColumn.Editable = false;
                    oColumn.DisplayDesc = true;

                    oColumns = MAT_Vendor.Columns;
                    oColumn = oColumns.Add("col_Cpcty", SAPbouiCOM.BoFormItemTypes.it_EDIT);
                    SAPbouiCOM.Column _edtCapacity = (SAPbouiCOM.Column)_matUserFieldsPBrand.Columns.Item("col_Cpcty");
                    form.DataSources.UserDataSources.Add("Capacity", SAPbouiCOM.BoDataType.dt_PRICE, 5);
                    _edtCapacity.DataBind.SetBound(true, "", "Capacity");
                    oColumn.Description = "col_Cpcty";
                    oColumn.TitleObject.Caption = "Capacity";
                    oColumn.Width = 200;
                    oColumn.Editable = true;
                    oColumn.DisplayDesc = true;

                    oColumns = MAT_Vendor.Columns;
                    oColumn = oColumns.Add("col_LTime", SAPbouiCOM.BoFormItemTypes.it_EDIT);
                    SAPbouiCOM.Column _edtLeadTime = (SAPbouiCOM.Column)_matUserFieldsPBrand.Columns.Item("col_LTime");
                    form.DataSources.UserDataSources.Add("LeadTime", SAPbouiCOM.BoDataType.dt_SHORT_NUMBER, 8);
                    _edtLeadTime.DataBind.SetBound(true, "", "LeadTime");
                    oColumn.Description = "col_LTime";
                    oColumn.TitleObject.Caption = "Lead Time";
                    oColumn.Width = 200;
                    oColumn.Editable = true;
                    oColumn.DisplayDesc = true;

                    oColumns = MAT_Vendor.Columns;
                    oColumn = oColumns.Add("col_Cost", SAPbouiCOM.BoFormItemTypes.it_EDIT);
                    SAPbouiCOM.Column _edtCost = (SAPbouiCOM.Column)_matUserFieldsPBrand.Columns.Item("col_Cost");
                    form.DataSources.UserDataSources.Add("LCost", SAPbouiCOM.BoDataType.dt_PRICE, 5);
                    _edtCost.DataBind.SetBound(true, "", "LCost");
                    oColumn.Description = "col_Cost";
                    oColumn.TitleObject.Caption = "Landed Cost";
                    oColumn.Width = 200;
                    oColumn.Editable = true;
                    oColumn.DisplayDesc = true;

                    oColumns = MAT_Vendor.Columns;
                    oColumn = oColumns.Add("col_MaxQty", SAPbouiCOM.BoFormItemTypes.it_EDIT);
                    SAPbouiCOM.Column _edtQty = (SAPbouiCOM.Column)_matUserFieldsPBrand.Columns.Item("col_MaxQty");
                    form.DataSources.UserDataSources.Add("MaxQty", SAPbouiCOM.BoDataType.dt_PRICE, 5);
                    _edtQty.DataBind.SetBound(true, "", "MaxQty");
                    oColumn.Description = "col_MaxQty";
                    oColumn.TitleObject.Caption = "Max Order Qty";
                    oColumn.Width = 200;
                    oColumn.Editable = true;
                    oColumn.DisplayDesc = true;

                    //--------------Added By Reena On 04-07 -2013---For Job Order--------------------------//


                    oColumns = MAT_Vendor.Columns;
                    oColumn = oColumns.Add("col_PLTime", SAPbouiCOM.BoFormItemTypes.it_EDIT);
                    SAPbouiCOM.Column _edtPdnL = (SAPbouiCOM.Column)_matUserFieldsPBrand.Columns.Item("col_PLTime");
                    form.DataSources.UserDataSources.Add("PdnLTime", SAPbouiCOM.BoDataType.dt_SHORT_NUMBER, 8);
                    _edtPdnL.DataBind.SetBound(true, "", "PdnLTime");
                    oColumn.Description = "col_PLTime";
                    oColumn.TitleObject.Caption = "OutSource Lead Time";
                    oColumn.Width = 200;
                    oColumn.Editable = true;
                    oColumn.DisplayDesc = true;


                    //--------------Added By Reena On 30-08 -2013---For Job Order--------------------------//


                    oColumns = MAT_Vendor.Columns;
                    oColumn = oColumns.Add("col_Stage", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX);
                    SAPbouiCOM.Column _edtStage = (SAPbouiCOM.Column)_matUserFieldsPBrand.Columns.Item("col_Stage");
                    form.DataSources.UserDataSources.Add("Stage", SAPbouiCOM.BoDataType.dt_SHORT_NUMBER, 8);
                    _edtStage.DataBind.SetBound(true, "", "Stage");
                    oColumn.Description = "col_Stage";
                    oColumn.TitleObject.Caption = "Prodn Stage";
                    oColumn.Width = 200;
                    oColumn.Editable = true;
                    oColumn.DisplayDesc = true;
                }
                form.Freeze(false);
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        ////#region Fill Combo
        ////internal bool FillCombo(SAPbouiCOM.Form frm)
        ////{
        ////    try
        ////    {
        ////      //  SAPbouiCOM.ComboBox _txtStage = (SAPbouiCOM.ComboBox)matrx.Columns.Item("col_Stage").Cells.Item(matrx.RowCount).Specific;
        ////        SAPbobsCOM.Recordset rsType = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

        ////        string strQry = "select Code,Name from [@STAGES] order by code";
        ////        rsType.DoQuery(strQry);
        ////        while (!rsType.EoF)
        ////        {

        ////            cmbType.ValidValues.Add(rsType.Fields.Item(0).Value.ToString(), rsType.Fields.Item(1).Value.ToString());
        ////            rsType.MoveNext();

        ////        }
        ////        cmbType.Select(2, SAPbouiCOM.BoSearchKey.psk_Index);
        ////    }
        ////    catch
        ////    {
        ////    }
        ////    return true;
        ////}
        ////#endregion


        #region fillMatrix
        internal void FillVendorMatrix()
        {
            SAPbouiCOM.Form form = Global.SapApplication.Forms.ActiveForm;
            try
            {

                int ini = 0;

                form.Freeze(true);
                SAPbouiCOM.Matrix matrx = (SAPbouiCOM.Matrix)form.Items.Item("mtx_Vendor").Specific;
                SAPbouiCOM.EditText txtItem = (SAPbouiCOM.EditText)form.Items.Item("5").Specific;
                SAPbobsCOM.Recordset oRecordSet1 = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                if (matrx.RowCount == 0)
                {
                    matrx.AddRow(1, 0);

                    SAPbobsCOM.Recordset oRecordSet = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                    SAPbouiCOM.EditText _txtVendor = (SAPbouiCOM.EditText)matrx.Columns.Item("col_Vendor").Cells.Item(matrx.RowCount).Specific;
                    SAPbouiCOM.EditText _txtCapacity = (SAPbouiCOM.EditText)matrx.Columns.Item("col_Cpcty").Cells.Item(matrx.RowCount).Specific;
                    SAPbouiCOM.EditText _txtLeadTime = (SAPbouiCOM.EditText)matrx.Columns.Item("col_LTime").Cells.Item(matrx.RowCount).Specific;
                    SAPbouiCOM.EditText _txtCost = (SAPbouiCOM.EditText)matrx.Columns.Item("col_Cost").Cells.Item(matrx.RowCount).Specific;
                    SAPbouiCOM.EditText _txtMaxQty = (SAPbouiCOM.EditText)matrx.Columns.Item("col_MaxQty").Cells.Item(matrx.RowCount).Specific;
                    SAPbouiCOM.EditText _txtPdnLTime = (SAPbouiCOM.EditText)matrx.Columns.Item("col_PLTime").Cells.Item(matrx.RowCount).Specific;
                    SAPbouiCOM.ComboBox _cmbStage = (SAPbouiCOM.ComboBox)matrx.Columns.Item("col_Stage").Cells.Item(matrx.RowCount).Specific;
                    
                    SAPbobsCOM.Recordset rsType = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    if (_cmbStage.ValidValues.Count==0)
                    {
                        string strQry = "select Code,Name from [@STAGES] order by code";
                        rsType.DoQuery(strQry);
                        _cmbStage.ValidValues.Add("-1", "");                   
                        while (!rsType.EoF)
                        {
                            if(rsType.Fields.Item(0).Value.ToString() =="3" | rsType.Fields.Item(0).Value.ToString() =="4"|rsType.Fields.Item(0).Value.ToString() =="5")
                          
                           
                            _cmbStage.ValidValues.Add(rsType.Fields.Item(0).Value.ToString(), rsType.Fields.Item(1).Value.ToString());
                            rsType.MoveNext();

                        }
                        _cmbStage.Select(2, SAPbouiCOM.BoSearchKey.psk_Index);
                    }

                    string _str_Query = " select VendorCode,isnull(U_Capacity,0)U_Capacity,isnull(U_LTime,0)U_LTime,isnull(U_LCost,0)U_LCost,isnull(U_MaxQty,0)U_MaxQty,isnull(U_PdnLTime,0)U_PdnLTime,isnull(U_Stage,'-1')U_Stage from ITM2 where ItemCode = '" + txtItem.Value.ToString() + "'  ";
                    oRecordSet.DoQuery(_str_Query);
                    while (!oRecordSet.EoF)
                    {
                        matrx.AddRow(1, 0);
                        _txtVendor.Value = oRecordSet.Fields.Item(0).Value.ToString();
                        _txtCapacity.Value = oRecordSet.Fields.Item(1).Value.ToString();
                        _txtLeadTime.Value = oRecordSet.Fields.Item(2).Value.ToString();
                        _txtCost.Value = oRecordSet.Fields.Item(3).Value.ToString();
                        _txtMaxQty.Value = oRecordSet.Fields.Item(4).Value.ToString();
                        _txtPdnLTime.Value = oRecordSet.Fields.Item(5).Value.ToString();
                        string _strStage = oRecordSet.Fields.Item(6).Value.ToString();
                        _cmbStage.Select(_strStage, SAPbouiCOM.BoSearchKey.psk_ByValue);
                        oRecordSet.MoveNext();
                    }
                }
               
            
                form.Freeze(false);

            }
            catch (Exception ex)
            {
                form.Freeze(false);
                Global.SapApplication.MessageBox(ex.Message, 1, "ok", "", "");
            }
        }


               #endregion
        #region Update ITM2
        internal void UpdateVendorEval()
        {
            SAPbouiCOM.Form form = Global.SapApplication.Forms.ActiveForm;
            try
            {

                int ini = 0;

                form.Freeze(true);
                SAPbouiCOM.Matrix matrx = (SAPbouiCOM.Matrix)form.Items.Item("mtx_Vendor").Specific;
                 SAPbouiCOM.EditText txtItem = (SAPbouiCOM.EditText)form.Items.Item("5").Specific;
                SAPbobsCOM.Recordset oRecordSet = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                int Count = matrx.RowCount;
                while(Count >0)
                {

                    SAPbouiCOM.EditText _txtVendor = (SAPbouiCOM.EditText)matrx.Columns.Item("col_Vendor").Cells.Item(Count).Specific;
                    SAPbouiCOM.EditText _txtCapacity = (SAPbouiCOM.EditText)matrx.Columns.Item("col_Cpcty").Cells.Item(Count).Specific;
                    SAPbouiCOM.EditText _txtLeadTime = (SAPbouiCOM.EditText)matrx.Columns.Item("col_LTime").Cells.Item(Count).Specific;
                    SAPbouiCOM.EditText _txtCost = (SAPbouiCOM.EditText)matrx.Columns.Item("col_Cost").Cells.Item(Count).Specific;
                    SAPbouiCOM.EditText _txtMaxQty = (SAPbouiCOM.EditText)matrx.Columns.Item("col_MaxQty").Cells.Item(Count).Specific;
                    SAPbouiCOM.EditText _txtPdnLTime = (SAPbouiCOM.EditText)matrx.Columns.Item("col_PLTime").Cells.Item(Count).Specific;
                    SAPbouiCOM.ComboBox _txtStage = (SAPbouiCOM.ComboBox)matrx.Columns.Item("col_Stage").Cells.Item(Count).Specific;

                    if(_txtVendor.Value.ToString() != "")
                    {
                        string _str_Query = "update ITM2 set U_Capacity='" + _txtCapacity.Value.ToString() + "',U_LTime ='" + _txtLeadTime.Value.ToString() + "',U_LCost='" + _txtCost.Value.ToString() + "',U_MaxQty='" + _txtMaxQty.Value.ToString() + "',U_PdnLTime ='" + _txtPdnLTime.Value.ToString() + "',U_Stage ='" + _txtStage.Value.ToString() +"' where ItemCode = '" + txtItem.Value.ToString() + "' and VendorCode = '" + _txtVendor.Value.ToString() + "' ";
                          oRecordSet.DoQuery(_str_Query);
                    }
                    Count--;
                    }
                    form.Freeze(false);
                }
            


               

       
            catch (Exception ex)
            {
                form.Freeze(false);
                Global.SapApplication.MessageBox(ex.Message, 1, "ok", "", "");
            }
        }


        #endregion

        #region Delete Un Wanted Row
        internal bool DeleteUnWantedRow(SAPbouiCOM.Form oForm)
        {
            try
            {
                SAPbouiCOM.Matrix _mat = (SAPbouiCOM.Matrix)oForm.Items.Item("mtx_Vendor").Specific;
                for (int i = 1; i <= _mat.RowCount; i++)
                {

                    SAPbouiCOM.EditText col1 = (SAPbouiCOM.EditText)_mat.Columns.Item("col_Vendor").Cells.Item(i).Specific;

                    string StrCatVal = col1.Value.ToString().Trim();
                    if (StrCatVal == "")
                    {
                        _mat.DeleteRow(i);
                    }
                }
                return true;

            }
            catch (Exception ex)
            {
                return false;

            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace VKC
{
    class MPurchaseRequisition
    {
        General gen = new General();

        #region Singleton

        private static MPurchaseRequisition instance;

        public static MPurchaseRequisition Instance
        {
            get
            {
                if (instance == null) instance = new MPurchaseRequisition();

                return instance;
            }
        }

        #endregion

        public MPurchaseRequisition()
        {
            VPurchaseRequisition vp = VPurchaseRequisition.Instance;
        }

        #region Initalsetting

        internal void Initalsetting(SAPbouiCOM.Form frmInit)
        {
            try
            {
               
                frmInit.DataBrowser.BrowseBy = "txtDoc";
                BeforeAdd(frmInit, Global.SapCompany);
                EnableColoums(frmInit);
                EnableItems(frmInit);
                DisableButton(frmInit);
                if (frmInit.DataSources.UserDataSources.Count == 0)//cmbUom
                frmInit.DataSources.UserDataSources.Add("Doc", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 30);
                SAPbouiCOM.DBDataSource dDsPurchase = frmInit.DataSources.DBDataSources.Item("@OPRQ");
                //frmInit.Items.Item("U_User").Enabled = true;
                //dDsPurchase.SetValue("U_User", 0, Global.SapCompany.UserName);
                //frmInit.Items.Item("U_User").Enabled = false;
                frmInit.Items.Item("16").Enabled = true;
                SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)frmInit.Items.Item("mtxPurReq").Specific;
               
                oMat.Columns.Item("cmbUom").Editable = true;
                frmInit.Items.Item("txtVendor").Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                //SAPbouiCOM.EditText txtOpenLC = (SAPbouiCOM.EditText)oMat.Columns.Item("colOpenLC").Cells.Item(val.Row).Specific;
            }
            catch (Exception ex)
            {
                //Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
            }
        }

        # endregion Initalsetting


        public void BeforeAdd(SAPbouiCOM.Form oForm, SAPbobsCOM.Company SapCompany)
        {
           // MPurchaseRequisition.Instance.SetStatus();
            string strQry = "select NextNumber from NNM1 where ObjectCode='OBJ_PREQ'";
            SAPbobsCOM.Recordset oRsDoc = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
            oRsDoc.DoQuery(strQry);
            if (oRsDoc.RecordCount > 0)
            {
                oForm.DataSources.DBDataSources.Item("@OPRQ").SetValue("DocNum", 0, oRsDoc.Fields.Item("NextNumber").Value.ToString());

            }
            IFormatProvider ifd = new System.Globalization.CultureInfo("en-us", true);
            oForm.DataSources.DBDataSources.Item("@OPRQ").SetValue("U_DtExpect", 0, String.Format("{0:yyyyMMdd}", System.DateTime.Now));
            oForm.DataSources.DBDataSources.Item("@OPRQ").SetValue("U_PostDate", 0, String.Format("{0:yyyyMMdd}", System.DateTime.Now));
            oForm.DataSources.DBDataSources.Item("@OPRQ").SetValue("U_DocStatu", 0, "Open");
        }
        public void RefreshDoc(SAPbouiCOM.Form oForm, SAPbobsCOM.Company oCompany)
        {
            string strQry = "select NextNumber from NNM1 where ObjectCode='OBJ_PREQ'";
            SAPbobsCOM.Recordset oRsDoc = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
            oRsDoc.DoQuery(strQry);
            if (oRsDoc.RecordCount > 0)
            {
                oForm.DataSources.DBDataSources.Item("@OPRQ").SetValue("DocNum", 0, oRsDoc.Fields.Item("NextNumber").Value.ToString());

            }
        }


        #region Matrix Add Row
        public void Matrixrowadd(SAPbouiCOM.Matrix oMatrix, SAPbouiCOM.DBDataSource DbMat)
        {

            if (oMatrix.RowCount == 0)
            {
                oMatrix.AddRow(1, oMatrix.RowCount);
            }
            else
            {
                SAPbouiCOM.EditText oEdit = (SAPbouiCOM.EditText)oMatrix.Columns.Item("colItmCode").Cells.Item(oMatrix.RowCount).Specific;
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




            //Line Number Increament
            int doc = 1;
            SAPbouiCOM.EditText oEdit1 = (SAPbouiCOM.EditText)oMatrix.Columns.Item("V_-1").Cells.Item(oMatrix.RowCount).Specific;
            if (oMatrix.RowCount > 1)
            {
                doc += oMatrix.RowCount - 1;
                // doc++;
            }
            oEdit1.Value = System.Convert.ToString(doc);
        }
        #endregion
        #region Status Creation 
        public void SetStatus()
        {
            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
            SAPbouiCOM.EditText oStatus = (SAPbouiCOM.EditText)oForm.Items.Item("txtStatus").Specific;
            oStatus.Value = "Open";
        }
        #endregion

        # region Choose From List
        public bool Choofromlist_Req(SAPbouiCOM.Form oForm, SAPbouiCOM.ItemEvent pVal)
        {
            try
            {
               
                //oForm.Freeze(true);
                SAPbouiCOM.ChooseFromListEvent OCFL_Evnt = (SAPbouiCOM.ChooseFromListEvent)(pVal);
                string strCFL_UID = OCFL_Evnt.ChooseFromListUID;
                SAPbouiCOM.ChooseFromList oCFL = oForm.ChooseFromLists.Item(strCFL_UID);
                SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)oForm.Items.Item("mtxPurReq").Specific;
                SAPbouiCOM.DBDataSource ODB_PEQ1 = oForm.DataSources.DBDataSources.Item("@PRQ1");
                SAPbouiCOM.Columns oColumns = oMat.Columns;
                ODB_PEQ1.Clear();
                ODB_PEQ1.InsertRecord(0);
                SAPbouiCOM.Condition Con = null;
                SAPbouiCOM.Conditions Cons = null;

                #region BeforeAction == true
                if (OCFL_Evnt.BeforeAction == true)
                {
                    Cons = new SAPbouiCOM.Conditions();

                    if (pVal.FormTypeEx == "PurchaseRequisition")
                    {
                        if (pVal.ItemUID == "BpCode")
                        {
                            Con = Cons.Add();
                            Con.Alias = "CardType";
                            Con.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                            Con.CondVal = "S";
                            oCFL.SetConditions(Cons);
                        }
                        else if (pVal.ItemUID == "ContPerson")
                        {
                            string CardCode = oForm.DataSources.DBDataSources.Item("@OPRQ").GetValue("U_BpCode", 0);
                            Con = Cons.Add();
                            Con.Alias = "CardCode";
                            Con.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                            Con.CondVal = CardCode;
                            oCFL.SetConditions(Cons);
                        }
                    }
                }
                #endregion

                else if (OCFL_Evnt.BeforeAction == false)
                {
                    SAPbouiCOM.DataTable oDataTable = null;
                    oDataTable = OCFL_Evnt.SelectedObjects;

                    try
                    {
                        if (oDataTable != null)
                        {
                            string billno = System.Convert.ToString(oDataTable.GetValue(0, 0));
                            #region New Purchase
                            if (pVal.FormTypeEx == "PurchaseRequisition")
                            {
                                if (pVal.ItemUID == "BpCode")
                                {
                                    string CardCode = "", CardName = "";
                                    SAPbouiCOM.Matrix MatPROD = (SAPbouiCOM.Matrix)(oForm.Items.Item("mtxPurReq").Specific);
                                    CardCode = Convert.ToString(oDataTable.GetValue(0, 0));
                                    CardName = Convert.ToString(oDataTable.GetValue(1, 0));
                                    oForm.DataSources.DBDataSources.Item("@OPRQ").SetValue("U_BpCode", 0, CardCode);
                                    oForm.DataSources.DBDataSources.Item("@OPRQ").SetValue("U_BpName", 0, CardName);
                                    //OpenStatusReq(oForm);
                                    if (MatPROD.RowCount == 0)
                                    {
                                        MatPROD.AddRow(1, 1);
                                      
                                    }
                                    oForm.DataSources.DBDataSources.Item("@PRQ1").Clear();

                                }
                                else if (pVal.ItemUID == "ContPerson")
                                {
                                    string ContactPerson = "";
                                    ContactPerson = Convert.ToString(oDataTable.GetValue(2, 0));
                                    oForm.DataSources.DBDataSources.Item("@OPRQ").SetValue("U_Contpers", 0, ContactPerson);
                                }
                                else if (pVal.ItemUID == "mtxPurReq")
                                {
                                    SAPbouiCOM.Matrix oMatrix = (SAPbouiCOM.Matrix)(oForm.Items.Item("mtxPurReq").Specific);
                                    if (pVal.ColUID == "colItmCode")
                                    {
                                       // oForm.Freeze(true);
                                        oForm.DataSources.DBDataSources.Item("@PRQ1").Clear();
                                        oMatrix.GetLineData(pVal.Row);
                                        string StrDocdate = "";
                                        StrDocdate = Convert.ToString(oForm.DataSources.DBDataSources.Item("@OPRQ").GetValue("U_PostDate", 0));
                                       // oMatrix.FlushToDataSource();
                                        string val1 = Convert.ToString(oForm.DataSources.DBDataSources.Item("@PRQ1").GetValue("U_ItemCode", 0));
                                        oForm.Freeze(false);
                                        bool boolValidate = ValidateSelection(oForm, oDataTable.GetValue(0, 0).ToString());

                                        if (boolValidate == false)
                                        {
                                            Global.SapApplication.StatusBar.SetText("Duplicate ItemCodes", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                                            return false;
                                        }

                                       // string strItemcode = "";
                                        // strItemcode = Convert.ToString(oDataTable.GetValue(0, 0));
                                        oForm.DataSources.DBDataSources.Item("@PRQ1").InsertRecord(0);
                                        if (oMat.RowCount > 0)
                                        {
                                           for (int i = 1; i <= oMat.RowCount; i++)
                                           {
                                               oMat.GetLineData(i);
                                                                                            
                                               oMat.SetLineData(i);
                                               
                                            }
                                        }
                                        
                                        
                                        oForm.DataSources.DBDataSources.Item("@PRQ1").SetValue("U_ItemCode", 0, oDataTable.GetValue(0, 0).ToString());
                                        oForm.DataSources.DBDataSources.Item("@PRQ1").SetValue("U_ItemName", 0, oDataTable.GetValue(1, 0).ToString());                                    
                                                                             
                                        oForm.DataSources.DBDataSources.Item("@PRQ1").SetValue("U_Qty", 0, "0.00");
                                        oMatrix.SetLineData(pVal.Row);
                                        oForm.DataSources.DBDataSources.Item("@PRQ1").Clear();
                                        if (pVal.Row == oMatrix.RowCount)
                                        {
                                            Matrixrowadd(oMatrix, oForm.DataSources.DBDataSources.Item("@PRQ1"));
                                            oForm.DataSources.DBDataSources.Item("@PRQ1").Clear();
                                        }
                                        oForm.Freeze(false);
                                    }
                                    if (pVal.ColUID == "A5")
                                    {
                                        oMatrix.GetLineData(pVal.Row);
                                        oForm.DataSources.DBDataSources.Item("@PRQ1").InsertRecord(0);
                                        oForm.DataSources.DBDataSources.Item("@PRQ1").SetValue("U_TaxCode", 0, oDataTable.GetValue(0, 0).ToString());
                                        oMatrix.SetLineData(pVal.Row);
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                    catch (Exception e)
                    {
                        Global.SapApplication.MessageBox(e.Message, 1, "OK", "", "");
                    }
                }
                oForm.Freeze(false);
                if (pVal.FormMode == (int)SAPbouiCOM.BoFormMode.fm_OK_MODE | pVal.FormMode == (int)SAPbouiCOM.BoFormMode.fm_UPDATE_MODE)
                {
                    oForm.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
                }
                return true;
            }
            catch
            {
               // oForm = Global.SapApplication.Forms.GetFormByTypeAndCount(2000080002, 1);
                oForm.Freeze(false);
                return false;
            }
        }
        #  endregion
        #region "Check Before Load"
        public bool CheckBeforeLoad(int DocEntry)
        {
            SAPbobsCOM.Recordset rsFill = null;
            rsFill = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
            string PH_Query = "select count(*) from POR1 where U_BEntry1 = '" + DocEntry + "'";
            rsFill.DoQuery(PH_Query);
            if (Convert.ToInt32( rsFill.Fields.Item(0).Value.ToString()) > 0)
            {
                return true;
            }
            else
                return false;
        }
        #endregion
        #region "Fill Employee Combo"
        public void FillCombos(bool Blank)
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)oForm.Items.Item("cmbReqBy").Specific;
                SAPbouiCOM.EditText oTextBox = (SAPbouiCOM.EditText)oForm.Items.Item("txtDept").Specific;
                SAPbobsCOM.Recordset rsFill = null;

                rsFill = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                gen.ClearCombo(oComboItem);
                string PH_Query = "SELECT empID,(E.firstName + ' ' + E.lastName)as Name FROM OHEM E INNER JOIN OUDP D ON E.dept = D.Code where D.Name='" + oTextBox.Value + "'";
                rsFill.DoQuery(PH_Query);
                if (Blank)
                {
                    oComboItem.ValidValues.Add("-3", "   ");
                }

                while (!rsFill.EoF)
                {
                    oComboItem.ValidValues.Add(rsFill.Fields.Item(0).Value.ToString(), rsFill.Fields.Item(1).Value.ToString());
                    rsFill.MoveNext();
                }

            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
            }

               
           
        }
#endregion
        public bool ChkBeforeAdd(SAPbouiCOM.Form oForm)
        {
            try
            {
               // oForm = Global.SapApplication.Forms.Item(VAL);
                SAPbouiCOM.DBDataSource ODB_OPRE = oForm.DataSources.DBDataSources.Item("@OPRQ");
                SAPbouiCOM.DBDataSource ODB_PRE1 = oForm.DataSources.DBDataSources.Item("@PRQ1");
                SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)oForm.Items.Item("mtxPurReq").Specific;
                

                if (oMat.RowCount > 0)
                {
                    oMat.GetLineData(1);
                }
                else
                {
                    Global.SapApplication.StatusBar.SetText("Invalid Data", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return  false ;
                    oMat.AddRow(1, 1);
                }
                string strBPCOde = ODB_OPRE.GetValue("U_BPCode", 0).Trim();
                string strItmCode = ODB_PRE1.GetValue("U_ItemCode", 0).Trim();
                if ((strBPCOde != "") & strItmCode != "")
                {
                    int in_RowCount = oMat.RowCount, arDel = 0;
                    for (int j = 1; j <= in_RowCount; j++)
                    {
                        double dblAppQty = 0.00;
                        string Chk = "";
                        oMat.GetLineData(j);
                        oMat.GetLineData(j);
                        string strItm = "";
                        strItm = Convert.ToString(ODB_PRE1.GetValue("U_ItemCode", 0)).Trim();
                        if (strItm == "")
                        {
                            oMat.DeleteRow(j);
                        }
                        else
                        {
                          
                        }
                    }
                    
                }
                else
                {
                    Global.SapApplication.StatusBar.SetText("Invalid Data", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                   return  false;
                }
                return true;
            }
            catch (Exception e)
            {
                Global.SapApplication.MessageBox(e.ToString(), 1, "Ok", "", "");
                return  false;
            }

        }


        internal void LineTotal(SAPbouiCOM.Form oForm)
        {
            try
            {
                //SAPbouiCOM.Form oForm = Global.SapApplication.Forms.Item(pVal.FormUID);
               // oForm.Freeze(true);
                SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)oForm.Items.Item("mtxPurReq").Specific;
                SAPbouiCOM.DBDataSource ODB_Ds_L = oForm.DataSources.DBDataSources.Item("@PRQ1");
                ODB_Ds_L.Clear();
                ODB_Ds_L.InsertRecord(0);
                double Qty = 0.00;
                double Price = 0.00;
                double L_tot = 0.00;
                int j = 0;
                //oMat.GetLineData();
                for (int i = 1; i <= oMat.RowCount; i++)
                {
                    oMat.GetLineData(i);


                  
                    Qty = Convert.ToDouble(ODB_Ds_L.GetValue("U_Qty", j));
                    Price = Convert.ToDouble(ODB_Ds_L.GetValue("U_Price", j));
                    L_tot = Qty * Price;

                    ODB_Ds_L.SetValue("U_LineTot", j, Convert.ToString(L_tot));// price in indian Currency
                    oMat.SetLineData(i);
                }
                j++;
                
                   // oForm.Freeze(false);
               
            }
            catch (Exception e)
            {
                //oForm.Freeze(false);
               
            }
        }

        public void TotBefDisc(SAPbouiCOM.Form oForm)
        {
            try
            {
                //SAPbouiCOM.Form oForm = Global.SapApplication.Forms.Item(pVal.FormUID);
                //oForm.Freeze(true);
                int j = 0;
                SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)oForm.Items.Item("mtxPurReq").Specific;
                SAPbouiCOM.DBDataSource ODB_PRE1 = oForm.DataSources.DBDataSources.Item("@PRQ1");
                SAPbouiCOM.DBDataSource ODB_OPRE = oForm.DataSources.DBDataSources.Item("@OPRQ");

                double Total = 0.00;
                double LineTot = 0.00;
                if (oMat.RowCount > 0)
                {
                    for (int i = 1; i <= oMat.RowCount; i++)
                    {
                        oMat.GetLineData(i);
                        //oMat.FlushToDataSource();
                        LineTot = 0.00;
                        LineTot = Convert.ToDouble(ODB_PRE1.GetValue("U_LineTot", 0));
                        Total = Total + LineTot; //U_TotBefDisc  
                        j++;
                    }
                    ODB_OPRE.SetValue("U_TotalBeF", 0, Convert.ToString(Total));
                }
                else
                {
                    ODB_OPRE.SetValue("U_TotalBeF", 0, "0.00");
                }
               // oForm.Freeze(false);
            }
            catch (Exception e)
            {
                //oForm.Freeze(false);
            }
        }

        # region CopyToPurchaseOrder
        public void CopyToPurchaseOrder(SAPbouiCOM.ItemEvent pVal)
        {
             try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.Item(pVal.FormUID);
               
                SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;
                frm.DataSources.UserDataSources.Add("Doc", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 30);
                string docnum = "";
               
                SAPbouiCOM.DBDataSource OdbDs = oForm.DataSources.DBDataSources.Item("@OPRQ");
                SAPbouiCOM.DBDataSource OdbDs1 = oForm.DataSources.DBDataSources.Item("@PRQ1");
                SAPbouiCOM.DBDataSource OdbDs_PO = frm.DataSources.DBDataSources.Item("OPOR");
                SAPbouiCOM.DBDataSource OdbDs_PO1 = frm.DataSources.DBDataSources.Item("POR1");
                //SAPbouiCOM.DBDataSource OdbDs_PO1 = frm.DataSources.DBDataSources.Item("POR1");
                SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)oForm.Items.Item("mtxPurReq").Specific;
                SAPbouiCOM.Matrix oMatPO = (SAPbouiCOM.Matrix)frm.Items.Item("38").Specific;
                //oMatPO.Columns.Item("1").Editable = false;
                docnum = OdbDs.GetValue("DocEntry", 0);
                frm.DataSources.UserDataSources.Item("Doc").Value = docnum;
                oMatPO.Columns.Item("1").Editable = true;
                //oMatPO.Columns.Item("U_BEntry1").Editable = true;
                //oMatPO.Columns.Item("U_BLine1").Editable = true;
                SAPbobsCOM.Recordset oRs_H = null;
                // SAPbobsCOM.Recordset oRs_L = null;
                oRs_H = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                //oRs_L = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                string PH_Query = "select DocEntry,U_PostDate,U_DtExpect,U_DocStatu,U_ReqBy,U_Vendor from [@OPRQ]    where DocEntry='" + docnum + "'";
                oRs_H.DoQuery(PH_Query);
               // string strUser = oRs_H.Fields.Item("U_User").Value.ToString();
                string strBP = oRs_H.Fields.Item("U_Vendor").Value.ToString();
                SAPbouiCOM.EditText edittext1 = (SAPbouiCOM.EditText)frm.Items.Item("4").Specific;
                edittext1.Value = strBP;
                SAPbouiCOM.EditText edittext2 = (SAPbouiCOM.EditText)frm.Items.Item("16").Specific;
                edittext2.Value = "Based On Purchase Requisition " + docnum;
              
                SAPbobsCOM.Recordset oRs_L = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                SAPbobsCOM.Recordset oRs_Val = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));

                string PL_Query = "select [@OPRQ].DocEntry,U_Check, U_ItemCode,U_Price,U_BalQty,LineId,isnull(U_Qty,0)U_Qty,U_Unit,U_UoM from [@PRQ1] inner join [@OPRQ] on [@PRQ1].DocEntry=[@OPRQ].DocEntry where [@OPRQ].DocNum='" + docnum + "'";// and [@PRQ1].U_ApStatus ='A' and [@PRQ1].U_Check='Y' ";
                oRs_L.DoQuery(PL_Query);
                int a = oRs_L.RecordCount;
                int i = 1;
                while (!oRs_L.EoF)
                {
                    if (oRs_L.Fields.Item("U_Check").Value.ToString().Trim() == "Y")
                    {
                        if (Convert.ToDouble(oRs_L.Fields.Item("U_BalQty").Value.ToString().Trim()) > 0)
                        {
                            string strApQty = oRs_L.Fields.Item("U_BalQty").Value.ToString();
                            //  string strIssued = oRs_L.Fields.Item("U_Issued").Value.ToString();
                            double dblApqty = Convert.ToDouble(strApQty);
                            //double dblIssued = Convert.ToDouble(strIssued);
                            //double dblBalanceAproved = (dblApqty - dblIssued);
                            //string strBalance = Convert.ToString(dblBalanceAproved);
                            SAPbouiCOM.EditText col1 = (SAPbouiCOM.EditText)oMatPO.Columns.Item("1").Cells.Item(i).Specific;
                            SAPbouiCOM.EditText col3 = (SAPbouiCOM.EditText)oMatPO.Columns.Item("11").Cells.Item(i).Specific;
                            SAPbouiCOM.EditText col4 = (SAPbouiCOM.EditText)oMatPO.Columns.Item("14").Cells.Item(i).Specific;
                            SAPbouiCOM.EditText col12 = (SAPbouiCOM.EditText)oMatPO.Columns.Item("212").Cells.Item(i).Specific;
                            SAPbouiCOM.ComboBox col112 = (SAPbouiCOM.ComboBox)oMatPO.Columns.Item("12").Cells.Item(i).Specific;

                            SAPbouiCOM.EditText col5 = (SAPbouiCOM.EditText)oMatPO.Columns.Item("U_BEntry1").Cells.Item(i).Specific;
                            SAPbouiCOM.EditText col6 = (SAPbouiCOM.EditText)oMatPO.Columns.Item("U_BLine1").Cells.Item(i).Specific;
                            //SAPbouiCOM.EditText col7 = (SAPbouiCOM.EditText)oMatPO.Columns.Item("160").Cells.Item(i).Specific;

                            col1.Value = oRs_L.Fields.Item("U_ItemCode").Value.ToString();
                            col3.Value = strApQty;
                            col4.Value = oRs_L.Fields.Item("U_Price").Value.ToString();
                            col5.Value = oRs_L.Fields.Item("DocEntry").Value.ToString();
                            col6.Value = oRs_L.Fields.Item("LineId").Value.ToString();
                            string qry = "select OITM.InvntryUom,NumInBuy FROM OITM where ItemCode='" + col1.Value.Trim() + "'";
                            oRs_Val.DoQuery(qry);
                            double NumInBuy =Convert.ToDouble (oRs_Val.Fields.Item("NumInBuy").Value.ToString());
                            if ( NumInBuy >1)
                            {
                                col112.Select(oRs_L.Fields.Item("U_Unit").Value.ToString(), SAPbouiCOM.BoSearchKey.psk_ByDescription);
                            }
                                //= oRs_L.Fields.Item("U_Unit").Value.ToString().Trim();
                            //OdbDs_PO1.SetValue("UseBaseUn", 0, oRs_L.Fields.Item("U_Unit").Value.ToString().Trim());
                            //oMatPO.SetLineData(i);
                            //col7.Value = "EXEMPT";
                            ////oMatPO.GetLineData(i);
                            ////OdbDs_PO1.SetValue("ItemCode", i, oRs_L.Fields.Item("U_ItemCode").Value.ToString());
                            ////OdbDs_PO1.SetValue("Quantity", i, strApQty);
                            ////OdbDs_PO1.SetValue("U_BEntry1", i, oRs_L.Fields.Item("DocEntry").Value.ToString());
                            ////OdbDs_PO1.SetValue("U_BLine1", i, oRs_L.Fields.Item("LineId").Value.ToString());
                            ////oMatPO.SetLineData(i);
                            i++;
                        }

                       
                    }
                    oRs_L.MoveNext();
                }
                DeleteUnWantedRow_OPOR(frm);
               
                SAPbouiCOM.Columns oColumns = oMatPO.Columns;
                oColumns.Item("14").Cells.Item(1).Click(SAPbouiCOM.BoCellClickType.ct_Regular, 0);

                //oForm.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE;
                //oForm.Close();
               // oForm.Close();
              //  oColumns.Item("15").Cells.Item(1).Click(SAPbouiCOM.BoCellClickType.ct_Regular, 0);
               // SAPbouiCOM.Matrix oMatPO = (SAPbouiCOM.Matrix)frm.Items.Item("38").Specific;
               
              
            }
            catch { }
        }
        # endregion

        # region CheckBeforeCopyToPO
        public int CheckBeforeCopyToPO(SAPbouiCOM.ItemEvent pVal)
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.Item(pVal.FormUID);
                string docnum = "";
                int _flagBalQty = 0;
                int _flagCheck = 0;
                int _flagCheck2 = 0;
                int _flagCheck3 = 0;
                SAPbouiCOM.DBDataSource OdbDs = oForm.DataSources.DBDataSources.Item("@OPRQ");
                docnum = OdbDs.GetValue("DocEntry", 0);
                if (docnum != "")
                {
                    SAPbobsCOM.Recordset oRs_L = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                    string PL_Query = "select [@OPRQ].DocEntry,Isnull(U_Check,'N')U_Check,Isnull(U_BalQty,0)U_BalQty, U_ItemCode,U_Price,LineId,isnull(U_Qty,0)U_Qty from [@PRQ1] inner join [@OPRQ] on [@PRQ1].DocEntry=[@OPRQ].DocEntry where [@OPRQ].DocNum='" + docnum + "'";// and [@PRQ1].U_ApStatus ='A' and [@PRQ1].U_Check='Y' ";
                    oRs_L.DoQuery(PL_Query);
                    while (!oRs_L.EoF)
                    {
                        if (oRs_L.Fields.Item("U_Check").Value.ToString().Trim() == "Y")
                        {
                            _flagCheck = 1;
                        }
                        if (Convert.ToDouble(oRs_L.Fields.Item("U_BalQty").Value.ToString()) > 0)
                        {
                            _flagBalQty = 1;
                        }
                        if (oRs_L.Fields.Item("U_Check").Value.ToString().Trim() == "N" && Convert.ToDouble(oRs_L.Fields.Item("U_BalQty").Value.ToString()) >0)
                        {
                            _flagCheck2 =1;

                        }
                        if (oRs_L.Fields.Item("U_Check").Value.ToString().Trim() == "Y" && Convert.ToDouble(oRs_L.Fields.Item("U_BalQty").Value.ToString()) > 0)
                        {
                            _flagCheck3 = 1;
                        }
                        oRs_L.MoveNext();
                    }
                    if (_flagCheck == 0)
                    {
                        return 1;
                    }
                    else if (_flagBalQty == 0)
                    {
                        return 2;
                    }
                    else if (_flagCheck2 == 1 & _flagCheck3 ==0)
                    {
                        return 3;
                    }


                }
                else
                {
                    return -1;
                }
            }
            catch { return -1; }
            return 0;
        }
        # endregion

        #region Close PurchaseReqStatus
        public void PurchaseReqStatusClose(SAPbouiCOM.Form oForm)
        {
            SAPbobsCOM.Recordset oRsDoc2 = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));

            SAPbouiCOM.DBDataSource oData_Doc = oForm.DataSources.DBDataSources.Item("@OPRQ");
            int IntDocentry = Convert.ToInt32(oData_Doc.GetValue("DocEntry", 0));

            string strBaseQry = " UPDATE [@OPRQ] SET [@OPRQ].U_DocStatu='Closed' where [@OPRQ].DocEntry='" + IntDocentry + "' ";
            oRsDoc2.DoQuery(strBaseQry);
           // Closing a document is irreversible. Document status will be changed to "Closed". Do you want to continue?
        }
        #endregion
        public bool FocusColoums(SAPbouiCOM.Form oForm)
        {
            try
            {
                SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)oForm.Items.Item("mtxPurReq").Specific;
               

                SAPbouiCOM.Columns oColumns = oMat.Columns;
                oColumns.Item("colItmCode").Cells.Item(1).Click(SAPbouiCOM.BoCellClickType.ct_Regular, 0);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DisableColoums(SAPbouiCOM.Form oForm)
        {
            try
            {
                SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)oForm.Items.Item("mtxPurReq").Specific;
                oMat.Columns.Item("colPrice").Editable = false;
                oMat.Columns.Item("colItmCode").Editable = false;
                oMat.Columns.Item("colQty").Editable = false;
                oMat.Columns.Item("colPrice").Editable = false;
                oMat.Columns.Item("V_4").Editable = false;
               // oForm.Items.Item("21").Enabled = false;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool EnableColoums(SAPbouiCOM.Form oForm)
        {
            try
            {
                SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)oForm.Items.Item("mtxPurReq").Specific;
                oMat.AddRow(1, oMat.RowCount);
                oMat.Columns.Item("colItmCode").Editable = true;
                oMat.Columns.Item("colQty").Editable = true;
                oMat.Columns.Item("ColItName").Editable = true;
                oMat.Columns.Item("colPrice").Editable = true;
                oMat.Columns.Item("V_4").Editable = true;
               // oMat.Columns.Item("colBal").Visible = false;
                SAPbouiCOM.Columns oColumns = oMat.Columns;
                oColumns.Item("colItmCode").Cells.Item(1).Click(SAPbouiCOM.BoCellClickType.ct_Regular, 0);

                
                
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool EnableColoumsForOpenReq(SAPbouiCOM.Form oForm)
        {
            try
            {
                SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)oForm.Items.Item("mtxPurReq").Specific;
                oForm.DataSources.DBDataSources.Item("@PRQ1").InsertRecord(0);
                if (oMat.RowCount > 0)
                {
                    for (int i = 1; i <= oMat.RowCount; i++)
                    {
                        oMat.GetLineData(i);

                        oMat.SetLineData(i);

                    }
                }
                if (oMat.RowCount > 0)
                {

                    oForm.DataSources.DBDataSources.Item("@PRQ1").SetValue("U_ItemCode", oMat.RowCount, "");
                    oForm.DataSources.DBDataSources.Item("@PRQ1").SetValue("U_ItemName", oMat.RowCount, "");

                    oForm.DataSources.DBDataSources.Item("@PRQ1").SetValue("U_Qty", oMat.RowCount, "0.00");
                    oMat.SetLineData(oMat.RowCount);
                }
                oForm.DataSources.DBDataSources.Item("@PRQ1").Clear();
                //if (pVal.Row == oMatrix.RowCount)
                //{
                    Matrixrowadd(oMat, oForm.DataSources.DBDataSources.Item("@PRQ1"));
                    oForm.DataSources.DBDataSources.Item("@PRQ1").Clear();
                //}
                oMat.Columns.Item("colItmCode").Editable = true;
                oMat.Columns.Item("colQty").Editable = true;
                oMat.Columns.Item("ColItName").Editable = true;
                oMat.Columns.Item("colPrice").Editable = true;
                oMat.Columns.Item("V_4").Editable = true;
                // oMat.Columns.Item("colBal").Visible = false;
                SAPbouiCOM.Columns oColumns = oMat.Columns;
                oColumns.Item("colItmCode").Cells.Item(1).Click(SAPbouiCOM.BoCellClickType.ct_Regular, 0);
           
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //--------------modified: 28-03-2012---------------------------
        public bool DisableButton(SAPbouiCOM.Form oForm)
        {
            try
            {
                SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)oForm.Items.Item("mtxPurReq").Specific;
             
                oForm.Items.Item("22").Visible = false;
                oForm.Items.Item("txtDoc").Enabled = false;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool EnableButton(SAPbouiCOM.Form oForm)
        {
            try
            {
                SAPbouiCOM.EditText textStatus = (SAPbouiCOM.EditText)oForm.Items.Item("txtStatus").Specific;
                if (textStatus.Value == "Open")
                {
                    oForm.Items.Item("22").Visible = true;
                }
                else
                {
                    oForm.Items.Item("22").Visible = false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool EnableItems(SAPbouiCOM.Form oForm)
        {
            try
            {             
                oForm.Items.Item("txtDoc").Enabled = true;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DisableItems(SAPbouiCOM.Form oForm)
        {
            try
            {

                oForm.Items.Item("BpCode").Enabled = false;
                oForm.Items.Item("51").Enabled = false;
                oForm.Items.Item("ContPerson").Enabled = false;
                oForm.Items.Item("8").Enabled = false;
                oForm.Items.Item("txtDoc").Enabled = false;
                oForm.Items.Item("16").Enabled = false;
                oForm.Items.Item("18").Enabled = false;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool IsApproved(SAPbouiCOM.Form oForm, int intDoc)
        {
            try
            {
                SAPbobsCOM.Recordset rsSelect = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                SAPbobsCOM.Recordset rsCheck = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                SAPbobsCOM.Recordset rsCheck1 = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                SAPbouiCOM.DBDataSource dDsPurchase = oForm.DataSources.DBDataSources.Item("@OPRQ");
                SAPbouiCOM.DBDataSource dDsPurchaseLine = oForm.DataSources.DBDataSources.Item("@PRQ1");
                string strDocNum = dDsPurchase.GetValue("DocNum", 0);
               
                string strQuery1 = @"select count(*) cnt from  [@PRQ1] where Docentry='" + intDoc + "' and U_ApStatus ='A' ";
                rsCheck.DoQuery(strQuery1);
               
                int iCount = 0;
                int iCCount = 0;
                if (!rsCheck.EoF)
                {
                    iCount = Convert.ToInt32(rsCheck.Fields.Item("cnt").Value);
                }

                if (iCount >= 1)
                {
                    //return true;
                    string strQuery2 = @"select count(*) cnt from  [@OPRQ] where Docentry='" + intDoc + "' and U_DocStatu='Closed' ";
                    rsCheck1.DoQuery(strQuery2);
                   
                    if (!rsCheck1.EoF)
                    {
                        iCCount = Convert.ToInt32(rsCheck1.Fields.Item("cnt").Value);
                    }
                    if (iCCount == 0)
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
            catch (Exception ex)
            {
                return false;
            }
        }

        #region Add Balance Qty
        internal bool AddBalanceQty(int DocEntry)
        {
            SAPbobsCOM.Recordset oRecordSet = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

            try
            {
                string strQuery = "update [@PRQ1] set U_BalQty = U_Qty where DocEntry ='" + DocEntry + "' ";
                oRecordSet.DoQuery(strQuery);
                return true;    
                   
               
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "ok", "", "");
                return false;
            }
        }
        #endregion
        #region Check Matrix for Duplication
        internal bool ValidateSelection(SAPbouiCOM.Form oForm, string strSelectedVal)
        {
            try
            {
                string strMtxVal = "";
                bool boolVal = true;
                SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)oForm.Items.Item("mtxPurReq").Specific;
               // SAPbouiCOM.Matrix mtxParam = (SAPbouiCOM.Matrix)frmItemMaster.Items.Item("mtxParams").Specific;
             
                for (int i = 1; i <= oMat.RowCount; i++)
                {
                    oMat.GetLineData(i);
                    SAPbouiCOM.DBDataSource dDsPurchaseLine = oForm.DataSources.DBDataSources.Item("@PRQ1");
                    strMtxVal = dDsPurchaseLine.GetValue("U_ItemCode", 0);
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


        

        #region Delete Un Wanted Row
        internal bool DeleteUnWantedRow(SAPbouiCOM.Form oForm)
        {
            try
            {
                //SAPbouiCOM.Form FrmTrget = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.Matrix _mat = (SAPbouiCOM.Matrix)oForm.Items.Item("mtxPurReq").Specific;
                //SAPbouiCOM.Matrix MxtTarget = (SAPbouiCOM.Matrix)FrmTrget.Items.Item("mtxpro").Specific;
                for (int i = 1; i <= _mat.RowCount; i++)
                {
                    // SAPbouiCOM.ComboBox ddlCat = (SAPbouiCOM.ComboBox)_mat.Columns.Item("colcat").Cells.Item(i).Specific;

                    SAPbouiCOM.EditText col1 = (SAPbouiCOM.EditText)_mat.Columns.Item("colItmCode").Cells.Item(i).Specific;

                    // string StrCatVal = ddlCat.Value.ToString().Trim();
                    string StrCatVal = col1.Value.ToString().Trim();
                    //Selected.Value.ToString().Trim();
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
                //Global.SapApplication.MessageBox(ex.Message, 1, "", "", "");

            }
        }
        #endregion

        #region Delete Un Wanted Row
        internal bool DeleteUnWantedRow_OPOR(SAPbouiCOM.Form oForm)
        {
            try
            {

                try
                {
                    //SAPbouiCOM.Form FrmTrget = Global.SapApplication.Forms.ActiveForm;
                    SAPbouiCOM.Matrix _mat = (SAPbouiCOM.Matrix)oForm.Items.Item("38").Specific;
                    //SAPbouiCOM.Matrix MxtTarget = (SAPbouiCOM.Matrix)FrmTrget.Items.Item("mtxpro").Specific;
                    for (int i = 1; i <= _mat.RowCount; i++)
                    {
                        // SAPbouiCOM.ComboBox ddlCat = (SAPbouiCOM.ComboBox)_mat.Columns.Item("colcat").Cells.Item(i).Specific;

                        SAPbouiCOM.EditText col1 = (SAPbouiCOM.EditText)_mat.Columns.Item("1").Cells.Item(i).Specific;

                        // string StrCatVal = ddlCat.Value.ToString().Trim();
                        string StrCatVal = col1.Value.ToString().Trim();
                        //Selected.Value.ToString().Trim();
                        if (StrCatVal == "")
                        {
                            _mat.DeleteRow(i);
                        }
                    }
                    return true;
                   
                }


                catch (Exception ex)
                {
                    Global.SapApplication.MessageBox(ex.Message, 1, "", "", "");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                //Global.SapApplication.MessageBox(ex.Message, 1, "", "", "");

            }
        }
        #endregion



        public bool FillReqLine(SAPbouiCOM.Form oForm)
        {
            try
                {
                //SAPbouiCOM.Form oForm = Global.SapApplication.Forms.Item(pVal.FormUID);
                //oForm.Freeze(true);
                //int j = 0;
                SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)oForm.Items.Item("mtxPurReq").Specific;
                SAPbouiCOM.DBDataSource ODB_PRE1 = oForm.DataSources.DBDataSources.Item("@PRQ1");
                SAPbouiCOM.DBDataSource ODB_OPRE = oForm.DataSources.DBDataSources.Item("@OPRQ");
                SAPbouiCOM.ComboBox col4 = (SAPbouiCOM.ComboBox)oMat.Columns.Item("U_Author").Cells.Item(1).Specific;
                double Total = 0.00;
                double LineTot = 0.00;

                
                for (int i = 1; i <= oMat.RowCount; i++)
                {
                 
                }
                
                return true;          
            }
            catch (Exception e)
            {
                return false;
                //oForm.Freeze(false);
            }
        }


        #region Is Savable

        internal bool IsSavable(SAPbouiCOM.Form frm)
        {
            try
            {
                SAPbouiCOM.DBDataSource dDsOPRQ = frm.DataSources.DBDataSources.Item("@PRQ1");
                string[] strDBfields ={ "U_ItemCode","U_Qty" ,"U_Price","U_LineTot"};
                SAPbouiCOM.Matrix mtxLoc = (SAPbouiCOM.Matrix)frm.Items.Item("mtxPurReq").Specific;
                for (int i = 1; i < mtxLoc.RowCount;i++ )
                {
                    SAPbouiCOM.EditText txtcolQty = (SAPbouiCOM.EditText)mtxLoc.Columns.Item("colQty").Cells.Item(i).Specific;
                    string Qty = txtcolQty.Value;
                SAPbouiCOM.EditText txtcolItm = (SAPbouiCOM.EditText)mtxLoc.Columns.Item("colQty").Cells.Item(i).Specific;
                string itm = txtcolItm.Value;
                if (Qty.Trim() == "0.0" | itm.Trim()=="")
                {
                    return false;
                }
                //bool boolRet = gen.MandatoryChecking(strDBfields, dDsOPRQ);
                //if (boolRet == true )
                //{
                //    return true;
                //}
                else
                {
                    //Global.SapApplication.MessageBox("Mandatory Fields are Empty", 1, "ok", "", "");
                    return true;
                }
                return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "ok", "", "");
                return false;
            }

        }

        #endregion


        #region Is Savable Header Tables

        internal bool IsSavableHeader(SAPbouiCOM.Form frm)
        {
            try
            {
                SAPbouiCOM.DBDataSource dDsOPRQ = frm.DataSources.DBDataSources.Item("@OPRQ");
                string[] strDBfields ={ "U_PostDate", "U_DtExpect" ,"U_Vendor","U_Depmnt"};
                bool boolRet = gen.MandatoryChecking(strDBfields, dDsOPRQ);
                if (boolRet == true)
                {
                    return true;
                }
                else
                {
                    //Global.SapApplication.MessageBox("Mandatory Fields are Empty", 1, "ok", "", "");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "ok", "", "");
                return false;
            }

        }

        #endregion

        internal void IsClearMatrix(SAPbouiCOM.Form frm)
        {
            try
            {
                SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)frm.Items.Item("mtxPurReq").Specific;
                oMat.Clear();
            }
            catch (Exception ex)
            {
            }
        }



        #region To Fill The Combo Uom
        internal void FillUom(SAPbouiCOM.Form Master_Frm)
        {
            try
            {
                SAPbobsCOM.Recordset oRecordSet2 = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                SAPbouiCOM.Matrix mtxParam = (SAPbouiCOM.Matrix)Master_Frm.Items.Item("mtxPurReq").Specific;
                SAPbouiCOM.DBDataSource dDsTargtL = Master_Frm.DataSources.DBDataSources.Item("@PRQ1");
                SAPbouiCOM.ComboBox ddlUom = (SAPbouiCOM.ComboBox)mtxParam.Columns.Item("cmbUom").Cells.Item(1).Specific;
                dDsTargtL.SetValue("U_Unit", 0, " ");
                gen.ClearCombo(ddlUom, false);
                string _str_Query = "select OWGT.UnitDisply,OWGT.UnitName from OWGT ";
                oRecordSet2.DoQuery(_str_Query);
                while (!oRecordSet2.EoF)
                {
                    ddlUom.ValidValues.Add(oRecordSet2.Fields.Item(0).Value.ToString(), oRecordSet2.Fields.Item(1).Value.ToString());

                    oRecordSet2.MoveNext();
                }


            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "ok", "", "");
            }
        }
        #endregion Uom
        # region Fill QuoteNo
        public bool FillUom_Line(SAPbouiCOM.ItemEvent pVal)
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.Item(pVal.FormUID);
                SAPbouiCOM.EditText Edttext1;
                SAPbouiCOM.DBDataSource dDsOPOR = oForm.DataSources.DBDataSources.Item("@PRQ1");
                SAPbobsCOM.Recordset rsChkStatus = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)oForm.Items.Item("mtxPurReq").Specific;
                for (int i = 1; i <= oMat.RowCount; i++)
                {
                    string strUom = "";
                    string strItemCode = "";
                    //SAPbouiCOM.EditText col3 = (SAPbouiCOM.EditText)oMat.Columns.Item("U_QuoteNo").Cells.Item(i).Specific;
                    //strQuoteNo = col3.Value;
                    SAPbouiCOM.EditText col4 = (SAPbouiCOM.EditText)oMat.Columns.Item("colItmCode").Cells.Item(i).Specific;
                    strItemCode = col4.Value;



                    string strItmPrice = @"select OITM.InvntryUom from OITM  where OITM.ItemCode='" + strItemCode + "'";
                    rsChkStatus.DoQuery(strItmPrice);
                    string strItmUom = rsChkStatus.Fields.Item("InvntryUom").Value.ToString();

                    SAPbouiCOM.EditText col2 = (SAPbouiCOM.EditText)oMat.Columns.Item("cmbUom").Cells.Item(i).Specific;
                    col2.Value = strItmUom;

                    //edittext1 = (SAPbouiCOM.EditText)oForm.Items.Item("Bankcode").Specific;
                    //edittext1.Value = strBank;



                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        # endregion

        internal void UOM(SAPbouiCOM.ItemEvent val)
        {
            try
            {
                string strQry = "", InvUom = "", itemCode="";
                SAPbouiCOM.Form form = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)form.Items.Item("mtxPurReq").Specific;
                SAPbouiCOM.DBDataSource OdbDs1 = form.DataSources.DBDataSources.Item("@PRQ1");
                SAPbouiCOM.DBDataSource OdbDs = form.DataSources.DBDataSources.Item("@OPRQ");
                SAPbobsCOM.Recordset oRs_L = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                //SAPbouiCOM.EditText txtInvUom = (SAPbouiCOM.EditText)objMtx.Columns.Item("cmbUom").Cells.Item(val.Row).Specific;
                oMat.GetLineData(val.Row);
                string InvUoM = OdbDs1.GetValue("U_Unit", 0).ToString().Trim();
                 itemCode = OdbDs1.GetValue("U_ItemCode", 0).ToString().Trim();
                 itemCode = itemCode.Trim();
                 itemCode = itemCode.TrimEnd('"');
                 itemCode = itemCode.TrimStart('"');
                if (InvUoM == "Y")
                {
                    strQry = "select OITM.InvntryUom FROM OITM where ItemCode='"+itemCode.Trim()+"'";
                    oRs_L.DoQuery(strQry);
                    InvUom = oRs_L.Fields.Item("InvntryUom").Value.ToString().Trim();
                }
                if (InvUoM == "N")
                {
                    strQry = "select OITM.BuyUnitMsr FROM OITM where ItemCode='" + itemCode.Trim() + "'";
                    oRs_L.DoQuery(strQry);
                    InvUom = oRs_L.Fields.Item("BuyUnitMsr").Value.ToString().Trim();
                }
                OdbDs1.SetValue("U_UoM", 0, InvUom);
                oMat.SetLineData(val.Row);
            }
            catch
            {
            }
            
        }



      

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace VKC
{
    class MRequsitionList
    {

         General gen = new General();

        #region Singleton

        private static MRequsitionList instance;

        public static MRequsitionList Instance
        {
            get
            {
                if (instance == null) instance = new MRequsitionList();

                return instance;
            }
        }

        #endregion

        public MRequsitionList()
        {
            VRequsitionList vp = VRequsitionList.Instance;
        }

        #region Initalsetting
        internal void Initalsetting(SAPbouiCOM.Form frmInit )
        {
            try
            {
                
                //frmInit.DataBrowser.BrowseBy = "txtDoc";
               // Refresh(frmInit, Global.SapCompany);
               

                //-----
                FillReqLst(frmInit);
                //SAPbouiCOM.LinkedButton lnkOrder = (SAPbouiCOM.LinkedButton)mtxAproval.Columns.Item("V_4").ExtendedObject;
               //  lnkOrder.LinkedObject = SAPbouiCOM.BoLinkedObject.lf_UserDefinedObject;
               // lnkOrder.LinkedObjectType = "4";
               
               
             

                //--
            }
            catch (Exception ex)
            {
                //Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
            }
        }

        #endregion

        # region FillRequest
        internal void FillReqLst(SAPbouiCOM.Form frm)
        {
            try
            {

                //SAPbouiCOM.Form frm = (SAPbouiCOM.Form)Global.SapApplication.Forms.Item(val.FormUID);
                SAPbouiCOM.Matrix mtxAproval = (SAPbouiCOM.Matrix)frm.Items.Item("mtxAproval").Specific;
                mtxAproval.Columns.Item("V_4").Width = 85;
                mtxAproval.Columns.Item("V_1").Width = 100;
                mtxAproval.Columns.Item("V_3").Width = 250;
                mtxAproval.Columns.Item("V_0").Width = 120;
                
                mtxAproval.Clear();
                SAPbobsCOM.Recordset rsFill = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string strQry = @"select [@OPRQ].DocEntry,  U_Vendor,CardName ,Convert(Varchar(20),U_PostDate,103)docDate,Convert(Varchar(20),U_DtExpect,103)ExpdDate from [@OPRQ]
                                   
                                    inner join [OCRD] on [@OPRQ].U_Vendor = [OCRD].CardCode where [@OPRQ].U_DocStatu ='Open'order by U_Vendor";
                                  
                                   

                strQry = string.Format(strQry);
                rsFill.DoQuery(strQry);



                while (!rsFill.EoF)
                {


                    frm.DataSources.UserDataSources.Item("DocEntry").ValueEx = rsFill.Fields.Item("DocEntry").Value.ToString();
                    //frm.DataSources.UserDataSources.Item("DocNum").ValueEx = rsFill.Fields.Item("DocNum").Value.ToString();
                    //frm.DataSources.UserDataSources.Item("LineId").ValueEx = rsFill.Fields.Item("LineId").Value.ToString();
                    //frm.DataSources.UserDataSources.Item("ItemCode").ValueEx = rsFill.Fields.Item("U_ItemCode").Value.ToString();
                    //frm.DataSources.UserDataSources.Item("ItemName").ValueEx = rsFill.Fields.Item("U_ItemName").Value.ToString();
                    //frm.DataSources.UserDataSources.Item("ItemCode").ValueEx = rsFill.Fields.Item("U_ItemCode").Value.ToString();
                    frm.DataSources.UserDataSources.Item("Cardode").ValueEx = rsFill.Fields.Item("U_Vendor").Value.ToString();
                    frm.DataSources.UserDataSources.Item("CardName").ValueEx = rsFill.Fields.Item("CardName").Value.ToString();
                    frm.DataSources.UserDataSources.Item("Date").ValueEx = rsFill.Fields.Item("docDate").Value.ToString();
                    frm.DataSources.UserDataSources.Item("ExpDate").ValueEx = rsFill.Fields.Item("ExpdDate").Value.ToString();

                    mtxAproval.AddRow(1, mtxAproval.RowCount);
                    rsFill.MoveNext();

                }

            }
            catch
            {
            }
        }
        # endregion

        # region LinkToPurchase Requisition
        public void LinkToPurReq(SAPbouiCOM.ItemEvent pVal, int IntDoc)
        {
            string strColValue = "";
            SAPbobsCOM.Recordset rsSelect = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.Item(pVal.FormUID);
            SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;
            frm.DataSources.UserDataSources.Add("DocVal", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 30);
            string docnum = "";
          
            SAPbouiCOM.DBDataSource OdbDs = frm.DataSources.DBDataSources.Item("@OPRQ");
            SAPbouiCOM.DBDataSource OdbDs1 = frm.DataSources.DBDataSources.Item("@PRQ1");
         
            SAPbouiCOM.Matrix oMat1 = (SAPbouiCOM.Matrix)oForm.Items.Item("mtxAproval").Specific;
            SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)frm.Items.Item("mtxPurReq").Specific;
        
            

            frm.Mode = SAPbouiCOM.BoFormMode.fm_FIND_MODE;

         
            string strQuery1 = @"SELECT [@OPRQ].DocNum FROM  [@OPRQ]  WHERE [@OPRQ].DocEntry='" + IntDoc + "'";
            rsSelect.DoQuery(strQuery1);
            docnum = rsSelect.Fields.Item("DocNum").Value.ToString();

            frm.DataSources.UserDataSources.Item("Doc").Value = docnum;
            SAPbobsCOM.Recordset oRs_H = null;
            oRs_H = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
            string PH_Query = "select DocEntry,DocNum,U_Specific,U_DocStatu,U_PostDate,U_Depmnt,U_DtExpect,U_Remarks,U_ReqBy,U_TotalBeF from [@OPRQ]   where DocEntry='" + docnum + "'";
            oRs_H.DoQuery(PH_Query);

            frm.Items.Item("txtDoc").Enabled = true;
            string DocNum = Convert.ToString(oRs_H.Fields.Item("DocNum").Value);
            SAPbouiCOM.EditText edittext1 = (SAPbouiCOM.EditText)frm.Items.Item("txtDoc").Specific;
            edittext1.Value = DocNum;
            SAPbouiCOM.Item itmFld3 = frm.Items.Item("1");
            itmFld3.Click(0);

          
            for (int i = 1; i <= oMat.RowCount; i++)
            {
                oMat.GetLineData(i);
                SAPbouiCOM.EditText col2 = (SAPbouiCOM.EditText)oMat.Columns.Item("colQty").Cells.Item(i).Specific;
                strColValue= col2.Value ;

            }
            
            oMat.Columns.Item("colQty").Editable = false;
        

           

           
        }
        # endregion

        #region Delete Un Wanted Row
        internal void DeleteUnWantedRow(SAPbouiCOM.ItemEvent val)
        {
            try
            {
                SAPbouiCOM.Form FrmTrget = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.Matrix _mat = (SAPbouiCOM.Matrix)FrmTrget.Items.Item("mtxPurReq").Specific;
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
                
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "", "", "");

            }
        }
        #endregion

        internal void CopyTo(SAPbouiCOM.ItemEvent pVal)
        {
            string DocEntry="",DocEnt="";
            SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;
            try
            {
                SAPbouiCOM.Form FrmTrget = Global.SapApplication.Forms.Item(pVal.FormUID);
               
                frm.Freeze(true);
                SAPbouiCOM.Matrix _mat = (SAPbouiCOM.Matrix)FrmTrget.Items.Item("mtxAproval").Specific;
                for (int j = 1; j <= _mat.RowCount; j++)
                {
                    bool selected = _mat.IsRowSelected(j);
                    string test = FrmTrget.DataSources.UserDataSources.Item("DocEntry").ValueEx;
                    SAPbouiCOM.CheckBox col2 = (SAPbouiCOM.CheckBox)_mat.Columns.Item("V_2").Cells.Item(j).Specific;
                    bool select = col2.Checked;
                    if (select == true)
                   {
                       SAPbouiCOM.EditText col1 = (SAPbouiCOM.EditText)_mat.Columns.Item("V_4").Cells.Item(j).Specific;
                       DocEntry = DocEntry + "'" + col1.Value + "'" + ",";
                   }
                }
                DocEntry = DocEntry.TrimEnd(',');
                SAPbouiCOM.DBDataSource OdbDs_PO = frm.DataSources.DBDataSources.Item("OPOR");
                SAPbouiCOM.Matrix oMatPO = (SAPbouiCOM.Matrix)frm.Items.Item("38").Specific;
                SAPbobsCOM.Recordset oRs_H  = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                string PH_Query = "select DocEntry,U_PostDate,U_DtExpect,U_DocStatu,U_ReqBy,U_Vendor from [@OPRQ]    where DocEntry in(" + DocEntry .Trim()+ ")";
                oRs_H.DoQuery(PH_Query);
                string strBP = oRs_H.Fields.Item("U_Vendor").Value.ToString();
                SAPbouiCOM.EditText edittext1 = (SAPbouiCOM.EditText)frm.Items.Item("4").Specific;
                edittext1.Value = strBP;
                SAPbouiCOM.EditText edittext2 = (SAPbouiCOM.EditText)frm.Items.Item("16").Specific;
                //edittext2.Value = "Based On Purchase Requisition " + DocEntry;
                SAPbobsCOM.Recordset oRs_L = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                string PL_Query = "select [@OPRQ].DocEntry,U_Check, U_ItemCode,LineId,U_Price,(U_BalQty)U_BalQty,(isnull(U_Qty,0))U_Qty from [@PRQ1] inner join [@OPRQ] on [@PRQ1].DocEntry=[@OPRQ].DocEntry where [@OPRQ].DocEntry in(" + DocEntry + ")";// and [@PRQ1].U_ApStatus ='A' and [@PRQ1].U_Check='Y' ";
                oRs_L.DoQuery(PL_Query);
                int a = oRs_L.RecordCount;
                int i = 1;
                while (!oRs_L.EoF)
                {
                    //if (oRs_L.Fields.Item("U_Check").Value.ToString().Trim() == "Y")
                    //{
                        if (Convert.ToDouble(oRs_L.Fields.Item("U_BalQty").Value.ToString().Trim()) > 0)
                        {
                            DocEnt = DocEnt + "" + oRs_L.Fields.Item("DocEntry").Value.ToString().Trim() + "" + ",";
                            string strApQty = oRs_L.Fields.Item("U_BalQty").Value.ToString();
                             double dblApqty = Convert.ToDouble(strApQty);
                            SAPbouiCOM.EditText col1 = (SAPbouiCOM.EditText)oMatPO.Columns.Item("1").Cells.Item(i).Specific;
                            SAPbouiCOM.EditText col3 = (SAPbouiCOM.EditText)oMatPO.Columns.Item("11").Cells.Item(i).Specific;
                            SAPbouiCOM.EditText col4 = (SAPbouiCOM.EditText)oMatPO.Columns.Item("14").Cells.Item(i).Specific;

                            SAPbouiCOM.EditText col5 = (SAPbouiCOM.EditText)oMatPO.Columns.Item("U_BEntry1").Cells.Item(i).Specific;
                            SAPbouiCOM.EditText col6 = (SAPbouiCOM.EditText)oMatPO.Columns.Item("U_BLine1").Cells.Item(i).Specific;
                            col1.Value = oRs_L.Fields.Item("U_ItemCode").Value.ToString();
                            col3.Value = strApQty;
                            col4.Value = oRs_L.Fields.Item("U_Price").Value.ToString();
                            col5.Value = oRs_L.Fields.Item("DocEntry").Value.ToString();
                            col6.Value = oRs_L.Fields.Item("LineId").Value.ToString();
                            i++;
                        }


                    //}
                    oRs_L.MoveNext();
                }
                DocEnt = DocEnt.TrimEnd(',');
                edittext2.Value = "Based On Purchase Requisition " + DocEnt;
                SAPbouiCOM.Columns oColumns = oMatPO.Columns;
                oColumns.Item("14").Cells.Item(1).Click(SAPbouiCOM.BoCellClickType.ct_Regular, 0);
                frm.Freeze(false);
            }
            catch
            {
                frm.Freeze(false);
            }
        }

        public bool CheckBeforeCopyToPO(SAPbouiCOM.ItemEvent pVal)
        {
            try
            {
                string DocEntry = "";
                SAPbouiCOM.Form FrmTrget = Global.SapApplication.Forms.Item(pVal.FormUID);
                SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.Matrix _mat = (SAPbouiCOM.Matrix)FrmTrget.Items.Item("mtxAproval").Specific;
                for (int j = 1; j <= _mat.RowCount; j++)
                {
                    bool selected = _mat.IsRowSelected(j);
                    string test = FrmTrget.DataSources.UserDataSources.Item("DocEntry").ValueEx;
                    SAPbouiCOM.CheckBox col2 = (SAPbouiCOM.CheckBox)_mat.Columns.Item("V_2").Cells.Item(j).Specific;
                    bool  select = col2.Checked;
                    if (select == true)
                    {
                        SAPbouiCOM.EditText col1 = (SAPbouiCOM.EditText)_mat.Columns.Item("V_4").Cells.Item(j).Specific;
                        DocEntry = DocEntry + "'" + col1.Value + "'" + ",";
                    }
                }
                DocEntry = DocEntry.TrimEnd(',');
                if (DocEntry != "")
                {
                    SAPbobsCOM.Recordset oRs_L = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                    string PL_Query = "select [@OPRQ].DocEntry,Isnull(U_Check,'N')U_Check,Isnull(U_BalQty,0)U_BalQty, U_ItemCode,U_Price,LineId,isnull(U_Qty,0)U_Qty from [@PRQ1] inner join [@OPRQ] on [@PRQ1].DocEntry=[@OPRQ].DocEntry where [@OPRQ].DocNum in(" + DocEntry  + ")";// and [@PRQ1].U_ApStatus ='A' and [@PRQ1].U_Check='Y' ";
                    oRs_L.DoQuery(PL_Query);
                    while (!oRs_L.EoF)
                    {  
                        double BalQty=Convert.ToDouble(oRs_L.Fields.Item("U_BalQty").Value.ToString());
                      
                        if (BalQty > 0)
                        {
                            return true;
                        }
                        oRs_L.MoveNext();
                    }
                  
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
       


       
    }
}
     
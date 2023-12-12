using System;
using System.Collections.Generic;
using System.Text;

namespace VKC
{
    class MPurchaseOrder
    {
        General gen = new General();
        public int docentry;

        public int NewDoc = 0;
        int i = 0;
        public string PFormID = null;
        double OldQty =0;
        SAPbouiCOM.ItemEvent Pval;
        int flag = 0;
        #region Singleton

        private static MPurchaseOrder instance;

        public static MPurchaseOrder Instance
        {
            get
            {
                if (instance == null) instance = new MPurchaseOrder();

                return instance;
            }
        }

        #endregion

        public MPurchaseOrder()
        {
            VPurchaseOrder vp = VPurchaseOrder.Instance;
        }

        # region InitialSettings
        internal void Initalsetting(SAPbouiCOM.Form frm)
        {
            try
            {
               
                MDeliveryDate.Instance.docentry = docentry;
                flag = 0;
                // SAPbouiCOM.DataTable dtableDDate;
                //  dtableDDate = new SAPbouiCOM.DataTable();
                 //SAPbouiCOM.Matrix oMatPO = (SAPbouiCOM.Matrix)frm.Items.Item("38").Specific;
                 //oMatPO.Columns.Item("U_BEntry1").Cells.Item(1).Click(SAPbouiCOM.BoCellClickType.ct_Regular, 0);

                //if (flag == 0)
                //{

                //    oMatPO.Columns.Add("DelvryDt", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON);
                //    flag = 1;
                //}


                // (SAPbouiCOM.BoFormItemTypes)oMatPO.Columns.Item("U_DelvryID").Type = SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON;                
                //oMatPO.Columns.Item("1").Editable = true;
                //SAPbouiCOM.Item itmReference;
                //SAPbouiCOM.Item itmSelected;
                //SAPbouiCOM.StaticText lblSelected;
                //SAPbouiCOM.EditText Edttext1;



                ////-----------------------------------------------------------------------------


                //frm.Items.Add("lblUser", SAPbouiCOM.BoFormItemTypes.it_STATIC);
                //frm.Items.Add("User", SAPbouiCOM.BoFormItemTypes.it_EDIT);


                //itmReference = frm.Items.Item("2");
                //itmSelected = frm.Items.Item("lblUser");

                //itmSelected.Left = itmReference.Left;
                //itmSelected.Top = itmReference.Top - 448;
                //itmSelected.Width = itmReference.Width;
                //itmSelected.Height = itmReference.Height;
                //itmSelected.Left = itmReference.Left + itmReference.Width + 715;
                ////itmSelected.FromPane = 5;
                //// itmSelected.ToPane = 5;
                //itmReference = frm.Items.Item("lblUser");
                //itmSelected = frm.Items.Item("User");

                //itmSelected.Left = itmReference.Left + itmReference.Width + 55;
                //itmSelected.Top = itmReference.Top;
                //itmSelected.Width = 137;
                ////itmSelected.FromPane = 5;
                ////itmSelected.ToPane = 5;

                //lblSelected = (SAPbouiCOM.StaticText)frm.Items.Item("lblUser").Specific;
                //lblSelected.Caption = "User Code";

                //Edttext1 = (SAPbouiCOM.EditText)frm.Items.Item("User").Specific;
                //Edttext1.DataBind.SetBound(true, "OPOR", "U_User");



                //-----------------------------------------------------------------------------

            }
            catch (Exception ex)
            {
                //Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
            }
        }
        # endregion
        #region Fill Datatable
        internal void FillDatatable(SAPbouiCOM.ItemEvent val)
        {
            try
            {


                SAPbouiCOM.Form CForm = Global.SapApplication.Forms.Item(val.FormUID);

                SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(PFormID);

                SAPbouiCOM.DataTable DtItems = (SAPbouiCOM.DataTable)CForm.DataSources.DataTables.Item("DtItems");
                SAPbouiCOM.DBDataSource DDate = (SAPbouiCOM.DBDataSource)CForm.DataSources.DBDataSources.Item("@OPOR_DDATE");
                SAPbouiCOM.Matrix Mtxitem = (SAPbouiCOM.Matrix)CForm.Items.Item("mtxDlvryDt").Specific;
                SAPbouiCOM.Matrix MatrixPform = (SAPbouiCOM.Matrix)PForm.Items.Item("38").Specific;
                SAPbouiCOM.EditText EdtItemPform = (SAPbouiCOM.EditText)MatrixPform.Columns.Item("1").Cells.Item(val.Row).Specific;
                string a = EdtItemPform.Value;
                int i = 0;

                DtItems.Rows.Clear();
                for (i = 1; i < Mtxitem.RowCount; i++)
                {

                    SAPbouiCOM.EditText EdtItem = (SAPbouiCOM.EditText)Mtxitem.Columns.Item("colDelDt").Cells.Item(i).Specific;


                    DtItems.Rows.Add(1);

                    DtItems.SetValue("colItemCode", i - 1, EdtItem.Value);
                    DtItems.SetValue("colDdate", i - 1, EdtItem.Value);



                }



            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
            }
        }
        #endregion
        internal void LoadXML(SAPbouiCOM.ItemEvent val, string FormName)
        {
            try
            {
                //string a = val.FormTypeEx;
               

                gen.LoadXMLForm(FormName);
                SAPbouiCOM.Form FrmDDate = Global.SapApplication.Forms.ActiveForm;
                FrmDDate.DataSources.UserDataSources.Item("RowVal").Value = val.Row.ToString();
                FrmDDate.DataSources.UserDataSources.Item("PFormID").Value = val.FormUID;
                MDeliveryDate.Instance.Initalsetting(val);
                //  MDeliveryDate.Instance.FillMtxItem(frm);
                // string abc = Global.SapApplication.Forms.ActiveForm.UniqueID;
                //MDeliveryDate.Instance.Initalsetting(val);
                //SAPbouiCOM.Form CForm = Global.SapApplication.Forms.ActiveForm;
                ////LoadDetails(PForm, CForm);
                ////init_S_CForm(PForm, CForm);
                // SetValue_S_CForm(PForm, CForm);
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "ok", "", "");
            }
        }
        private void init_S_CForm(SAPbouiCOM.Form PForm, SAPbouiCOM.Form CForm)
        {
            try
            {

                CForm.DataSources.UserDataSources.Item("PFormID").Value = Convert.ToString(PForm.UniqueID);

                PFormID = CForm.DataSources.UserDataSources.Item("PFormID").Value;
                //PForm.DataSources.UserDataSources.Item("S_CForm").ValueEx = CForm.UniqueID;
                SAPbouiCOM.DataTable _dt_PT = CForm.DataSources.DataTables.Item("DtItems");
                SetValue_S_CForm(PForm, CForm, _dt_PT);

            }
            catch { }
        }
        internal void SetValue_S_PForm(SAPbouiCOM.ItemEvent val)
        {
            try
            {

                //    SAPbouiCOM.Form CForm = Global.SapApplication.Forms.Item(val.FormUID);
                //    SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(CForm.DataSources.UserDataSources.Item("PFrmUID").ValueEx);
                //    SAPbouiCOM.DataTable _dt_PT = PForm.DataSources.DataTables.Item("dt_PForm");
                //    SAPbouiCOM.Matrix _mat = (SAPbouiCOM.Matrix)CForm.Items.Item("matAuthz").Specific;
                //    SAPbouiCOM.DBDataSource _dbds_OPTQ = (SAPbouiCOM.DBDataSource)CForm.DataSources.DBDataSources.Item("@NOR_HEM8");

                //    _dt_PT.Rows.Clear();

                //    _mat.FlushToDataSource();
                //    for (int i = 0; i <= _dbds_OPTQ.Size - 1; i++)
                //    {
                //        _dt_PT.Rows.Add(1);
                //        _dt_PT.SetValue("colCode", i, _dbds_OPTQ.GetValue("U_MODULE", i).Trim());
                //        _dt_PT.SetValue("colName", i, _dbds_OPTQ.GetValue("U_MODULENAM", i).Trim());
                //        _dt_PT.SetValue("colRead", i, _dbds_OPTQ.GetValue("U_READONLY", i).Trim());
                //        _dt_PT.SetValue("colEdit", i, _dbds_OPTQ.GetValue("U_EDIT", i).Trim());
                //        _dt_PT.SetValue("colAdd", i, _dbds_OPTQ.GetValue("U_ADD", i).Trim());
                //    }
                //    _dbds_OPTQ.Clear();
                //    _dbds_OPTQ.InsertRecord(0);
                //    if (PForm.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                //    {
                //        PForm.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
                //    }
            }


            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "ok", "", "");

            }

        }

        private void SetValue_S_CForm(SAPbouiCOM.Form PForm, SAPbouiCOM.Form CForm, SAPbouiCOM.DataTable _dt_PT)
        {
            try
            {
                //string abc = PFormID ;

                //PForm = Global.SapApplication.Forms.Item(PFormID);
                PForm = Global.SapApplication.Forms.Item(CForm.DataSources.UserDataSources.Item("PFormID").Value);

                ////SAPbouiCOM.DBDataSource OHEM = (SAPbouiCOM.DBDataSource)PForm.DataSources.DBDataSources.Item("OHEM");
                ////CForm.DataSources.UserDataSources.Item("EmpId").Value = OHEM.GetValue("empID", 0).ToString();
                ////CForm.DataSources.UserDataSources.Item("EmpName").Value = OHEM.GetValue("firstName", 0).ToString();
                ////SAPbouiCOM.Matrix _mat = (SAPbouiCOM.Matrix)CForm.Items.Item("matAuthz").Specific;
                ////SAPbouiCOM.DBDataSource _dbds_OPTQ = (SAPbouiCOM.DBDataSource)CForm.DataSources.DBDataSources.Item("@NOR_HEM8");
                ////_dbds_OPTQ.Clear();
                ////for (int i = 0; i < _dt_PT.Rows.Count; i++)
                ////{
                ////    _dbds_OPTQ.InsertRecord(0);
                ////}
                ////_mat.AddRow(1, 0);
                ////for (int i = 0; i < _dt_PT.Rows.Count; i++)
                ////{

                ////    _dbds_OPTQ.SetValue("U_MODULE", i, _dt_PT.GetValue("colCode", i).ToString());
                ////    _dbds_OPTQ.SetValue("U_MODULENAM", i, _dt_PT.GetValue("colName", i).ToString());
                ////    _dbds_OPTQ.SetValue("U_READONLY", i, _dt_PT.GetValue("colRead", i).ToString());
                ////    _dbds_OPTQ.SetValue("U_EDIT", i, _dt_PT.GetValue("colEdit", i).ToString());
                ////    _dbds_OPTQ.SetValue("U_ADD", i, _dt_PT.GetValue("colAdd", i).ToString());

                ////}
                ////_mat.Clear();
                ////_mat.LoadFromDataSource();
                ////_dbds_OPTQ.Clear();
            }
            catch { }
        }
        internal void LoadDetails(SAPbouiCOM.Form PForm, SAPbouiCOM.Form CForm)
        {
            try
            {
                SAPbobsCOM.Recordset _rs_Select = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                SAPbouiCOM.DataTable _dt_Paymnt = CForm.DataSources.DataTables.Item("DtItems");

                // SAPbouiCOM.DBDataSource _dbds_OCTG = (SAPbouiCOM.DBDataSource)PForm.DataSources.DBDataSources.Item("ORDR");
                //string _str_GroupNum = _dbds_OCTG.GetValue("empID", 0).ToString().Trim();
                //string _str_P_Select = "SELECT U_MODULE,U_MODULENAM,U_READONLY,U_EDIT,U_ADD FROM [@NOR_HEM8] WHERE CODE = '" + _str_GroupNum + "'";
                //_rs_Select.DoQuery(_str_P_Select);
                //if (_rs_Select.RecordCount != 0)
                //{
                MPurchaseOrder.Instance.Clear();
                //   MPurchaseOrder.Instance.GetExistingDetails(PForm, CForm);
                //}
                //else
                //{
                //    MAuthorization.Instance.GetNewDetails(Global.SapApplication.Forms.ActiveForm);
                //}
            }
            catch { }
        }

        internal void Clear()
        {
            try
            {
                SAPbouiCOM.Form form = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.DataTable _dt_Paymnt = form.DataSources.DataTables.Item("DtItems");
                _dt_Paymnt.Rows.Clear();

            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "ok", "", "");
            }
        }

        # region Before Add
        public bool UpdateBeforeAdd(SAPbouiCOM.Form oForm)
        {
            try
            {
                string docnum = "";
                string docentry = "";
                string strQty = "";
                string strBaseEntry = "";
                string strBaseLine = "";
                SAPbouiCOM.DBDataSource OdbDs_PO = oForm.DataSources.DBDataSources.Item("OPOR");
                SAPbouiCOM.DBDataSource OdbDs_Line = oForm.DataSources.DBDataSources.Item("POR1");
                SAPbobsCOM.Recordset rsCheck = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
               // SAPbobsCOM.Recordset rsAproval = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                SAPbobsCOM.Recordset rsLine = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                SAPbobsCOM.Recordset rsIssue = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                SAPbobsCOM.Recordset rsUser = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));

                SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)oForm.Items.Item("38").Specific;



                for (int i = 1; i <= oMat.RowCount; i++)
                {
                    oMat.GetLineData(i);
                    strQty = OdbDs_Line.GetValue("Quantity", 0);
                    strBaseEntry = OdbDs_Line.GetValue("U_BEntry1", 0);
                    strBaseLine = OdbDs_Line.GetValue("U_BLine1", 0);
                    //string strQry = @"select [@OPRQ].U_User  from [@OPRQ]  where [@OPRQ].DocEntry='" + strBaseEntry + "' ";
                    //rsUser.DoQuery(strQry);
                    //string strUser = rsUser.Fields.Item("U_User").Value.ToString();
                    //OdbDs_PO.SetValue("Comments", 0, strUser);

                    if (strBaseEntry == "" & strBaseLine == "")
                    {
                        return true;
                    }
                    else
                    {



                        string strIssueqty = "select ISNULL([@PRQ1].U_Issued,0)Issued,ISNULL([@PRQ1].U_Quantity,0)Qty,ISNULL([@PRQ1].U_ApQty,0)ApQty from [@PRQ1] where [@PRQ1].DocEntry='" + strBaseEntry + "' and [@PRQ1].LineId='" + strBaseLine + "'";
                        rsIssue.DoQuery(strIssueqty);
                        double dblIssured = Convert.ToDouble(rsIssue.Fields.Item("Issued").Value);
                        double dblTottalQty = Convert.ToDouble(rsIssue.Fields.Item("Qty").Value);
                        double dblApQty = Convert.ToDouble(rsIssue.Fields.Item("ApQty").Value);
                        double dblPurqty = Convert.ToDouble(strQty);
                        double dblTottalIssued = (dblIssured + dblPurqty);
                        double dblFinalAprQty = (dblApQty - dblTottalIssued);
                        if (dblTottalIssued > dblApQty)
                        {
                            return false;
                        }

                        if (dblFinalAprQty < 0)
                        {
                            //Global.SapApplication.StatusBar.SetText("Quantity Greater than Aproved Quantity ", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                            return false;
                        }


                    }
                }


                return true;
            }
            catch (Exception ex)
            {

                return false;
            }


        }
        # endregion


        # region Before Add
        public bool UpdateBeforeAdd_Update1(SAPbouiCOM.Form oForm)
        {
            try
            {
                string docnum = "";
                string docentry = "";
                string strQty = "";
                string strBaseEntry = "";
                string strBaseLine = "";
                SAPbouiCOM.DBDataSource OdbDs_PO = oForm.DataSources.DBDataSources.Item("OPOR");
                SAPbouiCOM.DBDataSource OdbDs_Line = oForm.DataSources.DBDataSources.Item("POR1");
                SAPbobsCOM.Recordset rsCheck = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                SAPbobsCOM.Recordset rsAproval = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                SAPbobsCOM.Recordset rsLine = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                SAPbobsCOM.Recordset rsIssue = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                SAPbobsCOM.Recordset rsUser = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));

                SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)oForm.Items.Item("38").Specific;



                for (int i = 1; i <= oMat.RowCount; i++)
                {
                    oMat.GetLineData(i);
                    strQty = OdbDs_Line.GetValue("Quantity", 0);
                    strBaseEntry = OdbDs_Line.GetValue("U_BEntry1", 0);
                    strBaseLine = OdbDs_Line.GetValue("U_BLine1", 0);
                    //string strQry = @"select [@OPRQ].U_User  from [@OPRQ]  where [@OPRQ].DocEntry='" + strBaseEntry + "' ";
                    //rsUser.DoQuery(strQry);
                    //string strUser = rsUser.Fields.Item("U_User").Value.ToString();
                    //OdbDs_PO.SetValue("Comments", 0, strUser);

                    if (strBaseEntry == "" & strBaseLine == "")
                    {
                        return true;
                    }
                    else
                    {



                        string strIssueqty = "select ISNULL([@PRQ1].U_Issued,0)Issued,ISNULL([@PRQ1].U_Quantity,0)Qty,ISNULL([@PRQ1].U_ApQty,0)ApQty from [@PRQ1] where [@PRQ1].DocEntry='" + strBaseEntry + "' and [@PRQ1].LineId='" + strBaseLine + "'";
                        rsIssue.DoQuery(strIssueqty);
                        double dblIssured = Convert.ToDouble(rsIssue.Fields.Item("Issued").Value);
                        double dblTottalQty = Convert.ToDouble(rsIssue.Fields.Item("Qty").Value);
                        double dblApQty = Convert.ToDouble(rsIssue.Fields.Item("ApQty").Value);
                        double dblPurqty = Convert.ToDouble(strQty);
                        double dblTottalIssued = (dblIssured);
                        double dblFinalAprQty = (dblApQty - dblTottalIssued);
                        if (dblTottalIssued > dblApQty)
                        {
                            return false;
                        }

                        if (dblFinalAprQty < 0)
                        {
                            //Global.SapApplication.StatusBar.SetText("Quantity Greater than Aproved Quantity ", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                            return false;
                        }


                    }
                }


                return true;
            }
            catch (Exception ex)
            {

                return false;
            }


        }
        # endregion





        # region Before Add _  Update Mode
        public bool UpdateBeforeAdd_Update(SAPbouiCOM.Form oForm)
        {
            try
            {
                string docnum = "";
                string docentry = "";
                string strQty = "";
                string strBaseEntry = "";
                string strBaseLine = "";
                double dblTottalIssued = 0;
                string strSelectedVal = "";
                SAPbouiCOM.DBDataSource OdbDs_PO = oForm.DataSources.DBDataSources.Item("OPOR");
                SAPbouiCOM.DBDataSource OdbDs_Line = oForm.DataSources.DBDataSources.Item("POR1");
                SAPbobsCOM.Recordset rsCheck = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                SAPbobsCOM.Recordset rsAproval = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                SAPbobsCOM.Recordset rsLine = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                SAPbobsCOM.Recordset rsIssue = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                SAPbobsCOM.Recordset rsUser = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));

                SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)oForm.Items.Item("38").Specific;



                for (int i = 1; i <= oMat.RowCount; i++)
                {
                    oMat.GetLineData(i);
                    strQty = OdbDs_Line.GetValue("Quantity", i - 1);
                    strBaseEntry = OdbDs_Line.GetValue("U_BEntry1", i - 1);
                    strBaseLine = OdbDs_Line.GetValue("U_BLine1", i - 1);
                    //string strQry = @"select [@OPRQ].U_User  from [@OPRQ]  where [@OPRQ].DocEntry='" + strBaseEntry + "' ";
                    //rsUser.DoQuery(strQry);
                    //string strUser = rsUser.Fields.Item("U_User").Value.ToString();
                    //OdbDs_PO.SetValue("Comments", 0, strUser);
                    SAPbouiCOM.EditText col3 = (SAPbouiCOM.EditText)oMat.Columns.Item("11").Cells.Item(i).Specific;
                    strSelectedVal = col3.Value;

                    if (strBaseEntry == "" & strBaseLine == "")
                    {
                        return true;
                    }
                    else
                    {

                        string strDocstatus = "  select [@OPRQ] .U_DocStatu from [@OPRQ]  where [@OPRQ].DocEntry='" + strBaseEntry + "' ";

                        rsAproval.DoQuery(strDocstatus);
                        string strAproval = rsAproval.Fields.Item("U_DocStatu").Value.ToString();
                        if (strAproval == "Closed")
                        {
                            return false;
                        }
                        else
                        {

                            //string strIssueqty1 = "update [@PRQ1] set [@PRQ1].U_Issued='" + strSelectedVal + "' where  [@PRQ1].DocEntry='" + strBaseEntry + "' and [@PRQ1].LineId='" + strBaseLine + "'";
                            //rsLine.DoQuery(strIssueqty1);
                            string strIssueqty = "select ISNULL([@PRQ1].U_Issued,0)Issued,ISNULL([@PRQ1].U_Quantity,0)Qty,ISNULL([@PRQ1].U_ApQty,0)ApQty from [@PRQ1] where [@PRQ1].DocEntry='" + strBaseEntry + "' and [@PRQ1].LineId='" + strBaseLine + "'";
                            rsIssue.DoQuery(strIssueqty);

                            //string Strstatus = rsIssue.Fields.Item("Status").Value.ToString();
                            double dblIssured = Convert.ToDouble(rsIssue.Fields.Item("Issued").Value);
                            double dblTottalQty = Convert.ToDouble(rsIssue.Fields.Item("Qty").Value);
                            double dblApQty = Convert.ToDouble(rsIssue.Fields.Item("ApQty").Value);
                            double dblPurqty = Convert.ToDouble(strSelectedVal);
                            double dbltot = dblIssured + dblPurqty;
                            if (dbltot > dblApQty)
                            {
                                return false;
                            }
                            else
                            {
                                string strIssueqty1 = "update [@PRQ1] set [@PRQ1].U_Issued='" + dblPurqty + "' where  [@PRQ1].DocEntry='" + strBaseEntry + "' and [@PRQ1].LineId='" + strBaseLine + "'";
                                rsLine.DoQuery(strIssueqty1);
                            }
                            //if (Strstatus == "Y")
                            //{
                            //     dblTottalIssued = (dblIssured + dblPurqty); ;
                            //}
                            //else
                            //{
                            string strIssueqty2 = "select ISNULL([@PRQ1].U_Issued,0)UpdateIssued from [@PRQ1] where [@PRQ1].DocEntry='" + strBaseEntry + "' and [@PRQ1].LineId='" + strBaseLine + "'";
                            rsCheck.DoQuery(strIssueqty2);

                            double UpatedblIssured = Convert.ToDouble(rsCheck.Fields.Item("UpdateIssued").Value);

                            dblTottalIssued = (UpatedblIssured);
                            //}
                            double dblFinalAprQty = (dblApQty - dblTottalIssued);
                            if (dblTottalIssued > dblApQty)
                            {
                                return false;
                            }

                            if (dblFinalAprQty < 0)
                            {
                                //Global.SapApplication.StatusBar.SetText("Quantity Greater than Aproved Quantity ", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                                return false;
                            }

                        }
                    }
                }


                return true;
            }
            catch (Exception ex)
            {

                return false;
            }


        }
        # endregion

        # region Check ItemQty in  Update Mode
        public bool UpdateBeforeAdd_ItemQty(SAPbouiCOM.ItemEvent pVal)
        {
            SAPbouiCOM.Form oForm = Global.SapApplication.Forms.Item(pVal.FormUID);
            SAPbouiCOM.DBDataSource OdbDs_PO = oForm.DataSources.DBDataSources.Item("OPOR");
            SAPbouiCOM.DBDataSource OdbDs_Line = oForm.DataSources.DBDataSources.Item("POR1");
            SAPbobsCOM.Recordset _rs_Select = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

            try
            {
                string docnum = "";
                string docentry = "";
                string strQty = "";
                string strBaseEntry = "";
                string strBaseLine = "";
                string strSelectedVal = "";
                

                SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)oForm.Items.Item("38").Specific;
                oMat.GetLineData(pVal.Row);
               
                
                strQty = OdbDs_Line.GetValue("Quantity", 0);
                SAPbouiCOM.EditText col3 = (SAPbouiCOM.EditText)oMat.Columns.Item("11").Cells.Item(pVal.Row).Specific;
                strSelectedVal = col3.Value;
                strBaseEntry = OdbDs_Line.GetValue("U_BEntry1",0);
                strBaseLine = OdbDs_Line.GetValue("U_BLine1", 0);
                if (strBaseEntry != "" && strBaseLine != "")
                {
                    string strQuery = "select U_BalQty from [@PRQ1] where DocEntry ='" + Convert.ToInt32(strBaseEntry) + "' and LineId = '" + Convert.ToInt32(strBaseLine) + "'";
                    _rs_Select.DoQuery(strQuery);
                    OldQty = Convert.ToDouble(_rs_Select.Fields.Item("U_BalQty").Value);
                    double dblOldqty = Convert.ToDouble(strQty);
                    double dblNewQty = Convert.ToDouble(strSelectedVal);


                    if (strBaseEntry == "" & strBaseLine == "")
                    {
                        return true;
                    }
                    else if (OldQty < dblNewQty)
                    {

                        Global.SapApplication.MessageBox("Entered Quantity is Greater than Requested Quantity !! Are you sure to continue ?? ", 1, "OK", "Cancel", "");

                    }
                }
                
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }


        }
        # endregion


        # region Form Data Event  After Add
        public bool UpdateAfterAdd(SAPbouiCOM.Form oForm, int intDoc)
        {
            try
            {
               // string abc = oForm.DataSources.UserDataSources.Item("PFormID").Value;
               SAPbouiCOM.Form  PForm = Global.SapApplication.Forms.Item(oForm.DataSources.UserDataSources.Item("CFormP").Value);
                string Base_Entry = "";
                string Doc_Entry = "";
                string strLineAll = "";
                string strPurLineAll = "", frmUID = "";
                SAPbouiCOM.DBDataSource OdbDs_PO = oForm.DataSources.DBDataSources.Item("OPOR");
                SAPbouiCOM.DBDataSource OdbDs_Line = oForm.DataSources.DBDataSources.Item("POR1");
                
                //if (frmUID == "PurchaseRequisition")
                //{
                    SAPbouiCOM.DBDataSource OdbDs1 = PForm.DataSources.DBDataSources.Item("@PRQ1");
                
                //SAPbouiCOM.DBDataSource OdbDs_PO1 = frm.DataSources.DBDataSources.Item("POR1");
                SAPbouiCOM.Matrix oMatReq = (SAPbouiCOM.Matrix)PForm.Items.Item("mtxPurReq").Specific;
                //SAPbobsCOM.Recordset oRsDoc = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                SAPbobsCOM.Recordset oRsDoc2 = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                //SAPbobsCOM.Recordset rsCheck = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                //SAPbobsCOM.Recordset rsCheck1 = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                SAPbobsCOM.Recordset rsAproval = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                SAPbobsCOM.Recordset rsLine = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                SAPbobsCOM.Recordset rsBaseLine = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                SAPbobsCOM.Recordset rsIssue = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                SAPbobsCOM.Recordset rsRequest = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                SAPbobsCOM.Recordset rsUser = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                SAPbobsCOM.Recordset rsPOUser = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));


                SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)oForm.Items.Item("38").Specific;
                SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;

                int Document = intDoc;
                if (Document != 0)
                {

                    Base_Entry = frm.DataSources.UserDataSources.Item("Doc").Value;



                    string strBaseline = " select POR1.Quantity,POR1.U_BEntry1,POR1.U_BLine1 from POR1  where POR1.DocEntry='" + Document + "' ";
                    rsBaseLine.DoQuery(strBaseline);

                    while (!rsBaseLine.EoF)
                    {
                        string strPurQty = rsBaseLine.Fields.Item("Quantity").Value.ToString();
                        string strBaseEntry = rsBaseLine.Fields.Item("U_BEntry1").Value.ToString();
                        string strBaseLine_1 = rsBaseLine.Fields.Item("U_BLine1").Value.ToString();
                        string strqty = "select  ISNULL([@PRQ1].U_BalQty,0)Qty from [@PRQ1] where [@PRQ1].DocEntry='" + Base_Entry + "' and [@PRQ1].LineId='" + strBaseLine_1 + "'";
                        rsIssue.DoQuery(strqty);
                       // double dblIssued = Convert.ToDouble(rsIssue.Fields.Item("Issued").Value);
                        double dblTotalQty = Convert.ToDouble(rsIssue.Fields.Item("Qty").Value);
                        //double dblApQty = Convert.ToDouble(rsIssue.Fields.Item("ApQty").Value);

                        double dblPurqty = Convert.ToDouble(strPurQty);
                        double dblBalQty =  (dblTotalQty - dblPurqty);
                        //double dblFinalAprQty = (dblApQty - dblTottalIssued);

                        string strIssueRequestion = " update [@PRQ1] set [@PRQ1].U_BalQty='" + dblBalQty + "' where [@PRQ1].DocEntry='" + Base_Entry + "' and [@PRQ1].LineId='" + strBaseLine_1 + "'";
                        rsRequest.DoQuery(strIssueRequestion);

                        if (oMatReq.RowCount > 0)
                        {
                            for (int i = 1; i <= oMatReq.RowCount; i++)
                            {
                                oMatReq.GetLineData(i);
                                if (OdbDs1.GetValue("DocEntry", 0).ToString() == Base_Entry && OdbDs1.GetValue("LineId", 0).ToString() == strBaseLine_1)
                                {
                                    OdbDs1.SetValue("U_BalQty", 0, dblBalQty.ToString());
                                }
                                oMatReq.SetLineData(i);

                            }
                        }

                        rsBaseLine.MoveNext();
                    }

                    ////string strQuery_Status = @"UPDATE  [@PRQ1] SET U_ApStatus ='C' where Docentry='" + Base_Entry + "' and [@PRQ1].U_ApQty =[@PRQ1].U_Issued ";
                    ////rsCheck.DoQuery(strQuery_Status);

                    ////string strQuery_Check = @"update  [@PRQ1] set [@PRQ1].U_Check='N'  where [@PRQ1].DocEntry='" + Base_Entry + "' ";
                    ////rsCheck1.DoQuery(strQuery_Check);

                    string strQuery1 = @"select count(*) cnt from  [@PRQ1] where Docentry='" + Base_Entry + "' and ISNULL(U_Check,'A') IN('N','A') ";
                    rsAproval.DoQuery(strQuery1);

                    int iCount = 0;

                    if (!rsAproval.EoF)
                    {
                        iCount = Convert.ToInt32(rsAproval.Fields.Item("cnt").Value);
                    }
                     strQuery1 = @"select count(*) cnt from  [@PRQ1] where Docentry='" + Base_Entry + "'  and ISNULL(U_BalQty,0)>0";
                    rsAproval.DoQuery(strQuery1);

                    int iCount1 = 0;

                    if (!rsAproval.EoF)
                    {
                        iCount1 = Convert.ToInt32(rsAproval.Fields.Item("cnt").Value);
                    }

                    ////string strQry = @" UPDATE OPOR SET OPOR.U_User=(SELECT [@OPRQ].U_User FROM [@OPRQ] inner join [@PRQ1] on  [@OPRQ].DocEntry= [@PRQ1].DocEntry where [@OPRQ].DocEntry='" + Base_Entry + "' group by [@OPRQ].DocEntry,[@OPRQ].U_User) where OPOR.DocEntry='" + Document + "' ";
                    ////rsUser.DoQuery(strQry);

                    if (iCount == 0 && iCount1 ==0)
                    {

                        string strBaseQry = " UPDATE [@OPRQ] SET [@OPRQ].U_DocStatu='Closed' where [@OPRQ].DocEntry='" + Base_Entry + "' ";
                        oRsDoc2.DoQuery(strBaseQry);
                    }
                    else
                    {
                        return false;
                    }


                    // string strUser = rsUser.Fields.Item("U_User").Value.ToString();

                    //string strNewQry = @"UPDATE  OPOR SET  OPOR.U_User='" + strUser + "' WHERE OPOR.DocEntry='" + Document + "' ";
                    //rsPOUser.DoQuery(strNewQry);
                }


                else
                {
                    return false;
                }


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        # endregion

        # region Choose From List
        public bool Choofromlist_Req(SAPbouiCOM.Form oForm, SAPbouiCOM.ItemEvent pVal)
        {
            try
            {

                //oForm.Freeze(true);
                SAPbouiCOM.ChooseFromListEvent OCFL_Evnt = (SAPbouiCOM.ChooseFromListEvent)(pVal);
                string strCFL_UID = OCFL_Evnt.ChooseFromListUID;
                SAPbouiCOM.ChooseFromList oCFL = oForm.ChooseFromLists.Item(strCFL_UID);
                SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)oForm.Items.Item("38").Specific;
                SAPbouiCOM.DBDataSource ODB_PEQ1 = oForm.DataSources.DBDataSources.Item("POR1");
                SAPbouiCOM.Columns oColumns = oMat.Columns;
                ODB_PEQ1.Clear();
                ODB_PEQ1.InsertRecord(0);
                SAPbouiCOM.Condition Con = null;
                SAPbouiCOM.Conditions Cons = null;

                #region BeforeAction == true
                if (OCFL_Evnt.BeforeAction == true)
                {
                    //Cons = new SAPbouiCOM.Conditions();

                    //if (pVal.FormTypeEx == "142")
                    //{
                    //    if (pVal.ItemUID == "BpCode")
                    //    {
                    //        Con = Cons.Add();
                    //        Con.Alias = "CardType";
                    //        Con.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                    //        Con.CondVal = "S";
                    //        oCFL.SetConditions(Cons);
                    //    }
                    //    else if (pVal.ItemUID == "ContPerson")
                    //    {
                    //        string CardCode = oForm.DataSources.DBDataSources.Item("@OPRQ").GetValue("U_BpCode", 0);
                    //        Con = Cons.Add();
                    //        Con.Alias = "CardCode";
                    //        Con.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                    //        Con.CondVal = CardCode;
                    //        oCFL.SetConditions(Cons);
                    //    }
                    //}
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
                            if (pVal.FormTypeEx == "142")
                            {
                                //if (pVal.ItemUID == "BpCode")
                                //{
                                //    string CardCode = "", CardName = "";
                                //    SAPbouiCOM.Matrix MatPROD = (SAPbouiCOM.Matrix)(oForm.Items.Item("mtxPurReq").Specific);
                                //    CardCode = Convert.ToString(oDataTable.GetValue(0, 0));
                                //    CardName = Convert.ToString(oDataTable.GetValue(1, 0));
                                //    oForm.DataSources.DBDataSources.Item("@OPRQ").SetValue("U_BpCode", 0, CardCode);
                                //    oForm.DataSources.DBDataSources.Item("@OPRQ").SetValue("U_BpName", 0, CardName);
                                //    //OpenStatusReq(oForm);
                                //    if (MatPROD.RowCount == 0)
                                //    {
                                //        MatPROD.AddRow(1, 1);
                                //    }
                                //    oForm.DataSources.DBDataSources.Item("@PRQ1").Clear();

                                //}
                                //else if (pVal.ItemUID == "ContPerson")
                                //{
                                //    string ContactPerson = "";
                                //    ContactPerson = Convert.ToString(oDataTable.GetValue(2, 0));
                                //    oForm.DataSources.DBDataSources.Item("@OPRQ").SetValue("U_Contpers", 0, ContactPerson);
                                //}
                                if (pVal.ItemUID == "38")
                                {
                                    SAPbouiCOM.Matrix oMatrix = (SAPbouiCOM.Matrix)(oForm.Items.Item("38").Specific);
                                    if (pVal.ColUID == "U_QuoteNo")
                                    {
                                        // oForm.Freeze(true);
                                        oForm.DataSources.DBDataSources.Item("POR1").Clear();
                                        oMatrix.GetLineData(pVal.Row);
                                        string StrDocdate = "";
                                        //StrDocdate = Convert.ToString(oForm.DataSources.DBDataSources.Item("@OPRQ").GetValue("U_DocDate", 0));
                                        //// oMatrix.FlushToDataSource();
                                        //string val1 = Convert.ToString(oForm.DataSources.DBDataSources.Item("@PRQ1").GetValue("U_ItemCode", 0));
                                        // oForm.Freeze(false);
                                        // bool boolValidate = ValidateSelection(oForm, oDataTable.GetValue(0, 0).ToString());

                                        //if (boolValidate == false)
                                        //{
                                        //    Global.SapApplication.StatusBar.SetText("Duplicate ItemCodes", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                                        //    return false;
                                        //}

                                        // string strItemcode = "";
                                        // strItemcode = Convert.ToString(oDataTable.GetValue(0, 0));
                                        oForm.DataSources.DBDataSources.Item("POR1").InsertRecord(0);

                                        oForm.DataSources.DBDataSources.Item("POR1").SetValue("U_QuoteNo", 0, oDataTable.GetValue(0, 0).ToString());
                                        //oForm.DataSources.DBDataSources.Item("POR1").SetValue("U_ItemName", 0, oDataTable.GetValue(1, 0).ToString());


                                        //oMatrix.FlushToDataSource();
                                        //SAPbobsCOM.Recordset oRsItm = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));

                                        //string StkQry =
                                        //                 "select A.OnHand,A.LastPurCur,A.Issue-A.Retn As Issued,A.LstPurPrc,A.OrgPrice from " +
                                        //                 "(select OnHand,oitm.LastPurCur," +
                                        //                "(select  sum (INV1.Quantity) from INV1 inner join OFPR on INV1.FinncPriod=OFPR.AbsEntry" +
                                        //                 " where  INV1.DocDate between OFPR.F_TaxDate and  GETDATE() and INV1.ItemCode=OITM.ItemCode)As Issue ," +
                                        //                 "(select  ISNULL( sum (RIN1.Quantity),0) from RIN1 inner join OFPR on RIN1.FinncPriod=OFPR.AbsEntry " +
                                        //                  "where  RIN1.DocDate between OFPR.F_TaxDate and  GETDATE() and RIN1.ItemCode=OITM.ItemCode) as Retn," +
                                        //                 "( select top 1  PCH1.Price from PCH1  where PCH1.ItemCode = OITM.ItemCode and  PCH1.DocDate=" +
                                        //                 "(select MAX(Docdate) from PCH1 where PCH1.ItemCode = OITM.ItemCode) order by pch1.DocEntry desc ) as LstPurPrc," +
                                        //                 "( select top 1 PCH1.Price*OPCH.DocRate from PCH1 inner join OPCH on OPCH.DocEntry=PCH1.DocEntry " +
                                        //                  "where PCH1.ItemCode = OITM.ItemCode " +
                                        //                  "and  PCH1.DocDate= (select MAX(Docdate) from PCH1 where PCH1.ItemCode = OITM.ItemCode) " +
                                        //                  "order by pch1.DocEntry desc) as OrgPrice " +
                                        //                  "from OITM  Where ItemCode='" + oDataTable.GetValue(0, 0).ToString() + "')A";

                                        //oRsItm.DoQuery(StkQry);
                                        //int Stock = System.Convert.ToInt32(oRsItm.Fields.Item("OnHand").Value);
                                        //double LPCost = System.Convert.ToDouble(oRsItm.Fields.Item("LstPurPrc").Value);
                                        //oForm.DataSources.DBDataSources.Item("@PRQ1").SetValue("U_StockHd", 0, Stock.ToString());
                                        //oForm.DataSources.DBDataSources.Item("@PRQ1").SetValue("U_Quantity", 0, "1");
                                        ////oForm.DataSources.DBDataSources.Item("@PRQ1").SetValue("U_LPCost", 0, Convert.ToString(oRsItm.Fields.Item("LastPurCur").Value));
                                        //oForm.DataSources.DBDataSources.Item("@PRQ1").SetValue("U_LPCost", 0, LPCost.ToString());
                                        ////oForm.DataSources.DBDataSources.Item("@PRQ1").SetValue("U_TaxAmnt", 0, Convert.ToString(oRsItm.Fields.Item("OrgPrice").Value));// price in indian Currency
                                        //string strYTD = System.Convert.ToString(oRsItm.Fields.Item("Issued").Value);
                                        //oForm.DataSources.DBDataSources.Item("@PRQ1").SetValue("U_Issued", 0, strYTD);
                                        oMatrix.SetLineData(pVal.Row);
                                        oForm.DataSources.DBDataSources.Item("POR1").Clear();
                                        if (pVal.Row == oMatrix.RowCount)
                                        {
                                            //Matrixrowadd(oMatrix, oForm.DataSources.DBDataSources.Item("POR1"));
                                            oForm.DataSources.DBDataSources.Item("POR1").Clear();
                                        }
                                        oForm.Freeze(false);
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

        #region To Set Warehouse Conditions-CFL

        internal void CFLWhs(SAPbouiCOM.ItemEvent pVal)
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.Item(pVal.FormUID);
                SAPbouiCOM.DBDataSource _dbds_Doc = oForm.DataSources.DBDataSources.Item(0);

                // SAPbouiCOM.ComboBox oBranch = (SAPbouiCOM.ComboBox)form.Items.Item("ddlBranch").Specific;
                // string Branch_Code = oBranch.Selected.Value;

                SAPbouiCOM.IChooseFromListEvent cflEvent = ((SAPbouiCOM.IChooseFromListEvent)(pVal));
                SAPbouiCOM.ChooseFromList cflList = oForm.ChooseFromLists.Item(cflEvent.ChooseFromListUID);
                SAPbouiCOM.Conditions oConds;


                SAPbouiCOM.Condition oCond;

                if (pVal.BeforeAction == true)
                {
                    if (pVal.FormTypeEx == "142")
                    {

                        if (pVal.ItemUID == "38")
                        {
                            SAPbouiCOM.Matrix oMatrix = (SAPbouiCOM.Matrix)(oForm.Items.Item("38").Specific);
                            if (pVal.ColUID == "U_QuoteNo")
                            {
                                if (cflList.ObjectType == "540000006")
                                {

                                    SAPbobsCOM.Recordset oSelect = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                                    string _str_Select = "select OPQT.DocNum from OPQT";
                                    oSelect.DoQuery(_str_Select);
                                    oConds = new SAPbouiCOM.Conditions();
                                    //oConds = new SAPbouiCOM.Conditions();

                                    oCond = oConds.Add();
                                    oCond.BracketOpenNum = 1;

                                    while (!oSelect.EoF)
                                    {
                                        oCond.Alias = "Quotation";
                                        oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                                        oCond.CondVal = oSelect.Fields.Item(0).Value.ToString();
                                        oSelect.MoveNext();
                                        if (!oSelect.EoF)
                                        {
                                            oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_OR;
                                            oCond = oConds.Add();

                                        }

                                    }
                                    oCond.BracketCloseNum = 1;
                                    cflList.SetConditions(oConds);
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        #endregion To Set Warehouse Conditions-CFL

        # region Matrix_Madatory

        internal bool Chk_QuotationNo(SAPbouiCOM.Form frm)
        {
            try
            {
                SAPbouiCOM.DBDataSource dDsTEST = frm.DataSources.DBDataSources.Item("POR1");
                SAPbouiCOM.Matrix mtxPO = (SAPbouiCOM.Matrix)frm.Items.Item("38").Specific;
                for (int i = 1; i <= mtxPO.RowCount - 1; i++)
                {
                    List<string> lstValues = new List<string>();
                    //string strWhere = "DocEntry={0} AND LineId={1}";
                    mtxPO.GetLineData(i);
                    string strSelectedVal = dDsTEST.GetValue("U_QuoteNo", 0).ToString();
                    if (strSelectedVal != "")
                    {
                        //Global.SapApplication.StatusBar.SetText("Sequence No Mismatching ", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                        // mtxStages.Clear();
                        // return true;

                    }

                    else
                    {
                        Global.SapApplication.StatusBar.SetText("Quotation No Missing", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                        return false;
                    }
                }



                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        # endregion

        # region Chk Req Item or Not
        public bool CheckRequestedItem(SAPbouiCOM.Form oForm)
        {
            try
            {

                string strBaseEntry = "";
                string strBaseLine = "";
                string strItemCode = "";
                string strSelectedVal = "";

                SAPbouiCOM.DBDataSource OdbDs_Line = oForm.DataSources.DBDataSources.Item("POR1");
                SAPbobsCOM.Recordset rsCheck = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)oForm.Items.Item("38").Specific;
                for (int i = 1; i <= oMat.RowCount; i++)
                {
                    oMat.GetLineData(i);
                    strItemCode = OdbDs_Line.GetValue("ItemCode", 0);

                    string strIssueqty = "select OITM.U_ReqItem from OITM where OITM.ItemCode='" + strItemCode + "'";
                    rsCheck.DoQuery(strIssueqty);
                    string strRequestedItem = rsCheck.Fields.Item("U_ReqItem").Value.ToString();
                    if (strRequestedItem == "Y")
                    {
                        strBaseEntry = OdbDs_Line.GetValue("U_BEntry1", i);
                        strBaseLine = OdbDs_Line.GetValue("U_BLine1", i);

                        if (strBaseEntry == "" & strBaseLine == "")
                        {
                            return false;
                        }
                        else
                        {
                            SAPbouiCOM.EditText col3 = (SAPbouiCOM.EditText)oMat.Columns.Item("U_QuoteNo").Cells.Item(i).Specific;
                            strSelectedVal = col3.Value;

                            //string strSelectedVal = OdbDs_Line.GetValue("U_QuoteNo", 0).ToString();
                            if (strSelectedVal != "")
                            {

                            }
                            else
                            {
                                //Global.SapApplication.StatusBar.SetText("Quotation No Missing", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                                return false;
                            }


                        }
                    }

                    //if (MPurchaseOrder.Instance.Chk_QuotationNo(oForm))
                    //{
                    //    return true;
                    //}
                    //else
                    //{
                    //    return false;
                    //}
                }




                return true;


            }
            catch (Exception ex)
            {

                return false;
            }


        }
        # endregion




        # region Fill QuoteNo
        public bool FillQuoteNo(SAPbouiCOM.ItemEvent pVal)
        {
            try
            {
                SAPbouiCOM.Form oForm = Global.SapApplication.Forms.Item(pVal.FormUID);
                SAPbouiCOM.EditText Edttext1;
                SAPbouiCOM.DBDataSource dDsOPOR = oForm.DataSources.DBDataSources.Item("POR1");
                SAPbobsCOM.Recordset rsChkStatus = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)oForm.Items.Item("38").Specific;
                for (int i = 1; i <= oMat.RowCount; i++)
                {
                    string strQuoteNo = "";
                    string strItemCode = "";
                    SAPbouiCOM.EditText col3 = (SAPbouiCOM.EditText)oMat.Columns.Item("U_QuoteNo").Cells.Item(i).Specific;
                    strQuoteNo = col3.Value;
                    SAPbouiCOM.EditText col4 = (SAPbouiCOM.EditText)oMat.Columns.Item("1").Cells.Item(i).Specific;
                    strItemCode = col4.Value;


                    if (strQuoteNo != "")
                    {
                        string strItmPrice = @"SELECT PQT1.Price,OPQT.DocDueDate FROM OPQT INNER JOIN PQT1 ON OPQT.DocEntry=PQT1.DocEntry
                                                where OPQT.DocNum= '" + strQuoteNo + "' and PQT1.ItemCode= '" + strItemCode + "'";
                        rsChkStatus.DoQuery(strItmPrice);
                        string strItmNewPrice = rsChkStatus.Fields.Item("Price").Value.ToString();

                        SAPbouiCOM.EditText col2 = (SAPbouiCOM.EditText)oMat.Columns.Item("14").Cells.Item(i).Specific;
                        col2.Value = strItmNewPrice;

                        //edittext1 = (SAPbouiCOM.EditText)oForm.Items.Item("Bankcode").Specific;
                        //edittext1.Value = strBank;


                    }
                    else
                    {
                        // SAPbouiCOM.EditText col2 = (SAPbouiCOM.EditText)oMat.Columns.Item("14").Cells.Item(i).Specific;
                        // col2.Value = "";


                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        # endregion

        # region Fill Delivery Date
        public bool FillDeliveryDate(SAPbouiCOM.Form PForm, int DocEntry)
        {
            try
            {
                //SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(pVal.FormUID);
                SAPbouiCOM.EditText Edttext1;
                SAPbouiCOM.DBDataSource dDsOPOR = PForm.DataSources.DBDataSources.Item("POR1");
                SAPbobsCOM.Recordset rsItem = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                SAPbouiCOM.DataTable DtItems = (SAPbouiCOM.DataTable)PForm.DataSources.DataTables.Item("DtItemsPO");
                IFormatProvider IFPD = new System.Globalization.CultureInfo("en-us", true);
                SAPbobsCOM.Recordset RsDelete = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string strDelete = "delete [@OPOR_DDATE] where DocEntry='" + DocEntry + "'";
                RsDelete.DoQuery(strDelete);
                int rowno = 0;
                for (int i = 0; i < DtItems.Rows.Count; i++)
                {
                    string colItemCode = DtItems.GetValue("colItemCode", i).ToString().Trim();
                    DateTime dateDelivery = Convert.ToDateTime(DtItems.GetValue("colDelDt", i));
                    string colDDate = dateDelivery.ToString("yyyyMMdd", IFPD);
                    int colLineID2 = Convert.ToInt32( DtItems.GetValue("colID", i).ToString().Trim());
                    double colQty = Convert.ToDouble(DtItems.GetValue("colQty", i).ToString().Trim());
                   string colStat = DtItems.GetValue("colStat", i).ToString().Trim();
                   string strQry = "SELECT LineNum  FROM [POR1] where [ItemCode]='" + colItemCode + "' and [DocEntry]='" + DocEntry + "' ";
                   rsItem.DoQuery(strQry);

                   int LineEntry = Convert.ToInt32(rsItem.Fields.Item("LineNum").Value.ToString());
                   AddDeliveryDate(colItemCode, colDDate, DocEntry, LineEntry,i,colQty,colStat);



                }
                 int iErrorCode = 0;
                
                if (iErrorCode == 0)
                {
                    Global.SapApplication.StatusBar.SetText("Successfully Saved", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                    return true;

                }
                else
                {

                    if (Global.SapCompany.InTransaction)
                        Global.SapCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                    string sErrorMsg = Global.SapCompany.GetLastErrorDescription();
                    Global.SapApplication.MessageBox(sErrorMsg, 1, "Ok", "", "");
                    return false;
                }
              //  Global.SapApplication.StatusBar.SetText("DB Error !!", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }
        # endregion
        #region Add to DeliveryDate
        public bool AddDeliveryDate(string colItemCode, string colDDate, int DocEntry, int LineID1,int LineID,double Qty,string colStatus)
        {
            try
            {
            // Global.SapApplication.Forms.ActiveForm.Mode == SAPbouiCOM.BoFormMode.fm_UPDATE_MODE 
              //  pVal.FormMode == (int)SAPbouiCOM.BoFormMode.fm_UPDATE_MODE



               


                SAPbobsCOM.Recordset rsItem = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);


                string strAddQry = @"INSERT INTO [@OPOR_DDATE]([DocEntry],[U_LineId1],[U_DelDate],[U_ItemCode],[LineId],[U_Qty],[U_DpStat])  
                                     VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}') ";
                strAddQry = string.Format(strAddQry, DocEntry, LineID1, colDDate, colItemCode, LineID,Qty,colStatus);
                rsItem.DoQuery(strAddQry);
            }


            catch {
                return false;
            }
            return true;
        }




        #endregion

        public bool UpdateAfterAddForRQList(SAPbouiCOM.Form oForm, int intDoc)
        {
            int Document = 0;
            try
            {
                SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)oForm.Items.Item("38").Specific;
                SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;
                SAPbobsCOM.Recordset rsPOUser = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                SAPbobsCOM.Recordset rsRequest = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                SAPbobsCOM.Recordset rsIssue = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                SAPbobsCOM.Recordset rsAproval = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                SAPbobsCOM.Recordset oRsDoc2 = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                Document = intDoc;
                if (Document != 0)
                {
                    string strBaseline = " select POR1.Quantity,POR1.U_BEntry1,POR1.U_BLine1 from POR1  where POR1.DocEntry='" + Document + "' ";
                    rsPOUser.DoQuery(strBaseline);
                    while (!rsPOUser.EoF)
                    {
                        string strPurQty = rsPOUser.Fields.Item("Quantity").Value.ToString();
                        string strBaseEntry = rsPOUser.Fields.Item("U_BEntry1").Value.ToString();
                        string strBaseLine_1 = rsPOUser.Fields.Item("U_BLine1").Value.ToString();
                        string strqty = "select  ISNULL([@PRQ1].U_BalQty,0)Qty from [@PRQ1] where [@PRQ1].DocEntry='" + strBaseEntry + "' and [@PRQ1].LineId='" + strBaseLine_1 + "'";
                        rsIssue.DoQuery(strqty);
                        // double dblIssued = Convert.ToDouble(rsIssue.Fields.Item("Issued").Value);
                        double dblTotalQty = Convert.ToDouble(rsIssue.Fields.Item("Qty").Value);
                        //double dblApQty = Convert.ToDouble(rsIssue.Fields.Item("ApQty").Value);

                        double dblPurqty = Convert.ToDouble(strPurQty);
                        double dblBalQty = (dblTotalQty - dblPurqty);
                        //double dblFinalAprQty = (dblApQty - dblTottalIssued);

                        string strIssueRequestion = " update [@PRQ1] set [@PRQ1].U_BalQty='" + dblBalQty + "' where [@PRQ1].DocEntry='" + strBaseEntry + "' and [@PRQ1].LineId='" + strBaseLine_1 + "'";
                        rsRequest.DoQuery(strIssueRequestion);
                        int count=0;
                        string strQuery1 = @"select count(*) cnt from  [@PRQ1] where Docentry='" + strBaseEntry + "'  and ISNULL(U_BalQty,0)>0";
                        rsAproval.DoQuery(strQuery1);
                        if (!rsAproval.EoF)
                        {
                             count = Convert.ToInt32(rsAproval.Fields.Item("cnt").Value);
                        }
                        if (count == 0)
                        {

                            string strBaseQry = " UPDATE [@OPRQ] SET [@OPRQ].U_DocStatu='Closed' where [@OPRQ].DocEntry='" + strBaseEntry + "' ";
                            oRsDoc2.DoQuery(strBaseQry);
                        }
                        rsPOUser.MoveNext();
                    }
                    
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
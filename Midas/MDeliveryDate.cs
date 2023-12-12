using System;
using System.Collections.Generic;
using System.Text;

namespace VKC
{
    class MDeliveryDate
    {
        General gen = new General();
        public int docentry;
        public int NewDoc = 0;
        static int rowNo;
        SAPbouiCOM.DataTable DtItems ;
        SAPbouiCOM.ItemEvent Pval;
        SAPbouiCOM.Form PForm;
        #region Singleton

        private static MDeliveryDate instance;

        public static MDeliveryDate Instance
        {
            get
            {
                if (instance == null) instance = new MDeliveryDate();

                return instance;
            }
        }

        #endregion

        public MDeliveryDate()
        {
            VDeliveryDate vp = VDeliveryDate.Instance;
        }

        # region InitialSettings
        internal void Initalsetting(SAPbouiCOM.ItemEvent val )
        {
            try
            {
                if (val.FormTypeEx == "142")
                {
                    //rowNo = val.Row;
                    //SAPbouiCOM.Form newForm = Global.SapApplication.Forms.ActiveForm;
                    //UpdateBalQty(newForm);
                }
            }
            catch (Exception ex)
            {
            }
        }
        # endregion
      
        #region AddDatatable
        internal void AddDatatable(SAPbouiCOM.ItemEvent val)
        {
            try
            {
                #region Create Data Table
                SAPbouiCOM.Form form = Global.SapApplication.Forms.Item(val.FormUID);
                SAPbouiCOM.DataTable DtItems = (SAPbouiCOM.DataTable)form.DataSources.DataTables.Add("DtItemsPO");
                DtItems.Clear();		// Clear DT
                DtItems.Columns.Add("colID", SAPbouiCOM.BoFieldsType.ft_Integer, 8); //Add Column
                DtItems.Columns.Add("colItemCode", SAPbouiCOM.BoFieldsType.ft_AlphaNumeric, 100); //Add Column
                DtItems.Columns.Add("colDelDt", SAPbouiCOM.BoFieldsType.ft_Date, 8); //Add Column
                DtItems.Columns.Add("colRowNo", SAPbouiCOM.BoFieldsType.ft_Integer, 8); //Add Column
                DtItems.Columns.Add("colQty", SAPbouiCOM.BoFieldsType.ft_Quantity, 10); //Add Column
                DtItems.Columns.Add("colStat", SAPbouiCOM.BoFieldsType.ft_AlphaNumeric, 2);
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
        #region Fill Datatable for Updation
        internal void FillDatatableUpdate(SAPbouiCOM.Form form ,int docentry)
        {
            try
            {
               
                SAPbouiCOM.DataTable DtItems = (SAPbouiCOM.DataTable)form.DataSources.DataTables.Item("DtItemsPO");
                DtItems.Rows.Clear();
                SAPbobsCOM.Recordset rSetDdate = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                string strDdate = "SELECT U_LineId1,U_DelDate,U_ItemCode,LineId,U_Qty,U_DpStat FROM [@OPOR_DDATE] WHERE [DocEntry] = '" + docentry + "'";
                rSetDdate.DoQuery(strDdate);
                if (rSetDdate.RecordCount != 0)
                {
                    while (!rSetDdate.EoF)
                    {
                        DtItems.Rows.Add(1);
                        DtItems.SetValue("colItemCode", DtItems.Rows.Count - 1, rSetDdate.Fields.Item("U_ItemCode").Value.ToString());
                        DtItems.SetValue("colDelDt", DtItems.Rows.Count - 1, Convert.ToDateTime(rSetDdate.Fields.Item("U_DelDate").Value.ToString()));
                        DtItems.SetValue("colRowNo", DtItems.Rows.Count - 1, Convert.ToInt32(rSetDdate.Fields.Item("U_LineId1").Value.ToString()));
                        DtItems.SetValue("colQty", DtItems.Rows.Count - 1, Convert.ToDouble(rSetDdate.Fields.Item("U_Qty").Value.ToString()));
                        DtItems.SetValue("colStat", DtItems.Rows.Count - 1, rSetDdate.Fields.Item("U_DpStat").Value.ToString());
                       
                        rSetDdate.MoveNext();
                    }
                }
            }
            catch { }

        }
        #endregion

        #region Delete Un Wanted Row
        internal bool DeleteUnWantedRow(SAPbouiCOM.Form oForm)
        {
            try
            {
                SAPbouiCOM.Matrix _mat = (SAPbouiCOM.Matrix)oForm.Items.Item("mtxDlvryDt").Specific;
                for (int i = 1; i <= _mat.RowCount; i++)
                {

                    SAPbouiCOM.EditText col1 = (SAPbouiCOM.EditText)_mat.Columns.Item("colDelDt").Cells.Item(i).Specific;
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

        #region Fill Datatable
        internal bool FillDatatable(SAPbouiCOM.ItemEvent val)
        {
            try
            {
                SAPbouiCOM.Form CForm = Global.SapApplication.Forms.Item(val.FormUID);
                int rowNo = Convert.ToInt32(CForm.DataSources.UserDataSources.Item("RowVal").Value);
                
                      
                PForm = Global.SapApplication.Forms.Item(CForm.DataSources.UserDataSources.Item("PFormID").Value);
              //  val.FormMode == (int)SAPbouiCOM.BoFormMode.fm_UPDATE_MODE & Global.SapApplication.Forms.ActiveForm.Mode == SAPbouiCOM.BoFormMode.fm_UPDATE_MODE
                if (PForm.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                {
                    PForm.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
                }
                SAPbouiCOM.DataTable DtItems = (SAPbouiCOM.DataTable)PForm.DataSources.DataTables.Item("DtItemsPO");
                //SAPbouiCOM.DBDataSource DDate = (SAPbouiCOM.DBDataSource)CForm.DataSources.DBDataSources.Item("@OPOR_DDATE");
                SAPbouiCOM.Matrix Mtxitem = (SAPbouiCOM.Matrix)CForm.Items.Item("mtxDlvryDt").Specific;
                SAPbouiCOM.Matrix MatrixPform = (SAPbouiCOM.Matrix)PForm.Items.Item("38").Specific;
                SAPbouiCOM.EditText EdtItemRowNo = (SAPbouiCOM.EditText)MatrixPform.Columns.Item("1").Cells.Item(rowNo).Specific;
                SAPbouiCOM.DBDataSource DPO = (SAPbouiCOM.DBDataSource)PForm.DataSources.DBDataSources.Item("POR1");
              
                int j = DtItems.Rows.Count;
                int i = 0;


                if (DtItems.Rows.Count > 0)
                {
                     int colRowNo;
                     DateTime colDate;
                     string Item;
                     string colStat;
                     int count = DtItems.Rows.Count;
                    for (int k = 0; k < count; k++)
                    {

                       colRowNo = Convert.ToInt32(DtItems.GetValue("colRowNo", k).ToString().Trim());
                       colDate = Convert.ToDateTime(DtItems.GetValue("colDelDt", k).ToString().Trim());
                       colStat = DtItems.GetValue("colStat", k).ToString().Trim();
                       Item = DtItems.GetValue("colItemCode", k).ToString().Trim();
                        //if (colRowNo == rowNo)
                        //{
                        //    DtItems.Rows.Remove(k);   
                          
                        //}
                        //--------modified on 31-03-2010--------
                       if (colRowNo==rowNo-1)
                       {
                           DtItems.Rows.Remove(k);
                           k--;
                           count--;
                       }
                    }
                   // int a = DtItems.Rows.Count;
                }
               // DtItems.Rows.Clear();
               
                for (i = 1; i <=Mtxitem.RowCount; i++)
                {


                    SAPbouiCOM.EditText EdtItem = (SAPbouiCOM.EditText)Mtxitem.Columns.Item("colDelDt").Cells.Item(i).Specific;
                    SAPbouiCOM.EditText EdtQty = (SAPbouiCOM.EditText)Mtxitem.Columns.Item("colQty").Cells.Item(i).Specific;
                    SAPbouiCOM.ComboBox cmbStatus = (SAPbouiCOM.ComboBox)Mtxitem.Columns.Item("colStat").Cells.Item(i).Specific;
                    DtItems.Rows.Add(1);
                    DtItems.SetValue("colItemCode", DtItems.Rows.Count-1 , EdtItemRowNo.Value);
                    DtItems.SetValue("colDelDt", DtItems.Rows.Count-1, EdtItem.Value);
                    DtItems.SetValue("colRowNo", DtItems.Rows.Count-1, rowNo-1);
                    DtItems.SetValue("colID", DtItems.Rows.Count - 1, DtItems.Rows.Count -1);
                    DtItems.SetValue("colQty", DtItems.Rows.Count - 1, EdtQty.Value);
                    DtItems.SetValue("colStat", DtItems.Rows.Count - 1, cmbStatus.Value);
                    //DtItems.SetValue("colDelDt", DtItems.Rows.Count - 1, EdtItem.Value);
                    
                   //int a= DtItems.Rows.Count;

                }

            }
            catch (Exception ex)
            {
                return false;
                Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
            }

            return true;

        }
        #region CheckValidQty
        internal bool CheckValidQty(SAPbouiCOM.Form FrmDDate)
        {
            try
            {

                int rowNo = Convert.ToInt32(FrmDDate.DataSources.UserDataSources.Item("RowVal").Value);
                PForm = Global.SapApplication.Forms.Item(FrmDDate.DataSources.UserDataSources.Item("PFormID").Value);
                SAPbouiCOM.Matrix MtxItem = (SAPbouiCOM.Matrix)FrmDDate.Items.Item("mtxDlvryDt").Specific;
                SAPbouiCOM.Matrix MatrixPform = (SAPbouiCOM.Matrix)PForm.Items.Item("38").Specific;
                SAPbouiCOM.EditText EdtItemPOQty = (SAPbouiCOM.EditText)MatrixPform.Columns.Item("11").Cells.Item(rowNo).Specific;
                double TotalQty = 0;
                for (int i = 1; i <= MtxItem.RowCount; ++i)
                {
                    SAPbouiCOM.EditText EdtItem = (SAPbouiCOM.EditText)MtxItem.Columns.Item("colQty").Cells.Item(i).Specific;
                     SAPbouiCOM.EditText EdtItemDate = (SAPbouiCOM.EditText)MtxItem.Columns.Item("colDelDt").Cells.Item(i).Specific;
                    if (EdtItem.Value != "")
                    {
                                double Qty = Convert.ToDouble(EdtItem.Value);
                               
                              
                                if (Qty > 0)
                                {
                                    TotalQty = TotalQty + Qty;
                                    
                                }
                              
                                else
                                {
                                    Global.SapApplication.MessageBox("Please Enter Qty !!", 1, "OK", "", "");
                                    return false;
                                }
                                if (EdtItemDate.Value == "")
                                {
                                    Global.SapApplication.MessageBox("Please Enter Date !!", 1, "OK", "", "");
                                    return false;
                                }
                                
                    }
                    else
                    {
                        return false;
                    }
  
                }
                if (TotalQty > Convert.ToDouble(EdtItemPOQty.Value))
                {
                  
                   
                     Global.SapApplication.MessageBox("Total Qty must be less than Order Qty", 1, "OK", "", "");
                     return false;
                                       
                }

                return true;
            }
            catch { return false; }
            return true;
        }
        #endregion

        #region UpdateBalanceQty
        internal void UpdateBalQty(SAPbouiCOM.Form FrmDDate)
        {
            try
            {

                int rowNo = Convert.ToInt32(FrmDDate.DataSources.UserDataSources.Item("RowVal").Value);
                PForm = Global.SapApplication.Forms.Item(FrmDDate.DataSources.UserDataSources.Item("PFormID").Value);
                SAPbouiCOM.Matrix MtxItem = (SAPbouiCOM.Matrix)FrmDDate.Items.Item("mtxDlvryDt").Specific;
                SAPbouiCOM.Matrix MatrixPform = (SAPbouiCOM.Matrix)PForm.Items.Item("38").Specific;
                SAPbouiCOM.EditText EdtItemPOQty = (SAPbouiCOM.EditText)MatrixPform.Columns.Item("11").Cells.Item(rowNo).Specific;
                SAPbouiCOM.StaticText lblPOQty = (SAPbouiCOM.StaticText)FrmDDate.Items.Item("5").Specific;
                double TotalQty = 0;
                double QtyPO = Convert.ToDouble(EdtItemPOQty.Value);
                for (int i = 1; i <= MtxItem.RowCount; ++i)
                {
                    SAPbouiCOM.EditText EdtItem = (SAPbouiCOM.EditText)MtxItem.Columns.Item("colQty").Cells.Item(i).Specific;
                   
                    if (EdtItem.Value != "")
                    {
                        double Qty = Convert.ToDouble(EdtItem.Value);
                                         
                        if (Qty > 0)
                        {
                            TotalQty = TotalQty + Qty;

                        }

                      
                       

                    }
                   
                }
                lblPOQty.Caption = Convert.ToString(QtyPO - TotalQty);

               

               
            }
            catch {  }
           
        }
        #endregion

        #region Fill Matrix Item
        internal void FillMtxItem(SAPbouiCOM.Form FrmDDate)
        {
            try
            {

            int rowNo = Convert.ToInt32(FrmDDate.DataSources.UserDataSources.Item("RowVal").Value);
            PForm = Global.SapApplication.Forms.Item(FrmDDate.DataSources.UserDataSources.Item("PFormID").Value);
            SAPbouiCOM.DataTable DtItems = (SAPbouiCOM.DataTable)PForm.DataSources.DataTables.Item("DtItemsPO");
            SAPbouiCOM.Matrix MtxItem = (SAPbouiCOM.Matrix)FrmDDate.Items.Item("mtxDlvryDt").Specific;
            SAPbouiCOM.DBDataSource DDate = (SAPbouiCOM.DBDataSource)FrmDDate.DataSources.DBDataSources.Item("@OPOR_DDATE");
            SAPbouiCOM.DBDataSource DPO = (SAPbouiCOM.DBDataSource)PForm.DataSources.DBDataSources.Item("POR1");
            

            ////if (docentry > 0)
            ////{
              
            ////    //DPO.Clear();
            ////    //DPO.InsertRecord(0);
            ////    SAPbouiCOM.Matrix MatrixPform = (SAPbouiCOM.Matrix)PForm.Items.Item("38").Specific;

            ////    MatrixPform.GetLineData(rowNo);
            ////    //  string a = DPO.GetValue("ItemCode", rowNo-1).ToString().Trim();
            ////    rowNo = Convert.ToInt32(DPO.GetValue("LineNum", rowNo-1).ToString().Trim());

            ////}
            //else
            //{
                IFormatProvider IFPD = new System.Globalization.CultureInfo("en-us", true);


                if (DtItems.Rows.Count > 0)
                {
                    for (int i = 0; i < DtItems.Rows.Count; i++)
                    {
                        string colItemCode = DtItems.GetValue("colItemCode", i).ToString().Trim();
                        DateTime dateDelivery = Convert.ToDateTime(DtItems.GetValue("colDelDt", i));
                        string colDDate = dateDelivery.ToString("yyyyMMdd", IFPD);
                        int colRowNo = Convert.ToInt32(DtItems.GetValue("colRowNo", i).ToString().Trim());
                        int colID = Convert.ToInt32(DtItems.GetValue("colID", i).ToString().Trim()) + 1;
                        double colQty = Convert.ToDouble(DtItems.GetValue("colQty", i).ToString().Trim());
                        string colStat = DtItems.GetValue("colStat", i).ToString().Trim();
                        if (colRowNo == rowNo-1)
                        {
                            MtxItem.AddRow(1, MtxItem.RowCount);
                            MtxItem.GetLineData(MtxItem.RowCount);
                            DDate.SetValue("DocEntry", 0, colID.ToString());
                            DDate.SetValue("U_DelDate", 0, colDDate);
                            DDate.SetValue("U_Qty", 0, colQty.ToString());
                            DDate.SetValue("U_DpStat", 0, colStat.ToString());
                            MtxItem.SetLineData(MtxItem.RowCount);
                            GetLineNo(MtxItem);
                        }
                       // GetLineNo(MtxItem);
                    }
                    
                }
               
            }
                 
           // }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
            }
        }
        #endregion
       

       
        # region MatrixAdd
        public bool MatrixAdd(SAPbouiCOM.Matrix oMatrix, SAPbouiCOM.DBDataSource DbMat)
        {

            try
            {
               
               //if (oMatrix.RowCount == 0 )
               // {
               //     oMatrix.AddRow(1, oMatrix.RowCount);
               // }
               // else
               // {
                //  SAPbouiCOM.DBDataSource DDate = (SAPbouiCOM.DBDataSource)FrmDDate.DataSources.DBDataSources.Item("@OPOR_DDATE");
 
                    SAPbouiCOM.EditText oEdit = (SAPbouiCOM.EditText)oMatrix.Columns.Item("colQty").Cells.Item(oMatrix.RowCount).Specific;
                    if (oEdit == null | oEdit.Value == "")
                    {
                        oMatrix.AddRow(1, oMatrix.RowCount);
                    }
                    else if (oEdit.Value != "")
                        oMatrix.AddRow(1, oMatrix.RowCount);

                    if (oMatrix.RowCount > 0 )
                    {
                        SAPbouiCOM.EditText oEditDate = (SAPbouiCOM.EditText)oMatrix.Columns.Item("colDelDt").Cells.Item(oMatrix.RowCount).Specific;
                        SAPbouiCOM.EditText oEditQty = (SAPbouiCOM.EditText)oMatrix.Columns.Item("colQty").Cells.Item(oMatrix.RowCount).Specific;
                        SAPbouiCOM.ComboBox cmbStatus = (SAPbouiCOM.ComboBox)oMatrix.Columns.Item("colStat").Cells.Item(oMatrix.RowCount).Specific;
                        oEditDate.Value = "";
                        oEditQty.Value = "";
                        cmbStatus.Select("0", SAPbouiCOM.BoSearchKey.psk_Index);

                    }
                //}
                //Line Number Increament
                //int doc = 1;
                //SAPbouiCOM.EditText oEdit1 = (SAPbouiCOM.EditText)oMatrix.Columns.Item("DocEntry").Cells.Item(oMatrix.RowCount).Specific;
                //if (oMatrix.RowCount > 1)
                //{
                //    doc += oMatrix.RowCount - 1;
                   
                //}
                //oEdit1.Value = System.Convert.ToString(doc);

                GetLineNo(oMatrix);
                return true;
            }
           
            catch
            {
                
                return false;
            }
        }
        #  endregion

        #region LineNumber
        public bool GetLineNo(SAPbouiCOM.Matrix oMatrix)
        {
            //Line Number Increament
            int doc = 1;
            SAPbouiCOM.EditText oEdit1 = (SAPbouiCOM.EditText)oMatrix.Columns.Item("colID").Cells.Item(oMatrix.RowCount).Specific;
            if (oMatrix.RowCount > 1)
            {
                doc += oMatrix.RowCount - 1;

            }
            oEdit1.Value = System.Convert.ToString(doc);


            return true;
        }
        #endregion
    }
        #endregion
}
       

 
      

      

       


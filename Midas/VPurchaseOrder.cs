using System;
using System.Collections.Generic;
using System.Text;

namespace VKC
{
    class VPurchaseOrder
    {

      General gen = new  General();
        public SAPbouiCOM.Form oForm = null;

        #region Singleton

        private static VPurchaseOrder instance;

        public static VPurchaseOrder Instance
        {
            get
            {
                if (instance == null) instance = new VPurchaseOrder();

                return instance;
            }
        }

        #endregion



         public VPurchaseOrder()
        {
         }
        ~VPurchaseOrder()
        {
        }


        public bool SBO_Application_ItemEvent(SAPbouiCOM.ItemEvent val)
        {
            try
            {
             // if (val.EventType == SAPbouiCOM.BoEventTypes.et_FORM_ACTIVATE & val.BeforeAction == false)
                if (val.EventType == SAPbouiCOM.BoEventTypes.et_FORM_LOAD & val.BeforeAction == true)

                {
                    SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;
                    MDeliveryDate.Instance.AddDatatable(val);
                    if (frm.TypeEx == "169")
                    {
                     //   MPurchaseOrder.Instance.Initalsetting(frm);
                    }
                        //VPurchaseOrder.instance.SBO_Application_ItemEvent(val);

        }

        #region  Choose From List event
        if (val.EventType == SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST)
        {
            oForm = Global.SapApplication.Forms.Item(val.FormUID);
            try
            {
                // oForm.Freeze(true);
                //pr.Choofromlist_Req(oForm, pVal);
                MPurchaseOrder.Instance.CFLWhs(val);
                // oForm.Freeze(false);
            }
            catch
            {
                oForm.Freeze(false);
            }
        }
        #endregion

        if (val.EventType == SAPbouiCOM.BoEventTypes.et_FORM_ACTIVATE & val.BeforeAction == false)
        {
            SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;
            MPurchaseOrder.Instance.Initalsetting(frm);
        }
        if (val.ItemUID == "1")
        {
       
        }
        if (val.ItemUID == "1" & val.EventType == SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED & val.BeforeAction == true & val.FormMode == (int)SAPbouiCOM.BoFormMode.fm_UPDATE_MODE & Global.SapApplication.Forms.ActiveForm.Mode == SAPbouiCOM.BoFormMode.fm_UPDATE_MODE)
        {
            SAPbouiCOM.Form CForm = Global.SapApplication.Forms.Item(val.FormUID);
            SAPbouiCOM.Matrix oMatPO = (SAPbouiCOM.Matrix)CForm.Items.Item("38").Specific;
            oMatPO.Columns.Item("U_BEntry1").Cells.Item(1).Click(SAPbouiCOM.BoCellClickType.ct_Regular, 0);
         SAPbouiCOM.Form   frmDataEvent = Global.SapApplication.Forms.Item(val.FormUID);

         MPurchaseOrder.Instance.FillDeliveryDate(frmDataEvent, MPurchaseOrder.Instance.docentry);
        }           
                    //////}


                    //////if (val.ItemUID == "1" & val.EventType == SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED & val.BeforeAction == true & (val.FormMode == (int)SAPbouiCOM.BoFormMode.fm_ADD_MODE ))
                    //////{
                    //////    SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;

                    //////    if (MPurchaseOrder.Instance.CheckRequestedItem(frm))
                    //////    {

                    //////        //if (MPurchaseOrder.Instance.Chk_QuotationNo(frm))
                    //////        //{

                    //////            if (MPurchaseOrder.Instance.UpdateBeforeAdd(frm))
                    //////            {
                    //////                return true;
                    //////            }
                    //////            else
                    //////            {
                    //////                Global.SapApplication.StatusBar.SetText("Purchase Order Quantity Greater than Aproved Quantity ", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    //////                return false;
                    //////            }
                          
                    //////        return true;
                    //////    }

                    //////    else
                    //////    {
                    //////        Global.SapApplication.StatusBar.SetText("Purchase Intend needed Some Items or Valid Quotation No Missing ", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    //////        return false;
                    //////    }

                    //////}


                    //////if (val.ItemUID == "1" & val.EventType == SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED & val.BeforeAction == true & (val.FormMode == (int)SAPbouiCOM.BoFormMode.fm_UPDATE_MODE))
                    //////{

                    //////    SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;

                    //////    if (MPurchaseOrder.Instance.CheckRequestedItem(frm))
                    //////    {

                    //////        //if (MPurchaseOrder.Instance.Chk_QuotationNo(frm))
                    //////        //{

                    //////        if (MPurchaseOrder.Instance.UpdateBeforeAdd_Update1(frm))
                    //////        {
                    //////            return true;
                    //////        }
                    //////        else
                    //////        {
                    //////            Global.SapApplication.StatusBar.SetText("Purchase Order Quantity Greater than Aproved Quantity ", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    //////            return false;
                    //////        }

                    //////        return true;
                    //////    }

                    //////    else
                    //////    {
                    //////        Global.SapApplication.StatusBar.SetText("Purchase Intend needed Some Items or Valid Quotation No Missing ", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    //////        return false;
                    //////    }


                    //////}



                    //////if (val.ItemUID == "10000329" & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK & val.FormMode == (int)SAPbouiCOM.BoFormMode.fm_OK_MODE & val.Before_Action == true)
                    //////{
                    //////    try
                    //////    {
                           

                    //////    }
                    //////    catch (Exception ex)
                    //////    {
                    //////        string strmsg = ex.Message;
                    //////    }
                    //////}


                    //////if (val.ItemUID == "38" & val.ColUID == "U_QuoteNo" & val.EventType == SAPbouiCOM.BoEventTypes.et_LOST_FOCUS & val.BeforeAction == false)
                    //////{
                    //////    if(MPurchaseOrder.Instance.FillQuoteNo(val))
                    //////    {
                    //////        return true;
                    //////    }
                    //////    else
                    //////    {
                    //////        return false;
                    //////    }
                    //////}

        if (val.ItemUID == "38" & val.ColUID == "11" & val.EventType == SAPbouiCOM.BoEventTypes.et_LOST_FOCUS & val.BeforeAction == false & (val.FormMode == (int)SAPbouiCOM.BoFormMode.fm_ADD_MODE)&& val.Row > 0)
        {
            SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;
            if (MPurchaseOrder.Instance.UpdateBeforeAdd_ItemQty(val))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //if (Global.SapApplication.Forms.ActiveForm.Mode == SAPbouiCOM.BoFormMode.fm_UPDATE_MODE&val.FormMode == (int)SAPbouiCOM.BoFormMode.fm_UPDATE_MODE)
        //{
        //    int a = 1;
        //}


                    if (val.ItemUID == "38" & val.ColUID == "U_DelvryID" & val.EventType == SAPbouiCOM.BoEventTypes.et_DOUBLE_CLICK & val.BeforeAction == true)
                    {
                        try
                        {
                          SAPbouiCOM.Form  PForm = Global.SapApplication.Forms.Item(val.FormUID);
                            SAPbouiCOM.Matrix MatrixPform = (SAPbouiCOM.Matrix)PForm.Items.Item("38").Specific;
                            SAPbouiCOM.EditText EdtItem = (SAPbouiCOM.EditText)MatrixPform.Columns.Item("1").Cells.Item(val.Row).Specific;

                            string ItemCode = EdtItem.Value;
                            if (ItemCode != "")
                            {
                                MPurchaseOrder.Instance.LoadXML(val, "DeliveryDate.xml");

                                SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;

                                frm.DataSources.UserDataSources.Item("PFormID").ValueEx = val.FormUID;
                                frm.DataSources.UserDataSources.Item("RowVal").ValueEx = val.Row.ToString();
                                Global.bubblevalue = VDeliveryDate.Instance.SBO_Application_ItemEvent(val);

                            
                                MDeliveryDate.Instance.FillMtxItem(frm);
                                SAPbouiCOM.Matrix oMatrix = (SAPbouiCOM.Matrix)frm.Items.Item("mtxDlvryDt").Specific;
                                if (oMatrix.RowCount == 0)
                                {
                                    oMatrix.AddRow(1, oMatrix.RowCount);
                                    MDeliveryDate.Instance.GetLineNo(oMatrix);
                                }
                                MDeliveryDate.Instance.UpdateBalQty(frm);
                            }
                            else
                            {
                                Global.SapApplication.MessageBox("Please Select an Item", 1, "OK", "", "");
                            }
                           
                           

                        }
                        catch { }
                    }

                    
             
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        

        public bool SBO_Application_FormDataEvent(SAPbouiCOM.BusinessObjectInfo BusinessObjectInfo)
        {
            SAPbouiCOM.Form frmDataEvent;
            try
            {
                frmDataEvent = Global.SapApplication.Forms.Item(BusinessObjectInfo.FormUID);

                if (BusinessObjectInfo.FormTypeEx == "142" & BusinessObjectInfo.ActionSuccess == true & BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_LOAD)
                {
                    
                   SAPbouiCOM.DBDataSource oDataSRC = Global.SapApplication.Forms.Item(BusinessObjectInfo.FormUID).DataSources.DBDataSources.Item("OPOR");

                   MPurchaseOrder.Instance.docentry = Convert.ToInt32(oDataSRC.GetValue("DocEntry", 0));
                 
                   MPurchaseOrder.Instance.Initalsetting(frmDataEvent);
                   MDeliveryDate.Instance.FillDatatableUpdate(frmDataEvent, MPurchaseOrder.Instance.docentry);

                }
                if (BusinessObjectInfo.FormTypeEx == "142" & BusinessObjectInfo.ActionSuccess == false & BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_LOAD)
                {

                    //SAPbouiCOM.DBDataSource oData_Doc = Global.SapApplication.Forms.Item(BusinessObjectInfo.FormUID).DataSources.DBDataSources.Item("OPOR");
                    //int IntDocentry = Convert.ToInt32(oData_Doc.GetValue("DocEntry", 0));
                    //MPurchaseOrder.Instance.Initalsetting(frmDataEvent);
                }
                if (BusinessObjectInfo.FormTypeEx == "142" & BusinessObjectInfo.ActionSuccess == true & BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD)
                {
                    SAPbouiCOM.DBDataSource oData_Doc = Global.SapApplication.Forms.Item(BusinessObjectInfo.FormUID).DataSources.DBDataSources.Item("OPOR");
                    int IntDocentry = Convert.ToInt32(oData_Doc.GetValue("DocEntry", 0));
                    SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(BusinessObjectInfo.FormUID);
                    SAPbouiCOM.Form frm = Global.SapApplication.Forms.Item(oForm.DataSources.UserDataSources.Item("CFormP").Value);
                    MPurchaseOrder.Instance.FillDeliveryDate(PForm, IntDocentry);
                    string  frmUID = "";
                    frmUID = frm.TypeEx;
                    if (frmUID == "PurchaseRequisition")
                    {
                        if (MPurchaseOrder.Instance.UpdateAfterAdd(frmDataEvent, IntDocentry))
                        {

                            return true;
                        }
                        else
                        {
                            //Global.SapApplication.StatusBar.SetText("Quantity Greater than Aproved Quantity ", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                            return false;
                        }
                    }
                    else  if (frmUID == "frmRequsition")
                    {
                    if (MPurchaseOrder.Instance.UpdateAfterAddForRQList(frmDataEvent, IntDocentry))
                    {

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    }
                  
                }


            }
            catch (Exception ex)
            {
                return false;

            }
            return true;

        }

        #region RightClick Event
        // private void SapApplication_RightClickEvent(ref SAPbouiCOM.ContextMenuInfo eventInfo, out bool BubbleEvent)
        // public  bool SBO_Application_ s(ref SAPbouiCOM.ContextMenuInfo eventInfo, out bool BubbleEvent)
        public bool SapApplication_RightClickEvent(SAPbouiCOM.ContextMenuInfo eventInfo)
        {
            //BubbleEvent = true;
            try
            {

                //SAPbouiCOM.Form frm = Global.SapApplication.Forms.GetFormByTypeAndCount(65211, 1);
                //SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;
                //if (MPurchaseOrder.Instance.Disable_IssueComponents(frm))
                //{
                //    eventInfo.RemoveFromContent("5923");
                //}
                //else
                //{
                //    //MProductionOrder.Instance.DisableAll(frm);
                //    // return;
                //}
                //eventInfo.RemoveFromContent("5923");

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        #endregion


    }
}

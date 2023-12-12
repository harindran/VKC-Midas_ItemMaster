using System;
using System.Collections.Generic;
using System.Text;

namespace VKC
{
    class VPurchaseRequisition
    {
        General gen = new General();
        public SAPbouiCOM.Form oForm = null;

        #region Singleton

        private static VPurchaseRequisition instance;

        public static VPurchaseRequisition Instance
        {
            get
            {
                if (instance == null) instance = new VPurchaseRequisition();

                return instance;
            }
        }

        #endregion


        public VPurchaseRequisition()
        {
         }
        ~ VPurchaseRequisition()
        {
        }

        public bool SBO_Application_ItemEvent(SAPbouiCOM.ItemEvent val)
        {
            try
            {
              bool bubblevalue = true;
                if (val.FormTypeEx == "PurchaseRequisition")
                {

                    #region  Choose From List event
                    if (val.EventType == SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST)
                    {
                        oForm = Global.SapApplication.Forms.Item(val.FormUID);
                        try
                        {
                            // oForm.Freeze(true);
                            //pr.Choofromlist_Req(oForm, pVal);
                            if (MPurchaseRequisition.Instance.Choofromlist_Req(oForm, val) == false)
                            {
                                return false;
                            }
                            // oForm.Freeze(false);
                        }
                        catch
                        {
                            oForm.Freeze(false);
                        }
                    }
                    #endregion

                   

                    if (val.ItemUID == "mtxPurReq" & val.ColUID == "colPrice" & val.EventType == SAPbouiCOM.BoEventTypes.et_VALIDATE & val.BeforeAction == false)
                    {
                 
                        SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;
                        MPurchaseRequisition.Instance.LineTotal(frm);

                        MPurchaseRequisition.Instance.TotBefDisc(frm);
                        return true;
                    }
                    if (val.ItemUID == "txtDept" & val.Before_Action == false & val.EventType == SAPbouiCOM.BoEventTypes.et_LOST_FOCUS) // val.EventType == SAPbouiCOM.BoEventTypes.et_LOST_FOCUS & val.BeforeAction == false)
                    {
                       ;
                        SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;
                        MPurchaseRequisition.Instance.FillCombos(true);

                       return true;
                    }
                    if (val.ItemUID == "txtVendor" & val.Before_Action == false & val.EventType == SAPbouiCOM.BoEventTypes.et_LOST_FOCUS) // val.EventType == SAPbouiCOM.BoEventTypes.et_LOST_FOCUS & val.BeforeAction == false)
                    {
                        SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;
                        SAPbobsCOM.Recordset oRsDoc2 = ((SAPbobsCOM.Recordset)(Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                        SAPbouiCOM.DBDataSource OdbDs1 = frm.DataSources.DBDataSources.Item("@OPRQ");
                        string CardCode=OdbDs1.GetValue ("U_Vendor",0).ToString ();
                        string StrQry = "select CardName from ocrd where CardCode='" + CardCode + "'";
                        oRsDoc2.DoQuery(StrQry);
                       string cardNme= oRsDoc2.Fields.Item("CardName").Value.ToString();
                       OdbDs1.SetValue("U_VendrNme", 0, cardNme);
                        //SAPbouiCOM.EditText txtVendrName = (SAPbouiCOM.EditText)frm.Items.Item("txtVnme").Specific;
                       // frm.Items.Item("txtVnme").Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                        
                     
                    }
                    
                    if (val.ItemUID == "mtxPurReq" & val.ColUID == "colItmCode" & val.EventType == SAPbouiCOM.BoEventTypes.et_LOST_FOCUS & val.BeforeAction == false)
                    {
                        if (MPurchaseRequisition.Instance.FillUom_Line(val))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    if (val.ItemUID == "mtxPurReq" & val.ColUID == "cmbUom" & val.EventType == SAPbouiCOM.BoEventTypes.et_COMBO_SELECT & val.BeforeAction == false)
                    {
                        MPurchaseRequisition.Instance.UOM(val);
                    }

                    if (val.ColUID == "colItmCode" & val.EventType == SAPbouiCOM.BoEventTypes.et_LOST_FOCUS)
                    {
                        SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;
                      


                    }
                    if (val.ItemUID == "21" & val.EventType == SAPbouiCOM.BoEventTypes.et_LOST_FOCUS & val.BeforeAction == false)
                    {
                        SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;
                        MPurchaseRequisition.Instance.FocusColoums(frm);

                        return true;
                    }

                  


                    if (val.ItemUID == "1" & val.EventType == SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED & val.BeforeAction == true & (val.FormMode == (int)SAPbouiCOM.BoFormMode.fm_ADD_MODE | val.FormMode == (int)SAPbouiCOM.BoFormMode.fm_UPDATE_MODE))
                    {
                        SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;
                        int _flag=0;
                        if (MPurchaseRequisition.Instance.IsSavableHeader(frm))
                        {
                            //if (MPurchaseRequisition.Instance.IsSavable(frm))
                            //{
                                _flag = 1;
                                if (MPurchaseRequisition.Instance.IsSavable(frm))
                                {
                                    _flag =2 ;
                                if (MPurchaseRequisition.Instance.DeleteUnWantedRow(frm))
                                {
                                    return true;

                                }
                                else
                                {
                                    return false;
                                }
                            }
                            if (_flag == 1)
                            {
                                Global.SapApplication.StatusBar.SetText("Mandatory Fields are Empty ", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                                return false;
                            }

                            //}
                        }
                       if(_flag ==0)
                        {
                            Global.SapApplication.StatusBar.SetText("Mandatory Fields are Empty ", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                            return false;
                        }

                    }
                    if (val.ItemUID == "1" & val.EventType == SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED & val.BeforeAction == false & val.FormMode == (int)SAPbouiCOM.BoFormMode.fm_ADD_MODE)
                    {
                       SAPbouiCOM.Form frmItmEvent = Global.SapApplication.Forms.Item(val.FormUID);
                        if (bubblevalue == true)
                            MPurchaseRequisition.Instance.Initalsetting(frmItmEvent);
                    }


                    if (val.ItemUID == "22" & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK & val.FormMode == (int)SAPbouiCOM.BoFormMode.fm_OK_MODE & val.Before_Action == true)
                    {
                        try
                        {
                            int _flag =MPurchaseRequisition.Instance.CheckBeforeCopyToPO(val);
                            if (_flag == 0)
                            {
                                Global.SapApplication.ActivateMenuItem("2305");
                                SAPbouiCOM.Form frmNew = Global.SapApplication.Forms.ActiveForm;
                                frmNew.DataSources.UserDataSources.Add("CFormP", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 50);
                                frmNew.DataSources.UserDataSources.Item("CFormP").Value = val.FormUID;
                                 MPurchaseRequisition.Instance.CopyToPurchaseOrder(val);
                               
                            }
                            else if( _flag==1)
                            {
                                Global.SapApplication.StatusBar.SetText("Please Select an Item ", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                                return false;

                            }
                            else if (_flag == 3)
                            {
                                Global.SapApplication.StatusBar.SetText("No Open Qty.Please Select another Item ", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                                return false;

                            }
                            
                        }
                        catch (Exception ex)
                        {
                            string strmsg = ex.Message;
                        }
                    }



                }
            }
            catch (Exception ex)
            {
                string strmsg = ex.Message;
                return false;
            }
            return true;
        }


        #region Menu Event
        public bool SBO_Application_MenuEvent(SAPbouiCOM.MenuEvent pVal)
        {

            try
            {

                //if ((pVal.MenuUID == "1024"))
                //{
                //    if (Global.SapApplication.Forms.ActiveForm.TypeEx == "PurchaseRequisition")
                //    {
                //        SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;
                //        MPurchaseRequisition.Instance.Initalsetting(frm);
                //    }
                //}
                #region FindMode

                if ((pVal.MenuUID == "1281") & (pVal.BeforeAction == false))
                {
                    if (Global.SapApplication.Forms.ActiveForm.TypeEx == "")
                    {

                    }

                }
                #endregion

                #region AddMode

                if ((pVal.MenuUID == "1282") & (pVal.BeforeAction == false))
                {
                    if (Global.SapApplication.Forms.ActiveForm.TypeEx == "PurchaseRequisition")
                    {
                        MPurchaseRequisition.Instance.Initalsetting(Global.SapApplication.Forms.ActiveForm);
                    }
                }
                #endregion AddMode

                #region Navigation
                if ((pVal.MenuUID == "1290" | pVal.MenuUID == "1288" | pVal.MenuUID == "1289" | pVal.MenuUID == "1291") & pVal.BeforeAction == false)
                {
                    if (Global.SapApplication.Forms.ActiveForm.TypeEx == "PurchaseRequisition")
                    {
                       

                    }
                }
                #endregion

                #region Remove Option
                if (Global.SapApplication.Forms.ActiveForm.TypeEx == "CategoryMaster" & pVal.MenuUID == "1283" & pVal.BeforeAction == true)
                {
                }
                #endregion

                if (pVal.MenuUID == "1286")
                {
                    if (Global.SapApplication.Forms.ActiveForm.TypeEx == "PurchaseRequisition")
                    {
                        if (Global.SapApplication.MessageBox("Closing a document is irreversible. Document status will be changed to Closed. Do you want to continue?", 1, "Yes", "No", "") == 1)
                        {
                            SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;
                            MPurchaseRequisition.Instance.PurchaseReqStatusClose(frm);
                        }
                        else
                        {
                           return false;
                            
                        }
                        
                    }
                }
                ////if ((pVal.MenuUID == "1024"))
                ////{
                ////    if (Global.SapApplication.Forms.ActiveForm.TypeEx == "")
                ////    {
                ////        SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;
                ////        MPurchaseRequisition.Instance.Initalsetting(frm);
                ////    }
                ////}


            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "ok", "", "");
            }

            return true;
        }
        #endregion

        #region DataEvent

        /*********************************************************************************************
         * Form Data Event works whenever an Adding or Deleting or Updating happen on Business Objects.
         * *******************************************************************************************/
        public bool SBO_Application_FormDataEvent(SAPbouiCOM.BusinessObjectInfo BusinessObjectInfo)
        {
            SAPbouiCOM.Form frmDataEvent;
            try
            {
                frmDataEvent = Global.SapApplication.Forms.Item(BusinessObjectInfo.FormUID);
                if (BusinessObjectInfo.FormTypeEx == "PurchaseRequisition")
                {
                    if (BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_LOAD & BusinessObjectInfo.ActionSuccess == true)
                    {

                        SAPbouiCOM.DBDataSource dDsOWOR = Global.SapApplication.Forms.Item(BusinessObjectInfo.FormUID).DataSources.DBDataSources.Item("@OPRQ");
                        int IntDocentry = Convert.ToInt32(dDsOWOR.GetValue("DocEntry", 0));

                        // MPurchaseRequisition.Instance.LineTotal(frmDataEvent);
                        //MPurchaseRequisition.Instance.TotBefDisc(frmDataEvent);
                        //if (MPurchaseRequisition.Instance.IsApproved(frmDataEvent, IntDocentry))
                        //{
                        //    MPurchaseRequisition.Instance.DisableItems(frmDataEvent);
                        //    MPurchaseRequisition.Instance.DisableColoums(frmDataEvent);
                        if (MPurchaseRequisition.Instance.CheckBeforeLoad(IntDocentry))
                        {
                            MPurchaseRequisition.Instance.DisableColoums(frmDataEvent);
                        }
                        else
                        {
                            MPurchaseRequisition.Instance.EnableColoumsForOpenReq(frmDataEvent);
                        }
                        MPurchaseRequisition.Instance.EnableButton(frmDataEvent);

                        // MPurchaseRequisition.Instance.CheckAlreadyAproval(frmDataEvent, IntDocentry);
                        //}
                        //else
                        //{
                        //    MPurchaseRequisition.Instance.DisableItems(frmDataEvent);
                        //   // MPurchaseRequisition.Instance.EnableColoums(frmDataEvent);
                        //    MPurchaseRequisition.Instance.DisableColoums(frmDataEvent);
                        //   MPurchaseRequisition.Instance.DisableButton(frmDataEvent);
                        //   // MPurchaseRequisition.Instance.EnableeButton(frmDataEvent);

                        //}

                    }


                    if (BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_UPDATE & BusinessObjectInfo.ActionSuccess == false)
                    {
                        SAPbouiCOM.DBDataSource dDsOWOR = Global.SapApplication.Forms.Item(BusinessObjectInfo.FormUID).DataSources.DBDataSources.Item("@OPRQ");
                        int IntDocentry = Convert.ToInt32(dDsOWOR.GetValue("DocEntry", 0));
                    

                        return true;
                        //if (MPurchaseRequisition.Instance.CheckAlreadyAproval(frmDataEvent, IntDocentry))
                        //{
                        //    return true;

                        //}
                        //else
                        //{
                        //    return false;
                        //}

                        //if (MPurchaseRequisition.Instance.CheckProcessed(frmDataEvent, IntDocentry))
                        //{
                        //    return true;

                        //}
                        //else
                        //{
                        //    return false;
                        //}

                    }
                    if (BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD & BusinessObjectInfo.ActionSuccess == true)
                    {
                        SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;
                        SAPbouiCOM.DBDataSource dDsOWOR = Global.SapApplication.Forms.Item(BusinessObjectInfo.FormUID).DataSources.DBDataSources.Item("@PRQ1");
                        int IntDocentry = Convert.ToInt32(dDsOWOR.GetValue("DocEntry", 0));
                         MPurchaseRequisition.Instance.AddBalanceQty(IntDocentry);
                         //MPurchaseRequisition.Instance.Initalsetting(Global.SapApplication.Forms.ActiveForm);

                        return true;
                    }

                    if (BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_UPDATE & BusinessObjectInfo.ActionSuccess == true)
                    {
                        SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;
                        SAPbouiCOM.DBDataSource dDsOWOR = Global.SapApplication.Forms.Item(BusinessObjectInfo.FormUID).DataSources.DBDataSources.Item("@PRQ1");
                        int IntDocentry = Convert.ToInt32(dDsOWOR.GetValue("DocEntry", 0));
                        if (!MPurchaseRequisition.Instance.CheckBeforeLoad(IntDocentry))
                        {
                            MPurchaseRequisition.Instance.AddBalanceQty(IntDocentry);
                        }
                        //MPurchaseRequisition.Instance.Initalsetting(Global.SapApplication.Forms.ActiveForm);

                        return true;
                    }



                }
            }
            catch (Exception ex)
            {
                return false;

            }
            return true;

        }

        #endregion


        #region RightClick Event
        // private void SapApplication_RightClickEvent(ref SAPbouiCOM.ContextMenuInfo eventInfo, out bool BubbleEvent)
        // public  bool SBO_Application_ s(ref SAPbouiCOM.ContextMenuInfo eventInfo, out bool BubbleEvent)
        public bool SapApplication_RightClickEvent(SAPbouiCOM.ContextMenuInfo eventInfo)
        {
            //BubbleEvent = true;
            try
            {

               
              eventInfo.RemoveFromContent("1283");
              eventInfo.RemoveFromContent("1284");

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

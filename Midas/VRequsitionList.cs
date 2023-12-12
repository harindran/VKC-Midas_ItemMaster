using System;
using System.Collections.Generic;
using System.Text;

namespace VKC
{
    class VRequsitionList
    {
          General gen = new General();
        public SAPbouiCOM.Form oForm = null;

        #region Singleton

        private static VRequsitionList instance;

        public static VRequsitionList Instance
        {
            get
            {
                if (instance == null) instance = new VRequsitionList();

                return instance;
            }
        }

        #endregion


        public VRequsitionList()
        {
        }
        ~VRequsitionList()
        {
        }


        public bool SBO_Application_ItemEvent(SAPbouiCOM.ItemEvent val)
        {
            try
            {
                if (val.FormTypeEx == "frmRequsition")
                {



                    //if (val.ItemUID == "mtxAproval" & val.ColUID == "V_4" & val.EventType == SAPbouiCOM.BoEventTypes.et_MATRIX_LINK_PRESSED & val.BeforeAction == false)
                        if (val.ItemUID == "mtxAproval" & val.ColUID == "V_4" & val.EventType == SAPbouiCOM.BoEventTypes.et_MATRIX_LINK_PRESSED & val.BeforeAction == false)
                        {
                            SAPbouiCOM.Form frm = Global.SapApplication.Forms.Item(val.FormUID);
                            //if (MRequsitionList.Instance.ChkBeforeAdd(frm))
                            //{
                               // return true;
                          
                           
                            SAPbouiCOM.Matrix oMat1 = (SAPbouiCOM.Matrix)frm.Items.Item("mtxAproval").Specific;
                            //frm.DataSources.UserDataSources.Add("DocValNew", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 30);
                            oMat1.GetLineData(val.Row);
                            //string strDoc = frm.DataSources.UserDataSources.Item("DocEntry").Value;
                            int IntDocentry = Convert.ToInt32(frm.DataSources.UserDataSources.Item("DocEntry").Value);

                            Global.SapApplication.ActivateMenuItem("Requisition");
                         
                            MRequsitionList.Instance.LinkToPurReq(val, IntDocentry);
                            
                           
                        //}
                        //else
                        //{
                        //    return false;
                        //}
                        }
                        if (val.ItemUID == "btnCopyTo" & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK & val.FormMode == (int)SAPbouiCOM.BoFormMode.fm_OK_MODE & val.Before_Action == true)
                        {
                            try
                            {   
                                bool flag=MRequsitionList.Instance.CheckBeforeCopyToPO(val);
                            if (flag == true)
                            {
                                Global.SapApplication.ActivateMenuItem("2305");
                                SAPbouiCOM.Form frmNew = Global.SapApplication.Forms.ActiveForm;
                                frmNew.DataSources.UserDataSources.Add("CFormP", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 50);
                                frmNew.DataSources.UserDataSources.Item("CFormP").Value = val.FormUID;
                                MRequsitionList.Instance.CopyTo(val);
                            }
                            else if (flag == false)
                            {
                                Global.SapApplication.StatusBar.SetText("No Open Qty to select", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                                

                            }

                            }
                            catch
                            {
                                return false;
                            }
                        }
                 


                    if (val.ItemUID == "1" & val.EventType == SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED & val.BeforeAction == true & (val.FormMode == (int)SAPbouiCOM.BoFormMode.fm_ADD_MODE | val.FormMode == (int)SAPbouiCOM.BoFormMode.fm_UPDATE_MODE))
                    {
                        SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;

                        //if (MRequsitionList.Instance.ChkBeforeAdd(frm))
                        //{
                        //    return true;
                        //}
                        //else
                        //{
                        //    return false;
                        //}

                    }

                }


                if (val.ItemUID == "22" & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK & val.FormMode == (int)SAPbouiCOM.BoFormMode.fm_OK_MODE & val.Before_Action == true)
                {
                    try
                    {
                        Global.SapApplication.ActivateMenuItem("2305");
                       // MRequsitionList.Instance.LinkToPurReq(val);
                        // pr.CopyToPurchaseOrder(val);
                    }
                    catch (Exception ex)
                    {
                        string strmsg = ex.Message;
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
                    if (Global.SapApplication.Forms.ActiveForm.TypeEx == "")
                    {

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
                if (BusinessObjectInfo.FormTypeEx == "frmRequsition")
                {
                    if (BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_LOAD & BusinessObjectInfo.ActionSuccess == true)
                    {
                        SAPbouiCOM.DBDataSource dDsOWOR = Global.SapApplication.Forms.Item(BusinessObjectInfo.FormUID).DataSources.DBDataSources.Item("@OPRQ");
                        int IntDocentry = Convert.ToInt32(dDsOWOR.GetValue("DocEntry", 0));
                        //MRequsitionList.Instance.LinkToPurReq(frmDataEvent, IntDocentry);
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
                eventInfo.RemoveFromContent("1286");

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

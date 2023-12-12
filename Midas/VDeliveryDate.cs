using System;
using System.Collections.Generic;
using System.Text;

namespace VKC
{
    class VDeliveryDate
    {

      General gen = new  General();
        public SAPbouiCOM.Form oForm = null;
        public SAPbouiCOM.Form frm = null;
        #region Singleton

        private static VDeliveryDate instance;

        public static VDeliveryDate Instance
        {
            get
            {
                if (instance == null) instance = new VDeliveryDate();

                return instance;
            }
        }

        #endregion



         public VDeliveryDate()
        {
         }
        ~VDeliveryDate()
        {
        }


        public bool SBO_Application_ItemEvent(SAPbouiCOM.ItemEvent val)
        {
            try
            {
                string a = val.FormTypeEx;
                int b = val.Row;
                if (val.EventType == SAPbouiCOM.BoEventTypes.et_FORM_LOAD & val.BeforeAction == false)
                {
                 frm = Global.SapApplication.Forms.Item(val.FormUID);
                 

                }
               
                        try
                        {
                            SAPbouiCOM.Form newForm = Global.SapApplication.Forms.Item(val.FormUID); //Global.SapApplication.Forms.ActiveForm;
                           
                            if (newForm.TypeEx == "frmDeliveryDt" & val.BeforeAction == false )
                            {

                                
                                SAPbouiCOM.DBDataSource DbMat = newForm.DataSources.DBDataSources.Item("@OPOR_DDATE");
                                SAPbouiCOM.Matrix oMatrix = (SAPbouiCOM.Matrix)newForm.Items.Item("mtxDlvryDt").Specific;
                             //  val.Row == oMatrix.RowCount &
                                if ( val.EventType == SAPbouiCOM.BoEventTypes.et_DOUBLE_CLICK & val.ColUID == "colQty" & val.Before_Action == false & val.ItemUID != "2")
                                {
                                  
                                        if (MDeliveryDate.Instance.CheckValidQty(newForm) == true)
                                        {
                                            MDeliveryDate.Instance.MatrixAdd(oMatrix, DbMat);
                                        }
                                     
                                }
                              
                               
                            }
                              if (val.EventType == SAPbouiCOM.BoEventTypes.et_VALIDATE & val.ColUID == "colQty")
                                {
                                    MDeliveryDate.Instance.UpdateBalQty(newForm);
                                }
                                //if (val.EventType == SAPbouiCOM.BoEventTypes.et_FORM_LOAD)
                                //{
                                //    MDeliveryDate.Instance.UpdateBalQty(newForm);
                                //}
                            
                        }
                        catch
                        {
                            oForm.Freeze(false);
                        }


                        if (val.ItemUID == "btnAdd" & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK & val.BeforeAction == false )
                        {
                            try
                            {


                            
                                SAPbouiCOM.Form newForm = Global.SapApplication.Forms.Item(val.FormUID);
                                MDeliveryDate.Instance.DeleteUnWantedRow(newForm);
                                SAPbouiCOM.Matrix oMatrix = (SAPbouiCOM.Matrix)newForm.Items.Item("mtxDlvryDt").Specific;
                              
                                  if (MDeliveryDate.Instance.CheckValidQty(newForm) == true)
                                    {
                                        if (MDeliveryDate.Instance.FillDatatable(val) == true)
                                        {
                                          // Global.SapApplication.StatusBar.SetText("Saved Successfully !!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                                           newForm.Close();

                                        }
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

                    //SAPbouiCOM.EditText Docno = (SAPbouiCOM.EditText)frmDataEvent.Items.Item("8").Specific;
                     MDeliveryDate.Instance.docentry = Convert.ToInt32(oDataSRC.GetValue("DocEntry", 0));
                }
                if (BusinessObjectInfo.FormTypeEx == "142" & BusinessObjectInfo.ActionSuccess == false & BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_LOAD)
                {

                    //SAPbouiCOM.DBDataSource oDataSRC = Global.SapApplication.Forms.Item(BusinessObjectInfo.FormUID).DataSources.DBDataSources.Item("OPOR");

                    ////SAPbouiCOM.EditText Docno = (SAPbouiCOM.EditText)frmDataEvent.Items.Item("8").Specific;
                    // MDeliveryDate.Instance.docentry = Convert.ToInt32(oDataSRC.GetValue("DocEntry", 0));
                    // MDeliveryDate.Instance.Initalsetting(frmDataEvent);
                }
                if (BusinessObjectInfo.FormTypeEx == "142" & BusinessObjectInfo.ActionSuccess == true & BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD)
                {
                    SAPbouiCOM.DBDataSource oData_Doc = Global.SapApplication.Forms.Item(BusinessObjectInfo.FormUID).DataSources.DBDataSources.Item("OPOR");
                    int IntDocentry = Convert.ToInt32(oData_Doc.GetValue("DocEntry", 0));
                  
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
                //if ( MDeliveryDate.Instance.Disable_IssueComponents(frm))
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

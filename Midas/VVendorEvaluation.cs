using System;
using System.Collections.Generic;
using System.Text;

namespace VKC
{
    class VVendorEvaluation
    {

        General gen = new General();

        #region Singleton

        private static VVendorEvaluation instance;
        int flag = 0;

        public static VVendorEvaluation Instance
        {
            get
            {
                if (instance == null) instance = new VVendorEvaluation();

                return instance;
            }
        }

        #endregion


        //public VVendorEvaluation()
        //{
        //    Global.SapApplication.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(SapApplication_ItemEvent);
        //    Global.SapApplication.FormDataEvent += new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(SBO_Application_FormDataEvent);
        //    Global.SapApplication.RightClickEvent += new SAPbouiCOM._IApplicationEvents_RightClickEventEventHandler(SapApplication_RightClickEvent);
        //    Global.SapApplication.MenuEvent += new SAPbouiCOM._IApplicationEvents_MenuEventEventHandler(SapApplication_MenuEvent);
        //}

        //~VVendorEvaluation()
        //{
        //    Global.SapApplication.ItemEvent -= new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(SapApplication_ItemEvent);
        //    Global.SapApplication.FormDataEvent -= new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(SBO_Application_FormDataEvent);
        //    Global.SapApplication.RightClickEvent -= new SAPbouiCOM._IApplicationEvents_RightClickEventEventHandler(SapApplication_RightClickEvent);
        //    Global.SapApplication.MenuEvent -= new SAPbouiCOM._IApplicationEvents_MenuEventEventHandler(SapApplication_MenuEvent);
        //}

        



        #region Item Event
        public void SapApplication_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent val, out bool BubbleEvent)
        {
             bool bubbleVal = true;
            try
            {
                BubbleEvent = true;
                SAPbouiCOM.Form frmItem = Global.SapApplication.Forms.Item(val.FormUID);
                if (val.FormTypeEx == "150" )
                {
                    if (val.EventType == SAPbouiCOM.BoEventTypes.et_FORM_LOAD & val.BeforeAction ==true & val.FormType == 150& val.ActionSuccess == false)
                    {
                        //if (flag == 0)
                        //{
                            MVendorEvaluation.Instance.ClickOnItm(val);
                        //    flag = 1;
                        //}
                    }



                    if (val.ItemUID == "vendor" & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK & val.Before_Action == true)
                    {
                        Global.SapApplication.Forms.Item(val.FormUID).PaneLevel = 45;
                        MVendorEvaluation.Instance.ChangePaneVendor(val);
                        MVendorEvaluation.Instance.FillVendorMatrix();
                        MVendorEvaluation.Instance.DeleteUnWantedRow(frmItem);

                    }
            
                }
                if (val.FormTypeEx == "frmVendorEval")
                {
                    SAPbouiCOM.Form oForm = Global.SapApplication.Forms.Item(val.FormUID);



                    #region  Choose From List event
                    if (val.EventType == SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST)
                    {
                        try
                        {                            
                            MVendorEvaluation.Instance.Choofromlist_Req(oForm, val);
                        }
                        catch
                        {
                            oForm.Freeze(false);
                        }
                    }
                    #endregion
                    #region Item Pressed
                    if (val.EventType == SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED & val.BeforeAction == false)
                    {
                        SAPbouiCOM.Form frmVendor = Global.SapApplication.Forms.ActiveForm;
                        if (val.ItemUID == "btnFind")
                        {
                            MVendorEvaluation.Instance.SelectVendorData(val);
                        }                   

                    }
                     #endregion                
                }
            }
            catch (Exception ex)
            { }

            BubbleEvent =bubbleVal;
            
        }
        #endregion
       
        #region RightClick Event
        private void SapApplication_RightClickEvent(ref SAPbouiCOM.ContextMenuInfo eventInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {

            }
            catch (Exception ex)
            { }
        }
        #endregion

        #region DataEvent
        /*********************************************************************************************
         * Form Data Event works whenever an Adding or Deleting or Updating happen on Business Objects.
         * *******************************************************************************************/
       public void SBO_Application_FormDataEvent(ref SAPbouiCOM.BusinessObjectInfo BusinessObjectInfo, out bool BubbleEvent)
        {
            SAPbouiCOM.Form frmDataEvent;
            BubbleEvent = true;
            try
            {
                frmDataEvent = Global.SapApplication.Forms.Item(BusinessObjectInfo.FormUID);

                if (BusinessObjectInfo.ActionSuccess == true )
                    if(BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD || BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_UPDATE)
                {
                    MVendorEvaluation.Instance.UpdateVendorEval(); 
                }

            }
            catch
            {
            }

        }
        #endregion

        #region Menu Event
        private void SapApplication_MenuEvent(ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            try
            {
                #region FindMode

                if ((pVal.MenuUID == "1281") & (pVal.BeforeAction == false))
                {
                    if (Global.SapApplication.Forms.ActiveForm.TypeEx == "TestParameter")
                    {

                    }

                }
                #endregion

               
                #region Navigation

                #endregion

            }
            catch (Exception ex)
            { }

        }
        #endregion

      
    }
}

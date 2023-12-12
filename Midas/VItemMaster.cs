using System;
using System.Collections.Generic;
using System.Text;

namespace VKC
{
    class VItemMaster
    {

        General gen = new General();

        #region Singleton

        private static VItemMaster instance;

        public static VItemMaster Instance
        {
            get
            {
                if (instance == null) instance = new VItemMaster();

                return instance;
            }
        }

        #endregion


        public VItemMaster()
        {
            Global.SapApplication.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(SapApplication_ItemEvent);
            Global.SapApplication.FormDataEvent += new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(SBO_Application_FormDataEvent);
            Global.SapApplication.RightClickEvent += new SAPbouiCOM._IApplicationEvents_RightClickEventEventHandler(SapApplication_RightClickEvent);
            Global.SapApplication.MenuEvent += new SAPbouiCOM._IApplicationEvents_MenuEventEventHandler(SapApplication_MenuEvent);
        }

        ~VItemMaster()
        {
            Global.SapApplication.ItemEvent -= new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(SapApplication_ItemEvent);
            Global.SapApplication.FormDataEvent -= new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(SBO_Application_FormDataEvent);
            Global.SapApplication.RightClickEvent -= new SAPbouiCOM._IApplicationEvents_RightClickEventEventHandler(SapApplication_RightClickEvent);
            Global.SapApplication.MenuEvent -= new SAPbouiCOM._IApplicationEvents_MenuEventEventHandler(SapApplication_MenuEvent);
        }

        



        #region Item Event
        private void SapApplication_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent val, out bool BubbleEvent)
        {
            try
            {
                if (val.FormTypeEx == "frmItemMasterData")
                {
                    if (val.ItemUID == "chkGST" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED )
                    {
                        MItemMaster.Instance.GST_CheckBOx();
                    }
                    if (val.ItemUID == "cmbSizeId" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                    {
                        MItemMaster.Instance.FillSizeCombo();
                    }
                    //if (val.ItemUID == "cmbBrand" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                    //{
                    //    MItemMaster.Instance.FillModelCombo();
                    //}
                    if (val.ItemUID == "cmbUnit" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                    {
                        MItemMaster.Instance.DefineUnit();
                    }
                    if (val.ItemUID == "cmbBrand" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                    {
                        MItemMaster.Instance.DefineBrand();
                    }
                    if (val.ItemUID == "cmbModel" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                    {
                        MItemMaster.Instance.DefineModel();
                    }
                    if (val.ItemUID == "cmbColor" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                    {
                        MItemMaster.Instance.DefineColor();
                    }
                    if (val.ItemUID == "cmbSizeId" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                    {
                        MItemMaster.Instance.DefineSizeID();
                    }
                    ////if (val.ItemUID == "cmbSize" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                    ////{
                    ////    MItemMaster.Instance.DefineSize();
                    ////}

                    if (val.ItemUID == "cmbLoc" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                    {
                        MItemMaster.Instance.DefineDeliveryLoc();
                    }

                }
               
             }
            catch (Exception ex)
            {
                Global.SapApplication.StatusBar.SetText("Successfully Saved" + ex.Message , SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
            }


            BubbleEvent = Global.bubblevalue;
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
        void SBO_Application_FormDataEvent(ref SAPbouiCOM.BusinessObjectInfo BusinessObjectInfo, out bool BubbleEvent)
        {
            SAPbouiCOM.Form frmDataEvent;
            BubbleEvent = true;
            try
            {

            }
            catch (Exception ex)
            { }

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

                #region AddMode

                if ((pVal.MenuUID == "1282") & (pVal.BeforeAction == true))
                {
                    if (Global.SapApplication.Forms.ActiveForm.TypeEx == "TAX")
                    {
                        //MTaxMaster.Instance.InitTax(Global.SapApplication.Forms.ActiveForm);
                    }
                }
                #endregion AddMode

                #region Navigation

                if (pVal.MenuUID == "FGMaster" & pVal.BeforeAction == false)
                {

                    //MItemMaster.Instance.GetCombos();
                    
                }
                #endregion

            }
            catch (Exception ex)
            { }

        }
        #endregion

    }
}

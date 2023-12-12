using System;
using System.Collections.Generic;
using System.Text;

namespace VKC
{
    class VSemiFinished
    {
           General gen = new General();

        #region Singleton

        private static VSemiFinished instance;

        public static VSemiFinished Instance
        {
            get
            {
                if (instance == null) instance = new VSemiFinished();

                return instance;
            }
        }

        #endregion


        public VSemiFinished()
        {
            Global.SapApplication.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(SapApplication_ItemEvent);
            Global.SapApplication.FormDataEvent += new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(SBO_Application_FormDataEvent);
            Global.SapApplication.RightClickEvent += new SAPbouiCOM._IApplicationEvents_RightClickEventEventHandler(SapApplication_RightClickEvent);
            Global.SapApplication.MenuEvent += new SAPbouiCOM._IApplicationEvents_MenuEventEventHandler(SapApplication_MenuEvent);
        }

        ~VSemiFinished()
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
                    //if (val.ItemUID == "cmbSBrand" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                    //{
                    //  MSemiFinished.Instance.FillModelCombo();
                    //}
                    if (val.ItemUID == "cmbSmSide" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                    {
                       MSemiFinished.Instance.DefineSide();
                    }
                    if (val.ItemUID == "cmbSemGrp" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                    {
                       MSemiFinished.Instance.DefineSemiGroup();
                    }
                    if (val.ItemUID == "cmbSemClr" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                    {
                       MSemiFinished.Instance.DefineSemiColor();
                    }
                    if (val.ItemUID == "cmbSmSzeId" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                    {
                       MSemiFinished.Instance.DefineSizeId();
                    }
                    if (val.ItemUID == "cmbSemSize" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                    {
                        MSemiFinished.Instance.DefineSize();
                    }
                    if (val.ItemUID == "cmbSModel" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                    {
                        MSemiFinished.Instance.DefineModel();
                    }
                    if (val.ItemUID == "cmbSBrand" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                    {
                        MSemiFinished.Instance.DefineBrand();
                    }
                    ////if (val.ItemUID == "btnOk" & val.BeforeAction == true)
                    ////{
                    ////    MSemiFinished.Instance.GenerateCode();
                    ////}
                    //if (val.ItemUID == "btnAdd" & val.BeforeAction == true)
                    //{
                    //    MSemiFinished.Instance.AddItem();
                    //}

                }
            }
            catch { }
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
                #region Navigation

                if (pVal.MenuUID == "SemiFinished" & pVal.BeforeAction == false)
                {

                    MSemiFinished.Instance.GetCombos();

                }
                #endregion
            }
            catch { }
        }
        #endregion
    }
}

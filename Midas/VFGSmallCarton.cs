using System;
using System.Collections.Generic;
using System.Text;


namespace VKC
{
    class VFGSmallCarton
    {
        General gen = new General();

        #region Singleton

        private static VFGSmallCarton instance;

        public static VFGSmallCarton Instance
        {
            get
            {
                if (instance == null) instance = new VFGSmallCarton();

                return instance;
            }
        }

        #endregion


        public VFGSmallCarton()
        {
            Global.SapApplication.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(SapApplication_ItemEvent);
            Global.SapApplication.FormDataEvent += new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(SBO_Application_FormDataEvent);
            Global.SapApplication.RightClickEvent += new SAPbouiCOM._IApplicationEvents_RightClickEventEventHandler(SapApplication_RightClickEvent);
            Global.SapApplication.MenuEvent += new SAPbouiCOM._IApplicationEvents_MenuEventEventHandler(SapApplication_MenuEvent);
        }

        ~VFGSmallCarton()
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
                    //if (val.ItemUID == "cmbSmBrand" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                    //{
                    //    MFGSmallCarton.Instance.FillModelCombo();
                    //}
                    if (val.ItemUID == "cmbSmlUnit" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_COMBO_SELECT)
                    {
                       MFGSmallCarton.Instance.DefineUnit();
                    }
                    if (val.ItemUID == "cmbSmBrand" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_COMBO_SELECT)
                    {
                        MFGSmallCarton.Instance.DefineBrand();
                    }
                    if (val.ItemUID == "cmbSmModel" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_COMBO_SELECT)
                    {
                        MFGSmallCarton.Instance.DefineModel();
                    }
                    if (val.ItemUID == "cmbSmlColr" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_COMBO_SELECT)
                    {
                        MFGSmallCarton.Instance.DefineColor();
                    }
                    if (val.ItemUID == "cmbSmSizId" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_COMBO_SELECT)
                    {
                        MFGSmallCarton.Instance.DefineSizeID();
                    }
                    if (val.ItemUID == "cmbSmlSize" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_COMBO_SELECT)
                    {
                        MFGSmallCarton.Instance.DefineSize();
                    }

                    if (val.ItemUID == "cmbSmlLoc" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_COMBO_SELECT)
                    {
                        MFGSmallCarton.Instance.DefineDeliveryLoc();
                    }
                    //if (val.EventType == SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED & val.BeforeAction == true)
                    //{
                    //    if (val.ItemUID == "btnSmalOk")
                    //    {
                    //        MFGSmallCarton.Instance.GenerateCodeForSmall();
                    //    }

                    //}
                    //if (val.ItemUID == "btnSmalAdd" & val.BeforeAction == true)
                    //{
                    //    MFGSmallCarton.Instance.AddItem();
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

                if (pVal.MenuUID == "FGSmall" & pVal.BeforeAction == false)
                {

                    MFGSmallCarton.Instance.GetCombos();

                }
                #endregion
            }
            catch { }
        }
        #endregion

    }
}

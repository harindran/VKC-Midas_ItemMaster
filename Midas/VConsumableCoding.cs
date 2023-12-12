using System;
using System.Collections.Generic;
using System.Text;

namespace VKC
{
    class VConsumableCoding
    {
           General gen = new General();

        #region Singleton

        private static VConsumableCoding instance;

        public static VConsumableCoding Instance
        {
            get
            {
                if (instance == null) instance = new VConsumableCoding();

                return instance;
            }
        }

        #endregion


        public VConsumableCoding()
        {
            Global.SapApplication.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(SapApplication_ItemEvent);
            Global.SapApplication.FormDataEvent += new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(SBO_Application_FormDataEvent);
            Global.SapApplication.RightClickEvent += new SAPbouiCOM._IApplicationEvents_RightClickEventEventHandler(SapApplication_RightClickEvent);
            Global.SapApplication.MenuEvent += new SAPbouiCOM._IApplicationEvents_MenuEventEventHandler(SapApplication_MenuEvent);
        }

        ~VConsumableCoding()
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
                    if (val.ItemUID == "cmbConGrp" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_COMBO_SELECT)
                    {
                        MConsumablesCoding.Instance.GetSubGroup();
                    }
                    if (val.ItemUID == "cmbConGrp" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                    {
                      MConsumablesCoding.Instance.DefineGroup();
                    }
                    if (val.ItemUID == "cmbSubCon" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                    {
                      MConsumablesCoding.Instance.DefineSubGroup();
                    }
                }
                //if (val.FormTypeEx == "frmConsumables")
                //{


                //    if (val.ItemUID == "btnOk" & val.BeforeAction == true)
                //    {
                //        MConsumablesCoding.Instance.GenerateCode();
                //    }
                //    if (val.ItemUID == "btnAdd" & val.BeforeAction == true)
                //    {
                //        MConsumablesCoding.Instance.AddItem();
                //    }

                //}
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

                if (pVal.MenuUID == "Consumables" & pVal.BeforeAction == false)
                {

                    MConsumablesCoding.Instance.GetCombos();

                }
                #endregion
            }
            catch { }
        }
        #endregion
    }
}

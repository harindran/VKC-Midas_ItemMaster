using System;
using System.Collections.Generic;
using System.Text;

namespace VKC
{
    class VFixedAssests
    {
         General gen = new General();

        #region Singleton

        private static VFixedAssests instance;

        public static VFixedAssests Instance
        {
            get
            {
                if (instance == null) instance = new VFixedAssests();

                return instance;
            }
        }

        #endregion


        public VFixedAssests()
        {
            Global.SapApplication.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(SapApplication_ItemEvent);
            Global.SapApplication.FormDataEvent += new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(SBO_Application_FormDataEvent);
            Global.SapApplication.RightClickEvent += new SAPbouiCOM._IApplicationEvents_RightClickEventEventHandler(SapApplication_RightClickEvent);
            Global.SapApplication.MenuEvent += new SAPbouiCOM._IApplicationEvents_MenuEventEventHandler(SapApplication_MenuEvent);
        }

        ~VFixedAssests()
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
                    if (val.ItemUID == "cmbAstGrp" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                    {
                       MFixedAssets.Instance.FillSubGroupCombo();
                       MFixedAssets.Instance.DefineGroup();
                    }
                    if (val.ItemUID == "cmbCat1" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                    {
                        MFixedAssets.Instance.DefineCategory1();
                    }
                    if (val.ItemUID == "cmbCat2" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                    {
                        MFixedAssets.Instance.DefineCategory2();
                    }
                    if (val.ItemUID == "cmbCat3" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                    {
                        MFixedAssets.Instance.DefineCategory3();
                    }
                    if (val.ItemUID == "cmbCat4" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                    {
                        MFixedAssets.Instance.DefineCategory4();
                    }


                    //if (val.ItemUID == "cmbGroup" & val.BeforeAction == false)
                    //{
                    //    MFixedAssets.Instance.FillSubGroupCombo();
                    //}
                    //if (val.ItemUID == "btnOk" & val.BeforeAction == true)
                    //{
                    //    MFixedAssets.Instance.GenerateCode();
                    //}
                    //if (val.ItemUID == "btnAdd" & val.BeforeAction == true)
                    //{
                    //    MFixedAssets.Instance.AddItem();
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

                if (pVal.MenuUID == "FixedAssets" & pVal.BeforeAction == false)
                {

                    MFixedAssets.Instance.GetCombos();

                }
                #endregion
            }
            catch { }
        }
        #endregion


    }
}

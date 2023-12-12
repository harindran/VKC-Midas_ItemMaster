using System;
using System.Collections.Generic;
using System.Text;

namespace VKC
{
    class VRawMaterials
    {
        General gen = new General();

        #region Singleton

        private static VRawMaterials instance;

        public static VRawMaterials Instance
        {
            get
            {
                if (instance == null) instance = new VRawMaterials();

                return instance;
            }
        }

        #endregion


        public VRawMaterials()
        {
            Global.SapApplication.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(SapApplication_ItemEvent);
            Global.SapApplication.FormDataEvent += new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(SBO_Application_FormDataEvent);
            Global.SapApplication.RightClickEvent += new SAPbouiCOM._IApplicationEvents_RightClickEventEventHandler(SapApplication_RightClickEvent);
            Global.SapApplication.MenuEvent += new SAPbouiCOM._IApplicationEvents_MenuEventEventHandler(SapApplication_MenuEvent);
        }

        ~VRawMaterials()
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
                    if (val.ItemUID == "cmbRawGrp" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_COMBO_SELECT)
                    {
                        MRawMaterial.Instance.FillSubGroupCombo();
                    }
                    if (val.ItemUID == "cmbRawGrp" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                    {
                       
                       MRawMaterial.Instance.DefineRawGrp();
                    }
                    if (val.ItemUID == "cmbRSubGrp" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                    {
                       MRawMaterial.Instance.DefineRawSubGrp();
                    }

                    if (val.ItemUID == "cbxMix" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK  & val.ActionSuccess == true)
                    {
                        MRawMaterial.Instance.ChemicalMixChange();
                    }
                }
                //if (val.FormTypeEx == "frmRawmateials")
                //{
                //    if (val.ItemUID == "cmbGroup" & val.BeforeAction == false)
                //    {
                //        MRawMaterial.Instance.FillSubGroupCombo();
                //    }
                    //if (val.EventType == SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED & val.BeforeAction == true)
                    //{
                    //    if (val.ItemUID == "btnOk")
                    //    {
                    //        MRawMaterial.Instance.GenerateCode();
                    //    }

                    //}
                    //if (val.ItemUID == "btnAdd" & val.BeforeAction == true)
                    //{
                    //    MRawMaterial.Instance.AddItem();
                    //}
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

                if (pVal.MenuUID == "RawMatr" & pVal.BeforeAction == false)
                {

                    MRawMaterial.Instance.GetCombos();

                }
                #endregion
            }
            catch { }
        }
        #endregion


    }
}

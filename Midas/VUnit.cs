using System;
using System.Collections.Generic;
using System.Text;


namespace VKC
{
    class VUnit
    {
        General gen = new General();

       
        #region Singleton

        private static VUnit instance;
        public static VUnit Instance
        {
            get
            {
                if (instance == null) instance = new VUnit();

                return instance;
            }
        }

        #endregion


        //public VUnit()
        //{
        //    Global.SapApplication.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(SapApplication_ItemEvent);
        //    Global.SapApplication.FormDataEvent += new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(SBO_Application_FormDataEvent);
        //    Global.SapApplication.RightClickEvent += new SAPbouiCOM._IApplicationEvents_RightClickEventEventHandler(SapApplication_RightClickEvent);
        //    Global.SapApplication.MenuEvent += new SAPbouiCOM._IApplicationEvents_MenuEventEventHandler(SapApplication_MenuEvent);
        //}

        //~VUnit()
        //{
        //    Global.SapApplication.ItemEvent -= new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(SapApplication_ItemEvent);
        //    Global.SapApplication.FormDataEvent -= new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(SBO_Application_FormDataEvent);
        //    Global.SapApplication.RightClickEvent -= new SAPbouiCOM._IApplicationEvents_RightClickEventEventHandler(SapApplication_RightClickEvent);
        //    Global.SapApplication.MenuEvent -= new SAPbouiCOM._IApplicationEvents_MenuEventEventHandler(SapApplication_MenuEvent);
        //}


        #region Item Event
        public bool SapApplication_ItemEvent(SAPbouiCOM.ItemEvent val)
        {
            try
            {
                if (val.ItemUID == "2" & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                {
                    SAPbouiCOM.Form newForm = Global.SapApplication.Forms.Item(val.FormUID);
                    newForm.Close();

                }
               
                if (val.ItemUID == "btnUnitAdd" & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK  )
                {
                    try
                    {



                        SAPbouiCOM.Form newForm = Global.SapApplication.Forms.Item(val.FormUID);
                     
                        SAPbouiCOM.Matrix oMatrix = (SAPbouiCOM.Matrix)newForm.Items.Item("mtxUnit").Specific;
                        SAPbouiCOM.Button oButton = (SAPbouiCOM.Button)newForm.Items.Item("btnUnitAdd").Specific;  
                          MUnit.Instance.FillDatatable(val);
                          MUnit.Instance.UnitButtonClick(val);
                        
                      //  newForm.Close(); //modified on 08-08-12
                         
                    }
                    catch { }
                }
                if (val.FormTypeEx == "frmUnit" & val.EventType == SAPbouiCOM.BoEventTypes.et_FORM_CLOSE)
                {

                }
                if (val.ItemUID == "mtxUnit" & val.ColUID == "colCheck" & val.BeforeAction == false & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                {
                    SAPbouiCOM.Form newForm = Global.SapApplication.Forms.Item(val.FormUID);
                  // SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(newForm.DataSources.UserDataSources.Item("PFormID").Value);
                    SAPbouiCOM.Matrix oMatrix = (SAPbouiCOM.Matrix)newForm.Items.Item("mtxUnit").Specific;
                    SAPbouiCOM.CheckBox chbx = (SAPbouiCOM.CheckBox)oMatrix.Columns.Item("colCheck").Cells.Item(val.Row).Specific;
                    SAPbouiCOM.EditText txtcode = (SAPbouiCOM.EditText)oMatrix.Columns.Item("colCode").Cells.Item(val.Row).Specific;
                    SAPbouiCOM.EditText txtName = (SAPbouiCOM.EditText)oMatrix.Columns.Item("colName").Cells.Item(val.Row).Specific;
                    string Button = newForm.DataSources.UserDataSources.Item("ButtonID").Value.ToString();
                    if (chbx.Checked == true )
                    {
                        if (Button == "btnAdd")
                        {
                         
                            MUnit.Instance.GenerateMasterItemCode(txtcode.Value, txtName.Value);
                        }
                        else if (Button == "btnSmalAdd")
                        {
                          
                            MUnit.Instance.GenerateCodeForSmall(txtcode.Value, txtName.Value);
                        }
                        else if (Button == "btnPkAdd")
                        {
                            MUnit.Instance.GenerateCode("btnPkAdd", txtcode.Value);
                        }
                        else if (Button == "btnRawAdd")
                        {
                            MUnit.Instance.GenerateCode("btnRawAdd", txtcode.Value);
                        }

                        else if (Button == "btnSrpAdd")
                        {
                            MUnit.Instance.GenerateCode("btnSrpAdd", txtcode.Value);
                        }

                        else if (Button == "btnSemAdd")
                        {
                            MUnit.Instance.GenerateCode("btnSemAdd", txtcode.Value);
                        }

                        else if (Button == "btnAstAdd")
                        {
                            
                          //  MUnit.Instance.GenerateCode("btnAstAdd", txtcode.Value);// by Reena on 25/05/2013
                            MUnit.Instance.GenerateCodeForAsset(txtcode.Value, txtName.Value);
                        }
                        else if (Button == "btnConAdd")
                        {
                            MUnit.Instance.GenerateCode("btnConAdd", txtcode.Value);
                        }
                   
                   
                    }
                    
                    else
                    {
                        MUnit.Instance.RemoveItemCode(txtcode.Value);
                    }
                }
                if (val.FormTypeEx == "frmUnit" & val.EventType == SAPbouiCOM.BoEventTypes.et_FORM_LOAD & val.BeforeAction == true)
                {

                   // MUnit.Instance.InitialSettings(val);
                    if (val.ItemUID == "cmbSmlUnit" & val.BeforeAction == false)
                    {
                       MFGSmallCarton.Instance.DefineUnit();
                    }
                    if (val.ItemUID == "cmbSmBrand" & val.BeforeAction == false)
                    {
                        MFGSmallCarton.Instance.DefineBrand();
                    }
                    if (val.ItemUID == "cmbSmModel" & val.BeforeAction == false)
                    {
                        MFGSmallCarton.Instance.DefineModel();
                    }
                    if (val.ItemUID == "cmbSmlColr" & val.BeforeAction == false)
                    {
                        MFGSmallCarton.Instance.DefineColor();
                    }
                    if (val.ItemUID == "cmbSmSizId" & val.BeforeAction == false)
                    {
                        MFGSmallCarton.Instance.DefineSizeID();
                    }
                    if (val.ItemUID == "cmbSmlSize" & val.BeforeAction == false)
                    {
                        MFGSmallCarton.Instance.DefineSize();
                    }

                    if (val.ItemUID == "cmbSmlLoc" & val.BeforeAction == false)
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
                return true;
            }
            catch { }
          //  BubbleEvent = Global.bubblevalue;
            return true;
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

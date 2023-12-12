using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;


namespace VKC
{
    class InitialSettings
    {
        General gen = new General();
        

        #region Singleton

        private static InitialSettings instance;

        public static InitialSettings Instance
        {
            get
            {
                if (instance == null) instance = new InitialSettings();

                return instance;
            }
        }

        #endregion

        #region Set Application Menu Items 

        public void SetMenuItems()
        {          
            //CreateMenuItem(SAPbouiCOM.BoMenuType.mt_POPUP, "Tax", "Tax", 14, "43520");
            //CreateMenuItem(SAPbouiCOM.BoMenuType.mt_STRING, "TaxDetails", "Tax Details", 1, "Tax");
           //-------------------------modified on20-03-2012---------------------------------------------
            CreateMenuItem(SAPbouiCOM.BoMenuType.mt_POPUP, "MenuVKC", "Item", 15, "43520", "UI.bmp");
          
            CreateMenuItem(SAPbouiCOM.BoMenuType.mt_STRING, "ItemMasterData", "Item Master Data", 1, "MenuVKC");
            CreateMenuItem(SAPbouiCOM.BoMenuType.mt_STRING, "Requisition", "Purchase Requisition", 0, "2304");
            CreateMenuItem(SAPbouiCOM.BoMenuType.mt_STRING, "Reqstnlst", "RequsitionList", 1, "2304");
            CreateMenuItem(SAPbouiCOM.BoMenuType.mt_STRING, "VendorEval", "Vendor Evaluation", 2, "2304");

        }

        #endregion

        #region Function to Create a menu item
        private void CreateMenuItem(SAPbouiCOM.BoMenuType mType, string uniqueID, string desc, int position, string menuItemId)
        {
            SAPbouiCOM.Menus Menu = null;
            SAPbouiCOM.MenuItem MenuItem = null;
            Menu = Global.SapApplication.Menus;
            string rootPath = Application.StartupPath;
            rootPath = rootPath.Remove(rootPath.Length - 9, 9); 

            SAPbouiCOM.MenuCreationParams CreationPara = null;
            CreationPara = (SAPbouiCOM.MenuCreationParams)(Global.SapApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams));
            MenuItem = Global.SapApplication.Menus.Item(menuItemId);

            try
            {
                Menu = MenuItem.SubMenus;
                CreationPara.Type = mType;
                CreationPara.UniqueID = uniqueID;
                CreationPara.String = desc;
                CreationPara.Position = position;
                CreationPara.Enabled = true;
                Menu.AddEx(CreationPara);
            }
            catch (Exception ex)
            {
                //Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
            }
        }
        
        private void CreateMenuItem(SAPbouiCOM.BoMenuType mType, string uniqueID, string desc, int position, string menuItemId,string strImg)
        {
            SAPbouiCOM.Menus Menus = null;
            SAPbouiCOM.MenuItem MenuItem = null;
            Menus = Global.SapApplication.Menus;
            string rootPath = Application.StartupPath;
            rootPath = rootPath.Remove(rootPath.Length - 9, 9); 


            SAPbouiCOM.MenuCreationParams CreationPara = null;
            CreationPara = (SAPbouiCOM.MenuCreationParams)(Global.SapApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams));
            MenuItem = Global.SapApplication.Menus.Item(menuItemId);

            try
            {
                Menus = MenuItem.SubMenus;
                CreationPara.Type = mType;
                CreationPara.UniqueID = uniqueID;
                CreationPara.String = desc;
                CreationPara.Position = position;
                CreationPara.Enabled = true;
                if (strImg != "")
                {
                    CreationPara.Image = Application.StartupPath + "\\" + strImg;
                }
                Menus.AddEx(CreationPara);
            }
            catch (Exception ex)
            {
                //Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
            }
        }
        #endregion

        #region InitialSettings
        public InitialSettings()
        {
            try
            {
                ApplicationSetUp();
                if (Global.SapApplication.Language == SAPbouiCOM.BoLanguages.ln_English | Global.SapApplication.Language == SAPbouiCOM.BoLanguages.ln_English_Cy | Global.SapApplication.Language == SAPbouiCOM.BoLanguages.ln_English_Gb | Global.SapApplication.Language == SAPbouiCOM.BoLanguages.ln_English_Sg)
                {
                    SetMenuItems();
                    //Change By Dhayalan Implementing HardwareKey Concept
                    Boolean oFlag = false;
                    oFlag = IsValid();
                    if (oFlag == true)
                    {
                        ConnectCompany();
                    }
                    else if(oFlag ==false )
                    {
                        Global.SapApplication.MessageBox("Installing Add-On failed due to License mismatch", 1, "Ok", "", "");
                    }
                        //ConnectCompany();
                }

                Global.SapApplication.MenuEvent += new SAPbouiCOM._IApplicationEvents_MenuEventEventHandler(SapApplication_MenuEvent);
                Global.SapApplication.AppEvent += new SAPbouiCOM._IApplicationEvents_AppEventEventHandler(SapApplication_AppEvent);
                Global.SapApplication.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(SapApplication_ItemEvent);
                Global.SapApplication.RightClickEvent += new SAPbouiCOM._IApplicationEvents_RightClickEventEventHandler(SBO_Application_RightClickEvent);
                Global.SapApplication.FormDataEvent += new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(SBO_Application_FormDataEvent);

                CreateInstance();
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
            }
        }
        #endregion

        #region Intialize Application Instance
        private void ApplicationSetUp()
        {
            SAPbouiCOM.SboGuiApi GuiApi = new SAPbouiCOM.SboGuiApi();
            try
            {
                string ConnString = null;
                ConnString = Environment.GetCommandLineArgs().GetValue(1).ToString();
                GuiApi.Connect(ConnString);
                Global.SapApplication = GuiApi.GetApplication(-1);
                
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("The Sap Buisness One Application could not be found");
                System.Environment.Exit(0);
            }

        }
        #endregion

        #region Connect to the company
        private void ConnectCompany()
        {
            try
            {
                string sErrorMsg;
                string cookie;
                string connStr;
                Global.SapCompany = new SAPbobsCOM.Company();
                cookie = Global.SapCompany.GetContextCookie();
                connStr = Global.SapApplication.Company.GetConnectionContext(cookie);
                Global.SapCompany.SetSboLoginContext(connStr);
                //Global.SapCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012;
                Global.SapCompany.Connect();
                //Global.SapCompany = (SAPbobsCOM.Company)Global.SapApplication.Company.GetDICompany();
                sErrorMsg = Global.SapCompany.GetLastErrorDescription();

                Global.SapApplication.StatusBar.SetText("Connected", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
            }
            catch
            {
                Global.SapApplication.MessageBox(Global.SapCompany.GetLastErrorDescription().ToString(), 1, "Ok", "", "");
            }
        }
        #endregion

        #region Menu Event
        private void SapApplication_MenuEvent(ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            try
            {               
                #region Item


                if ((pVal.MenuUID == "ItemMasterData") & (pVal.BeforeAction == true))
                {
                    try
                    {
                        VItemMasterData oSemi = VItemMasterData.Instance;
                        gen.LoadXMLForm("ItemMasterData.xml");
                        MItemMasterData item = MItemMasterData.Instance;
                        item.Init();
                       // item.FillCombo();

                    }
                    catch { }
                }
                //if ((pVal.MenuUID == "ItemMasterData") & (pVal.BeforeAction == false))
                //{
                //    try
                //    {
                //        gen.LoadXMLForm("ItemMasterData.xml");
                //        MItemMasterData item = MItemMasterData.Instance;
                //        item.Init();
                //        // item.FillCombo();

                //    }
                //    catch { }
                //}
                if ((pVal.MenuUID == "Requisition") & (pVal.BeforeAction == true))
                {
                    try
                    {
                        gen.LoadXMLForm("Purchase Requisition.xml");
                        MPurchaseRequisition.Instance.Initalsetting(Global.SapApplication.Forms.ActiveForm);


                    }
                    catch (Exception ex)
                    {
                        //Global.SBO_Application.MessageBox(ex.Message, 1, "Ok", "", "");
                    }

                }
                if ((pVal.MenuUID == "Reqstnlst") & (pVal.BeforeAction == true))
                {
                    try
                    {

                        gen.LoadXMLForm("ReqstnNew.xml");
                        MRequsitionList.Instance.Initalsetting(Global.SapApplication.Forms.ActiveForm);
                       

                    }
                    catch (Exception ex)
                    {
                    }

                }
                if ((pVal.MenuUID == "VendorEval") & (pVal.BeforeAction == true))
                {
                    try
                    {

                        gen.LoadXMLForm("VendorEvaluation.xml");
                       // MVendorEvaluation.Instance.Initalsetting(Global.SapApplication.Forms.ActiveForm);


                    }
                    catch (Exception ex)
                    {
                    }

                }
                if (pVal.MenuUID == "1282" & pVal.BeforeAction == false)
                {
                    SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;
                    if (frm.TypeEx == "PurchaseRequisition")
                    {
                        MPurchaseRequisition.Instance.Initalsetting(frm);

                    }

                }
                #endregion 

                if (pVal.MenuUID == "1286" & pVal.BeforeAction == true)
                {
                    SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;
                    if (frm.TypeEx == "PurchaseRequisition")
                    {
                      BubbleEvent=  VPurchaseRequisition.Instance.SBO_Application_MenuEvent(pVal);

                    }

                }
                
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
            }

        }
        #endregion       

        private void SapApplication_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent val, out bool BubbleEvent)
        {

           Global.bubblevalue = true;
           BubbleEvent = true;

           if (val.FormTypeEx == "PurchaseRequisition")
           {
               BubbleEvent = VPurchaseRequisition.Instance.SBO_Application_ItemEvent(val);
               Global.bubblevalue = BubbleEvent;
           }
             
               if (val.FormTypeEx == "frmRequsition")
                  Global.bubblevalue =  VRequsitionList.Instance.SBO_Application_ItemEvent(val);
               if (val.FormTypeEx == "142")

                   Global.bubblevalue =  VPurchaseOrder.Instance.SBO_Application_ItemEvent(val);
               if (val.FormTypeEx == "frmDeliveryDt" & val.BeforeAction == false)

                    Global.bubblevalue =  VDeliveryDate.Instance.SBO_Application_ItemEvent(val);
               if (val.FormTypeEx == "frmUnit" & val.BeforeAction == false)
                   Global.bubblevalue = VUnit.Instance.SapApplication_ItemEvent(val);
               if (val.FormTypeEx == "frmVendorEval" & val.BeforeAction == false)
               {
                  VVendorEvaluation.Instance.SapApplication_ItemEvent(FormUID, ref val, out BubbleEvent);
                   BubbleEvent = Global.bubblevalue;
                 
               }
               if (val.FormTypeEx == "150")
               {
                   VVendorEvaluation.Instance.SapApplication_ItemEvent(FormUID, ref val, out BubbleEvent);
               }
            //if (val.FormTypeEx == "frmItemMasterData" & val.BeforeAction == false)
            //{
            //    bool abc = true;
            //    VItemMasterData.Instance.SapApplication_ItemEvent(val.FormUID, ref val, out abc);
            //}

           
            
            
           

        }


        #region AppEvent
        private void SapApplication_AppEvent(SAPbouiCOM.BoAppEventTypes EventType)
        {

            switch (EventType)
            {
                case SAPbouiCOM.BoAppEventTypes.aet_ShutDown:
                    //Global.SapApplication.MessageBox("ShutDown Intiziated", 1, "Ok", "", "");
                    System.Environment.Exit(0);
                    break;
            }
        }
        #endregion
        private void SBO_Application_RightClickEvent(ref SAPbouiCOM.ContextMenuInfo eventInfo, out bool BubbleEvent)
        {
            SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;
            if (frm.TypeEx == "PurchaseRequisition")

                VPurchaseRequisition.Instance.SapApplication_RightClickEvent(eventInfo);
            ////if (frm.TypeEx == "TargetMaster")

            ////    VTargetMaster.Instance.SBO_Application_RightClickEvent(eventInfo);
            BubbleEvent = true;
        }

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
                frmDataEvent = Global.SapApplication.Forms.Item(BusinessObjectInfo.FormUID);
                if (frmDataEvent.TypeEx == "PurchaseRequisition")
                    BubbleEvent = VPurchaseRequisition.Instance.SBO_Application_FormDataEvent(BusinessObjectInfo);
                if (frmDataEvent.TypeEx == "142")
                    BubbleEvent = VPurchaseOrder.Instance.SBO_Application_FormDataEvent(BusinessObjectInfo);
                if (frmDataEvent.TypeEx == "150")
                {
                    VVendorEvaluation.Instance.SBO_Application_FormDataEvent(ref BusinessObjectInfo, out BubbleEvent);
                }
                ////if (frmDataEvent.TypeEx == "143")
                ////    BubbleEvent = VGoodsReceipts.Instance.SBO_Application_FormDataEvent(BusinessObjectInfo);
                //    BubbleEvent = VTargetMaster.Instance.SBO_Application_FormDataEvent(BusinessObjectInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        #endregion

        #region Creating Instances
        public void CreateInstance()
        {
            try
            {

              //  VItemMaster oItemmaster = VItemMaster.Instance;
                //VProductionReceipt ProductionRcpt = VProductionReceipt.Instance;
            }
            catch
            {
                

            }
        }
        #endregion
        //Change By Dhayalan For Hardware Key Validation
#region
        public Boolean IsValid()
        {
            try
            {
                Global.SapApplication.ActivateMenuItem("257");
                SAPbouiCOM.Form oform = Global.SapApplication.Forms.ActiveForm;
                SAPbouiCOM.EditText oHWKEY = (SAPbouiCOM.EditText)oform.Items.Item("79").Specific;

                General.HardwareKey();
                String CRRHWKEY = oHWKEY.Value.ToString();
                Global.SapApplication.Forms.ActiveForm.Close();          
                for (int i = 0; i <= Global.HWKEY.Length - 1; i++)
                {
                    if (CRRHWKEY == Global.HWKEY[i])
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");   
                return false;
            }
        }

#endregion


    }
}


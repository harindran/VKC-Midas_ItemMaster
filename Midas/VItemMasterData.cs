using System;
using System.Collections.Generic;
using System.Text;

namespace VKC
{
    class VItemMasterData
    {

        General gen = new General();

        #region Singleton

        private static VItemMasterData instance;
        int flag1 = 0, flag2 = 0, flag3 = 0, flag4 = 0, flag5 = 0, flag6 = 0, flag7 = 0, flag8 = 0;
        public static VItemMasterData Instance
        {
            get
            {
                if (instance == null) instance = new VItemMasterData();

                return instance;
            }
        }

        #endregion


        public VItemMasterData()
        {
            Global.SapApplication.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(SapApplication_ItemEvent);
            Global.SapApplication.FormDataEvent += new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(SBO_Application_FormDataEvent);
            Global.SapApplication.RightClickEvent += new SAPbouiCOM._IApplicationEvents_RightClickEventEventHandler(SapApplication_RightClickEvent);
          Global.SapApplication.MenuEvent += new SAPbouiCOM._IApplicationEvents_MenuEventEventHandler(SapApplication_MenuEvent);
        }

        ~VItemMasterData()
        {
            Global.SapApplication.ItemEvent -= new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(SapApplication_ItemEvent);
            Global.SapApplication.FormDataEvent -= new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(SBO_Application_FormDataEvent);
            Global.SapApplication.RightClickEvent -= new SAPbouiCOM._IApplicationEvents_RightClickEventEventHandler(SapApplication_RightClickEvent);
           Global.SapApplication.MenuEvent -= new SAPbouiCOM._IApplicationEvents_MenuEventEventHandler(SapApplication_MenuEvent);
        }

        
        #region Item Event
        public void SapApplication_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent val, out bool BubbleEvent)
        {
            try
            {
                #region Form Load
                if (val.EventType == SAPbouiCOM.BoEventTypes.et_FORM_LOAD & val.BeforeAction == true)
                    {
                        if (val.FormTypeEx == "frmItemMasterData" )
                        {
                            flag1 = 0; flag2 = 0; flag3 = 0; flag4 = 0; flag5 = 0; flag6 = 0; flag7 = 0; flag8 = 0;
                        }
                      SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;
                      MItemMasterData.Instance.AddDatatable(val);
                     frm.DataSources.UserDataSources.Item("FolderID").ValueEx = "fldrMaster";
                    }
                  
               #endregion
                #region OK Button Click
                  if (val.FormTypeEx == "frmItemMasterData" & val.BeforeAction == false )
                {
                       if (val.ItemUID == "btnOk")
                        {
                            MItemMaster.Instance.GenerateCode();
                        }
                        else if (val.ItemUID == "btnSmalOk")
                        {
                            MFGSmallCarton.Instance.GenerateCodeForSmall();
                        }
                        else if (val.ItemUID == "btnPkOk")
                        {
                            if (MPackingMaterials.Instance.Validation() == true)
                            {
                                MPackingMaterials.Instance.GenerateCode();
                            }
                        }
                        else if (val.ItemUID == "btnRawOk")
                        {
                            if (MRawMaterial.Instance.Validation() == true)
                            {
                                MRawMaterial.Instance.GenerateCode();
                            }
                        }
                        else if (val.ItemUID == "btnSrpOk")
                        {
                            if (MScrapCoding.Instance.Validation() == true)
                            {
                                MScrapCoding.Instance.GenerateCode();
                            }
                        }
                        else if (val.ItemUID == "btnSemiOk")
                        {
                            if (MSemiFinished.Instance.Validation() == true)
                            {
                                MSemiFinished.Instance.GenerateCode();
                            }
                        }
                        else if (val.ItemUID == "btnAstOk")
                        {
                            if (MFixedAssets.Instance.Validation() == true)
                            {
                                MFixedAssets.Instance.GenerateCode();
                            }
                        }
                        else if (val.ItemUID == "btnConsuOk")
                        {
                            if (MConsumablesCoding.Instance.Validation() == true)
                            {
                                MConsumablesCoding.Instance.GenerateCode();
                            }
                        }
                 
                        #region Cancel Button Click
                        //----------For cancel----------//
                        if (val.ItemUID == "cl1" || val.ItemUID == "cl2" || val.ItemUID == "cl3" || val.ItemUID == "cl4" || val.ItemUID == "cl5" || val.ItemUID == "cl6" || val.ItemUID == "cl7" || val.ItemUID == "cl8")
                        {
                           MItemMasterData.Instance.Close(val);
                       }
                        #endregion

                   }
                  #endregion
                #region Unit Click
                  if (val.FormTypeEx == "frmUnit" && val.ItemUID == "btnUnitAdd" && val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK && val.BeforeAction == false)
                   {
                       SAPbouiCOM.Form frmParent = Global.SapApplication.Forms.Item(val.FormUID);
                       string Button = frmParent.DataSources.UserDataSources.Item("ButtonID").Value.ToString();
                       SAPbouiCOM.Button button = (SAPbouiCOM.Button)frmParent.Items.Item("btnUnitAdd").Specific;


                       if (Global.bubbleUnit == true && Button == "btnAdd")
                       {
                           if (button.Caption == "Add")
                           {
                               SAPbouiCOM.Form CForm = Global.SapApplication.Forms.ActiveForm;
                               SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(CForm.DataSources.UserDataSources.Item("PFormID").Value);

                               // SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(pFormUID);
                               string strItem = "", strModel = "", strColor = "", strSizeId = "", strSize = "", strPairs = "", strBrand = "";
                               SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbItem").Specific;
                               SAPbouiCOM.ComboBox oComboBrand = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbBrand").Specific;
                               SAPbouiCOM.ComboBox oComboModel = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbModel").Specific;
                               SAPbouiCOM.ComboBox oComboColor = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbColor").Specific;
                               SAPbouiCOM.ComboBox oComboSizeId = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSizeId").Specific;
                               // SAPbouiCOM.ComboBox oComboSize = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSize").Specific;
                               SAPbouiCOM.EditText oTxtPairs = (SAPbouiCOM.EditText)PForm.Items.Item("txtPairs").Specific;
                               // SAPbouiCOM.Button button = (SAPbouiCOM.Button)CForm.Items.Item("btnUnitAdd").Specific;

                               strItem = oComboItem.Selected.Value;
                               strBrand = oComboBrand.Selected.Value;
                               strModel = oComboModel.Selected.Value;
                               strColor = oComboColor.Selected.Value;
                               strSizeId = oComboSizeId.Selected.Value;
                              // strSize = oComboSize.Selected.Value;
                               strPairs = oTxtPairs.Value;
                               if (MItemMasterData.Instance.AddItem(strItem, strModel, strColor, strSizeId, strSize, strPairs, strBrand) == true)
                               {
                                   MItemMaster.Instance.ClearCombo(val);
                                   CForm.Items.Item("btnUnitAdd").Enabled = false;
                                   button.Caption = "OK";
                                   MItemMasterData.Instance.ClearUDF(PForm);

                                   Global.SapApplication.StatusBar.SetText("Successfully Saved", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);

                               }
                           }
                       }

                       else if (Global.bubbleUnit == true && Button == "btnSmalAdd")
                       {
                           if (button.Caption == "Add")
                           {
                               SAPbouiCOM.Form CForm = Global.SapApplication.Forms.ActiveForm;
                               SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(CForm.DataSources.UserDataSources.Item("PFormID").Value);

                               // SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(pFormUID);
                               string strItem = "", strModel = "", strColor = "", strSizeId = "", strSize = "", strPairs = "", strBrand = "";
                               SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmGoods").Specific;
                               SAPbouiCOM.ComboBox oComboBrand = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmBrand").Specific;
                               SAPbouiCOM.ComboBox oComboModel = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmModel").Specific;
                               SAPbouiCOM.ComboBox oComboColor = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmlColr").Specific;
                               SAPbouiCOM.ComboBox oComboSizeId = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmSizId").Specific;
                               SAPbouiCOM.ComboBox oComboSize = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmlSize").Specific;
                               SAPbouiCOM.EditText oTxtPairs = (SAPbouiCOM.EditText)PForm.Items.Item("txtPairs").Specific;
                               strItem = oComboItem.Selected.Value;
                               strBrand = oComboBrand.Selected.Value;
                               strModel = oComboModel.Selected.Value;
                               strColor = oComboColor.Selected.Value;
                               strSizeId = oComboSizeId.Selected.Value;
                               strSize = oComboSize.Selected.Value;
                               strPairs = oTxtPairs.Value;
                               if (MItemMasterData.Instance.AddItem(strItem, strModel, strColor, strSizeId, strSize, strPairs, strBrand) == true)
                               {
                                   MFGSmallCarton.Instance.ClearCombo(val);
                                   CForm.Items.Item("btnUnitAdd").Enabled = false;
                                   button.Caption = "OK";
                                   MItemMasterData.Instance.ClearUDF(PForm);
                                   Global.SapApplication.StatusBar.SetText("Successfully Saved", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);

                               }
                           }
                       }

                       else if (Global.bubbleUnit == true && Button == "btnPkAdd")
                       {
                           if (button.Caption == "Add")
                           {
                               if (MItemMasterData.Instance.AddItem("txtPkName", "txtPkCode", "cmbGroup","","txtPDescri" ,"btnPkAdd", "cmbPItgp",false) == true)
                               {
                                   SAPbouiCOM.Form CForm = Global.SapApplication.Forms.ActiveForm;
                                   SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(CForm.DataSources.UserDataSources.Item("PFormID").Value);
                                   MPackingMaterials.Instance.ClearCombo(val);
                                   CForm.Items.Item("btnUnitAdd").Enabled = false;
                                   MItemMasterData.Instance.ClearUDF(PForm);
                                   button.Caption = "OK";
                               }
                           }
                       }

                       else if (Global.bubbleUnit == true && Button == "btnRawAdd")
                       {
                           if (button.Caption == "Add")
                           {
                               if (MItemMasterData.Instance.AddItem("txtRawName", "txtRawCode","cmbRawGrp", "cmbRSubGrp","txtDescrip" ,"btnRawAdd", "cmbRItgp",false) == true)
                               {
                                   SAPbouiCOM.Form CForm = Global.SapApplication.Forms.ActiveForm;
                                   SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(CForm.DataSources.UserDataSources.Item("PFormID").Value);
                                   MRawMaterial.Instance.ClearCombo(val);
                                   CForm.Items.Item("btnUnitAdd").Enabled = false;
                                   MItemMasterData.Instance.ClearUDF(PForm);
                                   button.Caption = "OK";
                               }
                           }
                       }

                       else if (Global.bubbleUnit == true && Button == "btnSrpAdd")
                       {
                           if (button.Caption == "Add")
                           {
                               if (MItemMasterData.Instance.AddItem("txtSrpName", "txtSrpCode", "cmbSrpGrp", "", "txtDescri2", "btnSrpAdd", "cmbSrpItgp",false) == true)
                               {
                                   SAPbouiCOM.Form CForm = Global.SapApplication.Forms.ActiveForm;
                                   SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(CForm.DataSources.UserDataSources.Item("PFormID").Value);
                                   MScrapCoding.Instance.ClearCombo(val);
                                   CForm.Items.Item("btnUnitAdd").Enabled = false;
                                   MItemMasterData.Instance.ClearUDF(PForm);
                                   button.Caption = "OK";
                               }
                           }
                       }

                       else if (Global.bubbleUnit == true && Button == "btnSemAdd")
                       {
                           if (button.Caption == "Add")
                           {
                               SAPbouiCOM.Form CForm = Global.SapApplication.Forms.ActiveForm;
                               SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(CForm.DataSources.UserDataSources.Item("PFormID").Value);

                               // SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(pFormUID);
                               string strItem = "", strModel = "", strColor = "", strSizeId = "", strSize = "", strPairs = "", strBrand = "";
                               SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmClass").Specific;
                               //SAPbouiCOM.ComboBox oComboSide = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmSide").Specific;
                               SAPbouiCOM.ComboBox oComboGroup = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSemGrp").Specific;
                               SAPbouiCOM.ComboBox oComboColor = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSemClr").Specific;
                               SAPbouiCOM.ComboBox oComboSizeId = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSmSzeId").Specific;
                               SAPbouiCOM.ComboBox oComboSize = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSemSize").Specific;
                               SAPbouiCOM.ComboBox oComboModel = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSModel").Specific;
                               SAPbouiCOM.ComboBox oComboBrand = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbSBrand").Specific;


                               strItem = oComboItem.Selected.Value;
                               strBrand = oComboBrand.Selected.Value;
                               strModel = oComboModel.Selected.Value;
                               strColor = oComboColor.Selected.Value;
                               strSizeId = oComboSizeId.Selected.Value;
                               strSize = oComboSize.Selected.Value;
                               if (MItemMasterData.Instance.AddItem("txtSemName", "txtSemCode", "cmbSemGrp", "btnSemAdd", "cmbSmItgp", strItem, strBrand, strModel, strColor, strSizeId, strSize) == true)
                               {
                                   MSemiFinished.Instance.ClearCombo(val);
                                   CForm.Items.Item("btnUnitAdd").Enabled = false;
                                   MItemMasterData.Instance.ClearUDF(PForm);
                                   button.Caption = "OK";
                               }
                           }
                       }

                       else if (Global.bubbleUnit == true && Button == "btnAstAdd")
                       {

                           if (button.Caption == "Add")
                           {
                               
                                   SAPbouiCOM.Form CForm = Global.SapApplication.Forms.ActiveForm;
                                   SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(CForm.DataSources.UserDataSources.Item("PFormID").Value);

                                  
                                   string strGroup = "", strCat1 = "", strCat2 = "", strCat3 = "", strCat4 = "";
                                   SAPbouiCOM.ComboBox oComboClass = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbClassi").Specific;
                                   SAPbouiCOM.ComboBox oCombogroup = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbAstGrp").Specific;
                                   SAPbouiCOM.ComboBox oComboCat1 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbCat1").Specific;
                                   SAPbouiCOM.ComboBox oComboCat2 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbCat2").Specific;
                                   SAPbouiCOM.ComboBox oComboCat3 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbCat3").Specific;
                                   SAPbouiCOM.ComboBox oComboCat4 = (SAPbouiCOM.ComboBox)PForm.Items.Item("cmbCat4").Specific;


                                   strGroup = oCombogroup.Selected.Value;
                                   strCat1 = oComboCat1.Selected.Value;
                                   strCat2 = oComboCat2.Selected.Value;
                                   strCat3 = oComboCat3.Selected.Value;
                                   strCat4 = oComboCat4.Selected.Value;

                                   if (MItemMasterData.Instance.AddItemAsset("txtAstName", "txtAstCode", "cmbAstGrp", "btnAstAdd", "cmbFItgp", strGroup,strCat1,strCat2,strCat3,strCat4) == true)
                                   {
                                     MFixedAssets.Instance.ClearCombo(val);
                                       CForm.Items.Item("btnUnitAdd").Enabled = false;
                                       MItemMasterData.Instance.ClearUDF(PForm);
                                       button.Caption = "OK";
                                   }
                              
                               ////if (MItemMasterData.Instance.AddItem("txtAstName", "txtAstCode", "cmbAstGrp", "btnAstAdd", "cmbFItgp") == true)
                               ////{
                               ////    SAPbouiCOM.Form CForm = Global.SapApplication.Forms.ActiveForm;
                               ////    MFixedAssets.Instance.ClearCombo(val);
                               ////    CForm.Items.Item("btnUnitAdd").Enabled = false;
                               ////    button.Caption = "OK";
                               ////}
                           }
                       }

                       else if (Global.bubbleUnit == true && Button == "btnConAdd")
                       {
                           if (button.Caption == "Add")
                           {
                               if (MItemMasterData.Instance.AddItem("txtConName", "txtConCode", "cmbConGrp", "cmbSubCon", "txtConDesc", "btnConAdd", "cmbConItgp",false) == true)
                               {
                                   SAPbouiCOM.Form CForm = Global.SapApplication.Forms.ActiveForm;
                                   SAPbouiCOM.Form PForm = Global.SapApplication.Forms.Item(CForm.DataSources.UserDataSources.Item("PFormID").Value);
                                   MConsumablesCoding.Instance.ClearCombo(val);
                                   CForm.Items.Item("btnUnitAdd").Enabled = false;
                                   MItemMasterData.Instance.ClearUDF(PForm);
                                   button.Caption = "OK";
                               }
                           }
                       }
                       else
                       {
                           Global.SapApplication.MessageBox("Please Select Unit", 1, "ok", "", "");
                       }
                   }
                   else
                   {
                       if (val.FormTypeEx == "frmUnit" & val.ItemUID == "btnUnitAdd")
                       {

                       }
                   }
                    #endregion
              
                #region AddData
                    if (val.BeforeAction == true)
                    {
                        if (val.ItemUID == "btnAdd" & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK & val.FormTypeEx == "frmItemMasterData")
                        {
                          
                            MItemMasterData.Instance.FillDtable(val);
                           
                        }
                        else if (val.ItemUID == "btnSmalAdd" & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                        {
                            MItemMasterData.Instance.FillDtable(val);
                          //  MItemMasterData.Instance.AddItem("txtSmlName", "txtSmalCod", "", "cmbSmlUnit");
                        }
                        else if (val.ItemUID == "btnPkAdd" & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                        {
                            MItemMasterData.Instance.FillDtable(val);
                            //MItemMasterData.Instance.AddItem("txtPkName", "txtPkCode", "cmbGroup","");
                        }
                        else if (val.ItemUID == "btnRawAdd" & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                        {
                            MItemMasterData.Instance.FillDtable(val);
                           // MItemMasterData.Instance.AddItem("txtRawName", "txtRawCode", "cmbRawGrp","");
                        }
                        else if (val.ItemUID == "btnSrpAdd" & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                        {
                            MItemMasterData.Instance.FillDtable(val);
                            //MItemMasterData.Instance.AddItem("txtSrpName", "txtSrpCode", "","");
                        }
                        else if (val.ItemUID == "btnSemAdd" & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                        {
                            MItemMasterData.Instance.FillDtable(val);  
                           // MItemMasterData.Instance.AddItem("txtSemName", "txtSemCode", "","");
                        }
                        else if (val.ItemUID == "btnAstAdd" & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                        {
                            MItemMasterData.Instance.FillDtable(val);
                            // MItemMasterData.Instance.AddItem("txtAstName", "txtAstCode", "","");
                        }
                        else if (val.ItemUID == "btnConAdd" & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
                        {
                            MItemMasterData.Instance.FillDtable(val);
                            //MItemMasterData.Instance.AddItem("txtConName", "txtConCode", "","");
                        }
                    }
                    #endregion
                #region AddNew

                    if (val.ItemUID == "4864" & val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK & val.BeforeAction == false)
                    {
                        SAPbouiCOM.Form frmItemMaster = Global.SapApplication.Forms.Item(val.FormUID);

                    }
#endregion
                #region Folder Click
                    if (val.EventType == SAPbouiCOM.BoEventTypes.et_CLICK && val.BeforeAction == false)
                    {
                        SAPbouiCOM.Form frmItemMaster = Global.SapApplication.Forms.Item(val.FormUID);
                        //frmItemMaster.Freeze(true);
                        if (val.ItemUID == "fldrMaster")
                        {
                            frmItemMaster.DataSources.UserDataSources.Item("FolderID").ValueEx = "fldrMaster";
                            frmItemMaster.PaneLevel = 1;
                            if (flag1 == 0)
                            {
                                MItemMaster.Instance.GetCombos();
                               // MItemMaster.Instance.FillSizeCombo();
                                flag1 = 1;
                            }
                            else
                            {
                                MItemMaster.Instance.Classification();
                            }
                           
                        }
                        if (val.ItemUID == "fldrSmall")
                        {
                            frmItemMaster.DataSources.UserDataSources.Item("FolderID").ValueEx = "fldrSmall";
                            frmItemMaster.PaneLevel = 2;
                            if (flag2 == 0)
                            {
                                MFGSmallCarton.Instance.GetCombos();
                                flag2 = 1;
                            }
                            else
                            {
                                MFGSmallCarton.Instance.Classification();
                            }

                        }
                        else
                            if (val.ItemUID == "fldrPack")
                            {
                                frmItemMaster.DataSources.UserDataSources.Item("FolderID").ValueEx = "fldrPack";
                                frmItemMaster.PaneLevel = 3;
                                if (flag3 == 0)
                                {
                                    MPackingMaterials.Instance.GetCombos();
                                    flag3 = 1;
                                }
                                else
                                {
                                    MPackingMaterials.Instance.Classification();
                                }
                            }
                            else if (val.ItemUID == "fldrRaw")
                            {
                                frmItemMaster.DataSources.UserDataSources.Item("FolderID").ValueEx = "fldrRaw";
                                frmItemMaster.PaneLevel = 4;
                                if (flag4 == 0)
                                {
                                    MRawMaterial.Instance.GetCombos();
                                  //  MRawMaterial.Instance.FillSubGroupCombo();
                                    flag4 = 1;
                                }
                                else
                                {
                                    MRawMaterial.Instance.Classification();
                                }
                            }
                            else if (val.ItemUID == "fldrScrap")
                            {
                                frmItemMaster.DataSources.UserDataSources.Item("FolderID").ValueEx = "fldrScrap";
                                frmItemMaster.PaneLevel = 5;
                                if (flag5 == 0)
                                {
                                    MScrapCoding.Instance.GetCombos();
                                    flag5 = 1;
                                }
                                else
                                {
                                    MScrapCoding.Instance.Classification();
                                }
                            }
                            else if (val.ItemUID == "fldrSemi")
                            {
                                frmItemMaster.DataSources.UserDataSources.Item("FolderID").ValueEx = "fldrSemi";
                                frmItemMaster.PaneLevel = 6;
                                if (flag6 == 0)
                                {
                                    MSemiFinished.Instance.GetCombos();
                                    flag6 = 1;
                                }
                                else
                                {
                                    MSemiFinished.Instance.Classification();
                                }
                            }
                            else if (val.ItemUID == "fldrAsset")
                            {
                                frmItemMaster.DataSources.UserDataSources.Item("FolderID").ValueEx = "fldrAsset";
                                frmItemMaster.PaneLevel = 7;
                                if (flag7 == 0)
                                {
                                    MFixedAssets.Instance.GetCombos();
                                    
                                    flag7 = 1;
                                }
                                else
                                {
                                    MFixedAssets.Instance.Classification();
                                }
                            }
                            else if (val.ItemUID == "fldrConsum")
                            {
                                frmItemMaster.DataSources.UserDataSources.Item("FolderID").ValueEx = "fldrConsum";
                                frmItemMaster.PaneLevel = 8;
                                if (flag8 == 0)
                                {
                                    MConsumablesCoding.Instance.GetCombos();
                                    flag8 = 1;
                                }
                                else
                                {
                                    MConsumablesCoding.Instance.Classification();
                                }
                            }
                            else if (val.ItemUID == "fldrudf")
                            {
                                frmItemMaster.DataSources.UserDataSources.Item("FolderID").ValueEx = "fldrudf";
                                frmItemMaster.PaneLevel = 9;
                            }
                            else if (val.ItemUID == "fldrprice")
                            {
                                frmItemMaster.DataSources.UserDataSources.Item("FolderID").ValueEx = "fldrprice";
                                frmItemMaster.PaneLevel = 10;
                            }
                       
                        //  frmItemMaster.Freeze(false);
                    }
                    #endregion
                    
#region Checkbox click
                    if (val.EventType == SAPbouiCOM.BoEventTypes.et_FORM_KEY_DOWN & val.BeforeAction == false)
                    {
                        if (val.FormTypeEx == "frmItemMasterData")
                        {
                            SAPbouiCOM.Form frm = Global.SapApplication.Forms.ActiveForm;
                            SAPbouiCOM.CheckBox ocheck = (SAPbouiCOM.CheckBox)frm.Items.Item("chkexcise").Specific;
                            if (ocheck.Checked == true)
                            {
                                frm.Items.Item("cmaterialt").Enabled = false;
                                frm.Items.Item("cchapterid").Enabled = false;
                                frm.Items.Item("tassessabl").Enabled = false;
                            }
                            if (ocheck.Checked == false)
                            {
                                frm.Items.Item("cmaterialt").Enabled = false;
                                frm.Items.Item("cchapterid").Enabled = false;
                                frm.Items.Item("tassessabl").Enabled = false;
                            }
                        }
                    }
#endregion





                }
               
             
            catch (Exception ex)
            { }

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
                 frmDataEvent = Global.SapApplication.Forms.Item(BusinessObjectInfo.FormUID);

                 if ( BusinessObjectInfo.ActionSuccess == true & BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD)
                 {
                     #region Define New ComboBind
                     try
                     {
                         string pFormUID = frmDataEvent.DataSources.UserDataSources.Item("usdParent").Value;
                        //--------FG Master Carton-------------------------------------//
                         if (frmDataEvent.Title == "BRAND" || frmDataEvent.Title == "MODEL" || frmDataEvent.Title == "COLOR" || frmDataEvent.Title == "SIZECAT" || frmDataEvent.Title == "SIZE" || frmDataEvent.Title == "DELIVERYLOC")
                         {
                             MItemMaster.Instance.RefreshCombos(pFormUID, frmDataEvent.Title);
                         }
                         //-------FG Small Carton---------------------------------------//
                         else if (frmDataEvent.Title == "BRAND" || frmDataEvent.Title == "MODEL" || frmDataEvent.Title == "COLOR" || frmDataEvent.Title == "SIZECAT" || frmDataEvent.Title == "SIZESMALL" || frmDataEvent.Title == "DELIVERYLOC")
                         {
                             MFGSmallCarton.Instance.RefreshCombos(pFormUID, frmDataEvent.Title);
                         }
                         //---------Consumables Coding---------------------------------------------------//
                         else if (frmDataEvent.Title == "CONGROUP" || frmDataEvent.Title == "CONSUBGROUP")
                         {
                             MConsumablesCoding.Instance.RefreshCombos(pFormUID, frmDataEvent.Title);

                         }
                         //------------------SemiFinished Coding----------------------------------------//
                         else if (frmDataEvent.Title == "SIDE" || frmDataEvent.Title == "SEMIGROUP" || frmDataEvent.Title == "COLOR" || frmDataEvent.Title == "SIZECAT" || frmDataEvent.Title == "SIZESMALL" || frmDataEvent.Title == "MODEL")
                         {
                           MSemiFinished.Instance.RefreshCombos(pFormUID, frmDataEvent.Title);
                          
                         }
                         //------------------Scrap Coding-----Mofified on 2-05-2012---------------------------------------------------//

                         else if (frmDataEvent.Title == "SCRAP")
                         {
                             MScrapCoding.Instance.RefreshCombos(pFormUID, frmDataEvent.Title);

                         }
                         //--------------------------------Packing Materials-------------------------------------//
                         else if (frmDataEvent.Title == "PACKGROUP")
                         {
                            MPackingMaterials.Instance.RefreshCombos(pFormUID, frmDataEvent.Title);

                         }
                         else if (frmDataEvent.Title == "PACK SUB GROUP")
                         {
                             MPackingMaterials.Instance.RefreshCombos(pFormUID, frmDataEvent.Title);

                         }
                        
                         //-----------------------------------Raw Materials--------------------------------------------//
                         else if (frmDataEvent.Title == "RAWGROUP" || frmDataEvent.Title == "RAWSUBGROUP")
                         {
                           MRawMaterial.Instance.RefreshCombos(pFormUID, frmDataEvent.Title);

                         }
                         //---------------------------Fixed Assets-----------------------------------------------------//

                         else if (frmDataEvent.Title == "GROUP" || frmDataEvent.Title == "CATEGORY1" || frmDataEvent.Title == "CATEGORY2" || frmDataEvent.Title == "CATEGORY3" || frmDataEvent.Title == "CATEGORY4")
                         {
                             MFixedAssets.Instance.RefreshCombos(pFormUID, frmDataEvent.Title);

                         }
                         else
                         {
                             return;
                         }
  
                         
                     }
                     catch { }
                     
                     #endregion
                 }


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
               

                #region Add Menu Click

                if (pVal.MenuUID == "1282" & pVal.BeforeAction == false)
                {
                    SAPbouiCOM.Form frmItemMaster = Global.SapApplication.Forms.ActiveForm;
                    SAPbouiCOM.Item folder = (SAPbouiCOM.Item)frmItemMaster.Items.Item("fldrMaster");
                    SAPbouiCOM.ComboBox oComboItem = (SAPbouiCOM.ComboBox)frmItemMaster.Items.Item("cmbItem").Specific;

                    folder.Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                          
                    //string folderID= frmItemMaster.DataSources.UserDataSources.Item("FolderID").Value.ToString();

                    //if (folderID == "fldrMaster")
                    //{
                    //    MItemMaster.Instance.GetCombos();
                    //}
                    //else if (folderID == "fldrSmall")
                    //{
                       
                    //}
                    //else if (folderID == "fldrPack")
                    //{
                        MPackingMaterials.Instance.GetCombos();
                    //}
                    //else if (folderID == "fldrRaw")
                    //{
                        MRawMaterial.Instance.GetCombos();
                    //}
                    //else if (folderID == "fldrScrap")
                    //{
                        MScrapCoding.Instance.GetCombos();
                    //}
                    //else if (folderID == "fldrSemi")
                    //{
                        MSemiFinished.Instance.GetCombos();
                    //}
                    //else if (folderID == "fldrAsset")
                    //{
                        MFixedAssets.Instance.GetCombos();
                    //}
                    //else if (folderID == "fldrConsum")
                    //{
                        MConsumablesCoding.Instance.GetCombos();

                    //}
                        MFGSmallCarton.Instance.GetCombos();
                        MItemMaster.Instance.GetCombos();
                        oComboItem.Select("2", SAPbouiCOM.BoSearchKey.psk_ByValue);

                   
                    
                }
                #endregion

            }
            catch (Exception ex)
            { }

        }
        #endregion


 
    }
}

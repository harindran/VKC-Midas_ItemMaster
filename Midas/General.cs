using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace VKC
{

    class General
    {
        public struct PairValue
        {
            public string value;
            public string desc;
        }

        /// <summary>
        /// Function used to enable Docnumber
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="ControlName"></param>
        public void EnableDocNumber(SAPbouiCOM.Form frm, string ControlName)
        {
            try
            {
                SAPbouiCOM.Item txtEnable = (SAPbouiCOM.Item)frm.Items.Item(ControlName);
                SAPbouiCOM.Item Btn = (SAPbouiCOM.Item)frm.Items.Item("1");
                txtEnable.Enabled = true;
                Btn.Enabled = true;
               // frm.EnableMenu("1282", true);
            }
            catch (Exception ex)
            {
                //Global.SapApplication.MessageBox(ex.Message, 1, "", "", "");
            }
        }

        public void DisableControl(SAPbouiCOM.Form frm, string ControlName)
        {
            try
            {
                SAPbouiCOM.Item txtDisable = (SAPbouiCOM.Item)frm.Items.Item(ControlName);
                txtDisable.Enabled = false;
            }
            catch (Exception ex)
            {
                //Global.SapApplication.MessageBox(ex.Message, 1, "", "", "");
            }
        }

        public void ClearCombo(SAPbouiCOM.ComboBox ddlClear, bool blnBlankNeeded)
        {
            try
            {
                for (int i = ddlClear.ValidValues.Count-1; i >= 0; i--)
                {
                    ddlClear.ValidValues.Remove(i, SAPbouiCOM.BoSearchKey.psk_Index);
                }
                if (blnBlankNeeded)
                {
                    ddlClear.ValidValues.Add("-1", "  ");
                    ddlClear.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                }
            }
            catch (Exception ex)
            {
                //Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
            }
        }

        public double CalcColumnTotal(SAPbouiCOM.Column colTotal)
        {
            
            double dblTotal = 0;
            SAPbouiCOM.EditText textCell;
            try
            {
                for (int i = 1; i <= colTotal.Cells.Count; i++)
                {
                    textCell = (SAPbouiCOM.EditText)colTotal.Cells.Item(i).Specific;
                    if (textCell.Value != "")
                    {
                        dblTotal = dblTotal + Convert.ToDouble(textCell.Value);
                    }
                }
                return dblTotal;
            }
            catch
            {
                return 0;
            }
        }

        public double CalcColumnTotal(SAPbouiCOM.DBDataSource dDs, string col)
        {

            double dblTotal = 0;
            try
            {
                for (int i = 0; i < dDs.Size; i++)
                {
                    dblTotal = dblTotal + Convert.ToDouble(dDs.GetValue(col, i));
                }
                return dblTotal;
            }
            catch
            {
                return 0;
            }
        }

        public List<PairValue> CalcGrpColumnTotal(SAPbouiCOM.Column colTotal, SAPbouiCOM.Column colGrouping)
        {
            List<PairValue> lstColumns = new List<PairValue>();
            List<PairValue> lstRetResult = new List<PairValue>();
            PairValue pairVal = new PairValue();
            double dblTotal = 0;
            SAPbouiCOM.EditText txtCellValue;
            SAPbouiCOM.EditText txtCellGrp;

            try
            {
                //Adding Matrix Value into List
                for (int i = 1; i <= colTotal.Cells.Count; i++)
                {
                    txtCellGrp = (SAPbouiCOM.EditText)colGrouping.Cells.Item(i).Specific;
                    txtCellValue = (SAPbouiCOM.EditText)colTotal.Cells.Item(i).Specific;

                    pairVal.value = txtCellValue.Value;
                    pairVal.desc = txtCellGrp.Value;
                    lstColumns.Add(pairVal);
                }

                //Sorting the List
                lstColumns.Sort(delegate(PairValue PV1, PairValue PV2) { return PV1.desc.CompareTo(PV2.desc); });
                pairVal.desc = "";
                pairVal.value = "0.0";

                //Adding a Blank data to Last position of the list
                lstColumns.Add(pairVal);
                string strPrevDesc = "";
                Double dblGrpTotal = 0;

                //Grouping the list and finding the sum of each group
                foreach (PairValue values in lstColumns)
                {
                    if (strPrevDesc == "")
                    {
                        dblGrpTotal = Convert.ToDouble(values.value);
                    }
                    else if (strPrevDesc == values.desc)
                    {
                        dblGrpTotal = dblGrpTotal + Convert.ToDouble(values.value);
                    }
                    else
                    {
                        pairVal.desc = strPrevDesc;
                        pairVal.value = Convert.ToString(dblGrpTotal);
                        lstRetResult.Add(pairVal);
                        dblGrpTotal = Convert.ToDouble(values.value);
                    }
                    strPrevDesc = values.desc;
                }

                return lstRetResult;
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "", "", "");
                return null;
            }
        }

        public void FillCombo(SAPbouiCOM.Form frmFill, SAPbouiCOM.ComboBox ddlFill, string tableName, string value, string description)
        {
            SAPbouiCOM.DBDataSource dDsFill;
            try
            {
                dDsFill = frmFill.DataSources.DBDataSources.Item(tableName);
                dDsFill.Query(null);
                ddlFill.ValidValues.Add("-1", "   ");
                for (int i = 0; i < dDsFill.Size; i++)
                {
                    ddlFill.ValidValues.Add(dDsFill.GetValue(value, i).Trim(), dDsFill.GetValue(description, i).Trim());
                }
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
            }

        }

        public void FillCombo(SAPbouiCOM.Form frmFill, SAPbouiCOM.ComboBox ddlFill, string tableName, string value, string description, string strWhere, bool DefineNew,bool Blank)
        {
            SAPbobsCOM.Recordset rsFill;
            string strQry;
            clearCombo(ddlFill, true);
            try
            {
                strQry = "SELECT DISTINCT " + value + ", " + description + " FROM [" + tableName + "] " + strWhere;
                rsFill = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                rsFill.DoQuery(strQry);
                if (ddlFill.ValidValues.Count > 0)
                {
                    ClearCombo(ddlFill, false);
                }
                if (Blank)
                {
                    ddlFill.ValidValues.Add("-1", "   ");
                }
                while (!rsFill.EoF)
                {
                    ddlFill.ValidValues.Add(rsFill.Fields.Item(value).Value.ToString(), rsFill.Fields.Item(description).Value.ToString());
                    rsFill.MoveNext();
                }

                if (DefineNew)
                {
                    ddlFill.ValidValues.Add("-999", "Define New");
                }
            }
            catch (Exception ex)
            {
               // Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
            }
        }

        public void FillCombo(SAPbouiCOM.Form frmFill, SAPbouiCOM.ComboBox ddlFill, string tableName, string value, string description, SAPbouiCOM.Conditions condsFill, bool DefineNew, bool Blank)
        {
            SAPbouiCOM.DBDataSource dDsFill;
            try
            {
                General gen = new General();
                clearCombo(ddlFill, true);
                dDsFill = frmFill.DataSources.DBDataSources.Item(tableName);
                dDsFill.Query(condsFill);
                if (ddlFill.ValidValues.Count > 0)
                {
                    gen.ClearCombo(ddlFill, false);
                }
                if (Blank)
                {
                    ddlFill.ValidValues.Add("-1", "   ");
                } 
                for (int i = 0; i < dDsFill.Size; i++)
                {
                    ddlFill.ValidValues.Add(dDsFill.GetValue(value, i).Trim(), dDsFill.GetValue(description, i).Trim());
                }

                if (DefineNew)
                {
                    ddlFill.ValidValues.Add("-999", "Define New");
                }
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
            }

        }

        internal void FillCombo(SAPbouiCOM.ComboBox ddlFill, string Table, string Value, string Description, string Filter, bool Blank)
        {
            SAPbobsCOM.Recordset rsFill;
            string strQry;
            try
            {
                ClearCombo(ddlFill);
                strQry = "SELECT [" + Table + "]." + Value + ", [" + Table + "]." + Description + " FROM [" + Table + "] " + Filter + " Order by [" + Table + "]." + Value;
                rsFill = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                rsFill.DoQuery(strQry);
                if (Blank)
                {
                    ddlFill.ValidValues.Add("-3", "   ");
                }
                while (!rsFill.EoF)
                {
                    ddlFill.ValidValues.Add(rsFill.Fields.Item(0).Value.ToString(), rsFill.Fields.Item(1).Value.ToString());
                    rsFill.MoveNext();
                }

            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
            }
        }

        public void ClearCombo(SAPbouiCOM.ComboBox ddlClear)
        {
            try
            {
                for (int i = ddlClear.ValidValues.Count - 1; i >= 0; i--)
                {
                    ddlClear.ValidValues.Remove(i, SAPbouiCOM.BoSearchKey.psk_Index);
                }
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
            }
        }

        public void FillCombo(SAPbouiCOM.Form frmFill, SAPbouiCOM.ComboBox ddlFill, string tableName, string value, string description, bool DefineNew,bool Blank)
        {
            SAPbouiCOM.DBDataSource dDsFill;
            try
            {
                dDsFill = frmFill.DataSources.DBDataSources.Item(tableName);
                dDsFill.Query(null);
                if (ddlFill.ValidValues.Count > 0)
                {
                    ClearCombo(ddlFill, false);
                }

                if (Blank)
                {
                    ddlFill.ValidValues.Add("-1", "   ");
                }

                for (int i = 0; i < dDsFill.Size; i++)
                {
                    ddlFill.ValidValues.Add(dDsFill.GetValue(value, i).Trim(), dDsFill.GetValue(description, i).Trim());
                }

                if (DefineNew)
                {
                    ddlFill.ValidValues.Add("-999", "Define New");
                }
              

            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
            }

        }

        public void FillCombo_Query(SAPbouiCOM.Form frmFill, SAPbouiCOM.ComboBox ddlFill, string Query, string value, string description, bool DefineNew, bool Blank)
        {
          
        try
            {
                
                if (ddlFill.ValidValues.Count > 0)
                {
                    ClearCombo(ddlFill, false);
                }

                //if (Blank)
                //{
                //    ddlFill.ValidValues.Add("-1", "   ");
                //}

                SAPbobsCOM.Recordset objrs;
                objrs = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                objrs.DoQuery(Query);
                for (int i = 0; i < objrs.RecordCount; i++)
                {
                    ddlFill.ValidValues.Add(Convert.ToString(objrs.Fields.Item(value).Value), Convert.ToString(objrs.Fields.Item(description).Value));
                    objrs.MoveNext();
                }

                if (DefineNew)
                {
                    ddlFill.ValidValues.Add("-999", "Define New");
                }


            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
            }

        }

        //----------------created on 23-06-2012------------------//
        internal void FillCombo(SAPbouiCOM.ComboBox ddlFill, bool Blank)
        {
            SAPbobsCOM.Recordset rsFill;
            string strQry;
            try
            {
                ClearCombo(ddlFill);
                strQry = "select ItmsGrpCod Code,ItmsGrpNam Name from OITB where isnull(U_isbrand,'N') ='N'";
                rsFill = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                rsFill.DoQuery(strQry);
                if (Blank)
                {
                    ddlFill.ValidValues.Add("-1", "   ");
                }
                while (!rsFill.EoF)
                {
                    ddlFill.ValidValues.Add(rsFill.Fields.Item(0).Value.ToString(), rsFill.Fields.Item(1).Value.ToString());
                    rsFill.MoveNext();
                }

            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
            }
        }
        //---------------------------------------------------------//
        public string getNextCode(string strTable, string strField)
        {
            string strQry = "SELECT isnull(MAX(CAST(" + strField + " AS int)),0) +1 AS nextNumber FROM [" + strTable + "]";
            SAPbobsCOM.Recordset rsNextNo;
            try
            {
                rsNextNo = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                rsNextNo.DoQuery(strQry);
                if (rsNextNo.EoF == false)
                {
                    if (System.Convert.ToString(rsNextNo.Fields.Item(0).Value) != "0")
                    {
                        return System.Convert.ToString(rsNextNo.Fields.Item(0).Value);
                    }
                    else
                    {
                        return "1";
                    }
                }
                else
                {
                    return "1";
                }
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
                return "0";
            }
        }

        public bool MandatoryChecking(string[] arrFields, SAPbouiCOM.DBDataSource dDsMand)
        {
            try
            {
                int iUBound = arrFields.GetUpperBound(0);
                for (int j = 0; j < dDsMand.Size; j++)
                {
                    for (int i = 0; i <= iUBound; i++)
                    {
                        if (dDsMand.GetValue(arrFields[i], j).Trim() == "")
                        {
                            Global.SapApplication.StatusBar.SetText("Mandatory fields are empty.", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
                return false;
            }
        }

        public bool MandatoryChecking(SAPbouiCOM.Form frm, string[] arrUserDataSource)
        {
            try
            {
                int iUBound = arrUserDataSource.GetUpperBound(0);
                for (int i = 0; i < iUBound; i++)
                {
                    if (frm.DataSources.UserDataSources.Item(arrUserDataSource.GetValue(i)).Value.Trim() == "")
                    {
                        Global.SapApplication.StatusBar.SetText("Mandatory fields are empty.", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
                return false;
            }
        }

        /// <summary>
        /// This Function used for checking wheather the mandatory fields are empty or not
        /// First argument should be list of Item names
        /// Each Item should have description (there is an option to set Description in each item)
        /// </summary>
        /// <param name="arrItems"></param>
        /// <param name="frm"></param>
        /// <returns></returns>
        public bool MandatoryChecking(string[] arrItems, SAPbouiCOM.Form frm)
        {
            try
            {
                int iUBound = arrItems.GetUpperBound(0);

                string strBlankFields="";
                bool boolBlankExist=false;
                SAPbouiCOM.Item item = null;
                for (int i = 0; i <= iUBound; i++)
                {
                    item = frm.Items.Item(arrItems[i]);
                    //For EditText
                    if (item.Type == SAPbouiCOM.BoFormItemTypes.it_EDIT)
                    {
                        SAPbouiCOM.EditText txtItem = (SAPbouiCOM.EditText)item.Specific;
                        if (txtItem.Value.Trim() == "")
                        {
                            boolBlankExist = true;
                            strBlankFields = strBlankFields + "," + item.Description;
                        }
                    }

                    //For ComboBox
                    if (item.Type == SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX)
                    {
                        SAPbouiCOM.ComboBox ddlItem = (SAPbouiCOM.ComboBox)item.Specific;
                        if (ddlItem.Selected == null)
                        {
                            boolBlankExist = true;
                            strBlankFields = strBlankFields + "," + item.Description;
                        }
                    }
                }
                if (boolBlankExist)
                {
                    strBlankFields = strBlankFields.Substring(1);
                    Global.SapApplication.StatusBar.SetText("Please Fill " + strBlankFields + " These are Mandatory", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    item.Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
                return false;
            }
        }

        private void clearCombo(SAPbouiCOM.ComboBox ddlClear, bool blnBlankNeeded)
        {
            try
            {
                for (int i = ddlClear.ValidValues.Count - 1; i >= 0; i--)
                {
                    ddlClear.ValidValues.Remove(i, SAPbouiCOM.BoSearchKey.psk_Index);
                }
                if (blnBlankNeeded)
                {
                    ddlClear.ValidValues.Add("-1", "  ");
                    ddlClear.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                }
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
            }
        }

        private void FillCombo1(SAPbouiCOM.Form frmFill, SAPbouiCOM.ComboBox ddlFill, string tableName, string value, string description, bool DefineNew)
        {
            SAPbobsCOM.Recordset rsFill;
            string strQry;
            try
            {
                strQry = "SELECT " + value + ", " + description + " FROM " + tableName ;
                rsFill = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                rsFill.DoQuery(strQry);
                ddlFill.ValidValues.Add("-1", "   ");
                if (ddlFill.ValidValues.Count > 0)
                {
                    clearCombo(ddlFill, false);
                }
                while (!rsFill.EoF)
                {
                    ddlFill.ValidValues.Add(rsFill.Fields.Item(value).Value.ToString(), rsFill.Fields.Item(description).Value.ToString());
                    rsFill.MoveNext();
                }
                if (DefineNew)
                {
                    ddlFill.ValidValues.Add("-999", "Define New");
                }
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
            }
        }
 
        public void FillBranch(SAPbouiCOM.Form frmInit, SAPbouiCOM.ComboBox ddlBranch, bool defineNew)
        {
            string strWhere = "WHERE U_UsrCode ='" + Global.SapCompany.UserName + "'";
            ClearCombo(ddlBranch, false);
            FillCombo(frmInit, ddlBranch, "[@UBRS]", "U_BrCode", "U_BrName", strWhere, defineNew,false);
        }

        public void SetFocus(SAPbouiCOM.Form form,string matUID, string ColUID)
        {
            SAPbouiCOM.Item matItem = form.Items.Item(matUID);
            SAPbouiCOM.Matrix mat = (SAPbouiCOM.Matrix)matItem.Specific;
            
            if (mat.RowCount > 0)
            {
                //SAPbouiCOM.Column col = (SAPbouiCOM.Column)mat.Columns.Item(ColUID);
                //if (col.Type == SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX)
                //{
                //    SAPbouiCOM.ComboBox ddl = (SAPbouiCOM.ComboBox)mat.Columns.Item(ColUID).Cells.Item(1).Specific;
                //    ddl.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                //}
                //else
                {
                    try
                    {
                        SAPbouiCOM.Cell cell = (SAPbouiCOM.Cell)mat.Columns.Item(ColUID).Cells.Item(1);
                        cell.Click(SAPbouiCOM.BoCellClickType.ct_Regular, mat.RowCount);
                    }
                    catch { }
                    
                }
            }

        }

        public void FillBranchTo(SAPbouiCOM.Form frmInit, SAPbouiCOM.ComboBox ddlBranch, bool defineNew)
        {
            string strWhere = "WHERE U_UsrCode ='" + Global.SapCompany.UserName + "'";
            ClearCombo(ddlBranch, false);
            FillCombo1(frmInit, ddlBranch, "[OUBR]", "Code", "Name",true);
        }

        public int Insert(string strTable, string[] arrFields, string[] arrValues)
        {
            return 0;
        }

        public void SetMenu(SAPbouiCOM.ContextMenuInfo pVal)
        {
            try
            {
                SAPbouiCOM.Form frmSetMenu = Global.SapApplication.Forms.Item(pVal.FormUID);
                if (pVal.ItemUID != "")
                {
                    SAPbouiCOM.Item itmSetMenu = frmSetMenu.Items.Item(pVal.ItemUID);
                    SAPbouiCOM.Matrix mat;
                    if (itmSetMenu.Type == SAPbouiCOM.BoFormItemTypes.it_MATRIX)
                    {
                        mat = (SAPbouiCOM.Matrix)itmSetMenu.Specific;
                        if (mat.RowCount > 0)
                        {
                            frmSetMenu.Menu.Add("mnuDelRow", "Delete Row", SAPbouiCOM.BoMenuType.mt_STRING, 2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Global.SapApplication.MessageBox(ex.Message, 1, "", "", "");
            }
        }

        internal void NavigateEventArgs(string formUID, string tableName)
        {
            SAPbouiCOM.Form form = Global.SapApplication.Forms.Item(formUID);
            SAPbouiCOM.DBDataSource DbDsDoc = form.DataSources.DBDataSources.Item(tableName);
            SAPbobsCOM.Recordset rsNavigate = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

            string strQry = "SELECT SeriesName FROM [NNM1] WHERE ObjectCode='" + form.BusinessObject.Type + "' AND Series=" + DbDsDoc.GetValue("Series", 0);
            rsNavigate.DoQuery(strQry);
            if (!rsNavigate.EoF)
            {
                form.DataSources.UserDataSources.Item("Nor_Series").Value = rsNavigate.Fields.Item("SeriesName").Value.ToString();
            }
        }

        internal void LoadXMLForm(string fileName)
        {
            try
            {
                System.Xml.XmlDocument oXmlDoc = null;

                oXmlDoc = new System.Xml.XmlDocument();

                // load the content of the XML File
                string sPath = null;

                sPath = System.Windows.Forms.Application.StartupPath.ToString();
                //sPath = System.IO.Directory.GetParent(sPath).ToString();

                //oXmlDoc.Load(sPath + "\\XML\\" + fileName);
                oXmlDoc.Load(sPath + "\\" + fileName);

                // load the form to the SBO application in one batch
                string sXML = oXmlDoc.InnerXml.ToString();
                Global.SapApplication.LoadBatchActions(ref sXML);
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "Ok", "", "");
            }
        }

        private void SaveAsXML(SAPbouiCOM.Form oForm, string strName)
        {
            //**********************************************************************
            // always use XML to work with user forms.
            // after creating your form save it as an XML file
            //**********************************************************************

            System.Xml.XmlDocument oXmlDoc = null;

            oXmlDoc = new System.Xml.XmlDocument();
            string sXmlString = null;
            //get the form as an XML string
            sXmlString = oForm.GetAsXML();

            //load the form's XML string to the
            //XML document object
            oXmlDoc.LoadXml(sXmlString);

            //save the XML Document
            string sPath = null;
            sPath = Application.StartupPath;
            oXmlDoc.Save((sPath + "\\" + strName + ".srf"));

        }

        internal string GetWhseItemStock(string strItemCode, string strWhseCode)
        {
            SAPbobsCOM.Recordset rsStock = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            string strQry = "SELECT OnHand FROM OITW WHERE (WhsCode = '{0}') AND (ItemCode = '{1}')";
            strQry = string.Format(strQry, strWhseCode, strItemCode);
            rsStock.DoQuery(strQry);

            if (!rsStock.EoF)
                return rsStock.Fields.Item(0).Value.ToString();
            else
                return "0";
        }

        internal int ItemWhsLineNo(string WhsCode)
        {
            SAPbobsCOM.Recordset rsSelect = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            string query = "select a.whscode,a.row from(select whscode [whscode],row_number() over(order by WhsCode asc) [row] from owhs) a  where a.whscode='{0}'";
            query = String.Format(query, WhsCode);
            rsSelect.DoQuery(query);
            if (!rsSelect.EoF)
                return Convert.ToInt16(rsSelect.Fields.Item(0).Value.ToString());
            else
                return 0;
        }

        internal string GetPrdFloor(string strBranch)
        {
            string strWhsCode = "";
            try
            {

                SAPbobsCOM.Recordset rsProdWhs = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string strQuery = "select WhsCode,WhsName from OWHS where U_Nor_Branch='" + strBranch + "' and U_Nor_ProdFloor='Y'";
                rsProdWhs.DoQuery(strQuery);
                if (!rsProdWhs.EoF)
                {
                    strWhsCode = rsProdWhs.Fields.Item(0).Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "", "", "");
            }

            return strWhsCode;
        }

        internal int GetDefltDocNum(string strUDO)
        {
            try
            {
                SAPbobsCOM.Recordset rsDocNum = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string strQry = "SELECT NNM1.NextNumber FROM NNM1 INNER JOIN ONNM ON NNM1.ObjectCode = ONNM.ObjectCode AND NNM1.Series = ONNM.DfltSeries WHERE (ONNM.ObjectCode = N'{0}')";
                strQry = string.Format(strQry, strUDO);
                rsDocNum.DoQuery(strQry);

                if (!rsDocNum.EoF)
                {
                    return Convert.ToInt32(rsDocNum.Fields.Item(0).Value);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "", "", "");
                return 0;
            }


        }

        internal int GetNextDocEntry(string strObjectCode)
        {
            try
            {
                SAPbobsCOM.Recordset rsDocNum = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string strQry = "SELECT AutoKey FROM ONNM WHERE (ONNM.ObjectCode = N'{0}')";
                strQry = string.Format(strQry, strObjectCode);
                rsDocNum.DoQuery(strQry);

                if (!rsDocNum.EoF)
                {
                    return Convert.ToInt32(rsDocNum.Fields.Item(0).Value);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "", "", "");
                return 0;
            }
        }

        internal string GetNextRawItemCode(string strPrdCode, string strAreaCode)
        {
            try
            {
                string strQry = "SELECT MAX(CAST (SUBSTRING(ItemCode,len('{0}')+1, LEN(ItemCode)-len('{0}')) AS NUMERIC))+1 FROM OITM WHERE U_OU_PCode='{1}' AND U_OU_ACode='{2}'";
                strQry = string.Format(strQry, strPrdCode.Trim() + strAreaCode.Trim(), strPrdCode.Trim(), strAreaCode.Trim());
                SAPbobsCOM.Recordset rsNextCode = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                rsNextCode.DoQuery(strQry);

                if (rsNextCode.EoF)
                    return strPrdCode.Trim() + strAreaCode.Trim() + "0001";
                else
                    return strPrdCode.Trim() + strAreaCode.Trim() + rsNextCode.Fields.Item(0).Value.ToString().PadLeft(4, '0');
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "", "", "");
                return "";
            }
        }

        #region Generate Serial Number For the Product
        internal string genSerial(string strDocNum)
        {
            try
            {
                string strQry = "SELECT CONVERT(VARCHAR, OWOR.PostDate,112) PrdDate,ISNULL( MAX(CAST(REVERSE(SUBSTRING(REVERSE(TR.U_Serial),0,3)) AS INT )),0)+1 " +
                                "AS NEXTNO FROM OWOR LEFT JOIN [@TEST_REPORT_DOC] TR ON OWOR.DocEntry=TR.U_PrdnId  WHERE OWOR.DocNum={0} GROUP BY OWOR.PostDate ";
                strQry = string.Format(strQry, strDocNum.Trim());
                SAPbobsCOM.Recordset rsSerial = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                rsSerial.DoQuery(strQry);
                string strPostDate = rsSerial.Fields.Item("PrdDate").Value.ToString();
                string strNextNo = rsSerial.Fields.Item("NEXTNO").Value.ToString();

                string strSerialNo = strPostDate.Trim() + '-' + strDocNum.Trim() + '-' + strNextNo.Trim().ToString().PadLeft(3, '0');

                return strSerialNo;
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "", "", "");
                return "";
            }
        }
        #endregion

        internal string CreateItemCode(string strPrdCode, string strMType)
        {
            try
            {
                return strMType + "_" + strPrdCode.Trim();
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "", "", "");
                return "";
            }
        }

        internal string CreateItemCode(string strPrdCode, string strMType, string strCurc)
        {
            try
            {
                return strMType + "_" + strPrdCode.Trim() + "_" + strCurc.Trim();
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "", "", "");
                return "";
            }
        }

        internal string CreateItemCode(string strPrdCode, string strMType, string strPiperine, string strOil)
        {
            try
            {
                return strMType + "_" + strPrdCode.Trim() + "_" + strPiperine.Trim() + "/" + strOil.Trim();
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "", "", "");
                return "";
            }
        }

        internal int GetItemGrpCode(string strPrdCode, string strMType)
        {
            SAPbobsCOM.Recordset rsItmGrp = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            string strQry = "SELECT ItmsGrpCod FROM OITB WHERE (U_UO_PrdCode = N'{0}') AND (U_UO_MType = N'{1}')";

            strQry = string.Format(strQry, strPrdCode, strMType);

            rsItmGrp.DoQuery(strQry);
            if (!rsItmGrp.EoF)
            {
                return Convert.ToInt32(rsItmGrp.Fields.Item(0).Value);
            }
            else
            {
                return CreateItemGroup(strPrdCode, strMType);
            }
            rsItmGrp = null;

        }

        internal int CreateItemGroup(string strPrdCode, string strMType)
        {
            try
            {
                int iErrCode = 0;
                SAPbobsCOM.ItemGroups itmGrps = (SAPbobsCOM.ItemGroups)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItemGroups);
                SAPbobsCOM.Recordset rsDesc = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string strPrdName = "";
                string strMTypeName = "";
                string strQry = "SELECT Name FROM [@UO_MTYPE] WHERE (Code = N'{0}')";
                //strQry = string.Format(strQry, strMType);
                //rsDesc.DoQuery(strQry);
                //if (!rsDesc.EoF)
                //{
                //    strMTypeName = rsDesc.Fields.Item(0).Value.ToString();
                //}
                strQry = "SELECT Name FROM [@UO_PRODUCT] WHERE (Code = N'{0}')";
                strQry = string.Format(strQry, strPrdCode.Trim());

                rsDesc.DoQuery(strQry);
                if (!rsDesc.EoF)
                {
                    strPrdName = rsDesc.Fields.Item(0).Value.ToString();
                }

                rsDesc = null;
                int iEnd = strPrdName.Length;
                if (iEnd >= 15)
                    iEnd = 15;
                itmGrps.GroupName = strPrdName.Substring(0, iEnd) + "-" + strMType;
                itmGrps.UserFields.Fields.Item("U_UO_PrdCode").Value = strPrdCode;
                itmGrps.UserFields.Fields.Item("U_UO_MType").Value = strMType;


                iErrCode = itmGrps.Add();

                if (iErrCode != 0)
                {
                    Global.SapApplication.StatusBar.SetText(Global.SapCompany.GetLastErrorDescription(), SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return 0;
                }
                else
                {
                    itmGrps.Browser.MoveFirst();
                    itmGrps = null;
                    return GetItemGrpCode(strPrdCode, strMType);
                }

            }
            catch (Exception ex)
            {
                Global.SapApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return 0;
            }

        }

        /// <summary>
        /// Function for creating the Item Description
        /// </summary>
        /// <param name="strPrdCode"></param>
        /// <param name="strAreaCode"></param>
        /// <returns></returns>
        private string CreateItemDescr(string strPrdCode, string strAreaCode, string strTypeName)
        {
            string strPrdDesc = "";
            string strAreaDesc = "";
            string strSupDesc = "";
            SAPbobsCOM.Recordset rsGetDesc = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            string strQry = "SELECT Name FROM [@UO_PRODUCT] WHERE (Code = N'" + strPrdCode + "')";

            rsGetDesc.DoQuery(strQry);
            if (!rsGetDesc.EoF)
            {
                strPrdDesc = rsGetDesc.Fields.Item(0).Value.ToString();
            }

            strQry = "SELECT Name FROM [@UO_AREA] WHERE (Code = N'" + strAreaCode + "')";

            rsGetDesc.DoQuery(strQry);
            if (!rsGetDesc.EoF)
            {
                strAreaDesc = rsGetDesc.Fields.Item(0).Value.ToString();
            }

            return strPrdDesc + "-" + strAreaDesc + "-" + strTypeName;
        }

        /// <summary>
        /// General Function for Closing Document
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="DocEntry"></param>
        internal void CloseDocument(string TableName,string DocEntry)
        {
            try
            {
                SAPbobsCOM.Recordset rsClose = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string strQry = "UPDATE [{0}] SET Status = 'C' WHERE DocEntry = {1}";

                strQry = string.Format(strQry, TableName, DocEntry);
                rsClose.DoQuery(strQry);
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "", "", "");
            }

        }

        /// <summary>
        /// Function for Closing the Production Order
        /// </summary>
        /// <param name="iDocEntry"></param>
        /// <returns></returns>
        internal bool ClosePrdOrder(int iDocEntry)
        {
            try
            {
                int iErrorCode = 0;
                SAPbobsCOM.ProductionOrders prdOrders = (SAPbobsCOM.ProductionOrders)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oProductionOrders);
                prdOrders.GetByKey(iDocEntry);
                prdOrders.ProductionOrderStatus = SAPbobsCOM.BoProductionOrderStatusEnum.boposClosed;
                iErrorCode = prdOrders.Update();

                if (iErrorCode == 0)
                {
                    return true;
                }
                else
                {
                    Global.SapApplication.MessageBox(Global.SapCompany.GetLastErrorDescription(), 1, "", "", "");
                }

            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "", "", "");
            }
            return false;

        }

        internal double GetBatchStock(string strItemCode, string strWhseCode,string strBatch)
        {

            try
            {
                SAPbobsCOM.Recordset rsStock = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string strQry = "SELECT Quantity FROM OIBT WHERE (BatchNum = N'{0}') AND (ItemCode = N'{1}') AND (WhsCode = N'{2}')";
                strQry = string.Format(strQry, strBatch, strItemCode, strWhseCode);
                rsStock.DoQuery(strQry);

                if (!rsStock.EoF)
                    return Convert.ToDouble(rsStock.Fields.Item(0).Value);
                else
                    return 0.0;
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "", "", "");
                return 0.0;
            }
        }

        internal double GetFIFOItemCost(string strItemCode, string strWhs, double dblQty)
        {
            SAPbobsCOM.Recordset rsItemCost = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            double dblCost = 0.0;
            double dblBalQty = dblQty;
            double dblTotalCost = 0.0;
            double dblPrice = 0.0;
            string strQry = @"SELECT OINM.Price, OINM.OpenQty FROM OINM WHERE  OINM.ItemCode='{0}' AND " +
                            @" OINM.Warehouse='{1}' AND OINM.OpenQty>0 AND OINM.InQty>0 " +
                            @" ORDER BY OINM.DocDate, OINM.BASE_REF";

            strQry = string.Format(strQry, strItemCode, strWhs);
            rsItemCost.DoQuery(strQry);

            if (!rsItemCost.EoF)
            {
                while (!rsItemCost.EoF & dblBalQty > 0)
                {
                    double dblOpnQty = Convert.ToDouble(rsItemCost.Fields.Item("OpenQty").Value);
                    dblPrice = Convert.ToDouble(rsItemCost.Fields.Item("Price").Value);
                    if (dblOpnQty >= dblBalQty)
                    {
                        dblTotalCost = dblTotalCost + dblBalQty * dblPrice;
                        dblBalQty = 0;
                    }
                    else
                    {
                        dblTotalCost = dblTotalCost + dblOpnQty * dblPrice;
                        dblBalQty = dblBalQty - dblOpnQty;
                    }
                    rsItemCost.MoveNext();

                }
                if (dblBalQty > 0)
                {
                    dblTotalCost = dblTotalCost + dblBalQty * dblPrice;
                }
                if (dblQty > 0)
                {
                    dblCost = dblTotalCost / dblQty;
                }
            }
            return dblCost;
        }

        internal void FillItemType(SAPbouiCOM.ComboBox ddlFill)
        {
            string strQry = "SELECT Code, Name FROM [@KPL_ITEMTYPE]";
            SAPbobsCOM.Recordset rsFill = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            try
            {
                rsFill.DoQuery(strQry);
                while (!rsFill.EoF)
                {
                    try
                    {
                        ddlFill.ValidValues.Add(rsFill.Fields.Item(0).Value.ToString(), rsFill.Fields.Item(1).Value.ToString());
                        rsFill.MoveNext();
                    }
                    catch (Exception ex)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "", "", "");
            }
        }

        /// <summary>
        /// Function used to Delete Matrix row 
        /// </summary>
        /// <param name="frm"></param>
        internal void DeleteMatrixRow(SAPbouiCOM.Form frm)
        {
            try
            {
                string strMtxId = frm.DataSources.UserDataSources.Item("usdRtItem").Value;
                int iRow = Convert.ToInt32(frm.DataSources.UserDataSources.Item("usdRtRow").Value);
                SAPbouiCOM.Matrix mtxDR = (SAPbouiCOM.Matrix)frm.Items.Item(strMtxId).Specific;
                if (mtxDR.RowCount > 1)
                {
                    mtxDR.DeleteRow(iRow);

                }
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "", "", "");
            }

        }

        internal string GetDigits(string Digit)
        {
            string strInwords = "";

            try
            {
                switch (Convert.ToInt32(Digit))
                {
                    case 1:
                        strInwords = "One ";
                        break;
                    case 2:
                        strInwords = "Two ";
                        break;
                    case 3:
                        strInwords = "Three ";
                        break;
                    case 4:
                        strInwords = "Four ";
                        break;
                    case 5:
                        strInwords = "Five ";
                        break;
                    case 6:
                        strInwords = "Six ";
                        break;
                    case 7:
                        strInwords = "Seven ";
                        break;
                    case 8:
                        strInwords = "Eight ";
                        break;
                    case 9:
                        strInwords = "Nine ";
                        break;
                }
            }
            catch (Exception ex)
            {
 
            }
            return strInwords;
 
        }

        internal string GetTens(string TensText)
        {
            try
            {
                string strInwords="";
                if (Convert.ToInt32(TensText.Substring(0, 1)) == 1)
                {
                    switch (Convert.ToInt32(TensText))
                    {
                        case 10:
                            strInwords = "Ten ";
                            break;
                        case 11:
                            strInwords = "Eleven ";
                            break;
                        case 12:
                            strInwords = "Twelve ";
                            break;
                        case 13:
                            strInwords = "Thirteen ";
                            break;
                        case 14:
                            strInwords = "Fourteen ";
                            break;
                        case 15:
                            strInwords = "Fifteen ";
                            break;
                        case 16:
                            strInwords = "Sixteen ";
                            break;
                        case 17:
                            strInwords = "Seventeen ";
                            break;
                        case 18:
                            strInwords = "Eighteen ";
                            break;
                        case 19:
                            strInwords = "Ninteen ";
                            break;
                    }
                }
                else
                {
                    switch (Convert.ToInt32(TensText.Substring(0, 1)))
                    {
                        case 2:
                            strInwords = "Twenty ";
                            break;
                        case 3:
                            strInwords = "Thirty ";
                            break;
                        case 4:
                            strInwords = "Forty ";
                            break;
                        case 5:
                            strInwords = "Fifty ";
                            break;
                        case 6:
                            strInwords = "Sixty ";
                            break;
                        case 7:
                            strInwords = "Seventy ";
                            break;
                        case 8:
                            strInwords = "Eighty ";
                            break;
                        case 9:
                            strInwords = "Ninety ";
                            break;
                    }
                }
                strInwords = strInwords + GetDigits(TensText.Substring(1, 1));
                return strInwords;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        internal string GetHundreds(string HundredsText)
        {
            try
            {
                string strInwords = "";
                if (Convert.ToInt32(HundredsText) == 0)
                    return "";
                else
                {
                    if (HundredsText.Substring(0, 1) != "0")
                    {
                        strInwords = GetDigits(HundredsText.Substring(0, 1)) + "Hundred and ";
                    }
                    if (HundredsText.Substring(1, 1) != "0")
                    {
                        strInwords = strInwords + GetTens(HundredsText.Substring(1, 2));
                    }
                    else
                    {
                        strInwords = strInwords + GetDigits(HundredsText.Substring(2, 1));
                    }
                }
                return strInwords;
            }
            catch (Exception ex)
            {
                return "";
            }


        }


        internal string NumberToWords(double dblAmount)
        {
            string strInwords = "";

            try
            {
                string strThousand = "Thousand ";
                string strLakh = "Lakh ";
                string strCrore = "Crore ";

                int iDecimalPoint = dblAmount.ToString().IndexOf('.');
                string strWholeNo = dblAmount.ToString().Substring(0, iDecimalPoint);
                string strDecimalPart = dblAmount.ToString().Substring(iDecimalPoint, 2);
                if (strWholeNo.Length <= 3)
                    strInwords = GetHundreds(strWholeNo);
                else
                {

                    if (strWholeNo.Length == 4)
                        strInwords = GetDigits(strWholeNo.Substring(0, 1)) + strThousand + GetHundreds(strWholeNo.Substring(1, 3));
                    else if (strWholeNo.Length == 5)
                        strInwords = GetTens(strWholeNo.Substring(0, 2)) + strThousand + GetHundreds(strWholeNo.Substring(2, 3));
                    else if (strWholeNo.Length == 6)
                        strInwords = GetDigits(strWholeNo.Substring(0, 1)) + strLakh + GetTens(strWholeNo.Substring(1, 2)) + strThousand + GetHundreds(strWholeNo.Substring(3, 3));
                    else if (strWholeNo.Length == 7)
                        strInwords = GetTens(strWholeNo.Substring(0, 2)) + strLakh + GetTens(strWholeNo.Substring(2, 2)) + strThousand + GetHundreds(strWholeNo.Substring(4, 3));

                }
            }
            catch (Exception ex)
            {
                
            }

            return strInwords;
        }
        #region RawMaterialSerialNo
        //---------------------created date: 22-03-2012-------VKC---------------------
        internal string GetNextItemCode(string strGroupCode ,string strStartchar)
        {
            try
            {
                string strQry = "SELECT ISNULL(MAX(CAST (SUBSTRING(ItemCode,LEN(ItemCode) -3,4) AS NUMERIC)),0)+1 FROM OITM WHERE  LEFT(ItemCode,6)  ='" + strStartchar + "'";
               // strQry = string.Format(strQry, strPrdCode.Trim() + strAreaCode.Trim(), strPrdCode.Trim(), strAreaCode.Trim());
                SAPbobsCOM.Recordset rsNextCode = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                rsNextCode.DoQuery(strQry);
                int a = rsNextCode.RecordCount;
               
                    if (rsNextCode.EoF || rsNextCode.RecordCount == 0)
                        return "0001";
                    else
                        return rsNextCode.Fields.Item(0).Value.ToString().PadLeft(4, '0');
               
            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "", "", "");
                return "";
            }
        }
        #endregion

        #region Consumable Code
        //---------------------created date: 22-03-2012-------VKC---------------------
        internal string GetNextItemCode(string strGroupCode)
        {
            try
            {
                string strQry = "SELECT ISNULL(MAX(CAST (SUBSTRING(ItemCode,LEN(ItemCode) -3,4) AS NUMERIC)),0)+1 FROM OITM WHERE U_GrpCode='" + strGroupCode + "'";
                // strQry = string.Format(strQry, strPrdCode.Trim() + strAreaCode.Trim(), strPrdCode.Trim(), strAreaCode.Trim());
                SAPbobsCOM.Recordset rsNextCode = (SAPbobsCOM.Recordset)Global.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                rsNextCode.DoQuery(strQry);
                int a = rsNextCode.RecordCount;
                string val = rsNextCode.Fields.Item(0).Value.ToString().PadLeft(4, '0');
                if (rsNextCode.EoF || rsNextCode.RecordCount == 0)
                    return "0001";
                else
                    return rsNextCode.Fields.Item(0).Value.ToString().PadLeft(4, '0');

            }
            catch (Exception ex)
            {
                Global.SapApplication.MessageBox(ex.Message, 1, "", "", "");
                return "";
            }
        }
        #endregion

//---------------------On 11-04-2012-----------------------------------------------------------------------------//
        #region Connect To Other Company
        internal void connectOtherCompany(string Server, string CompanyDB, string SAPUser, string SAPPass, string SQLUser, string SQLPass)
        {
            try
            {
                string cookie, sErrorMsg;
                int iErrorCode = 0;
                string connStr;
                Global.NewCompany = new SAPbobsCOM.Company();
                Global.NewCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2019;
                Global.NewCompany.Server = Server;
                Global.NewCompany.CompanyDB = CompanyDB;
                Global.NewCompany.UserName = SAPUser;
                Global.NewCompany.Password = SAPPass;
                Global.NewCompany.DbUserName = SQLUser;
                Global.NewCompany.DbPassword = SQLPass;

                iErrorCode = Global.NewCompany.Connect();
                if (iErrorCode != 0)
                {
                    sErrorMsg = Global.NewCompany.GetLastErrorDescription();

                }
                if (Global.NewCompany.Connected == true)
                {

                }

            }
            catch
            {
                //SBO_Application.MessageBox(Global.oCompny2.GetLastErrorDescription().ToString(), 1, "Ok", "", "");
            }
        }
        #endregion
        //-------------------------------------------------------------------------------------------------------------//
        //Change By Dhayalan For Hardware Key
        //Need To Enter A HardwareKey
        #region HardwareKey
        public static void HardwareKey()
        {
            try
            {
              //Global.HWKEY = new string[] { "F0123559701", "N2092941383", "Y1334940735", "T1796204260" };
                Global.HWKEY = new string[] { "N2092941383", "F1534030594", "Y1334940735", "D1206114874", "A0061802481", "T1796204260", "Q0021813522", "K1679825911", "A0335651095", "E0649908341", "K0718181110" };
            }
            catch
            {
            }

        }
#endregion
        //-------------------------------------------------------------------------------------------------------------//
    }
}

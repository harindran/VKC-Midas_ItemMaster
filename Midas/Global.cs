using System;
using System.Collections.Generic;
using System.Text;

namespace VKC
{
   public  class Global
    {
       public static SAPbobsCOM.Company SapCompany;
       public static SAPbobsCOM.Company NewCompany;
       public static SAPbouiCOM.Application SapApplication;

       public static string BatchCreator = "";
       public static string SerialNo = "";
       public static bool bubblevalue = false;
       public static bool bubbleUnit = false;
       public static string[] HWKEY=new String[16] ;
   }
}

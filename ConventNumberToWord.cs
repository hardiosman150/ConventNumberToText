
namespace ConventNumberToWord
{
    using System;
    class ConventNumberToKurdish
    {
        private static string ones(string Number)
        {
            int _Number = Convert.ToInt32(Number);
            string name = "";
            switch (_Number)
            {
                case 1:
                    name = "یەک";
                    break;
                case 2:
                    name = "دوو";
                    break;
                case 3:
                    name = "سێ";
                    break;
                case 4:
                    name = "چوار";
                    break;
                case 5:
                    name = "پێنج";
                    break;
                case 6:
                    name = "شەش";
                    break;
                case 7:
                    name = "حەوت";
                    break;
                case 8:
                    name = "هەشت";
                    break;
                case 9:
                    name = "نۆ";
                    break;
            }
            return name;
        }
        private static string tens(string Number)
        {
            int _Number = Convert.ToInt32(Number);
            string name = null;
            switch (_Number)
            {
                case 10:
                    name = "دە";
                    break;
                case 11:
                    name = "یانزە";
                    break;
                case 12:
                    name = "دوانزدە";
                    break;
                case 13:
                    name = "سیانزە";
                    break;
                case 14:
                    name = "چواردە";
                    break;
                case 15:
                    name = "پانزدە";
                    break;
                case 16:
                    name = "شانزدە";
                    break;
                case 17:
                    name = "حەڤدە";
                    break;
                case 18:
                    name = "هەژدە";
                    break;
                case 19:
                    name = "نۆزدە";
                    break;
                case 20:
                    name = "بیست";
                    break;
                case 30:
                    name = "سی";
                    break;
                case 40:
                    name = "چل";
                    break;
                case 50:
                    name = "پەنجا";
                    break;
                case 60:
                    name = "شەست";
                    break;
                case 70:
                    name = "حەفتا";
                    break;
                case 80:
                    name = "هەشتا";
                    break;
                case 90:
                    name = "نەود";
                    break;
                default:
                    if (_Number > 0)
                    {
                        name = tens(Number.Substring(0, 1) + "0") + "  " + ones(Number.Substring(1));
                    }
                    break;
            }
            return name;
        }
        private static string ConvertWholeNumber(string Number)
        {
            string word = "";
            try
            {
                bool beginsZero = false;//tests for 0XX    
                bool isDone = false;//test if already translated    
                double dblAmt = (Convert.ToDouble(Number));
                //if ((dblAmt > 0) && number.StartsWith("0"))    
                if (dblAmt > 0)
                {//test for zero or digit zero in a nuemric    
                    beginsZero = Number.StartsWith("0");

                    int numDigits = Number.Length;
                    int pos = 0;//store digit grouping    
                    string place = "";//digit grouping name:hundres,thousand,etc...    
                    switch (numDigits)
                    {
                        case 1://ones' range    

                            word = ones(Number);
                            isDone = true;
                            break;
                        case 2://tens' range    
                            word = tens(Number);
                            isDone = true;
                            break;
                        case 3://hundreds' range    
                            pos = (numDigits % 3) + 1;
                            place = " سەد ";
                            break;
                        case 4://thousands' range    
                        case 5:
                        case 6:
                            pos = (numDigits % 4) + 1;
                            place = " هەزار ";
                            break;
                        case 7://millions' range    
                        case 8:
                        case 9:
                            pos = (numDigits % 7) + 1;
                            place = " میلوێن ";
                            break;
                        case 10://Billions's range    
                        case 11:
                        case 12:

                            pos = (numDigits % 10) + 1;
                            place = " بیلوێن ";
                            break;
                        //add extra case options for anything above Billion...    
                        default:
                            isDone = true;
                            break;
                    }
                    if (!isDone)
                    {//if transalation is not done, continue...(Recursion comes in now!!)    
                        if (Number.Substring(0, pos) != "0" && Number.Substring(pos) != "0")
                        {
                            try
                            {
                                word = ConvertWholeNumber(Number.Substring(0, pos)) + place + " و " + ConvertWholeNumber(Number.Substring(pos));
                            }
                            catch { }
                        }
                        else
                        {
                            word = ConvertWholeNumber(Number.Substring(0, pos)) + ConvertWholeNumber(Number.Substring(pos));
                        }

                        //check for trailing zeros    
                        //if (beginsZero) word = " and " + word.Trim();    
                    }
                    //ignore digit grouping names    
                    if (word.Trim().Equals(place.Trim())) word = "";
                }
            }
            catch { }
            return word.Trim();
        }
        // Multiple parameters.
        /// <param name="numb">Just Number when value gereter than -1.</param>
        public static string ConvertToWords(string numb)
        {
            string val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";

            try
            {
                int decimalPlace = numb.IndexOf(".");
                if (decimalPlace > 0)
                {
                    wholeNo = numb.Substring(0, decimalPlace);
                    points = numb.Substring(decimalPlace + 1);
                    if (Convert.ToInt32(points) > 0)
                    {
                        andStr = " لەگەڵ   ";// just to separate whole numbers from points/cents    
                                             // endStr = "Paisa" + endStr;//Cents    
                        pointStr = ConvertDecimals(points);
                    }
                }
                val = string.Format("{0} {1}{2}", ConvertWholeNumber(wholeNo).Trim(), andStr, pointStr);
            }
            catch { }
            return val;
        }
        private static string ConvertDecimals(string number)
        {
            string cd = "", digit = "", engOne = "";
            //for (int i = 0; i < number.Length; i++)
            //{
            //    digit = number[i].ToString();
            //    if (digit.Equals("0"))
            //    {
            //        engOne = "سفر";
            //    }
            //    else
            //    {
            //        engOne = ones(digit);
            //    }
            //    cd += " " + engOne;
            //}
            int len = number.Length;
            switch (len)
            {
                case 1:
                    cd = cd + ones(number);
                    break;
                case 2:
                    cd = cd + tens(number);
                    break;
                default:
                    cd = cd + ConvertWholeNumber(number);
                    break;
            }
            return cd + " پۆینت ";
        }
    }

    class ConventNumberToEnglish
    {
        private static string ones(string Number)
        {
            int _Number = Convert.ToInt32(Number);
            string name = "";
            switch (_Number)
            {

                case 1:
                    name = "One";
                    break;
                case 2:
                    name = "Two";
                    break;
                case 3:
                    name = "Three";
                    break;
                case 4:
                    name = "Four";
                    break;
                case 5:
                    name = "Five";
                    break;
                case 6:
                    name = "Six";
                    break;
                case 7:
                    name = "Seven";
                    break;
                case 8:
                    name = "Eight";
                    break;
                case 9:
                    name = "Nine";
                    break;
            }
            return name;
        }
        private static string tens(string Number)
        {
            int _Number = Convert.ToInt32(Number);
            string name = null;
            switch (_Number)
            {
                case 10:
                    name = "Ten";
                    break;
                case 11:
                    name = "Eleven";
                    break;
                case 12:
                    name = "Twelve";
                    break;
                case 13:
                    name = "Thirteen";
                    break;
                case 14:
                    name = "Fourteen";
                    break;
                case 15:
                    name = "Fifteen";
                    break;
                case 16:
                    name = "Sixteen";
                    break;
                case 17:
                    name = "Seventeen";
                    break;
                case 18:
                    name = "Eighteen";
                    break;
                case 19:
                    name = "Nineteen";
                    break;
                case 20:
                    name = "Twenty";
                    break;
                case 30:
                    name = "Thirty";
                    break;
                case 40:
                    name = "Fourty";
                    break;
                case 50:
                    name = "Fifty";
                    break;
                case 60:
                    name = "Sixty";
                    break;
                case 70:
                    name = "Seventy";
                    break;
                case 80:
                    name = "Eighty";
                    break;
                case 90:
                    name = "Ninety";
                    break;
                default:
                    if (_Number > 0)
                    {
                        name = tens(Number.Substring(0, 1) + "0") + " " + ones(Number.Substring(1));
                    }
                    break;
            }
            return name;
        }
        private static String ConvertWholeNumber(String Number)
        {
            string word = "";
            try
            {
                bool beginsZero = false;//tests for 0XX    
                bool isDone = false;//test if already translated    
                double dblAmt = (Convert.ToDouble(Number));
                //if ((dblAmt > 0) && number.StartsWith("0"))    
                if (dblAmt > 0)
                {//test for zero or digit zero in a nuemric    
                    beginsZero = Number.StartsWith("0");

                    int numDigits = Number.Length;
                    int pos = 0;//store digit grouping    
                    string place = "";//digit grouping name:hundres,thousand,etc...    
                    switch (numDigits)
                    {
                        case 1://ones' range    

                            word = ones(Number);
                            isDone = true;
                            break;
                        case 2://tens' range    
                            word = tens(Number);
                            isDone = true;
                            break;
                        case 3://hundreds' range    
                            pos = (numDigits % 3) + 1;
                            place = " Hundred ";
                            break;
                        case 4://thousands' range    
                        case 5:
                        case 6:
                            pos = (numDigits % 4) + 1;
                            place = " Thousand ";
                            break;
                        case 7://millions' range    
                        case 8:
                        case 9:
                            pos = (numDigits % 7) + 1;
                            place = " Million ";
                            break;
                        case 10://Billions's range    
                        case 11:
                        case 12:

                            pos = (numDigits % 10) + 1;
                            place = " Billion ";
                            break;
                        //add extra case options for anything above Billion...    
                        default:
                            isDone = true;
                            break;
                    }
                    if (!isDone)
                    {//if transalation is not done, continue...(Recursion comes in now!!)    
                        if (Number.Substring(0, pos) != "0" && Number.Substring(pos) != "0")
                        {
                            try
                            {
                                word = ConvertWholeNumber(Number.Substring(0, pos)) + place + ConvertWholeNumber(Number.Substring(pos));
                            }
                            catch { }
                        }
                        else
                        {
                            word = ConvertWholeNumber(Number.Substring(0, pos)) + ConvertWholeNumber(Number.Substring(pos));
                        }

                        //check for trailing zeros    
                        //if (beginsZero) word = " and " + word.Trim();    
                    }
                    //ignore digit grouping names    
                    if (word.Trim().Equals(place.Trim())) word = "";
                }
            }
            catch { }
            return word.Trim();
        }
        private static string ConvertDecimals(string number)
        {
            string cd = "", digit = "", engOne = "";
            for (int i = 0; i < number.Length; i++)
            {
                digit = number[i].ToString();
                if (digit.Equals("0"))
                {
                    engOne = "Zero";
                }
                else
                {
                    engOne = ones(digit);
                }
                cd += " " + engOne;
            }
            return cd;
        }
        // Multiple parameters.
        /// <param name="numb">Just Number when value gereter than -1.</param>

        public static string ConvertToWords(string numb)
        {
            string val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
            string endStr = "Only";
            try
            {
                int decimalPlace = numb.IndexOf(".");
                if (decimalPlace > 0)
                {
                    wholeNo = numb.Substring(0, decimalPlace);
                    points = numb.Substring(decimalPlace + 1);
                    if (Convert.ToInt32(points) > 0)
                    {
                        andStr = "and";// just to separate whole numbers from points/cents    
                        endStr = "Paisa " + endStr;//Cents    
                        pointStr = ConvertDecimals(points);
                    }
                }
                val = string.Format("{0} {1}{2} {3}", ConvertWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);
            }
            catch { }
            return val;
        }
    }
}

namespace ConventNumberLanguge_A_K_EN
{
    class ConventNKurdishToEnglish
    {
        // Multiple parameters.
        /// <param name="Numbers">Please number file Number English the result is number Kurdish or Arabic languge .</param>
        public static string Convert(string Numbers)
        {
            string result = "";
            foreach (var c in Numbers)
                result += Convert(c);
            return result;
        }
        // Multiple parameters.
        /// <param name="number">Please number file Number English the result is number Kurdish or Arabic languge .</param>
        public static char? Convert(char number)
        {
            switch (number)
            {
                case '٠':
                    return '0';
                case '١':
                    return '1';
                case '٢':
                    return '2';
                case '٣':
                    return '3';
                case '٤':
                    return '4';
                case '٥':
                    return '5';
                case '٦':
                    return '6';
                case '٧':
                    return '7';
                case '٨':
                    return '8';
                case '٩':
                    return '9';
                case '،':
                    return '.';

                default:
                    return number;
            }
        }
    }
    public class ConventNEnglishToKurdish
    {
        // Multiple parameters.
        /// <param name="Numbers">Please number file Number English the result is number Kurdish or Arabic languge .</param>
        public static string Convert(string Numbers)
        {
            string Result = "";
            foreach (var number in Numbers)
            {
                Result += Convert(number);
            }
            return Result;
        }
        // Multiple parameters.
        /// <param name="number">Please number file Number English the result is number Kurdish or Arabic languge .</param>
        public static char? Convert(char Number)
        {
            switch (Number)
            {
                case '0':
                    return '٠';
                case '1':
                    return '١';
                case '2':
                    return '٢';
                case '3':
                    return '٣';
                case '4':
                    return '٤';
                case '5':
                    return '٥';
                case '6':
                    return '٦';
                case '7':
                    return '٧';
                case '8':
                    return '٨';
                case '9':
                    return '٩';
                case '.':
                    return '،';

                default:
                    return Number;
            }
        }
    }

}
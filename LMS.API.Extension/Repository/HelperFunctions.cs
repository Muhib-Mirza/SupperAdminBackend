using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APL.API.Extensions.Repository
{
    public class HelperFunctions
    {
        public static string GenerateUniqueNumber(string latestUniqueNo, string farmNo = null)
        {
            var uniqueNo = string.Empty;
            DateTime currentDate = DateTime.Now;
            string month = currentDate.ToString("MM");
            string Year = currentDate.ToString("yy");
            if(farmNo != null && farmNo != "")
            {
                if (latestUniqueNo != null && latestUniqueNo != "")
                {
                    string[] parts = latestUniqueNo.Split('-');
                    string lastPart = parts[parts.Length - 1];
                    string key = string.Join("-", parts.Take(parts.Length - 1));
                    if (key == $"{farmNo}-{month}{Year}")
                    {
                        uniqueNo = GenerateReferenceSerialNumber(month, Year, lastPart, farmNo);
                    }
                    else
                    {
                        uniqueNo = GenerateReferenceSerialNumber(month, Year, null, farmNo);
                    }
                }
                else
                {
                    uniqueNo = GenerateReferenceSerialNumber(month, Year, null, farmNo);
                }
            }
            else
            {
                if (latestUniqueNo != null && latestUniqueNo != ""){
                    string[] parts = latestUniqueNo.Split('-');
                    string lastPart = parts[parts.Length - 1];
                    string key = string.Join("-", parts.Take(parts.Length - 1));
                    if(key == $"{month}{Year}")
                    {
                        uniqueNo = GenerateReferenceSerialNumber(month, Year, lastPart);
                    }
                    else
                    {
                        uniqueNo = GenerateReferenceSerialNumber(month, Year);
                    }
                }
                else
                {
                    uniqueNo = GenerateReferenceSerialNumber(month, Year);
                }
            }
            return uniqueNo;
        }
        public static string GenerateReferenceSerialNumber(string twoDigitMonthNo, string twoDigitYearNo, string latestSerialNo = null, string refrenceNo = null)
        {
            string nextNumber = "A001";
            string refSerialNo = "";
            if (latestSerialNo != null)
            {
                string currentNumber = latestSerialNo;
                nextNumber = IncrementNumber(currentNumber);
            }
            if (refrenceNo != null)
            {
                refSerialNo = $"{refrenceNo}-{twoDigitMonthNo}{twoDigitYearNo}-{nextNumber}";
            }
            else
            {
                refSerialNo = $"{twoDigitMonthNo}{twoDigitYearNo}-{nextNumber}";
            }
            return refSerialNo;
        }
        public static string IncrementNumber(string number)
        {
            char prefix = number[0];
            int lastNumber = int.Parse(number.Substring(1));

            lastNumber++;

            if (lastNumber > 999)
            {
                prefix++;
                lastNumber = 1;
            }

            return $"{prefix}{lastNumber:D3}";
        }






        public static string GenerateFarmUniqueNo(string latestSerialNo = null)
        {
            if (latestSerialNo == null)
            {
                return "AAA0001";
            }

            char[] alphabetArray = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            int alphabetIndex1 = (int)(latestSerialNo[0]) - 65;
            int alphabetIndex2 = (int)(latestSerialNo[1]) - 65;
            int alphabetIndex3 = (int)(latestSerialNo[2]) - 65;
            int number = int.Parse(latestSerialNo.Substring(3));

            if (alphabetIndex1 == 25 && alphabetIndex2 == 25 && alphabetIndex3 == 25 && number == 9999)
            {
                throw new Exception("FarmUniqueNo: Unable to generate unique number. Maximum limit reached.");
            }

            if (number == 9999)
            {
                if (alphabetIndex3 == 25)
                {
                    if (alphabetIndex2 == 25)
                    {
                        if (alphabetIndex1 == 25)
                        {
                            throw new Exception("FarmUniqueNo: Unable to generate unique number. Maximum limit reached.");
                        }
                        else
                        {
                            alphabetIndex1++;
                            alphabetIndex2 = 0;
                            alphabetIndex3 = 0;
                        }
                    }
                    else
                    {
                        alphabetIndex2++;
                        alphabetIndex3 = 0;
                    }
                }
                else
                {
                    alphabetIndex3++;
                }

                number = 1;
            }
            else
            {
                number++;
            }

            string nextNumber = $"{alphabetArray[alphabetIndex1]}{alphabetArray[alphabetIndex2]}{alphabetArray[alphabetIndex3]}{number:D4}";
            return nextNumber;
        }

    }
}

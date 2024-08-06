using System;

namespace MizeApi.Helper
{
    public static class CommonExtension
    {
        public static decimal ObjToDecimal(this object input, decimal alternativeValue)
        {
            if (input == null)
                return alternativeValue;
            else
            {
                try
                {
                    return Convert.ToDecimal(input);
                }
                catch
                {
                    return alternativeValue;
                }

            }
        }
        public static decimal ObjToDecimalN2(this object input, decimal alternativeValue)
        {
            if (input == null)
                return alternativeValue;
            else
            {
                try
                {
                    return Math.Round(Convert.ToDecimal(input), 2);
                }
                catch
                {
                    return alternativeValue;
                }

            }
        }
        public static decimal ObjToDecimalNX(this object input, decimal alternativeValue,int x)
        {
            if (input == null)
                return alternativeValue;
            else
            {
                try
                {
                    return Math.Round(Convert.ToDecimal(input), x);
                }
                catch
                {
                    return alternativeValue;
                }

            }
        }
        public static string ObjToString(this object input, string alternativeValue)
        {
            if (input == null)
                return alternativeValue;
            else
            {
                try
                {
                    return input.ToString();
                }
                catch
                {
                    return alternativeValue;
                }

            }
        }
        public static string ObjToStringIfIsNullOrEmpty(this object input, string alternativeValue)
        {
            if (input == null)
                return alternativeValue;
            else
            {
                try
                {
                    if (input.ToString() != string.Empty)
                        return input.ToString();
                    else
                        return alternativeValue;
                }
                catch
                {
                    return alternativeValue;
                }

            }
        }
        public static int ObjToInt(this object input, int alternativeValue)
        {
            if (input == null)
                return alternativeValue;
            else
            {
                try
                {
                    return Convert.ToInt32(input);
                }
                catch
                {
                    return alternativeValue;
                }

            }
        }
        public static bool ObjToBoolean(this object input, bool alternativeValue)
        {
            if (input == null)
                return alternativeValue;
            else
            {
                try
                {
                    return Convert.ToBoolean(input);
                }
                catch
                {
                    return alternativeValue;
                }

            }
        }
        public static DateTime ObjToDateElseToday(this object input)
        {
            DateTime alternativeValue = DateTime.Now.Date;
            if (input == null)
                return alternativeValue;
            else
            {
                try
                {
                    return Convert.ToDateTime(input);
                }
                catch
                {
                    return alternativeValue;
                }

            }
        }
        public static DateTime ObjToDateTimeElseNow(this object input)
        {
            DateTime alternativeValue = DateTime.Now;
            if (input == null)
                return alternativeValue;
            else
            {
                try
                {
                    return Convert.ToDateTime(input);
                }
                catch
                {
                    return alternativeValue;
                }

            }
        }
    }
}

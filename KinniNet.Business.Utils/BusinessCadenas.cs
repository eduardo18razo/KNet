using System;
using System.Collections.Generic;
using System.Globalization;

namespace KinniNet.Business.Utils
{
    public static class BusinessCadenas
    {
        public static class Fechas
        {
            public static DateTime ObtenerFechaInicioSemana(int anio, int numeroSemana)
            {
                DateTime result;
                try
                {
                    DateTime jan1 = new DateTime(anio, 1, 1);
                    int daysOffset = DayOfWeek.Tuesday - jan1.DayOfWeek;
                    DateTime firstMonday = jan1.AddDays(daysOffset);
                    var cal = CultureInfo.CurrentCulture.Calendar;
                    int firstWeek = cal.GetWeekOfYear(jan1, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                    int weekNum = numeroSemana;
                    if (firstWeek <= 1)
                    {
                        weekNum -= 1;
                    }

                    result = firstMonday.AddDays(weekNum*7 + 0 - 1);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return result;
            }

            public static DateTime ObtenerFechaFinSemana(int anio, int numeroSemana)
            {
                DateTime result;
                try
                {
                    DateTime jan1 = new DateTime(anio, 1, 1);
                    int daysOffset = DayOfWeek.Tuesday - jan1.DayOfWeek;
                    DateTime firstMonday = jan1.AddDays(daysOffset);
                    var cal = CultureInfo.CurrentCulture.Calendar;
                    int firstWeek = cal.GetWeekOfYear(jan1, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                    int weekNum = numeroSemana;
                    if (firstWeek <= 1)
                    {
                        weekNum -= 1;
                    }
                    result = firstMonday.AddDays(weekNum * 7 + 0 - 1).AddDays(6);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return result;
            }

            public static List<DateTime> ObtenerRangoFechasNumeroSemana(int anio, int numeroSemana)
            {
                List<DateTime> result = new List<DateTime>();
                try
                {
                    DateTime jan1 = new DateTime(anio, 1, 1);
                    int daysOffset = DayOfWeek.Tuesday - jan1.DayOfWeek;
                    DateTime firstMonday = jan1.AddDays(daysOffset);
                    var cal = CultureInfo.CurrentCulture.Calendar;
                    int firstWeek = cal.GetWeekOfYear(jan1, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                    int weekNum = numeroSemana;
                    if (firstWeek <= 1)
                    {
                        weekNum -= 1;
                    }
                    result.Add(firstMonday.AddDays(weekNum * 7 + 0 - 1));
                    result.Add(result[0].AddDays(6));
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return result;
            }
        }
        
    }
}

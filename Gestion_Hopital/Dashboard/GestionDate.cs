using System;
using System.Globalization;

namespace Gestion_Hopital.Dashboard
{
    class GestionDate
    {
        public static int NumeroSemaineEnCours(int i)
        {
            DateTimeFormatInfo dateforminfo = DateTimeFormatInfo.CurrentInfo;
            DateTime ajd = DateTime.Today.AddDays(i * 7);
            GregorianCalendar Calendrier = new GregorianCalendar();

            return Calendrier.GetWeekOfYear(ajd, dateforminfo.CalendarWeekRule, dateforminfo.FirstDayOfWeek);
        }

        public static DateTime Date_Premier_J_Semaine(int i)
        {
            DateTimeFormatInfo dateforminfo = DateTimeFormatInfo.CurrentInfo;
            GregorianCalendar Calendrier = new GregorianCalendar();
            // On récupère le premier jour de l'année en cours
            DateTime premier_j_annee = new DateTime(DateTime.Today.Year, 1, 1);
            // Calcule de l'offset pur déterminer la première semaine selon la culture ( une semaine complète contient 4 jours minimum)
            int JourOffset = (int)dateforminfo.FirstDayOfWeek - (int)premier_j_annee.DayOfWeek;
            // On calcule du premier de la véritale première semaine
            DateTime Date_premiere_semaine = premier_j_annee.AddDays(JourOffset);
            int Num_premiere_semaine = Calendrier.GetWeekOfYear(premier_j_annee, dateforminfo.CalendarWeekRule,
                dateforminfo.FirstDayOfWeek);
            int Num_semaine = NumeroSemaineEnCours(i), nbr_semaine = 52;

            // On vérifie les année qui comptent 53 semaines sur l'année
            if (DateTime.Today.Year == 2020 || DateTime.Today.Year == 2026 || DateTime.Today.Year == 2032 ||
                DateTime.Today.Year == 2037 || DateTime.Today.Year == 2048)
            {
                nbr_semaine = 53;
            }

            // On rectifie le numéro de la semaine en cours
            if ((Num_premiere_semaine <= 1 || Num_premiere_semaine >= nbr_semaine) && JourOffset >= -3)
            {
                Num_semaine -= 1;
            }

            // On calcule le premier jour de la semaine en cours
            return Date_premiere_semaine.AddDays(Num_semaine * 7);
        }

    }
}

using System.Collections.Generic;

namespace C969_Performance_Assessment
{
    static class GlobalVariables
    {
        static public List<Appointment> appointmentList = new List<Appointment>();

        static public string DbConn { get; set; }
        static public bool LoggedIn { get; set; }
        static public int UserId { get; set; }
        static public string UserName { get; set; }
    }
}
using EscuelaDeCienciasEconomicas.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace EscuelaDeCienciasEconomicas.DAL
{
    public class RaptorAppContext: ContextBoundObject
    {
        public static String SESSION_USER_OBJ = "USER_OBJ";

        public static String PAGINA_ERROR = "Error";
        public static String PAGINA_PERMISOS_INSUFICIENTES = "Deny";
        public static String SOLICITUD_ERRONEA = "Los datos para la solicitud no son correctos. <br />Por favor, vuelve al inicio he intentalo de nuevo.";
        public static String REGISTRO_NO_ENCONTRADO = "El registro al que intentas acceder no existe o ya no esta disponible. <br />Por favor, vuelve al inicio he intentalo de nuevo.";
        public static String PAGINA_NO_ENCONTRADA = "La página que buscas o intentas acceder no existe o no esta disponible. <br />Por favor, vuelve al inicio he intentalo de nuevo.";
        public static String ERROR_INTERNO = "Parece que la página que buscas o intentas acceder tiene problemas. <br />Por favor, vuelve al inicio he intentalo de nuevo.";

        //public static String IMAGE_PATH = "Images";

        public static Boolean isSessionActive()
        {
            if (System.Web.HttpContext.Current.Session == null || System.Web.HttpContext.Current.Session.IsNewSession)
            {
                return false;
            }
            return true;
        }

        public static void setSessionVAR(string var, string value) { 
            System.Web.HttpContext.Current.Session[var] = value;
        }

        public static String getSessionVAR(string var)
        {
            return System.Web.HttpContext.Current.Session[var] as String;
        }

        public static void setSessionObj(string var, Object value)
        {
            System.Web.HttpContext.Current.Session[var] = value;
        }

        public static Object getSessionObj(string var)
        {
            return System.Web.HttpContext.Current.Session[var];
        }

        public static String Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static String Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static bool IsBase64String(string value)
        {
            value = value.Trim();
            return (value.Length % 4 == 0) && Regex.IsMatch(value, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }

        public static String MaskString(string value)
        {
            return value.Substring(value.Length).PadLeft(value.Length, '*');
        }

        public static void sendMailTest(string url)
        {
            // Command line argument must the the SMTP host.
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "in-v3.mailjet.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("e4144e67e607e78760404ab936b6aac7", "c771c39b7ab3eb668167a524fa34528f");
            client.EnableSsl = true;
            MailMessage mm = new MailMessage("donotreply@raptor-enel.com", "cristian.ariel.paredes@ericsson.com", "Recuperación de contraseña", url);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);

        }

    }
}
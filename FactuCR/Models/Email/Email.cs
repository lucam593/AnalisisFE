using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FactuCR.Models.Email
{
    public class Email
    {
        string From = "sandy.valdez@ucr.ac.cr"; //de quien procede, puede ser un alias
        string To;  //a quien vamos a enviar el mail
        string Body = "Estos son los documentos generados por el proceso de Facturacion";  //mensaje
        string Subject = "Documentos de Facturacion"; //Subject
        List<string> Archivo = new List<string>(); //lista de archivos a enviar
        string DE = "sandy.valdez@ucr.ac.cr"; //smtp user
        string PASS = "mD2oJHTfX3"; //smtp password
        byte[] decodedFile;
        string fileName;

        MailMessage email;

        public string error = "";
        
        public Email(string _To, byte[] _decodedFile, string _fileName)
        {
            To = _To;
            decodedFile = _decodedFile;
            fileName = _fileName;
        }
        
        // metodo que envia el mail
        public bool sendEmail()
        {

            //una validación básica
            if (To.Trim().Equals("") || Body.Trim().Equals("") || Subject.Trim().Equals(""))
            {
                error = "El mail, el asunto y el mensaje son obligatorios";
                return false;
            }

            //aqui comenzamos el proceso
            //comienza-------------------------------------------------------------------------
            try
            {
                email = new MailMessage();
                email.From = new MailAddress(From); //definimos la direccion de procedencia
                email.To.Add(To);
                email.Subject = Subject;
                email.Body = Body;
                email.IsBodyHtml = false; //definimos si el contenido sera html
                email.Priority = MailPriority.Normal;

                Attachment attachPDF = new Attachment(new MemoryStream(decodedFile), fileName);

                email.Attachments.Add(attachPDF);

                /*
                //si viene archivo a adjuntar
                //realizamos un recorrido por todos los adjuntos enviados en la lista
                //la lista se llena con direcciones fisicas, por ejemplo: c:/pato.txt
                if (Archivo != null)
                {
                    //agregado de archivo
                    foreach (string archivo in Archivo)
                    {
                        //comprobamos si existe el archivo y lo agregamos a los adjuntos
                        if (System.IO.File.Exists(@archivo))
                        { 
                            email.Attachments.Add(new Attachment());
                        }
                    }
                }*/

                //aqui creamos un objeto tipo SmtpClient el cual recibe el servidor que utilizaremos como smtp
                //en este caso me colgare de gmail
                SmtpClient smtpMail = new SmtpClient();

                smtpMail.EnableSsl = false;//le definimos si es conexión ssl
                smtpMail.UseDefaultCredentials = false; //le decimos que no utilice la credencial por defecto
                smtpMail.Host = "smtp-pulse.com"; //agregamos el servidor smtp
                smtpMail.Port = 2525; //le asignamos el puerto, en este caso gmail utiliza el 465
                smtpMail.Credentials = new NetworkCredential(DE, PASS); //agregamos nuestro usuario y pass de gmail

                //enviamos el mail
                smtpMail.Send(email);

                //eliminamos el objeto
                smtpMail.Dispose();

                //regresamos true
                return true;
            }
            catch (Exception ex)
            {
                //si ocurre un error regresamos false y el error
                error = "Ocurrio un error: " + ex.Message;
                return false;
            }
        }
    }
}

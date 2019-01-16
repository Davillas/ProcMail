using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using EAGetMail;
using System.Threading;
using ProcMail.Properties;
using System.Windows.Forms;


namespace ProcMail
{
    static class MainProgram
    {
        static public string directory = Properties.Settings.Default.Path;
        static public string mailAdress = Properties.Settings.Default.MailAdress;
        static public string mailPassword = Properties.Settings.Default.MailPassword;
        static public string site = Properties.Settings.Default.SiteName;
        static public bool delete = Properties.Settings.Default.DeleteMails;
        static public int latency = Properties.Settings.Default.Latency;
        static public string[] filters = new string[Properties.Settings.Default.Filter.Count];

        static public bool notification = false;

        static public void FilterAssign()
        {
            
            for (int i = 0; i < Properties.Settings.Default.Filter.Count; i++)
            {
                filters[i] = Properties.Settings.Default.Filter[i];
            }
         
        }

        static public void FilterCheck()
        {


            
        }

        static public string RRS(string str)
        {

            char[] restricted = { '/', ':', '<', '>', '*', '"', '?', '|' };
            foreach (char c in restricted)
            {

                bool check = str.Contains(c);
                if (check == true)
                {
                    string temp = String.Empty;
                    temp = str.Replace(c, ' ');
                    str = temp;
                }

            }
            return str;
        }

        public static void RunProgram()
        {
            string curpath = String.Format("{0}", directory);
            string mailbox = String.Format("{0}\\Входящие", curpath);

            //Определяет надобность появления уведомления
           
            // Если директории не существует, создать ее
            if (!Directory.Exists(mailbox))
            {
                Directory.CreateDirectory(mailbox);
            }

            MailServer oServer = new MailServer(String.Format("imap.{0}", site),
                        String.Format("{0}", mailAdress), String.Format("{0}", mailPassword), ServerProtocol.Imap4);
            MailClient oClient = new MailClient("");
            oClient.GetMailInfosParam.GetMailInfosOptions = GetMailInfosOptionType.NewOnly;

            FilterAssign();
            oServer.SSLConnection = true;
            oServer.Port = 993;
            //while (true)
            //{
                try
                {
                    oClient.Connect(oServer);
                    MailInfo[] infos = oClient.GetMailInfos();

                    for (int i = 0; i < infos.Length; i++)
                    {
                        MailInfo info = infos[i];
                        

                        // Получение письма с POP3 сервера
                        Mail oMail = oClient.GetMail(info);
                        string from = oMail.From.ToString();
                        string temp = oMail.Subject;

                        
                        string subject = temp.Replace("(Trial Version)", "");
                    
                    
                    

                    // Генерация файла письма на основе имени отправителя и даты
                    System.DateTime d = System.DateTime.Now;
                        System.Globalization.CultureInfo cur = new
                            System.Globalization.CultureInfo("ru-RU");
                        string sdate = d.ToString("yyyyMMddHHmmss", cur);
                        string syear = d.ToString("yyyy", cur);
                        string smonth = d.ToString("MM", cur);
                        string sday = d.ToString("dd", cur);

                        string crtD = String.Format("{0}\\{1}\\{2}\\{3}\\{4}\\{5}", mailbox, syear, smonth, sday, RRS(oMail.From.Name.ToString()).Trim(), RRS(subject).Trim());

                    // Пометить письмо как прочитанное
                    oClient.MarkAsRead(info, true);
                    // Обработка вложений
                    Attachment[] atts = oMail.Attachments;
                    int count = atts.Length;
                    bool danger = false;
                    string dangerPath = String.Format("{0}\\Подозрительные лица", directory.ToString());
                    string dangerName = String.Format("{0}\\{1}.txt", dangerPath, RRS(oMail.From.Name.ToString()));
                    for (int j = 0; j < count; j++)
                    {
                        Attachment att = atts[j];
                        string attname = String.Format("{0}\\{1}_{2}_{3}", crtD, RRS(oMail.From.Name.ToString()).Trim(), RRS(subject).Trim(), att.Name);


                        for (int k = 0; k < filters.Count(); k++)
                        {
                            if (attname.Contains(filters[k]))
                            {
                                danger = true;
                                //break;
                            }



                        }

                        if (danger == true)
                        {
                            notification = true;
                            if (!Directory.Exists(dangerPath))
                            {
                                Directory.CreateDirectory(dangerPath);
                            }
                            System.IO.File.WriteAllText(dangerName, String.Format("Письмо, присланное c почтового адреса {0}, содержит потенциально вредоносные файлы", oMail.From.Address));
                            break;

                        }
                        else
                        {
                            if (!System.IO.Directory.Exists(crtD) && danger == false)
                            {
                                System.IO.Directory.CreateDirectory(crtD);
                            }
                            att.SaveAs(attname, true);
                        }

                        
                    }
                    if (danger == false)
                    {
                        Directory.CreateDirectory(crtD);
                        string fileName = String.Format("{0}\\{1}_{2}_{3}_{4}.txt",

                        crtD, RRS(oMail.From.Name.ToString()).Trim(), RRS(subject).Trim(), RRS(sdate).Trim(), i);









                        //Исключение возможности попадания запрещенных символов в название файла
                        System.IO.File.WriteAllText(fileName, oMail.TextBody);

                    }

                        // Сохранить письмо на диск
                        //oMail.SaveAs(fileName, true);

                        

                        if(delete == true)
                        {
                            oClient.Delete(info);
                        }
                    }



                }
                catch (Exception ep)
                {
                }
                //Thread.Sleep(latency*1000);
            //}

        }

    

    

       

       
    }

}

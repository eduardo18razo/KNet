using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KinniNet.Business.Utils;

namespace KinniNet.Core.Demonio
{
    public class BusinessTicketMailService: IDisposable
    {
        private readonly bool _proxy;

        public void Dispose()
        {

        }

        public BusinessTicketMailService(bool proxy = false)
        {
            _proxy = proxy;
        }

        public void RecibeCorreos()
        {
            try
            {
                //TODO: Cambiar cliente por parametro
                new Imap4Mail.Retrieve().GetMails(BusinessVariables.EnumtServerImap.Gmail);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

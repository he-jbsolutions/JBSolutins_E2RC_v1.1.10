using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e2rcModel.BusinessLayer.Interface;

namespace e2rcModel.BusinessLayer
{
    public class Address :ICRUD<Address, long> 
    {
        public long? Address_ID { get; set; }

        public string City { get; set; }

        public State State { get; set; }

        public string MailingAddress { get; set; }
        public string MailingAddress2 { get; set; }

        public string ZipCode { get; set; }

        public virtual bool Create()
        {
            throw new NotImplementedException();
        }

        public virtual bool Edit()
        {
            throw new NotImplementedException();
        }

        public virtual bool Delete()
        {
            throw new NotImplementedException();
        }

        public virtual Address Single(long value)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<Address> List()
        {
            throw new NotImplementedException();
        }
    }
}

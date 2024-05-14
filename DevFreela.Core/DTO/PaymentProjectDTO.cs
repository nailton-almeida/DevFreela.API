using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.DTO
{
    public class PaymentProjectDTO
    {
        public PaymentProjectDTO(Guid IdProject, string CardName, string CardNumber, string CVV, string ValidateDate)
        {
            this.IdProject = IdProject;
            this.CardName = CardName;
            this.CardNumber = CardNumber;
            this.CVV = CVV;
            this.ValidateDate = ValidateDate;
        }

        public Guid IdProject { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string ValidateDate { get; set; }
    }
}

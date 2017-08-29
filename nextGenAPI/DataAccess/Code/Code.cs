using System;

namespace nextGenAPI.DataAccess.Codes {
    public class Template {
        public int CodeDetailId { get; set; }
        public string CodeValue { get; set; }
        public string CodeDescription { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }

        public Template(int codeDetailId, string codeValue, string codeDescription, string createdBy, DateTime createdDate, string modifiedBy, DateTime modifiedDate) {
            this.CodeDetailId = codeDetailId;
            this.CodeValue = codeValue;
            this.CodeDescription = codeDescription;
            this.CreatedBy = createdBy;
            this.CreatedDate = createdDate.ToShortDateString();
            this.ModifiedBy = modifiedBy;
            this.ModifiedDate = modifiedDate.ToShortDateString();
        }
    }
}